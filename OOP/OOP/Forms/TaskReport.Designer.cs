namespace OOP.Forms
{
    partial class TaskReport
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
            listViewActivityLog = new System.Windows.Forms.ListView();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // listViewActivityLog
            // 
            listViewActivityLog.Location = new System.Drawing.Point(73, 128);
            listViewActivityLog.Margin = new System.Windows.Forms.Padding(6);
            listViewActivityLog.Name = "listViewActivityLog";
            listViewActivityLog.Size = new System.Drawing.Size(1375, 673);
            listViewActivityLog.TabIndex = 7;
            listViewActivityLog.UseCompatibleStateImageBehavior = false;
            listViewActivityLog.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Dock = System.Windows.Forms.DockStyle.Top;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(1533, 92);
            label2.TabIndex = 8;
            label2.Text = "Task Report";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TaskReport
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(30, 30, 32);
            ClientSize = new System.Drawing.Size(1533, 871);
            Controls.Add(label2);
            Controls.Add(listViewActivityLog);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "TaskReport";
            Text = "Report";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ListView listViewActivityLog;
        private System.Windows.Forms.Label label2;
    }
}