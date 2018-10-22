namespace ReportGenApp
{
    partial class Form1
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
            DisposeResources();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnShowTableReport = new System.Windows.Forms.Button();
            this.txtXmlReport = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnParaReport = new System.Windows.Forms.Button();
            this.btnShowLabelReport = new System.Windows.Forms.Button();
            this.btnMultiPartReport = new System.Windows.Forms.Button();
            this.btnBigReport = new System.Windows.Forms.Button();
            this.txtLargeReportElementCount = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowTableReport
            // 
            this.btnShowTableReport.Location = new System.Drawing.Point(13, 13);
            this.btnShowTableReport.Name = "btnShowTableReport";
            this.btnShowTableReport.Size = new System.Drawing.Size(125, 23);
            this.btnShowTableReport.TabIndex = 0;
            this.btnShowTableReport.Text = "Show tabular report";
            this.btnShowTableReport.UseVisualStyleBackColor = true;
            this.btnShowTableReport.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtXmlReport
            // 
            this.txtXmlReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXmlReport.Location = new System.Drawing.Point(0, 0);
            this.txtXmlReport.Multiline = true;
            this.txtXmlReport.Name = "txtXmlReport";
            this.txtXmlReport.ReadOnly = true;
            this.txtXmlReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXmlReport.Size = new System.Drawing.Size(776, 386);
            this.txtXmlReport.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtXmlReport);
            this.panel1.Location = new System.Drawing.Point(12, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 386);
            this.panel1.TabIndex = 2;
            // 
            // btnParaReport
            // 
            this.btnParaReport.Location = new System.Drawing.Point(144, 12);
            this.btnParaReport.Name = "btnParaReport";
            this.btnParaReport.Size = new System.Drawing.Size(125, 23);
            this.btnParaReport.TabIndex = 3;
            this.btnParaReport.Text = "Show paragraph report";
            this.btnParaReport.UseVisualStyleBackColor = true;
            this.btnParaReport.Click += new System.EventHandler(this.BtnParaReport_Click);
            // 
            // btnShowLabelReport
            // 
            this.btnShowLabelReport.Location = new System.Drawing.Point(275, 12);
            this.btnShowLabelReport.Name = "btnShowLabelReport";
            this.btnShowLabelReport.Size = new System.Drawing.Size(125, 23);
            this.btnShowLabelReport.TabIndex = 4;
            this.btnShowLabelReport.Text = "Show Label report";
            this.btnShowLabelReport.UseVisualStyleBackColor = true;
            this.btnShowLabelReport.Click += new System.EventHandler(this.BtnShowLabelReport_Click);
            // 
            // btnMultiPartReport
            // 
            this.btnMultiPartReport.Location = new System.Drawing.Point(406, 12);
            this.btnMultiPartReport.Name = "btnMultiPartReport";
            this.btnMultiPartReport.Size = new System.Drawing.Size(125, 23);
            this.btnMultiPartReport.TabIndex = 5;
            this.btnMultiPartReport.Text = "Show multi-part report";
            this.btnMultiPartReport.UseVisualStyleBackColor = true;
            this.btnMultiPartReport.Click += new System.EventHandler(this.BtnMultiPartReport_Click);
            // 
            // btnBigReport
            // 
            this.btnBigReport.Location = new System.Drawing.Point(538, 13);
            this.btnBigReport.Name = "btnBigReport";
            this.btnBigReport.Size = new System.Drawing.Size(123, 23);
            this.btnBigReport.TabIndex = 6;
            this.btnBigReport.Text = "Large Report";
            this.btnBigReport.UseVisualStyleBackColor = true;
            this.btnBigReport.Click += new System.EventHandler(this.BtnBigReport_ClickAsync);
            // 
            // txtLargeReportElementCount
            // 
            this.txtLargeReportElementCount.Location = new System.Drawing.Point(668, 14);
            this.txtLargeReportElementCount.Name = "txtLargeReportElementCount";
            this.txtLargeReportElementCount.Size = new System.Drawing.Size(100, 20);
            this.txtLargeReportElementCount.TabIndex = 7;
            this.txtLargeReportElementCount.Text = "100000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtLargeReportElementCount);
            this.Controls.Add(this.btnBigReport);
            this.Controls.Add(this.btnMultiPartReport);
            this.Controls.Add(this.btnShowLabelReport);
            this.Controls.Add(this.btnParaReport);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnShowTableReport);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowTableReport;
        private System.Windows.Forms.TextBox txtXmlReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnParaReport;
        private System.Windows.Forms.Button btnShowLabelReport;
        private System.Windows.Forms.Button btnMultiPartReport;
        private System.Windows.Forms.Button btnBigReport;
        private System.Windows.Forms.TextBox txtLargeReportElementCount;
    }
}

