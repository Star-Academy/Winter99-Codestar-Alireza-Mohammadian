using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace SearchApi.Models
{
    public class FileReader
    {
        public string path;
        public FileReader(string path)
        {
            this.path = path;
        }

        public List<Document> ReadContent()
        {
            var filesContent = new List<Document>();
            System.Array.ForEach(this.GetFiles(), file => filesContent.Add(new Document(file.Split("\\").Last(), File.ReadAllText(file).Trim())));
            return filesContent;
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(path);
        }
    }
}