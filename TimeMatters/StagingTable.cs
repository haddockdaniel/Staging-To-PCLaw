using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace APIObjects
{
    public abstract class StagingTable : object
    {
        public StagingTable()
        {
            AMIDtoPLID = new Dictionary<string, int>();
            AMNametoPLName = new Dictionary<string, string>();
            AMNametoAMID = new Dictionary<string, string>();
            existingPLNNs = new List<string>();
        }//end constructor

        public string sAMServer;
        public void ReadAMTable(ref DataTable Table, string sSelect)
        {
            string sConn = string.Empty;

            if (Table == null)
                Table = new DataTable();
            try
            {
                sConn = @"Data Source=localhost;Initial Catalog=PCLawStg;Integrated Security=SSPI;";
                //sConn = @"Data Source=npc365;Initial Catalog=Amicus;Integrated Security=True;";
                //SqlConnection Conn = new SqlConnection(sConn);
                //Conn.Open();
                SqlDataAdapter Adapter = new SqlDataAdapter(sSelect, sConn);//Conn);
                //Conn.Close();
                Adapter.Fill(Table);
            }//end try
            catch (Exception objError)
            {
                string sError = objError.ToString();
                System.Diagnostics.Debug.Assert(false);
            }//end catch
        }//end method

        public override string ToString()
        {
            return base.ToString();
        }//end method

        public abstract void AddRecords(bool bPCLawMoreUptoDate);
        public abstract void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate);

        public Dictionary<string, int> AMIDtoPLID;
        public Dictionary<string, string> AMNametoPLName;
        public Dictionary<string, string> AMNametoAMID;
        protected PLConvert.PCLawConversion PCLaw;
        public List<string> existingPLNNs;


    }

}
