using System;
using System.Text;
using ZstdSharp;

namespace Professor
{
    public static class CompressionHelper
    {
        private static int compressionLevel = 3;

        public static byte[] Compress(string text)
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(text);

            var compressor = new Compressor(compressionLevel);

            byte[] compressedBytes = compressor.Wrap(originalBytes).ToArray();

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
