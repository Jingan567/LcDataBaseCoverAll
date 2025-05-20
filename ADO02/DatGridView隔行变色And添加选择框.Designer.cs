namespace ADO02
{
    partial class DatGridView隔行变色And添加选择框
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            button4 = new Button();
            button2 = new Button();
            button3 = new Button();
            button1 = new Button();
            bt_QueryDatas = new Button();
            bt_InsertOne = new Button();
            dataGridView1 = new DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Size = new Size(1309, 820);
            splitContainer1.SplitterDistance = 190;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(button5, 0, 0);
            tableLayoutPanel1.Controls.Add(button4, 0, 0);
            tableLayoutPanel1.Controls.Add(button2, 0, 0);
            tableLayoutPanel1.Controls.Add(button3, 0, 0);
            tableLayoutPanel1.Controls.Add(button1, 0, 0);
            tableLayoutPanel1.Controls.Add(bt_QueryDatas, 0, 0);
            tableLayoutPanel1.Controls.Add(bt_InsertOne, 0, 0);
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Size = new Size(165, 333);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // button4
            // 
            button4.Dock = DockStyle.Fill;
            button4.Location = new Point(8, 213);
            button4.Margin = new Padding(8);
            button4.Name = "button4";
            button4.Size = new Size(149, 25);
            button4.TabIndex = 7;
            button4.Text = "清除下方新增一行";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(8, 254);
            button2.Margin = new Padding(8);
            button2.Name = "button2";
            button2.Size = new Size(149, 25);
            button2.TabIndex = 6;
            button2.Text = "添加一列选择框";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(8, 131);
            button3.Margin = new Padding(8);
            button3.Name = "button3";
            button3.Size = new Size(149, 25);
            button3.TabIndex = 5;
            button3.Text = "设置行和列只读";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(8, 8);
            button1.Margin = new Padding(8);
            button1.Name = "button1";
            button1.Size = new Size(149, 25);
            button1.TabIndex = 2;
            button1.Text = "设置隔行变色";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // bt_QueryDatas
            // 
            bt_QueryDatas.Dock = DockStyle.Fill;
            bt_QueryDatas.Location = new Point(8, 49);
            bt_QueryDatas.Margin = new Padding(8);
            bt_QueryDatas.Name = "bt_QueryDatas";
            bt_QueryDatas.Size = new Size(149, 25);
            bt_QueryDatas.TabIndex = 1;
            bt_QueryDatas.Text = "查询表数据";
            bt_QueryDatas.UseVisualStyleBackColor = true;
            bt_QueryDatas.Click += bt_QueryDatas_Click;
            // 
            // bt_InsertOne
            // 
            bt_InsertOne.Dock = DockStyle.Fill;
            bt_InsertOne.Location = new Point(8, 90);
            bt_InsertOne.Margin = new Padding(8);
            bt_InsertOne.Name = "bt_InsertOne";
            bt_InsertOne.Size = new Size(149, 25);
            bt_InsertOne.TabIndex = 0;
            bt_InsertOne.Text = "新增一条数据";
            bt_InsertOne.UseVisualStyleBackColor = true;
            bt_InsertOne.Click += bt_InsertOne_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1115, 820);
            dataGridView1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Fill;
            button5.Location = new Point(8, 172);
            button5.Margin = new Padding(8);
            button5.Name = "button5";
            button5.Size = new Size(149, 25);
            button5.TabIndex = 8;
            button5.Text = "隐藏行和列";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // DatGridView隔行变色And添加选择框
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1309, 820);
            Controls.Add(splitContainer1);
            Name = "DatGridView隔行变色And添加选择框";
            Text = "DatGridView隔行变色And添加选择框";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private Button bt_InsertOne;
        private Button bt_QueryDatas;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}