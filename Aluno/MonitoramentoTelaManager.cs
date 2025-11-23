using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aluno
{
    public class MonitoramentoTelaManager
    {
        private static readonly MonitoramentoTelaManager _instancia = new MonitoramentoTelaManager();
        public static MonitoramentoTelaManager Instance => _instancia;

        public bool IsStreaming => _ffmpegTask != null && !_ffmpegTask.IsCompleted;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _ffmpegTask;
        private Process _ffmpegProcess;

        private bool _isClosing = false;
        public event Action<string> LogAtualizado;
        public event Action<bool> StatusStreamAtualizado;

        private MonitoramentoTelaManager() { }

        public void StartStream(string targetIp, int targetPort, int fps = 5, string bitrate = "1000k")
        {
            if (IsStreaming)
            {
                StopStream();
                Thread.Sleep(200);
            }

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
                    $"-framerate {fps}",
                    "-i desktop",
                    "-c:v libx264",
                    $"-b:v {bitrate}",
                    "-preset ultrafast",
                    "-tune zerolatency",
                    "-an",
                    "-f rtp",
                    $"rtp://{targetIp}:{targetPort}"
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
