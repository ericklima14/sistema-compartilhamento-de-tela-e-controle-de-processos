using Professor;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private TcpListener listener;
        private Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();

        public FormProfessor()
        {
            InitializeComponent();
        }

        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {
            Task.Run(() => IniciarServidor());
            btnIniciarServidor.Enabled = false;
        }

        private async Task IniciarServidor()
        {
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            AtualizarLog("Servidor iniciado. Aguardando conexões dos alunos...");

            while (true)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();

                    Task.Run(() => HandleClientAsync(client));
                }
                catch (Exception ex)
                {
                    AtualizarLog($"Erro ao aceitar conexão: {ex.Message}");
                }
            }

        }

        private async Task HandleClientAsync(TcpClient client)
        {
            string clientIdentifier = client.Client.RemoteEndPoint.ToString(); ;
            NetworkStream stream = null;

            try
            {
                stream = client.GetStream();

                //usado para que duas threads nao escrevam ao mesmo tempo
                lock (client)
                {
                    clients.Add(client, clientIdentifier);
                }

                AtualizarLog($"Novo aluno conectado: {clientIdentifier}");
                AtualizarListaAlunos();

                while (client.Connected)
                {
                    byte[] lengthBuffer = new byte[4];
                    await ReadTotalBytesAsync(stream, lengthBuffer);
                    int messageLength = BitConverter.ToInt32(lengthBuffer, 0);

                    byte[] compressedMessage = new byte[messageLength];
                    await ReadTotalBytesAsync(stream, compressedMessage);

                    string mensagem = CompressionHelper.Decompress(compressedMessage);
                    AtualizarLog($"[{clientIdentifier}]: {mensagem}");

                    string broadcastMessage = $"[{clientIdentifier}]: {mensagem}";
                    await BroadcastMessage(broadcastMessage, client);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                lock (client)
                {
                    clients.Remove(client);
                }

                client.Close();
                AtualizarLog($"Aluno {clientIdentifier} desconectado.");
                AtualizarListaAlunos();
            }
        }

        private async Task BroadcastMessage(string message, TcpClient sender = null)
        {
            byte[] compressedMessage = CompressionHelper.Compress(message);
            byte[] lengthBuffer = BitConverter.GetBytes(compressedMessage.Length);

            List<TcpClient> clientsParaEnviar;
            lock (clients)
            {
                clientsParaEnviar = clients.Keys.ToList();
            }

            foreach (var client in clientsParaEnviar)
            {
                if (client != sender)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        await stream.WriteAsync(lengthBuffer, 0, lengthBuffer.Length);
                        await stream.WriteAsync(compressedMessage, 0, compressedMessage.Length);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private async Task ReadTotalBytesAsync(NetworkStream stream, byte[] buffer)
        {
            int totalBytesLidos = 0;
            while (totalBytesLidos < buffer.Length)
            {
                int bytesLidos = await stream.ReadAsync(buffer, totalBytesLidos, buffer.Length - totalBytesLidos);

                if (bytesLidos == 0)
                    throw new IOException("Conexão perdida.");

                totalBytesLidos += bytesLidos;
            }
        }

        private void AtualizarLog(string mensagem)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action(() =>
                {
                    lstLog.Items.Add(mensagem);
                    lstLog.TopIndex = lstLog.Items.Count - 1;
                }));
            }
            else
            {
                lstLog.Items.Add(mensagem);
                lstLog.TopIndex = lstLog.Items.Count - 1;
            }
        }

        private void AtualizarListaAlunos()
        {
            if (lstAlunosConectados.InvokeRequired)
            {
                lstAlunosConectados.Invoke(new Action(() =>
                {
                    lstAlunosConectados.Items.Clear();
                    lock (clients)
                    {
                        foreach (var clientName in clients.Values)
                        {
                            lstAlunosConectados.Items.Add(clientName);
                        }
                    }
                }));
            }
            else
            {
                lstAlunosConectados.Items.Clear();
                lock (clients)
                {
                    foreach (var clientName in clients.Values)
                    {
                        lstAlunosConectados.Items.Add(clientName);
                    }
                }
            }
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            if (clients.Count > 0 && !string.IsNullOrEmpty(txtMensagem.Text))
            {
                string message = $"Professor (Broadcast): {txtMensagem.Text}";
                AtualizarLog(message);
                await BroadcastMessage(message);
                txtMensagem.Clear();
            }
        }

        private void lstAlunosConectados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAlunosConectados.SelectedItem != null)
            {
                btnListarProcessos.Visible = true;
            }
            else
            {
                btnListarProcessos.Visible = false;
            }
        }

        private void lstAlunosConectados_Leave(object sender, EventArgs e)
        {
            if (!btnListarProcessos.Focused)
                lstAlunosConectados.SelectedItem = null;
        }

        private void btnListarProcessos_Click(object sender, EventArgs e)
        {
            lblProcessosAluno.Visible = true;
            clbProcessos.Visible = true;

            lblProcessosAluno.Text = $"Processos do aluno: {lstAlunosConectados.SelectedItem}";
        }
    }
}
