using Professor;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private TcpListener listener;
        private readonly Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();
        private string _alunoAtual = null;

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
            NetworkStream stream = client.GetStream(); ;

            try
            {
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

                    if (mensagem.StartsWith("RSP_PROCESS_LIST|"))
                    {
                        if (clientIdentifier == _alunoAtual)
                        {
                            string payload = mensagem.Substring("RSP_PROCESS_LIST|".Length);
                            string[] processNames = payload.Split('|');
                            AtualizarListaProcessos(processNames);
                        }
                    }
                    else
                    {
                        string broadcastMessage = $"[{clientIdentifier}]: {mensagem}";
                        AtualizarLog(broadcastMessage);
                        await BroadcastMessage(broadcastMessage, client);
                    }
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

        private void AtualizarListaProcessos(string[] processItems)
        {
            this.Invoke(new Action(() =>
            {
                // Antes de limpar a lista, salva os nomes de todos os processos que estão marcados.
                var checkedProcessNames = new HashSet<string>(
                    lvProcessos.CheckedItems.Cast<ListViewItem>().Select(item => item.Text)
                );

                int topItemIndex = 0;
                if (lvProcessos.TopItem != null)
                    topItemIndex = lvProcessos.TopItem.Index;


                lvProcessos.BeginUpdate();
                lvProcessos.Items.Clear();
                imageListProcessos.Images.Clear();

                imageListProcessos.Images.Add(SystemIcons.Application);

                foreach (var item in processItems)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    string[] parts = item.Split(";");
                    if (parts.Length < 2)
                        continue;

                    string processName = parts[0];
                    string processPathIcon = parts[1];
                    int iconIndex = 0;

                    try
                    {
                        if (!string.IsNullOrEmpty(processPathIcon))
                        {
                            Icon icon = Icon.ExtractAssociatedIcon(processPathIcon);
                            if (icon != null)
                            {
                                imageListProcessos.Images.Add(icon);
                                iconIndex = imageListProcessos.Images.Count - 1;
                            }

                        }
                    }
                    catch
                    {

                    }

                    ListViewItem listItem = new ListViewItem(processName, iconIndex);

                    if (checkedProcessNames.Contains(processName))
                    {
                        listItem.Checked = true;
                    }

                    lvProcessos.Items.Add(listItem);
                }

                lvProcessos.EndUpdate();

                if (lvProcessos.Items.Count > topItemIndex)
                {
                    lvProcessos.EnsureVisible(topItemIndex);
                }
            }));
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
                    await SendMessageAsync(client, message);
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

        //TODO: Fazer a verificacao para que o botao seja o proximo a ser clicado, e assim desselecionar os dois
        private void lstAlunosConectados_Leave(object sender, EventArgs e)
        {
            if (!btnListarProcessos.Focused)
                lstAlunosConectados.SelectedItem = null;
        }

        private async Task SendMessageAsync(TcpClient client, string message)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] compressedMessage = CompressionHelper.Compress(message);
                    byte[] lengthBuffer = BitConverter.GetBytes(compressedMessage.Length);
                    await stream.WriteAsync(lengthBuffer, 0, lengthBuffer.Length);
                    await stream.WriteAsync(compressedMessage, 0, compressedMessage.Length);
                }
                catch (Exception ex)
                {
                    AtualizarLog($"Erro ao enviar mensagem para {clients[client]}: {ex.Message}");
                }
            }
        }

        private async void btnListarProcessos_Click(object sender, EventArgs e)
        {
            if (lstAlunosConectados.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um aluno na lista.", "Nenhum Aluno Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblProcessosAluno.Visible = true;
            lvProcessos.Visible = true;

            lblProcessosAluno.Text = $"Processos do aluno: {lstAlunosConectados.SelectedItem}";

            string selectedIdentifier = lstAlunosConectados.SelectedItem.ToString();
            TcpClient targetClient = null;
            TcpClient oldClient = null;

            lock (clients)
            {
                targetClient = clients.FirstOrDefault(kvp => kvp.Value == selectedIdentifier).Key;

                if (_alunoAtual != null)
                    oldClient = clients.FirstOrDefault(kvp => kvp.Value == _alunoAtual).Key;
            }

            if (targetClient == null)
            {
                MessageBox.Show("Não foi possível encontrar o cliente selecionado. Ele pode ter se desconectado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedIdentifier == _alunoAtual)
            {
                await SendMessageAsync(targetClient, "CMD_STOP_MONITORING");
                AtualizarLog($"Monitoramento de processos parado para {selectedIdentifier}.");
                _alunoAtual = null;
                btnListarProcessos.Text = "Iniciar Monitoramento";
                lvProcessos.Items.Clear();
            }
            else
            {
                if (_alunoAtual != null)
                {
                    if (oldClient != null)
                    {
                        await SendMessageAsync(oldClient, "CMD_STOP_MONITORING");
                        AtualizarLog($"Monitoramento de processos parado para {_alunoAtual}.");
                    }
                }

                await SendMessageAsync(targetClient, "CMD_START_MONITORING");
                AtualizarLog($"Iniciando monitoramento de processos para {selectedIdentifier}...");
                _alunoAtual = selectedIdentifier;
                btnListarProcessos.Text = "Parar Monitoramento";
            }


        }

        private async void btnMatarProcesso_Click(object sender, EventArgs e)
        {

            if (_alunoAtual == null || lvProcessos.CheckedItems.Count == 0)
            {
                MessageBox.Show("Por favor, inicie o monitoramento de um aluno e selecione um processo na lista para matar.", "Ação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TcpClient targetClient = null;
            lock (clients)
            {
                targetClient = clients.FirstOrDefault(kvp => kvp.Value == _alunoAtual).Key;
            }
            if (targetClient == null)
            {
                MessageBox.Show("O aluno selecionado parece ter se desconectado. Não é possível enviar o comando.", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nomesProcessos = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(item => item.Text);
            string payload = string.Join("|", nomesProcessos);

            string mensagem = $"CMD_KILL_PROCESSES|{payload}";
            await SendMessageAsync(targetClient, mensagem);
            AtualizarLog($"Comando para matar os processos [{payload}] enviado para o aluno {_alunoAtual}.");
        }
    }
}
