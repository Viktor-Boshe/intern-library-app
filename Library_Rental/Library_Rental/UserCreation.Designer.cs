namespace Library_Rental
{
    partial class UserCreation
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
            this.Create_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(37, 50);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(147, 22);
            this.username_textbox.TabIndex = 0;
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(37, 91);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(147, 22);
            this.password_textbox.TabIndex = 1;
            // 
            // Create_btn
            // 
            this.Create_btn.Location = new System.Drawing.Point(37, 152);
            this.Create_btn.Name = "Create_btn";
            this.Create_btn.Size = new System.Drawing.Size(75, 23);
            this.Create_btn.TabIndex = 2;
            this.Create_btn.Text = "Create";
            this.Create_btn.UseVisualStyleBackColor = true;
            this.Create_btn.Click += new System.EventHandler(this.Create_btn_Click);
            // 
            // UserCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Create_btn);
            this.Controls.Add(this.password_textbox);
            this.Controls.Add(this.username_textbox);
            this.Name = "UserCreation";
            this.Text = "UserCreation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.Button Create_btn;
    }
}