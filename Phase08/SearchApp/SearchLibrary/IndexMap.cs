using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SearchLibrary
{
    public class IndexMap
    {
        public string Word { get; set; }
        public int EntryId {get; set;}
        public Entry Entry { get; set;}
    }
}