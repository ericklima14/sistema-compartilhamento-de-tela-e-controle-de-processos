using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professor
{
    public class VideoTransmissaoManager
    {
        private static readonly VideoTransmissaoManager _instancia = new VideoTransmissaoManager();
        public static VideoTransmissaoManager Instance => _instancia;

        public string StreamAddress { get; set; }
        public int StreamPort { get; private set; } = 1234;
        public bool IsStreaming => _ffmpegTask != null && !_ffmpegTask.IsCompleted;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _ffmpegTask;
        private Process _ffmpegProcess;

        private bool _isClosing = false;
        public event Action<string> LogAtualizado;
        public event Action<bool> StatusStreamAtualizado;
        public event Action<string, int> StreamIniciado;

        private VideoTransmissaoManager() { }

        public void StartStream()
        {
            if (IsStreaming)
            {
                Log("Stream já em andamento.");
                return;
            }

            // Mudei para multicast (antes 127.0.0.1)
            string multicastIp = StreamAddress;
            int multicastPort = StreamPort;
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string ffmpegPath = Path.Combine(baseDirectory, "ffmpeg.exe");

                if (!File.Exists(ffmpegPath))
                {
                    MessageBox.Show($"O arquivo 'ffmpeg.exe' não foi encontrado no diretório da aplicação: {baseDirectory}",
                                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    FinalizarStream(Task.FromException(new FileNotFoundException("ffmpeg.exe não encontrado")));
                    return;
                }

                string ffmpegArguments = string.Join(" ",
                    "-f gdigrab",
                    "-framerate 24",
                    "-i desktop",
                    "-c:v libx264",
                    "-b:v 1000k",
                    "-maxrate 1200k",
                    "-bufsize 2000k",
                    "-preset faster",
                    "-tune zerolatency",
                    "-an",
                    "-f rtp",
                    $"rtp://{multicastIp}:{multicastPort}"
                );

                Debug.WriteLine($"Argumentos do FFMpeg: {ffmpegArguments}");
                _ffmpegProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = ffmpegPath,
                        Arguments = ffmpegArguments,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardError = true
                    }
                };

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
                    FinalizarStream(task);
                });

                StatusStreamAtualizado?.Invoke(true);
                Log("Transmissão iniciada.");
                StreamIniciado?.Invoke(multicastIp, multicastPort);
            }
            catch (Exception ex)
            {
                Log($"Falha ao iniciar o stream: {ex.Message}");
                FinalizarStream(Task.FromException(ex));
            }
        }

        public void StopStream()
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

            Log("Transmissão parada.");
        }

        private void FinalizarStream(Task task)
        {
            StatusStreamAtualizado?.Invoke(false);
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _ffmpegTask = null;
            _ffmpegProcess?.Dispose();
            _ffmpegProcess = null;

            if (_isClosing)
            {
                Log("Cleanup concluído após fechamento.");
            }
        }

        public void PrepareForClosing()
        {
            _isClosing = true;
            StopStream();
        }

        private void Log(string message) => LogAtualizado?.Invoke(message);
    }
}

