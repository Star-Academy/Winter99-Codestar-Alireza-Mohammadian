using System.Linq;
using System.Text;

using System.Collections.Generic;
namespace library
{
    public class Result
    {
        public HashSet<Entry> resultSet { set; get; } = new HashSet<Entry>();
        public bool isQuery { get; set; }

        public Result(HashSet<Entry> resultSet, bool isQuery)
        {
            this.resultSet = resultSet;
            this.isQuery = isQuery;
        }

        public override string ToString()
        {
            var resultStr = new StringBuilder("");
            if (this.resultSet.Count ==  0) {
                resultStr.Append("No result found");
            } else {
                if (this.isQuery) {
                    foreach(Entry entry in resultSet){
                        resultStr.Append(" " + entry.DocName);
                    }
                } else {
                    
                    // foreach(Entry entry in resultSet){
                    //     //resultStr.Append(" " + entry.DocName +);//[Document Id: test2, Index: 14, Document Id: test1, Index: 11]"
                    // }
                    resultStr.Append(string.Join(", ", this.resultSet));
                }
            }
        return resultStr.ToString();
        }
    }
}