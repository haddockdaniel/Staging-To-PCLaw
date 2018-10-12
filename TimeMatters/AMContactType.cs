using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using PLConvert;
using System.Windows.Forms;

namespace APIObjects
{
    public class AMContactType : StagingTable
    {
        public AMContactType()
        {
        }//end constructor

        public AMContactType(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            try
            {
                if (PCLaw == null)
                    return;

                DataTable Table = new DataTable("ContactType");

                string sSelect = "SELECT * from ContactTypes";
                ReadAMTable(ref Table, sSelect);

                List<string> existingContactTypes = new List<string>();
                //gets existing contact types to help prevent dupes
                while (PCLaw.ContactType.GetNextRecord() == 0)
                    existingContactTypes.Add(PCLaw.TypeOfLaw.Name.Trim());

                for (int nContactType = 0; nContactType < Table.Rows.Count; nContactType++)
                {

                    bool match = false;//see if the name already exists in PCLaw, if it does, dont add it
                    foreach (string tol in existingContactTypes)
                    {
                        if (tol.Equals(Table.Rows[nContactType]["Name"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            match = true;
                            break;
                        }//end if
                    }//end foreach

                    if (!match) //if we didnt find a match, add that bad boy
                    {
                        PCLaw.ContactType.Name = Table.Rows[nContactType]["name"].ToString();
                        PCLaw.ContactType.NickName = Table.Rows[nContactType]["nickname"].ToString();
                        //see if the nickname already exists and if it does, generate a new one for this entry
                        if (PLConvert.PLContactType.GetIDFromNN(Table.Rows[nContactType]["NickName"].ToString().Trim().ToLower()) == 0)
                            PCLaw.ContactType.NickName = Table.Rows[nContactType]["Nickname"].ToString().Trim().ToLower();
                        else
                        {
                            int NNcount = 0;
                            while (true)
                            {
                                NNcount++;
                                if (PLConvert.PLContactType.GetIDFromNN(PCLaw.ContactType.NickName) == 0)
                                    break;
                                else
                                    PCLaw.ContactType.NickName = Table.Rows[nContactType]["Nickname"].ToString().Trim() + NNcount.ToString();
                            }//end while
                        }//end else
                        PCLaw.ContactType.ExternalID_1 = Table.Rows[nContactType]["nickname"].ToString();
                        PCLaw.ContactType.ExternalID_2 = Table.Rows[nContactType]["name"].ToString();

                        PCLaw.ContactType.AddRecord();
                    }//end if
                }//end for

                PCLaw.ContactType.SendLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PCLaw, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method

        public override string ToString()
        {
            return "ContactType";
        }//end method

    }//end class
}//end namespace
