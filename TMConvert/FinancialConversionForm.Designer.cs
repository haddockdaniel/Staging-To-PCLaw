namespace StagingConvert
{
    partial class FinancialConversionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinancialConversionForm));
            this.buttonFinanceContinue = new System.Windows.Forms.Button();
            this.checkBoxWIPFees = new System.Windows.Forms.CheckBox();
            this.checkBoxWIPExpenses = new System.Windows.Forms.CheckBox();
            this.checkBoxAR = new System.Windows.Forms.CheckBox();
            this.checkBoxTrust = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonFinanceContinue
            // 
            this.buttonFinanceContinue.Location = new System.Drawing.Point(51, 168);
            this.buttonFinanceContinue.Name = "buttonFinanceContinue";
            this.buttonFinanceContinue.Size = new System.Drawing.Size(112, 23);
            this.buttonFinanceContinue.TabIndex = 0;
            this.buttonFinanceContinue.Text = "Continue";
            this.buttonFinanceContinue.UseVisualStyleBackColor = true;
            this.buttonFinanceContinue.Click += new System.EventHandler(this.buttonFinanceContinue_Click);
            // 
            // checkBoxWIPFees
            // 
            this.checkBoxWIPFees.AutoSize = true;
            this.checkBoxWIPFees.Location = new System.Drawing.Point(29, 26);
            this.checkBoxWIPFees.Name = "checkBoxWIPFees";
            this.checkBoxWIPFees.Size = new System.Drawing.Size(111, 17);
            this.checkBoxWIPFees.TabIndex = 1;
            this.checkBoxWIPFees.Text = "Include WIP Fees";
            this.checkBoxWIPFees.UseVisualStyleBackColor = true;
            // 
            // checkBoxWIPExpenses
            // 
            this.checkBoxWIPExpenses.AutoSize = true;
            this.checkBoxWIPExpenses.Location = new System.Drawing.Point(29, 59);
            this.checkBoxWIPExpenses.Name = "checkBoxWIPExpenses";
            this.checkBoxWIPExpenses.Size = new System.Drawing.Size(134, 17);
            this.checkBoxWIPExpenses.TabIndex = 2;
            this.checkBoxWIPExpenses.Text = "Include WIP Expenses";
            this.checkBoxWIPExpenses.UseVisualStyleBackColor = true;
            // 
            // checkBoxAR
            // 
            this.checkBoxAR.AutoSize = true;
            this.checkBoxAR.Location = new System.Drawing.Point(28, 92);
            this.checkBoxAR.Name = "checkBoxAR";
            this.checkBoxAR.Size = new System.Drawing.Size(166, 17);
            this.checkBoxAR.TabIndex = 3;
            this.checkBoxAR.Text = "Include Accounts Receivable";
            this.checkBoxAR.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrust
            // 
            this.checkBoxTrust.AutoSize = true;
            this.checkBoxTrust.Location = new System.Drawing.Point(29, 127);
            this.checkBoxTrust.Name = "checkBoxTrust";
            this.checkBoxTrust.Size = new System.Drawing.Size(88, 17);
            this.checkBoxTrust.TabIndex = 4;
            this.checkBoxTrust.Text = "Include Trust";
            this.checkBoxTrust.UseVisualStyleBackColor = true;
            // 
            // FinancialConversionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 218);
            this.Controls.Add(this.checkBoxTrust);
            this.Controls.Add(this.checkBoxAR);
            this.Controls.Add(this.checkBoxWIPExpenses);
            this.Controls.Add(this.checkBoxWIPFees);
            this.Controls.Add(this.buttonFinanceContinue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FinancialConversionForm";
            this.Text = "Financial Conversion Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFinanceContinue;
        private System.Windows.Forms.CheckBox checkBoxWIPFees;
        private System.Windows.Forms.CheckBox checkBoxWIPExpenses;
        private System.Windows.Forms.CheckBox checkBoxAR;
        private System.Windows.Forms.CheckBox checkBoxTrust;
    }
}