using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SearchApi.Models
{
    public class Query
    {
        public const string NORMAL_PATTERN = @"\s(\w+)";
        public const string PLUS_PATTERN = @"\+(\w+)";
        public const string MINUS_PATTERN = @"-(\w+)";
        private const string SPACE_PATTERN = @"\s+";

        public List<string> normals { get; }
        public List<string> pluses { get; }
        public List<string> minuses { get; }
        public Query(string query){ 
            normals = FindPattern(" "+query , NORMAL_PATTERN);
            minuses = FindPattern(query , MINUS_PATTERN);
            pluses = FindPattern(query , PLUS_PATTERN);   
        }
        
        public static List<string> FindPattern(string query, string pattern)
        {
            var words = new List<string>();
            Regex regex = new Regex(pattern);
            MatchCollection mathces = regex.Matches(query);
            foreach (Match match in mathces)
                words.Add(match.Groups[1].Value);
            return words;
        }
    }
}