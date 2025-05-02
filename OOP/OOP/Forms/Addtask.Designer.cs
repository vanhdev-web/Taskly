namespace OOP.Forms // Changed namespace from OOP to OOP.Forms
{
    partial class Addtask
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
            lbInputName = new System.Windows.Forms.Label();
            txtbInputNameTask = new System.Windows.Forms.TextBox();
            lbDeadline = new System.Windows.Forms.Label();
            dtpNewTask = new System.Windows.Forms.DateTimePicker();
            btnConfirm = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            btnAddinProject = new System.Windows.Forms.Label();
            cbbSelectProject = new System.Windows.Forms.ComboBox();
            TopPanel = new System.Windows.Forms.Panel();
            WelcomeName = new System.Windows.Forms.Label();
            cbbAssignedUser = new System.Windows.Forms.ComboBox();
            lblAssignUser = new System.Windows.Forms.Label();
            errLoginTask = new System.Windows.Forms.ErrorProvider(components);
            TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errLoginTask).BeginInit();
            SuspendLayout();
            // 
            // lbInputName
            // 
            lbInputName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            lbInputName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lbInputName.Location = new System.Drawing.Point(91, 126);
            lbInputName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lbInputName.Name = "lbInputName";
            lbInputName.Size = new System.Drawing.Size(277, 50);
            lbInputName.TabIndex = 0;
            lbInputName.Text = "Nhập tên nhiệm vụ";
            lbInputName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbInputNameTask
            // 
            txtbInputNameTask.Location = new System.Drawing.Point(430, 126);
            txtbInputNameTask.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            txtbInputNameTask.Name = "txtbInputNameTask";
            txtbInputNameTask.Size = new System.Drawing.Size(624, 39);
            txtbInputNameTask.TabIndex = 1;
            txtbInputNameTask.Validating += txtbInputNameTask_Validating;
            // 
            // lbDeadline
            // 
            lbDeadline.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            lbDeadline.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lbDeadline.Location = new System.Drawing.Point(91, 243);
            lbDeadline.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lbDeadline.Name = "lbDeadline";
            lbDeadline.Size = new System.Drawing.Size(277, 50);
            lbDeadline.TabIndex = 6;
            lbDeadline.Text = "Deadline";
            lbDeadline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNewTask
            // 
            dtpNewTask.Location = new System.Drawing.Point(430, 243);
            dtpNewTask.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            dtpNewTask.Name = "dtpNewTask";
            dtpNewTask.Size = new System.Drawing.Size(624, 39);
            dtpNewTask.TabIndex = 7;
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            btnConfirm.Location = new System.Drawing.Point(231, 587);
            btnConfirm.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new System.Drawing.Size(178, 94);
            btnConfirm.TabIndex = 8;
            btnConfirm.Text = "Xác nhận";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            btnCancel.Location = new System.Drawing.Point(761, 587);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(178, 94);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += button1_Click;
            // 
            // btnAddinProject
            // 
            btnAddinProject.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            btnAddinProject.ForeColor = System.Drawing.SystemColors.Control;
            btnAddinProject.Location = new System.Drawing.Point(91, 352);
            btnAddinProject.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            btnAddinProject.Name = "btnAddinProject";
            btnAddinProject.Size = new System.Drawing.Size(277, 50);
            btnAddinProject.TabIndex = 10;
            btnAddinProject.Text = "Project";
            btnAddinProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbSelectProject
            // 
            cbbSelectProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbbSelectProject.FormattingEnabled = true;
            cbbSelectProject.Location = new System.Drawing.Point(430, 352);
            cbbSelectProject.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            cbbSelectProject.Name = "cbbSelectProject";
            cbbSelectProject.Size = new System.Drawing.Size(624, 40);
            cbbSelectProject.TabIndex = 11;
            cbbSelectProject.SelectionChangeCommitted += cbbSelectProject_SelectionChangeCommitted;
            // 
            // TopPanel
            // 
            TopPanel.BackColor = System.Drawing.Color.FromArgb(30, 30, 32);
            TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            TopPanel.Controls.Add(WelcomeName);
            TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            TopPanel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            TopPanel.Location = new System.Drawing.Point(0, 0);
            TopPanel.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new System.Drawing.Size(1143, 85);
            TopPanel.TabIndex = 12;
            // 
            // WelcomeName
            // 
            WelcomeName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            WelcomeName.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            WelcomeName.ForeColor = System.Drawing.Color.FromArgb(63, 150, 252);
            WelcomeName.Location = new System.Drawing.Point(451, -2);
            WelcomeName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            WelcomeName.Name = "WelcomeName";
            WelcomeName.Size = new System.Drawing.Size(241, 82);
            WelcomeName.TabIndex = 1;
            WelcomeName.Text = "Add task";
            WelcomeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbAssignedUser
            // 
            cbbAssignedUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbbAssignedUser.FormattingEnabled = true;
            cbbAssignedUser.Location = new System.Drawing.Point(430, 456);
            cbbAssignedUser.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            cbbAssignedUser.Name = "cbbAssignedUser";
            cbbAssignedUser.Size = new System.Drawing.Size(624, 40);
            cbbAssignedUser.TabIndex = 14;
            cbbAssignedUser.Click += cbbAssignedUser_Click;
            // 
            // lblAssignUser
            // 
            lblAssignUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 163);
            lblAssignUser.ForeColor = System.Drawing.SystemColors.Control;
            lblAssignUser.Location = new System.Drawing.Point(91, 456);
            lblAssignUser.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lblAssignUser.Name = "lblAssignUser";
            lblAssignUser.Size = new System.Drawing.Size(277, 50);
            lblAssignUser.TabIndex = 13;
            lblAssignUser.Text = "Assign User";
            lblAssignUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errLoginTask
            // 
            errLoginTask.ContainerControl = this;
            // 
            // Addtask
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(30, 30, 32);
            ClientSize = new System.Drawing.Size(1143, 763);
            Controls.Add(cbbAssignedUser);
            Controls.Add(lblAssignUser);
            Controls.Add(TopPanel);
            Controls.Add(cbbSelectProject);
            Controls.Add(btnAddinProject);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(dtpNewTask);
            Controls.Add(lbDeadline);
            Controls.Add(txtbInputNameTask);
            Controls.Add(lbInputName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            Name = "Addtask";
            Text = "Addtask";
            Load += Addtask_Load;
            TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errLoginTask).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInputName;
        private System.Windows.Forms.TextBox txtbInputNameTask;
        private System.Windows.Forms.Label lbDeadline;
        private System.Windows.Forms.DateTimePicker dtpNewTask;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label btnAddinProject;
        private System.Windows.Forms.ComboBox cbbSelectProject;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label WelcomeName;
        private System.Windows.Forms.ComboBox cbbAssignedUser;
        private System.Windows.Forms.Label lblAssignUser;
        private System.Windows.Forms.ErrorProvider errLoginTask;
    }
}