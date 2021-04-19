using System.Collections.Generic;
using Nest;

namespace SearchApi.Models
{
    public interface ISearchEngine
    {
        public BulkResponse PostDocuments(string path);

        public List<string> Search(List<string> normals, List<string> pluses, List<string> minuses);

        public List<QueryContainer> MakeMatchQueryList(List<string> words);
    }
}