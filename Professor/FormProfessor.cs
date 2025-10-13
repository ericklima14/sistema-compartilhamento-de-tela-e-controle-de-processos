using FFMpegCore;
using System.Diagnostics;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private CancellationTokenSource? _cancellationTokenSource;
        private string? _sdpFilePath;

        public FormProfessor()
        {
            InitializeComponent();
            btnStopStream.Enabled = false;
        }

        private async void btnStartStream_Click(object sender, EventArgs e)
        {
            string receiverIp = "127.0.0.1";
            int receiverPort = 1234;

            btnStartStream.Enabled = false;
            btnStopStream.Enabled = true;

            this.Text = "Transmitindo...";

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                // Define um caminho temporário para o arquivo SDP que o FFmpeg irá criar.
                _sdpFilePath = Path.Combine(Path.GetTempPath(), "stream.sdp");

                await FFMpegArguments
                    // Entrada: ffmpeg -f gdigrab -framerate 30 -i desktop
                    .FromFileInput("desktop", verifyExists: false, options => options
                        .ForceFormat("gdigrab")
                        .WithFramerate(30))

                    // Saída: -c:v h264_nvenc -preset p5 -b:v 6M -an -f rtp rtp://... -sdp_file ...
                    .OutputToUrl($"rtp://{receiverIp}:{receiverPort}", options => options

                        // --- OPÇÃO 1 (RECOMENDADA): Usar encoder de hardware da NVIDIA (NVENC) ---
                        //.WithVideoCodec("h264_nvenc")
                        //.WithVideoBitrate(6000) // -b:v 6M (6000 kbps)
                        //.WithCustomArgument("-preset p5") // Preset de performance/qualidade para NVENC

                         // --- OPÇÃO 2 (ALTERNATIVA): Se não tiver GPU NVIDIA, use o encoder da CPU ---
                         .WithVideoCodec("libx264")
                         .WithVideoBitrate(6000)
                         .WithCustomArgument("-preset ultrafast -tune zerolatency") // Otimizado para baixa latência

                        .WithAudioBitrate(0) // Equivalente a -an
                        .WithCustomArgument($"-sdp_file \"{_sdpFilePath}\"") // Gera o arquivo SDP
                        .ForceFormat("rtp"))
                    .CancellableThrough(_cancellationTokenSource.Token)
                    .ProcessAsynchronously(true);

                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                    MessageBox.Show("Transmissão finalizada.", "Info");
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Transmissão cancelada pelo usuário.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar a transmissão: {ex.Message}", "Erro");
            }
            finally
            {
                // Restaura o estado inicial da interface
                btnStartStream.Enabled = true;
                btnStopStream.Enabled = false;
                this.Text = "Professor App";
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;

                // Apaga o arquivo SDP temporário que o FFmpeg criou
                DeleteSdpFile();
            }
        }

        private void btnStopStream_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private void FormProfessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Garante que o processo FFmpeg será finalizado ao fechar o form
            btnStopStream_Click(sender, e);
        }

        private void DeleteSdpFile()
        {
            try
            {
                if (_sdpFilePath != null && File.Exists(_sdpFilePath))
                {
                    File.Delete(_sdpFilePath);
                    Debug.WriteLine("Arquivo SDP temporário apagado.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Não foi possível apagar o arquivo SDP: {ex.Message}");
            }
        }
    }
}
