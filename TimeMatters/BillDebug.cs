using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace APIObjects
{
    public class BillDebug
    {
        public BillDebug()
        {
            invID = 0;
            matterID = "Fake";
            difference = -99.99;
            applied = -99.99;
            received = -99.99;
            oldID = "Fake";
        }



        public int invID { get; set; }
        public string matterID { get; set; }
        public double difference { get; set; }
        public double applied { get; set; }
        public double received { get; set; }
        public string oldID { get; set; }
    }
}
