using System;

namespace APIObjects
{

    public class Selections
    {
        public Selections()
        {
        }//end constructor

        static Selections()
        {
            bPCLawMoreUpToDate = true;
            bImportActiveMatters = true;
            bImportArchivedMattres = true;
            bImportSpecialMatters = true;
            bCombineCliMatt = false;
            bImportContacts = true;
            bImportDiary = true;
            bImportTimeFees = true;
        }//end constructor

        public static bool bPCLawMoreUpToDate;
        public static bool bImportActiveMatters;
        public static bool bImportArchivedMattres;
        public static bool bImportSpecialMatters;
        public static bool bCombineCliMatt;
        public static bool bImportContacts;
        public static bool bImportDiary;
        public static bool bImportTimeFees;
    }
}
