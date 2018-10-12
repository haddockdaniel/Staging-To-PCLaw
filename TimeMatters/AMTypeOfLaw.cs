using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APIObjects
{
    public class AMTypeOfLaw : StagingTable
    {
        public AMTypeOfLaw()
        {
        }//en constructor

        public AMTypeOfLaw(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {

            if (PCLaw == null)
                return;

            DataTable Table = new DataTable("TypeOfLaw");
            string sSelect = "";
            int nTypeOfLaw = 0;
            List<string> existingTypesOfLaw = new List<string>();

            //deal with duplicate types of law
            while (PCLaw.TypeOfLaw.GetNextRecord() == 0)
                existingTypesOfLaw.Add(PCLaw.TypeOfLaw.Name.Trim());

            sSelect = "SELECT * FROM [TypeOfLaw]";
            ReadAMTable(ref Table, sSelect);

            for (nTypeOfLaw = 0; nTypeOfLaw < Table.Rows.Count; nTypeOfLaw++)
            {
                //see if the name already exists. if so, dont add it
                bool match = false;
                foreach (string tol in existingTypesOfLaw)
                {
                    if (tol.Equals(Table.Rows[nTypeOfLaw]["Name"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        match = true;
                        break;
                    }
                }//end foreach
                if (!match)
                {
                    PCLaw.TypeOfLaw.Name = Table.Rows[nTypeOfLaw]["Name"].ToString().Trim();
                    PCLaw.TypeOfLaw.NickName = Table.Rows[nTypeOfLaw]["Nickname"].ToString().Trim().ToLower();
                    //see if the nickname already exists and if it does, generate a new one for this entry
                    if (PLConvert.PLTypeOfLaw.GetIDFromNN(Table.Rows[nTypeOfLaw]["NickName"].ToString().Trim().ToLower()) == 0)
                        PCLaw.TypeOfLaw.NickName = Table.Rows[nTypeOfLaw]["Nickname"].ToString().Trim().ToLower();
                    else
                    {
                        int NNcount = 0;
                        while (true)
                        {
                            NNcount++;
                            if (PLConvert.PLTypeOfLaw.GetIDFromNN(PCLaw.TypeOfLaw.NickName) == 0)
                                break;
                            else
                                PCLaw.TypeOfLaw.NickName = Table.Rows[nTypeOfLaw]["Nickname"].ToString().Trim().ToLower() + NNcount.ToString();
                        }//end while
                    }//end else
                    PCLaw.TypeOfLaw.ExternalID_1 = Table.Rows[nTypeOfLaw]["typeoflawid"].ToString().Trim();
                    PCLaw.TypeOfLaw.ExternalID_2 = Table.Rows[nTypeOfLaw]["Nickname"].ToString().Trim();
                    PCLaw.TypeOfLaw.AddRecord();
                }//end if

            }//end for

            //ad the default generic type of law if it doesnt exist
            if (PLConvert.PLTypeOfLaw.GetIDFromNN("NA~") == 0)
            {
                PCLaw.TypeOfLaw.Name = "Not Supplied";
                PCLaw.TypeOfLaw.NickName = "NA~";
                PCLaw.TypeOfLaw.AddRecord();
            }//end if

            PCLaw.TypeOfLaw.SendLast();
        }//ed method

        public override void AddRecords(PLConvert.PCLawConversion PCLaw, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method

        public override string ToString()
        {
            return "TypeOfLaw";
        }//end method
    }//end class
}//end namespace
