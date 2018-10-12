using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLConvert;
using System.Data;
using System.Collections;

namespace APIObjects
{
    public class AMExpense : StagingTable
    {
        public AMExpense()
        {
        }

        public AMExpense(PLConvert.PCLawConversion PL)
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

            DataTable Table = new DataTable("WIPExpense");
            string sSelect;
            int nWIPExpenses = 0;
            int nDate = 0;


            if (includeClosedMatters)
                PLExpense.m_bAllowEntOnClosedMtr = true;

            sSelect = "SELECT * FROM [WIPExpense]";
            ReadAMTable(ref Table, sSelect);

            for (nWIPExpenses = 0; nWIPExpenses < Table.Rows.Count; nWIPExpenses++)
            {
                nDate = int.Parse(Table.Rows[nWIPExpenses]["entrydate"].ToString().Trim());
                if (nDate < 19820101)
                    nDate = 19820101;

                PCLaw.Expense.Date = nDate;
                PCLaw.Expense.IsSoftCost = false;
                if (!string.IsNullOrEmpty(Table.Rows[nWIPExpenses]["ExpensePaidTo"].ToString().Trim()))
                    PCLaw.Expense.PaidTo = Table.Rows[nWIPExpenses]["ExpensePaidTo"].ToString().Trim();

                PCLaw.Expense.MatterID = PLConvert.PLMatter.GetIDFromNN(Table.Rows[nWIPExpenses]["matterid"].ToString().Trim());//old sysid

                PCLaw.Expense.CheckNum = Table.Rows[nWIPExpenses]["CheckNo"].ToString().Trim();

                switch (Table.Rows[nWIPExpenses]["ExpenseType"].ToString().Trim())
                {
                    case "B":
                        PCLaw.Expense.GSTCat = TransactionData.eGST_CAT.BOTH;
                        break;
                    case "Z":
                        PCLaw.Expense.GSTCat = TransactionData.eGST_CAT.ZERO;
                        break;
                    case "E":
                        PCLaw.Expense.GSTCat = TransactionData.eGST_CAT.EXEMPT;
                        break;
                    default:
                        PCLaw.Expense.GSTCat = TransactionData.eGST_CAT.BOTH;
                        break;
                }

                if (string.IsNullOrEmpty(Table.Rows[nWIPExpenses]["Explanation"].ToString().Trim()))
                    PCLaw.Expense.Explanation = "Converted expense, no detail provided";
                else
                    PCLaw.Expense.Explanation = Table.Rows[nWIPExpenses]["Explanation"].ToString().Trim();

                int nID = PLExpCode.GetIDFromExtID2(Table.Rows[nWIPExpenses]["ExplanationCodeID"].ToString().Trim());//old id

                if (nID != 0)
                    PCLaw.Expense.ExplCodeID = nID;
                else
                {
                    nID = PLExpCode.GetIDFromNN(Table.Rows[nWIPExpenses]["ExplanationCodeID"].ToString().Trim());//old nn
                    if (nID == 0)
                        PCLaw.Expense.ExplCodeID = PLConvert.PLExpCode.GetIDFromNN("06");//misc explanation code (default)
                    else
                        PCLaw.Expense.ExplCodeID = nID;
                }


                PCLaw.Expense.Quantity = Convert.ToDouble(Table.Rows[nWIPExpenses]["Quantity"].ToString().Trim());
                PCLaw.Expense.Rate = Convert.ToDouble(Table.Rows[nWIPExpenses]["Rate"].ToString().Trim());
                PCLaw.Expense.Amount = Convert.ToDouble(Table.Rows[nWIPExpenses]["TotalExpensed"].ToString().Trim());

                PCLaw.Expense.PaidTo = "Conversion - Expenses";
                if (string.IsNullOrEmpty(Table.Rows[nWIPExpenses]["GLAcctID"].ToString().Trim()))
                    PCLaw.Expense.GLNN = "5210"; //default misc account
                else
                {
                    //based off nickname
                    int glID = PLConvert.PLGLAccts.GetIDFromNN(Table.Rows[nWIPExpenses]["GLAcctID"].ToString().Trim());
                    if (glID == 0) //no match
                        PCLaw.Expense.GLNN = "5210"; //default misc account
                    else
                        PCLaw.Expense.GLNN = PLConvert.PLGLAccts.GetNNFromID(glID);
                }

                PCLaw.Expense.AddRecord();
            }
            PCLaw.Expense.SendLast();
        }

        public override string ToString()
        {
            return "WIPExpense";
        }

    }
}
