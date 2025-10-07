using System.Diagnostics;
using System.Management;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Aluno
{
    public partial class FormAluno : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private List<string> processosBloqueados = new List<string>();
        private ManagementEventWatcher wmiWatcher;

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

                Task.Run(() => ReceberMensagem());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar: {ex.Message}");
            }
        }

        private void IniciarVigiaDeProcessos()
        {
            try
            {
                string query = "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
                wmiWatcher = new ManagementEventWatcher(new WqlEventQuery(query));

                wmiWatcher.EventArrived += ProcessoIniciado_EventArrived;

                wmiWatcher.Start();
                AtualizarLog("Vigia de processos em tempo real ATIVADO");

                MatarProcessosInciais();    
            } catch (Exception ex)
            {
                AtualizarLog($"Erro ao iniciar o vigia de processos: {ex.Message}");
            }
        }

        private void MatarProcessosInciais()
        {
            foreach(var nome in processosBloqueados)
            {
                try
                {
                    Process[] processesToKill = Process.GetProcessesByName(nome);

                    if (processesToKill.Length > 0)
                    {
                        foreach (Process process in processesToKill)
                        {
                            process.Kill();
                            AtualizarLog($"Processo '{nome}' (ID: {process.Id}) finalizado com sucesso.");
                        }
                    }
                    else
                    {
                        AtualizarLog($"Processo '{nome}' não foi encontrado em execução.");
                    }
                }
                catch (Exception ex)
                {
                    AtualizarLog($"Erro ao tentar finalizar '{nome}': {ex.Message}");
                }
            }
        }

        private void PararVigiaDeProcessos()
        {
            if(wmiWatcher != null)
            {
                wmiWatcher.Stop();
                wmiWatcher.Dispose();
                wmiWatcher = null;
                AtualizarLog("Vigia de processos em tempo real DESATIVADO");
            }
        }

        private void ProcessoIniciado_EventArrived(object sender, EventArrivedEventArgs e)
        {
            try
            {
                string nomeProcesso = ((ManagementBaseObject)e.NewEvent["TargetInstance"])["Name"].ToString();

                AtualizarLog($"[DEBUG] Novo processo detectado: {nomeProcesso}");

                if (processosBloqueados.Contains(nomeProcesso.Replace(".exe", ""), StringComparer.OrdinalIgnoreCase)) {
                    AtualizarLog($"Processo proibido '{nomeProcesso}' detectado instantaneamente");

                    string nome = nomeProcesso.Replace(".exe", "");

                    try
                    {
                        Process[] processesToKill = Process.GetProcessesByName(nome);

                        if (processesToKill.Length > 0)
                        {
                            foreach (Process process in processesToKill)
                            {
                                process.Kill();
                                AtualizarLog($"Processo '{nome}' (ID: {process.Id}) finalizado com sucesso.");
                            }
                        }
                        else
                        {
                            AtualizarLog($"Processo '{nome}' não foi encontrado em execução.");
                        }
                    }
                    catch (Exception ex)
                    {
                        AtualizarLog($"Erro ao tentar finalizar '{nome}': {ex.Message}");
                    }
                  
                }
            } 
            catch (Exception ex)
            {
                AtualizarLog($"Erro no evento do vigia: {ex.Message}");
            }
        }
         
        private async Task ReceberMensagem()
        {
            while (client.Connected)
            {
                try
                {
                    byte[] lengthBuffer = new byte[4];
                    await ReadTotalBytesAsync(stream, lengthBuffer);
                    int messageLength = BitConverter.ToInt32(lengthBuffer, 0);
                    byte[] compressedMessage = new byte[messageLength];
                    await ReadTotalBytesAsync(stream, compressedMessage);

                    string mensagem = CompressionHelper.Decompress(compressedMessage);

                    if (mensagem.StartsWith("CMD_UPDATE_BLOCKLIST|")) 
                    {
                        string payload = mensagem.Substring("CMD_UPDATE_BLOCKLIST|".Length);
                        processosBloqueados = new List<string>(payload.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                        AtualizarLog($"Lista de bloqueio atualizada. Fiscalização ativada.");

                        if (wmiWatcher == null)
                        {
                            this.Invoke(new Action(() => IniciarVigiaDeProcessos()));
                        }
                    }
                    else if (mensagem == "CMD_START_MONITORING")
                    {
                        this.Invoke(new Action(() => {
                            AtualizarLog("Professor iniciou o monitoramento de processos.");
                            IniciarVigiaDeProcessos();
                            processTimer.Start();
                        }));
                    }
                    else if (mensagem == "CMD_STOP_MONITORING")
                    {
                        this.Invoke(new Action(() => {
                            AtualizarLog("Professor parou o monitoramento de processos.");
                            processTimer.Stop();
                        }));
                    }
                    else if (mensagem.StartsWith("CMD_KILL_PROCESSES|"))
                    {
                        MatarProcessos(mensagem);
                    }
                    else
                    {
                        AtualizarLog(mensagem);
                    }
                }
                catch (Exception)
                {
                    AtualizarLog("Conexão perdida.");
                    if (processTimer.Enabled) 
                        this.Invoke(new Action(() => {
                            PararVigiaDeProcessos();
                            processTimer.Stop();
                        }));
                    
                    break;
                }
            }
        }

        private async Task SendMessageAsync(string message)
        {
            if (stream != null && stream.CanWrite)
            {
                byte[] compressedMessage = CompressionHelper.Compress(message);
                byte[] lengthBuffer = BitConverter.GetBytes(compressedMessage.Length);

                await stream.WriteAsync(lengthBuffer, 0, lengthBuffer.Length);
                await stream.WriteAsync(compressedMessage, 0, compressedMessage.Length);
            }
        }

        private async Task ReadTotalBytesAsync(NetworkStream streamAtual, byte[] buffer)
        {
            int totalBytesLidos = 0;

            while (totalBytesLidos < buffer.Length)
            {
                int bytesLidos = await streamAtual.ReadAsync(buffer, totalBytesLidos, buffer.Length - totalBytesLidos);
                if (bytesLidos == 0)
                    throw new IOException("Conexão perdida.");
                totalBytesLidos += bytesLidos;
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
            if (stream != null && !string.IsNullOrEmpty(txtMensagem.Text))
            {
                await SendMessageAsync(txtMensagem.Text);
                AtualizarLog($"Você: {txtMensagem.Text}");
                txtMensagem.Clear();
            }
        }

        private async void processTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //var processNames = Process.GetProcesses().Select(p => p.ProcessName).Distinct().OrderBy(name => name);
                var processNames = Process.GetProcesses()
                                          .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle))
                                          .Select(p => p.ProcessName )
                                          .Distinct()
                                          .OrderBy(name => name);


                string responsePayload = string.Join("|", processNames);
                await SendMessageAsync("RSP_PROCESS_LIST|" + responsePayload);
            } catch {
                Console.WriteLine("Processo que nao pode ser acessado");
            }
            
        }

        private void MatarProcessos(string comando)
        {
            string payload = comando.Substring("CMD_KILL_PROCESSES|".Length);

            AtualizarLog($"Recebido comando para finalizar os processos: '{payload}'.");

            string[] nomesProcessos = payload.Split('|');

            foreach (string nome in nomesProcessos)
            {
                if (string.IsNullOrWhiteSpace(nome))
                    continue;

                try
                {
                    Process[] processesToKill = Process.GetProcessesByName(nome);

                    if (processesToKill.Length > 0)
                    {
                        foreach (Process process in processesToKill)
                        {
                            process.Kill();
                            AtualizarLog($"Processo '{nome}' (ID: {process.Id}) finalizado com sucesso.");
                        }
                    }
                    else
                    {
                        AtualizarLog($"Processo '{nome}' não foi encontrado em execução.");
                    }
                }
                catch (Exception ex)
                {
                    AtualizarLog($"Erro ao tentar finalizar '{nome}': {ex.Message}");
                }
            }
        }
    }
}
