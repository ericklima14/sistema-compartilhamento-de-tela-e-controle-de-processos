using System.Net.Sockets;

namespace Aluno
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = txtIpProfessor.Text;
                int port = 8080;

                client = new TcpClient();
                client.Connect(ip, port);

                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream)
                {
                    AutoFlush = true
                };

                btnConectar.Enabled = false;
                AtualizarLog("Conectado ao Professor");

                Task.Run(() => ReceberMensagem());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar: {ex.Message}");
            }
        }

        private async Task ReceberMensagem()
        {
            while (client.Connected)
            {
                try
                {
                    string msg = await reader.ReadLineAsync();

                    if (msg != null)
                    {
                        AtualizarLog($"Professor: {msg}");
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void AtualizarLog(string msg)
        {
            if (lstBox.InvokeRequired)
            {
                lstBox.Invoke(new Action(() => lstBox.Items.Add(msg)));
            }
            else
            {
                lstBox.Items.Add(msg);
            }

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if(writer != null && !string.IsNullOrEmpty(txtMensagem.Text))
            {
                writer.WriteLine(txtMensagem.Text);
                AtualizarLog($"Aluno: {txtMensagem.Text}");
                txtMensagem.Clear();
            }
        }
    }
}
