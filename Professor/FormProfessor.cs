using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private LibVLC _libVLC;
        private List<MediaPlayer> _activeMediaPlayers = new List<MediaPlayer>();
        private string? _sdpFilePath;

        public FormProfessor()
        {
            InitializeComponent();

            Core.Initialize();

            // Logs detalhados
            _libVLC = new LibVLC("--verbose=2");
            _libVLC.Log += Vlc_Log;

            ConexaoService.Instance.ListaDeAlunosAtualizada += AtualizarListaAlunos;

            ProcessosManager.Instance.LogAtualizado += msg => AdicionarLog(msg);

            AtualizarListaAlunos();
        }

        private void Vlc_Log(object? sender, LogEventArgs e)
        {
            Debug.WriteLine($"[VLC] {e.Level}: {e.Message} (em {e.Module})");
        }

        private void ReceberTela(VideoView targetVideoView, string professorIp, int chosenPort)
        {

            AdicionarLog($"Recebendo tela na porta {chosenPort}");

            // --- SOLUÇÃO HÍBRIDA: SDP Hardcoded, Carregado via Arquivo Temporário ---
            try
            {
                // 1. Definimos o conteúdo do arquivo SDP diretamente em uma string.
                string sdpContent = $@"
v=0
o=- 0 0 IN IP4 {professorIp}
s=No Name
c=IN IP4 {professorIp}
t=0 0
a=tool:libavformat 62.4.101
m=video {chosenPort} RTP/AVP 96
b=AS:6000
a=framerate:30
a=rtpmap:96 H264/90000
a=fmtp:96 packetization-mode=1
".Trim();

                _sdpFilePath = Path.Combine(Path.GetTempPath(), $"prof_recv_{chosenPort}.sdp");
                File.WriteAllText(_sdpFilePath, sdpContent);

                // 3. Criamos a mídia a partir da URI do arquivo local.
                //    Este é o método mais compatível e robusto para o LibVLC.
                var media = new Media(_libVLC, new Uri(_sdpFilePath));
                var newMediaPlayer = new MediaPlayer(_libVLC);

                targetVideoView.MediaPlayer = newMediaPlayer;
                //  Adiciona um buffer no cliente
                //media.AddOption(":rtp-caching=300");

                newMediaPlayer.Play(media);

                _activeMediaPlayers.Add(newMediaPlayer);

                //this.Text = "Recebendo stream via SDP...";
                this.Text = $"Visualizando aluno na porta {chosenPort}";
            }
            catch (Exception ex)
            {
                AdicionarLog($"Erro ao iniciar stream porta {chosenPort}: {ex.Message}");
                MessageBox.Show($"Erro ao iniciar stream: {ex.Message}");
            }
        }

        private void LimparStreams()
        {
            // Para e dispensa todos os media players ativos
            foreach (var player in _activeMediaPlayers)
            {
                player.Stop();
                player.Dispose();
            }
            _activeMediaPlayers.Clear();

            // Limpa a UI
            if (flpStudentStreams.InvokeRequired)
            {
                flpStudentStreams.Invoke(new Action(() => flpStudentStreams.Controls.Clear()));
            }
            else
            {
                flpStudentStreams.Controls.Clear();
            }
        }

        private void FormProfessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _libVLC.Log -= Vlc_Log;
            _libVLC.Dispose();
            LimparStreams();

            // Limpeza do arquivo SDP ao fechar
            //if (!string.IsNullOrEmpty(_sdpFilePath) && File.Exists(_sdpFilePath))
            //{
            //    try { File.Delete(_sdpFilePath); }
            //    catch (Exception ex) { Debug.WriteLine($"Erro ao deletar SDP: {ex.Message}"); }
            //}
        }

        private void AtualizarListaAlunos()
        {
            if (lstAlunosConectados.InvokeRequired)
            {
                lstAlunosConectados.Invoke(new Action(AtualizarListaAlunos));
                return;
            }

            var selecionado = lstAlunosConectados.SelectedItem?.ToString();
            lstAlunosConectados.Items.Clear();
            lock (ConexaoService.Instance.Clients)
            {
                foreach (var aluno in ConexaoService.Instance.Clients.Values)
                {
                    lstAlunosConectados.Items.Add(aluno);
                }
            }
            if (selecionado != null && lstAlunosConectados.Items.Contains(selecionado))
                lstAlunosConectados.SelectedItem = selecionado;
        }

        private void AdicionarLog(string msg)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action<string>(AdicionarLog), msg);
                return;
            }
            lstLog.Items.Add(msg);
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            AdicionarLog($"Encerrando o monitoramento de telas dos alunos.");
            string comando = $"CMD_STOP_SCREEN_MONITORING";
            Task.Run(async () => await ConexaoService.Instance.BroadcastMessage(comando));

            _libVLC.Log -= Vlc_Log;
            _libVLC.Dispose();
            LimparStreams();

            ConexaoService.Instance.ListaDeAlunosAtualizada -= AtualizarListaAlunos;
            ProcessosManager.Instance.LogAtualizado -= AdicionarLog;
            base.OnFormClosed(e);
        }

        private async void btnGerenciarBloqueios_Click(object sender, EventArgs e)
        {
            using (var form = new FormGerenciarBloqueio(ProcessosManager.Instance.ProcessosBloqueados.ToList()))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var novosIcones = form.ProgramasEncontrados
                        .Where(p => !string.IsNullOrEmpty(p.CaminhoIcone))
                        .ToDictionary(p => p.NomeProcesso, p => p.CaminhoIcone);

                    ProcessosManager.Instance.AtualizarBlocklist(form.ListaBloqueioFinal, novosIcones);
                }
            }
        }

        private void btnIniciarTelas_Click(object sender, EventArgs e)
        {
            LimparStreams();

            AdicionarLog($"Você iniciou o monitoramento de telas dos alunos.");

            string professorIP = ConexaoService.Instance.ProfessorIP;
            int basePort = 5004;

            Dictionary<TcpClient, string> clientMap;
            lock (ConexaoService.Instance.Clients)
            {
                clientMap = new Dictionary<TcpClient, string>(ConexaoService.Instance.Clients);
            }

            foreach (var entry in clientMap)
            {
                TcpClient client = entry.Key;
                string identifier = entry.Value;
                int currentPort = basePort += 2;

                CriarPainelStream(identifier, currentPort);

                string comando = $"CMD_START_SCREEN_MONITORING|{currentPort}";
                Task.Run(async () => await ConexaoService.Instance.SendMessageAsync(client, comando));
            }
        }

        private void btnPararTelas_Click(object sender, EventArgs e)
        {
            LimparStreams();

            string comando = $"CMD_STOP_SCREEN_MONITORING";
            Task.Run(async () => await ConexaoService.Instance.BroadcastMessage(comando));

            AdicionarLog($"Você encerrou o monitoramento de telas dos alunos.");
        }

        private void CriarPainelStream(string identifier, int currentPort)
        {
            string professorIP = ConexaoService.Instance.ProfessorIP;

            Panel studentPanel = new Panel
            {
                Width = 320,
                Height = 210,
                Margin = new Padding(5),
                Tag = new { Port = currentPort, Nome = identifier }
            };

            Label studentLabel = new Label
            {
                Text = identifier,
                AutoSize = true,
                Location = new Point(10, 180),

                ForeColor = Color.White,
                BackColor = Color.FromArgb(150, 0, 0, 0),

                Font = new Font("Arial", 9, FontStyle.Bold),
                Padding = new Padding(3)
            };

            VideoView studentVideoView = new VideoView
            {
                Dock = DockStyle.Fill,
                MediaPlayer = null
            };

            studentPanel.Controls.Add(studentVideoView);
            studentPanel.Controls.Add(studentLabel);
            studentLabel.BringToFront();

            studentLabel.Click += StudentLabel_Click;

            // Adiciona ao FlowLayoutPanel (garantindo a thread-safety)
            if (flpStudentStreams.InvokeRequired)
            {
                flpStudentStreams.Invoke(new Action(() => flpStudentStreams.Controls.Add(studentPanel)));
            }
            else
            {
                flpStudentStreams.Controls.Add(studentPanel);
            }

            ReceberTela(studentVideoView, professorIP, currentPort);
        }

        private void StudentLabel_Click(object sender, EventArgs e)
        {
            Panel studentPanel = null;
            if (sender is Panel p) studentPanel = p;
            else if (sender is Control c && c.Parent is Panel p2) studentPanel = p2;

            if (studentPanel == null) return;

            // Recupera os dados que salvamos na Tag
            dynamic dadosAluno = studentPanel.Tag;
            if (dadosAluno == null) return;

            int port = dadosAluno.Port;
            string nome = dadosAluno.Nome;

            VideoView videoView = studentPanel.Controls.OfType<VideoView>().FirstOrDefault();
            if (videoView != null && videoView.MediaPlayer != null)
            {
                var playerToStop = videoView.MediaPlayer;
                playerToStop.Stop();
                _activeMediaPlayers.Remove(playerToStop);
                playerToStop.Dispose();
            }

            flpStudentStreams.Controls.Remove(studentPanel);
            studentPanel.Dispose();

            FormFoco formFoco = new FormFoco(_libVLC, port, nome);
            formFoco.FormClosed += FormFoco_FormClosed;

            formFoco.Show();
            AdicionarLog($"Foco aberto para: {nome} na porta {port}");
        }

        private void FormFoco_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is FormFoco formFoco)
            {
                int port = formFoco.Port;
                string nome = formFoco.NomeAluno;

                AdicionarLog($"Foco fechado. Restaurando stream de: {nome} (Porta {port})");
                CriarPainelStream(nome, port);
            }
        }

        private void lstAlunosConectados_DoubleClick(object sender, EventArgs e)
        {
            if (lstAlunosConectados.SelectedItem == null) return;

            string nomeAlunoSelecionado = lstAlunosConectados.SelectedItem.ToString();
            Panel painelDoAluno = null;

            foreach (Control controle in flpStudentStreams.Controls)
            {
                if (controle is Panel p && p.Tag != null)
                {
                    dynamic dados = p.Tag;

                    if (dados.Nome == nomeAlunoSelecionado)
                    {
                        painelDoAluno = p;
                        break;
                    }
                }
            }

            if (painelDoAluno != null)
            {
                StudentLabel_Click(painelDoAluno, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show($"O monitoramento de tela para {nomeAlunoSelecionado} não está ativo no momento.",
                                "Stream não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
