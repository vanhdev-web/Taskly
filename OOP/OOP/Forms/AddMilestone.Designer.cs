using System.Drawing;

namespace Taskly.Forms
{
    partial class AddMilestone
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
            components = new System.ComponentModel.Container();
            lblMilestoneName = new System.Windows.Forms.Label();
            txtbMilestoneName = new System.Windows.Forms.TextBox();
            lalMilestoneDate = new System.Windows.Forms.Label();
            dtpMilestonedate = new System.Windows.Forms.DateTimePicker();
            btnMilestoneConfirm = new ReaLTaiizor.Controls.Button();
            btnMilestoneCancel = new ReaLTaiizor.Controls.Button();
            cbbSelectProject = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            errMilestoneName = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errMilestoneName).BeginInit();
            SuspendLayout();
            // 
            // lblMilestoneName
            // 
            lblMilestoneName.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblMilestoneName.ForeColor = SystemColors.ButtonHighlight;
            lblMilestoneName.Location = new Point(72, 104);
            lblMilestoneName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMilestoneName.Name = "lblMilestoneName";
            lblMilestoneName.Size = new Size(195, 46);
            lblMilestoneName.TabIndex = 0;
            lblMilestoneName.Text = "Nhập Milestone";
            // 
            // txtbMilestoneName
            // 
            txtbMilestoneName.Location = new Point(295, 104);
            txtbMilestoneName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtbMilestoneName.Name = "txtbMilestoneName";
            txtbMilestoneName.Size = new Size(486, 39);
            txtbMilestoneName.TabIndex = 1;
            txtbMilestoneName.Validating += txtbMilestoneName_Validating;
            // 
            // lalMilestoneDate
            // 
            lalMilestoneDate.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lalMilestoneDate.ForeColor = SystemColors.ButtonHighlight;
            lalMilestoneDate.Location = new Point(72, 214);
            lalMilestoneDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lalMilestoneDate.Name = "lalMilestoneDate";
            lalMilestoneDate.Size = new Size(147, 46);
            lalMilestoneDate.TabIndex = 2;
            lalMilestoneDate.Text = "Nhập ngày";
            lalMilestoneDate.Click += label1_Click;
            // 
            // dtpMilestonedate
            // 
            dtpMilestonedate.Location = new Point(295, 216);
            dtpMilestonedate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dtpMilestonedate.Name = "dtpMilestonedate";
            dtpMilestonedate.Size = new Size(486, 39);
            dtpMilestonedate.TabIndex = 3;
            // 
            // btnMilestoneConfirm
            // 
            btnMilestoneConfirm.BackColor = Color.White;
            btnMilestoneConfirm.BorderColor = Color.White;
            btnMilestoneConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnMilestoneConfirm.EnteredBorderColor = Color.White;
            btnMilestoneConfirm.EnteredColor = Color.White;
            btnMilestoneConfirm.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnMilestoneConfirm.Image = null;
            btnMilestoneConfirm.ImageAlign = ContentAlignment.MiddleLeft;
            btnMilestoneConfirm.InactiveColor = Color.White;
            btnMilestoneConfirm.Location = new Point(155, 435);
            btnMilestoneConfirm.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            btnMilestoneConfirm.Name = "btnMilestoneConfirm";
            btnMilestoneConfirm.PressedBorderColor = Color.White;
            btnMilestoneConfirm.PressedColor = Color.White;
            btnMilestoneConfirm.Size = new Size(212, 61);
            btnMilestoneConfirm.TabIndex = 6;
            btnMilestoneConfirm.Text = "OK";
            btnMilestoneConfirm.TextAlignment = StringAlignment.Center;
            btnMilestoneConfirm.Click += btnMilestoneConfirm_Click;
            // 
            // btnMilestoneCancel
            // 
            btnMilestoneCancel.BackColor = Color.White;
            btnMilestoneCancel.BorderColor = Color.White;
            btnMilestoneCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnMilestoneCancel.EnteredBorderColor = Color.White;
            btnMilestoneCancel.EnteredColor = Color.White;
            btnMilestoneCancel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnMilestoneCancel.Image = null;
            btnMilestoneCancel.ImageAlign = ContentAlignment.MiddleLeft;
            btnMilestoneCancel.InactiveColor = Color.White;
            btnMilestoneCancel.Location = new Point(534, 435);
            btnMilestoneCancel.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            btnMilestoneCancel.Name = "btnMilestoneCancel";
            btnMilestoneCancel.PressedBorderColor = Color.White;
            btnMilestoneCancel.PressedColor = Color.White;
            btnMilestoneCancel.Size = new Size(212, 61);
            btnMilestoneCancel.TabIndex = 7;
            btnMilestoneCancel.Text = "Cancel";
            btnMilestoneCancel.TextAlignment = StringAlignment.Center;
            btnMilestoneCancel.Click += btnMilestoneCancel_Click;
            // 
            // cbbSelectProject
            // 
            cbbSelectProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbbSelectProject.FormattingEnabled = true;
            cbbSelectProject.Location = new Point(295, 336);
            cbbSelectProject.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            cbbSelectProject.Name = "cbbSelectProject";
            cbbSelectProject.Size = new Size(486, 40);
            cbbSelectProject.TabIndex = 12;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(72, 336);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(147, 46);
            label1.TabIndex = 13;
            label1.Text = "Project";
            // 
            // errMilestoneName
            // 
            errMilestoneName.ContainerControl = this;
            // 
            // AddMilestone
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 32);
            ClientSize = new Size(898, 574);
            Controls.Add(label1);
            Controls.Add(cbbSelectProject);
            Controls.Add(btnMilestoneCancel);
            Controls.Add(btnMilestoneConfirm);
            Controls.Add(dtpMilestonedate);
            Controls.Add(lalMilestoneDate);
            Controls.Add(txtbMilestoneName);
            Controls.Add(lblMilestoneName);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AddMilestone";
            Text = "AddMilestone";
            Load += AddMilestone_Load;
            ((System.ComponentModel.ISupportInitialize)errMilestoneName).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMilestoneName;
        private System.Windows.Forms.TextBox txtbMilestoneName;
        private System.Windows.Forms.Label lalMilestoneDate;
        private System.Windows.Forms.DateTimePicker dtpMilestonedate;
        private ReaLTaiizor.Controls.Button btnMilestoneConfirm;
        private ReaLTaiizor.Controls.Button btnMilestoneCancel;
        private System.Windows.Forms.ComboBox cbbSelectProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errMilestoneName;
    }
}