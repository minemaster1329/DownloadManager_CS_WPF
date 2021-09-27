using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using FluentFTP;
using System.Net;

using Newtonsoft.Json;

namespace Sandbox2
{
    class MainObject
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<MainObject> mainObjects = new List<MainObject>();

            for (int i = 0; i < 10; i++) mainObjects.Add(new MainObject() { Title = "My title", Message = "My Message" });

            
        }
    }
}
