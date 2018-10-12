using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using PLConvert;
using System.Data.SqlClient;


namespace AM7Convert
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPageWIPfees;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabSelections;
        private System.Windows.Forms.TabPage tabLawyers;
        private System.Windows.Forms.ListView lvPLLawyers;
        private System.Windows.Forms.ListView lvAMLawyers;
        private System.Windows.Forms.Button btnMatchLwr;
        private System.Windows.Forms.Button btnUnMatchLwr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUnMatchUsers;
        private System.Windows.Forms.Button btnMatchUsers;
        private System.Windows.Forms.ListView lvAMUsers;
        private System.Windows.Forms.ListView lvPLUsers;
        private System.Windows.Forms.TabPage tabFileType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvAMTypeOfLaw;
        private System.Windows.Forms.ListView lvPLTypeOfLaw;
        private System.Windows.Forms.Button btnUnMatchTypeOfLaw;
        private System.Windows.Forms.Button btnMatchTypeOfLaw;
        private System.Windows.Forms.TabPage tabTaskCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUnMatchTask;
        private System.Windows.Forms.Button btnMatchTask;
        private System.Windows.Forms.ListView lvAMTask;
        private System.Windows.Forms.ListView lvPLTask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListView lvAMGLAccts;
        private System.Windows.Forms.ListView lvPLGLAccts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.ListView listViewAMWIPfees;
        private System.Windows.Forms.ListView listViewPLWIPfees;
        private System.Windows.Forms.TabPage tabCTCTType;
        private System.Windows.Forms.TabPage tabDiaryCode;
        private System.Windows.Forms.Button btnUnMatchCTCTType;
        private System.Windows.Forms.Button btnMatchCTCTType;
        private System.Windows.Forms.ListView lvAMCTCTType;
        private System.Windows.Forms.ListView lvPLCTCTType;
        private System.Windows.Forms.Button btnUnMatchDiaryCode;
        private System.Windows.Forms.Button btnMatchDiaryCode;
        private System.Windows.Forms.ListView lvAMDiaryCode;
        private System.Windows.Forms.ListView lvPLDiaryCode;
        private System.Windows.Forms.CheckBox chkPLMoreUpToDate;
        private System.Windows.Forms.CheckBox chkActiveMatters;
        private System.Windows.Forms.CheckBox chkArchivedMatters;
        private System.Windows.Forms.CheckBox chkCombineCli_Mat;
        private System.Windows.Forms.CheckBox chkContacts;
        private System.Windows.Forms.CheckBox chkAppointments;
        private System.Windows.Forms.CheckBox chkTimeFees;
        private System.Windows.Forms.CheckBox chkSpecialMatters;
        private System.Windows.Forms.Button btnOK;
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
            Conversion = new AMConversion();
            if (AMConversion.PCLaw != null)
                PCLaw = AMConversion.PCLaw;
            else
                PCLaw = new  PLConvert.PCLawConversion();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSelections = new System.Windows.Forms.TabPage();
            this.chkTimeFees = new System.Windows.Forms.CheckBox();
            this.chkAppointments = new System.Windows.Forms.CheckBox();
            this.chkContacts = new System.Windows.Forms.CheckBox();
            this.chkCombineCli_Mat = new System.Windows.Forms.CheckBox();
            this.chkSpecialMatters = new System.Windows.Forms.CheckBox();
            this.chkArchivedMatters = new System.Windows.Forms.CheckBox();
            this.chkActiveMatters = new System.Windows.Forms.CheckBox();
            this.chkPLMoreUpToDate = new System.Windows.Forms.CheckBox();
            this.tabLawyers = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUnMatchLwr = new System.Windows.Forms.Button();
            this.btnMatchLwr = new System.Windows.Forms.Button();
            this.lvAMLawyers = new System.Windows.Forms.ListView();
            this.lvPLLawyers = new System.Windows.Forms.ListView();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUnMatchUsers = new System.Windows.Forms.Button();
            this.btnMatchUsers = new System.Windows.Forms.Button();
            this.lvAMUsers = new System.Windows.Forms.ListView();
            this.lvPLUsers = new System.Windows.Forms.ListView();
            this.tabFileType = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUnMatchTypeOfLaw = new System.Windows.Forms.Button();
            this.btnMatchTypeOfLaw = new System.Windows.Forms.Button();
            this.lvAMTypeOfLaw = new System.Windows.Forms.ListView();
            this.lvPLTypeOfLaw = new System.Windows.Forms.ListView();
            this.tabTaskCode = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUnMatchTask = new System.Windows.Forms.Button();
            this.btnMatchTask = new System.Windows.Forms.Button();
            this.lvAMTask = new System.Windows.Forms.ListView();
            this.lvPLTask = new System.Windows.Forms.ListView();
            this.tabCTCTType = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUnMatchCTCTType = new System.Windows.Forms.Button();
            this.btnMatchCTCTType = new System.Windows.Forms.Button();
            this.lvAMCTCTType = new System.Windows.Forms.ListView();
            this.lvPLCTCTType = new System.Windows.Forms.ListView();
            this.tabDiaryCode = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUnMatchDiaryCode = new System.Windows.Forms.Button();
            this.btnMatchDiaryCode = new System.Windows.Forms.Button();
            this.lvAMDiaryCode = new System.Windows.Forms.ListView();
            this.lvPLDiaryCode = new System.Windows.Forms.ListView();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.lvAMGLAccts = new System.Windows.Forms.ListView();
            this.lvPLGLAccts = new System.Windows.Forms.ListView();
            this.tabPageWIPfees = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.listViewAMWIPfees = new System.Windows.Forms.ListView();
            this.listViewPLWIPfees = new System.Windows.Forms.ListView();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabSelections.SuspendLayout();
            this.tabLawyers.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.tabFileType.SuspendLayout();
            this.tabTaskCode.SuspendLayout();
            this.tabCTCTType.SuspendLayout();
            this.tabDiaryCode.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPageWIPfees.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSelections);
            this.tabControl1.Controls.Add(this.tabLawyers);
            this.tabControl1.Controls.Add(this.tabUsers);
            this.tabControl1.Controls.Add(this.tabFileType);
            this.tabControl1.Controls.Add(this.tabTaskCode);
            this.tabControl1.Controls.Add(this.tabCTCTType);
            this.tabControl1.Controls.Add(this.tabDiaryCode);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPageWIPfees);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Location = new System.Drawing.Point(24, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(728, 424);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabSelections
            // 
            this.tabSelections.Controls.Add(this.chkTimeFees);
            this.tabSelections.Controls.Add(this.chkAppointments);
            this.tabSelections.Controls.Add(this.chkContacts);
            this.tabSelections.Controls.Add(this.chkCombineCli_Mat);
            this.tabSelections.Controls.Add(this.chkSpecialMatters);
            this.tabSelections.Controls.Add(this.chkArchivedMatters);
            this.tabSelections.Controls.Add(this.chkActiveMatters);
            this.tabSelections.Controls.Add(this.chkPLMoreUpToDate);
            this.tabSelections.Location = new System.Drawing.Point(4, 22);
            this.tabSelections.Name = "tabSelections";
            this.tabSelections.Size = new System.Drawing.Size(720, 398);
            this.tabSelections.TabIndex = 0;
            this.tabSelections.Text = "Selections";
            this.tabSelections.Click += new System.EventHandler(this.tabSelections_Click);
            // 
            // chkTimeFees
            // 
            this.chkTimeFees.Checked = true;
            this.chkTimeFees.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeFees.Location = new System.Drawing.Point(40, 248);
            this.chkTimeFees.Name = "chkTimeFees";
            this.chkTimeFees.Size = new System.Drawing.Size(416, 16);
            this.chkTimeFees.TabIndex = 7;
            this.chkTimeFees.Text = "Import Time and Feea";
            // 
            // chkAppointments
            // 
            this.chkAppointments.Checked = true;
            this.chkAppointments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAppointments.Location = new System.Drawing.Point(40, 216);
            this.chkAppointments.Name = "chkAppointments";
            this.chkAppointments.Size = new System.Drawing.Size(416, 16);
            this.chkAppointments.TabIndex = 6;
            this.chkAppointments.Text = "Import Appointments";
            // 
            // chkContacts
            // 
            this.chkContacts.Checked = true;
            this.chkContacts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContacts.Location = new System.Drawing.Point(40, 184);
            this.chkContacts.Name = "chkContacts";
            this.chkContacts.Size = new System.Drawing.Size(416, 16);
            this.chkContacts.TabIndex = 5;
            this.chkContacts.Text = "Import Contacts";
            // 
            // chkCombineCli_Mat
            // 
            this.chkCombineCli_Mat.Location = new System.Drawing.Point(40, 152);
            this.chkCombineCli_Mat.Name = "chkCombineCli_Mat";
            this.chkCombineCli_Mat.Size = new System.Drawing.Size(416, 16);
            this.chkCombineCli_Mat.TabIndex = 4;
            this.chkCombineCli_Mat.Text = "Combine Client and Matter to Form Matter Nickname";
            // 
            // chkSpecialMatters
            // 
            this.chkSpecialMatters.Checked = true;
            this.chkSpecialMatters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpecialMatters.Location = new System.Drawing.Point(40, 120);
            this.chkSpecialMatters.Name = "chkSpecialMatters";
            this.chkSpecialMatters.Size = new System.Drawing.Size(416, 16);
            this.chkSpecialMatters.TabIndex = 3;
            this.chkSpecialMatters.Text = "Import Amicus Special Matters";
            // 
            // chkArchivedMatters
            // 
            this.chkArchivedMatters.Checked = true;
            this.chkArchivedMatters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArchivedMatters.Location = new System.Drawing.Point(40, 88);
            this.chkArchivedMatters.Name = "chkArchivedMatters";
            this.chkArchivedMatters.Size = new System.Drawing.Size(416, 16);
            this.chkArchivedMatters.TabIndex = 2;
            this.chkArchivedMatters.Text = "Import Archived Matters";
            // 
            // chkActiveMatters
            // 
            this.chkActiveMatters.Checked = true;
            this.chkActiveMatters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActiveMatters.Location = new System.Drawing.Point(40, 56);
            this.chkActiveMatters.Name = "chkActiveMatters";
            this.chkActiveMatters.Size = new System.Drawing.Size(416, 16);
            this.chkActiveMatters.TabIndex = 1;
            this.chkActiveMatters.Text = "Import Active Matters";
            // 
            // chkPLMoreUpToDate
            // 
            this.chkPLMoreUpToDate.Checked = true;
            this.chkPLMoreUpToDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPLMoreUpToDate.Location = new System.Drawing.Point(40, 24);
            this.chkPLMoreUpToDate.Name = "chkPLMoreUpToDate";
            this.chkPLMoreUpToDate.Size = new System.Drawing.Size(416, 16);
            this.chkPLMoreUpToDate.TabIndex = 0;
            this.chkPLMoreUpToDate.Text = "PCLaw Data is More UP to Date";
            // 
            // tabLawyers
            // 
            this.tabLawyers.Controls.Add(this.label1);
            this.tabLawyers.Controls.Add(this.btnUnMatchLwr);
            this.tabLawyers.Controls.Add(this.btnMatchLwr);
            this.tabLawyers.Controls.Add(this.lvAMLawyers);
            this.tabLawyers.Controls.Add(this.lvPLLawyers);
            this.tabLawyers.Location = new System.Drawing.Point(4, 22);
            this.tabLawyers.Name = "tabLawyers";
            this.tabLawyers.Size = new System.Drawing.Size(720, 398);
            this.tabLawyers.TabIndex = 1;
            this.tabLawyers.Text = "Lawyers";
            this.tabLawyers.Click += new System.EventHandler(this.tabLawyers_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(648, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "  Amicus Lawyers                                                                 " +
    "                                              PCLaw Timekeepers";
            // 
            // btnUnMatchLwr
            // 
            this.btnUnMatchLwr.Location = new System.Drawing.Point(376, 336);
            this.btnUnMatchLwr.Name = "btnUnMatchLwr";
            this.btnUnMatchLwr.Size = new System.Drawing.Size(128, 32);
            this.btnUnMatchLwr.TabIndex = 3;
            this.btnUnMatchLwr.Text = "UnMatch";
            this.btnUnMatchLwr.Click += new System.EventHandler(this.btnUnMatchLwr_Click);
            // 
            // btnMatchLwr
            // 
            this.btnMatchLwr.Location = new System.Drawing.Point(208, 336);
            this.btnMatchLwr.Name = "btnMatchLwr";
            this.btnMatchLwr.Size = new System.Drawing.Size(136, 32);
            this.btnMatchLwr.TabIndex = 2;
            this.btnMatchLwr.Text = "Match";
            this.btnMatchLwr.Click += new System.EventHandler(this.btnMatchLwr_Click);
            // 
            // lvAMLawyers
            // 
            this.lvAMLawyers.FullRowSelect = true;
            this.lvAMLawyers.GridLines = true;
            this.lvAMLawyers.HideSelection = false;
            this.lvAMLawyers.Location = new System.Drawing.Point(48, 40);
            this.lvAMLawyers.Name = "lvAMLawyers";
            this.lvAMLawyers.Size = new System.Drawing.Size(384, 280);
            this.lvAMLawyers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMLawyers.TabIndex = 1;
            this.lvAMLawyers.UseCompatibleStateImageBehavior = false;
            this.lvAMLawyers.View = System.Windows.Forms.View.Details;
            this.lvAMLawyers.SelectedIndexChanged += new System.EventHandler(this.lvAMLawyers_SelectedIndexChanged);
            this.lvAMLawyers.DoubleClick += new System.EventHandler(this.lvAMLawyers_DoubleClick);
            // 
            // lvPLLawyers
            // 
            this.lvPLLawyers.FullRowSelect = true;
            this.lvPLLawyers.GridLines = true;
            this.lvPLLawyers.HideSelection = false;
            this.lvPLLawyers.Location = new System.Drawing.Point(456, 40);
            this.lvPLLawyers.MultiSelect = false;
            this.lvPLLawyers.Name = "lvPLLawyers";
            this.lvPLLawyers.Size = new System.Drawing.Size(224, 280);
            this.lvPLLawyers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLLawyers.TabIndex = 0;
            this.lvPLLawyers.UseCompatibleStateImageBehavior = false;
            this.lvPLLawyers.View = System.Windows.Forms.View.Details;
            this.lvPLLawyers.SelectedIndexChanged += new System.EventHandler(this.lvPLLawyers_SelectedIndexChanged);
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.label2);
            this.tabUsers.Controls.Add(this.btnUnMatchUsers);
            this.tabUsers.Controls.Add(this.btnMatchUsers);
            this.tabUsers.Controls.Add(this.lvAMUsers);
            this.tabUsers.Controls.Add(this.lvPLUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(720, 398);
            this.tabUsers.TabIndex = 2;
            this.tabUsers.Text = "Users";
            this.tabUsers.Click += new System.EventHandler(this.tabUsers_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(36, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(648, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "  Amicus Users                                                                   " +
    "                                              PCLaw Users";
            // 
            // btnUnMatchUsers
            // 
            this.btnUnMatchUsers.Location = new System.Drawing.Point(372, 336);
            this.btnUnMatchUsers.Name = "btnUnMatchUsers";
            this.btnUnMatchUsers.Size = new System.Drawing.Size(136, 32);
            this.btnUnMatchUsers.TabIndex = 8;
            this.btnUnMatchUsers.Text = "UnMatch";
            this.btnUnMatchUsers.Click += new System.EventHandler(this.btnUnMatchUsers_Click);
            // 
            // btnMatchUsers
            // 
            this.btnMatchUsers.Location = new System.Drawing.Point(204, 336);
            this.btnMatchUsers.Name = "btnMatchUsers";
            this.btnMatchUsers.Size = new System.Drawing.Size(144, 32);
            this.btnMatchUsers.TabIndex = 7;
            this.btnMatchUsers.Text = "Match";
            this.btnMatchUsers.Click += new System.EventHandler(this.btnMatchUsers_Click);
            // 
            // lvAMUsers
            // 
            this.lvAMUsers.FullRowSelect = true;
            this.lvAMUsers.GridLines = true;
            this.lvAMUsers.HideSelection = false;
            this.lvAMUsers.Location = new System.Drawing.Point(48, 40);
            this.lvAMUsers.Name = "lvAMUsers";
            this.lvAMUsers.Size = new System.Drawing.Size(384, 280);
            this.lvAMUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMUsers.TabIndex = 6;
            this.lvAMUsers.UseCompatibleStateImageBehavior = false;
            this.lvAMUsers.View = System.Windows.Forms.View.Details;
            // 
            // lvPLUsers
            // 
            this.lvPLUsers.FullRowSelect = true;
            this.lvPLUsers.GridLines = true;
            this.lvPLUsers.HideSelection = false;
            this.lvPLUsers.Location = new System.Drawing.Point(456, 40);
            this.lvPLUsers.MultiSelect = false;
            this.lvPLUsers.Name = "lvPLUsers";
            this.lvPLUsers.Size = new System.Drawing.Size(224, 280);
            this.lvPLUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLUsers.TabIndex = 5;
            this.lvPLUsers.UseCompatibleStateImageBehavior = false;
            this.lvPLUsers.View = System.Windows.Forms.View.Details;
            // 
            // tabFileType
            // 
            this.tabFileType.Controls.Add(this.label3);
            this.tabFileType.Controls.Add(this.btnUnMatchTypeOfLaw);
            this.tabFileType.Controls.Add(this.btnMatchTypeOfLaw);
            this.tabFileType.Controls.Add(this.lvAMTypeOfLaw);
            this.tabFileType.Controls.Add(this.lvPLTypeOfLaw);
            this.tabFileType.Location = new System.Drawing.Point(4, 22);
            this.tabFileType.Name = "tabFileType";
            this.tabFileType.Size = new System.Drawing.Size(720, 398);
            this.tabFileType.TabIndex = 3;
            this.tabFileType.Text = "Law Types";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(36, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(648, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "  Amicus File Type                                                               " +
    "                                                PCLaw Type of Law";
            // 
            // btnUnMatchTypeOfLaw
            // 
            this.btnUnMatchTypeOfLaw.Location = new System.Drawing.Point(372, 336);
            this.btnUnMatchTypeOfLaw.Name = "btnUnMatchTypeOfLaw";
            this.btnUnMatchTypeOfLaw.Size = new System.Drawing.Size(136, 32);
            this.btnUnMatchTypeOfLaw.TabIndex = 13;
            this.btnUnMatchTypeOfLaw.Text = "UnMatch";
            this.btnUnMatchTypeOfLaw.Click += new System.EventHandler(this.btnUnMatchTypeOfLaw_Click);
            // 
            // btnMatchTypeOfLaw
            // 
            this.btnMatchTypeOfLaw.Location = new System.Drawing.Point(204, 336);
            this.btnMatchTypeOfLaw.Name = "btnMatchTypeOfLaw";
            this.btnMatchTypeOfLaw.Size = new System.Drawing.Size(144, 32);
            this.btnMatchTypeOfLaw.TabIndex = 12;
            this.btnMatchTypeOfLaw.Text = "Match";
            this.btnMatchTypeOfLaw.Click += new System.EventHandler(this.btnMatchTypeOfLaw_Click);
            // 
            // lvAMTypeOfLaw
            // 
            this.lvAMTypeOfLaw.FullRowSelect = true;
            this.lvAMTypeOfLaw.GridLines = true;
            this.lvAMTypeOfLaw.HideSelection = false;
            this.lvAMTypeOfLaw.Location = new System.Drawing.Point(48, 40);
            this.lvAMTypeOfLaw.Name = "lvAMTypeOfLaw";
            this.lvAMTypeOfLaw.Size = new System.Drawing.Size(384, 280);
            this.lvAMTypeOfLaw.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMTypeOfLaw.TabIndex = 11;
            this.lvAMTypeOfLaw.UseCompatibleStateImageBehavior = false;
            this.lvAMTypeOfLaw.View = System.Windows.Forms.View.Details;
            // 
            // lvPLTypeOfLaw
            // 
            this.lvPLTypeOfLaw.FullRowSelect = true;
            this.lvPLTypeOfLaw.GridLines = true;
            this.lvPLTypeOfLaw.HideSelection = false;
            this.lvPLTypeOfLaw.Location = new System.Drawing.Point(456, 40);
            this.lvPLTypeOfLaw.MultiSelect = false;
            this.lvPLTypeOfLaw.Name = "lvPLTypeOfLaw";
            this.lvPLTypeOfLaw.Size = new System.Drawing.Size(224, 280);
            this.lvPLTypeOfLaw.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLTypeOfLaw.TabIndex = 10;
            this.lvPLTypeOfLaw.UseCompatibleStateImageBehavior = false;
            this.lvPLTypeOfLaw.View = System.Windows.Forms.View.Details;
            // 
            // tabTaskCode
            // 
            this.tabTaskCode.Controls.Add(this.label4);
            this.tabTaskCode.Controls.Add(this.btnUnMatchTask);
            this.tabTaskCode.Controls.Add(this.btnMatchTask);
            this.tabTaskCode.Controls.Add(this.lvAMTask);
            this.tabTaskCode.Controls.Add(this.lvPLTask);
            this.tabTaskCode.Location = new System.Drawing.Point(4, 22);
            this.tabTaskCode.Name = "tabTaskCode";
            this.tabTaskCode.Size = new System.Drawing.Size(720, 398);
            this.tabTaskCode.TabIndex = 4;
            this.tabTaskCode.Text = "Task Codes";
            this.tabTaskCode.Click += new System.EventHandler(this.tabTaskCode_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(36, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(648, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "  Amicus Task                                                                    " +
    "                                              PCLaw Task";
            // 
            // btnUnMatchTask
            // 
            this.btnUnMatchTask.Location = new System.Drawing.Point(372, 336);
            this.btnUnMatchTask.Name = "btnUnMatchTask";
            this.btnUnMatchTask.Size = new System.Drawing.Size(136, 32);
            this.btnUnMatchTask.TabIndex = 18;
            this.btnUnMatchTask.Text = "UnMatch";
            this.btnUnMatchTask.Click += new System.EventHandler(this.btnUnMatchTask_Click);
            // 
            // btnMatchTask
            // 
            this.btnMatchTask.Location = new System.Drawing.Point(204, 336);
            this.btnMatchTask.Name = "btnMatchTask";
            this.btnMatchTask.Size = new System.Drawing.Size(144, 32);
            this.btnMatchTask.TabIndex = 17;
            this.btnMatchTask.Text = "Match";
            this.btnMatchTask.Click += new System.EventHandler(this.btnMatchTask_Click);
            // 
            // lvAMTask
            // 
            this.lvAMTask.FullRowSelect = true;
            this.lvAMTask.GridLines = true;
            this.lvAMTask.HideSelection = false;
            this.lvAMTask.Location = new System.Drawing.Point(48, 40);
            this.lvAMTask.Name = "lvAMTask";
            this.lvAMTask.Size = new System.Drawing.Size(384, 280);
            this.lvAMTask.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMTask.TabIndex = 16;
            this.lvAMTask.UseCompatibleStateImageBehavior = false;
            this.lvAMTask.View = System.Windows.Forms.View.Details;
            // 
            // lvPLTask
            // 
            this.lvPLTask.FullRowSelect = true;
            this.lvPLTask.GridLines = true;
            this.lvPLTask.HideSelection = false;
            this.lvPLTask.Location = new System.Drawing.Point(456, 40);
            this.lvPLTask.MultiSelect = false;
            this.lvPLTask.Name = "lvPLTask";
            this.lvPLTask.Size = new System.Drawing.Size(224, 280);
            this.lvPLTask.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLTask.TabIndex = 15;
            this.lvPLTask.UseCompatibleStateImageBehavior = false;
            this.lvPLTask.View = System.Windows.Forms.View.Details;
            this.lvPLTask.SelectedIndexChanged += new System.EventHandler(this.lvPLTask_SelectedIndexChanged);
            // 
            // tabCTCTType
            // 
            this.tabCTCTType.Controls.Add(this.label5);
            this.tabCTCTType.Controls.Add(this.btnUnMatchCTCTType);
            this.tabCTCTType.Controls.Add(this.btnMatchCTCTType);
            this.tabCTCTType.Controls.Add(this.lvAMCTCTType);
            this.tabCTCTType.Controls.Add(this.lvPLCTCTType);
            this.tabCTCTType.Location = new System.Drawing.Point(4, 22);
            this.tabCTCTType.Name = "tabCTCTType";
            this.tabCTCTType.Size = new System.Drawing.Size(720, 398);
            this.tabCTCTType.TabIndex = 5;
            this.tabCTCTType.Text = "Contact Types";
            this.tabCTCTType.Click += new System.EventHandler(this.tabCTCTType_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(36, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(648, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "  Amicus Role                                                                    " +
    "                                                 PCLaw Contact Type";
            // 
            // btnUnMatchCTCTType
            // 
            this.btnUnMatchCTCTType.Location = new System.Drawing.Point(372, 336);
            this.btnUnMatchCTCTType.Name = "btnUnMatchCTCTType";
            this.btnUnMatchCTCTType.Size = new System.Drawing.Size(136, 32);
            this.btnUnMatchCTCTType.TabIndex = 18;
            this.btnUnMatchCTCTType.Text = "UnMatch";
            this.btnUnMatchCTCTType.Click += new System.EventHandler(this.btnUnMatchCTCTType_Click);
            // 
            // btnMatchCTCTType
            // 
            this.btnMatchCTCTType.Location = new System.Drawing.Point(204, 336);
            this.btnMatchCTCTType.Name = "btnMatchCTCTType";
            this.btnMatchCTCTType.Size = new System.Drawing.Size(144, 32);
            this.btnMatchCTCTType.TabIndex = 17;
            this.btnMatchCTCTType.Text = "Match";
            this.btnMatchCTCTType.Click += new System.EventHandler(this.btnMatchCTCTType_Click);
            // 
            // lvAMCTCTType
            // 
            this.lvAMCTCTType.FullRowSelect = true;
            this.lvAMCTCTType.GridLines = true;
            this.lvAMCTCTType.HideSelection = false;
            this.lvAMCTCTType.Location = new System.Drawing.Point(48, 40);
            this.lvAMCTCTType.Name = "lvAMCTCTType";
            this.lvAMCTCTType.Size = new System.Drawing.Size(384, 280);
            this.lvAMCTCTType.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMCTCTType.TabIndex = 16;
            this.lvAMCTCTType.UseCompatibleStateImageBehavior = false;
            this.lvAMCTCTType.View = System.Windows.Forms.View.Details;
            // 
            // lvPLCTCTType
            // 
            this.lvPLCTCTType.FullRowSelect = true;
            this.lvPLCTCTType.GridLines = true;
            this.lvPLCTCTType.HideSelection = false;
            this.lvPLCTCTType.Location = new System.Drawing.Point(456, 40);
            this.lvPLCTCTType.MultiSelect = false;
            this.lvPLCTCTType.Name = "lvPLCTCTType";
            this.lvPLCTCTType.Size = new System.Drawing.Size(224, 280);
            this.lvPLCTCTType.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLCTCTType.TabIndex = 15;
            this.lvPLCTCTType.UseCompatibleStateImageBehavior = false;
            this.lvPLCTCTType.View = System.Windows.Forms.View.Details;
            // 
            // tabDiaryCode
            // 
            this.tabDiaryCode.Controls.Add(this.label6);
            this.tabDiaryCode.Controls.Add(this.btnUnMatchDiaryCode);
            this.tabDiaryCode.Controls.Add(this.btnMatchDiaryCode);
            this.tabDiaryCode.Controls.Add(this.lvAMDiaryCode);
            this.tabDiaryCode.Controls.Add(this.lvPLDiaryCode);
            this.tabDiaryCode.Location = new System.Drawing.Point(4, 22);
            this.tabDiaryCode.Name = "tabDiaryCode";
            this.tabDiaryCode.Size = new System.Drawing.Size(720, 398);
            this.tabDiaryCode.TabIndex = 6;
            this.tabDiaryCode.Text = "Diary Codes";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(36, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(648, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "  Amicus Case Type                                                               " +
    "                                              PCLaw Type of Law";
            // 
            // btnUnMatchDiaryCode
            // 
            this.btnUnMatchDiaryCode.Location = new System.Drawing.Point(372, 328);
            this.btnUnMatchDiaryCode.Name = "btnUnMatchDiaryCode";
            this.btnUnMatchDiaryCode.Size = new System.Drawing.Size(136, 32);
            this.btnUnMatchDiaryCode.TabIndex = 18;
            this.btnUnMatchDiaryCode.Text = "UnMatch";
            // 
            // btnMatchDiaryCode
            // 
            this.btnMatchDiaryCode.Location = new System.Drawing.Point(204, 328);
            this.btnMatchDiaryCode.Name = "btnMatchDiaryCode";
            this.btnMatchDiaryCode.Size = new System.Drawing.Size(144, 32);
            this.btnMatchDiaryCode.TabIndex = 17;
            this.btnMatchDiaryCode.Text = "Match";
            // 
            // lvAMDiaryCode
            // 
            this.lvAMDiaryCode.FullRowSelect = true;
            this.lvAMDiaryCode.GridLines = true;
            this.lvAMDiaryCode.HideSelection = false;
            this.lvAMDiaryCode.Location = new System.Drawing.Point(48, 40);
            this.lvAMDiaryCode.Name = "lvAMDiaryCode";
            this.lvAMDiaryCode.Size = new System.Drawing.Size(384, 265);
            this.lvAMDiaryCode.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMDiaryCode.TabIndex = 16;
            this.lvAMDiaryCode.UseCompatibleStateImageBehavior = false;
            this.lvAMDiaryCode.View = System.Windows.Forms.View.Details;
            // 
            // lvPLDiaryCode
            // 
            this.lvPLDiaryCode.FullRowSelect = true;
            this.lvPLDiaryCode.GridLines = true;
            this.lvPLDiaryCode.HideSelection = false;
            this.lvPLDiaryCode.Location = new System.Drawing.Point(456, 40);
            this.lvPLDiaryCode.MultiSelect = false;
            this.lvPLDiaryCode.Name = "lvPLDiaryCode";
            this.lvPLDiaryCode.Size = new System.Drawing.Size(224, 264);
            this.lvPLDiaryCode.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLDiaryCode.TabIndex = 15;
            this.lvPLDiaryCode.UseCompatibleStateImageBehavior = false;
            this.lvPLDiaryCode.View = System.Windows.Forms.View.Details;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.label7);
            this.tabPage8.Controls.Add(this.button7);
            this.tabPage8.Controls.Add(this.button8);
            this.tabPage8.Controls.Add(this.lvAMGLAccts);
            this.tabPage8.Controls.Add(this.lvPLGLAccts);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(720, 398);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "GLAccounts";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(36, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(648, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "  Amicus GL Type                                                                 " +
    "                                            PCLaw GL Type";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(372, 287);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(136, 32);
            this.button7.TabIndex = 18;
            this.button7.Text = "UnMatch";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(204, 287);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(144, 32);
            this.button8.TabIndex = 17;
            this.button8.Text = "Match";
            // 
            // lvAMGLAccts
            // 
            this.lvAMGLAccts.FullRowSelect = true;
            this.lvAMGLAccts.GridLines = true;
            this.lvAMGLAccts.HideSelection = false;
            this.lvAMGLAccts.Location = new System.Drawing.Point(44, 39);
            this.lvAMGLAccts.Name = "lvAMGLAccts";
            this.lvAMGLAccts.Size = new System.Drawing.Size(384, 224);
            this.lvAMGLAccts.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAMGLAccts.TabIndex = 16;
            this.lvAMGLAccts.UseCompatibleStateImageBehavior = false;
            this.lvAMGLAccts.View = System.Windows.Forms.View.Details;
            // 
            // lvPLGLAccts
            // 
            this.lvPLGLAccts.FullRowSelect = true;
            this.lvPLGLAccts.GridLines = true;
            this.lvPLGLAccts.HideSelection = false;
            this.lvPLGLAccts.Location = new System.Drawing.Point(456, 40);
            this.lvPLGLAccts.MultiSelect = false;
            this.lvPLGLAccts.Name = "lvPLGLAccts";
            this.lvPLGLAccts.Size = new System.Drawing.Size(224, 224);
            this.lvPLGLAccts.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPLGLAccts.TabIndex = 15;
            this.lvPLGLAccts.UseCompatibleStateImageBehavior = false;
            this.lvPLGLAccts.View = System.Windows.Forms.View.Details;
            // 
            // tabPageWIPfees
            // 
            this.tabPageWIPfees.Controls.Add(this.label8);
            this.tabPageWIPfees.Controls.Add(this.button9);
            this.tabPageWIPfees.Controls.Add(this.button10);
            this.tabPageWIPfees.Controls.Add(this.listViewAMWIPfees);
            this.tabPageWIPfees.Controls.Add(this.listViewPLWIPfees);
            this.tabPageWIPfees.Location = new System.Drawing.Point(4, 22);
            this.tabPageWIPfees.Name = "tabPageWIPfees";
            this.tabPageWIPfees.Size = new System.Drawing.Size(720, 398);
            this.tabPageWIPfees.TabIndex = 8;
            this.tabPageWIPfees.Text = "WIP Fees";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(648, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "  Amicus File Type                                                               " +
    "                                              PCLaw Type of Law";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(368, 288);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(136, 32);
            this.button9.TabIndex = 18;
            this.button9.Text = "UnMatch";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(200, 288);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(144, 32);
            this.button10.TabIndex = 17;
            this.button10.Text = "Match";
            // 
            // listViewAMWIPfees
            // 
            this.listViewAMWIPfees.FullRowSelect = true;
            this.listViewAMWIPfees.GridLines = true;
            this.listViewAMWIPfees.HideSelection = false;
            this.listViewAMWIPfees.Location = new System.Drawing.Point(40, 40);
            this.listViewAMWIPfees.Name = "listViewAMWIPfees";
            this.listViewAMWIPfees.Size = new System.Drawing.Size(384, 224);
            this.listViewAMWIPfees.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewAMWIPfees.TabIndex = 16;
            this.listViewAMWIPfees.UseCompatibleStateImageBehavior = false;
            this.listViewAMWIPfees.View = System.Windows.Forms.View.Details;
            // 
            // listViewPLWIPfees
            // 
            this.listViewPLWIPfees.FullRowSelect = true;
            this.listViewPLWIPfees.GridLines = true;
            this.listViewPLWIPfees.HideSelection = false;
            this.listViewPLWIPfees.Location = new System.Drawing.Point(448, 40);
            this.listViewPLWIPfees.MultiSelect = false;
            this.listViewPLWIPfees.Name = "listViewPLWIPfees";
            this.listViewPLWIPfees.Size = new System.Drawing.Size(224, 224);
            this.listViewPLWIPfees.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewPLWIPfees.TabIndex = 15;
            this.listViewPLWIPfees.UseCompatibleStateImageBehavior = false;
            this.listViewPLWIPfees.View = System.Windows.Forms.View.Details;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(720, 398);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "tabPage10";
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(720, 398);
            this.tabPage11.TabIndex = 10;
            this.tabPage11.Text = "tabPage11";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(768, 80);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 40);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(864, 478);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Amicus 7 Converison";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSelections.ResumeLayout(false);
            this.tabLawyers.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabFileType.ResumeLayout(false);
            this.tabTaskCode.ResumeLayout(false);
            this.tabCTCTType.ResumeLayout(false);
            this.tabDiaryCode.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPageWIPfees.ResumeLayout(false);
            this.ResumeLayout(false);

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

        private void lvPLLawyers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }

        private void tabLawyers_Click(object sender, System.EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.tabControl1.Name = "Tabctl1";
            string[] strPL  = {"", "", ""};
            string[] strAM = {"", "", "", ""};
            DataTable Table;
            string sSelect;
            string sName;
            char cTest;
            string sTest;
            
            

            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    if (lvPLLawyers.Columns.Count > 0)
                        break;

                    lvPLLawyers.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLLawyers.Columns.Add("NickName",  50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLLawyers.Columns.Add("ID",        50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMLawyers.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMLawyers.Columns.Add("Match",    150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMLawyers.Columns.Add("AMID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMLawyers.Columns.Add("PLID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    
                    strPL[0]  =   "***Add To PCLaw***";
                    ListViewItem itm = new ListViewItem(strPL);
                    lvPLLawyers.Items.Add(itm);

                    strPL[0]  =   "***Inactive TimeKeepers***";
                    strPL[1]  =   "IT~";
                    strPL[2]  =    "";
                    itm = new ListViewItem(strPL);
                    lvPLLawyers.Items.Add(itm);

                     
                    while (PCLaw.Lawyer.GetNextRecord() == 0)
                    {
                        if (PCLaw.Lawyer.Status.Equals(PLConvert.PLLawyer.eSTATUS.ACTIVE) == false)
                            continue;

                        if (PCLaw.Lawyer.NickName.Equals("IT~") )
                            lvPLLawyers.Items[1].SubItems[2].Text = PCLaw.Lawyer.ID.ToString();
                        else
                        {
                            strPL[0] = PCLaw.Lawyer.Name;
                            strPL[1] = PCLaw.Lawyer.NickName;
                            strPL[2] = PCLaw.Lawyer.ID.ToString();
                            itm = new ListViewItem(strPL);
                            lvPLLawyers.Items.Add(itm);
                        }
                    }
                    
                    
                    Table = new DataTable("AMLawyers");
                    sSelect = "Select ID, GivenNames, LastName, Initials from LAWYER";
                    AMData.AMLawyer Lawyer = new  AMData.AMLawyer();
                    Lawyer.ReadAMTable(ref Table, sSelect);

                    
                    for (int i = 0; i<Table.Rows.Count; i++)
                    {
                        addLawyerToStaging(Table.Rows[i]["ID"].ToString().Trim(), Table.Rows[i]["GivenNames"].ToString().Trim() + " " + Table.Rows[i]["LastName"].ToString().Trim(), Table.Rows[i]["Initials"].ToString().Trim());
                        strAM[0] = Table.Rows[i]["GivenNames"].ToString().Trim() + " " + Table.Rows[i]["LastName"].ToString().Trim(); ;
                        strAM[2]  =   Table.Rows[i]["ID"].ToString().Trim();
                        itm = new ListViewItem(strAM);
                        lvAMLawyers.Items.Add(itm);
                    }

                    AutoMatch(lvPLLawyers, ref lvAMLawyers);
                    break;

                case 2:
                    if (this.chkAppointments.Checked == false)
                    {
                        this.btnMatchUsers.Enabled = false;
                        this.btnUnMatchUsers.Enabled = false;
                        break;
                    }


                    if (lvPLUsers.Columns.Count > 0 ) //we have been here already
                        break; 

                    lvPLUsers.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLUsers.Columns.Add("NickName",  50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLUsers.Columns.Add("ID",        50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMUsers.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMUsers.Columns.Add("Match",    150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMUsers.Columns.Add("AMID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMUsers.Columns.Add("PLID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    
                     
                    while (PCLaw.User.GetNextRecord() == 0)
                    {
                        if (PCLaw.User.Status.Equals(PLConvert.PLUser.eSTATUS.ACTIVE) == false)
                            continue;
                        
                        strPL[0] = PCLaw.User.Name;
                        strPL[1] = PCLaw.User.NickName;
                        strPL[2] = PCLaw.User.ID.ToString();
                        itm = new ListViewItem(strPL);
                        lvPLUsers.Items.Add(itm);
                    }
                    
                    Table = new DataTable("AMUsers");
                    sSelect = "SELECT TTLastName, TTFirstNam, TTUserID FROM TTUSER ORDER BY TTUserID";
                    AMData.AMUser User = new  AMData.AMUser();
                    User.ReadAMTable(ref Table, sSelect);
                    
                    for (int i = 0; i<Table.Rows.Count; i++)
                    {
                        strAM[0] = Table.Rows[i]["TTFirstNam"].ToString().Trim() + " " + Table.Rows[i]["TTLastName"].ToString().Trim();
                        strAM[2] = Table.Rows[i]["TTUserID"].ToString().Trim();
                        itm = new ListViewItem(strAM);
                        lvAMUsers.Items.Add(itm);
                    }
            
                    AutoMatch(lvPLUsers, ref lvAMUsers);
                   
                    break;

                case 3:
                    
                   // if (this.chkActiveMatters.Checked == false && 
                   //     this.chkArchivedMatters.Checked == false &&
                   //     this.chkSpecialMatters.Checked == false)
                   // {
                   //     this.btnMatchTypeOfLaw.Enabled = false;
                   //     this.btnUnMatchTypeOfLaw.Enabled = false;
                   //     break;
                   // }
                   // else
                  //  {
                   //     this.btnMatchTypeOfLaw.Enabled = true;
                   //     this.btnUnMatchTypeOfLaw.Enabled = true;
                   // }

                    if (this.lvPLTypeOfLaw.Columns.Count > 0 )
                        break; 

                    lvPLTypeOfLaw.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLTypeOfLaw.Columns.Add("NickName",  50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLTypeOfLaw.Columns.Add("ID",        50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMTypeOfLaw.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMTypeOfLaw.Columns.Add("Match",    150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMTypeOfLaw.Columns.Add("AMID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMTypeOfLaw.Columns.Add("PLID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    
                     
                    while (PCLaw.TypeOfLaw.GetNextRecord() == 0)
                    {
                        if (PCLaw.TypeOfLaw.Status.Equals(PLConvert.PLTypeOfLaw.eSTATUS.ACTIVE) == false)
                            continue;
                        
                        strPL[0] = PCLaw.TypeOfLaw.Name;
                        strPL[1] = PCLaw.TypeOfLaw.NickName;
                        strPL[2] = PCLaw.TypeOfLaw.ID.ToString();
                        itm = new ListViewItem(strPL);
                        lvPLTypeOfLaw.Items.Add(itm);
                    }
                    
                    Table = new DataTable("AMFileType");
                    sSelect = "select id, [desc] from arealaw";
                    AMData.AMTypeOfLaw TypeOfLaw = new  AMData.AMTypeOfLaw();
                    TypeOfLaw.ReadAMTable(ref Table, sSelect);
                    
                    for (int i = 0; i<Table.Rows.Count; i++)
                    {
                        addLawTypeToStaging(Table.Rows[i]["id"].ToString().Trim(), Table.Rows[i]["desc"].ToString().Trim());
                        strAM[0]  =   Table.Rows[i]["desc"].ToString().Trim();
                        strAM[2]  =   Table.Rows[i]["id"].ToString().Trim();
                        if (strAM[0].Equals(""))
                            continue;

                        itm = new ListViewItem(strAM);
                        lvAMTypeOfLaw.Items.Add(itm);
                    }

                    AutoMatch(lvPLTypeOfLaw, ref lvAMTypeOfLaw);
                     
                    break;
                case 4:
                    /*
                    if (this.chkTimeFees.Checked == false)
                    {
                        this.btnMatchTask.Enabled = false;
                        this.btnUnMatchTask.Enabled = false;
                        break;
                    }
                    else
                    {
                        this.btnMatchTask.Enabled = true;
                        this.btnUnMatchTask.Enabled = true;
                    }

                    if (this.lvPLTask.Columns.Count > 0 )
                        break; 

                    lvPLTask.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLTask.Columns.Add("NickName",  50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLTask.Columns.Add("ID",        50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMTask.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMTask.Columns.Add("Match",    150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMTask.Columns.Add("AMID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMTask.Columns.Add("PLID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    
                     
                    while (PCLaw.Task.GetNextRecord() == 0)
                    {
                        if (PCLaw.Task.Status.Equals(PLConvert.PLTask.eSTATUS.ACTIVE) == false)
                            continue;

                        strPL[0] = PCLaw.Task.Name;
                        strPL[1] = PCLaw.Task.NickName;
                        strPL[2] = PCLaw.Task.ID.ToString();
                        itm = new ListViewItem(strPL);
                        lvPLTask.Items.Add(itm);
                    }
                    
                    Table = new DataTable("AMTAsk");
                    sSelect = "SELECT * FROM BillingCode2Lexicon ORDER BY Label";
                    AMData.AMTask Task = new  AMData.AMTask();
                    Task.ReadAMTable(ref Table, sSelect);
                    
                    bool bIsTaskBased = true;
                    for (int i = 0; i<Table.Rows.Count; i++)
                    {
                        if (Table.Rows[i]["Label"].ToString().Trim().Equals(""))
                            continue;

                        bIsTaskBased = true;
                        if (Table.Rows[i]["Label"].ToString().Trim().Length <= 4)
                            bIsTaskBased = false;

                        cTest = Table.Rows[i]["Label"].ToString().Trim()[0];
                        if (bIsTaskBased && Char.IsDigit(cTest))
                            bIsTaskBased = false;

                        cTest = Table.Rows[i]["Label"].ToString().Trim()[4];
                        if (bIsTaskBased && cTest != ' ' )
                            bIsTaskBased = false;
                            
                                                    
                        if (bIsTaskBased)
                        {
                            sTest = Table.Rows[i]["Label"].ToString().Trim().Substring(1,3);
                            

                            for (int j = 0; j < sTest.Length; j++)
                            {
                                if (!Char.IsDigit(sTest[j]))
                                {
                                    bIsTaskBased = false;
                                    break;
                                }
                            }
                        }

                        if (bIsTaskBased)
                            sName = Table.Rows[i]["Label"].ToString().Trim().Substring(5);
                        else
                            sName = Table.Rows[i]["Label"].ToString().Trim();


                        if (sName.Equals("Codes"))
                            continue;

                        strAM[0]  =   sName;
                        strAM[2]  =   Table.Rows[i]["BillingCodeId"].ToString().Trim();
                        

                        itm = new ListViewItem(strAM);
                        lvAMTask.Items.Add(itm);
                    }

                    AutoMatch(lvPLTask, ref lvAMTask);
                     * */
                    break;

                case 5:
                    /*
                    if (this.chkContacts.Checked == false)
                    {
                        this.btnMatchCTCTType.Enabled = false;
                        this.btnUnMatchCTCTType.Enabled = false;
                        break;
                    }
                    else
                    {
                        this.btnMatchCTCTType.Enabled = true;
                        this.btnUnMatchCTCTType.Enabled = true;
                    }

                    if (this.lvPLCTCTType.Columns.Count > 0 )
                        break; 
                    lvPLCTCTType.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLCTCTType.Columns.Add("NickName",  50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLCTCTType.Columns.Add("ID",        50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMCTCTType.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMCTCTType.Columns.Add("Match",    150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMCTCTType.Columns.Add("AMID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMCTCTType.Columns.Add("PLID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    
                     
                    while (PCLaw.ContactType.GetNextRecord() == 0)
                    {
                        if (PCLaw.ContactType.Status.Equals(PLConvert.PLContactType.eSTATUS.ACTIVE) == false)
                            continue;
                        
                        strPL[0] = PCLaw.ContactType.Name;
                        strPL[1] = PCLaw.ContactType.NickName;
                        strPL[2] = PCLaw.ContactType.ID.ToString();
                        itm = new ListViewItem(strPL);
                        lvPLCTCTType.Items.Add(itm);
                    }
                    
                    Table = new DataTable("AMCTCTType");
                    sSelect = "SELECT * FROM Role2Lexicon ORDER BY Label";
                    AMData.AMContactType CTCTType = new  AMData.AMContactType();
                    CTCTType.ReadAMTable(ref Table, sSelect);
                    
                    for (int i = 0; i<Table.Rows.Count; i++)
                    {
                        if (Table.Rows[i]["Label"].ToString().Trim().Equals(""))
                            continue;

                        strAM[0]  =   Table.Rows[i]["Label"].ToString().Trim();
                        strAM[2]  =   Table.Rows[i]["RoleId"].ToString().Trim();
                        

                        itm = new ListViewItem(strAM);
                        lvAMCTCTType.Items.Add(itm);
                    }

                    AutoMatch(lvPLCTCTType, ref lvAMCTCTType);
                     * */
                    break;

                case 6:
                    /*
                    if (this.chkAppointments.Checked == false)
                    {
                        this.btnMatchDiaryCode.Enabled = false;
                        this.btnUnMatchDiaryCode.Enabled= false;
                        break;
                    }
                    else
                    {
                        this.btnMatchDiaryCode.Enabled = true;
                        this.btnUnMatchDiaryCode.Enabled= true;
                    }

                    if (this.lvPLDiaryCode.Columns.Count > 0 )
                        break; 

                    
                    lvPLDiaryCode.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLDiaryCode.Columns.Add("NickName",  50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLDiaryCode.Columns.Add("ID",        50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMDiaryCode.Columns.Add("Name",     150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMDiaryCode.Columns.Add("Match",    150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMDiaryCode.Columns.Add("AMID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMDiaryCode.Columns.Add("PLID",      50, System.Windows.Forms.HorizontalAlignment.Left);
                    
                     
                    while (PCLaw.DiaryCode.GetNextRecord() == 0)
                    {
                        if (PCLaw.DiaryCode.Status.Equals(PLConvert.PLDiaryCode.eSTATUS.ACTIVE) == false)
                            continue;

                        strPL[0] = PCLaw.DiaryCode.Name;
                        strPL[1] = PCLaw.DiaryCode.NickName;
                        strPL[2] = PCLaw.DiaryCode.ID.ToString();
                        itm = new ListViewItem(strPL);
                        lvPLDiaryCode.Items.Add(itm);
                    }
                    
                    Table = new DataTable("AMDiarycode");
                    sSelect = "SELECT * FROM EventCategory2Lexicon ORDER BY Label";
                    AMData.AMDiaryCode DiaryCode = new  AMData.AMDiaryCode();
                    DiaryCode.ReadAMTable(ref Table, sSelect);
                    
                    for (int i = 0; i<Table.Rows.Count; i++)
                    {
                        if (Table.Rows[i]["Label"].ToString().Trim().Equals(""))
                            continue;

                        strAM[0]  =   Table.Rows[i]["Label"].ToString().Trim();
                        strAM[2]  =   Table.Rows[i]["EventCategoryId"].ToString().Trim();
                        

                        itm = new ListViewItem(strAM);
                        lvAMDiaryCode.Items.Add(itm);
                    }

                    AutoMatch(lvPLDiaryCode, ref lvAMDiaryCode);
                     */
                    break;
                case 7:


                    if (this.lvAMGLAccts.Columns.Count > 0)
                        break;

                    lvPLGLAccts.Columns.Add("Name", 150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLGLAccts.Columns.Add("NickName", 50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvPLGLAccts.Columns.Add("ID", 50, System.Windows.Forms.HorizontalAlignment.Left);

                    lvAMGLAccts.Columns.Add("Name", 150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMGLAccts.Columns.Add("Match", 150, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMGLAccts.Columns.Add("AMID", 50, System.Windows.Forms.HorizontalAlignment.Left);
                    lvAMGLAccts.Columns.Add("PLID", 50, System.Windows.Forms.HorizontalAlignment.Left);


                    while (PCLaw.GLAccts.GetNextRecord() == 0)
                    {
                       // if (PCLaw.GLAccts.Status.Equals(PLConvert.PLTypeOfLaw.eSTATUS.ACTIVE) == false)
                         //   continue;
                        Conversion.GLAcct.existingPLNNs.Add(PCLaw.GLAccts.NickName);
                        strPL[0] = PCLaw.GLAccts.Name;
                        strPL[1] = PCLaw.GLAccts.NickName;
                        strPL[2] = PCLaw.GLAccts.ID.ToString();
                        itm = new ListViewItem(strPL);
                        lvPLGLAccts.Items.Add(itm);
                    }

                    Table = new DataTable("AMGLAccts");
                    sSelect = "select glacct, descr, rectyp, inactive, banktype from glmaster";
                    AMData.AMGLAcct GLAcct = new AMData.AMGLAcct();
                    GLAcct.ReadAMTable(ref Table, sSelect);

                    for (int i = 0; i < Table.Rows.Count; i++)
                    {
                        addGLAcctToStaging(Table.Rows[i]["glacct"].ToString().Trim(), Table.Rows[i]["descr"].ToString().Trim(), Table.Rows[i]["rectyp"].ToString().Trim(), Table.Rows[i]["inactive"].ToString().Trim());
                        if (string.IsNullOrEmpty(Table.Rows[i]["banktype"].ToString().Trim()))
                            strAM[0] = Table.Rows[i]["descr"].ToString().Trim();
                        else
                            strAM[0] = Table.Rows[i]["descr"].ToString().Trim() + "#$%" + Table.Rows[i]["banktype"].ToString().Trim();
                        strAM[2] = Table.Rows[i]["glacct"].ToString().Trim();
                        if (strAM[0].Equals(""))
                            continue;

                        itm = new ListViewItem(strAM);
                        lvAMGLAccts.Items.Add(itm);
                    }

                    AutoMatch(lvPLGLAccts, ref lvAMGLAccts);

                    break;


            }
             
        
        }


        private void addLawyerToStaging(string oldID, string name, string initials)
        {
           // MessageBox.Show("Got here");
            string sql = "INSERT INTO stg_LawyertoID (LawyerIDold,Name,oldNN)VALUES (@val1, @val2, @val3)";
            string connString = @"Data Source=npc365;Initial Catalog=AMTest;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.Parameters.AddWithValue("@val1", int.Parse(oldID));
                comm.Parameters.AddWithValue("@val2", name);
                comm.Parameters.AddWithValue("@val3", initials);
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch(SqlException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            }




        }


        private void addLawTypeToStaging(string oldID, string name)
        {
            // MessageBox.Show("Got here");
            string sql = "INSERT INTO stg_LawTypes (LawIDold,descrip)VALUES (@val1, @val2)";
            string connString = @"Data Source=npc365;Initial Catalog=AMTest;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = sql;
                    comm.Parameters.AddWithValue("@val1", int.Parse(oldID));
                    comm.Parameters.AddWithValue("@val2", name);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }




        }

        private void addGLAcctToStaging(string oldID, string name, string recType, string inact)
        {
            string sql = "INSERT INTO stg_GLAccountToID (GLIDold,Name,recType, inactive)VALUES (@val1, @val2, @val3, @val4)";
            string connString = @"Data Source=npc365;Initial Catalog=AMTest;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    char inactive = 'N';
                    if (!inact.Equals("0"))
                        inactive = 'Y';
                    comm.Connection = conn;
                    comm.CommandText = sql;
                    comm.Parameters.AddWithValue("@val1", int.Parse(oldID));
                    comm.Parameters.AddWithValue("@val2", name);
                    comm.Parameters.AddWithValue("@val3", int.Parse(recType));
                    comm.Parameters.AddWithValue("@val4", inactive);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        protected void AutoMatch( System.Windows.Forms.ListView OurList, ref System.Windows.Forms.ListView OutsideList)
        {
            int nI = 0;
            int nJ = 0;
            string Our_sTest;
            string Outside_sTest;

            for (nI = 0; nI < OurList.Items.Count; nI++)
            {
                Our_sTest = OurList.Items[nI].SubItems[0].Text;
                Our_sTest = Our_sTest.ToUpper();
                while (Our_sTest.IndexOf("  ") >= 0)
                    Our_sTest = Our_sTest.Replace( "  ", " ");

                for (nJ = 0; nJ < OutsideList.Items.Count; nJ++)
                {
                    Outside_sTest = OutsideList.Items[nJ].SubItems[0].Text;
                    Outside_sTest = Outside_sTest.ToUpper();
                    string[] items = Outside_sTest.Trim().Split(new string[] { "#$%" }, StringSplitOptions.None);
                    Outside_sTest = items[0];
                    while (Outside_sTest.IndexOf("  ") >= 0)
                        Outside_sTest = Our_sTest.Replace( "  ", " ");

                    if (Our_sTest.Equals(Outside_sTest))
                    {
                        OutsideList.Items[nJ].SubItems[1].Text = OurList.Items[nI].SubItems[0].Text;
                        OutsideList.Items[nJ].SubItems[3].Text = OurList.Items[nI].SubItems[2].Text;
                    }
                }
            }
        }

        private void Match( System.Windows.Forms.ListView OurList, ref System.Windows.Forms.ListView OutsideList)
        {

            if (OurList.SelectedItems.Count == 0)
                return;

            if (OutsideList.SelectedItems.Count == 0 )
                return;

            string sOurName = OurList.SelectedItems[0].SubItems[0].Text;
            if (sOurName.Length == 0)
                return;


            int nJ = 0;
            
            for (nJ = 0; nJ < OutsideList.SelectedItems.Count ; nJ++)
            {
                if (OutsideList.SelectedItems[nJ].SubItems[1].Text.Equals("") )
                {
                    OutsideList.SelectedItems[nJ].SubItems[1].Text = sOurName;
                    OutsideList.SelectedItems[nJ].SubItems[3].Text = OurList.SelectedItems[0].SubItems[2].Text;
                }
            }
            
        }

        private void UnMatch( ref System.Windows.Forms.ListView OutsideList)
        {
            if (OutsideList.SelectedItems.Count == 0 )
                return;

            int nJ = 0;
            
            for (nJ = 0; nJ < OutsideList.SelectedItems.Count ; nJ++)
            {
                OutsideList.SelectedItems[nJ].SubItems[1].Text = "";
            }
            
        }



        public PLConvert.PCLawConversion PCLaw;
        public AMConversion Conversion;

        private void btnMatchLwr_Click(object sender, System.EventArgs e)
        {
            Match(this.lvPLLawyers, ref this.lvAMLawyers);
        }

        private void btnUnMatchLwr_Click(object sender, System.EventArgs e)
        {
            UnMatch(ref this.lvAMLawyers);
        }

        private void lvAMLawyers_DoubleClick(object sender, System.EventArgs e)
        {                                   
            Match(this.lvPLLawyers, ref this.lvAMLawyers);
        }

        private void tabUsers_Click(object sender, System.EventArgs e)
        {
        
        }

        private void btnMatchUsers_Click(object sender, System.EventArgs e)
        {
            Match(this.lvPLUsers, ref this.lvAMUsers);
        }

        private void btnUnMatchUsers_Click(object sender, System.EventArgs e)
        {
            UnMatch(ref this.lvAMUsers);
        }

        private void btnMatchTypeOfLaw_Click(object sender, System.EventArgs e)
        {
            Match(this.lvPLTypeOfLaw, ref this.lvAMTypeOfLaw);
        }

        private void btnUnMatchTypeOfLaw_Click(object sender, System.EventArgs e)
        {
            UnMatch(ref this.lvAMTypeOfLaw);
        }

        private void btnMatchTask_Click(object sender, System.EventArgs e)
        {
            Match(this.lvPLTask, ref this.lvAMTask);
        }

        private void btnUnMatchTask_Click(object sender, System.EventArgs e)
        {
            UnMatch(ref this.lvAMTask);
        }

        private void tabTaskCode_Click(object sender, System.EventArgs e)
        {
        
        }

        private void lvPLTask_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }

        private void tabCTCTType_Click(object sender, System.EventArgs e)
        {
        
        }

        private void btnMatchCTCTType_Click(object sender, System.EventArgs e)
        {
            Match(this.lvPLCTCTType, ref this.lvAMCTCTType);
        }

        private void btnUnMatchCTCTType_Click(object sender, System.EventArgs e)
        {
            UnMatch(ref this.lvAMCTCTType);
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            int nI = 0;
            int nID = 0;
            
            AMConversion.bPCLawMoreUpToDate = this.chkPLMoreUpToDate.Checked;
            AMData.Selections.bPCLawMoreUpToDate =this.chkPLMoreUpToDate.Checked;
            AMConversion.bImportActiveMatters = this.chkActiveMatters.Checked;
            AMData.Selections.bImportActiveMatters = this.chkActiveMatters.Checked;
            AMConversion.bImportArchivedMattres = this.chkArchivedMatters.Checked;
            AMData.Selections.bImportArchivedMattres = this.chkArchivedMatters.Checked;
            AMConversion.bImportSpecialMatters = this.chkSpecialMatters.Checked;
            AMData.Selections.bImportSpecialMatters = this.chkSpecialMatters.Checked;
            AMConversion.bCombineCliMatt = this.chkCombineCli_Mat.Checked;
            AMData.Selections.bCombineCliMatt = this.chkCombineCli_Mat.Checked;
            AMConversion.bImportContacts = this.chkContacts.Checked;
            AMData.Selections.bImportContacts = this.chkContacts.Checked;
            AMConversion.bImportDiary = this.chkAppointments.Checked;
            AMData.Selections.bImportDiary = this.chkAppointments.Checked;
            AMConversion.bImportTimeFees = this.chkTimeFees.Checked;
            AMData.Selections.bImportTimeFees = this.chkTimeFees.Checked;

            for (nI = 0; nI < this.lvAMLawyers.Items.Count; nI++)
            {
                if (lvAMLawyers.Items[nI].SubItems[3].Text.Equals(""))
                {
                    Conversion.Lawyer.AMNametoPLName.Add(lvAMLawyers.Items[nI].SubItems[0].Text, lvAMLawyers.Items[nI].SubItems[1].Text );
                    Conversion.Lawyer.AMNametoAMID.Add  (lvAMLawyers.Items[nI].SubItems[0].Text, lvAMLawyers.Items[nI].SubItems[2].Text );
                }
                else
                {
                    if (Conversion.Lawyer.AMIDtoPLID.ContainsKey(lvAMLawyers.Items[nI].SubItems[2].Text) == false)
                        Conversion.Lawyer.AMIDtoPLID.Add (lvAMLawyers.Items[nI].SubItems[2].Text, Convert.ToInt32(lvAMLawyers.Items[nI].SubItems[3].Text) );
                }
            }

            for (nI = 0; nI < this.lvAMGLAccts.Items.Count; nI++)
            {
                if (lvAMGLAccts.Items[nI].SubItems[3].Text.Equals(""))
                {
                        //Conversion.GLAcct.AMNametoPLName.Add(lvAMGLAccts.Items[nI].SubItems[0].Text, lvAMGLAccts.Items[nI].SubItems[1].Text);
                        Conversion.GLAcct.AMNametoAMID.Add(lvAMGLAccts.Items[nI].SubItems[2].Text, lvAMGLAccts.Items[nI].SubItems[0].Text);
                       // MessageBox.Show(lvAMGLAccts.Items[nI].SubItems[0].Text + " -- " + lvAMGLAccts.Items[nI].SubItems[1].Text);
                }
                else
                {
                    if (Conversion.GLAcct.AMIDtoPLID.ContainsKey(lvAMGLAccts.Items[nI].SubItems[2].Text) == false)
                        Conversion.GLAcct.AMIDtoPLID.Add(lvAMGLAccts.Items[nI].SubItems[2].Text, Convert.ToInt32(lvAMGLAccts.Items[nI].SubItems[3].Text));
                }
            }
            //MessageBox.Show(Conversion.GLAcct.AMNametoPLName.Count.ToString());
            for (nI = 0; nI < this.lvAMUsers.Items.Count; nI++)
            {
                if (lvAMUsers.Items[nI].SubItems[3].Text.Equals("") == false)
                    Conversion.User.AMIDtoPLID.Add (lvAMUsers.Items[nI].SubItems[2].Text, Convert.ToInt32( lvAMUsers.Items[nI].SubItems[3].Text) );
                else
                    Conversion.User.AMIDtoPLID.Add (lvAMUsers.Items[nI].SubItems[2].Text, PLConvert.PLUser.GetIDFromNN("ADMIN"));
            }

            for (nI = 0; nI < this.lvAMTypeOfLaw.Items.Count; nI++)
            {
                if (lvAMTypeOfLaw.Items[nI].SubItems[3].Text.Equals("") )
                {
                    if (Conversion.TypeOfLaw.AMNametoPLName.ContainsKey(lvAMTypeOfLaw.Items[nI].SubItems[0].Text) == false)
                        Conversion.TypeOfLaw.AMNametoPLName.Add (lvAMTypeOfLaw.Items[nI].SubItems[0].Text,  lvAMTypeOfLaw.Items[nI].SubItems[0].Text);

                    if (Conversion.TypeOfLaw.AMNametoAMID.ContainsKey(lvAMTypeOfLaw.Items[nI].SubItems[0].Text) == false)
                        Conversion.TypeOfLaw.AMNametoAMID.Add   (lvAMTypeOfLaw.Items[nI].SubItems[0].Text,  lvAMTypeOfLaw.Items[nI].SubItems[2].Text);
                }
                else
                {
                    nID = Convert.ToInt32( lvAMTypeOfLaw.Items[nI].SubItems[3].Text);
                    PLConvert.PLTypeOfLaw.AddMapExtID1toPLID(lvAMTypeOfLaw.Items[nI].SubItems[2].Text, nID);
                }
            }
            
            for (nI = 0; nI < this.lvAMTask.Items.Count; nI++)
            {
                if (lvAMTask.Items[nI].SubItems[3].Text.Equals("") )
                {
                    if (Conversion.Task.AMNametoPLName.ContainsKey(lvAMTask.Items[nI].SubItems[0].Text) == false)
                        Conversion.Task.AMNametoPLName.Add  (lvAMTask.Items[nI].SubItems[0].Text,   lvAMTask.Items[nI].SubItems[0].Text);
                    
                    if (Conversion.Task.AMIDtoPLID.ContainsKey(lvAMTask.Items[nI].SubItems[0].Text) == false)
                    {
                        if (Conversion.Task.AMNametoAMID.ContainsKey(lvAMTask.Items[nI].SubItems[0].Text) == false)
                            Conversion.Task.AMNametoAMID.Add    (lvAMTask.Items[nI].SubItems[0].Text,   lvAMTask.Items[nI].SubItems[2].Text);
                    }
                }
                else
                {
                    nID = Convert.ToInt32( lvAMTask.Items[nI].SubItems[3].Text);
                    PLConvert.PLTask.AddMapExtID1toPLID(lvAMTask.Items[nI].SubItems[2].Text, nID);
                }

//                    if (Conversion.Task.AMIDtoPLID.ContainsKey (lvAMTask.Items[nI].SubItems[2].Text.ToString()) == false)
//                        Conversion.Task.AMIDtoPLID.Add (lvAMTask.Items[nI].SubItems[2].Text.ToString(), lvAMTask.Items[nI].SubItems[3].Text );
            }

            for (nI = 0; nI < this.lvAMCTCTType.Items.Count; nI++)
            {
                if (lvAMCTCTType.Items[nI].SubItems[3].Text.Equals("") )
                {
                    if (Conversion.ContactType.AMNametoPLName.ContainsKey(lvAMCTCTType.Items[nI].SubItems[0].Text) == false)
                        Conversion.ContactType.AMNametoPLName.Add   (lvAMCTCTType.Items[nI].SubItems[0].Text, lvAMCTCTType.Items[nI].SubItems[0].Text);
                    
                    if (Conversion.ContactType.AMNametoAMID.ContainsKey(lvAMCTCTType.Items[nI].SubItems[0].Text) == false)
                        Conversion.ContactType.AMNametoAMID.Add     (lvAMCTCTType.Items[nI].SubItems[0].Text, lvAMCTCTType.Items[nI].SubItems[2].Text);
                }
                else
                {
                    if (Conversion.ContactType.AMIDtoPLID.ContainsKey (lvAMCTCTType.Items[nI].SubItems[2].Text)== false)
                        Conversion.ContactType.AMIDtoPLID.Add (lvAMCTCTType.Items[nI].SubItems[2].Text, Convert.ToInt32( lvAMCTCTType.Items[nI].SubItems[3].Text) );
                }
            }

            for (nI = 0; nI < this.lvAMDiaryCode.Items.Count; nI++)
            {
                if (lvAMDiaryCode.Items[nI].SubItems[3].Text.Equals("") )
                {
                    if ( Conversion.DiaryCode.AMNametoPLName.ContainsKey(lvAMDiaryCode.Items[nI].SubItems[0].Text) == false)
                        Conversion.DiaryCode.AMNametoPLName.Add (lvAMDiaryCode.Items[nI].SubItems[0].Text, lvAMDiaryCode.Items[nI].SubItems[0].Text);
                    
                    if ( Conversion.DiaryCode.AMNametoAMID.ContainsKey(lvAMDiaryCode.Items[nI].SubItems[0].Text) == false)
                        Conversion.DiaryCode.AMNametoAMID.Add   (lvAMDiaryCode.Items[nI].SubItems[0].Text, lvAMDiaryCode.Items[nI].SubItems[2].Text);
                }
                else
                {
                    if(Conversion.DiaryCode.AMIDtoPLID.ContainsKey (lvAMDiaryCode.Items[nI].SubItems[2].Text)== false) 
                        Conversion.DiaryCode.AMIDtoPLID.Add (lvAMDiaryCode.Items[nI].SubItems[2].Text, Convert.ToInt32(lvAMDiaryCode.Items[nI].SubItems[3].Text) );
                }
            }

            Conversion.DoConversion();
            MessageBox.Show("Done");
        }

        private void lvAMLawyers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }

        private void tabSelections_Click(object sender, System.EventArgs e)
        {
        
        }
        



        
	}
}
