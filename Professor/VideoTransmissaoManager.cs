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

        private CancellationTokenSource _cancellationTokenSource;
        private Task _ffmpegTask;
        private Process _ffmpegProcess;

        private bool _isClosing = false;
        public event Action<string> LogAtualizado;0
        public event Action<bool> StatusStreamAtualizado;

        private VideoTransmissaoManager() { }

        public void StartStream()
        {
            if (_ffmpegTask != null && !_ffmpegTask.IsCompleted)
            {
                Log("Stream já em andamento.");
                return;
            }

            string receiverIp = "127.0.0.1";
            int receiverPort = 1234;
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
                _ffmpegProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = "ffmpeg.exe",
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

