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
            cbbSelectProject = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            errMilestoneName = new System.Windows.Forms.ErrorProvider(components);
            btnMilestoneConfirm = new System.Windows.Forms.Button();
            btnMilestoneCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)errMilestoneName).BeginInit();
            SuspendLayout();
            // 
            // lblMilestoneName
            // 
            lblMilestoneName.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblMilestoneName.ForeColor = SystemColors.ButtonHighlight;
            lblMilestoneName.Location = new Point(39, 49);
            lblMilestoneName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblMilestoneName.Name = "lblMilestoneName";
            lblMilestoneName.Size = new Size(105, 22);
            lblMilestoneName.TabIndex = 0;
            lblMilestoneName.Text = "Nhập Milestone";
            // 
            // txtbMilestoneName
            // 
            txtbMilestoneName.Location = new Point(159, 49);
            txtbMilestoneName.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            txtbMilestoneName.Name = "txtbMilestoneName";
            txtbMilestoneName.Size = new Size(264, 23);
            txtbMilestoneName.TabIndex = 1;
            txtbMilestoneName.Validating += txtbMilestoneName_Validating;
            // 
            // lalMilestoneDate
            // 
            lalMilestoneDate.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lalMilestoneDate.ForeColor = SystemColors.ButtonHighlight;
            lalMilestoneDate.Location = new Point(39, 100);
            lalMilestoneDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lalMilestoneDate.Name = "lalMilestoneDate";
            lalMilestoneDate.Size = new Size(79, 22);
            lalMilestoneDate.TabIndex = 2;
            lalMilestoneDate.Text = "Nhập ngày";
            lalMilestoneDate.Click += label1_Click;
            // 
            // dtpMilestonedate
            // 
            dtpMilestonedate.Location = new Point(159, 101);
            dtpMilestonedate.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            dtpMilestonedate.Name = "dtpMilestonedate";
            dtpMilestonedate.Size = new Size(264, 23);
            dtpMilestonedate.TabIndex = 3;
            // 
            // cbbSelectProject
            // 
            cbbSelectProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbbSelectProject.FormattingEnabled = true;
            cbbSelectProject.Location = new Point(159, 158);
            cbbSelectProject.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            cbbSelectProject.Name = "cbbSelectProject";
            cbbSelectProject.Size = new Size(264, 23);
            cbbSelectProject.TabIndex = 12;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(39, 158);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(79, 22);
            label1.TabIndex = 13;
            label1.Text = "Project";
            // 
            // errMilestoneName
            // 
            errMilestoneName.ContainerControl = this;
            // 
            // btnMilestoneConfirm
            // 
            btnMilestoneConfirm.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnMilestoneConfirm.Location = new Point(99, 212);
            btnMilestoneConfirm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            btnMilestoneConfirm.Name = "btnMilestoneConfirm";
            btnMilestoneConfirm.Size = new Size(114, 26);
            btnMilestoneConfirm.TabIndex = 14;
            btnMilestoneConfirm.Text = "OK";
            btnMilestoneConfirm.UseVisualStyleBackColor = true;
            btnMilestoneConfirm.Click += btnMilestoneConfirm_Click;
            // 
            // btnMilestoneCancel
            // 
            btnMilestoneCancel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnMilestoneCancel.Location = new Point(266, 212);
            btnMilestoneCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            btnMilestoneCancel.Name = "btnMilestoneCancel";
            btnMilestoneCancel.Size = new Size(114, 26);
            btnMilestoneCancel.TabIndex = 15;
            btnMilestoneCancel.Text = "Cancel";
            btnMilestoneCancel.UseVisualStyleBackColor = true;
            btnMilestoneCancel.Click += btnMilestoneCancel_Click;
            // 
            // AddMilestone
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 32);
            ClientSize = new Size(484, 269);
            Controls.Add(btnMilestoneCancel);
            Controls.Add(btnMilestoneConfirm);
            Controls.Add(label1);
            Controls.Add(cbbSelectProject);
            Controls.Add(dtpMilestonedate);
            Controls.Add(lalMilestoneDate);
            Controls.Add(txtbMilestoneName);
            Controls.Add(lblMilestoneName);
            Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
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
        private ReaLTaiizor.Controls.Button d;
      
        private System.Windows.Forms.ComboBox cbbSelectProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errMilestoneName;
        private System.Windows.Forms.Button btnMilestoneCancel;
        private System.Windows.Forms.Button btnMilestoneConfirm;
    }
}