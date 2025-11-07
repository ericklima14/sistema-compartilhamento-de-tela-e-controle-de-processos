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
        private string _alunoAtual = null;

        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private string? _sdpFilePath;

        public FormProfessor()
        {
            InitializeComponent();

            Core.Initialize();

            // Logs detalhados
            _libVLC = new LibVLC("--verbose=2");
            _libVLC.Log += Vlc_Log;

            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mediaPlayer;

            ConexaoService.Instance.ListaDeAlunosAtualizada += AtualizarListaAlunos;
            ConexaoService.Instance.LogAtualizado += msg => AdicionarLog(msg);

            ProcessosManager.Instance.LogAtualizado += msg => AdicionarLog(msg);
            ProcessosManager.Instance.ListaProcessosAtualizada += AtualizarListaProcessos;

            AtualizarListaAlunos();
        }

        private void Vlc_Log(object? sender, LogEventArgs e)
        {
            Debug.WriteLine($"[VLC] {e.Level}: {e.Message} (em {e.Module})");
        }

        private void ReceberTela(string professorIp, int chosenPort)
        {
            if (_mediaPlayer.IsPlaying)
                return;

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

                //  Adiciona um buffer no cliente
                //media.AddOption(":rtp-caching=300");

                _mediaPlayer.Play(media);

                //this.Text = "Recebendo stream via SDP...";
                this.Text = $"Visualizando aluno na porta {chosenPort}";
            }
            catch (Exception ex)
            {
                AdicionarLog($"Erro ao iniciar stream: {ex.Message}");
                MessageBox.Show($"Erro ao iniciar stream: {ex.Message}");
            }
        }

        private void FormProfessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _libVLC.Log -= Vlc_Log;
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();

            // Limpeza do arquivo SDP ao fechar
            //if (!string.IsNullOrEmpty(_sdpFilePath) && File.Exists(_sdpFilePath))
            //{
            //    try { File.Delete(_sdpFilePath); }
            //    catch (Exception ex) { Debug.WriteLine($"Erro ao deletar SDP: {ex.Message}"); }
            //}
        }

        private void AtualizarListaProcessos(string[] processos)
        {
            if (lvProcessos.InvokeRequired)
            {
                lvProcessos.Invoke(new Action(() => AtualizarListaProcessos(processos)));
                return;
            }

            var checkedItems = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(i => i.Text).ToHashSet();
            int topIndex = lvProcessos.TopItem?.Index ?? 0;

            lvProcessos.BeginUpdate();
            lvProcessos.Items.Clear();
            imageListProcessos.Images.Clear();
            imageListProcessos.Images.Add(SystemIcons.Application);

            var iconCache = new Dictionary<string, int>();
            foreach (var proc in processos)
            {
                if (string.IsNullOrEmpty(proc)) continue;

                int iconIndex = 0;
                if (ProcessosManager.Instance.CaminhosDeIcone.TryGetValue(proc, out var caminho) && !string.IsNullOrEmpty(caminho))
                {
                    try
                    {
                        var icon = Icon.ExtractAssociatedIcon(caminho);
                        if (icon != null)
                        {
                            imageListProcessos.Images.Add(icon);
                            iconIndex = imageListProcessos.Images.Count - 1;
                        }
                    }
                    catch { }
                }

                var item = new ListViewItem(proc, iconIndex) { Checked = checkedItems.Contains(proc) };
                lvProcessos.Items.Add(item);
            }

            lvProcessos.EndUpdate();
            if (lvProcessos.Items.Count > topIndex)
                lvProcessos.EnsureVisible(topIndex);
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

        private void lstAlunosConectados_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnListarProcessos.Visible = true;
            btnListarProcessos.Enabled = lstAlunosConectados.SelectedItem != null;
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

            await ProcessosManager.Instance.IniciarMonitoramento(selectedIdentifier);
        }

        private async void btnMatarProcesso_Click(object sender, EventArgs e)
        {
            List<string> processos = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(item => item.Text).ToList();
            await ProcessosManager.Instance.MatarProcessos(processos);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ConexaoService.Instance.ListaDeAlunosAtualizada -= AtualizarListaAlunos;
            ProcessosManager.Instance.LogAtualizado -= AdicionarLog;
            ProcessosManager.Instance.ListaProcessosAtualizada -= AtualizarListaProcessos;
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
            string comando = $"CMD_START_SCREEN_MONITORING";

            Task.Run(async () => await ConexaoService.Instance.BroadcastMessage(comando));

            AdicionarLog($"Você iniciou o monitoramento de telas dos alunos.");

            ReceberTela("127.0.0.1", 5004);
        }

        private void btnPararTelas_Click(object sender, EventArgs e)
        {
            string comando = $"CMD_STOP_SCREEN_MONITORING";

            Task.Run(async () => await ConexaoService.Instance.BroadcastMessage(comando));

            AdicionarLog($"Você encerrou o monitoramento de telas dos alunos.");
        }
    }
}
