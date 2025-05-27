using static ReaLTaiizor.Drawing.Poison.PoisonPaint;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OOP
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
            Logintext = new Label();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
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
            txtUsername.Location = new Point(585, 108);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(229, 24);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(585, 169);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(229, 24);
            txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(438, 108);
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
            label2.Location = new Point(438, 169);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(516, 308);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 43);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(673, 308);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(123, 43);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // TopPanel
            // 
            TopPanel.BackColor = Color.FromArgb(30, 30, 32);
            TopPanel.BorderStyle = BorderStyle.FixedSingle;
            TopPanel.Controls.Add(Logintext);
            TopPanel.Controls.Add(nightControlBox1);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.ForeColor = SystemColors.Control;
            TopPanel.Location = new Point(0, 0);
            TopPanel.Margin = new Padding(3, 5, 3, 5);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(889, 60);
            TopPanel.TabIndex = 16;
            // 
            // Logintext
            // 
            Logintext.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Logintext.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Logintext.ForeColor = Color.Cornsilk;
            Logintext.Location = new Point(127, -1);
            Logintext.Name = "Logintext";
            Logintext.Size = new Size(617, 51);
            Logintext.TabIndex = 19;
            Logintext.Text = "Login";
            Logintext.TextAlign = ContentAlignment.MiddleCenter;
            Logintext.Click += Logintext_Click;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.Cursor = Cursors.Hand;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = true;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(750, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 1;
            // 
            // appName
            // 
            appName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            appName.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            appName.ForeColor = SystemColors.ControlLightLight;
            appName.Location = new Point(24, 109);
            appName.Name = "appName";
            appName.Size = new Size(324, 74);
            appName.TabIndex = 20;
            appName.Text = "Welcome to ";
            appName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 32F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(16, 158);
            label3.Name = "label3";
            label3.Size = new Size(347, 96);
            label3.TabIndex = 21;
            label3.Text = "FLOWHUB";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // description
            // 
            description.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            description.ForeColor = SystemColors.ActiveBorder;
            description.Location = new Point(42, 254);
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
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtUsername);
            panel1.Location = new Point(0, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(889, 363);
            panel1.TabIndex = 23;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = Color.FromArgb(30, 30, 32);
            ClientSize = new Size(889, 403);
            Controls.Add(description);
            Controls.Add(label3);
            Controls.Add(appName);
            Controls.Add(TopPanel);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Login";
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
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private Label appName;
        private Label label3;
        private Label description;
        private Panel panel1;
    }
}