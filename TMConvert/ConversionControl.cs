using System;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using PLConvert;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StagingConvert
{
    public class ConversionControl : object
    {

        bool includeClosedMatters = false;


        public ConversionControl(bool inclClosedMatters)
        {
            includeClosedMatters = inclClosedMatters;
            PCLaw = new PLConvert.PCLawConversion();
            PLConvert.PLLink.ResetLogFile();
            Client = new APIObjects.AMClient(PCLaw);
            Contact = new APIObjects.AMContact(PCLaw);
            ContactType = new APIObjects.AMContactType(PCLaw);
            DiaryCode = new APIObjects.AMDiaryCode(PCLaw);
            Lawyer = new APIObjects.AMLawyer(PCLaw);
            Matter = new APIObjects.AMMatter(PCLaw);
            Code = new APIObjects.AMCode(PCLaw);
            TypeOfLaw = new APIObjects.AMTypeOfLaw(PCLaw);
            User = new APIObjects.AMUser(PCLaw);
            TimeEntry = new APIObjects.AMTimeEntry(PCLaw);
            Diary = new APIObjects.AMDiary(PCLaw);
            GLAcct = new APIObjects.AMGLAcct(PCLaw);
            Bill = new APIObjects.AMBill(PCLaw);
            Expense = new APIObjects.AMExpense(PCLaw);
            Trust = new APIObjects.AMTrust(PCLaw);
            Vendor = new APIObjects.AMVendor(PCLaw);
        }//end constructor


        public void ImportLawyers()
        {
            Lawyer.AddRecords(ConversionControl.bPCLawMoreUpToDate);
        }//end method

        public void ImportUsers()
        {
            User.AddRecords(ConversionControl.bPCLawMoreUpToDate);
        }//end method

        public void ImportClients()
        {
            Client.AddRecords(ConversionControl.bPCLawMoreUpToDate);
        }//end method

        public void ImportTypesOfLaw()
        {
            TypeOfLaw.AddRecords(ConversionControl.bPCLawMoreUpToDate);
        }//end method

        public void ImportMatters()
        {
            Matter.AddRecords(ConversionControl.bPCLawMoreUpToDate, ConversionControl.bCombineCliMatt, includeClosedMatters);
        }//end method

        public void ImportGLAccounts()
        {
           // GLAcct.AddRecords(ConversionControl.bPCLawMoreUpToDate);

        }//end method

        public void ImportCodes()
        {
            Code.AddRecords(ConversionControl.bPCLawMoreUpToDate);
            
        }//end method

        public void ImportContacts()
        {
            //ContactType.AddRecords(ConversionControl.bPCLawMoreUpToDate);
            //Contact.AddRecords(ConversionControl.bPCLawMoreUpToDate);
            //PLConvert.PLContact.ClearMapNameKeytoPLID();
        }//end method



        public void ImportVendors()
        {
            Vendor.AddRecords(ConversionControl.bPCLawMoreUpToDate);
            MessageBox.Show("Worked!");
        }//end method



        public void ImportDiary()
        {
            //DiaryCode.AddRecords(ConversionControl.bPCLawMoreUpToDate);
           // Diary.AddRecords(ConversionControl.bPCLawMoreUpToDate);
        }//end method

        public void ImportWIPFees()
        {
            //TimeEntry.AddRecords(includeClosedMatters);
        }//end method

        public void ImportWIPExpenses()
        {
            //Expense.AddRecords(includeClosedMatters);
        }//end method

        public void ImportAR()
        {
            //Bill.AddRecords(includeClosedMatters);
            
        }//end method

        public void ImportTrust()
        {
            //Trust.AddRecords(bPCLawMoreUpToDate);
            
        }//end method



        public static PLConvert.PCLawConversion PCLaw;
        public static bool bPCLawMoreUpToDate = false;
        public static bool bImportActiveMatters;
        public static bool bImportArchivedMattres;
        public static bool bImportSpecialMatters;
        public static bool bCombineCliMatt;
        public static bool bImportContacts;
        public static bool bImportDiary;
        public static bool bImportTimeFees;

        public APIObjects.AMClient Client;
        public APIObjects.AMContact Contact;
        public APIObjects.AMContactType ContactType;
        public APIObjects.AMDiaryCode DiaryCode;
        public APIObjects.AMLawyer Lawyer;
        public APIObjects.AMMatter Matter;
        public APIObjects.AMCode Code;
        public APIObjects.AMTypeOfLaw TypeOfLaw;
        public APIObjects.AMUser User;
        public APIObjects.AMTimeEntry TimeEntry;
        public APIObjects.AMDiary Diary;
        public APIObjects.AMGLAcct GLAcct;
        public APIObjects.AMBill Bill;
        public APIObjects.AMExpense Expense;
        public APIObjects.AMTrust Trust;
        public APIObjects.AMVendor Vendor;

    }//end class
}//end namespace
