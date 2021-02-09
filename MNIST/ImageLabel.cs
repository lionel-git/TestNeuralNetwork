using System;
using System.Collections.Generic;
using System.Text;

namespace MNIST
{
    public class ImageLabel
    {
        public double[] Pixels { get; set; } // 28*28 image with pixel in [0,1]

        public byte Label { get; set; } // 0 to 9

        public ImageLabel(byte label)
        {
            Label = label;
        }
    }
}
