using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Xceed.Words.NET;
using System.IO;


namespace WordsCreator
{
    public class Creator
    {
        public void CreateFile(string fileDirectory, string fileName, string fileText)
        {
            try
            {
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                using (var doc = Word.DocX.Create(String.Format(fileDirectory + fileName)))
                {

                    doc.InsertParagraphs(fileText);

                    doc.Save();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
            

    }
}
