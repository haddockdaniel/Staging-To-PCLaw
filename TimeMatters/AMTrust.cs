using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PLConvert;
using System.Windows.Forms;

namespace APIObjects
{
    public class AMTrust : StagingTable
    {
        public AMTrust()
            : base()
        {
        }

        public AMTrust(PLConvert.PCLawConversion PL)
            : base()
        {
            PCLaw = PL;
        }

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            DataTable Table = new DataTable();

        }

        public override void AddRecords(bool importClosedMatters)
        {
            DataTable TableTrust = new DataTable("Trust");

            string sSelect = "SELECT * from Trust";
            ReadAMTable(ref TableTrust, sSelect);

            if (importClosedMatters)
                PLTBEnt.m_bAllowEntOnClosedMtr = true;

            for (int nTrust = 0; nTrust < TableTrust.Rows.Count; nTrust++)
            {
                double total = double.Parse(TableTrust.Rows[nTrust]["Amount"].ToString().Trim());
                int invalidCheckNumberOffset = 99999;
                switch (TableTrust.Rows[nTrust]["EntryType"].ToString().Trim())
                {
                    case "0":
                        PCLaw.Trust.EntryType = PLTBEnt.eTBEntryType.Recipt; //type 2050 - usually positive
                        PCLaw.Trust.TotalAmount = total;
                        break;
                    case "1":
                        PCLaw.Trust.EntryType = PLTBEnt.eTBEntryType.Check; //type 2049 - usualy positive
                        PCLaw.Trust.TotalAmount = total;
                        break;
                    case "2":
                        PCLaw.Trust.EntryType = PLTBEnt.eTBEntryType.OpeningBal; //type 2051 - usually positive
                        PCLaw.Trust.TotalAmount = total;
                        break;
                    case "3":
                        PCLaw.Trust.EntryType = PLTBEnt.eTBEntryType.TrustToTrustTransfer; //type 2052 - offsetting positive and negative
                        PCLaw.Trust.TotalAmount = total;
                        break;
                    case "4":
                        PCLaw.Trust.EntryType = PLTBEnt.eTBEntryType.TRUST_TDT; //type 2053 - usually positive
                        PCLaw.Trust.TotalAmount = total;
                        break;
                }

                PCLaw.Trust.Date = int.Parse(TableTrust.Rows[nTrust]["Date"].ToString().Trim());
                PCLaw.Trust.DateEntered = PCLaw.Trust.Date;
                //PCLaw.Trust.BankAcctID = PLTBAcct.GetIDFromExtID1(TableTrust.Rows[nTrust]["GLAcctID"].ToString().Trim());

                //if (PCLaw.Trust.BankAcctID == 0)
                //{
                    PCLaw.Trust.BankAcctID = 1;
                //}

                PCLaw.Trust.CheckNum = TableTrust.Rows[nTrust]["CheckNum"].ToString().Trim();
                if (PCLaw.Trust.CheckNum.Equals("0") || string.IsNullOrEmpty(PCLaw.Trust.CheckNum)) //we need a valid check number and we give a generic one if there isnt one in the source
                {
                    PCLaw.Trust.CheckNum = invalidCheckNumberOffset.ToString();
                    invalidCheckNumberOffset--;
                }
                if (!string.IsNullOrEmpty(TableTrust.Rows[nTrust]["PaidTo"].ToString().Trim()))
                    PCLaw.Trust.PaidTo = TableTrust.Rows[nTrust]["PaidTo"].ToString().Trim();
                else
                    PCLaw.Trust.PaidTo = "Converted - No explanation given";
                PCLaw.Trust.PmtMethodFlag = PLTBEnt.ePmtMethod.Check;
                PCLaw.Trust.ClientCheckNum = "";

                //Alloc
                PCLaw.Trust.Alloc.MatterID = PLConvert.PLMatter.GetIDFromNN(TableTrust.Rows[nTrust]["MatterID"].ToString().Trim());
                PCLaw.Trust.Alloc.Amount = total;
                if (!string.IsNullOrEmpty(TableTrust.Rows[nTrust]["Explanation"].ToString().Trim()))
                    PCLaw.Trust.Alloc.Explanation = TableTrust.Rows[nTrust]["Explanation"].ToString().Trim();
                else
                    PCLaw.Trust.Alloc.Explanation = "Converted Trust Transaction - No Explanation Given";
                PCLaw.Trust.AddAllocation();
                //dTotTrust += Convert.ToDecimal(rdr["TransValue"]);
                PCLaw.Trust.AddRecord();


            }

            PCLaw.Trust.SendLast();

            //dTrustDifference = dTotCalculatedTrust - dTotTrust;

        }

        static public int GetDate(int ClarionDate)
        {
            DateTime DateDec1800 = new DateTime(1800, 12, 28);
            DateTime PLDate = DateDec1800.AddDays(ClarionDate);
            int nDate = PLDate.Year * 10000 + PLDate.Month * 100 + PLDate.Day;
            if (nDate < 19820101 || nDate > 21991231)
                nDate = 21991231;

            return nDate;
        }

        public override string ToString()
        {
            return "Trust";
        }

    }
}
