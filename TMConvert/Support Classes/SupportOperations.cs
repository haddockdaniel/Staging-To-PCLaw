using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StagingConvert
{
    public class SupportOperations
    {

        private string SQLServer = "";
        private string Database = "";
        public string errorMessage {get; set;}
        public string connectionString { get; set; }

        public SupportOperations(string server, string db)
        {
            SQLServer = server;
            Database = db;
            connectionString = "Data Source=" + server + ";Initial Catalog=" + db + ";Integrated Security=SSPI;";
        }

        public SupportOperations()
        { }

        public bool testSQLConnection()
        {

            using (var l_oConnection = new SqlConnection(connectionString))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }

        }


        //The label names and text are done in such a way so the order and method name they call (from TMConversion)
        //are derived from their name and text. Do not rename them unless you are sure
        public List<ConversionObject> createConvObjects(List<Label> labelList)
        {
            List<ConversionObject> convObj = new List<ConversionObject>();
            ConversionObject co = null;
           // MessageBox.Show(labelList.Count.ToString());
            foreach (Label lbl in labelList)
            {
                co = new ConversionObject();
                co.label = lbl;
                co.method = "Import" + lbl.Text;
                co.order = Int32.Parse(lbl.Name.ToLower().Trim().Replace("label", ""));
                convObj.Add(co);
            }
            return convObj;
        }
    }
}
