using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitIt.Models
{
    public class Items
    {
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int ItemQty { get; set; }

        public Person[] PaidBy { get; set; }


    }
}