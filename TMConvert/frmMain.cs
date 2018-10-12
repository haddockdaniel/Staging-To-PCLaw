using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using PLConvert;
using System.Data.SqlClient;


namespace StagingConvert
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
        
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.buttonFirstStep = new System.Windows.Forms.Button();
            this.labelServer = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.labelDBName = new System.Windows.Forms.Label();
            this.checkBoxDemoOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxExistingData = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeClosedMatters = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonFirstStep
            // 
            this.buttonFirstStep.Location = new System.Drawing.Point(303, 199);
            this.buttonFirstStep.Name = "buttonFirstStep";
            this.buttonFirstStep.Size = new System.Drawing.Size(75, 23);
            this.buttonFirstStep.TabIndex = 0;
            this.buttonFirstStep.Text = "Next";
            this.buttonFirstStep.UseVisualStyleBackColor = true;
            this.buttonFirstStep.Click += new System.EventHandler(this.buttonFirstStep_Click);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(62, 46);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(62, 13);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "SQL Server";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(134, 39);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(235, 20);
            this.textBoxServer.TabIndex = 2;
            this.textBoxServer.Text = "localhost";
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(134, 85);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(235, 20);
            this.textBoxDBName.TabIndex = 4;
            this.textBoxDBName.Text = "PCLawStg";
            // 
            // labelDBName
            // 
            this.labelDBName.AutoSize = true;
            this.labelDBName.Location = new System.Drawing.Point(32, 92);
            this.labelDBName.Name = "labelDBName";
            this.labelDBName.Size = new System.Drawing.Size(92, 13);
            this.labelDBName.TabIndex = 3;
            this.labelDBName.Text = "Staging DB Name";
            // 
            // checkBoxDemoOnly
            // 
            this.checkBoxDemoOnly.AutoSize = true;
            this.checkBoxDemoOnly.Location = new System.Drawing.Point(134, 131);
            this.checkBoxDemoOnly.Name = "checkBoxDemoOnly";
            this.checkBoxDemoOnly.Size = new System.Drawing.Size(118, 17);
            this.checkBoxDemoOnly.TabIndex = 5;
            this.checkBoxDemoOnly.Text = "Demographics Only";
            this.checkBoxDemoOnly.UseVisualStyleBackColor = true;
            this.checkBoxDemoOnly.CheckedChanged += new System.EventHandler(this.checkBoxDemoOnly_CheckedChanged);
            // 
            // checkBoxExistingData
            // 
            this.checkBoxExistingData.AutoSize = true;
            this.checkBoxExistingData.Location = new System.Drawing.Point(134, 154);
            this.checkBoxExistingData.Name = "checkBoxExistingData";
            this.checkBoxExistingData.Size = new System.Drawing.Size(159, 17);
            this.checkBoxExistingData.TabIndex = 6;
            this.checkBoxExistingData.Text = "PCLaw books are not empty";
            this.checkBoxExistingData.UseVisualStyleBackColor = true;
            this.checkBoxExistingData.CheckedChanged += new System.EventHandler(this.checkBoxExistingData_CheckedChanged);
            // 
            // checkBoxIncludeClosedMatters
            // 
            this.checkBoxIncludeClosedMatters.AutoSize = true;
            this.checkBoxIncludeClosedMatters.Location = new System.Drawing.Point(134, 178);
            this.checkBoxIncludeClosedMatters.Name = "checkBoxIncludeClosedMatters";
            this.checkBoxIncludeClosedMatters.Size = new System.Drawing.Size(134, 17);
            this.checkBoxIncludeClosedMatters.TabIndex = 7;
            this.checkBoxIncludeClosedMatters.Text = "Include Closed Matters";
            this.checkBoxIncludeClosedMatters.UseVisualStyleBackColor = true;
            this.checkBoxIncludeClosedMatters.CheckedChanged += new System.EventHandler(this.checkBoxIncludeClosedMatters_CheckedChanged);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(403, 241);
            this.Controls.Add(this.checkBoxIncludeClosedMatters);
            this.Controls.Add(this.checkBoxExistingData);
            this.Controls.Add(this.checkBoxDemoOnly);
            this.Controls.Add(this.textBoxDBName);
            this.Controls.Add(this.labelDBName);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.buttonFirstStep);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "PCLaw Staging Conversion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

        private void frmMain_Load(object sender, System.EventArgs e)
        {
        
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            

        }


        
        private Button buttonFirstStep;
        private Label labelServer;
        private TextBox textBoxServer;
        private TextBox textBoxDBName;
        private Label labelDBName;
        private CheckBox checkBoxDemoOnly;
        private CheckBox checkBoxExistingData;
        
        private bool demoOnly = false;
        private CheckBox checkBoxIncludeClosedMatters;
        private bool existingData = false;
        private bool includeClosedMatters = false;

        private void buttonFirstStep_Click(object sender, EventArgs e)
        {
            //check sql credentials to make sure connection is sound
            if (textBoxServer.Text != "")
                if (textBoxDBName.Text != "")
                {
                    SupportOperations dbIO = new SupportOperations(textBoxServer.Text, textBoxDBName.Text);
                    if (!dbIO.testSQLConnection())
                        MessageBox.Show("There was an issue connecting with that information. Details:" + "\r\n" + dbIO.errorMessage,
                            "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (demoOnly)
                            runDemoForm(dbIO.connectionString);
                        else
                            runFinancialForm(dbIO.connectionString);
                    }
                }
                else
                    MessageBox.Show("Database Name box cannot be blank");
            else
                MessageBox.Show("SQL Server box cannot be blank");
        }

        private void checkBoxDemoOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDemoOnly.Checked == true)
                demoOnly = true;
            else
                demoOnly = false;
        }

        private void checkBoxExistingData_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExistingData.Checked == true)
                existingData = true;
            else
                existingData = false;
        }


        private void runDemoForm(string connString)
        {
            this.Hide();
            var form2 = new ProgressForm(connString, existingData, includeClosedMatters, false, false, false, false);
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }


        private void runFinancialForm(string connString)
        {
            this.Hide();
            var form2 = new FinancialConversionForm(connString, existingData, includeClosedMatters);
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void checkBoxIncludeClosedMatters_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncludeClosedMatters.Checked == true)
                includeClosedMatters = true;
            else
                includeClosedMatters = false;
        }

        


        



        
	}
}
