namespace Baygani.Screens
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.PasswodTextBox = new System.Windows.Forms.TextBox();
            this.UserNamecomboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnstatus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PasswodTextBox
            // 
            this.PasswodTextBox.Location = new System.Drawing.Point(11, 66);
            this.PasswodTextBox.Name = "PasswodTextBox";
            this.PasswodTextBox.PasswordChar = '*';
            this.PasswodTextBox.Size = new System.Drawing.Size(173, 26);
            this.PasswodTextBox.TabIndex = 1;
            // 
            // UserNamecomboBox
            // 
            this.UserNamecomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.UserNamecomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UserNamecomboBox.FormattingEnabled = true;
            this.UserNamecomboBox.Location = new System.Drawing.Point(11, 34);
            this.UserNamecomboBox.Name = "UserNamecomboBox";
            this.UserNamecomboBox.Size = new System.Drawing.Size(173, 26);
            this.UserNamecomboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "نام کاربری";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "کلمه عبور";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(106, 112);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(79, 29);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "ورود";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(11, 112);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(79, 29);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "انصراف";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(136, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "وضعیت ارتباط با پایگاه داده";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label4.Location = new System.Drawing.Point(26, 166);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(104, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "در حال بررسی ...";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnstatus
            // 
            this.btnstatus.BackColor = System.Drawing.Color.Yellow;
            this.btnstatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnstatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnstatus.Location = new System.Drawing.Point(12, 168);
            this.btnstatus.Name = "btnstatus";
            this.btnstatus.Size = new System.Drawing.Size(16, 16);
            this.btnstatus.TabIndex = 16;
            this.btnstatus.UseVisualStyleBackColor = false;
            // 
            // Login
            // 
            this.AcceptButton = this.LoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 188);
            this.Controls.Add(this.btnstatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserNamecomboBox);
            this.Controls.Add(this.PasswodTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ورود کاربران";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswodTextBox;
        private System.Windows.Forms.ComboBox UserNamecomboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoginButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnstatus;
    }
}