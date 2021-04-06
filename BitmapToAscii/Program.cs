using System;
using System.Drawing;
using System.IO;

namespace BitmapToAscii
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\anm2\Desktop\Bitmap\Gummiente.jpg";
            var originalImage = new Bitmap(path);
            var scaledImage = new Bitmap(50, 50);
            var graphicsImage = Graphics.FromImage(scaledImage);
            var compressionRectangle = new Rectangle(0, 0, 50, 50);
            graphicsImage.DrawImage(originalImage, compressionRectangle);

            double[,] intensityValue = GetIntensityValues(scaledImage);
            WriteTextToFile(intensityValue, @"C:\Users\anm2\Desktop\Bitmap\ascii.txt");
        }

        static double[,] GetIntensityValues(Bitmap bitmap)
        {
            double[,] intensityValues = new double[bitmap.Width, bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var currentPixel = bitmap.GetPixel(x, y);

                    var r = currentPixel.R;
                    var g = currentPixel.G;
                    var b = currentPixel.B;

                    intensityValues[x, y] = Math.Round(((r + g + b) / 3) / 255.0, 1);
                }
            }

            return intensityValues;
        }

        static void WriteTextToFile(double[,] intensityValues, string path)
        {
            using (var streamWriter = new StreamWriter(path))
            {
                for (int y = 0; y < intensityValues.GetLength(1); y++)
                {
                    for (int x = 0; x < intensityValues.GetLength(0); x++)
                    {
                        var character = " ";

                        switch (intensityValues[x, y])
                        {
                            case 0: character = "@"; break;
                            case 0.1: character = "%"; break;
                            case 0.2: character = "#"; break;
                            case 0.3: character = "*"; break;
                            case 0.4: character = "+"; break;
                            case 0.5: character = "="; break;
                            case 0.6: character = "-"; break;
                            case 0.7: character = ":"; break;
                            case 0.8: character = "."; break;
                        }

                        Console.Write(character + character);
                        streamWriter.Write(character + character);
                    }

                    Console.WriteLine();
                    streamWriter.WriteLine();
                }
            }
        }
    }
}
