using System;
using System.Collections.Generic;

namespace SplitIt.Models
{
    public class Record
    {
        public string RecordId { get; set; }
        public List<Items> ItemsList { get; set; }
        public DateTime DateCreated { get; set; }
        public Dictionary<string, double> PersonTotals { get; set; }
    }
}
