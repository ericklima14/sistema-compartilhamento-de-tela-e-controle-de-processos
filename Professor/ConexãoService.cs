using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Professor
{
    public class ConexaoService
    {
        private static readonly ConexaoService _instancia = new ConexaoService();
        public static ConexaoService Instance => _instancia;
        private TcpListener _listener;
        private readonly Dictionary<TcpClient, string> _clients = new Dictionary<TcpClient, string>();
        public IReadOnlyDictionary<TcpClient, string> Clients => _clients;
        
        public event Action<string> LogAtualizado;
        public event Action ListaDeAlunosAtualizada;
        public event Action<string, string> MensagemRecebida;

        public string ProfessorIP { get; set; }

        private ConexaoService() { }

        public void IniciarServidor()
        {
            VideoTransmissaoManager.Instance.StreamIniciado += NotificarAlunosSobreStream; 

            Task.Run(async () => {
                _listener = new TcpListener(IPAddress.Any, 8080);
                _listener.Start();

                Log("Servidor iniciado. Aguardando conexões...");

                while (true)
                {
                    try
                    {
                        TcpClient client = await _listener.AcceptTcpClientAsync();
                        Task.Run(() => HandleClientAsync(client));
                    }
                    catch (Exception ex)
                    {
                       Log($"Erro ao aceitar conexão: {ex.Message}");
                    }
                }

            });
        }
        private void NotificarAlunosSobreStream(string ipInutil, int port)
        {
            // O primeiro argumento (ipInutil) não é necessário, pois os alunos já sabem o IP.
            Log($"Stream iniciado na porta {port}. Notificando {Clients.Count} alunos conectados...");

            string comando = $"CMD_STREAM_INFO|{port}";

            // Usamos Task.Run para disparar o broadcast sem bloquear o thread do evento
            Task.Run(async () =>
            {
                List<TcpClient> clientsAtuais;
                lock (_clients)
                {
                    clientsAtuais = _clients.Keys.ToList();
                }

                foreach (var client in clientsAtuais)
                {
                    await SendMessageAsync(client, comando);
                }
            });
        }

        public void PararServidor()
        {
            VideoTransmissaoManager.Instance.StreamIniciado -= NotificarAlunosSobreStream; 

            _listener?.Stop();

            lock (_clients)
            {
                foreach (var client in _clients.Keys.ToList())
                {
                    client.Close();
                }
                _clients.Clear();
            }
            Log("Servidor parado.");
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            //string clientIdentifier = client.Client.RemoteEndPoint?.ToString() ?? "Desconhecido";
            string clientIp = client.Client.RemoteEndPoint?.ToString() ?? "Desconhecido";
            string clientIdentifier = null;
            NetworkStream stream = client.GetStream();

            try
            {
                // O servidor espera o cliente se identificar primeiro.
                byte[] initialLengthBuffer = new byte[4];
                await ReadTotalBytesAsync(stream, initialLengthBuffer);
                int initialMessageLength = BitConverter.ToInt32(initialLengthBuffer, 0);

                byte[] initialCompressedMessage = new byte[initialMessageLength];
                await ReadTotalBytesAsync(stream, initialCompressedMessage);

                string initialMessage = CompressionHelper.Decompress(initialCompressedMessage);

                if (initialMessage.StartsWith("INFO_USER_NAME|"))
                {
                    clientIdentifier = $"{initialMessage.Substring("INFO_USER_NAME|".Length)} {clientIp}";
                }
                else
                {
                    Log($"Cliente {clientIp} falhou na identificação (mensagem inválida). Desconectando.");
                    client.Close();
                    return;
                }

                lock (_clients)
                {
                    _clients.Add(client, clientIdentifier);
                }
                Log($"Novo aluno conectado: {clientIdentifier}");
                //Log($"Novo aluno conectado: {clientIdentifier}");
                AtualizarListaAlunos();

                // Se a transmissao ja estiver rolando quando o aluno se conectar, envia IMEDIATAMENTE para este aluno.
                if (VideoTransmissaoManager.Instance.IsStreaming)
                {
                    int port = VideoTransmissaoManager.Instance.StreamPort;
                    string comando = $"CMD_STREAM_INFO|{port}";

                    await SendMessageAsync(client, comando);
                }

                while (client.Connected)
                {
                    byte[] lengthBuffer = new byte[4];
                    await ReadTotalBytesAsync(stream, lengthBuffer);
                    int messageLength = BitConverter.ToInt32(lengthBuffer, 0);

                    byte[] compressedMessage = new byte[messageLength];
                    await ReadTotalBytesAsync(stream, compressedMessage);

                    string mensagem = CompressionHelper.Decompress(compressedMessage);

                    // Notifica serviços interessados (ex.: ProcessControlService)
                    MensagemRecebida?.Invoke(clientIdentifier, mensagem);

                    if (!mensagem.StartsWith("RSP_PROCESS_LIST|")) // Deixa parsing específico para outros serviços
                    {
                        string broadcastMessage = $"[{clientIdentifier}]: {mensagem}";
                        Log(broadcastMessage);
                        await BroadcastMessage(broadcastMessage, client);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Erro no handling do client {clientIdentifier}: {ex.Message}");
            }
            finally
            {
                lock (_clients)
                {
                    _clients.Remove(client);
                }
                client.Close();
                Log($"Aluno {clientIdentifier} desconectado.");
                AtualizarListaAlunos();
            }
        }

        private void Log(string message) => LogAtualizado?.Invoke(message);

        private void AtualizarListaAlunos() => ListaDeAlunosAtualizada?.Invoke();

        public async Task SendMessageAsync(TcpClient client, string message)
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
                    Log($"Erro ao enviar mensagem para {_clients[client]}: {ex.Message}");
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

        public async Task BroadcastMessage(string message, TcpClient sender = null)
        {
            List<TcpClient> clientsParaEnviar;
            lock (_clients)
            {
                clientsParaEnviar = _clients.Keys.ToList();
            }

            foreach (var client in clientsParaEnviar)
            {
                if (client != sender)
                {
                    await SendMessageAsync(client, message);
                }
            }
        }

        public static Dictionary<string, string> ObterIntefaces()
        {
            var interfaces = new Dictionary<string, string>();

            // Itera por todas as placas de rede da máquina
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                // Não pode ser "Loopback" (ignora o 127.0.0.1)
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {

                    var ipProps = ni.GetIPProperties();
                    foreach (var addr in ipProps.UnicastAddresses)
                    {
                        // Apenas endereços IPv4
                        if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            string ip = addr.Address.ToString();
                            string descricao = $"{ni.Name} ({ni.Description})";

                            if (!interfaces.ContainsKey(ip))
                            {
                                interfaces.Add(ip, descricao);
                            }
                        }
                    }
                }
            }
            return interfaces;
        }
    }
}
