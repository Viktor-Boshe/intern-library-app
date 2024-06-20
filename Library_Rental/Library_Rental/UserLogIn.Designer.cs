namespace Library_Rental
{
    partial class UserLogIn
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
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.LogIn_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(12, 28);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(150, 22);
            this.username_textbox.TabIndex = 0;
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(12, 87);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(150, 22);
            this.password_textbox.TabIndex = 1;
            // 
            // LogIn_Button
            // 
            this.LogIn_Button.Location = new System.Drawing.Point(12, 123);
            this.LogIn_Button.Name = "LogIn_Button";
            this.LogIn_Button.Size = new System.Drawing.Size(75, 23);
            this.LogIn_Button.TabIndex = 2;
            this.LogIn_Button.Text = "log in";
            this.LogIn_Button.UseVisualStyleBackColor = true;
            this.LogIn_Button.Click += new System.EventHandler(this.LogIn_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "password";
            // 
            // UserLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 170);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogIn_Button);
            this.Controls.Add(this.password_textbox);
            this.Controls.Add(this.username_textbox);
            this.Name = "UserLogIn";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "UserLogIn";
            this.Load += new System.EventHandler(this.UserLogIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.Button LogIn_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}