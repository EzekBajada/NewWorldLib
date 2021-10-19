using System.Data;
using Oodle.NET;

namespace NewWorldLib.Compression
{
    public static class Oodle
    {
        public static unsafe void Decompress(byte[] compressed, int compressedOffset, int compressedSize,
            byte[] uncompressed, int uncompressedOffset, int uncompressedSize)
        {
            fixed (byte* compressedPtr = compressed, uncompressedPtr = uncompressed)
            {
                using var oodle = new OodleCompressor(@"C:\Users\Ezek_\Documents\Code\NewWorldLib\NewWorldLib.Console\bin\Debug\net5.0\Libraries\oo2core_8_win64.dll");
                var result = oodle.DecompressBuffer(compressed, compressedSize, uncompressed, uncompressedSize, OodleLZ_FuzzSafe.No, OodleLZ_CheckCRC.No, OodleLZ_Verbosity.None, 0L, 0L, 0L, 0L, 0L, 0L, OodleLZ_Decode_ThreadPhase.Unthreaded);

                if (result <= 0)
                {
                    throw new DataException($"Oodle decompression failed with result {result}");
                }
            }
        }
    }
}