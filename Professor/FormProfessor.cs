using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private TcpListener listener;
        private readonly Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();
        private string _alunoAtual = null;
        private List<string> _processosBloqueados = new List<string>();
        private Dictionary<string, string> _caminhosDeIcone = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private CancellationTokenSource? _cancellationTokenSource;
        private Task? _ffmpegTask;
        private Process? _ffmpegProcess;
        private string? _sdpFilePath;
        private bool _isClosing = false; // Flag para controlar o fechamento ordenado

        public FormProfessor()
        {
            InitializeComponent();
            btnStopStream.Enabled = false;
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

                if (_processosBloqueados.Count > 0)
                {
                    string payload = string.Join("|", _processosBloqueados);
                    await SendMessageAsync(client, "CMD_UPDATE_BLOCKLIST|" + payload);
                    AtualizarLog($"Enviando lista de bloqueio atual para {clientIdentifier}.");
                }

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

                var iconCacheLocal = new Dictionary<string, int>();

                foreach (var item in processItems)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    int iconIndex = 0;

                    if (iconCacheLocal.ContainsKey(item))
                    {
                        iconIndex = iconCacheLocal[item];
                    }
                    else
                    {
                        if (_caminhosDeIcone.ContainsKey(item))
                        {
                            string caminhoIconeLocal = _caminhosDeIcone[item];

                            try
                            {
                                Icon icon = Icon.ExtractAssociatedIcon(caminhoIconeLocal);
                                if (icon != null)
                                {
                                    imageListProcessos.Images.Add(icon);
                                    iconIndex = imageListProcessos.Images.Count - 1;
                                }
                            }
                            catch
                            {

                            }
                        }

                        iconCacheLocal[item] = iconIndex;
                    }

                    ListViewItem listItem = new ListViewItem(item, iconIndex);

                    if (checkedProcessNames.Contains(item))
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

            var nomesProcessos = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(item => item.Text).ToList();
            string payload = string.Join("|", nomesProcessos);

            string mensagem = $"CMD_KILL_PROCESSES|{payload}";
            await SendMessageAsync(targetClient, mensagem);
            AtualizarLog($"Comando para matar os processos [{payload}] enviado para o aluno {_alunoAtual}.");

            bool listaMudou = false;

            foreach (var nome in nomesProcessos)
            {
                if (!_processosBloqueados.Contains(nome, StringComparer.OrdinalIgnoreCase))
                {
                    _processosBloqueados.Add(nome);
                    listaMudou = true;
                }
            }

            if (listaMudou)
            {
                AtualizarLog($"Processos [{payload}] adicionados à lista de bloqueio global.");
                string blocklistPayload = string.Join("|", _processosBloqueados);

                await BroadcastMessage("CMD_UPDATE_BLOCKLIST|" + blocklistPayload);
            }
        }

        private void lblListaProcessos_Click(object sender, EventArgs e)
        {

        }

        private async void btnGerenciarBloqueios_Click(object sender, EventArgs e)
        {
            using (FormGerenciarBloqueio formBloqueio = new FormGerenciarBloqueio())
            {
                if (formBloqueio.ShowDialog() == DialogResult.OK)
                {
                    _processosBloqueados = formBloqueio.ListaBloqueioFinal;

                    _caminhosDeIcone.Clear();

                    foreach (var programa in formBloqueio.ProgramasEncontrados)
                    {
                        if (!_caminhosDeIcone.ContainsKey(programa.NomeProcesso))
                        {
                            _caminhosDeIcone.Add(programa.NomeProcesso, programa.CaminhoIcone);
                        }
                    }

                    AtualizarLog("Lista de bloqueio foi atualizada");

                    string payload = string.Join("|", _processosBloqueados);
                    await BroadcastMessage("CMD_UPDATE_BLOCKLIST|" + payload);
                }
            }
        }

        private void btnStartStream_Click(object sender, EventArgs e)
        {
            string receiverIp = txtIpReciever.Text;
            int receiverPort = 1234;

            btnStartStream.Enabled = false;
            btnStopStream.Enabled = true;
            this.Text = "Transmitindo...";
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                // Usando ffmpeg self-contained.
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string ffmpegPath = Path.Combine(baseDirectory, "ffmpeg.exe");

                if (!File.Exists(ffmpegPath))
                {
                    MessageBox.Show($"O arquivo 'ffmpeg.exe' não foi encontrado no diretório da aplicação: {baseDirectory}",
                                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    CleanupAfterStream(Task.FromException(new FileNotFoundException("ffmpeg.exe não encontrado")));
                    return;
                }

                string ffmpegArguments = string.Join(" ",
                    "-f gdigrab",
                    "-framerate 30",
                    "-i desktop",
                    "-c:v libx264",
                    "-b:v 6000k",
                    "-preset ultrafast",
                    "-tune zerolatency",
                    "-an",
                    "-f rtp",
                    $"rtp://{receiverIp}:{receiverPort}"
                );

                Debug.WriteLine($"Argumentos do FFMpeg: {ffmpegArguments}");

                _ffmpegProcess = new Process();
                _ffmpegProcess.StartInfo.FileName = ffmpegPath;
                _ffmpegProcess.StartInfo.Arguments = ffmpegArguments;
                _ffmpegProcess.StartInfo.UseShellExecute = false;
                _ffmpegProcess.StartInfo.CreateNoWindow = true;
                _ffmpegProcess.StartInfo.RedirectStandardError = true;

                _ffmpegProcess.ErrorDataReceived += (s, args) =>
                {
                    if (!string.IsNullOrWhiteSpace(args.Data))
                    {
                        Debug.WriteLine($"[FFMpeg] {args.Data}");
                    }
                };

                _ffmpegTask = Task.Run(() =>
                {
                    _ffmpegProcess.Start();
                    _ffmpegProcess.BeginErrorReadLine();
                    _ffmpegProcess.WaitForExit();
                    _cancellationTokenSource?.Token.ThrowIfCancellationRequested();
                }, _cancellationTokenSource.Token);

                _ffmpegTask.ContinueWith(task =>
                {
                    if (this.IsDisposed) return; // Verificação de segurança adicional

                    if (InvokeRequired)
                    {
                        try
                        {
                            Invoke(new Action(() => CleanupAfterStream(task)));
                        }
                        catch (ObjectDisposedException)
                        {
                            Debug.WriteLine("Formulário já foi descartado, limpeza da UI ignorada.");
                        }
                    }
                    else
                    {
                        CleanupAfterStream(task);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao configurar o FFMpeg: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanupAfterStream(Task.FromException(ex));
            }
        }

        private void CleanupAfterStream(Task task)
        {
            // Restaura o estado da UI
            btnStartStream.Enabled = true;
            btnStopStream.Enabled = false;
            this.Text = "Professor App";

            // Limpa os recursos
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _ffmpegTask = null;
            _ffmpegProcess?.Dispose();
            _ffmpegProcess = null;

            // --- LÓGICA DE FECHAMENTO SEGURO ---
            // Se o formulário estava esperando o FFMpeg terminar para fechar,
            // agora é a hora de fechar de verdade.
            if (_isClosing)
            {
                this.Close();
            }
        }

        private void btnStopStream_Click(object sender, EventArgs e)
        {
            if (_ffmpegProcess != null && !_ffmpegProcess.HasExited)
            {
                try
                {
                    _ffmpegProcess.Kill(true);
                    Debug.WriteLine("Processo FFmpeg encerrado forçadamente.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Não foi possível encerrar o processo FFmpeg: {ex.Message}");
                }
            }

            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private void FormProfessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // --- LÓGICA DE FECHAMENTO SEGURO ---
            // Verifica se a transmissão ainda está ativa.
            if (_ffmpegTask != null && !_ffmpegTask.IsCompleted)
            {
                // Impede que o formulário feche agora.
                e.Cancel = true;
                _isClosing = true; // Marca que queremos fechar assim que possível.

                // Desabilita a interface para evitar cliques duplos.
                this.Enabled = false;

                // Inicia o processo de parada. A limpeza (`CleanupAfterStream`)
                // será chamada no final, e ela irá chamar `this.Close()` novamente.
                btnStopStream_Click(sender, e);
            }
        }
    }
}
