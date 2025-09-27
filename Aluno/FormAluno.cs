using System.Net.Sockets;
using System.Threading.Tasks;

namespace Aluno
{
    public partial class FormAluno : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        public FormAluno()
        {
            InitializeComponent();
        }

        private async void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = txtIpProfessor.Text;
                int port = 8080;

                client = new TcpClient();
                await client.ConnectAsync(ip, port);
                stream = client.GetStream();

                btnConectar.Enabled = false;
                AtualizarLog("Conectado ao Professor");

                await Task.Run(() => ReceberMensagem());
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
                    byte[] lengthBuffer = new byte[4];
                    int totalBytesLidos = 0;

                    while (totalBytesLidos < lengthBuffer.Length){
                        int bytesLidos = await stream.ReadAsync(lengthBuffer, totalBytesLidos, lengthBuffer.Length - totalBytesLidos);
                        if (bytesLidos == 0)
                            throw new IOException("Conexao Perdida");
                        totalBytesLidos += bytesLidos;
                    }

                    int messageLength = BitConverter.ToInt32(lengthBuffer, 0);

                    byte[] compressedMessage = new byte[messageLength];
                    totalBytesLidos = 0;

                    while (totalBytesLidos < compressedMessage.Length)
                    {
                        int bytesLidos = await stream.ReadAsync(compressedMessage, totalBytesLidos, compressedMessage.Length - totalBytesLidos);
                        if (bytesLidos == 0)
                            throw new IOException("Conexao Perdida");
                        totalBytesLidos += bytesLidos;
                    }

                    string mensagem = CompressionHelper.Decompress(compressedMessage);
                    AtualizarLog($"Professor: {mensagem}");
                }
                catch (Exception)
                {
                    AtualizarLog("Conexão perdida.");
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

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            if(stream != null && !string.IsNullOrEmpty(txtMensagem.Text))
            {
                byte[] compressedMessage = CompressionHelper.Compress(txtMensagem.Text);

                byte[] lengthMessage = BitConverter.GetBytes(compressedMessage.Length);

                await stream.WriteAsync(lengthMessage, 0, lengthMessage.Length);

                await stream.WriteAsync(compressedMessage, 0, compressedMessage.Length);

                AtualizarLog($"Aluno: {txtMensagem.Text}");
                txtMensagem.Clear();
            }
        }
    }
}
