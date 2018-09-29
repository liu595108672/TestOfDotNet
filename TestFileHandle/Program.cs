using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFileHandle
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileHandle fileHandle = new FileHandle();
            //fileHandle.fileName = "";
            //fileHandle.filePath = @"E:\TestTmp\TestFile";

            //fileHandle.CreateNewFile("newFileTest");

            //fileHandle.GetFileProperty("test.txt");

            //fileHandle.TestException();






            ModifyFiles modifyFiles = new ModifyFiles();
            modifyFiles.DeletePdbAndLogFiles(@"E:\GitLab\DGW\Dgw-3.0.2.1\Release\Plugins");

            modifyFiles.ChangeCompressedFilesName("zip", @"E:\GitLab\DGW\Dgw-3.0.2.1\Release\Plugins");






            //FileReading fileReading = new FileReading();
            //fileReading.ReadFileAndSerilaize();

        }
    }
}
