using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using PLConvert;

namespace APIObjects
{
    public class AMContact : StagingTable
    {
        public AMContact()
        {
        }//end constructor

        public AMContact(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            try
            {
                if (PCLaw == null)
                    return;

                DataTable Table;
                string sSelect;
                int nContact = 0;

                Table = new DataTable("Contact");

                sSelect = @"SELECT * FROM [Contact]";
                ReadAMTable(ref Table, sSelect);

                for (nContact = 0; nContact < Table.Rows.Count; nContact++)
                {

                    PCLaw.Contact.ExternalID_1 = Table.Rows[nContact]["oldId"].ToString();


                    PCLaw.Contact.Name.IsCorp = bool.Parse(Table.Rows[nContact]["IsCorp"].ToString().Trim());
                    if (PCLaw.Contact.Name.IsCorp)
                    {
                        string name = Table.Rows[nContact]["CompanyName"].ToString().Trim();
                        if (string.IsNullOrEmpty(name))
                            PCLaw.Contact.Name.Company = "Name not Supplied";
                        else
                            PCLaw.Contact.Name.Company = name;
                    }//end if
                    else
                    {
                        if (!string.IsNullOrEmpty(Table.Rows[nContact]["FirstName"].ToString().Trim()))
                            PCLaw.Contact.Name.First = Table.Rows[nContact]["FirstName"].ToString().Trim();
                        else
                            PCLaw.Contact.Name.First = "Name Not Supplied";
                        if (!string.IsNullOrEmpty(Table.Rows[nContact]["MiddleName"].ToString().Trim()))
                            PCLaw.Contact.Name.Middle = Table.Rows[nContact]["MiddleName"].ToString().Trim();
                        if (!string.IsNullOrEmpty(Table.Rows[nContact]["LastName"].ToString().Trim()))
                            PCLaw.Contact.Name.Last = Table.Rows[nContact]["LastName"].ToString().Trim();
                        else
                            PCLaw.Contact.Name.Last = "Name not Supplied";
                        if (!string.IsNullOrEmpty(Table.Rows[nContact]["title"].ToString().Trim()))
                            PCLaw.Contact.Name.Title = Table.Rows[nContact]["title"].ToString().Trim();
                        if (!string.IsNullOrEmpty(Table.Rows[nContact]["Suffix"].ToString().Trim()))
                            PCLaw.Contact.Name.Suffix = Table.Rows[nContact]["Suffix"].ToString().Trim();
                        if (!string.IsNullOrEmpty(Table.Rows[nContact]["CompanyName"].ToString().Trim()))
                            PCLaw.Contact.Name.Company = Table.Rows[nContact]["CompanyName"].ToString().Trim();
                    }//end else

                    //PCLaw.Contact.Name.MakeNameKey();
                    if (!string.IsNullOrEmpty(Table.Rows[nContact]["NickName"].ToString().Trim()))
                    {
                        PCLaw.Contact.ExternalID_2 = Table.Rows[nContact]["NickName"].ToString().Trim();
                        PCLaw.Contact.NickName = Table.Rows[nContact]["NickName"].ToString().Trim();
                    }
                    else
                    {
                        PCLaw.Contact.NickName = PCLaw.Contact.MakeNN(true);
                        PCLaw.Contact.ExternalID_2 = PCLaw.Contact.NickName;
                    }

                    PCLaw.Contact.AddressMain.Addr1 = Table.Rows[nContact]["AddressLine1"].ToString().Trim();
                    PCLaw.Contact.AddressMain.Addr2 = Table.Rows[nContact]["AddressLine2"].ToString().Trim();
                    PCLaw.Contact.AddressMain.City = Table.Rows[nContact]["City"].ToString().Trim();
                    PCLaw.Contact.AddressMain.Prov = Table.Rows[nContact]["State"].ToString().Trim();
                    PCLaw.Contact.AddressMain.Postal = Table.Rows[nContact]["ZIP"].ToString().Trim();


                    //optional info
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["Country"].ToString().Trim()))
                        PCLaw.Contact.AddressMain.Country = Table.Rows[nContact]["Country"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["BusPhone"].ToString().Trim()))
                        PCLaw.Contact.Phone.BusPhone = Table.Rows[nContact]["BusPhone"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["BusFax"].ToString().Trim()))
                        PCLaw.Contact.Phone.BusFax = Table.Rows[nContact]["BusFax"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["HomePhone"].ToString().Trim()))
                        PCLaw.Contact.Phone.HomePhone = Table.Rows[nContact]["HomePhone"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["CellPhone"].ToString().Trim()))
                        PCLaw.Contact.Phone.CellPhone = Table.Rows[nContact]["CellPhone"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["Email"].ToString().Trim()))
                        PCLaw.Contact.Phone.BusEMail = Table.Rows[nContact]["Email"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["SecondAddressLine1"].ToString().Trim()))
                        PCLaw.Contact.AddressOther.Addr1 = Table.Rows[nContact]["SecondAddressLine1"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["SecondAddressLine2"].ToString().Trim()))
                        PCLaw.Contact.AddressOther.Addr2 = Table.Rows[nContact]["SecondAddressLine2"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["SecondCity"].ToString().Trim()))
                        PCLaw.Contact.AddressOther.City = Table.Rows[nContact]["SecondCity"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["SecondState"].ToString().Trim()))
                        PCLaw.Contact.AddressOther.Prov = Table.Rows[nContact]["SecondState"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["SecondZip"].ToString().Trim()))
                        PCLaw.Contact.AddressOther.Postal = Table.Rows[nContact]["SecondZIP"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Table.Rows[nContact]["SecondCountry"].ToString().Trim()))
                        PCLaw.Contact.AddressOther.Country = Table.Rows[nContact]["SecondCountry"].ToString().Trim();

                    //try to match up contact types. If you cant, assign it to "other"
                    int nID = 0;
                    nID = PLConvert.PLContactType.GetIDFromNN(Table.Rows[nContact]["ContactType"].ToString().Trim());
                    if (nID > 0)
                        PCLaw.Contact.MainContTypeID = nID;
                    else
                        PCLaw.Contact.MainContTypeNN = "oth";

                    PCLaw.Contact.AddRecord();

                }//end for
                PCLaw.Contact.SendLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method

        public override string ToString()
        {
            return "Contact";
        }//end method
    }//end class
}//end namespace
