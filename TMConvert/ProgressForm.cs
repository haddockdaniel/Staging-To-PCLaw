using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace StagingConvert
{
    public partial class ProgressForm : Form
    {
        public ProgressForm(string connString, bool existingdata, bool inclClosedMatters, bool inclWIPfees, bool inclWIPexp, bool inclAR, bool inclTrust)
        {
            InitializeComponent();

            //variable assignments
            connectionString = connString;
            includeClosedMatters = inclClosedMatters;
            useExistingData = existingdata;
            Conversion = new ConversionControl(inclClosedMatters);
            if (ConversionControl.PCLaw != null)
                PCLaw = ConversionControl.PCLaw;
            else
                PCLaw = new PLConvert.PCLawConversion();
            label11.Visible = inclWIPfees;
            label12.Visible = inclWIPexp;
            label14.Visible = inclAR;
            label13.Visible = inclTrust;

            //get a list of all labels so we know what and how to process the items
            List<Label> tempList = this.Controls.OfType<Label>().ToList();
            List<Label> labelList = new List<Label>();
            foreach (Label c in tempList)
            {
                if (c.Visible != true)
                    labelList.Add(c);
            }
            SupportOperations supportOp = new SupportOperations();
            convObjList = supportOp.createConvObjects(labelList).ToList();

            //Control method calls
            beginConversion();
            finishConversion();
        }

        //variable delcarations
        public PLConvert.PCLawConversion PCLaw;
        public ConversionControl Conversion;
        private string connectionString;
        private bool useExistingData;
        private bool includeClosedMatters;
        public List<ConversionObject> convObjList;


        public void beginConversion()
        {
            
            //get total number of records to know progressbar max
            //make first control red
            //Start processing first items
            //move onto next item (clear progressbar and get total number of records)
            //make first item green and next item red


            //Conversion.DoConversion();
            // MessageBox.Show("Done");

            //int totalRows = dbIO.getNumberOfRows("inserttable");
           // progressBarTotal.Value = 0;
           // if (totalRows > 0)
           // {
             //   progressBarTotal.Maximum = totalRows;
            convObjList = convObjList.OrderBy(x => x.order).ToList();
            progressBarTotal.Value = 0;
            progressBarTotal.Maximum = convObjList.Count;
            ConversionControl demoConverter = new ConversionControl(includeClosedMatters);
            backgroundWorker1.RunWorkerAsync(demoConverter);
            //}
           // else
             //   runFinished();

        }


        private void finishConversion()
        {



        }

        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Conversion = e.Argument as ConversionControl;
           int current = 0;
            BackgroundWorker worker = sender as BackgroundWorker;
            

            foreach (ConversionObject co in convObjList)
            {
                try
                {
                    co.label.ForeColor = Color.Red;

                    Type type = typeof(ConversionControl);
                    MethodInfo info = type.GetMethod(co.method);
                    info.Invoke(Conversion, null);
                    current++;
                    backgroundWorker1.ReportProgress(current);
                    co.label.ForeColor = Color.Green;
                }
                catch (Exception ex4)
                { MessageBox.Show("Error: " + ex4.Message); }
            }


        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarTotal.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }


        private void caseForm_Close(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel? If so, the process" + "\r\n" + "will wait until the current case is finished processing" + "\r\n" + "to preserve your data integrity", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
                backgroundWorker1.CancelAsync();
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            PLConvert.PLLink.ShowLogFile();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {

        }//end method


    }
}
