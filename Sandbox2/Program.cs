using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Sandbox2
{
    class Steganography
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("drzewa.png"))
            {
                const string message = "Siemanko, witam w mojej kuchni!";
                string decrypted_message = "";
                Bitmap bmp = new Bitmap("drzewa.png");

                Console.WriteLine($"Image dimensions: {bmp.Width} x {bmp.Height}");
            }

            else
            {
                Console.WriteLine("File not found");
            }

            Console.ReadKey();
        }
    }
}
