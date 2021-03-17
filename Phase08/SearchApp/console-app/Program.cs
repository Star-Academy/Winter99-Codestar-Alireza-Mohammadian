using System;
using System.IO;
using System.Linq;
using SearchLibrary;
using System.Collections.Generic;

namespace console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SearchContext()) {
                var path = "data/EnglishData";
                var fr = new FileReader(path);
                var docs = fr.ReadContent();
                var documentsList = new List<Document>();
                foreach (var doc in docs){
                    var tempDoc =new Document(){
                        DocumentName = doc.Key ,
                        Content = doc.Value
                    };
                    documentsList.Add(tempDoc);
                }
                context.Documents.AddRange(documentsList);
                context.SaveChanges();  
            }
        }
        
    }

    public class FileReader
    {
        public string path { get; set; }
        public FileReader(string path)
        {
            this.path = path;
        }

        public Dictionary<string, string> ReadContent()
        {
            var filesContent = new Dictionary<string, string>();
            System.Array.ForEach(this.GetFiles(), file => filesContent.Add(file.Split("\\").Last(), File.ReadAllText(file).Trim()));
            return filesContent;
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(path);
        }
    }
}
