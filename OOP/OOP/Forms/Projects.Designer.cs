﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Taskly
{
    partial class Projects
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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Projects));
            projectPanel = new Panel();
            btnReport = new Button();
            label6 = new Label();
            projectContainer = new Panel();
            panel11 = new Panel();
            comboBox1 = new ComboBox();
            btnDeleteProject = new Button();
            btnCreateProject = new Button();
            sidebar = new FlowLayoutPanel();
            panel4 = new ReaLTaiizor.Controls.Panel();
            button4 = new ReaLTaiizor.Controls.Button();
            panel5 = new ReaLTaiizor.Controls.Panel();
            button5 = new ReaLTaiizor.Controls.Button();
            menu = new ReaLTaiizor.Controls.Panel();
            button7 = new ReaLTaiizor.Controls.Button();
            panel2 = new ReaLTaiizor.Controls.Panel();
            btnUser = new ReaLTaiizor.Controls.Button();
            panel7 = new ReaLTaiizor.Controls.Panel();
            btnExit = new ReaLTaiizor.Controls.Button();
            panel3 = new Panel();
            memberPanel = new Panel();
            description = new PlaceholderTextBox();
            btnAddMember = new ReaLTaiizor.Controls.Button();
            projectName = new Label();
            projectRole = new Label();
            TopPanel = new Panel();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            label3 = new Label();
            btnHam = new PictureBox();
            WelcomeName = new Label();
            panel1 = new Panel();
            AllTasks = new Label();
            taskContainer = new Panel();
            projectPanel.SuspendLayout();
            projectContainer.SuspendLayout();
            panel11.SuspendLayout();
            sidebar.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            menu.SuspendLayout();
            panel2.SuspendLayout();
            panel7.SuspendLayout();
            panel3.SuspendLayout();
            TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnHam).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // projectPanel
            // 
            projectPanel.BackColor = Color.FromArgb(30, 30, 32);
            projectPanel.BorderStyle = BorderStyle.FixedSingle;
            projectPanel.Controls.Add(btnReport);
            projectPanel.Controls.Add(label6);
            projectPanel.Controls.Add(projectContainer);
            projectPanel.Controls.Add(btnDeleteProject);
            projectPanel.Controls.Add(btnCreateProject);
            projectPanel.Location = new Point(192, 63);
            projectPanel.Name = "projectPanel";
            projectPanel.Size = new Size(743, 61);
            projectPanel.TabIndex = 10;
            // 
            // btnReport
            // 
            btnReport.Location = new Point(621, 13);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(81, 32);
            btnReport.TabIndex = 9;
            btnReport.Text = "Report";
            btnReport.UseVisualStyleBackColor = true;
            btnReport.Click += btnReport_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(3, 12);
            label6.Name = "label6";
            label6.Size = new Size(116, 25);
            label6.TabIndex = 2;
            label6.Text = "My Projects";
            // 
            // projectContainer
            // 
            projectContainer.Controls.Add(panel11);
            projectContainer.Location = new Point(140, -1);
            projectContainer.Name = "projectContainer";
            projectContainer.Size = new Size(275, 83);
            projectContainer.TabIndex = 5;
            projectContainer.Paint += projectContainer_Paint;
            // 
            // panel11
            // 
            panel11.Controls.Add(comboBox1);
            panel11.Location = new Point(14, 8);
            panel11.Name = "panel11";
            panel11.Size = new Size(246, 49);
            panel11.TabIndex = 5;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 9);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(240, 27);
            comboBox1.TabIndex = 4;
            comboBox1.Text = " Project List";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnDeleteProject
            // 
            btnDeleteProject.Location = new Point(524, 13);
            btnDeleteProject.Name = "btnDeleteProject";
            btnDeleteProject.Size = new Size(74, 32);
            btnDeleteProject.TabIndex = 8;
            btnDeleteProject.Text = "Delete";
            btnDeleteProject.UseVisualStyleBackColor = true;
            btnDeleteProject.Click += button3_Click;
            // 
            // btnCreateProject
            // 
            btnCreateProject.Location = new Point(426, 13);
            btnCreateProject.Name = "btnCreateProject";
            btnCreateProject.Size = new Size(73, 32);
            btnCreateProject.TabIndex = 7;
            btnCreateProject.Text = "Create";
            btnCreateProject.UseVisualStyleBackColor = true;
            btnCreateProject.Click += btnCreateProject_Click;
            // 
            // sidebar
            // 
            sidebar.BackColor = Color.FromArgb(30, 30, 32);
            sidebar.BorderStyle = BorderStyle.FixedSingle;
            sidebar.Controls.Add(panel4);
            sidebar.Controls.Add(panel5);
            sidebar.Controls.Add(menu);
            sidebar.Controls.Add(panel2);
            sidebar.Controls.Add(panel7);
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 55);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(167, 461);
            sidebar.TabIndex = 11;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(30, 30, 32);
            panel4.Controls.Add(button4);
            panel4.EdgeColor = Color.Transparent;
            panel4.ForeColor = SystemColors.ControlLightLight;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(13, 5, 4, 5);
            panel4.Size = new Size(255, 51);
            panel4.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            panel4.TabIndex = 4;
            panel4.Text = "Dashboard";
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BorderColor = Color.Transparent;
            button4.Cursor = Cursors.Hand;
            button4.Dock = DockStyle.Fill;
            button4.EnteredBorderColor = Color.Transparent;
            button4.EnteredColor = Color.Transparent;
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button4.Image = Properties.Resources.Home__2_;
            button4.ImageAlign = ContentAlignment.TopLeft;
            button4.InactiveColor = Color.Transparent;
            button4.Location = new Point(13, 5);
            button4.Name = "button4";
            button4.Padding = new Padding(87, 0, 0, 0);
            button4.PressedBorderColor = Color.Transparent;
            button4.PressedColor = Color.Transparent;
            button4.Size = new Size(238, 41);
            button4.TabIndex = 2;
            button4.Text = "              Home";
            button4.TextAlignment = StringAlignment.Near;
            button4.Click += btnHome_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(30, 30, 32);
            panel5.Controls.Add(button5);
            panel5.EdgeColor = Color.Transparent;
            panel5.ForeColor = SystemColors.ControlLightLight;
            panel5.Location = new Point(3, 60);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(13, 5, 4, 5);
            panel5.Size = new Size(255, 51);
            panel5.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            panel5.TabIndex = 4;
            panel5.Text = "Dashboard";
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.BorderColor = Color.Transparent;
            button5.Cursor = Cursors.Hand;
            button5.Dock = DockStyle.Fill;
            button5.EnteredBorderColor = Color.Transparent;
            button5.EnteredColor = Color.Transparent;
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button5.Image = Properties.Resources.image__1___1_1;
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.InactiveColor = Color.Transparent;
            button5.Location = new Point(13, 5);
            button5.Name = "button5";
            button5.Padding = new Padding(87, 0, 0, 0);
            button5.PressedBorderColor = Color.Transparent;
            button5.PressedColor = Color.Transparent;
            button5.Size = new Size(238, 41);
            button5.TabIndex = 2;
            button5.Text = "              Task";
            button5.TextAlignment = StringAlignment.Near;
            button5.Click += btnTask_Click;
            // 
            // menu
            // 
            menu.BackColor = Color.FromArgb(63, 150, 252);
            menu.Controls.Add(button7);
            menu.EdgeColor = Color.Transparent;
            menu.ForeColor = Color.Transparent;
            menu.Location = new Point(0, 114);
            menu.Margin = new Padding(0);
            menu.Name = "menu";
            menu.Padding = new Padding(13, 5, 4, 5);
            menu.Size = new Size(255, 51);
            menu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            menu.TabIndex = 3;
            menu.Text = "Dashboard";
            // 
            // button7
            // 
            button7.BackColor = Color.Transparent;
            button7.BorderColor = Color.Transparent;
            button7.Cursor = Cursors.Hand;
            button7.Dock = DockStyle.Fill;
            button7.EnteredBorderColor = Color.Transparent;
            button7.EnteredColor = Color.Transparent;
            button7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button7.Image = Properties.Resources.image;
            button7.ImageAlign = ContentAlignment.MiddleLeft;
            button7.InactiveColor = Color.Transparent;
            button7.Location = new Point(13, 5);
            button7.Name = "button7";
            button7.Padding = new Padding(87, 0, 0, 0);
            button7.PressedBorderColor = Color.Transparent;
            button7.PressedColor = Color.Transparent;
            button7.Size = new Size(238, 41);
            button7.TabIndex = 2;
            button7.Text = "               Project";
            button7.TextAlignment = StringAlignment.Near;
            button7.Click += btnProject_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(30, 30, 32);
            panel2.Controls.Add(btnUser);
            panel2.EdgeColor = Color.Transparent;
            panel2.ForeColor = SystemColors.ControlLightLight;
            panel2.Location = new Point(3, 168);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(13, 5, 4, 5);
            panel2.Size = new Size(255, 51);
            panel2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            panel2.TabIndex = 9;
            panel2.Text = "Dashboard";
            // 
            // btnUser
            // 
            btnUser.BackColor = Color.Transparent;
            btnUser.BorderColor = Color.Transparent;
            btnUser.Cursor = Cursors.Hand;
            btnUser.Dock = DockStyle.Fill;
            btnUser.EnteredBorderColor = Color.Transparent;
            btnUser.EnteredColor = Color.Transparent;
            btnUser.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnUser.Image = (Image)resources.GetObject("btnUser.Image");
            btnUser.ImageAlign = ContentAlignment.MiddleLeft;
            btnUser.InactiveColor = Color.Transparent;
            btnUser.Location = new Point(13, 5);
            btnUser.Name = "btnUser";
            btnUser.Padding = new Padding(87, 0, 0, 0);
            btnUser.PressedBorderColor = Color.Transparent;
            btnUser.PressedColor = Color.Transparent;
            btnUser.Size = new Size(238, 41);
            btnUser.TabIndex = 2;
            btnUser.Text = "              User";
            btnUser.TextAlignment = StringAlignment.Near;
            btnUser.Click += btnUser_Click;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(30, 30, 32);
            panel7.Controls.Add(btnExit);
            panel7.EdgeColor = Color.Transparent;
            panel7.ForeColor = Color.White;
            panel7.Location = new Point(3, 225);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(13, 5, 4, 5);
            panel7.Size = new Size(255, 51);
            panel7.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            panel7.TabIndex = 8;
            panel7.Text = "Dashboard";
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.BorderColor = Color.Transparent;
            btnExit.Cursor = Cursors.Hand;
            btnExit.Dock = DockStyle.Fill;
            btnExit.EnteredBorderColor = Color.Transparent;
            btnExit.EnteredColor = Color.Transparent;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnExit.Image = Properties.Resources.exit;
            btnExit.ImageAlign = ContentAlignment.MiddleLeft;
            btnExit.InactiveColor = Color.Transparent;
            btnExit.Location = new Point(13, 5);
            btnExit.Name = "btnExit";
            btnExit.Padding = new Padding(87, 0, 0, 0);
            btnExit.PressedBorderColor = Color.Transparent;
            btnExit.PressedColor = Color.Transparent;
            btnExit.Size = new Size(238, 41);
            btnExit.TabIndex = 2;
            btnExit.Text = "              Exit";
            btnExit.TextAlignment = StringAlignment.Near;
            btnExit.Click += btnExit_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(30, 30, 32);
            panel3.Controls.Add(memberPanel);
            panel3.Controls.Add(description);
            panel3.Controls.Add(btnAddMember);
            panel3.Controls.Add(projectName);
            panel3.Controls.Add(projectRole);
            panel3.Location = new Point(185, 129);
            panel3.Name = "panel3";
            panel3.Size = new Size(732, 171);
            panel3.TabIndex = 9;
            panel3.Paint += panel3_Paint;
            // 
            // memberPanel
            // 
            memberPanel.AutoScroll = true;
            memberPanel.Location = new Point(354, 82);
            memberPanel.Margin = new Padding(3, 2, 3, 2);
            memberPanel.Name = "memberPanel";
            memberPanel.Size = new Size(366, 81);
            memberPanel.TabIndex = 6;
            memberPanel.Paint += panel2_Paint_1;
            // 
            // description
            // 
            description.BackColor = Color.FromArgb(30, 30, 32);
            description.Font = new Font("Segoe UI", 11F);
            description.ForeColor = Color.Gray;
            description.Location = new Point(8, 39);
            description.Margin = new Padding(3, 2, 3, 2);
            description.Multiline = true;
            description.Name = "description";
            description.PlaceholderText = "What's this project about";
            description.Size = new Size(281, 126);
            description.TabIndex = 5;
            description.Text = "What's this project about";
            description.TextChanged += description_TextChanged;
            description.KeyDown += description_KeyDown;
            // 
            // btnAddMember
            // 
            btnAddMember.BackColor = Color.Transparent;
            btnAddMember.BorderColor = Color.Transparent;
            btnAddMember.Cursor = Cursors.Hand;
            btnAddMember.EnteredBorderColor = Color.Transparent;
            btnAddMember.EnteredColor = Color.Transparent;
            btnAddMember.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnAddMember.Image = Properties.Resources.add_White;
            btnAddMember.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddMember.InactiveColor = Color.Transparent;
            btnAddMember.Location = new Point(354, 42);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.Padding = new Padding(87, 0, 0, 0);
            btnAddMember.PressedBorderColor = Color.Transparent;
            btnAddMember.PressedColor = Color.Transparent;
            btnAddMember.Size = new Size(151, 42);
            btnAddMember.TabIndex = 2;
            btnAddMember.Text = "           Add Member";
            btnAddMember.TextAlignment = StringAlignment.Near;
            btnAddMember.Click += button1_Click_1;
            // 
            // projectName
            // 
            projectName.AutoSize = true;
            projectName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            projectName.ForeColor = SystemColors.ButtonHighlight;
            projectName.Location = new Point(9, 0);
            projectName.Name = "projectName";
            projectName.Size = new Size(146, 32);
            projectName.TabIndex = 1;
            projectName.Text = "Description";
            projectName.Click += label1_Click;
            // 
            // projectRole
            // 
            projectRole.AutoSize = true;
            projectRole.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            projectRole.ForeColor = SystemColors.ButtonHighlight;
            projectRole.Location = new Point(354, 5);
            projectRole.Name = "projectRole";
            projectRole.Size = new Size(152, 32);
            projectRole.TabIndex = 2;
            projectRole.Text = "Project Role";
            projectRole.Click += label2_Click;
            // 
            // TopPanel
            // 
            TopPanel.BackColor = Color.FromArgb(30, 30, 32);
            TopPanel.BorderStyle = BorderStyle.FixedSingle;
            TopPanel.Controls.Add(nightControlBox1);
            TopPanel.Controls.Add(label3);
            TopPanel.Controls.Add(btnHam);
            TopPanel.Controls.Add(WelcomeName);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.ForeColor = SystemColors.Control;
            TopPanel.Location = new Point(0, 0);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(934, 55);
            TopPanel.TabIndex = 10;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.Cursor = Cursors.Hand;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.Transparent;
            nightControlBox1.DisableMinimizeColor = Color.Transparent;
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = false;
            nightControlBox1.EnableMaximizeColor = Color.Transparent;
            nightControlBox1.EnableMinimizeButton = false;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(858, 0);
            nightControlBox1.MaximizeHoverColor = Color.Transparent;
            nightControlBox1.MaximizeHoverForeColor = Color.Transparent;
            nightControlBox1.MinimizeHoverColor = Color.Transparent;
            nightControlBox1.MinimizeHoverForeColor = Color.Transparent;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Black", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.ForeColor = Color.FromArgb(63, 150, 252);
            label3.Location = new Point(59, 12);
            label3.Name = "label3";
            label3.Size = new Size(72, 25);
            label3.TabIndex = 2;
            label3.Text = "Taskly";
            // 
            // btnHam
            // 
            btnHam.Image = Properties.Resources.image__8_;
            btnHam.Location = new Point(19, 14);
            btnHam.Name = "btnHam";
            btnHam.Size = new Size(35, 27);
            btnHam.SizeMode = PictureBoxSizeMode.Zoom;
            btnHam.TabIndex = 1;
            btnHam.TabStop = false;
            // 
            // WelcomeName
            // 
            WelcomeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WelcomeName.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            WelcomeName.ForeColor = Color.FromArgb(63, 150, 252);
            WelcomeName.Location = new Point(165, 8);
            WelcomeName.Name = "WelcomeName";
            WelcomeName.Size = new Size(764, 38);
            WelcomeName.TabIndex = 1;
            WelcomeName.Text = "Welcome back";
            WelcomeName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(AllTasks);
            panel1.Controls.Add(taskContainer);
            panel1.Location = new Point(185, 164);
            panel1.Name = "panel1";
            panel1.Size = new Size(750, 409);
            panel1.TabIndex = 5;
            // 
            // AllTasks
            // 
            AllTasks.AutoSize = true;
            AllTasks.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            AllTasks.ForeColor = SystemColors.ButtonHighlight;
            AllTasks.Location = new Point(7, 139);
            AllTasks.Name = "AllTasks";
            AllTasks.Size = new Size(109, 32);
            AllTasks.TabIndex = 7;
            AllTasks.Text = "All tasks";
            // 
            // taskContainer
            // 
            taskContainer.AllowDrop = true;
            taskContainer.AutoScroll = true;
            taskContainer.BackColor = Color.FromArgb(30, 30, 32);
            taskContainer.BorderStyle = BorderStyle.FixedSingle;
            taskContainer.Location = new Point(7, 184);
            taskContainer.Margin = new Padding(3, 2, 3, 2);
            taskContainer.Name = "taskContainer";
            taskContainer.Padding = new Padding(11, 7, 0, 0);
            taskContainer.Size = new Size(726, 223);
            taskContainer.TabIndex = 23;
            // 
            // Projects
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 32);
            ClientSize = new Size(934, 516);
            ControlBox = false;
            Controls.Add(projectPanel);
            Controls.Add(sidebar);
            Controls.Add(panel3);
            Controls.Add(TopPanel);
            Controls.Add(panel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Projects";
            Text = "Projects";
            Load += Projects_Load;
            projectPanel.ResumeLayout(false);
            projectPanel.PerformLayout();
            projectContainer.ResumeLayout(false);
            panel11.ResumeLayout(false);
            sidebar.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            menu.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            TopPanel.ResumeLayout(false);
            TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnHam).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.Panel panel1;
        private Button btnDeleteProject;
        private Button btnCreateProject;
        private Panel TopPanel;
        private Label label3;
        private PictureBox btnHam;
        private Label WelcomeName;
        private FlowLayoutPanel sidebar;
        private ReaLTaiizor.Controls.Panel panel4;
        private ReaLTaiizor.Controls.Button button4;
        private ReaLTaiizor.Controls.Panel panel5;
        private ReaLTaiizor.Controls.Button button5;
        private ReaLTaiizor.Controls.Panel menu;
        private ReaLTaiizor.Controls.Button button7;
        private Panel panel3;
        private Label projectName;
        private Label projectRole;
        private Panel projectPanel;
        private Label label6;
        private Panel projectContainer;
        private Panel panel11;
        private ReaLTaiizor.Controls.Button btnAddMember;
        private PlaceholderTextBox description;
        private Panel memberPanel;
        private ComboBox comboBox1;
        private ReaLTaiizor.Controls.Panel panel7;
        private ReaLTaiizor.Controls.Button btnExit;
        private Label AllTasks;
        private Panel taskContainer;
        private ReaLTaiizor.Controls.Panel panel2;
        private ReaLTaiizor.Controls.Button btnUser;
        private Button btnReport;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}