using System;
using System.Data;
using PLConvert;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APIObjects
{
    public class AMDiary : StagingTable
    {
        public AMDiary()
        {
        }//end constructor

        public AMDiary(PLConvert.PCLawConversion PL)
        {
            PCLaw = PL;
        }//end constructor

        public override void AddRecords(bool bPCLawMoreUptoDate)
        {
            if (PCLaw == null)
                return;
            
            //Diary consists of 3 items:
            //1) Calendar which is Appointments(events) and ToDos
            //2) Phone calls
            //3) Notes - show on matter and contact but are added seperately

            addCalendarEntries(); //code type 2 and 3 on the DiaryCodes table
            addPhoneCalls();
            addNotes(); //code type 1 on the DiaryCodes table
        }//end method

        public override void AddRecords(PLConvert.PCLawConversion PL, bool bPCLawMoreUptoDate)
        {
            //not used, yet.
        }//end method


        private void addCalendarEntries()
        {
            int nCalendar = 0;
            TimeSpan Duration;
            int nDate = 0;
            //int nDateEnd = 0;
            int nTimeStart = 0;
            int nValue = 0;

            DataTable Table = new DataTable("Appointment");
            string sSelect = "SELECT * FROM [Appointment]";
            ReadAMTable(ref Table, sSelect);

            for (nCalendar = 0; nCalendar < Table.Rows.Count; nCalendar++)
            {
                PCLaw.Diary.IsRecurringEntry = bool.Parse(Table.Rows[nCalendar]["IsRecurring"].ToString());

                nDate = int.Parse(Table.Rows[nCalendar]["EnteredDate"].ToString().Trim());
                if (nDate < 19820101) //if the date is before pclaw's min level, make it the min
                    nDate = 19820101;
                PCLaw.Diary.EnteredDate = nDate;

                nDate = int.Parse(Table.Rows[nCalendar]["DueDate"].ToString().Trim());
                if (nDate < 19820101)
                    nDate = 19820101;
                PCLaw.Diary.DueDate = nDate;

                DateTime DateEnd;
                DateTime DateStart;

                if (double.Parse(Table.Rows[nCalendar]["Duration"].ToString()) == 0)
                {
                    DateEnd = Convert.ToDateTime(Table.Rows[nCalendar]["DueDate"].ToString().Insert(6, "-").Insert(4, "-"));
                    DateStart = Convert.ToDateTime(Table.Rows[nCalendar]["StartDate"].ToString().Insert(6, "-").Insert(4, "-"));
                    Duration = DateEnd - DateStart; //duration of the event
                    PCLaw.Diary.Duration = Duration.Days * 24 + Duration.Hours + Duration.Minutes / 60;
                }
                else
                {
                    Duration = TimeSpan.FromHours(double.Parse(Table.Rows[nCalendar]["Duration"].ToString()));
                    PCLaw.Diary.Duration = double.Parse(Duration.ToString());
                }


                if (PCLaw.Diary.IsRecurringEntry)//if its recurring, we have work to do!
                {
                    PCLaw.Diary.RecurringStartDate = int.Parse(Table.Rows[nCalendar]["RecurringStartDate"].ToString().Trim());
                    PCLaw.Diary.RecurringEndDate = int.Parse(Table.Rows[nCalendar]["RecurringEndDate"].ToString().Trim());

                    PCLaw.Diary.RecurringRepeatUnit = PLDiary.eRepeatUnit.CalendarDay;

                    //1) Daily requires a number od days before repeating (rDaysDaily)
                    //2) Monthly requires:
                    //   a) Which day of the month to repeat (rDayOfMonth) - OR-
                    //   b) Which week of the month (rWeekOfMonth) AND which day of the week to repeat (rDayOfWeek)
                    //3) Quarterly requires a day of the month (rDayOfMonth) AND how many months before it repeats (rMonthsQuarterly)
                    //4) Weekly requires a day of the week (rDayOfWeek)
                    //5) Anually requires a month of the year (rMonthsOfYear) AND
                    //   a) Day of the month (rDayOfMonth) OR
                    //   b) Week of month (rWeekOfMonth) AND day of week (rDaysOfWeek)
                    //Frequency means how often to make the appointment

                    switch (int.Parse(Table.Rows[nCalendar]["RecurringType"].ToString()))
                    {
                        case 1:   //Daily
                            PCLaw.Diary.RecurringRepeatUnit = PLDiary.eRepeatUnit.CalendarDay;
                            PCLaw.Diary.RecurringFreq = int.Parse(Table.Rows[nCalendar]["rDaysDaily"].ToString());
                            break;

                        case 2: //Monthly   
                            PCLaw.Diary.RecurringRepeatUnit = PLDiary.eRepeatUnit.Month;

                            if (int.Parse(Table.Rows[nCalendar]["rDayOfMonth"].ToString()) != 0)
                                PCLaw.Diary.RecurringDayOfMonth = int.Parse(Table.Rows[nCalendar]["rDayOfMonth"].ToString());
                            else
                            {
                                if (int.Parse(Table.Rows[nCalendar]["rWeekOfMonth"].ToString()) != 0)
                                    PCLaw.Diary.RecurringWeekOfMonth = int.Parse(Table.Rows[nCalendar]["rWeekOfMonth"].ToString());

                                if (PCLaw.Diary.RecurringWeekOfMonth == 0)
                                    PCLaw.Diary.RecurringWeekOfMonth = 1;

                                switch (int.Parse(Table.Rows[nCalendar]["rDayOfWeek"].ToString()))
                                {
                                    case 1:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Sun;
                                        break;
                                    case 2:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Non;
                                        break;
                                    case 3:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Tue;
                                        break;
                                    case 4:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Wed;
                                        break;
                                    case 5:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Thur;
                                        break;
                                    case 6:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Fri;
                                        break;
                                    case 7:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Sat;
                                        break;
                                    default:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Non;
                                        break;
                                }//end switch
                            }//end else
                            decimal month = Duration.Days/30; // there are 30 days in an average month so days/30 = months
                            PCLaw.Diary.RecurringFreq = Convert.ToInt32(Math.Ceiling(month)); //ceiling because we need an int here
                            if (PCLaw.Diary.RecurringFreq < 1)
                                PCLaw.Diary.RecurringFreq = 1;
                            break;

                        case 3: //Quarterly
                            PCLaw.Diary.RecurringRepeatUnit = PLDiary.eRepeatUnit.Month;
                            PCLaw.Diary.RecurringDayOfMonth = int.Parse(Table.Rows[nCalendar]["rDayOfMonth"].ToString());
                            PCLaw.Diary.RecurringFreq = int.Parse(Table.Rows[nCalendar]["rMonthsQuarterly"].ToString());
                            break;

                        case 4: //Weekly
                            PCLaw.Diary.RecurringRepeatUnit = PLDiary.eRepeatUnit.Week;

                            switch (int.Parse(Table.Rows[nCalendar]["rDayOfWeek"].ToString()))
                            {
                                case 1:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Sun;
                                    break;
                                case 2:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Non;//to do - change that type in plconvert
                                    break;
                                case 3:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Tue;
                                    break;
                                case 4:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Wed;
                                    break;
                                case 5:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Thur;
                                    break;
                                case 6:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Fri;
                                    break;
                                case 7:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Sat;
                                    break;
                                default:
                                    PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Non;
                                    break;
                            }//end switch

                            decimal week = Duration.Days/4; //there are 4 weeks in a month so days/4 = weeks
                            PCLaw.Diary.RecurringFreq = Convert.ToInt32(Math.Ceiling(week));
                            if (PCLaw.Diary.RecurringFreq < 1)
                                PCLaw.Diary.RecurringFreq = 1;
                            break;
                        case 5: //Annually
                            PCLaw.Diary.RecurringRepeatUnit = PLDiary.eRepeatUnit.Year;

                            PCLaw.Diary.RecurringMonthOfYear = int.Parse(Table.Rows[nCalendar]["rMonthsOfYear"].ToString());

                            if (int.Parse(Table.Rows[nCalendar]["rDayOfMonth"].ToString()) != 0)
                                PCLaw.Diary.RecurringDayOfMonth = int.Parse(Table.Rows[nCalendar]["rDayOfMonth"].ToString());
                            else
                            {
                                PCLaw.Diary.RecurringWeekOfMonth = int.Parse(Table.Rows[nCalendar]["rWeekOfMonth"].ToString());

                                switch (int.Parse(Table.Rows[nCalendar]["rDayOfWeek"].ToString()))
                                {
                                    case 1:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Sun;
                                        break;
                                    case 2:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Non;
                                        break;
                                    case 3:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Tue;
                                        break;
                                    case 4:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Wed;
                                        break;
                                    case 5:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Thur;
                                        break;
                                    case 6:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Fri;
                                        break;
                                    case 7:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Sat;
                                        break;
                                    default:
                                        PCLaw.Diary.RecurringDayOfWeek = PLConvert.PLDiary.eDay.Non;
                                        break;
                                }//end switch
                            }//end else

                            decimal year = Duration.Days/365;
                            PCLaw.Diary.RecurringFreq = Convert.ToInt32(Math.Ceiling(year));
                            if (PCLaw.Diary.RecurringFreq < 1)
                                PCLaw.Diary.RecurringFreq = 1;
                            break;
                    }//end switch with no default on purpose

                    switch (int.Parse(Table.Rows[nCalendar]["HolidayRecurringBehavior"].ToString()))  
                    {
                        case 1:
                            PCLaw.Diary.RecurringAdjust = PLDiary.eAdjust.NextBusDay;
                            break;
                        case 2:
                            PCLaw.Diary.RecurringAdjust = PLDiary.eAdjust.PrevBusDay;
                            break;
                        case 3:
                            PCLaw.Diary.RecurringAdjust = PLDiary.eAdjust.SameDay;
                            break;
                        case 4:
                            PCLaw.Diary.RecurringAdjust = PLDiary.eAdjust.CancelEntry;
                            break;
                    }//end switch
                }//end recurring if

                switch (int.Parse(Table.Rows[nCalendar]["EntryType"].ToString()))
                {
                    case 0:
                        PCLaw.Diary.EntryType = PLDiary.eType.Holiday;
                        break;
                    case 1:
                        PCLaw.Diary.EntryType = PLDiary.eType.Appointment;
                        break;
                    case 2:
                        PCLaw.Diary.EntryType = PLDiary.eType.TODO;
                        break;
                }//end switch

                if (!string.IsNullOrEmpty(Table.Rows[nCalendar]["Description"].ToString()))
                    PCLaw.Diary.Description = Table.Rows[nCalendar]["Description"].ToString();
                else
                    PCLaw.Diary.Description = "Converted Event - No Description Available";

                if (PCLaw.Diary.EntryType == PLDiary.eType.Appointment)
                {
                    //pclaw needs the time in this format hhmm as an int
                        //nTimeStart = DateStart.Hour * 100 + DateStart.Minute;
                    nTimeStart = int.Parse(Table.Rows[nCalendar]["StartTime"].ToString());
                        if (nTimeStart < 700 || nTimeStart > 2359)//why would this appointment happen before 7am or be greater than 11:59?
                            nTimeStart = 700;

                        PCLaw.Diary.StartTime = nTimeStart;
                    //duration needs to be an double and is in hours which is why the funky math
                        //PCLaw.Diary.Duration = Duration.Days * 24 + Duration.Hours + Duration.Minutes / 60;
                }//end if

                if (PCLaw.Diary.Duration <= 0) //duration cant be zero or negative
                    PCLaw.Diary.Duration = 0.5;

                switch (int.Parse(Table.Rows[nCalendar]["Priority"].ToString()))
                {
                    case 0:
                        PCLaw.Diary.Priority = PLDiary.ePriority.Normal;
                        break;
                    case 1:
                        PCLaw.Diary.Priority = PLDiary.ePriority.High;
                        break;
                    case 2:
                        PCLaw.Diary.Priority = PLDiary.ePriority.Low;
                        break;
                    default:
                        PCLaw.Diary.Priority = PLDiary.ePriority.Normal;
                        break;
                }//end switch

                if (PCLaw.Diary.EntryType == PLDiary.eType.TODO)
                {
                    if (!string.IsNullOrEmpty(Table.Rows[nCalendar]["Completed"].ToString()))
                        PCLaw.Diary.Completed = bool.Parse(Table.Rows[nCalendar]["Completed"].ToString());
                    else
                        PCLaw.Diary.Completed = false;
                    if (PCLaw.Diary.Completed)
                        PCLaw.Diary.CompletionDate = int.Parse(Table.Rows[nCalendar]["CompletionDate"].ToString()); //always on time! (you can change this if required though)
                }//end if

                PCLaw.Diary.DiaryCodeID = PLDiaryCode.GetIDFromExtID1(Table.Rows[nCalendar]["DiaryCodeID"].ToString());

                PCLaw.Diary.MatterID = PLMatter.GetIDFromExtID1(Table.Rows[nCalendar]["MatterID"].ToString());
                if (PCLaw.Diary.MatterID == 0) //not found and is required....diary entry not added
                {
                    PCLaw.Diary.Clear();
                    continue;
                }

                //add lawyers to diary entry
                string[] lawyers = Table.Rows[nCalendar]["LawyerID"].ToString().Split('|');
                foreach (string lawyer in lawyers)
                {
                    nValue = PLLawyer.GetIDFromExtID1(lawyer);
                    if (nValue != 0)
                        PCLaw.Diary.AddLawyer(nValue);
                }//end foreach

                //add contacts to diary entry
                string[] contacts = Table.Rows[nCalendar]["ContactID"].ToString().Split('|');
                foreach (string contact in contacts)
                {
                    nValue = PLContact.GetIDFromExtID1(contact);
                    if (nValue != 0)
                        PCLaw.Diary.AddContact(nValue);
                }//end foreach

                nDate = int.Parse(Table.Rows[nCalendar]["ReminderDate"].ToString().Replace("-", ""));
                if (nDate < 19820101)
                    PCLaw.Diary.ReminderDate = PCLaw.Diary.DueDate;
                else
                    PCLaw.Diary.ReminderDate = nDate;

                PCLaw.Diary.MinutesToRemind = int.Parse(Table.Rows[nCalendar]["MinutesToRemind"].ToString());

                PCLaw.Diary.AddRecord();
            }//end for

            PCLaw.Diary.SendLast();
        }//end method

        private void addPhoneCalls()
        {
            int nPhone = 0;
            DateTime DateStart;
            DateTime DateEnd;
            TimeSpan Duration;
            //int nDateStart = 0;
            int nStartTime = 0;
            int nEndTime = 0;
            int nValue = 0;

            DataTable Table = new DataTable("Phone");
            string sSelect = "SELECT * FROM [PhoneCall]";
            ReadAMTable(ref Table, sSelect);

            for (nPhone = 0; nPhone < Table.Rows.Count; nPhone++)
            {
                PCLaw.Diary.EntryType = PLConvert.PLDiary.eType.Call;
                PCLaw.Diary.CallDirectionOut = bool.Parse(Table.Rows[nPhone]["OutgoingCall"].ToString().Trim());

                PCLaw.Diary.CallContactName = Table.Rows[nPhone]["CallerName"].ToString().Trim();
                PCLaw.Diary.CallPhoneNumber = Table.Rows[nPhone]["CallerNumber"].ToString().Trim();

                int nDate = int.Parse(Table.Rows[nPhone]["DueDate"].ToString().Trim()); //see appointment for same code with notes
                if (nDate < 19820101)
                    nDate = 19820101;
                DateStart = Convert.ToDateTime(Table.Rows[nPhone]["StartTime"].ToString());
                PCLaw.Diary.DueDate = nDate;

                PCLaw.Diary.EnteredDate = int.Parse(Table.Rows[nPhone]["EnteredDate"].ToString().Trim());

                nStartTime = DateStart.Hour * 100 + DateStart.Minute; //see appointment for same code with notes hhmm
                PCLaw.Diary.StartTime = nStartTime;

                if (!string.IsNullOrEmpty(Table.Rows[nPhone]["EndTime"].ToString()))//shouldnt ever be null or empty
                {
                    if (int.Parse(Table.Rows[nPhone]["EndTime"].ToString()) != 0) //should be zero if we are using duration (like for splits)
                    {
                        DateEnd = Convert.ToDateTime(Table.Rows[nPhone]["EndTime"].ToString());
                        nEndTime = DateEnd.Hour * 100 + DateEnd.Minute;

                        Duration = DateEnd - DateStart;

                        PCLaw.Diary.Duration = Duration.Days * 24 + Duration.Hours + Duration.Minutes / 60; //see appointment for same code with notes
                    }
                }

                else
                    PCLaw.Diary.Duration = double.Parse(Table.Rows[nPhone]["Duration"].ToString());

                
                if (PCLaw.Diary.Duration < 0)
                    PCLaw.Diary.Duration = 1.0 / 60.0; //one minute


                if (!string.IsNullOrEmpty(Table.Rows[nPhone]["Description"].ToString().Trim()))
                    PCLaw.Diary.Description = Table.Rows[nPhone]["Description"].ToString().Trim();
                else
                    PCLaw.Diary.Description = "Converted Call - No Description available";

                switch (Convert.ToInt32(Table.Rows[nPhone]["CallActionTaken"].ToString().Trim()))
                {
                    // 1 = spoke, 2 = Left Message, 3 = No Answer, 4 = Busy, 5 = Voice Mail
                    case 1:
                        PCLaw.Diary.PhoneCallAction = PLConvert.PLDiary.eCallAction.Spoke;
                        break;

                    case 2:
                        PCLaw.Diary.PhoneCallAction = PLConvert.PLDiary.eCallAction.LeftMsg;
                        break;

                    case 3:
                        PCLaw.Diary.PhoneCallAction = PLConvert.PLDiary.eCallAction.NoAnswer;
                        break;

                    case 4:
                        PCLaw.Diary.PhoneCallAction = PLConvert.PLDiary.eCallAction.Busy;
                        break;

                    case 5:
                        PCLaw.Diary.PhoneCallAction = PLConvert.PLDiary.eCallAction.VoiceMail;
                        break;

                }//end switch

                //did they call them back?
                if (!string.IsNullOrEmpty(Table.Rows[nPhone]["PhoneCallReturned"].ToString().Trim()))
                    PCLaw.Diary.PhoneCallReturnedCall = bool.Parse(Table.Rows[nPhone]["PhoneCallReturned"].ToString().Trim());
                else
                    PCLaw.Diary.PhoneCallReturnedCall = false;

                if (!string.IsNullOrEmpty(Table.Rows[nPhone]["IsUrgent"].ToString().Trim()))
                {
                    if (bool.Parse(Table.Rows[nPhone]["IsUrgent"].ToString().Trim()))
                        PCLaw.Diary.Priority = PLConvert.PLDiary.ePriority.High;
                    else
                        PCLaw.Diary.Priority = PLConvert.PLDiary.ePriority.Normal;
                }//end if
                else
                    PCLaw.Diary.Priority = PLConvert.PLDiary.ePriority.Normal;

                if (bool.Parse(Table.Rows[nPhone]["Completed"].ToString().Trim()))
                {
                    PCLaw.Diary.Completed = true;
                    PCLaw.Diary.CompletionDate = int.Parse(Table.Rows[nPhone]["CompletedDate"].ToString().Trim());
                }
                else
                    PCLaw.Diary.Completed = false;

                PCLaw.Diary.MatterID = PLMatter.GetIDFromExtID1(Table.Rows[nPhone]["MatterID"].ToString().Trim());
                if (PCLaw.Diary.MatterID == 0) //not found and is required....diary entry not added
                {
                    PCLaw.Diary.Clear();
                    continue;
                }//end if

                //add users to diary entry
                string[] users = Table.Rows[nPhone]["UserID"].ToString().Split('|');
                bool isMessageRead = true;
                bool hasUser = false;
                foreach (string user in users)
                {
                    nValue = PLUser.GetIDFromNN(user.Trim());
                    if (nValue != 0)
                    {
                        PCLaw.Diary.AddUser(nValue, isMessageRead);
                        hasUser = true;
                    }//end if
                }//end foreach

                //add contacts to diary entry
                string[] contacts = Table.Rows[nPhone]["ContactID"].ToString().Split('|');
                foreach (string contact in contacts)
                {
                    nValue = PLContact.GetIDFromExtID1(contact.Trim());
                    if (nValue != 0)
                        PCLaw.Diary.AddContact(nValue);
                }//end foreach


                if (!hasUser)//at least one user is required so if we have none, assign it to ADMIN
                {
                    nValue = PLUser.GetIDFromNN("ADMIN");
                    PCLaw.Diary.AddUser(nValue, isMessageRead);
                }//end if

                PCLaw.Diary.AddRecord();
            }//end for
            PCLaw.Diary.SendLast();
        }//end method

        private void addNotes()
        {
            int nNote = 0;
            int date = 0;
            int nValue = 0;

            DataTable Table = new DataTable("Note");
            string sSelect = "SELECT * FROM [Note]";
            ReadAMTable(ref Table, sSelect);

            for (nNote = 0; nNote < Table.Rows.Count; nNote++)
            {
                PCLaw.Diary.EntryType = PLConvert.PLDiary.eType.Notes;
                if (!string.IsNullOrEmpty(Table.Rows[nNote]["ShortDescription"].ToString().Trim()))
                    PCLaw.Diary.NoteShortDescription = Table.Rows[nNote]["ShortDescription"].ToString().Trim();
                else
                    PCLaw.Diary.NoteShortDescription = "General Note";
                if (!string.IsNullOrEmpty(Table.Rows[nNote]["Description"].ToString().Trim()))
                    PCLaw.Diary.Description = Table.Rows[nNote]["Description"].ToString().Trim();
                else
                    PCLaw.Diary.Description = "Converted Note - no memo text in original";
                PCLaw.Diary.MatterID = PLConvert.PLMatter.GetIDFromExtID1(Table.Rows[nNote]["MatterID"].ToString().Trim());
                if (PCLaw.Diary.MatterID == 0) //matter is required. if no matter, do not add this note
                {
                    PCLaw.Diary.Clear();
                    continue;
                }//end if

                date = int.Parse(Table.Rows[nNote]["EnteredDate"].ToString().Trim());
                if (date < 19820101)
                    date = 19820101;
                else
                    date = 20101231;
                PCLaw.Diary.EnteredDate = date;

                date = int.Parse(Table.Rows[nNote]["DueDate"].ToString().Trim());
                if (date < 19820101)
                    date = 19820101;
                else
                    date = 20101231; ;
                PCLaw.Diary.DueDate = date;

                date = int.Parse(Table.Rows[nNote]["ReminderDate"].ToString().Trim());
                if (date < 19820101)
                    date = 19820101;
                else
                    date = 20101231;
                PCLaw.Diary.ReminderDate = date;

                //add lawyers to diary entry
                string[] lawyers = Table.Rows[nNote]["LawyerIDs"].ToString().Split('|');
                foreach (string lawyer in lawyers)
                {
                    nValue = PLLawyer.GetIDFromExtID1(lawyer);
                    if (nValue != 0)
                        PCLaw.Diary.AddLawyer(nValue);
                }//end foreach

                //add contacts to diary entry
                string[] contacts = Table.Rows[nNote]["ContactIDs"].ToString().Split('|');
                foreach (string contact in contacts)
                {
                    if (int.Parse(Table.Rows[nNote]["ContactIDsAreClientIDs"].ToString()) == 1)
                        nValue = PLClient.GetContactIDFromClientID(int.Parse(contact));
                    nValue = PLContact.GetIDFromExtID1(nValue.ToString());
                    if (nValue != 0)
                        PCLaw.Diary.AddContact(nValue);
                }//end foreach

                //PCLaw.Diary.ExternalID_1 = Table.Rows[nContactCount]["OldID"].ToString().Trim();

                PCLaw.Diary.AddRecord();
            }//end for
            PCLaw.Diary.SendLast();
        }//end method
    }//end class
}//end namespace
