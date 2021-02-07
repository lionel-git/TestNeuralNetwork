using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace MNIST
{
    // http://yann.lecun.com/exdb/mnist/
    // 2 types: 
    //  labels [ 0 - 9 ], magic = 2049
    // Images: N images de 28x28 by row pixel de 0 a 255, , magic = 2051
    public class MnistReader
    {
        public MnistReader()
        {
        }

        public byte[] LoadSample(string path)
        {
            var data = Helpers.Decompress(path);
            return data;
        }
    }
}
