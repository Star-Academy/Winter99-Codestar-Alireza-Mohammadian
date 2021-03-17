using System.Collections.Generic;

namespace SearchLibrary
{
    public class Entry
    {
        public int EntryId { get; set; }
        public string DocumentName { get; set; }
        public int Index { get; set; }
        
        public ICollection<IndexMap> IndexMap { get; set; }    
    }
}