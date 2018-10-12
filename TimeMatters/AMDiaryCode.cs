using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using PLConvert;

namespace APIObjects
{
    public class AMDiaryCode : StagingTable
    {
        public AMDiaryCode()
        {
        }//end constructor

        public AMDiaryCode(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;

            //i seperated these into different methods so it wouldnt be one giant mass of code
            addCodes();

        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PCLaw, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method

        public override string ToString()
        {
            return "DiaryCode";
        }//end method

        private void addCodes()
        {
            DataTable Table = new DataTable("Code");
            string sSelect = "";
            int nNameCode = 0;
            List<string> diaryCodes = new List<string>();

            while (PCLaw.DiaryCode.GetNextRecord() == 0)//make sure they dont already exist
                diaryCodes.Add(PCLaw.DiaryCode.Name.Trim());

            sSelect = "SELECT [OldID] ,[Name] ,[Nickname] ,[CodeType] FROM [DiaryCodes]";
            ReadAMTable(ref Table, sSelect);

            for (nNameCode = 0; nNameCode < Table.Rows.Count; nNameCode++)
            {
                bool exists = false;
                Random r = new Random();
                foreach (string item in diaryCodes)
                {
                    if (item.Equals(Table.Rows[nNameCode]["Name"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                        break;
                    }//end if
                }//end foreach
                if (!exists)
                {
                    PCLaw.DiaryCode.Name = Table.Rows[nNameCode]["Name"].ToString().Trim();
                    PCLaw.DiaryCode.NickName = Table.Rows[nNameCode]["Nickname"].ToString().Trim().ToLower();
                    //see if the nickname already exists and if it does, make a new one
                    if (PLConvert.PLDiaryCode.GetIDFromNN(Table.Rows[nNameCode]["NickName"].ToString().Trim().ToLower()) == 0)
                        PCLaw.DiaryCode.NickName = Table.Rows[nNameCode]["Nickname"].ToString().Trim().ToLower();
                    else
                    {
                        int NNcount = 0;
                        while (true)
                        {
                            NNcount++;
                            if (PLConvert.PLDiaryCode.GetIDFromNN(PCLaw.DiaryCode.NickName) == 0)
                                break;
                            else
                                PCLaw.DiaryCode.NickName = Table.Rows[nNameCode]["Nickname"].ToString().Trim().ToLower() + NNcount.ToString();
                        }//end while
                    }//end else
                    PCLaw.DiaryCode.ExternalID_1 = Table.Rows[nNameCode]["Nickname"].ToString().Trim();
                    PCLaw.DiaryCode.ExternalID_2 = Table.Rows[nNameCode]["Name"].ToString().Trim();

                    PCLaw.DiaryCode.AddRecord();
                }//end if
            }//end for
            PCLaw.DiaryCode.SendLast();
        }//end method
    }//end class
}//end namespace
