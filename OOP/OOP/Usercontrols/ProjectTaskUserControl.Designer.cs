namespace Taskly.Usercontrols
{
    partial class ProjectTaskUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel9 = new System.Windows.Forms.Panel();
            this.type = new System.Windows.Forms.Label();
            this.who = new System.Windows.Forms.Label();
            this.taskContent = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.PictureBox();
            this.taskDeadline = new System.Windows.Forms.Label();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.type);
            this.panel9.Controls.Add(this.who);
            this.panel9.Controls.Add(this.taskContent);
            this.panel9.Controls.Add(this.checkBox);
            this.panel9.Controls.Add(this.taskDeadline);
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(789, 49);
            this.panel9.TabIndex = 4;
            // 
            // type
            // 
            this.type.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.type.ForeColor = System.Drawing.SystemColors.Highlight;
            this.type.Location = new System.Drawing.Point(49, 11);
            this.type.Name = "type";
            this.type.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.type.Size = new System.Drawing.Size(101, 21);
            this.type.TabIndex = 5;
            this.type.Text = "Milestone:";
            this.type.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // who
            // 
            this.who.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.who.ForeColor = System.Drawing.SystemColors.Highlight;
            this.who.Location = new System.Drawing.Point(685, 4);
            this.who.Name = "who";
            this.who.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.who.Size = new System.Drawing.Size(121, 35);
            this.who.TabIndex = 4;
            this.who.Text = "Bảo";
            this.who.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // taskContent
            // 
            this.taskContent.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.taskContent.ForeColor = System.Drawing.SystemColors.Highlight;
            this.taskContent.Location = new System.Drawing.Point(155, 0);
            this.taskContent.Name = "taskContent";
            this.taskContent.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.taskContent.Size = new System.Drawing.Size(412, 42);
            this.taskContent.TabIndex = 3;
            this.taskContent.Text = "Check bài nhóm";
            this.taskContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox
            // 
            this.checkBox.ErrorImage = global::Taskly.Properties.Resources.check;
            this.checkBox.InitialImage = null;
            this.checkBox.Location = new System.Drawing.Point(15, 7);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(28, 25);
            this.checkBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.checkBox.TabIndex = 1;
            this.checkBox.TabStop = false;
            // 
            // taskDeadline
            // 
            this.taskDeadline.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.taskDeadline.ForeColor = System.Drawing.SystemColors.Highlight;
            this.taskDeadline.Location = new System.Drawing.Point(573, 4);
            this.taskDeadline.Name = "taskDeadline";
            this.taskDeadline.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.taskDeadline.Size = new System.Drawing.Size(107, 35);
            this.taskDeadline.TabIndex = 2;
            this.taskDeadline.Text = "29/03/2025";
            this.taskDeadline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProjectTaskUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.panel9);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProjectTaskUserControl";
            this.Size = new System.Drawing.Size(799, 58);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label type;
        private System.Windows.Forms.Label taskContent;
        private System.Windows.Forms.PictureBox checkBox;
        private System.Windows.Forms.Label taskDeadline;
        private System.Windows.Forms.Label who;
    }
}
