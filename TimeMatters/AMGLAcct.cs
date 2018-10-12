using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using PLConvert;

namespace APIObjects
{
    public class AMGLAcct : StagingTable
    {
        public AMGLAcct()
        {
        }//end constructor

        public AMGLAcct(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;

            
            DataTable Table = new DataTable("GLAcct");
            string sSelect = "";
            int nGLAccount = 0;
            List<string> existingGLAccts = new List<string>();

            //get all existing gl account names from pclaw
            while (PCLaw.GLAccts.GetNextRecord() == 0)
                existingGLAccts.Add(PCLaw.GLAccts.Name.Trim());

            sSelect = "SELECT * FROM [GLAcct]";
            ReadAMTable(ref Table, sSelect);

            //get each gl, see if they already exist, check to see if the shortname exists and add the gl account
            //if its a bank account then add it accordingly and add the gl reference, else add it as a gl account only

            //we add bank accounts first, then gl accounts (including bank accounts)
            for (nGLAccount = 0; nGLAccount < Table.Rows.Count; nGLAccount++) //only add the ones that arent already in pclaw
            {
                bool match = false;
                foreach (string tol in existingGLAccts)
                {
                    if (tol.Equals(Table.Rows[nGLAccount]["GLName"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        match = true;
                        break;
                    }//end if
                }//end foreach

                if (!match && !string.IsNullOrEmpty(Table.Rows[nGLAccount]["BankType"].ToString().Trim()))//since there is a bank type, we know its a bank account
                {
                    //add bank accounts first then add all GL accounts including bank accounts
                    switch (Table.Rows[nGLAccount]["BankType"].ToString().Trim())
                    {
                        case "G":
                            PCLaw.GBAcct.NickName = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                            if (!string.IsNullOrEmpty(Table.Rows[nGLAccount]["BankAccountNumber"].ToString().Trim()))
                                PCLaw.GBAcct.BankAcctNumber = Table.Rows[nGLAccount]["BankAccountNumber"].ToString().Trim();
                            else
                                PCLaw.GBAcct.BankAcctNumber = "99999";
                            PCLaw.GBAcct.BankName = Table.Rows[nGLAccount]["GLName"].ToString().Trim();
                            //see if the nickname already exists and if it does, generate a new one for this entry
                            if (PLConvert.PLGLAccts.GetIDFromNN(Table.Rows[nGLAccount]["NickName"].ToString().Trim()) == 0)
                                PCLaw.GBAcct.GLNN = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                            else
                            {
                                int NNcount = 0;
                                while (true)
                                {
                                    NNcount++;
                                    if (PLConvert.PLGLAccts.GetIDFromNN(PCLaw.GBAcct.GLNN) == 0)
                                        break;
                                    else
                                        PCLaw.GBAcct.GLNN = Table.Rows[nGLAccount]["Nickname"].ToString().Trim() + NNcount.ToString();
                                }//end while
                            }//end else

                            PCLaw.GBAcct.ExternalID_1 = Table.Rows[nGLAccount]["GLAcctID"].ToString().Trim();
                            PCLaw.GBAcct.ExternalID_2 = Table.Rows[nGLAccount]["NickName"].ToString().Trim();

                            PCLaw.GBAcct.ID = PLGBAcct.GetIDFromNN(PCLaw.GBAcct.NickName.ToUpper());

                            PCLaw.GBAcct.AddRecord();
                            break;
                        case "T":
                            PCLaw.TBAcct.NickName = Table.Rows[nGLAccount]["NickName"].ToString().Trim();

                            PCLaw.TBAcct.BankAcctNumber = Table.Rows[nGLAccount]["BankAccountNumber"].ToString().Trim();
                            PCLaw.TBAcct.BankName = Table.Rows[nGLAccount]["GLName"].ToString().Trim();

                            if (PLConvert.PLGLAccts.GetIDFromNN(Table.Rows[nGLAccount]["NickName"].ToString().Trim()) == 0)
                                PCLaw.TBAcct.GLNN = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                            else
                            {
                                int NNcount = 0;
                                while (true)
                                {
                                    NNcount++;
                                    if (PLConvert.PLGLAccts.GetIDFromNN(PCLaw.TBAcct.GLNN) == 0)
                                        break;
                                    else
                                        PCLaw.TBAcct.GLNN = Table.Rows[nGLAccount]["Nickname"].ToString().Trim() + NNcount.ToString();
                                }//end while
                            }//end else

                            PCLaw.TBAcct.ExternalID_1 = Table.Rows[nGLAccount]["GLAcctID"].ToString().Trim();
                            PCLaw.TBAcct.ExternalID_2 = Table.Rows[nGLAccount]["NickName"].ToString().Trim();

                            PCLaw.TBAcct.ID = PLTBAcct.GetIDFromNN(PCLaw.TBAcct.NickName.ToUpper());

                            PCLaw.TBAcct.AddRecord();
                            break;
                        default: //there are no other accounts so if you get here, you did something wrong
                            break;
                    }//end switch
                }//end if
            }//end foreach
            PCLaw.GBAcct.SendLast();
            PCLaw.TBAcct.SendLast();
            //done with bank accounts, now add glaccounts. You dont need to add bank acounts as gl accounts. 
            //When we added the bank accounts, they were automatically added to the gl
            for (nGLAccount = 0; nGLAccount < Table.Rows.Count; nGLAccount++)
            {
                bool match = false;
                foreach (string tol in existingGLAccts)
                {
                    if (tol.Equals(Table.Rows[nGLAccount]["GLName"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        match = true;
                        break;
                    }//end if
                }//end foreach

                if (!match && string.IsNullOrEmpty(Table.Rows[nGLAccount]["BankType"].ToString().Trim()))
                {
                    PCLaw.GLAccts.Name = Table.Rows[nGLAccount]["GLName"].ToString().Trim();
                    if (PCLaw.GLAccts.Name.Equals(""))
                        PCLaw.GLAccts.Name = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                    switch (Table.Rows[nGLAccount]["GLType"].ToString().Trim())
                    {
                        case "1":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.CURRENT_ASSET;
                            break;
                        case "2":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.SHORT_LIABILITY;
                            break;
                        case "3":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.EQUITY;
                            break;
                        case "4":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.INCOME;
                            break;
                        case "5":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.EXPENSE;
                            break;
                        case "6":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.LONG_LIABILITY;
                            break;
                        case "7":
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.FIXED_ASSET;
                            break;
                        default:
                            PCLaw.GLAccts.AcctType = PLGLAccts.eACCOUNT_TYPE.EXPENSE;
                            break;
                    }//end switch

                    existingGLAccts.Clear(); //we are done with it

                    //PCLaw.GLAccts.SpecialAcct = PLGLAccts.eSPEC_ACCT.NOT_SET; //todo. any way to tell how to get this done?
                    PCLaw.GLAccts.NickName = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                    if (PLConvert.PLGLAccts.GetIDFromNN(Table.Rows[nGLAccount]["NickName"].ToString().Trim()) == 0)
                        PCLaw.GLAccts.NickName = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                    else
                    {
                        int NNcount = 0;
                        while (true)
                        {
                            NNcount++;
                            if (PLConvert.PLGLAccts.GetIDFromNN(PCLaw.GLAccts.NickName) == 0)
                                break;
                            else
                                PCLaw.GLAccts.NickName = Table.Rows[nGLAccount]["Nickname"].ToString().Trim() + NNcount.ToString();
                        }//end while
                    }//end else
                    PCLaw.GLAccts.DepartmentNN = PLConvert.PLDepartment.GetNNFromID(1);
                  //  if (!string.IsNullOrEmpty(Table.Rows[nGLAccount]["SubAccount"].ToString().Trim()))
                   //     PCLaw.GLAccts.SuBAcctOfNN = Table.Rows[nGLAccount]["SubAccount"].ToString().Trim();
                    //MessageBox.Show(PCLaw.GLAccts.DepartmentNN);
                    //PCLaw.GLAccts.ID = PLGLAccts.GetIDFromNN(PCLaw.GLAccts.NickName);
                    PCLaw.GLAccts.ExternalID_1 = Table.Rows[nGLAccount]["NickName"].ToString().Trim();
                    PCLaw.GLAccts.ExternalID_2 = Table.Rows[nGLAccount]["GLAcctID"].ToString().Trim();

                    PCLaw.GLAccts.AddRecord();
                }//end if
            }//end for

            PCLaw.GLAccts.SendLast();
        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method

        public override string ToString()
        {
            return "GLAcct";
        }//end method
    }//end class
}//end namespace
