using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using Xunit;
using SearchLibrary;
using Elasticsearch.Net;
using System.Collections.Generic;

namespace SearchTest
{
    public class ElasticTest : IDisposable
    {
        private static string indexName = "test-index";
        private Elastic elastic = new Elastic(indexName, new Uri("http://localhost:9200"));

        public ElasticTest()
        {
            elastic.CreateIndex<Document>();
        }

        [Fact]
        public void CreateClientTest()
        {
            var pingResponse = elastic.Client.Ping();
            Assert.True(pingResponse.IsValid);
        }

        [Fact]
        public void IndexTest()
        {
            var response = elastic.Client.Indices.Exists(indexName).Validate();
            Assert.True(response.Exists);
        }

        [Fact]
        public void CreateMatchQueryTest()
        {
            var queryContainer = Elastic.MakeMatchQuery("query-test", "field-test", fuzziness: 1);
            var queryString = elastic.Client.RequestResponseSerializer.SerializeToString(queryContainer);
            var realQuery = "{\"match\":{\"field-test\":{\"fuzziness\":1,\"query\":\"query-test\"}}}";
            Assert.Equal(realQuery, queryString);
        }

        [Fact]
        public void BulkTest()
        {
            var documents = new List<Document> { new Document("1", "one"), new Document("2", "two"), new Document("3", "three") };
            elastic.BulkIndex<Document>(documents, "DocumentId");
            elastic.Refresh();
            var response = elastic.GetResponseOfQuery<Document>(Elastic.MakeMatchQuery("one", "content"));
            Assert.Equal("one", response.Hits.ToList().Single().Source.Content);
        }

        public void Dispose()
        {
            elastic.DeleteIndex();
            elastic.Refresh();
        }
    }
}
