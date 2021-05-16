using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace SearchApi.Models
{
    public class SearchEngine : ISearchEngine
    {
        public readonly Elastic elastic;
        public string indexName { get; }
        public SearchEngine(string indexName, Uri uri, bool indexCreated)
        {
            this.indexName = indexName;
            elastic = new Elastic(indexName, uri);
            if (!indexCreated)
                elastic.CreateIndex<Document>(mapSelector: CreateMapping).Validate();
        }

        public BulkResponse PostDocuments(string path)
        {
            return elastic.BulkIndex(new FileReader(path).ReadContent(), nameof(Document.documentId)).Validate();
        }

        public IndexResponse PostDocument(Document document)
        {
            return elastic.Index(document, nameof(Document.documentId)).Validate();
        }

        public static ITypeMapping CreateMapping(TypeMappingDescriptor<Document> mappingDescriptor)
        {
            return mappingDescriptor.Properties(d => d
                                        .Keyword(k => k
                                           .Name(d => d.documentId)
                                           .IgnoreAbove(256)
                                            ));
        }

        public List<string> Search(List<string> normals, List<string> pluses, List<string> minuses)
        {
            var must_list = MakeMatchQueryList(normals);
            must_list.Add(Elastic.MakeBoolQuery(
                            should: MakeMatchQueryList(pluses).ToArray()
                        ));
            var queryContainer = Elastic.MakeBoolQuery(
                    must: must_list.ToArray(),
                    mustNot: MakeMatchQueryList(minuses).ToArray()
                );

            var response = elastic.GetResponseOfQuery<Document>(queryContainer).Validate();
            return response.Hits.ToList().Select(x => x.Source.documentId).ToList();
        }

        public Document GetDocument(string id)
        {
            return elastic.GetDocument<Document>(id);
        }

        public List<QueryContainer> MakeMatchQueryList(List<string> words)
        {
            var list = new List<QueryContainer>();
            foreach (var word in words)
            {
                list.Add(Elastic.MakeMatchQuery(query: word, field: "content"));
            }
            return list;
        }
    }
}