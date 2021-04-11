using System;
using SearchLibrary;

namespace ConsoleApp
{
    class Program
    {
        private static SearchEngine searchEngine;
        private const string ELASTIC_URI = "http://localhost:9200";
        static void Main(string[] args)
        {
            Console.WriteLine("Is the documents index created? (y/n)");
            var createdIndex = Console.ReadLine().ToLower();
            searchEngine = new SearchEngine(new Uri(ELASTIC_URI), createdIndex[0] == 'y');

            Console.WriteLine("Do you want to post new documents? (path/n) ");
            var newDocumentPath = Console.ReadLine().ToLower();

            if (newDocumentPath != "n")
                searchEngine.PostDocuments(newDocumentPath);

            var command = "";
            while (command != "exit()")
            {
                Console.WriteLine("Enter your query or exit() to end.");
            
                command = Console.ReadLine();
                var query = new Query(command);
                var responseList = searchEngine.Search(query.Normals, query.Pluses, query.Minuses);
                Console.WriteLine($"Results:\n Docuemtn Ids: \n {string.Join(", ", responseList)}\n ");
            }
        }
    }
}
