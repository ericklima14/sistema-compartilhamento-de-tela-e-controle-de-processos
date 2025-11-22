using System.Diagnostics;
using System.Text;
using ZstdSharp;

namespace Aluno
{
    public static class CompressionHelper
    {
        private static int compressionLevel = 3;

        public static byte[] Compress(string text)
        {
            //Debug.WriteLine($"MENSAGEM ANTES DA COMPRESSAO (PROFESSOR): {text}");
            byte[] originalBytes = Encoding.UTF8.GetBytes(text);

            var compressor = new Compressor(compressionLevel);
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            byte[] compressedBytes = compressor.Wrap(originalBytes).ToArray();
            stopwatch.Stop();

            double tempoGastoMs = stopwatch.Elapsed.TotalMilliseconds;
            long tempoGastoTicks = stopwatch.ElapsedTicks;

            Debug.WriteLine($"[Métrica] Compressão Zstd (Aluno):");
            Debug.WriteLine($" - Tamanho: {originalBytes.Length}B -> {compressedBytes.Length}B");
            Debug.WriteLine($" - Tempo: {tempoGastoMs:F4} ms ({tempoGastoTicks} ticks)");

            //Debug.WriteLine($"TAMANHO DA MENSAGEM DEPOIS DA COMPRESSAO (PROFESSOR): {compressedBytes.Length} bytes");

            return compressedBytes;
        }

        public static string Decompress(byte[] compressedData)
        {
            var decompressor = new Decompressor();
            var stopwatch = new Stopwatch();

            byte[] decompressedBytes = decompressor.Unwrap(compressedData).ToArray();

            stopwatch.Start();
            string originalText = Encoding.UTF8.GetString(decompressedBytes);
            stopwatch.Stop();

            double tempoGastoMs = stopwatch.Elapsed.TotalMilliseconds;
            long tempoGastoTicks = stopwatch.ElapsedTicks;

            Debug.WriteLine($"[Métrica] Descompressão Zstd (Aluno):");
            Debug.WriteLine($" - Tamanho: {compressedData.Length}B -> {decompressedBytes.Length}B");
            Debug.WriteLine($" - Tempo: {tempoGastoMs:F4} ms ({tempoGastoTicks} ticks)");


            return originalText;
        }
    }
}
