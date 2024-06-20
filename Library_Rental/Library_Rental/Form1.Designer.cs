namespace Library_Rental
{
    partial class Form1
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
            this.Create_User_btn = new System.Windows.Forms.Button();
            this.login_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Create_User_btn
            // 
            this.Create_User_btn.Location = new System.Drawing.Point(33, 96);
            this.Create_User_btn.Name = "Create_User_btn";
            this.Create_User_btn.Size = new System.Drawing.Size(124, 23);
            this.Create_User_btn.TabIndex = 1;
            this.Create_User_btn.Text = "Create User";
            this.Create_User_btn.UseVisualStyleBackColor = true;
            this.Create_User_btn.Click += new System.EventHandler(this.Create_User_btn_Click);
            // 
            // login_button
            // 
            this.login_button.Location = new System.Drawing.Point(33, 31);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(124, 23);
            this.login_button.TabIndex = 0;
            this.login_button.Text = "Log in";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 164);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.Create_User_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Create_User_btn;
        private System.Windows.Forms.Button login_button;
    }
}

