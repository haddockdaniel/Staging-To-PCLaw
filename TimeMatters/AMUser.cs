using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Reflection;


namespace APIObjects
{

    public class AMUser : StagingTable
    {
        public AMUser()
        {
        }//end constructor

        public AMUser(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;

            DataTable Table = new DataTable("User");
            string sSelect = "SELECT * FROM [user]";
            ReadAMTable(ref Table, sSelect);

            for (int nUser = 0; nUser < Table.Rows.Count; nUser++)
            {
                
                PCLaw.User.Name = Table.Rows[nUser]["name"].ToString().Trim();
                PCLaw.User.Password = Table.Rows[nUser]["password"].ToString().Trim();
                PCLaw.User.NewPassword = Table.Rows[nUser]["password"].ToString().Trim();

                PCLaw.User.NickName = Table.Rows[nUser]["nickname"].ToString().Trim();
                //see if the nickname already exists and if it does, generate a new one for this entry
                if (PLConvert.PLUser.GetIDFromNN(Table.Rows[nUser]["nickname"].ToString().Trim()) == 0)
                    PCLaw.User.NickName = Table.Rows[nUser]["nickname"].ToString().Trim();
                else
                {
                    int NNcount = 0;
                    while (true)
                    {
                        NNcount++;
                        if (PLConvert.PLUser.GetIDFromNN(PCLaw.User.NickName) == 0)
                            break;
                        else
                            PCLaw.User.NickName = Table.Rows[nUser]["nickname"].ToString().Trim() + NNcount.ToString();
                    }//end while
                }//end else
                PCLaw.User.ExternalID_1 = Table.Rows[nUser]["nickname"].ToString().Trim();
                PCLaw.User.ExternalID_2 = Table.Rows[nUser]["Userid"].ToString().Trim();
                PCLaw.User.Status = PLConvert.PLXMLData.eSTATUS.ACTIVE;
                PCLaw.User.AddGroupID(1); //adds to default group

                PCLaw.User.AddRecord();
            }//end for
            /*
            //add lawyers as users - this has to be done to import phone calls
            while (PCLaw.Lawyer.GetNextRecord() == 0)
            {
                PCLaw.User.Name = PCLaw.Lawyer.Name.Trim();
                PCLaw.User.Password = "admin";
                PCLaw.User.NewPassword = "admin";
                //PCLaw.User.Status = PCLaw.Lawyer.Status;

                PCLaw.User.NickName = PCLaw.Lawyer.NickName.Trim();
                //see if the nickname already exists and if it does, generate a new one for this entry
                if (PLConvert.PLUser.GetIDFromNN(PCLaw.Lawyer.NickName.Trim()) == 0)
                    PCLaw.User.NickName = PCLaw.Lawyer.NickName.Trim();
                else
                {
                    int NNcount = 0;
                    while (true)
                    {
                        NNcount++;
                        if (PLConvert.PLUser.GetIDFromNN(PCLaw.User.NickName) == 0)
                            break;
                        else
                            PCLaw.User.NickName = PCLaw.Lawyer.NickName.Trim() + NNcount.ToString();
                    }//end while
                }//end else
                PCLaw.User.ExternalID_1 = PCLaw.Lawyer.NickName.Trim();
                PCLaw.User.Status = PLConvert.PLXMLData.eSTATUS.ACTIVE;
                PCLaw.User.AddGroupID(1);

                PCLaw.User.AddRecord(); 
            }//end while

            PCLaw.User.SendLast();
             * */
        }//end of method

        public override void AddRecords(PLConvert.PCLawConversion PCLaw, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end of method

        public override string ToString()
        {
            return "Users";
        }//end of method
    }//end class
}//end namespace
