namespace ADO02
{
    partial class Form1
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
            btn_Connection = new Button();
            btn_Insert = new Button();
            SuspendLayout();
            // 
            // btn_Connection
            // 
            btn_Connection.Location = new Point(12, 12);
            btn_Connection.Name = "btn_Connection";
            btn_Connection.Size = new Size(92, 32);
            btn_Connection.TabIndex = 0;
            btn_Connection.Text = "数据库连接";
            btn_Connection.UseVisualStyleBackColor = true;
            btn_Connection.Click += btn_Connection_Click;
            // 
            // btn_Insert
            // 
            btn_Insert.Location = new Point(12, 61);
            btn_Insert.Name = "btn_Insert";
            btn_Insert.Size = new Size(92, 32);
            btn_Insert.TabIndex = 1;
            btn_Insert.Text = "插入数据";
            btn_Insert.UseVisualStyleBackColor = true;
            btn_Insert.Click += btn_Insert_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_Insert);
            Controls.Add(btn_Connection);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Connection;
        private Button btn_Insert;
    }
}
