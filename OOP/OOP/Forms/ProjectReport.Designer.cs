namespace OOP.Forms
{
    partial class ProjectReport
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
            comboBoxProjectList = new System.Windows.Forms.ComboBox();
            listViewActivityLog = new System.Windows.Forms.ListView();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // comboBoxProjectList
            // 
            comboBoxProjectList.FormattingEnabled = true;
            comboBoxProjectList.Location = new System.Drawing.Point(568, 61);
            comboBoxProjectList.Margin = new System.Windows.Forms.Padding(6);
            comboBoxProjectList.Name = "comboBoxProjectList";
            comboBoxProjectList.Size = new System.Drawing.Size(876, 40);
            comboBoxProjectList.TabIndex = 0;
            comboBoxProjectList.SelectedIndexChanged += comboBoxProjectList_SelectedIndexChanged;
            // 
            // listViewActivityLog
            // 
            listViewActivityLog.Location = new System.Drawing.Point(69, 129);
            listViewActivityLog.Margin = new System.Windows.Forms.Padding(6);
            listViewActivityLog.Name = "listViewActivityLog";
            listViewActivityLog.Size = new System.Drawing.Size(1375, 673);
            listViewActivityLog.TabIndex = 2;
            listViewActivityLog.UseCompatibleStateImageBehavior = false;
            listViewActivityLog.View = System.Windows.Forms.View.List;
            listViewActivityLog.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            label2.Location = new System.Drawing.Point(69, 50);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(490, 53);
            label2.TabIndex = 5;
            label2.Text = "Choose your projects";
            // 
            // ProjectReport
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(30, 30, 32);
            ClientSize = new System.Drawing.Size(1492, 865);
            Controls.Add(label2);
            Controls.Add(listViewActivityLog);
            Controls.Add(comboBoxProjectList);
            Margin = new System.Windows.Forms.Padding(6);
            Name = "ProjectReport";
            Text = "ProjectReport";
            Load += ProjectReport_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProjectList;
        private System.Windows.Forms.ListView listViewActivityLog;
        private System.Windows.Forms.Label label2;
    }
}