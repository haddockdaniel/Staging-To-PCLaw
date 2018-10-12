using System;
using System.Data;
using PLConvert;
using System.Windows.Forms;

namespace APIObjects
{
    /// <summary>
    /// Summary description for AMTimeFee.
    /// </summary>
    public class AMTimeEntry : StagingTable
    {
        public AMTimeEntry()
        {
        }

        public AMTimeEntry(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {

        }

        public override void AddRecords(bool includeClosedMatters)
        {
            if (PCLaw == null)
                return;

            if (includeClosedMatters)
                PLTimeEntry.m_bAllowEntOnClosedMtr = true;

            int nWIPFees = 0;
            int nDate = 0;

            DataTable Table = new DataTable("WIPFee");
            string sSelect = "SELECT * FROM [WIPFee]";
            ReadAMTable(ref Table, sSelect);


            for (nWIPFees = 0; nWIPFees < Table.Rows.Count; nWIPFees++)
            {
                try
                {
                    //string[] matter = Table.Rows[nI]["clientno"].ToString().Trim().Split(' ');
                    PCLaw.TimeEntry.MatterID = PLConvert.PLMatter.GetIDFromNN(Table.Rows[nWIPFees]["MatterID"].ToString().Trim());


                   // PCLaw.TimeEntry.LawyerID = PLConvert.PLLawyer.GetIDFromExtID2(Table.Rows[nWIPFees]["LawyerID"].ToString().Trim());
                    PCLaw.TimeEntry.LawyerID = PLConvert.PLLawyer.GetIDFromNN(Table.Rows[nWIPFees]["LawyerID"].ToString().Trim());
                    if (PCLaw.TimeEntry.LawyerID == 0)
                        PCLaw.TimeEntry.LawyerNN = "IT~";

                    nDate = int.Parse(Table.Rows[nWIPFees]["entrydate"].ToString().Trim());
                    
                    if (nDate < 19820101)
                        nDate = 19820101;
                    PCLaw.TimeEntry.Date = nDate;


                    double dur = Convert.ToDouble(Table.Rows[nWIPFees]["Duration"]);
                    PCLaw.TimeEntry.Hours = dur;
                    if (PCLaw.TimeEntry.Hours == 0.0000)
                        PCLaw.TimeEntry.EntryType = PLConvert.PLTimeEntry.eFeeEntryType.FEE_ENTRY;
                    else
                        PCLaw.TimeEntry.EntryType = PLConvert.PLTimeEntry.eFeeEntryType.TimeEnt;
                    
                    switch (int.Parse(Table.Rows[nWIPFees]["Status"].ToString().Trim()))
                    {
                        case 0: //Non Billable
                            PCLaw.TimeEntry.HoldFlag = PLTimeEntry.eFeeHoldFlag.NEVER_BILL;
                            break;
                        case 1: //Billable
                            PCLaw.TimeEntry.HoldFlag = PLTimeEntry.eFeeHoldFlag.NO_HOLD;
                            break;
                        case 2: //No Charge
                            PCLaw.TimeEntry.HoldFlag = PLTimeEntry.eFeeHoldFlag.SHOW_TEXT_ONLY;
                            break;
                        case 3: //Override (or additional charge)
                            PCLaw.TimeEntry.HoldFlag = PLTimeEntry.eFeeHoldFlag.NO_HOLD;
                            break;
                        case 4: //Hold
                            PCLaw.TimeEntry.HoldFlag = PLTimeEntry.eFeeHoldFlag.HOLD;
                            break;
                    }
                    
                    PCLaw.TimeEntry.Rate = double.Parse(Table.Rows[nWIPFees]["rate"].ToString().Trim());
                    PCLaw.TimeEntry.Amount = double.Parse(Table.Rows[nWIPFees]["totalbillable"].ToString().Trim());

                    //if (PCLaw.TimeEntry.HoldFlag == PLTimeEntry.eFeeHoldFlag.NEVER_BILL)
                   // PCLaw.TimeEntry.TaskID = PLConvert.PLTask.GetIDFromNN(Table.Rows[nWIPFees]["TaskCodeID"].ToString().Trim());
                   // if (PCLaw.TimeEntry.TaskID.Equals(0))
                      //  PCLaw.TimeEntry.TaskID = PLConvert.PLTask.GetIDFromExtID2(Table.Rows[nWIPFees]["TaskCodeID"].ToString().Trim());
                    if (PCLaw.TimeEntry.TaskID.Equals(0))
                    {
                        if (PCLaw.TimeEntry.Amount == 0)
                            PCLaw.TimeEntry.TaskID = PLConvert.PLTask.GetIDFromNN("NBW");
                        else
                            PCLaw.TimeEntry.TaskID = PLConvert.PLTask.GetIDFromNN("BW");
                    }
                    if (!string.IsNullOrEmpty(Table.Rows[nWIPFees]["TaskCodeID"].ToString().Trim()))
                    {
                        PCLaw.TimeEntry.ExplCodeID = PLConvert.PLExpCode.GetIDFromNN(Table.Rows[nWIPFees]["TaskCodeID"].ToString().Trim());
                        PCLaw.TimeEntry.Explanation = PLConvert.PLExpCode.GetExplanationFromID(PCLaw.TimeEntry.ExplCodeID) + " " + Table.Rows[nWIPFees]["Explanation"].ToString();
                    }
                  //  if (!string.IsNullOrEmpty(Table.Rows[nWIPFees]["Explanation"].ToString()))
                        
                    //else
                        //PCLaw.TimeEntry.Explanation = "Converted Time - No Explanation Given";

                    PCLaw.TimeEntry.AddRecord();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            PCLaw.TimeEntry.SendLast();
        }

        public override string ToString()
        {
            return "WIPFee";
        }

    }
}
