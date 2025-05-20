using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO02
{
    public partial class DatGridView隔行变色And添加选择框 : Form
    {
        public DatGridView隔行变色And添加选择框()
        {
            InitializeComponent();
        }

        private SqlConnectionStringBuilder ConnectStr = new SqlConnectionStringBuilder()
        {
            Pooling = true,
            DataSource = ".",
            InitialCatalog = "02ADO",
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            ConnectTimeout = 30//连接超时
        };

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Yellow;//获取或设置应用于 DataGridView 的奇数行的默认单元格样式。这里设置背景色，好像没有按照上下去更新，要么奇数行优先级更高
            //dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightSkyBlue;//获取或设置 DataGridView 单元格的背景色
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//设置对齐方式
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//根据Header和所有单元格的内容自动调整行的高度
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//根据Header和所有单元格的内容自动调整列的宽度                                                                              
            dataGridView1.Font = new Font("宋体", 16);//设置字体
            //dataGridView1.CurrentCell = dataGridView1[1, 1];// 设定 (1, 1) 为当前单元格 


            //dataGridView1.AllowUserToDeleteRows = false;

            //bt_InsertOne_Click(null, null);
            //bt_QueryDatas_Click(null, null);
            timer1.Enabled = true;
        }

        private void bt_InsertOne_Click(object sender, EventArgs e)
        {
            string executeStr = "INSERT INTO [dbo].[DataGridViewTest] ([OccuranceTime],[Content]) VALUES(@occuretime ,@content)";
            using (SqlConnection conn = new SqlConnection(ConnectStr.ToString()))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = executeStr;
                    sqlCommand.CommandTimeout = 10;


                    SqlParameter parameter = new SqlParameter("@occuretime", SqlDbType.DateTime);
                    parameter.Value = DateTime.Now;
                    sqlCommand.Parameters.Add(parameter);

                    SqlParameter parameter1 = new SqlParameter("@content", SqlDbType.NVarChar, 255);
                    parameter1.Value = $"时间：【{DateTime.Now.ToString("HH:mm:ss fff")}】";
                    sqlCommand.Parameters.Add(parameter1);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private void bt_QueryDatas_Click(object sender, EventArgs e)
        {
            string executeStr = "SELECT [Id], [OccuranceTime], [Content] FROM [dbo].[DataGridViewTest]";
            using (SqlConnection conn = new SqlConnection(ConnectStr.ToString()))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = executeStr;
                    sqlCommand.CommandTimeout = 1;//这个设置好像就失效了

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = sqlCommand;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt != null)
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.CurrentCell = dataGridView1[0, 0];// 设定 (1, 1) 为当前单元格，设置选择单元格
                    }
                }
            }
        }

        /// <summary>
        /// 设置隔行变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //设置每一行的数据
            //遍历表格中的每一行
            foreach (DataGridViewRow row in dataGridView1.Rows)//DataGridViewRow不是DataRow
            {
                //需要获取当当前遍历的所在行
                int index = row.Index;
                //判断这个索引的奇偶性
                if (index % 2 == 1)
                {
                    //正常来算，对2取余数等于1，是奇数
                    //索引从0开始，行和列都是从0开始
                    //所以这是偶数行
                    row.DefaultCellStyle.BackColor = Color.Azure;
                }
                else
                {
                    //计数行
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
            }
        }

        /// <summary>
        /// 给DataGridView一列选择框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridViewCheckBoxColumn.HeaderText = "选择列";
            dataGridViewCheckBoxColumn.Name = "SelectColoumn";


            //if (this.dataGridView1.Columns.Contains(dataGridViewCheckBoxColumn))
            //{
            //    return;//这个写法有问题
            //}

            if (this.dataGridView1.Columns.Contains("SelectColoumn"))
            {
                return;
            }

            dataGridView1.Columns.Insert(0, dataGridViewCheckBoxColumn);//插入到第一列,这样添加的一列，即使DataDource改变仍然在。
        }

        /// <summary>
        /// 设置行和列只读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //dataGridView1.ReadOnly = true;//设置所有的单元格只读

            #region 设置所有单元格只读，删除可以，但新增不可以操作了。
            //foreach (DataGridViewBand band in dataGridView1.Columns)//[DataGridViewBand]是 和 [DataGridViewColumn] 类的[DataGridViewRow]基类。
            //{
            //    band.ReadOnly = true;
            //}
            #endregion

            #region 设置某行或某列自读
            #region 失败1
            // 设置 DataGridView1 的第2列整列单元格为只读
            //dataGridView1.Columns[1].ReadOnly = true;
            // 设置 DataGridView1 的第3行整行单元格为只读
            //dataGridView1.Rows[2].ReadOnly = true;//这样设置没有效果，因为行的只读属性优先级更高
            #endregion

            #region 成功1      
            Init();

            void Init()
            {
                // 设置第2列（索引1）为只读
                dataGridView1.Columns[1].ReadOnly = true;

                // 设置第3行（索引2）为只读
                SetRowReadOnly(2, true);

                // 注册事件确保动态更新只读状态
                dataGridView1.RowsAdded += DataGridView1_RowsAdded;
                dataGridView1.DataSourceChanged += DataGridView1_DataSourceChanged;
            }

            void SetRowReadOnly(int rowIndex, bool readOnly)
            {
                if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                {
                    foreach (DataGridViewCell cell in dataGridView1.Rows[rowIndex].Cells)
                    {
                        cell.ReadOnly = readOnly;//这里的遍历是核心
                    }
                }
            }

            void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
            {
                // 确保新添加的行也保持只读设置
                if (e.RowIndex == 2) // 如果添加的是第3行
                {
                    SetRowReadOnly(2, true);
                }
            }

            void DataGridView1_DataSourceChanged(object sender, EventArgs e)
            {
                // 数据源变更时重新设置只读状态
                SetRowReadOnly(2, true);
            }
            #endregion
            #endregion

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;//不想显示该新行，可以将 DataGridView 对象的 AllowUserToAddRows 属性设置为 False。
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows[0].HeaderCell.Value = "第一行";//设置行头，需改数据源自动消失\
            dataGridView1.RowHeadersVisible = false;// 隐藏行头
            dataGridView1.ColumnHeadersVisible = false; // 列头隐藏 
            dataGridView1.Columns[0].Visible = false;    // DataGridView1的第一列隐藏 
            dataGridView1.Rows[0].Visible = false;       // DataGridView1的第一行隐藏 
        }
    }
}
