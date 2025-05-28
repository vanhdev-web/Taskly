namespace Taskly.Usercontrols
{
    partial class MemberItem
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
            lblMemberName = new System.Windows.Forms.Label();
            memberTitle = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // lblMemberName
            // 
            lblMemberName.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            lblMemberName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lblMemberName.Location = new System.Drawing.Point(-4, 0);
            lblMemberName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblMemberName.Name = "lblMemberName";
            lblMemberName.Size = new System.Drawing.Size(103, 28);
            lblMemberName.TabIndex = 0;
            lblMemberName.Text = "Bảo";
            lblMemberName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblMemberName.Click += label1_Click;
            // 
            // memberTitle
            // 
            memberTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            memberTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            memberTitle.Location = new System.Drawing.Point(0, 28);
            memberTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            memberTitle.Name = "memberTitle";
            memberTitle.Size = new System.Drawing.Size(97, 26);
            memberTitle.TabIndex = 1;
            memberTitle.Text = "Project owner";
            memberTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MemberItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(memberTitle);
            Controls.Add(lblMemberName);
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            Name = "MemberItem";
            Size = new System.Drawing.Size(105, 53);
            Load += MemberItem_Load;
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMemberName;
        private System.Windows.Forms.Label memberTitle;
    }
}
