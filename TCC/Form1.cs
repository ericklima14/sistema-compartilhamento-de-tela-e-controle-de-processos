using System.Net;
using System.Net.Sockets;

namespace TCC
{
    public partial class Form1 : Form
    {
        private TcpListener listener;
        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {
            Task.Run(() => IniciarServidor());
            btnIniciarServidor.Enabled = false;
        }

        private void IniciarServidor()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8080);
                listener.Start();

                AtualizarLog("Servidor iniciado. Aguardando conexão do aluno...");

                client = listener.AcceptTcpClient();
                AtualizarLog("Aluno conectado!");

                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                while (client.Connected)
                {
                    string msg = reader.ReadLine();
                    if (msg != null)
                    {
                        AtualizarLog($"Aluno: {msg}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no servidor: {ex.Message}");
            }
        }

        private void AtualizarLog(string mensagem)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action(() => lstLog.Items.Add(mensagem)));
            }
            else
            {
                lstLog.Items.Add(mensagem);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if(writer != null && !string.IsNullOrEmpty(txtMensagem.Text))
            {
                writer.WriteLine(txtMensagem.Text);
                AtualizarLog($"Professor: {txtMensagem.Text}");
                txtMensagem.Clear();
            }
        }
    }
}
