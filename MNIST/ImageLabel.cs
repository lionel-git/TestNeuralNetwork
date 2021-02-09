using System;
using System.Collections.Generic;
using System.Text;

namespace MNIST
{
    public class ImageLabel
    {
        public double[] Pixels { get; set; } // 28*28 image with pixel in [0,1]

        public byte Label { get; set; } // 0 to 9

        public int Rows { get; set; }
        public int Columns { get; set; }

        private static string GreyLevel1 = @"$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\|()1{}[]?-_+~<>i!lI;:,^`'. ";
        private static string GreyLevel2 = @" .:-=+*#%@";

        private static char GetGreyLevel(double v, bool useGreyLevel1)
        {
            string grayLevel;
            if (useGreyLevel1)
            {
                grayLevel = GreyLevel1;
                v = 1.0 - v;
            }
            else
            {
                grayLevel = GreyLevel2;
            }

            int pos = (int)(v * grayLevel.Length);
            pos = Math.Max(pos, 0);
            pos = Math.Min(pos, grayLevel.Length - 1);
            return grayLevel[pos];
        }

        public ImageLabel(byte label)
        {
            Label = label;
        }

        public string GetImageString()
        {
            var sb = new StringBuilder();
            if (Rows * Columns != Pixels.Length)
                throw new Exception("Invalid image content");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double v = Pixels[i * Columns + j];
                    sb.Append(GetGreyLevel(v, true));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
