using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using PLConvert;


namespace APIObjects
{
    public class AMVendor : StagingTable
    {
        public AMVendor()
        {
        }//end constructor

        public AMVendor(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;

            //sKey = rdr["NAME"].ToString().Trim();
                        //sKey = sKey.ToUpper()
            //nID = PLVendor.GetIDFromNameKey(sKey);
           // if (nID > 0)
              //  bIsInPCLaw = true;


            DataTable Table = new DataTable("Vendor");
            string sSelect = "SELECT * FROM [Vendor]";
            ReadAMTable(ref Table, sSelect);

                int nVendor = 0;
                for (nVendor = 0; nVendor < Table.Rows.Count; nVendor++)
                {
                    PCLaw.Vendor.NickName = Table.Rows[nVendor]["VendorID"].ToString().Trim();
                   // PCLaw.Vendor.MakeNN(true);
                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["AccountNumber"].ToString().Trim()))
                        PCLaw.Vendor.AcctNum = Table.Rows[nVendor]["AccountNumber"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["BusPhone"].ToString().Trim()))
                        PCLaw.Vendor.Phone.BusPhone = Table.Rows[nVendor]["BusPhone"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["HomeFax"].ToString().Trim()))
                        PCLaw.Vendor.Phone.HomeFax = Table.Rows[nVendor]["HomeFax"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["HomePhone"].ToString().Trim()))
                        PCLaw.Vendor.Phone.HomePhone = Table.Rows[nVendor]["HomePhone"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["BusFax"].ToString().Trim()))
                        PCLaw.Vendor.Phone.BusFax = Table.Rows[nVendor]["BusFax"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["Cell"].ToString().Trim()))
                        PCLaw.Vendor.Phone.CellPhone= Table.Rows[nVendor]["Cell"].ToString().Trim();

                    PCLaw.Vendor.ActiveFlag = bool.Parse(Table.Rows[nVendor]["isActive"].ToString().Trim());

                    PCLaw.Vendor.Name.IsCorp = bool.Parse(Table.Rows[nVendor]["isCorp"].ToString().Trim());
                    if (PCLaw.Vendor.Name.IsCorp)
                    {
                        if (!string.IsNullOrEmpty(Table.Rows[nVendor]["CompanyName"].ToString().Trim()))
                            PCLaw.Vendor.Name.Company = Table.Rows[nVendor]["CompanyName"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["firstname"].ToString().Trim()))
                        PCLaw.Vendor.Name.First = Table.Rows[nVendor]["firstname"].ToString().Trim();
                    else
                        PCLaw.Vendor.Name.First = "Company:";
                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["middlename"].ToString().Trim()))
                        PCLaw.Vendor.Name.Middle = Table.Rows[nVendor]["middlename"].ToString().Trim();
                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["lastname"].ToString().Trim()))
                        PCLaw.Vendor.Name.Last = Table.Rows[nVendor]["lastname"].ToString().Trim();  
                    else
                        PCLaw.Vendor.Name.Last = Table.Rows[nVendor]["CompanyName"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["AddressLine1"].ToString().Trim()))
                        PCLaw.Vendor.Address.Addr1 = Table.Rows[nVendor]["AddressLine1"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["AddressLine2"].ToString().Trim()))
                        PCLaw.Vendor.Address.Addr2 = Table.Rows[nVendor]["AddressLine2"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["City"].ToString().Trim()))
                        PCLaw.Vendor.Address.City = Table.Rows[nVendor]["City"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["State"].ToString().Trim()))
                        PCLaw.Vendor.Address.Prov = Table.Rows[nVendor]["State"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["Zip"].ToString().Trim()))
                        PCLaw.Vendor.Address.Postal = Table.Rows[nVendor]["Zip"].ToString().Trim();

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["Country"].ToString().Trim()))
                        PCLaw.Vendor.Address.Country = Table.Rows[nVendor]["Country"].ToString().Trim();

                 //   if (!string.IsNullOrEmpty(Table.Rows[nVendor]["Terms"].ToString().Trim()))
                //        PCLaw.Vendor.Terms = Table.Rows[nVendor]["Terms"].ToString().Trim();
                //    PCLaw.Vendor.DiscPct1 = Convert.ToDouble(Table.Rows[nVendor]["DiscountPercentage"].ToString().Trim());
               //     PCLaw.Vendor.DiscDays1 = Convert.ToInt32(Table.Rows[nVendor]["DiscountDays"].ToString().Trim());

                    if (!string.IsNullOrEmpty(Table.Rows[nVendor]["AccountNumber"].ToString().Trim()))
                    {
                        PCLaw.Vendor.US1099ID = Table.Rows[nVendor]["AccountNumber"].ToString().Trim();
                        PCLaw.Vendor.US1099BoxNumber = 7;
                        PCLaw.Vendor.US1099Type = "Misc";
                    }


                    //PCLaw.Vendor.DefGSTCat = 121;
                    

                    //PCLaw.Vendor.ExternalID_1 = Table.Rows[nVendor]["OldID"].ToString().Trim();

                    PCLaw.Vendor.AddRecord();
                }//end for
                PCLaw.Vendor.SendLast();
        }//end method



        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used
        }//end method
    }//end class
}//end namespace
