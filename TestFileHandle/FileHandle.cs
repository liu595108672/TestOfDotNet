using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestFileHandle
{
    class FileHandle
    {
        public string filePath = "";
        public string fileName = "";


        public FileHandle(string fileName, string filePath)
        {
            this.fileName = fileName;
            this.filePath = filePath;
        }
        public FileHandle(string filePath)
        {
            this.filePath = filePath;
        }

        public FileHandle( )
        {

        }

        public void CreateNewFile(string fileName)
        {

        }


        public void AppandFile(string content)
        {

        }

        public void DeleteThisFile( )
        {

        }
        public void DeleteFile(string fileName)
        {
            if(File.Exists(filePath + fileName))
            {
                try
                {
                    File.Delete(filePath + "\\" + fileName);
                }
                catch(Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                    throw;
                }
            }
        }

        public void GetFileProperty(string fileName)
        {
            try
            {
                this.fileName += fileName;
                var file = filePath + "\\" + fileName;
                if(File.Exists(file))
                {
                    var fAttributes = File.GetAttributes(filePath + "\\" + fileName);
                    var fCreatedTime = File.GetCreationTime(file);
                    var fLastWriteTime = File.GetLastWriteTime(file);
                    var fLastAccessTime = File.GetLastAccessTime(file); 
                }
            }
            catch(Exception e)
            {

                throw;
            }


        }


        public void TestException( )
        {
            string filePath = @"E:\TestTmp\TestFile";
            var fileList = Directory.GetFiles(filePath);



            foreach(var item in fileList)
            {
                var sr = new StreamReader(item);

                try
                {
                    File.Delete(item);
                }
                catch(Exception e)
                {
                    
                }
            }
        }
    }
}
