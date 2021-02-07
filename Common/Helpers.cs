using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Common
{
    public static class Helpers
    {
        public static double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public static double SigmoidDerivedFromValue(double y)
        {
            return y * (1.0 - y);
        }

        public static double ReLu(double x)
        {
            return x > 0 ? x : 0.0;
        }

        public static double ReLuDerivedFromValue(double y)
        {
            // if (abs(y)<eps ? 0 ou 1)
            return y > 0 ? 1.0 : 0.0;
        }

        public static void DisplayList(string msg, IList<double> list)
        {
            Console.Write($"{msg} ({list.Count}): ");
            for (int i = 0; i < list.Count; i++)
                Console.Write($" {list[i]}");
            Console.WriteLine();
        }

        public static byte[] Decompress(string path)
        {
            var fileToDecompress = new FileInfo(path);
            using (var originalFileStream = fileToDecompress.OpenRead())
            {
                using (var decompressedFileStream = new MemoryStream())
                {
                    using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        return decompressedFileStream.ToArray();
                    }
                }
            }
        }
    }
}
