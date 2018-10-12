using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIObjects
{
    public class Invoice
    {
        public int newID { get; set; }
        public int number { get; set; }
        public string oldID { get; set; }
        public int date { get; set; }
    }
}
