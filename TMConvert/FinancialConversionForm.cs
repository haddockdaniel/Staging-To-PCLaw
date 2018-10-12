using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StagingConvert
{
    public partial class FinancialConversionForm : Form
    {
        public FinancialConversionForm(string connString, bool existData, bool closedMaters)
        {
            InitializeComponent();
            connectionString = connString;
            existingData = existData;
            includeClosedMatters = closedMaters;
            Conversion = new ConversionControl(includeClosedMatters);
            if (ConversionControl.PCLaw != null)
                PCLaw = ConversionControl.PCLaw;
            else
                PCLaw = new PLConvert.PCLawConversion();
        }

        public PLConvert.PCLawConversion PCLaw;
        public ConversionControl Conversion;
        private string connectionString;
        private bool existingData;
        private bool includeClosedMatters;

        private void buttonFinanceContinue_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new ProgressForm(connectionString, existingData, includeClosedMatters, checkBoxWIPFees.Checked, checkBoxWIPExpenses.Checked, checkBoxAR.Checked, checkBoxTrust.Checked);
            form2.Closed += (s, args) => this.Close();
            form2.Show();

            //Conversion.DoConversion();
            // MessageBox.Show("Done");
        }
    }
}
