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
        public List<ImageLabel> ImagesLabels { get; set; }

        public MnistReader()
        {
        }

        private static uint ReadUint32(byte[] data, ref int offset)
        {
            uint v = 0;
            for (int i = 0; i < 4; i++)
                v |= ((uint)data[offset + i]) << (8 * (3-i));
            offset += 4;
            return v;
        }

        private static double[] ReadImageValues(byte[] data, uint count, ref int offset)
        {
            var values = new double[count];
            for (int i = 0; i < count; i++)
                values[i] = data[offset + i]/255.0;
            offset += (int)count;
            return values;
        }

        private void ProcessLabels(byte[] data)
        {
            int offset = 0;
            uint magic = ReadUint32(data, ref offset);
            if (magic != 2049)
                throw new Exception($"Invalid file, magic={magic}");
            uint nbItems = ReadUint32(data, ref offset);

            if (data.Length != offset + nbItems)
                throw new Exception($"File size is invalid");

            ImagesLabels = new List<ImageLabel>((int)nbItems);
            for (int i = 0; i < nbItems; i++)
                ImagesLabels.Add(new ImageLabel(data[offset + i]));
        }

       

        private void ProcessImages(byte[] data)
        {
            int offset = 0;
            uint magic = ReadUint32(data, ref offset);
            if (magic != 2051)
                throw new Exception($"Invalid file, magic={magic}");
            uint nbItems = ReadUint32(data, ref offset);
            uint nbRows = ReadUint32(data, ref offset);
            uint nbCols = ReadUint32(data, ref offset);

            if (data.Length != offset + nbItems *nbRows *nbCols)
                throw new Exception($"File size is invalid");

            if (nbItems != ImagesLabels.Count)
                throw new Exception($"Labels and images do not match");

            for (int i = 0; i < nbItems; i++)
            {
                ImagesLabels[i].Rows = (int)nbRows;
                ImagesLabels[i].Columns = (int)nbCols;
                ImagesLabels[i].Pixels = ReadImageValues(data, nbRows * nbCols, ref offset);
            }

            if (offset!=data.Length)
                throw new Exception($"Error reading all datas");
        }

        public void LoadImagesAndLabels(string pathImages, string pathLabels)
        {
            var dataLabels = Helpers.Decompress(pathLabels);
            ProcessLabels(dataLabels);

            var dataImages = Helpers.Decompress(pathImages);
            ProcessImages(dataImages);
        }      
    }
}
