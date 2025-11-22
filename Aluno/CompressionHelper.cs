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
            Debug.WriteLine($"MENSAGEM ANTES DA COMPRESSAO (PROFESSOR): {text}");
            byte[] originalBytes = Encoding.UTF8.GetBytes(text);

            //Debug.WriteLine($"TAMANHO DA MENSAGEM ANTES DA COMPRESSAO (ALUNO): {originalBytes.Length} bytes");

            var compressor = new Compressor(compressionLevel);

            byte[] compressedBytes = compressor.Wrap(originalBytes).ToArray();

            //Debug.WriteLine($"TAMANHO DA MENSAGEM DEPOIS DA COMPRESSAO (ALUNO): {compressedBytes.Length} bytes");

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
