using LibVLCSharp.Shared;
using System.Diagnostics;
using System.IO;

namespace Aluno
{
    public partial class FormAluno : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private string? _sdpFilePath;

        public FormAluno()
        {
            InitializeComponent();
            Core.Initialize();

            // Logs detalhados
            _libVLC = new LibVLC("--verbose=2");
            _libVLC.Log += Vlc_Log;

            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mediaPlayer;
            this.Load += FormAluno_Load;
        }

        private void Vlc_Log(object? sender, LogEventArgs e)
        {
            Debug.WriteLine($"[VLC] {e.Level}: {e.Message} (em {e.Module})");
        }

        private void FormAluno_Load(object? sender, EventArgs e)
        {
            // O Professor é quem deve gerar o arquivo SDP, pois ele contém
            // detalhes da sessão de streaming que só ele conhece.
            // Para este teste, vamos assumir que o arquivo já existe no caminho esperado.
            
            // O caminho deve ser o mesmo usado na aplicação do Professor.
            _sdpFilePath = Path.Combine(Path.GetTempPath(), "stream.sdp");

            // A maneira mais robusta de abrir um arquivo local no LibVLCSharp é usando
            // um objeto Uri com o esquema "file://". Isso resolve ambiguidades e
            // permite que o VLC use seus módulos de acesso a arquivos corretamente.

            // Verificamos se o arquivo existe antes de tentar abri-lo.
            // Isso evita erros se o Professor ainda não iniciou a transmissão.
            if (!File.Exists(_sdpFilePath))
            {
                this.Text = $"Aguardando o arquivo SDP em: {_sdpFilePath}";
                MessageBox.Show($"O arquivo de stream '{_sdpFilePath}' não foi encontrado.\n\nPor favor, inicie a transmissão no sistema do Professor primeiro.", "Aguardando Transmissão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Criamos a mídia a partir da URI do arquivo.
            // Não adicionamos nenhuma opção extra. O VLC é inteligente o suficiente
            // para identificar a extensão .sdp e usar o demuxer correto.
            var media = new Media(_libVLC, new Uri(_sdpFilePath));
            
            _mediaPlayer.Play(media);

            this.Text = "Recebendo stream via SDP...";
        }

        private void FormAluno_FormClosing(object sender, FormClosingEventArgs e)
        {
            _libVLC.Log -= Vlc_Log;
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
            // O aluno não deve apagar o arquivo, pois ele é gerado pelo professor.
        }
    }
}

