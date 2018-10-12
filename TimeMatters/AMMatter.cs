using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using PLConvert;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace APIObjects
{
    public class AMMatter : StagingTable
    {
        public AMMatter()
        {
        }//end constructor

        public AMMatter(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            //not used
        }//end constructor

        public void AddRecords(bool bPCLawMoreUptoDate, bool combineCliMatt, bool includeClosedMatters)
        {
            if (PCLaw == null)
                return;

            DataTable Table;
            string sSelect;
            int nMatter = 0;
            int nDate = 0;
            List<string> AddedMattNN = new List<string>();
            int AssignMatterNum = 1000;

            sSelect = @"SELECT * FROM [Matter]";
            Table = new DataTable("Matter");
            ReadAMTable(ref Table, sSelect);

            for (nMatter = 0; nMatter < Table.Rows.Count; nMatter++)
            {
                
                //if (includeClosedMatters || bool.Parse(Table.Rows[nMatter]["isActive"].ToString().Trim()))
              //  if (true)
             //   {
                    PCLaw.Matter.TaskNN = "BW";
                    PCLaw.Matter.ExternalID_1 = Table.Rows[nMatter]["matterID"].ToString().Trim();
                    PCLaw.Matter.ExternalID_2 = Table.Rows[nMatter]["nickname"].ToString().Trim();

                    PCLaw.Matter.NickName = Table.Rows[nMatter]["nickname"].ToString().Trim();

                    if (PCLaw.Matter.NickName.Equals(""))
                    {
                        AssignMatterNum++;
                        PCLaw.Matter.NickName = 'Z' + AssignMatterNum.ToString(); ;
                    }//end if

                    if (!string.IsNullOrEmpty(Table.Rows[nMatter]["Description"].ToString().Trim()))
                        PCLaw.Matter.Description = Table.Rows[nMatter]["Description"].ToString().Trim();
                    if (PCLaw.Matter.Description.Trim() == "")
                        PCLaw.Matter.Description = "General Matters";
                    if (!string.IsNullOrEmpty(Table.Rows[nMatter]["Memo"].ToString().Trim()))
                        PCLaw.Matter.Memos = Table.Rows[nMatter]["Memo"].ToString().Trim();

                    nDate = int.Parse(Table.Rows[nMatter]["opendate"].ToString().Trim());
                    if (nDate <= 19820101 || nDate >= 21991231)
                        nDate = 20061231;

                    PCLaw.Matter.DateOpened = nDate;
                    PCLaw.Matter.IsInactive = false;

                   // PCLaw.Matter.IsInactive = !bool.Parse(Table.Rows[nMatter]["isActive"].ToString().Trim());
                    //if (PCLaw.Matter.IsInactive)
                   // {
                    //    nDate = int.Parse(Table.Rows[nMatter]["closeddate"].ToString().Trim());
                    //    if (nDate <= 19820101 || nDate >= 21991231)
                    //        nDate = 20061231;

                    //    PCLaw.Matter.DateClosed = nDate;
                   // }

                    //PCLaw.Matter.Memos = Table.Rows[nMatter]["MatterName"].ToString().Trim();

                    //assign the type of law based on the externalID_2 we stored when we made the types of law (try to get the existing ID)
                    PCLaw.Matter.TypeOfLawID = PLConvert.PLTypeOfLaw.GetIDFromExtID1(Table.Rows[nMatter]["TypeOfLawID"].ToString().Trim());
                    if (PCLaw.Matter.TypeOfLawID == 0) //if that didnt work, lets try to get the nickname
                    {
                        PCLaw.Matter.TypeOfLawNN = Table.Rows[nMatter]["TypeOfLawID"].ToString().Trim();
                        if (PCLaw.Matter.TypeOfLawNN.Equals("")) //if neither work, assign the default type of law
                            PCLaw.Matter.TypeOfLawNN = "misc";
                    }//end if

                    try
                    {
                        double rate = 0.00;
                        if (!Table.Rows[nMatter]["rateamount"].ToString().Trim().Equals("0"))
                            rate = double.Parse(Table.Rows[nMatter]["rateamount"].ToString().Trim());
                        else
                            rate = 0.00;
                        //PCLaw.Matter.RateAmount = rate;
                        PCLaw.Matter.RateAmount = rate;
                        //if (PCLaw.Matter.RateAmount == 0.00) //if the rate is zero, try to get it from the responsible lawyer
                           // PCLaw.Matter.RateAmount = getRateFromLawyer(Table.Rows[nMatter]["resplawyerid"].ToString().Trim());
                    }//end try
                    catch (Exception ex1)
                    {
                        //if there is no rate (or an invalid one, get the defalt rate from the responsible lawyer
                        
                    }//end catch

                    PCLaw.Matter.LwrRespID = PLConvert.PLLawyer.GetIDFromExtID2(Table.Rows[nMatter]["resplawyerid"].ToString().Trim());
                    if (PCLaw.Matter.LwrRespID == 0) //if we didnt get a match on the IDfor the responsible lawyer, set the shortname
                    {
                        PCLaw.Matter.LwrRespNN = "IT~";
                        PCLaw.Matter.LwrAssignNN = "IT~";
                    }
                   // else
                       // PCLaw.Matter.LwrAssignID = PLConvert.PLLawyer.GetIDFromExtID2(Table.Rows[nMatter]["resplawyerid"].ToString().Trim());


                    //PCLaw.Matter.LwrAssignID = 11111;
                
                   // if (!string.IsNullOrWhiteSpace(Table.Rows[nMatter]["reflawyerName"].ToString().Trim()))
                   //     PCLaw.Matter.ReferredBy = Table.Rows[nMatter]["reflawyerName"].ToString().Trim();

                    //match up the client and matter based on eithet the client id. If you cant, assign the generic client
                    int nID = 0;
                    nID = PLConvert.PLClient.GetIDFromExtID1(Table.Rows[nMatter]["clientid"].ToString().Trim());
                    if (nID > 0)
                        PCLaw.Matter.ClientID = nID;
                    else
                    {
                        nID = PLConvert.PLClient.GetIDFromNN(Table.Rows[nMatter]["clientid"].ToString().Trim());
                        if (nID > 0)
                            PCLaw.Matter.ClientID = nID;
                        else
                            PCLaw.Matter.ClientNN = "GENCLI";
                    }//end else


                    if (String.IsNullOrEmpty(Table.Rows[nMatter]["billnametitle"].ToString().Trim()) == false)
                        PCLaw.Matter.BillName1.Title = Table.Rows[nMatter]["billnametitle"].ToString().Trim();
                    else
                        PCLaw.Matter.BillName1.Title = "";

                    if (String.IsNullOrEmpty(Table.Rows[nMatter]["billnamesuffix"].ToString().Trim()) == false)
                        PCLaw.Matter.BillName1.Suffix = Table.Rows[nMatter]["billnamesuffix"].ToString().Trim();
                    else
                        PCLaw.Matter.BillName1.Suffix = "";

                    if (!String.IsNullOrEmpty(Table.Rows[nMatter]["billnamefirst"].ToString().Trim()))
                        PCLaw.Matter.BillName1.First = Table.Rows[nMatter]["billnamefirst"].ToString().Trim();
                    else
                        PCLaw.Matter.BillName1.First = "";
                    if (!String.IsNullOrEmpty(Table.Rows[nMatter]["billnamemiddle"].ToString().Trim()))
                        PCLaw.Matter.BillName1.Middle = Table.Rows[nMatter]["billnamemiddle"].ToString().Trim();
                    else
                        PCLaw.Matter.BillName1.Middle = "";
                    PCLaw.Matter.BillName1.Last = Table.Rows[nMatter]["billnamelast"].ToString().Trim();

                   // if (bool.Parse(Table.Rows[nMatter]["iscorp"].ToString().Trim()) == true)
                    if (!String.IsNullOrEmpty(Table.Rows[nMatter]["billnamecompany"].ToString().Trim()))
                    {
                        PCLaw.Matter.BillName1.Company = Table.Rows[nMatter]["billnamecompany"].ToString().Trim();
                        PCLaw.Matter.BillName1.IsCorp = true;
                    }//end if
                    else
                    {
                        PCLaw.Matter.BillName1.Company = "";
                        PCLaw.Matter.BillName1.IsCorp = false;
                    }//end else

                    PCLaw.Matter.BillAddress1.Addr1 = Table.Rows[nMatter]["billaddressline1"].ToString().Trim();
                    //if (!string.IsNullOrEmpty(Table.Rows[nMatter]["billaddressline2"].ToString().Trim()))
                    //    PCLaw.Matter.BillAddress1.Addr2 = Table.Rows[nMatter]["billaddressline2"].ToString().Trim();
                    PCLaw.Matter.BillAddress1.City = Table.Rows[nMatter]["billcity"].ToString().Trim();
                    PCLaw.Matter.BillAddress1.Prov = Table.Rows[nMatter]["billstate"].ToString().Trim();
                    PCLaw.Matter.BillAddress1.Postal = Table.Rows[nMatter]["billzip"].ToString().Trim();

                    if (!string.IsNullOrWhiteSpace(Table.Rows[nMatter]["billcountry"].ToString().Trim()))
                        PCLaw.Matter.BillAddress1.Country = Table.Rows[nMatter]["billcountry"].ToString().Trim();

              //  }//end if

                PCLaw.Matter.AddRecord();
            }//end for

            //assigns the generic defaults to the NOTES matter. I honestly have no idea why this is required....but it is
            if (PLConvert.PLMatter.GetIDFromNN("NOTES").Equals(0))
            {
                PCLaw.Matter.NickName = "Notes";
                PCLaw.Matter.ClientNN="GENCLI";
                //PCLaw.Matter.TypeOfLawNN = "lit";
                PCLaw.Matter.TypeOfLawNN = "NA~";
                //PCLaw.Matter.LwrRespNN = "~IT";
                PCLaw.Matter.LwrRespNN = "IT~";
                PCLaw.Matter.AddRecord();
            }//end if
            PCLaw.Matter.SendLast();

        }//end method


        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method


 
    }//end class
}//end namespace
