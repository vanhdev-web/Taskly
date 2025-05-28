using static ReaLTaiizor.Drawing.Poison.PoisonPaint;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Taskly
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnLogin = new Button();
            btnRegister = new Button();
            TopPanel = new Panel();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            Logintext = new Label();
            appName = new Label();
            label3 = new Label();
            description = new Label();
            panel1 = new Panel();
            TopPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(377, 83);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(229, 24);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(377, 146);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(229, 24);
            txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(280, 83);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 3;
            label1.Text = "User name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(280, 146);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(273, 212);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(114, 39);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(477, 212);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(139, 39);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // TopPanel
            // 
            TopPanel.BackColor = Color.FromArgb(30, 30, 32);
            TopPanel.BorderStyle = BorderStyle.FixedSingle;
            TopPanel.Controls.Add(nightControlBox1);
            TopPanel.Controls.Add(Logintext);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.ForeColor = SystemColors.Control;
            TopPanel.Location = new Point(0, 0);
            TopPanel.Margin = new Padding(3, 5, 3, 5);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(682, 101);
            TopPanel.TabIndex = 16;
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
            nightControlBox1.Location = new Point(543, 0);
            nightControlBox1.MaximizeHoverColor = Color.Transparent;
            nightControlBox1.MaximizeHoverForeColor = Color.Transparent;
            nightControlBox1.MinimizeHoverColor = Color.Transparent;
            nightControlBox1.MinimizeHoverForeColor = Color.Transparent;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 20;
            // 
            // Logintext
            // 
            Logintext.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Logintext.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Logintext.ForeColor = Color.FromArgb(63, 150, 252);
            Logintext.Location = new Point(264, 29);
            Logintext.Name = "Logintext";
            Logintext.Size = new Size(139, 41);
            Logintext.TabIndex = 19;
            Logintext.Text = "Login";
            Logintext.TextAlign = ContentAlignment.MiddleCenter;
            Logintext.Click += Logintext_Click;
            // 
            // appName
            // 
            appName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            appName.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            appName.ForeColor = SystemColors.ControlLightLight;
            appName.Location = new Point(51, 33);
            appName.Name = "appName";
            appName.Size = new Size(126, 74);
            appName.TabIndex = 20;
            appName.Text = "Welcome to ";
            appName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 32F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(63, 150, 252);
            label3.Location = new Point(37, 97);
            label3.Name = "label3";
            label3.Size = new Size(140, 96);
            label3.TabIndex = 21;
            label3.Text = "TASKLY";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // description
            // 
            description.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            description.ForeColor = SystemColors.ActiveBorder;
            description.Location = new Point(-36, 163);
            description.Margin = new Padding(4, 0, 4, 0);
            description.Name = "description";
            description.Size = new Size(298, 30);
            description.TabIndex = 22;
            description.Text = "Let your productivity flow";
            description.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(description);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(appName);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(btnRegister);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 101);
            panel1.Name = "panel1";
            panel1.Size = new Size(682, 304);
            panel1.TabIndex = 23;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = Color.FromArgb(30, 30, 32);
            ClientSize = new Size(682, 405);
            Controls.Add(TopPanel);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inbox";
            Load += Login_Load;
            TopPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label label1;
        private Label label2;
        private Button btnLogin;
        private Button btnRegister;
        private Panel TopPanel;
        private Label Logintext;
        private Label appName;
        private Label label3;
        private Label description;
        private Panel panel1;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}