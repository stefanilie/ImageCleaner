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


        /**
         * Deletes a photo if it has a height or width lower than 1456.
         */
        public void cleanOne(string filePath)
        {
            try
            {
                var image = Image.FromFile(filePath);
                if (image.Height < 1456 || image.Width < 1456)
                    if (File.Exists(filePath))
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers(); 
                        File.Delete(filePath);
                    }
                        
                Console.WriteLine("\n\nSuccesfully delete file: "+filePath+"!");
            }
            catch (Exception e)
            {
                Console.Write("");
            }
        }

        /**
         * Deletes all photos with low res.
         */ 
        public void cleanAll(string folderPath) 
        {
            string[] filePaths = Directory.GetFiles(folderPath);
            foreach (string file in filePaths)
            {
                this.cleanOne(file);
            }
        }

        /**
         * Renames all photos in the folder.
         */ 
        public void renameAll(string folderPath)
        {
            string[] filePaths = Directory.GetFiles(folderPath);
            foreach (string file in filePaths)
            {
                this.rename(file, folderPath);
            }
        }

        /**
         * This will check wether the name already exists.
         */ 
        public bool nameExists(string fileName, string folderPath)
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);
            FileInfo[] fileInfo = d.GetFiles();
            foreach (FileInfo fi in fileInfo)
            {
                if (fi.Name == fileName)
                    return true;
            }
            return true;
        }        

        /**
         * This decodes the raw date from the image and changes it in the Nokia format.
         */ 
        public string decodeDate(string date)
        {
            //NEEDS REFACTOR
            string newDate = "";
            if (date != "")
            {
                string month = date[0].ToString();
                string day = date[2].ToString() + date[3].ToString();
                string year = date[5].ToString() + date[6].ToString() +
                    date[7].ToString() + date[8].ToString();
                newDate = year + month + day;
            }
            return newDate;
        }

        /**
         * Renames the photos by first getting the date it was taken,
         * then it decodes it so that it can have the Nokia format,
         * then it checks if the same name exists (case multiple photos were taken that day),
         * then it renames it.
         */ 
        public void rename(string path, string folderPath)
        {
            try
            {
                FileInfo f = new FileInfo(path);
                FileStream fs = new FileStream(f.FullName, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
                BitmapSource img = BitmapFrame.Create(fs);
                BitmapMetadata md = (BitmapMetadata)img.Metadata;
                string date = md.DateTaken;
                string newDate = this.decodeDate(date);
                string oldName = f.Name;
                if (newDate.Length > 0)
                {
                    string newName = "WP_" + newDate;
                    if (!this.nameExists(newName, folderPath))
                        System.IO.File.Move(f.Name, newName);
                    else if (this.nameExists(newName, folderPath) &&
                        newName.Split('_').Length - 1>1)
                    {
                        string[] toIterate = newName.Split('_');
                        int nToIterate = Convert.ToInt32(toIterate[2]) + 1;
                        toIterate[2] = nToIterate.ToString();
                        newName = toIterate[0] + toIterate[1] + toIterate[2];
                        System.IO.File.Move(f.Name, newName);
                    }
                    else if (this.nameExists(newName, folderPath) && 
                        newName.Split('_').Length-1==1)
                    {
                        newName += "_001";
                        System.IO.File.Move(f.Name, newName);
                    }
                }
                Console.WriteLine("Schimbat " + oldName + " in: " + f.Name);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
