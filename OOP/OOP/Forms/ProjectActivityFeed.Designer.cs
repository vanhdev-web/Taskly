namespace OOP.Forms
{
    partial class ProjectActivityFeed
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
            SuspendLayout();
            // 
            // comboBoxProjectList
            // 
            comboBoxProjectList.FormattingEnabled = true;
            comboBoxProjectList.Location = new System.Drawing.Point(306, 36);
            comboBoxProjectList.Name = "comboBoxProjectList";
            comboBoxProjectList.Size = new System.Drawing.Size(321, 23);
            comboBoxProjectList.TabIndex = 0;
            comboBoxProjectList.SelectedIndexChanged += comboBoxProjectList_SelectedIndexChanged;
            // 
            // listViewActivityLog
            // 
            listViewActivityLog.Location = new System.Drawing.Point(183, 119);
            listViewActivityLog.Name = "listViewActivityLog";
            listViewActivityLog.Size = new System.Drawing.Size(564, 287);
            listViewActivityLog.TabIndex = 2;
            listViewActivityLog.UseCompatibleStateImageBehavior = false;
            listViewActivityLog.View = System.Windows.Forms.View.List;
            listViewActivityLog.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // ProjectActivityFeed
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(984, 598);
            Controls.Add(listViewActivityLog);
            Controls.Add(comboBoxProjectList);
            Name = "ProjectActivityFeed";
            Text = "ProjectActivityFeed";
            Load += ProjectActivityFeed_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProjectList;
        private System.Windows.Forms.ListView listViewActivityLog;
    }
}