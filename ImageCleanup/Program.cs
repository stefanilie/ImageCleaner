using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Pictures\\4276672.JPG";
            Cleanup objCleanup = new Cleanup();
            FileInfo fl = new FileInfo(path);
            objCleanup.rename(fl);

            Console.Read();
        }
    }
}
