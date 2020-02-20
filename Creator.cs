using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Xceed.Words.NET;
using System.IO;
using System.IO.Compression;


namespace FileCreator
{
    public class Creator
    {
        public void CreateFiles(List<FileData> filesData, string mode, bool archiveFiles)
        {
            try
            {
                switch (mode)
                {
                    case "WORD":
                        Parallel.ForEach(filesData, fileData => CreateWordFile(fileData, archiveFiles));
                        break;
                    case "BAT":
                        Parallel.ForEach(filesData, fileData => CreateBatFile(fileData, archiveFiles));
                        break;
                    case "SH":
                        Parallel.ForEach(filesData, fileData => CreateShFile(fileData, archiveFiles));
                        break;
                    case "CMD":
                        Parallel.ForEach(filesData, fileData => CreateCmdFile(fileData, archiveFiles));
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ArchiveFile(FileData fileData, string format)
        {
            try
            {
                string filePath = String.Format(fileData.FileDirectory + "\\" + fileData.FileName);
                using (ZipArchive archive = ZipFile.Open(String.Format(filePath + format.Trim('.') + ".zip"), ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(String.Format(filePath + format), String.Format(fileData.FileName + format));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateWordFile(FileData fileData, bool archiveFiles)
        {
            try
            {
                string filePath = String.Format(fileData.FileDirectory + "\\" + fileData.FileName);
                if (!Directory.Exists(fileData.FileDirectory))
                {
                    Directory.CreateDirectory(fileData.FileDirectory);
                }
                using (var doc = Word.DocX.Create(filePath))
                {
                    doc.InsertParagraphs(fileData.FileContext);
                    doc.Save();                  
                }
                if (archiveFiles)
                {
                    ArchiveFile(fileData, ".docx");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void CreateBatFile(FileData fileData, bool archiveFiles)
        {
            try
            {
                string filePath = String.Format(fileData.FileDirectory + "\\" + fileData.FileName);
                if (!Directory.Exists(fileData.FileDirectory))
                {
                    Directory.CreateDirectory(fileData.FileDirectory);
                }
                using (FileStream fileStream = new FileStream(String.Format(filePath + ".bat"), FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine(String.Format("@echo " + fileData.FileContext));
                        streamWriter.WriteLine("@pause");
                    }
                }
                if (archiveFiles)
                {
                    ArchiveFile(fileData, ".bat");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void CreateCmdFile(FileData fileData, bool archiveFiles)
        {
            try
            {
                string filePath = String.Format(fileData.FileDirectory + "\\" + fileData.FileName);
                if (!Directory.Exists(fileData.FileDirectory))
                {
                    Directory.CreateDirectory(fileData.FileDirectory);
                }
                using (FileStream fileStream = new FileStream(String.Format(filePath + ".cmd"), FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine(String.Format("@echo " + fileData.FileContext));
                        streamWriter.WriteLine("@pause");
                    }
                }
                if (archiveFiles)
                {
                    ArchiveFile(fileData, ".cmd");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateShFile(FileData fileData, bool archiveFiles)
        {
            try
            {
                string filePath = String.Format(fileData.FileDirectory + "\\" + fileData.FileName);
                if (!Directory.Exists(fileData.FileDirectory))
                {
                    Directory.CreateDirectory(fileData.FileDirectory);
                }
                using (FileStream fileStream = new FileStream(String.Format(filePath + ".sh"), FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write("#!/bin/bash\n");
                        streamWriter.WriteLine("echo " + fileData.FileName);
                        streamWriter.WriteLine("mkdir  testdir");
                        streamWriter.WriteLine("touch test1 test2 test3");
                        streamWriter.WriteLine("echo test1 >> test1");
                        streamWriter.WriteLine("echo test2 >> test2");
                        streamWriter.WriteLine("echo test3 >> test3");
                        streamWriter.WriteLine("cat test1");
                    }
                }
                if (archiveFiles)
                {
                    ArchiveFile(fileData, ".sh");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
