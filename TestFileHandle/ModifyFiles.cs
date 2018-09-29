using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace TestFileHandle
{
    public class ModifyFiles
    {
        public string compressPattern = @"\w*.zip$";
        public string exePattern = @"\w*.exe$";
        private List<string> zipFiles = new List<string>();
        private List<string> originalFiles = new List<string>();
        private List<string> originalDirectories = new List<string>();


        private List<string> GetAllFiles(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                return null;
            }
            var fileListToReturn = Directory.GetFiles(filePath).ToList();
            return fileListToReturn;
        }

        private List<string> GetAllDirectories(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return null;
            }
            var directoryListToReturn = Directory.GetDirectories(directoryPath).ToList();
            return directoryListToReturn;
        }

        private void GetZipFiles(List<string> files)
        {
            foreach (var item in files)
            {
                if (Regex.Match(item,compressPattern).Success)
                {
                    zipFiles.Add(item);
                }
            }
        }

        private void GetExeFiles(List<string> directories)
        {
            List<string> tmpFiles = new List<string>();
            foreach (var tmpDirectory in directories)
            {
                tmpFiles = Directory.GetFiles(tmpDirectory).ToList();
                foreach (var tmpFile in tmpFiles)
                {
                    if (Regex.Match(tmpFile,exePattern).Success)
                    {
                        originalFiles.Add(tmpFile);
                    }
                }
                
            }
        }

        private void ChangeZipName(string exeFilePath)
        {
            FileVersionInfo fInfo = FileVersionInfo.GetVersionInfo(exeFilePath);
            string mappingPartten = string.Empty;

            foreach (var item in zipFiles)
            {
                mappingPartten = Regex.Match(fInfo.OriginalFilename, @"(\w*)(?=\.exe)").Value + @"(?=\.zip)";
                var tmpMatch = Regex.Match(item, mappingPartten);
                if (tmpMatch.Success)
                {
                    FileInfo fileInfo = new FileInfo(item);
                    string tmpName = fileInfo.Directory+"\\" + tmpMatch.Value +"-"+ fInfo.FileVersion + fileInfo.Extension;
                    fileInfo.MoveTo(tmpName);
                    break;
                }
            }
        }

        public void ChangeCompressedFilesName(string compressType,string directotyPath = "")
        {
            compressPattern = @"\w*."+compressType+"$";
            if (string.IsNullOrEmpty(directotyPath))
            {
                directotyPath = @"I:\tmp\PluginsForTest";
            }
            GetZipFiles(GetAllFiles(directotyPath));
            GetExeFiles(GetAllDirectories(directotyPath));

            foreach (var item in originalFiles)
            {
                ChangeZipName(item);
            }
        }

        public void DeletePdbAndLogFiles(string dir="")
        {
            if (string.IsNullOrEmpty(dir))
            {
                dir = @"I:\tmp\PluginsForTest";
            }
            var allDirs = GetAllDirectories(dir);
            foreach (var dirSecond in allDirs)
            {
                //TODO: 找到所有的log文件，并将文件删除
                var allDirThird = GetAllDirectories(dirSecond);
                foreach (var dirThird in allDirThird)
                {
                    if (Regex.Match(dirThird, "log").Success);
                    {
                        Directory.Delete(dirThird,true);
                    }
                }
                var allFiles = GetAllFiles(dirSecond);
                foreach (var file in allFiles)
                {
                    //TODO: 匹配所有的  .pdb 文件，并且将文件删除
                    if (Regex.Match(file, @".*\.pdb").Success)
                    {
                        File.Delete(file);
                    }
                }
            }

        }
    }
}
