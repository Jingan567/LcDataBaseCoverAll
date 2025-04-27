namespace ADO02
{
    partial class SQLServerTestForm
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
            bt_SetCommandTimeOut = new Button();
            bt_SetCommandParas = new Button();
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
            btn_Insert.Location = new Point(12, 50);
            btn_Insert.Name = "btn_Insert";
            btn_Insert.Size = new Size(92, 32);
            btn_Insert.TabIndex = 1;
            btn_Insert.Text = "插入数据";
            btn_Insert.UseVisualStyleBackColor = true;
            btn_Insert.Click += btn_Insert_Click;
            // 
            // bt_SetCommandTimeOut
            // 
            bt_SetCommandTimeOut.Location = new Point(12, 88);
            bt_SetCommandTimeOut.Name = "bt_SetCommandTimeOut";
            bt_SetCommandTimeOut.Size = new Size(92, 52);
            bt_SetCommandTimeOut.TabIndex = 2;
            bt_SetCommandTimeOut.Text = "设置命令超时时间";
            bt_SetCommandTimeOut.UseVisualStyleBackColor = true;
            bt_SetCommandTimeOut.Click += bt_SetCommandTimeOut_Click;
            // 
            // bt_SetCommandParas
            // 
            bt_SetCommandParas.Location = new Point(12, 146);
            bt_SetCommandParas.Name = "bt_SetCommandParas";
            bt_SetCommandParas.Size = new Size(92, 52);
            bt_SetCommandParas.TabIndex = 3;
            bt_SetCommandParas.Text = "设置命令参数";
            bt_SetCommandParas.UseVisualStyleBackColor = true;
            bt_SetCommandParas.Click += bt_SetCommandParas_Click;
            // 
            // SQLServerTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bt_SetCommandParas);
            Controls.Add(bt_SetCommandTimeOut);
            Controls.Add(btn_Insert);
            Controls.Add(btn_Connection);
            Name = "SQLServerTestForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Connection;
        private Button btn_Insert;
        private Button bt_SetCommandTimeOut;
        private Button button1;
        private Button bt_SetCommandParas;
    }
}
