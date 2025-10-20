using LibVLCSharp.Shared;
using System.Diagnostics;

namespace Aluno
{
    public partial class FormAluno : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private string? _sdpFilePath; // O caminho para o nosso arquivo SDP temporário

        public FormAluno()
        {
            InitializeComponent();
            Core.Initialize();

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
            // --- SOLUÇÃO HÍBRIDA: SDP Hardcoded, Carregado via Arquivo Temporário ---

            // 1. Definimos o conteúdo do arquivo SDP diretamente em uma string.
            string sdpContent = @"
v=0
o=- 0 0 IN IP4 127.0.0.1
s=No Name
c=IN IP4 127.0.0.1
t=0 0
a=tool:libavformat 62.4.101
m=video 1234 RTP/AVP 96
b=AS:6000
a=framerate:30
a=rtpmap:96 H264/90000
a=fmtp:96 packetization-mode=1
".Trim();

            // 2. Criamos um arquivo temporário para armazenar nosso SDP.
            //    Isso torna a aplicação autônoma, sem depender de um arquivo externo.
            _sdpFilePath = Path.Combine(Path.GetTempPath(), "aluno_stream.sdp");
            File.WriteAllText(_sdpFilePath, sdpContent);

            // 3. Criamos a mídia a partir da URI do arquivo local.
            //    Este é o método mais compatível e robusto para o LibVLC.
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

            // Limpeza: Apagamos o arquivo SDP temporário que criamos.
            try
            {
                if (_sdpFilePath != null && File.Exists(_sdpFilePath))
                {
                    File.Delete(_sdpFilePath);
                    Debug.WriteLine("Arquivo SDP temporário do aluno apagado.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Não foi possível apagar o arquivo SDP do aluno: {ex.Message}");
            }
        }
    }
}

