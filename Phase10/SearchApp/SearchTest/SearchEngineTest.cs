using System;
using System.Collections.Generic;
using SearchLibrary;
using Xunit;

namespace SearchTest
{
    public class SearchEngineTest : IDisposable
    {
        static SearchEngine searchEngine = new SearchEngine("test-index", new Uri("http://localhost:9200"), false);

        [Fact]
        public void SearchTest()
        {
            try
            {
                searchEngine.PostDocuments("../../../../Test-Data");
                searchEngine.elastic.Refresh();
                var testResult = searchEngine.Search(new List<string> { "hello" }, new List<string> { "world" }, new List<string> { "freedom" });
                Assert.Equal("testData1", testResult[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            searchEngine.elastic.DeleteIndex();
            searchEngine.elastic.Refresh();
        }
    }
}