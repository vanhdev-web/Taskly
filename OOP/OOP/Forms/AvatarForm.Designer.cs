using System.Drawing;
using System.Windows.Forms;

namespace OOP
{
    partial class AvatarForm
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
            btnBrowse = new Button();
            btnSkip = new Button();
            btnSave = new Button();
            TopPanel = new Panel();
            WelcomeName = new Label();
            pbAvatar = new PictureBox();
            TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(243, 295);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(106, 29);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSkip
            // 
            btnSkip.Location = new Point(162, 342);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(106, 29);
            btnSkip.TabIndex = 2;
            btnSkip.Text = "Skip";
            btnSkip.UseVisualStyleBackColor = true;
            btnSkip.Click += btnSkip_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(326, 342);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(106, 29);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // TopPanel
            // 
            TopPanel.BackColor = Color.FromArgb(30, 30, 32);
            TopPanel.BorderStyle = BorderStyle.FixedSingle;
            TopPanel.Controls.Add(WelcomeName);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.ForeColor = SystemColors.Control;
            TopPanel.Location = new Point(0, 0);
            TopPanel.Margin = new Padding(3, 5, 3, 5);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(577, 82);
            TopPanel.TabIndex = 16;
            // 
            // WelcomeName
            // 
            WelcomeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WelcomeName.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            WelcomeName.ForeColor = Color.FromArgb(63, 150, 252);
            WelcomeName.Location = new Point(139, 8);
            WelcomeName.Name = "WelcomeName";
            WelcomeName.Size = new Size(305, 51);
            WelcomeName.TabIndex = 19;
            WelcomeName.Text = "Avatar";
            WelcomeName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbAvatar
            // 
            pbAvatar.Location = new Point(206, 102);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.Size = new Size(190, 158);
            pbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            pbAvatar.TabIndex = 0;
            pbAvatar.TabStop = false;
            // 
            // AvatarForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(30, 30, 32);
            ClientSize = new Size(577, 400);
            ControlBox = false;
            Controls.Add(TopPanel);
            Controls.Add(btnSave);
            Controls.Add(btnSkip);
            Controls.Add(btnBrowse);
            Controls.Add(pbAvatar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AvatarForm";
            Text = "AvatarForm";
            TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private PictureBox pbAvatar;
        private Button btnBrowse;
        private Button btnSkip;
        private Button btnSave;
        private Panel TopPanel;
        private Label WelcomeName;
    }
}