namespace SearchApi.Models
{
    public class Document
    {
        public string documentId { get; set; }
        public string content { get; set; }

        public Document(string documentId, string content)
        {
            this.documentId = documentId;
            this.content = content;
        }
    }
}