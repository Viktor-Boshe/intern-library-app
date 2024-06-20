namespace Library_Rental
{
    partial class Library
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
            this.books_list = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.EditLibraryButton = new System.Windows.Forms.Button();
            this.ReturnBook_button = new System.Windows.Forms.Button();
            this.searchBox_title = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DetailsBox = new System.Windows.Forms.RichTextBox();
            this.AuthorNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Search_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // books_list
            // 
            this.books_list.FormattingEnabled = true;
            this.books_list.ItemHeight = 16;
            this.books_list.Location = new System.Drawing.Point(12, 71);
            this.books_list.Name = "books_list";
            this.books_list.Size = new System.Drawing.Size(301, 164);
            this.books_list.TabIndex = 3;
            this.books_list.SelectedIndexChanged += new System.EventHandler(this.books_list_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Rent Book";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditLibraryButton
            // 
            this.EditLibraryButton.Location = new System.Drawing.Point(219, 241);
            this.EditLibraryButton.Name = "EditLibraryButton";
            this.EditLibraryButton.Size = new System.Drawing.Size(94, 30);
            this.EditLibraryButton.TabIndex = 5;
            this.EditLibraryButton.Text = "EditLibrary";
            this.EditLibraryButton.UseVisualStyleBackColor = true;
            this.EditLibraryButton.Visible = false;
            // 
            // ReturnBook_button
            // 
            this.ReturnBook_button.Location = new System.Drawing.Point(380, 241);
            this.ReturnBook_button.Name = "ReturnBook_button";
            this.ReturnBook_button.Size = new System.Drawing.Size(106, 29);
            this.ReturnBook_button.TabIndex = 6;
            this.ReturnBook_button.Text = "Return rental";
            this.ReturnBook_button.UseVisualStyleBackColor = true;
            this.ReturnBook_button.Click += new System.EventHandler(this.ReturnBook_button_Click);
            // 
            // searchBox_title
            // 
            this.searchBox_title.Location = new System.Drawing.Point(12, 27);
            this.searchBox_title.Name = "searchBox_title";
            this.searchBox_title.Size = new System.Drawing.Size(141, 22);
            this.searchBox_title.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "List of books";
            // 
            // DetailsBox
            // 
            this.DetailsBox.Location = new System.Drawing.Point(380, 121);
            this.DetailsBox.Name = "DetailsBox";
            this.DetailsBox.ReadOnly = true;
            this.DetailsBox.Size = new System.Drawing.Size(308, 114);
            this.DetailsBox.TabIndex = 8;
            this.DetailsBox.TabStop = false;
            this.DetailsBox.Text = "";
            // 
            // AuthorNameBox
            // 
            this.AuthorNameBox.Location = new System.Drawing.Point(380, 71);
            this.AuthorNameBox.Name = "AuthorNameBox";
            this.AuthorNameBox.ReadOnly = true;
            this.AuthorNameBox.Size = new System.Drawing.Size(308, 22);
            this.AuthorNameBox.TabIndex = 7;
            this.AuthorNameBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "Book Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "Book Author";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(159, 27);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(141, 22);
            this.textBox2.TabIndex = 1;
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(306, 25);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(79, 24);
            this.Search_button.TabIndex = 2;
            this.Search_button.Text = "Search";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "Search by Title";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 33;
            this.label5.Text = "Search by Author";
            // 
            // Library
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 283);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Search_button);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AuthorNameBox);
            this.Controls.Add(this.DetailsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox_title);
            this.Controls.Add(this.ReturnBook_button);
            this.Controls.Add(this.EditLibraryButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.books_list);
            this.Name = "Library";
            this.Text = "Library";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox books_list;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button EditLibraryButton;
        private System.Windows.Forms.Button ReturnBook_button;
        private System.Windows.Forms.TextBox searchBox_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox DetailsBox;
        private System.Windows.Forms.TextBox AuthorNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}