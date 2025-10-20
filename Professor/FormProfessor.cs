using FFMpegCore;
using System.Diagnostics;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private CancellationTokenSource? _cancellationTokenSource;
        private Task? _ffmpegTask;
        private Process? _ffmpegProcess;
        private bool _isClosing = false; // Flag para controlar o fechamento ordenado

        public FormProfessor()
        {
            InitializeComponent();
            btnStopStream.Enabled = false;
        }

        private void btnStartStream_Click(object sender, EventArgs e)
        {
            string receiverIp = "127.0.0.1";
            int receiverPort = 1234;

            btnStartStream.Enabled = false;
            btnStopStream.Enabled = true;
            this.Text = "Transmitindo...";
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
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
                _ffmpegProcess.StartInfo.FileName = "ffmpeg.exe";
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

