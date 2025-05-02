namespace Taskly.Forms
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
            label2 = new System.Windows.Forms.Label();
            listViewActivityLog = new System.Windows.Forms.ListView();
            SuspendLayout();
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Dock = System.Windows.Forms.DockStyle.Top;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.Color.FromArgb(63, 150, 252);
            label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(825, 43);
            label2.TabIndex = 8;
            label2.Text = "Report";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewActivityLog
            // 
            listViewActivityLog.FullRowSelect = true;
            listViewActivityLog.Location = new System.Drawing.Point(39, 60);
            listViewActivityLog.Name = "listViewActivityLog";
            listViewActivityLog.Size = new System.Drawing.Size(742, 318);
            listViewActivityLog.TabIndex = 7;
            listViewActivityLog.UseCompatibleStateImageBehavior = false;
            listViewActivityLog.View = System.Windows.Forms.View.Details;
            // 
            // TaskReport
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(30, 30, 32);
            ClientSize = new System.Drawing.Size(825, 408);
            Controls.Add(label2);
            Controls.Add(listViewActivityLog);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            Name = "TaskReport";
            Text = "TaskReport";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewActivityLog;
    }
}