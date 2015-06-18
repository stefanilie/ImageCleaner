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
            string path = "C:\\Pictures\\";
            Cleanup objCleanup = new Cleanup();
            objCleanup.renameAll(path);

            Console.Read();
        }
    }
}
