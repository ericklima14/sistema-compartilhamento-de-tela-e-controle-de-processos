using System.Diagnostics;
using System.Text;
using ZstdSharp;

namespace Professor
{
    public static class CompressionHelper
    {
        private static int compressionLevel = 3;

        public static byte[] Compress(string text)
        {
            Debug.WriteLine($"MENSAGEM ANTES DA COMPRESSAO (PROFESSOR): {text}");
            byte[] originalBytes = Encoding.UTF8.GetBytes(text);

            var compressor = new Compressor(compressionLevel);
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            byte[] compressedBytes = compressor.Wrap(originalBytes).ToArray();
            stopwatch.Stop();

            double tempoGastoMs = stopwatch.Elapsed.TotalMilliseconds;
            long tempoGastoTicks = stopwatch.ElapsedTicks;

            Console.WriteLine($"[Métrica] Compressão Zstd:");
            Console.WriteLine($" - Tamanho: {compressedBytes.Length}B -> {compressedBytes.Length}B");
            Console.WriteLine($" - Tempo: {tempoGastoMs:F4} ms ({tempoGastoTicks} ticks)");

            Debug.WriteLine($"TAMANHO DA MENSAGEM DEPOIS DA COMPRESSAO (PROFESSOR): {compressedBytes.Length} bytes");

            return compressedBytes;
        }

        public static string Decompress(byte[] compressedData)
        {
            var decompressor = new Decompressor();

            byte[] decompressedBytes = decompressor.Unwrap(compressedData).ToArray();

            string originalText = Encoding.UTF8.GetString(decompressedBytes);

            return originalText;
        }
    }
}
