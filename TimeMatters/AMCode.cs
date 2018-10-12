using System;
using System.Collections.Generic;
using System.Data;
using PLConvert;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace APIObjects
{
    public class AMCode : StagingTable
    {
        public AMCode()
        {
        }//end constructor

        public AMCode(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;
            //i seperated these into different methods so it wouldnt be one giant mass of code
            importExplanationCodes();
            importTaskCodes();
           // importLocationCodes();
           // importDepartmentCodes();
        }//end method

        //explanation and activity codes
        public void importExplanationCodes()
        {
            List<string> explanCodes = new List<string>();
            while (PCLaw.ExpCode.GetNextRecord() == 0) //make sure they dont already exist
            {
                explanCodes.Add(PCLaw.ExpCode.NickName.Trim());
            }

            DataTable Table = new DataTable("Explanation");
            string sSelect = "";


                sSelect = "SELECT * FROM [ExplanationCode]";
                ReadAMTable(ref Table, sSelect);

                //expense codes (D with a glacct)
                for (int nExplanationCode = 0; nExplanationCode < Table.Rows.Count; nExplanationCode++)
                {
                    bool existing = false;
                    if (string.IsNullOrEmpty(Table.Rows[nExplanationCode]["Name"].ToString().Trim()))  //skip any items that have no name
                        continue;
                    foreach (string expl in explanCodes)
                    {
                        if (expl.Equals(Table.Rows[nExplanationCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", ""), StringComparison.OrdinalIgnoreCase))
                        {
                            existing = true;
                            break;
                        }//end if
                    }//end foreach
                    if (existing) //if a code with that nickname already exists, do not add it again
                        continue;
                    try
                    {
                        PCLaw.ExpCode.Name = Table.Rows[nExplanationCode]["Name"].ToString().Trim();
                       // string output = PCLaw.ExpCode.MakeNN(true);
                        //MessageBox.Show("show " + output);
                        PCLaw.ExpCode.NickName = Table.Rows[nExplanationCode]["NickName"].ToString().Trim().ToLower().Replace("~", "").Replace("*", "");
                        PCLaw.ExpCode.RateAmount = double.Parse(Table.Rows[nExplanationCode]["RateAmount"].ToString().Trim());
                    }
                    catch (Exception ex1)
                    {
                        MessageBox.Show("N " + ex1.Message);
                    }
                    int explType = 0;
                    explType = int.Parse(Table.Rows[nExplanationCode]["ExplType"].ToString().Trim());

                    switch (explType)
                    {

                        case 1: //Expense
                            //if there is no glacct or it is somehow messed up, assign the generic misc expense gl code already in PCLaw

                                if (string.IsNullOrEmpty(Table.Rows[nExplanationCode]["GLAcctID"].ToString().Trim()))
                                    PCLaw.ExpCode.GLNN = "5010";
                                else
                                {
                                    int glacct = PLConvert.PLGLAccts.GetIDFromExtID1(Table.Rows[nExplanationCode]["GLAcctID"].ToString().Trim()); //use nickname
                                    if (glacct != 0)
                                        PCLaw.ExpCode.GLNN = PLConvert.PLGLAccts.GetNNFromID(glacct);
                                    else
                                    {
                                        if (PLConvert.PLGLAccts.GetIDFromExtID2(Table.Rows[nExplanationCode]["GLAcctID"].ToString().Trim()) != 0)//use oldid
                                            PCLaw.ExpCode.GLNN = Table.Rows[nExplanationCode]["GLAcctID"].ToString().Trim();
                                        else
                                            PCLaw.ExpCode.GLNN = "5010";
                                    }
                                }//end else

                                PCLaw.ExpCode.ExplType = PLConvert.PLExpCode.eEXPL_TYPE.EXPENSE;
                                PCLaw.ExpCode.Summarize = PLConvert.PLExpCode.eSUMMARIZE.DO_NOT_SUMMARIZE;
                                PCLaw.ExpCode.ExternalID_1 = Table.Rows[nExplanationCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", "");
                                PCLaw.ExpCode.ExternalID_2 = Table.Rows[nExplanationCode]["ExplanationCodeID"].ToString().Trim();

                            break;
                        case 2: //Time
                            PCLaw.ExpCode.ExplType = PLConvert.PLExpCode.eEXPL_TYPE.TIME;
                            PCLaw.ExpCode.Summarize = PLConvert.PLExpCode.eSUMMARIZE.DO_NOT_SUMMARIZE;
                            PCLaw.ExpCode.ExternalID_1 = Table.Rows[nExplanationCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", "");
                            PCLaw.ExpCode.ExternalID_2 = Table.Rows[nExplanationCode]["ExplanationCodeID"].ToString().Trim();

                            break;
                        case 3: //All (default)
                    
                            PCLaw.ExpCode.ExplType = PLConvert.PLExpCode.eEXPL_TYPE.ALL;
                            PCLaw.ExpCode.Summarize = PLConvert.PLExpCode.eSUMMARIZE.DO_NOT_SUMMARIZE;
                            PCLaw.ExpCode.ExternalID_1 = Table.Rows[nExplanationCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", "");
                            PCLaw.ExpCode.ExternalID_2 = Table.Rows[nExplanationCode]["ExplanationCodeID"].ToString().Trim();

                            break;
                    }//end switch
                    PCLaw.ExpCode.AddRecord();
                }//end for

                PCLaw.ExpCode.SendLast();

        }//end method
        
        public void importTaskCodes()
        {
            DataTable Table = new DataTable("Task");
            string sSelect = "";

            sSelect = "SELECT * FROM [TaskCode]";
            ReadAMTable(ref Table, sSelect);

            List<string> taskCodes = new List<string>();


            while (PCLaw.Task.GetNextRecord() == 0) //make sure they dont already exist
                taskCodes.Add(PCLaw.Task.NickName.Trim());

            for (int nTaskCode = 0; nTaskCode < Table.Rows.Count; nTaskCode++)
            {
                bool existing = false;
                if (string.IsNullOrEmpty(Table.Rows[nTaskCode]["Name"].ToString().Trim()))  //skip any items that have no description
                    continue;
                foreach (string task in taskCodes)
                {
                    if (task.Equals(Table.Rows[nTaskCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        existing = true;
                        break;
                    }//end if
                }//end foreach
                if (existing) //if a task code with the same name already exists in PCLaw, do not add it again
                    continue;
                PCLaw.Task.Name = Table.Rows[nTaskCode]["Name"].ToString().Trim();
                //PCLaw.Task.MakeNN(false);
                PCLaw.Task.NickName = Table.Rows[nTaskCode]["Nickname"].ToString().Trim().ToLower().Replace("~", "").Replace("*", "");

                PCLaw.Task.ExternalID_1 = Table.Rows[nTaskCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", "");
                PCLaw.Task.ExternalID_2 = Table.Rows[nTaskCode]["TaskCodeID"].ToString().Trim();

                //add type of law (this is required which is why, if we cant find one, we assign it to 'misc'
                if (!string.IsNullOrEmpty(Table.Rows[nTaskCode]["TypeOfLawID"].ToString().Trim()))
                {
                    int lawid = PLConvert.PLTypeOfLaw.GetIDFromExtID2(Table.Rows[nTaskCode]["TypeOfLawID"].ToString().Trim());
                    if (lawid != 0)
                    {
                        int nn = PLConvert.PLTypeOfLaw.GetIDFromNN(Table.Rows[nTaskCode]["TypeOfLawID"].ToString().Trim());
                        if (nn != 0 )
                            PCLaw.Task.TypeOfLawNN = Table.Rows[nTaskCode]["TypeOfLawID"].ToString().Trim();
                        else
                            PCLaw.Task.TypeOfLawNN = "misc";
                    }
                }
                else
                    PCLaw.Task.TypeOfLawNN = "misc";
                switch (Table.Rows[nTaskCode]["Category"].ToString().Trim())
                {
                    case "1":
                        PCLaw.Task.Category = PLConvert.PLTask.eCATEGORY.BILLABLE;
                        break;
                    case "2":
                        PCLaw.Task.Category = PLConvert.PLTask.eCATEGORY.NON_BILLABLE;
                        break;
                    case "3":
                        PCLaw.Task.Category = PLConvert.PLTask.eCATEGORY.WRITE_UP_DOWN;
                        break;
                    default:
                        PCLaw.Task.Category = PLConvert.PLTask.eCATEGORY.BILLABLE;
                        break;
                }//end switch
                PCLaw.Task.AddRecord();
            }//end for
            PCLaw.Task.SendLast();
        }//end method

        public void importLocationCodes() //added but as of now, they are not used to my knowledge
        {
            DataTable Table = new DataTable("Location");
            string sSelect = "";

            sSelect = "SELECT * from [LocationCode]";
            ReadAMTable(ref Table, sSelect);

            List<string> locationCodes = new List<string>();

            while (PCLaw.Location.GetNextRecord() == 0)//make sure they dont already exist
                locationCodes.Add(PCLaw.Location.NickName.Trim());

            for (int nLocationCode = 0; nLocationCode < Table.Rows.Count; nLocationCode++)
            {
                bool existing = false;
                if (string.IsNullOrEmpty(Table.Rows[nLocationCode]["Name"].ToString().Trim()))  //skip any items that have no description
                    continue;
                foreach (string location in locationCodes)
                {
                    if (location.Equals(Table.Rows[nLocationCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        existing = true;
                        break;
                    }//end if
                }//end foreach
                if (existing) //if a location code with the same name already exists in PCLaw, do not add it again
                    continue;
                PCLaw.Location.Name = Table.Rows[nLocationCode]["Name"].ToString().Trim();
                //PCLaw.Location.MakeNN(false);
                PCLaw.Location.NickName = Table.Rows[nLocationCode]["NickName"].ToString().Trim().ToLower().Replace("~", "").Replace("*", "");
                PCLaw.Location.ExternalID_1 = Table.Rows[nLocationCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", "");
                PCLaw.Location.ExternalID_2 = Table.Rows[nLocationCode]["OldID"].ToString().Trim();

                PCLaw.Location.AddRecord();
            }//end for
            PCLaw.Location.SendLast();
        }//end method

        public void importDepartmentCodes()
        {
            DataTable Table = new DataTable("Department");
            string sSelect = "";

            sSelect = "SELECT * FROM [DepartmentCode]";
            ReadAMTable(ref Table, sSelect);


            for (int nDeptCode = 0; nDeptCode < Table.Rows.Count; nDeptCode++)
            {
                if (string.IsNullOrEmpty(Table.Rows[nDeptCode]["Name"].ToString().Trim()))  //skip any items that have no description
                    continue;
                PCLaw.Department.Name = Table.Rows[nDeptCode]["Name"].ToString().Trim();
                //PCLaw.Task.MakeNN(false);
                PCLaw.Department.NickName = Table.Rows[nDeptCode]["Nickname"].ToString().Trim().ToLower().Replace("~", "").Replace("*", "");

                PCLaw.Department.ExternalID_1 = Table.Rows[nDeptCode]["NickName"].ToString().Trim().Replace("~", "").Replace("*", "");
                PCLaw.Department.ExternalID_2 = Table.Rows[nDeptCode]["OldID"].ToString().Trim();

                PCLaw.Department.AddRecord();
            }//end for
            PCLaw.Department.SendLast();

        }

        public override void AddRecords(PLConvert.PCLawConversion PCLaw, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method

        public override string ToString()
        {
            return "Code";
        }//end method
    }//end class
}//end namespace
