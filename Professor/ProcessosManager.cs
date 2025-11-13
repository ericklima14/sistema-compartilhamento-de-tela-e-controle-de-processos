using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Professor
{
    public class ProcessosManager
    {
        private static readonly ProcessosManager _instancia = new ProcessosManager();
        public static ProcessosManager Instance => _instancia;

        private string _alunoAtual;
        private readonly List<string> _processosBloqueados = new List<string>();
        public IReadOnlyList<string> ProcessosBloqueados => _processosBloqueados;
        private readonly Dictionary<string, string> _caminhosDeIcone = new Dictionary<string, string>();
        public IReadOnlyDictionary<string, string> CaminhosDeIcone => _caminhosDeIcone;

        public event Action<string> LogAtualizado;
        public event Action<string[]> ListaProcessosAtualizada;

        private ProcessosManager() {
            ConexaoService.Instance.MensagemRecebida += OnMessagemRecebida;
            ConexaoService.Instance.LogAtualizado += (msg) => LogAtualizado?.Invoke(msg);
        }

        private void OnMessagemRecebida(string clientIdentifier, string mensagem)
        {
            if (mensagem.StartsWith("RSP_PROCESS_LIST|") && clientIdentifier == _alunoAtual)
            {
                string payload = mensagem.Substring("RSP_PROCESS_LIST|".Length);
                string[] processNames = payload.Split('|');
                ListaProcessosAtualizada?.Invoke(processNames);
                Log($"Lista de processos recebida de {clientIdentifier}.");
            }
        }

        public async Task IniciarMonitoramento(string alunoIdentifier)
        {
            if (_alunoAtual != null && _alunoAtual != alunoIdentifier)
            {
                await PararMonitoramento();
            }

            TcpClient targetClient = GetClientByIdentifier(alunoIdentifier);
            if (targetClient != null)
            {
                await EnviarMensagemAsync(targetClient, "CMD_START_MONITORING");
                _alunoAtual = alunoIdentifier;
                Log($"Monitoramento iniciado para {alunoIdentifier}.");
            }
            else
            {
                Log($"Aluno {alunoIdentifier} não encontrado para monitoramento.");
            }
        }

        public async Task PararMonitoramento()
        {
            if (_alunoAtual != null)
            {
                TcpClient targetClient = GetClientByIdentifier(_alunoAtual);
                if (targetClient != null)
                {
                    await EnviarMensagemAsync(targetClient, "CMD_STOP_MONITORING");
                    Log($"Monitoramento parado para {_alunoAtual}.");
                }
                _alunoAtual = null;
            }
        }

        public async Task MatarProcessos(List<string> nomesProcessos)
        {
            if (nomesProcessos.Count == 0)
            {
                Log("Nenhum processo selecionado para matar.");
                return;
            }

            string payload = string.Join("|", nomesProcessos);
            string mensagem = $"CMD_KILL_PROCESSES|{payload}";
            await BroadcastMessageAsync(mensagem);
            Log($"Comando para matar processos [{payload}] enviado para todos os alunos.");

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
                Log($"Processos [{payload}] adicionados à lista de bloqueio global.");
                await AtualizarBlocklistGlobal();
            }
        }

        public void AtualizarBlocklist(List<string> novaListaBloqueio, Dictionary<string, string> novosCaminhosIcone)
        {
            _processosBloqueados.Clear();
            _processosBloqueados.AddRange(novaListaBloqueio);
            _caminhosDeIcone.Clear();
            foreach (var kvp in novosCaminhosIcone)
            {
                _caminhosDeIcone[kvp.Key] = kvp.Value;
            }
            Log("Lista de bloqueio atualizada.");
            _ = AtualizarBlocklistGlobal(); // Atualiza assincronamente
        }

        private async Task AtualizarBlocklistGlobal()
        {
            string payload = string.Join("|", _processosBloqueados);
            await BroadcastMessageAsync("CMD_UPDATE_BLOCKLIST|" + payload);
        }

        private TcpClient GetClientByIdentifier(string identifier)
        {
            return ConexaoService.Instance.Clients.FirstOrDefault(kvp => kvp.Value == identifier).Key;
        }

        private async Task EnviarMensagemAsync(TcpClient client, string message)
        {
            await ConexaoService.Instance.SendMessageAsync(client, message);
        }
        private async Task BroadcastMessageAsync(string message)
        {
            await ConexaoService.Instance.BroadcastMessage(message);
        }

        private void Log(string message) => LogAtualizado?.Invoke(message);
    }
}
