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
            btn_Update = new Button();
            bt_Transaction = new Button();
            bt_SqlDataReader = new Button();
            bt_SqlDataAdapter = new Button();
            dataGridView1 = new DataGridView();
            bt_存储过程 = new Button();
            bt_存储过程_DataSet = new Button();
            bt_存储过程_影响行数 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            // btn_Update
            // 
            btn_Update.Location = new Point(12, 204);
            btn_Update.Name = "btn_Update";
            btn_Update.Size = new Size(92, 32);
            btn_Update.TabIndex = 4;
            btn_Update.Text = "更新数据";
            btn_Update.UseVisualStyleBackColor = true;
            btn_Update.Click += btn_Update_Click;
            // 
            // bt_Transaction
            // 
            bt_Transaction.Location = new Point(12, 242);
            bt_Transaction.Name = "bt_Transaction";
            bt_Transaction.Size = new Size(92, 32);
            bt_Transaction.TabIndex = 5;
            bt_Transaction.Text = "事务";
            bt_Transaction.UseVisualStyleBackColor = true;
            bt_Transaction.Click += bt_Transaction_Click;
            // 
            // bt_SqlDataReader
            // 
            bt_SqlDataReader.Location = new Point(12, 280);
            bt_SqlDataReader.Name = "bt_SqlDataReader";
            bt_SqlDataReader.Size = new Size(92, 46);
            bt_SqlDataReader.TabIndex = 6;
            bt_SqlDataReader.Text = "SqlDataReader";
            bt_SqlDataReader.UseVisualStyleBackColor = true;
            bt_SqlDataReader.Click += bt_SqlDataReader_Click;
            // 
            // bt_SqlDataAdapter
            // 
            bt_SqlDataAdapter.Location = new Point(12, 332);
            bt_SqlDataAdapter.Name = "bt_SqlDataAdapter";
            bt_SqlDataAdapter.Size = new Size(92, 46);
            bt_SqlDataAdapter.TabIndex = 7;
            bt_SqlDataAdapter.Text = "SqlDataAdapter";
            bt_SqlDataAdapter.UseVisualStyleBackColor = true;
            bt_SqlDataAdapter.Click += bt_SqlDataAdapter_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(130, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(658, 366);
            dataGridView1.TabIndex = 8;
            // 
            // bt_存储过程
            // 
            bt_存储过程.Location = new Point(12, 384);
            bt_存储过程.Name = "bt_存储过程";
            bt_存储过程.Size = new Size(92, 46);
            bt_存储过程.TabIndex = 9;
            bt_存储过程.Text = "执行存储过程";
            bt_存储过程.UseVisualStyleBackColor = true;
            bt_存储过程.Click += bt_存储过程_Click;
            // 
            // bt_存储过程_DataSet
            // 
            bt_存储过程_DataSet.Location = new Point(130, 384);
            bt_存储过程_DataSet.Name = "bt_存储过程_DataSet";
            bt_存储过程_DataSet.Size = new Size(117, 46);
            bt_存储过程_DataSet.TabIndex = 10;
            bt_存储过程_DataSet.Text = "执行存储过程，返回DataSet";
            bt_存储过程_DataSet.UseVisualStyleBackColor = true;
            bt_存储过程_DataSet.Click += bt_存储过程_DataSet_Click;
            // 
            // bt_存储过程_影响行数
            // 
            bt_存储过程_影响行数.Location = new Point(266, 384);
            bt_存储过程_影响行数.Name = "bt_存储过程_影响行数";
            bt_存储过程_影响行数.Size = new Size(117, 46);
            bt_存储过程_影响行数.TabIndex = 11;
            bt_存储过程_影响行数.Text = "执行存储过程，返回影响行数";
            bt_存储过程_影响行数.UseVisualStyleBackColor = true;
            bt_存储过程_影响行数.Click += bt_存储过程_影响行数_Click;
            // 
            // button2
            // 
            button2.Location = new Point(401, 384);
            button2.Name = "button2";
            button2.Size = new Size(117, 46);
            button2.TabIndex = 12;
            button2.Text = "Test";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // SQLServerTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(bt_存储过程_影响行数);
            Controls.Add(bt_存储过程_DataSet);
            Controls.Add(bt_存储过程);
            Controls.Add(dataGridView1);
            Controls.Add(bt_SqlDataAdapter);
            Controls.Add(bt_SqlDataReader);
            Controls.Add(bt_Transaction);
            Controls.Add(btn_Update);
            Controls.Add(bt_SetCommandParas);
            Controls.Add(bt_SetCommandTimeOut);
            Controls.Add(btn_Insert);
            Controls.Add(btn_Connection);
            Name = "SQLServerTestForm";
            Text = "SQLServerTestForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Connection;
        private Button btn_Insert;
        private Button bt_SetCommandTimeOut;
        private Button button1;
        private Button bt_SetCommandParas;
        private Button btn_Update;
        private Button bt_Transaction;
        private Button bt_SqlDataReader;
        private Button bt_SqlDataAdapter;
        private DataGridView dataGridView1;
        private Button bt_存储过程;
        private Button bt_存储过程_DataSet;
        private Button bt_存储过程_影响行数;
        private Button button2;
    }
}
