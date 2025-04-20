

using Microsoft.Data.SqlClient;

namespace ADO02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Connection_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=dbtest;User ID=sa;pwd=123456;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //这里使用数据库连接
            conn.Close();

            //也可以使用using
            using (SqlConnection conn1 = new SqlConnection(connectionString))
            {
                conn1.Open();
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {

        }
    }
}
