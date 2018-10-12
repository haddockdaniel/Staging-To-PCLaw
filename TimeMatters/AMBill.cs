using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLConvert;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace APIObjects
{
    public class AMBill : StagingTable
    {
        //List<InvoiceToMatter> invoiceMatter = new List<InvoiceToMatter>();
       // List<GLAcctNickToID> glToID = new List<GLAcctNickToID>();
        List<Invoice> invList = new List<Invoice>();

        public AMBill()
        {
        }

        public AMBill(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {

        }

        public override void AddRecords(bool importClosedMatters)
        {
            if (PCLaw == null)
                return;
            
            importBills(importClosedMatters);
            importWriteOffs(importClosedMatters); //includes write ups/downs
            importPayments(importClosedMatters);
          //  importGeneralRetainer(importClosedMatters);
        }

        //this step includes adding the invoices, allocations and time/expense entries for the invoices
        public void importBills(bool importClosedMatters)
        {
            DataTable TableBills = new DataTable("Bill");
            DataTable TableAllocations = new DataTable("Allocation");

            string sSelect = "";
            string allocSelect = "";

            int nInvID = 0;
            int nInvNum = 0;
            int nInvDate = 0;
           // bool usingAllocTable = true;
            
            
           // InvoiceToMatter itm = null;

            if (importClosedMatters)
            {
                PLBilling.m_bAllowEntOnClosedMtr = true;
                PLTimeEntry.m_bAllowEntOnClosedMtr = true;
                PLExpense.m_bAllowEntOnClosedMtr = true;
            }

            //glToID = getGLAcctIDs().ToList();

            sSelect = "SELECT * from Bill";
            ReadAMTable(ref TableBills, sSelect);

            allocSelect = "Select * from Allocation";
            ReadAMTable(ref TableAllocations, allocSelect);

            //add bills and allocations for each bill (add all allocations to the bill before moving to the next bill)
            for (int nBill = 0; nBill < TableBills.Rows.Count; nBill++)
            {
                double allocFees = 0;
                double allocDisb = 0;
                
                //if (Convert.ToInt32(rdr["void_date"]) > 0)
                   // continue; //voided invoice

                //handle interest only invoices?

                //skip invoice is there is no matter or invoice number
                if (!string.IsNullOrEmpty(TableBills.Rows[nBill]["MatterID"].ToString().Trim()) && !string.IsNullOrEmpty(TableBills.Rows[nBill]["BillNumber"].ToString().Trim()))
                {
                    int nDate = int.Parse(TableBills.Rows[nBill]["Date"].ToString().Trim());
                    if (nDate <= 19820101 || nDate >= 21991231)
                        nDate = 20061231;
                    PCLaw.Bill.MatterID = PLMatter.GetIDFromNN(TableBills.Rows[nBill]["MatterID"].ToString().Trim());
                    PCLaw.Bill.CollLawyerID = PLLawyer.GetIDFromNN(TableBills.Rows[nBill]["LawyerID"].ToString().Trim());
                    if (PCLaw.Bill.CollLawyerID == 0)
                        PCLaw.Bill.CollLawyerID = PLMatter.GetRespFromMattID(PCLaw.Bill.MatterID);
                    PCLaw.Bill.TypeOfLawID = PLMatter.GetTypeofLawFromMattID(PCLaw.Bill.MatterID);
                    PCLaw.Bill.InvoiceNumber = Convert.ToInt32(TableBills.Rows[nBill]["BillNumber"].ToString().Trim());
                    PCLaw.Bill.Date = nDate;
                    PCLaw.Bill.Fees = Math.Round(double.Parse(TableBills.Rows[nBill]["Fees"].ToString().Trim()), 2) + Math.Round(double.Parse(TableBills.Rows[nBill]["interest"].ToString().Trim()), 2); //what about interest?
                    PCLaw.Bill.Disbs = Math.Round(double.Parse(TableBills.Rows[nBill]["Disb"].ToString().Trim()), 2);
                    //to avoid problems with firms not using G/L accounting. Bug in PCLaw where Soft Costs are not 0
                    PCLaw.Bill.SoftCosts = 0;
                   // if (!PCLaw.GenInf.UsingGST)
                     //   PCLaw.Bill.PSTFees = Math.Round(double.Parse(TableBills.Rows[nBill]["Taxes"].ToString().Trim()), 2);
                   // else
                        PCLaw.Bill.GSTFees = Math.Round(double.Parse(TableBills.Rows[nBill]["Taxes"].ToString().Trim()), 2);

                    int nCollectID = PLMatter.GetRespFromMattID(PCLaw.Bill.MatterID); //used to create generic entries in case the allocations do not match up with the invoice amounts

                    //bill ready to go, now add allocations
                    for (int nAllocs = 0; nAllocs < TableAllocations.Rows.Count; nAllocs++)
                    {
                        //only add allocations associated with this current bill, otherwise, ignore this allocation
                        
                        if (TableBills.Rows[nBill]["oldID"].ToString().Trim().Equals(TableAllocations.Rows[nAllocs]["BillID"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (int.Parse(TableAllocations.Rows[nAllocs]["TimeExp"].ToString().Trim()) == 0) //time entry
                            {
                                //int nID = PLLawyer.GetIDFromNN(TableAllocations.Rows[nAllocs]["LawyerID"].ToString().Trim());
                               // if (nID.Equals(0))
                               // {
                              //      nID = PLLawyer.GetIDFromNN(TableAllocations.Rows[nAllocs]["LawyerID"].ToString().Trim());
                               // }
                               // if (nID.Equals(0))
                               //     PCLaw.Bill.Alloc.LawyerID = nCollectID;
                               // else
                                   // PCLaw.Bill.Alloc.LawyerID = nID;
                                PCLaw.Bill.Alloc.LawyerNN = PLLawyer.GetNNFromID(PLMatter.GetRespFromMattID(PCLaw.Bill.MatterID));
                                double dHours = Math.Round(Convert.ToDouble(TableAllocations.Rows[nAllocs]["QuantityHours"].ToString().Trim()), 2);
                                PCLaw.Bill.Alloc.Amount = Math.Round(Convert.ToDouble(TableAllocations.Rows[nAllocs]["Total"].ToString().Trim()), 2);
                                PCLaw.Bill.Alloc.Hours = dHours;
                                //PCLaw.Bill.Alloc.Seconds = Convert.ToInt32(dHours * Convert.ToDouble(3600));
                                PCLaw.Bill.Alloc.GLNN = "4000." + PCLaw.Bill.Alloc.LawyerNN;
                                //allocFees = allocFees + PCLaw.Bill.Alloc.Amount;
                                PCLaw.Bill.AddAllocation();
                                
                            }
                            else  //expense record
                                allocDisb = allocDisb + double.Parse(TableAllocations.Rows[nAllocs]["Total"].ToString().Trim());
                            //Interest charges must match - TODO
                         //   if (!Math.Round(double.Parse(TableBills.Rows[nBill]["Interest"].ToString().Trim()), 2).Equals(0.0))
                        //    {
                         //       PCLaw.Bill.Alloc.Amount = Math.Round(double.Parse(TableBills.Rows[nBill]["Interest"].ToString().Trim()), 2);
                         //       PCLaw.Bill.Alloc.Hours = 0.0;
                         //       PCLaw.Bill.Alloc.LawyerNN =
                         //           PLLawyer.GetNNFromID(PLMatter.GetRespFromMattID(PCLaw.Bill.MatterID));
                         //       PCLaw.Bill.Alloc.GLNN = "4000." + PCLaw.Bill.Alloc.LawyerNN;
                                //
                          //      PCLaw.Bill.AddAllocation();
                                //  allocFees = allocFees + PCLaw.Bill.Alloc.Amount;
                          //  }
                        }//end if
                    }//end allocs for



                    double feeDiff = 0.00;
                 //   feeDiff = Math.Round(double.Parse(TableBills.Rows[nBill]["Fees"].ToString().Trim()), 2) + Math.Round(double.Parse(TableBills.Rows[nBill]["Interest"].ToString().Trim()), 2)  - allocFees;
                    //handle mismatch of fees between invoice and sum if individual entries.
                    //need a more graceful way to handle this

                    //if (feeDiff != 0)
                   // {
                        //MessageBox.Show(TableBills.Rows[count]["oldID"].ToString().Trim() + " : " + feeDiff.ToString());
                     //   PCLaw.Bill.Alloc.Amount = feeDiff;
                     //   PCLaw.Bill.Alloc.Hours = 0;
                     //   PCLaw.Bill.Alloc.LawyerID = nCollectID;
                        //PCLaw.Bill.Alloc.GLNN = PCLaw.Bill.CollLawyerID;
                      //  PCLaw.Bill.AddAllocation();
                   // }
                    //handle mismatch of disb between invoice and sum if individual entries. - currently there is only one disb per bill so it will always match
                 //   double disbDiff = Math.Round(double.Parse(TableBills.Rows[nBill]["Disb"].ToString().Trim()), 2) - allocDisb;
                  //  if (disbDiff != 0)
                   //     disbDiff = PCLaw.Bill.Disbs - allocDisb;


                     

                    //add the bill
                    int matterID = PCLaw.Bill.MatterID;  //saved for use later on time and expense entries
                    Invoice i = new Invoice();
                    PCLaw.Bill.bAutoCreateCharges = false;
                    PCLaw.Bill.ExternalID_1 = TableBills.Rows[nBill]["OldID"].ToString().Trim();
                    PCLaw.Bill.ExternalID_2 = TableBills.Rows[nBill]["BillNumber"].ToString().Trim();

                    PCLaw.Bill.AddRecord(ref nInvID, ref nInvNum, ref nInvDate);
                    i.newID = nInvID;
                    i.number = nInvNum;
                    i.oldID = TableBills.Rows[nBill]["BillID"].ToString().Trim();
                    i.date = nInvDate;
                    invList.Add(i);

                    //allocs and bill done, now we add the time and expense entries associated with them
                    //bill ready to go, now add allocations
                    for (int allocs = 0; allocs < TableAllocations.Rows.Count; allocs++)
                    {
                        //only time/fee entries (allocations) associated with this current bill, otherwise, ignore this entry
                        if (TableBills.Rows[nBill]["BillID"].ToString().Trim().Equals(TableAllocations.Rows[allocs]["BillID"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (int.Parse(TableAllocations.Rows[allocs]["TimeExp"].ToString().Trim()) == 0) //time entry
                            {
                                PCLaw.TimeEntry.InvoiceID = nInvID;
                                PCLaw.TimeEntry.InvNumber = nInvNum;
                                PCLaw.TimeEntry.Date = int.Parse(TableAllocations.Rows[allocs]["EntryDate"].ToString().Trim());
                                PCLaw.TimeEntry.DateEntered = int.Parse(TableAllocations.Rows[allocs]["EntryDate"].ToString().Trim());

                                double dur = Convert.ToDouble(TableAllocations.Rows[allocs]["QuantityHours"].ToString().Trim());
                                PCLaw.TimeEntry.Hours = dur;

                                if (double.Parse(TableAllocations.Rows[allocs]["QuantityHours"].ToString().Trim()) == 0.0000)
                                    PCLaw.TimeEntry.EntryType = PLTimeEntry.eFeeEntryType.FEE_ENTRY;
                                else
                                    PCLaw.TimeEntry.EntryType = PLTimeEntry.eFeeEntryType.TimeEnt;

                                PCLaw.TimeEntry.MatterID = matterID;

                                int taskID = 0;
                                //if (!string.IsNullOrEmpty(TableAllocations.Rows[allocs]["CodeID"].ToString().Trim()))
                                //    taskID = PLTask.GetIDFromNN(TableAllocations.Rows[allocs]["CodeID"].ToString().Trim());
                               // if (taskID == 0)
                               // {
                                    taskID = PLTask.GetIDFromNN("BW");
                                   // if (taskID == 0)
                                   // {
                                  //      PCLaw.TimeEntry.TaskNN = "BW";
                                    ////    if (PCLaw.TimeEntry.TaskID.Equals(0))
                                     //   {
                                      //      PCLaw.Task.Name = "Billable Work";
                                      //      PCLaw.Task.NickName = "BW";
                                      //      PCLaw.Task.Category = PLTask.eCATEGORY.NON_BILLABLE;
                                       //     PCLaw.Task.AddRecord();
                                       //     PCLaw.Task.SendLast();
                                       //     PCLaw.TimeEntry.TaskNN = "BW";
                                       // }
                                    //}
                                        //}
                                //else
                                    PCLaw.TimeEntry.TaskID = taskID;

                                string descr = "";
                                if (!string.IsNullOrEmpty(TableAllocations.Rows[allocs]["Description"].ToString().Trim()))
                                    descr = TableAllocations.Rows[allocs]["Description"].ToString().Trim();
                                if (descr.Equals(""))
                                    descr = "Converted time entry, no explanaion provided";
                                PCLaw.TimeEntry.Explanation = descr;

                                int lawyerID = PLLawyer.GetIDFromNN(TableAllocations.Rows[allocs]["LawyerID"].ToString().Trim());
                                if (lawyerID.Equals(0))
                                    PCLaw.TimeEntry.LawyerNN = PLLawyer.GetNNFromID(PLMatter.GetRespFromMattID(matterID));
                                else
                                    PCLaw.TimeEntry.LawyerID = lawyerID;

                                if (double.Parse(TableAllocations.Rows[allocs]["Rate"].ToString().Trim()) == 0.0)
                                {
                                    try
                                    {
                                        if (Convert.ToDouble(TableAllocations.Rows[allocs]["Total"].ToString().Trim()) != 0 && dur != 0)
                                            PCLaw.TimeEntry.Rate = Math.Round(Convert.ToDouble(TableAllocations.Rows[allocs]["Total"].ToString().Trim()) / dur, 2);
                                    }
                                    catch (Exception ex1) 
                                    {
                                        PCLaw.TimeEntry.Rate = double.Parse(TableAllocations.Rows[allocs]["Rate"].ToString().Trim());
                                    }
                                }
                                else
                                    PCLaw.TimeEntry.Rate = double.Parse(TableAllocations.Rows[allocs]["Rate"].ToString().Trim());
                                PCLaw.TimeEntry.Amount = Convert.ToDouble(TableAllocations.Rows[allocs]["Total"].ToString().Trim());


                                PCLaw.TimeEntry.AddRecord();
                            }//end time entries
                            else  //expense entry
                            {
                                PCLaw.Expense.InvoiceID = nInvID;
                                PCLaw.Expense.InvoiceNumber = nInvNum;

                                PCLaw.Expense.Date = int.Parse(TableAllocations.Rows[allocs]["EntryDate"].ToString().Trim());

                                PCLaw.Expense.MatterID = matterID;

                                string descr = "";
                                if (!string.IsNullOrEmpty(TableAllocations.Rows[allocs]["Description"].ToString().Trim()))
                                    descr = TableAllocations.Rows[allocs]["Description"].ToString().Trim();
                                if (descr.Equals(""))
                                    descr = "Converted expense, no explanaion provided";

                                PCLaw.Expense.Explanation = descr;

                                int explID = 0;
                                if (!string.IsNullOrEmpty(TableAllocations.Rows[allocs]["CodeID"].ToString().Trim()))
                                    explID = PLExpCode.GetIDFromNN(TableAllocations.Rows[allocs]["CodeID"].ToString().Trim());
                                if (explID == 0)
                                    PCLaw.Expense.ExplCodeID = PLExpCode.GetIDFromNN("A111"); //default code "other"
                                else
                                    PCLaw.Expense.ExplCodeID = explID;

                                PCLaw.Expense.Quantity = Convert.ToDouble(TableAllocations.Rows[allocs]["QuantityHours"].ToString().Trim());
                                PCLaw.Expense.Rate = Convert.ToDouble(TableAllocations.Rows[allocs]["Rate"].ToString().Trim());
                                PCLaw.Expense.Amount = Convert.ToDouble(TableAllocations.Rows[allocs]["Total"].ToString().Trim());
                                PCLaw.Expense.IsSoftCost = false;

                                if (string.IsNullOrEmpty(TableAllocations.Rows[allocs]["ExpensePaidTo"].ToString().Trim()))
                                    PCLaw.Expense.PaidTo = "Converted - Billed Expenses";
                                else
                                    PCLaw.Expense.PaidTo = TableAllocations.Rows[allocs]["ExpensePaidTo"].ToString().Trim();
                                if (!string.IsNullOrEmpty(TableAllocations.Rows[allocs]["GLAcctID"].ToString().Trim()))
                                {
                                    int id = PLConvert.PLGLAccts.GetIDFromExtID2(TableAllocations.Rows[allocs]["GLAcctID"].ToString().Trim());
                                    if (id == 0)
                                        PCLaw.Expense.GLNN = "5210";
                                    else
                                        PCLaw.Expense.GLNN = PLConvert.PLGLAccts.GetNNFromID(id);
                                }
                                else
                                    PCLaw.Expense.GLNN = "5210";


                                PCLaw.Expense.AddRecord();
                            }//end expense entries (else)
                        }//end outer if
                    }//end time/expense for
                    
                    //if time and expense entries do not match bill, we add a generic entry to make them even
                    /*
                        if (feeDiff != 0.00)
                        {
                            PCLaw.TimeEntry.Amount = feeDiff;
                            feeDiff = 0.0;

                            PCLaw.TimeEntry.InvDate = nInvDate;
                            PCLaw.TimeEntry.InvoiceID = nInvID;
                            PCLaw.TimeEntry.InvNumber = nInvNum;
                            PCLaw.TimeEntry.Date = nInvDate;
                            PCLaw.TimeEntry.Seconds = 0;
                            PCLaw.TimeEntry.EntryType = PLTimeEntry.eFeeEntryType.FEE_ENTRY;
                            PCLaw.TimeEntry.MatterID = matterID;
                            PCLaw.TimeEntry.TaskNN = "BW";

                            if (PCLaw.TimeEntry.TaskID.Equals(0))
                            {
                                PCLaw.Task.Name = "Billable Work";
                                PCLaw.Task.NickName = "BW";
                                PCLaw.Task.Category = PLTask.eCATEGORY.NON_BILLABLE;
                                PCLaw.Task.AddRecord();
                                PCLaw.Task.SendLast();
                                PCLaw.TimeEntry.TaskNN = "BW";
                            }

                            PCLaw.TimeEntry.Explanation = "Time Offset - bill mismatch";
                            PCLaw.TimeEntry.LawyerID = nCollectID;

                            PCLaw.TimeEntry.AddRecord();
                        }

                        if (disbDiff != 0)
                        {
                            PCLaw.Expense.InvDate = nInvDate;
                            PCLaw.Expense.InvoiceID = nInvID;
                            PCLaw.Expense.InvoiceNumber = nInvNum;
                            PCLaw.Expense.Date = nInvDate;
                            PCLaw.Expense.MatterID = matterID;
                            PCLaw.Expense.IsSoftCost = false;
                            PCLaw.Expense.Explanation = "Conversion Repair";
                            PCLaw.Expense.Amount = disbDiff;
                            disbDiff = 0.0;
                            PCLaw.Expense.PaidTo = "Expense Offset - Billed Expenses";
                            PCLaw.Expense.GLNN = "5210";
                            PCLaw.Expense.AddRecord();
                        }

                   
                        if (!Math.Round(double.Parse(TableBills.Rows[nBill]["interest"].ToString().Trim()), 2).Equals(0.0))
                        {
                            PCLaw.TimeEntry.Amount =
                                Math.Round(double.Parse(TableBills.Rows[nBill]["interest"].ToString().Trim()), 2);

                            PCLaw.TimeEntry.InvDate = nInvDate;
                            PCLaw.TimeEntry.InvoiceID = nInvID;
                            PCLaw.TimeEntry.InvNumber = nInvNum;
                            PCLaw.TimeEntry.Date = nInvDate;
                            PCLaw.TimeEntry.Seconds = 0;
                            PCLaw.TimeEntry.EntryType = PLTimeEntry.eFeeEntryType.FEE_ENTRY;
                            PCLaw.TimeEntry.MatterID = matterID;
                            PCLaw.TimeEntry.TaskNN = "BW";

                            if (PCLaw.TimeEntry.TaskID.Equals(0))
                            {
                                PCLaw.Task.Name = "Billable Work";
                                PCLaw.Task.NickName = "BW";
                                PCLaw.Task.Category = PLTask.eCATEGORY.NON_BILLABLE;
                                PCLaw.Task.AddRecord();
                                PCLaw.Task.SendLast();
                                PCLaw.TimeEntry.TaskNN = "BW";
                            }

                            PCLaw.TimeEntry.Explanation = "Interest Charged on Invoice " + nInvNum.ToString();
                            PCLaw.TimeEntry.LawyerNN = PLLawyer.GetNNFromID(PLMatter.GetRespFromMattID(matterID));

                            PCLaw.TimeEntry.AddRecord();
                        }
                     
                    */
                }//end if
            }//end bills for


            PCLaw.TimeEntry.SendLast();
            PCLaw.Expense.SendLast();

        }

        public void importWriteOffs(bool importClosedMatters)
        {
            DataTable TableWUD = new DataTable("WUD");
            string sSelect = "";
            int nInvID = 0;


            sSelect = "SELECT * from WUD where amount <> 0.0000";
            ReadAMTable(ref TableWUD, sSelect);

            if (importClosedMatters)
            {
                PLBilling.m_bAllowEntOnClosedMtr = true;
                PLTimeEntry.m_bAllowEntOnClosedMtr = true;
                PLExpense.m_bAllowEntOnClosedMtr = true;
                PLGBEnt.m_bAllowEntOnClosedMtr = true;
            }

            for (int nWUD = 0; nWUD < TableWUD.Rows.Count; nWUD++)
            {
                foreach (Invoice i in invList)
                {
                    if (i.number == int.Parse(TableWUD.Rows[nWUD]["BillID"].ToString().Trim()))
                    {
                        nInvID = i.newID;
                        break;
                    }
                }

               // if (nInvID > 0)
               // {
                    if (!string.IsNullOrEmpty(TableWUD.Rows[nWUD]["Date"].ToString().Trim()))
                    {
                        int nDate = int.Parse(TableWUD.Rows[nWUD]["Date"].ToString().Trim());
                        if (nDate <= 19820101 || nDate >= 21991231)
                            nDate = 20061231;
                        PCLaw.WUD.Date = nDate; 
                    }
                    else
                        PCLaw.WUD.Date = 21991231; //assign arbitrary date

                    if (!string.IsNullOrEmpty(TableWUD.Rows[nWUD]["Explanation"].ToString().Trim()))
                        PCLaw.WUD.Explanation = TableWUD.Rows[nWUD]["Explanation"].ToString().Trim();
                    else
                        PCLaw.WUD.Explanation = "Write Off - No Explanation Given";


                    PCLaw.WUD.InvID = nInvID;
                    PCLaw.WUD.MatterID = PLMatter.GetIDFromNN(TableWUD.Rows[nWUD]["MatterID"].ToString().Trim());
                    //only use this type per the PCLaw dev folks...FEE_WO_ADJ is broken
                    PCLaw.WUD.Alloc.EntryType = PLWOAlloc.eWOAllocEntryType.FEE_WO_BADDEBT;

                    PCLaw.WUD.TaskNN = "WD";
                    if (double.Parse(TableWUD.Rows[nWUD]["Amount"].ToString().Trim()) > 0)
                        PCLaw.WUD.Alloc.Amount = double.Parse(TableWUD.Rows[nWUD]["Amount"].ToString().Trim()) * -1;
                    else
                        PCLaw.WUD.Alloc.Amount = double.Parse(TableWUD.Rows[nWUD]["Amount"].ToString().Trim());

                    PCLaw.WUD.Alloc.GLID = PLConvert.PLGLAccts.GetIDFromNN(TableWUD.Rows[nWUD]["GLAcct"].ToString().Trim());
                    PCLaw.WUD.Alloc.LawyerID = PLLawyer.GetIDFromNN(TableWUD.Rows[nWUD]["LawyerID"].ToString().Trim());

                    PCLaw.WUD.AddWOAllocation();

                    PCLaw.WUD.AddRecord();
                    PCLaw.WUD.SendLast();
              /*  }
                else
                {//write off is not associated with any invoice so we create a negative invoice (credit memo)
                    MessageBox.Show(TableWUD.Rows[nWUD]["BillID"].ToString().Trim() + " no invoicenum");
                    if (!string.IsNullOrEmpty(TableWUD.Rows[nWUD]["Date"].ToString().Trim()))
                    {
                        int nDate = int.Parse(TableWUD.Rows[nWUD]["Date"].ToString().Trim());
                        if (nDate <= 19820101 || nDate >= 21991231)
                            nDate = 20061231;
                        PCLaw.Bill.Date = nDate;
                    }
                    else
                        PCLaw.Bill.Date = 21991231; //assign arbitrary date

                    //we need to see if amount is negative. If it is, keep the value, if not, make it negative
                    if (double.Parse(TableWUD.Rows[nWUD]["Amount"].ToString().Trim()) < 0)
                        PCLaw.Bill.Fees = double.Parse(TableWUD.Rows[nWUD]["Amount"].ToString().Trim());
                    else
                        PCLaw.Bill.Fees = -double.Parse(TableWUD.Rows[nWUD]["Amount"].ToString().Trim());
                    PCLaw.Bill.InvoiceNumber = iInvMum;
                    iInvMum--;
                    iInvMatterID = PLMatter.GetIDFromExtID1(TableWUD.Rows[nWUD]["MatterID"].ToString().Trim());
                    PCLaw.Bill.MatterID = iInvMatterID;
                    PCLaw.Bill.CollLawyerID = PLMatter.GetRespFromMattID(iInvMatterID);
                    PCLaw.Bill.TypeOfLawID = PLMatter.GetTypeofLawFromMattID(iInvMatterID);
                    PCLaw.Bill.Alloc.Amount = PCLaw.Bill.Fees;
                    PCLaw.Bill.Alloc.LawyerID = PCLaw.Bill.CollLawyerID;
                    PCLaw.Bill.AddAllocation();
                    PCLaw.Bill.AddRecord(ref iInvID, ref iInvNum, ref iInvDate);


                    PCLaw.TimeEntry.Date = iInvDate;
                    PCLaw.TimeEntry.InvDate = iInvDate;
                    PCLaw.TimeEntry.InvNumber = iInvNum;
                    PCLaw.TimeEntry.InvoiceID = iInvID;
                    PCLaw.TimeEntry.DateEntered = iInvDate;
                    PCLaw.TimeEntry.Amount = PCLaw.Bill.Fees;
                    PCLaw.TimeEntry.EntryType = PLTimeEntry.eFeeEntryType.FEE_ENTRY;
                    PCLaw.TimeEntry.Explanation = "Credit: " + TableWUD.Rows[nWUD]["Explanation"].ToString().Trim();
                    PCLaw.TimeEntry.LawyerID = PLMatter.GetRespFromMattID(iInvMatterID);
                    PCLaw.TimeEntry.MatterID = iInvMatterID;
                    PCLaw.TimeEntry.TaskNN = "BW";
                    PCLaw.TimeEntry.AddRecord();
                    PCLaw.TimeEntry.SendLast();

                }
                    */

            }


        }

        public void importPayments(bool importClosedMatters)
        {
            if (importClosedMatters)
            {
                PLBilling.m_bAllowEntOnClosedMtr = true;
                PLTimeEntry.m_bAllowEntOnClosedMtr = true;
                PLExpense.m_bAllowEntOnClosedMtr = true;
                PLGBEnt.m_bAllowEntOnClosedMtr = true;
            }
            try
            {

                //payments must have an invoice number
                DataTable TablePayment = new DataTable("Payment");

                string sSelect = "SELECT * FROM Payment";
                ReadAMTable(ref TablePayment, sSelect);

                for (int nPayment = 0; nPayment < TablePayment.Rows.Count; nPayment++)
                {
                    //exID 1 for oldID and 2 for nickname
                    //int bankAcctID = PLGBAcct.GetIDFromExtID1(TablePayment.Rows[nPayment]["BankAccountID"].ToString().Trim());
                    // if (bankAcctID == 0)
                    // {
                    //   bankAcctID = PLGBAcct.GetIDFromExtID2(TablePayment.Rows[nPayment]["BankAccountID"].ToString().Trim());
                    //  if (bankAcctID == 0)
                    PCLaw.General.BankAcctID = 1;
                    //}
                    //else
                    //PCLaw.General.BankAcctID = bankAcctID;
                    PCLaw.General.CheckNum = TablePayment.Rows[nPayment]["TransactionID"].ToString().Trim();
                    PCLaw.General.ClientCheckNum = TablePayment.Rows[nPayment]["CheckNumber"].ToString().Trim(); //doesnt matter
                    PCLaw.General.Date = int.Parse(TablePayment.Rows[nPayment]["EntryDate"].ToString().Trim());
                    PCLaw.General.DateEntered = PCLaw.General.Date;

                    PCLaw.General.EntryGSTCat = TransactionData.eGST_CAT.NO;
                    PCLaw.General.EntryType = PLGBEnt.eGBEntryType.GEN_RCPT;

                    if (string.IsNullOrEmpty(TablePayment.Rows[nPayment]["PaidTo"].ToString().Trim()))
                        PCLaw.General.PaidTo = "Payment Applied";
                    else
                        PCLaw.General.PaidTo = TablePayment.Rows[nPayment]["PaidTo"].ToString().Trim();
                    PCLaw.General.PmtMethodFlag = PLGBEnt.ePmtMethod.Check;
                    //if (PCLaw.GenInf.UsingGST)
                    //PCLaw.General.GSTAmount = 0.00;

                    PCLaw.General.TotalAmount = Math.Abs(double.Parse(TablePayment.Rows[nPayment]["PaymentReceived"].ToString().Trim())); //payments must be a positive number

                    int InvID = 0;//oldid
                    int invNum = 0; //invoice number
                    int invDate = 0;


                    foreach (Invoice i in invList)
                    {
                        if (i.oldID.Equals(TablePayment.Rows[nPayment]["InvoiceID"].ToString().Trim()))
                        {
                            InvID = i.newID;
                            invNum = i.number;//invoice number
                            invDate = i.date;
                            break;
                        }
                    }
                    //   int InvID = PLBilling.GetInvIDfromExtID1(TablePayment.Rows[nPayment]["InvoiceID"].ToString().Trim());//oldid
                    //  int invNum = PLBilling.GetInvNumfromExtID2(TablePayment.Rows[nPayment]["InvoiceNum"].ToString().Trim()); //invoice number
                    //   int invDate = PLBilling.GetInvDatefromExtID2(TablePayment.Rows[nPayment]["InvoiceNum"].ToString().Trim());

                    //add the general allocation

                    PCLaw.General.Alloc.Amount = PCLaw.General.TotalAmount;
                    PCLaw.General.Alloc.EntryType = PLGBAlloc.eGBAllocEntryType.GEN_RCPT_PMNT; //actual payment

                    PCLaw.General.Alloc.MatterID = PLMatter.GetIDFromNN(TablePayment.Rows[nPayment]["MatterID"].ToString().Trim()); //oldid
                    // if (PCLaw.General.Alloc.MatterID == 0)
                    //   PCLaw.General.Alloc.MatterID = PLMatter.GetIDFromNN(TablePayment.Rows[nPayment]["MatterID"].ToString().Trim());
                    // if (PCLaw.General.Alloc.MatterID == 0)
                    //    PCLaw.General.Alloc.MatterID = PLBilling.GetMattIDfromInvID(InvID);

                    PCLaw.General.Alloc.Explanation = TablePayment.Rows[nPayment]["Explanation"].ToString().Trim();
                    if (string.IsNullOrEmpty(PCLaw.General.Alloc.Explanation))
                        PCLaw.General.Alloc.Explanation = "Payment for invoice " + invNum;

                    PCLaw.General.Alloc.GLID = 0;//PLGLAccts.GetIDFromExtID1(TablePayment.Rows[nPayment]["GLAcct"].ToString().Trim()); //nickname
                    // if (PCLaw.General.Alloc.GLID == 0)
                    //    PCLaw.General.Alloc.GLID = PLGLAccts.GetIDFromNN(TablePayment.Rows[nPayment]["GLAcct"].ToString().Trim());
                    PCLaw.General.Alloc.ExplCodeNN = "fd";
                    PCLaw.General.Alloc.InvID = InvID;
                    PCLaw.General.Alloc.InvDate = invDate;
                    PCLaw.General.Alloc.InvNumber = invNum;

                    // MessageBox.Show("MatterID: " + PLMatter.GetIDFromExtID1(TablePayment.Rows[nPayment]["MatterID"].ToString().Trim()).ToString()  +
                    //    " GLID: " + PLGLAccts.GetIDFromExtID1(TablePayment.Rows[nPayment]["GLAcct"].ToString().Trim()).ToString() +
                    //    " InvoiceID: " + InvID.ToString() + " InvNum: " + invNum.ToString() + " InvDate: " + invDate.ToString() + "\r\n" +
                    //    "MatterID: " + PCLaw.General.Alloc.MatterID.ToString() +
                    //     " GLID: " + PCLaw.General.Alloc.GLID.ToString());

                    //Add AR allocation
                    PCLaw.General.RcptARAlloc.Amount = PCLaw.General.TotalAmount;
                    PCLaw.General.RcptARAlloc.InvID = InvID;
                    PCLaw.General.RcptARAlloc.ARAllocType = PLGBARAlloc.eAllocType.PAYMENT;

                    PCLaw.General.AddGBAllocation();
                    PCLaw.General.AddRcptARAllocation();

                    PCLaw.General.AddRecord();

                }

                PCLaw.General.SendLast();
            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message);
            }
        }

        public void importGeneralRetainer(bool importClosedMatters)
        {
            if (importClosedMatters)
            {
                PLBilling.m_bAllowEntOnClosedMtr = true;
                PLTimeEntry.m_bAllowEntOnClosedMtr = true;
                PLExpense.m_bAllowEntOnClosedMtr = true;
                PLGBEnt.m_bAllowEntOnClosedMtr = true;
            }

            //gen retainer doesnt have an invoice number or when received > applied. The diff is added as a gen retainer
            DataTable TableGenRetainer = new DataTable("GenRetainer");

            string sSelect = "SELECT * FROM GenRetainer";
            ReadAMTable(ref TableGenRetainer, sSelect);

            for (int nGenRetainer = 0; nGenRetainer < TableGenRetainer.Rows.Count; nGenRetainer++)
            {
                if (string.IsNullOrEmpty(TableGenRetainer.Rows[nGenRetainer]["InvoiceID"].ToString().Trim())) //if there is no invoice, it is a general retainer
                {
                    //add General bank item
                    //exID 1 for oldID and 2 for nickname
                    int bankAcctID = PLGBAcct.GetIDFromExtID1(TableGenRetainer.Rows[nGenRetainer]["BankAcct"].ToString().Trim());
                    if (bankAcctID == 0)
                        PCLaw.General.BankAcctID = 1;
                    else
                        PCLaw.General.BankAcctID = bankAcctID;
                    PCLaw.General.CheckNum = TableGenRetainer.Rows[nGenRetainer]["checkno"].ToString().Trim();
                    PCLaw.General.ClientCheckNum = TableGenRetainer.Rows[nGenRetainer]["checkno"].ToString().Trim();
                    PCLaw.General.Date = int.Parse(TableGenRetainer.Rows[nGenRetainer]["Date"].ToString().Trim());
                    //PCLaw.General.DateEntered = (Int32)(PLDate) rdr["AddingDateTime"];
                    PCLaw.General.EntryGSTCat = TransactionData.eGST_CAT.NO;
                    PCLaw.General.EntryType = PLGBEnt.eGBEntryType.GEN_RCPT;
                    PCLaw.General.PaidTo = "Unapplied Gen Retainer";
                    PCLaw.General.PmtMethodFlag = PLGBEnt.ePmtMethod.Check;
                    PCLaw.General.TotalAmount = Math.Abs(double.Parse(TableGenRetainer.Rows[nGenRetainer]["Payment"].ToString().Trim()));

                    //if (PCLaw.GenInf.UsingGST)
                       // PCLaw.General.GSTAmount = 0.00;

                    //add retainer allocation
                    PCLaw.General.Alloc.Amount = Math.Abs(double.Parse(TableGenRetainer.Rows[nGenRetainer]["Payment"].ToString().Trim()));
                    PCLaw.General.Alloc.EntryType = PLGBAlloc.eGBAllocEntryType.GEN_RCPT_RTNR;

                    PCLaw.General.Alloc.Explanation = TableGenRetainer.Rows[nGenRetainer]["Explanation"].ToString().Trim();
                    if (PCLaw.General.Alloc.Explanation == "")
                        PCLaw.General.Alloc.Explanation = "Conversion - Unapplied GenRet";
                    
                    PCLaw.General.Alloc.GLID = PLGLAccts.GetExpRecovID();
                    PCLaw.General.Alloc.MatterID = PLMatter.GetIDFromExtID2(TableGenRetainer.Rows[nGenRetainer]["MatterID"].ToString().Trim());
                    PCLaw.General.AddGBAllocation();

                    PCLaw.General.AddRecord();
                    PCLaw.General.SendLast();

                }

                //if it has an invoice ID but the amount received is greater than the amount applied, the difference is added as a general retainer
                else
                {
                    double difference = Math.Abs(double.Parse(TableGenRetainer.Rows[nGenRetainer]["Payment"].ToString().Trim())) - Math.Abs(double.Parse(TableGenRetainer.Rows[nGenRetainer]["PaymentApplied"].ToString().Trim()));
                    if (difference != 0)
                    {
                        //exID 1 for oldID and 2 for nickname
                        int bankAcctID = PLGBAcct.GetIDFromExtID1(TableGenRetainer.Rows[nGenRetainer]["BankAcct"].ToString().Trim());
                        if (bankAcctID == 0)
                            PCLaw.General.BankAcctID = 1;
                        else
                            PCLaw.General.BankAcctID = bankAcctID;
                        //PCLaw.General.CheckNum = StringManip.NullToString(rdr["RefNo"]);
                        //PCLaw.General.ClientCheckNum = StringManip.NullToString(rdr["RefNo"]);
                        PCLaw.General.Date = int.Parse(TableGenRetainer.Rows[nGenRetainer]["Date"].ToString().Trim());
                        //PCLaw.General.DateEntered = (Int32)(PLDate) rdr["AddingDateTime"];
                        PCLaw.General.EntryGSTCat = TransactionData.eGST_CAT.NO;
                        PCLaw.General.EntryType = PLGBEnt.eGBEntryType.GEN_RCPT;
                        PCLaw.General.PaidTo = "Unapplied Gen Retainer";
                        PCLaw.General.PmtMethodFlag = PLGBEnt.ePmtMethod.Check;
                        PCLaw.General.TotalAmount = Math.Abs(double.Parse(TableGenRetainer.Rows[nGenRetainer]["Received"].ToString().Trim()));

                        if (PCLaw.GenInf.UsingGST)
                            PCLaw.General.GSTAmount = 0.00;

                        PCLaw.General.Alloc.Amount = Math.Abs(difference);
                        PCLaw.General.Alloc.EntryType = PLGBAlloc.eGBAllocEntryType.GEN_RCPT_RTNR;

                        if (PCLaw.General.Alloc.Explanation == "")
                            PCLaw.General.Alloc.Explanation = "Conversion - Unapplied GenRet with Inv";

                        PCLaw.General.Alloc.GLID = PLGLAccts.GetExpRecovID();
                        PCLaw.General.Alloc.MatterID = PLMatter.GetIDFromExtID2(TableGenRetainer.Rows[nGenRetainer]["MatterID"].ToString().Trim());
                        PCLaw.General.AddGBAllocation();

                        PCLaw.General.AddRecord();
                        PCLaw.General.SendLast();
                    }
                }

            }
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
            return "Bills";
        }


    }
}
