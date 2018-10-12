using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Collections;

namespace APIObjects
{
    public class AMClient : StagingTable
    {
        public AMClient()
        {
        }//end constructor

        public AMClient(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;

            DataTable Table;
            string sSelect = "";
            int nClient = 0;

            Table = new DataTable("Client");

            sSelect = "SELECT * FROM [Client]";
            ReadAMTable(ref Table, sSelect);

            for (nClient = 0; nClient < Table.Rows.Count; nClient++)
            {

                //bIsInPCLaw = false;
                string fullName = Table.Rows[nClient]["lastname"].ToString().Trim();

                PCLaw.Client.ExternalID_1 = Table.Rows[nClient]["clientid"].ToString().Trim();
                PCLaw.Client.ExternalID_2 = Table.Rows[nClient]["nickname"].ToString().Trim();
                PCLaw.Client.NickName = Table.Rows[nClient]["nickname"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["attention"].ToString().Trim()))
                    PCLaw.Client.Address.Attn = Table.Rows[nClient]["attention"].ToString().Trim();
                else
                    PCLaw.Client.Address.Attn = "";
                //see if the nickname is blank and make a new one if so
                if (PCLaw.Client.NickName.Equals("") == true)
                    PCLaw.Client.NickName = PLConvert.PLClient.GetNextClientNN();


                PCLaw.Client.Name.IsCorp = bool.Parse(Table.Rows[nClient]["iscorp"].ToString());
                if (PCLaw.Client.Name.IsCorp)
                    PCLaw.Client.Name.Company = Table.Rows[nClient]["companyname"].ToString().Trim();

                if (!String.IsNullOrEmpty(Table.Rows[nClient]["CompanyName"].ToString().Trim()))
                {
                    PCLaw.Client.Name.Company = Table.Rows[nClient]["CompanyName"].ToString().Trim();
                    PCLaw.Client.Name.IsCorp = true;
                }//end if
                else
                {
                    PCLaw.Client.Name.Company = "";
                    PCLaw.Client.Name.IsCorp = false;
                }//end else

                if (!string.IsNullOrEmpty(Table.Rows[nClient]["firstname"].ToString().Trim()))
                    PCLaw.Client.Name.First = Table.Rows[nClient]["firstname"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["middlename"].ToString().Trim()))
                    PCLaw.Client.Name.Middle = Table.Rows[nClient]["middlename"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["lastname"].ToString().Trim()))
                    PCLaw.Client.Name.Last = Table.Rows[nClient]["lastname"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["title"].ToString().Trim()))
                    PCLaw.Client.Name.Title = Table.Rows[nClient]["title"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["suffix"].ToString().Trim()))
                    PCLaw.Client.Name.Suffix = Table.Rows[nClient]["suffix"].ToString().Trim();
 
                //ensure there is a name
                if (PCLaw.Client.Name.IsCorp)
                {
                    if (string.IsNullOrWhiteSpace(PCLaw.Client.Name.Company))
                        PCLaw.Client.Name.Company = "Name not Supplied";
                }//end if
                else
                {
                    if (string.IsNullOrWhiteSpace(PCLaw.Client.Name.Last))
                    {
                        PCLaw.Client.Name.Last = PCLaw.Client.Name.First = " " + PCLaw.Client.Name.Middle;
                        PCLaw.Client.Name.Last = PCLaw.Client.Name.Last.Trim();
                        PCLaw.Client.Name.First = "";
                        PCLaw.Client.Name.Middle = "";
                    }//end if
                    if (string.IsNullOrWhiteSpace(PCLaw.Client.Name.Last))
                    {
                        PCLaw.Client.Name.Last = PCLaw.Client.Name.Company;
                        PCLaw.Client.Name.Company = "";
                    }//end if

                    if (string.IsNullOrWhiteSpace(PCLaw.Client.Name.Last))
                        PCLaw.Client.Name.Last = "Name not Supplied";
                }//end else
                 
                try
                {
                PCLaw.Client.Address.Addr1 = Table.Rows[nClient]["addressline1"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["addressline2"].ToString().Trim()))
                    PCLaw.Client.Address.Addr2 = Table.Rows[nClient]["addressline2"].ToString().Trim();
                PCLaw.Client.Address.City = Table.Rows[nClient]["city"].ToString().Trim();
                PCLaw.Client.Address.Prov = Table.Rows[nClient]["state"].ToString().Trim();
                PCLaw.Client.Address.Postal = Table.Rows[nClient]["zip"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["country"].ToString().Trim()))
                    PCLaw.Client.Address.Country = Table.Rows[nClient]["country"].ToString().Trim();
                //optional info
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["busphone"].ToString().Trim()))
                    PCLaw.Client.Phone.BusPhone = Table.Rows[nClient]["busphone"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["homephone"].ToString().Trim()))
                    PCLaw.Client.Phone.HomePhone = Table.Rows[nClient]["homephone"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["cellphone"].ToString().Trim()))
                    PCLaw.Client.Phone.CellPhone = Table.Rows[nClient]["cellphone"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["homefax"].ToString().Trim()))
                    PCLaw.Client.Phone.HomeFax = Table.Rows[nClient]["homefax"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["busfax"].ToString().Trim()))
                    PCLaw.Client.Phone.BusFax = Table.Rows[nClient]["busfax"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["email"].ToString().Trim()))
                    PCLaw.Client.Phone.BusEMail = Table.Rows[nClient]["email"].ToString().Trim();
                if (!string.IsNullOrEmpty(Table.Rows[nClient]["website"].ToString().Trim()))
                    PCLaw.Client.Phone.WebPage = Table.Rows[nClient]["website"].ToString().Trim();

                PCLaw.Client.AddRecord();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Error 1 " + ex2);
                }
            }//end for

            //add the generic default client if it doesnt exist
            if (PLConvert.PLClient.GetIDFromNN("GENCLI") == 0)
            {
                PCLaw.Client.NickName = "GENCLI";
                PCLaw.Client.Name.Last = "Generic Client";
                PCLaw.Client.AddRecord();
            }//end if

            PCLaw.Client.SendLast();
        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end if

        public override string ToString()
        {
            return "Client";
        }//end method
    }//end class
}//end namespace
