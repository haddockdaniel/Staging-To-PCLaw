using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

namespace APIObjects
{
    public class AMLawyer : StagingTable
    {
        public AMLawyer()
        {
        }//end constructor

        public AMLawyer(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;

            DataTable Table = new DataTable("Lawyer");
            string sSelect = "SELECT * FROM lawyer";
            ReadAMTable(ref Table, sSelect);

            //ensure default lawyer is added
          //  if (PLConvert.PLLawyer.GetIDFromNN("IT~") == 0)
           // {
                PCLaw.Lawyer.Name = "Inactive Timekeepers";
                PCLaw.Lawyer.NickName = "IT~";
                PCLaw.Lawyer.Initials = "IT";
                PCLaw.Lawyer.DepartmentID = 1;
                PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.SeniorPartner;
                PCLaw.Lawyer.DiaryActive = true;
                PCLaw.Lawyer.Status = PLConvert.PLXMLData.eSTATUS.ARCHIVED;
                PCLaw.Lawyer.AddRecord();
           // }//end if


            for (int nLawyer = 0; nLawyer < Table.Rows.Count; nLawyer++)
            {
                //add lawyers
                PCLaw.Lawyer.Name = Table.Rows[nLawyer]["name"].ToString().Trim();
                PCLaw.Lawyer.NickName = Table.Rows[nLawyer]["nickname"].ToString().Trim();
                PCLaw.Lawyer.Initials = Table.Rows[nLawyer]["initials"].ToString().Trim();
                //PCLaw.Lawyer.DepartmentID = PLConvert.PLDepartment.GetIDFromNN(Table.Rows[nLawyer]["Department"].ToString().Trim());
                //if (PCLaw.Lawyer.DepartmentID == 0)
                    PCLaw.Lawyer.DepartmentID = 1;

                //tells pclaw what type of job they have
                switch (int.Parse(Table.Rows[nLawyer]["classification"].ToString().Trim()))
                {
                    case 1:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.SeniorPartner;
                        PCLaw.Lawyer.IsPartner = true;
                        break;
                    case 2:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.JrPartner;
                        PCLaw.Lawyer.IsPartner = true;
                        break;
                    case 3:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.Associate;
                        break;
                    case 4:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.LawClerk;
                        break;
                    case 5:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.Paralegal;
                        break;
                    case 6:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.TimeKeeper;
                        break;
                    default:
                        PCLaw.Lawyer.Classification = PLConvert.PLLawyer.LawyerClassification.Associate;
                        break;
                }//end switch

                if (!string.IsNullOrEmpty(Table.Rows[nLawyer]["DiaryActive"].ToString().Trim()))
                    PCLaw.Lawyer.DiaryActive = bool.Parse(Table.Rows[nLawyer]["DiaryActive"].ToString().Trim());
                else
                    PCLaw.Lawyer.DiaryActive = true;
                if (bool.Parse(Table.Rows[nLawyer]["Active"].ToString().Trim()))
                    PCLaw.Lawyer.Status = PLConvert.PLXMLData.eSTATUS.ACTIVE;
                else
                    PCLaw.Lawyer.Status = PLConvert.PLXMLData.eSTATUS.ARCHIVED;
                
                //used for referring back to them by these IDs to relate them to other tables like matters or appointments
                PCLaw.Lawyer.ExternalID_2 = Table.Rows[nLawyer]["LawyerID"].ToString().Trim();
                PCLaw.Lawyer.ExternalID_1 = Table.Rows[nLawyer]["nickname"].ToString().Trim();

                //Add rates to the lawyers from the lawyerRateTable
                DateTime Date = DateTime.Today;
                int nDate = 0;
                if (Date.Year > 0 && Date.Month > 0 && Date.Day > 0)
                    nDate = Date.Year * 10000 + Date.Month * 100 + Date.Day;
                else
                    nDate = 21991231;
                //since rates start at number one, the Z char is a placeholder and is NOT used. They are named "rate 1, rate 2, etc
                char[] array = {'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',};
                for (int i = 1; i < 11; i++)
                {
                    string rateName = "rate" + i.ToString();
                    double rate = Convert.ToDouble(Table.Rows[nLawyer][rateName].ToString().Trim());
                    int nID = PLConvert.PLRate.GetIDFromNN(array[i].ToString());
                    if (nID != 0)
                        PCLaw.Lawyer.AddRate(nID, rate, nDate);
                    else
                        MessageBox.Show("Lawyer Error: " + PCLaw.Lawyer.Initials);
                }//end inner for
                PCLaw.Lawyer.AddRecord();
            }//end outer for
                    
            PCLaw.Lawyer.SendLast();

        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used

        }//end method

 


        public override string ToString()
        {
            return "Lawyer";
        }//end method



    }//end class
}//end namespace
