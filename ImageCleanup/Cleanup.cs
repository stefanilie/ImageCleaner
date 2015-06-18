using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageCleanup
{
    class Cleanup
    {
        // Later to be changed to folder path
        string filePath;

        // this will delete all the photos that are low res
        public void clean(string folderPath) { }

        public void rename(FileInfo f)
        {
            FileStream fs = new FileStream(f.FullName, FileMode.Open,
                FileAccess.Read, FileShare.Read);
            BitmapSource img = BitmapFrame.Create(fs);
            BitmapMetadata md = (BitmapMetadata)img.Metadata;
            string date = md.DateTaken;
            Console.WriteLine("Data pozei " + f.Name + " este: " + date);
        }
    }
}
