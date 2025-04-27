

using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO02
{
    public partial class SQLServerTestForm : Form
    {
        public SQLServerTestForm()
        {
            InitializeComponent();

        }

        #region ����
        private SqlConnectionStringBuilder ConnectStr = new SqlConnectionStringBuilder()
        {
            Pooling = true,
            DataSource = ".",
            InitialCatalog = "02ADO",
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            ConnectTimeout = 30,


        };
        #endregion

        private void btn_Connection_Click(object sender, EventArgs e)
        {
            //������ӱ��ص�string connectionString = "Data Source=.;Initial Catalog=DB;Integrated Security=true;Trust Server Certificate=True;";
            //SqlConnection conn = new SqlConnection(connectionString);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            #region �ɹ����ӵ����ݿ�����
            //builder.DataSource = ".";
            //builder.DataSource = "tcp:1116-SL,1433";//����ַ�����������
            //builder.DataSource = "tcp:(local)";//���Ҳ���ԣ�֮ǰ���п�����δ���÷���ǽ��ԭ��
            //builder.DataSource = "np:(local)";//OK
            //builder.DataSource = @"1116-SL";//OK
            //builder.DataSource = "1116-SL,1433";//OK
            //builder.DataSource = "tcp:1116-SL";//OK
            //builder.DataSource = @"tcp:127.0.0.1,1433";//OK
            //builder.DataSource = "127.0.0.1,1433\\MSSQLSERVER";//OK,�ǻ����൱���ұ�������������
            //������Ҫô��IP��ַ��Ҫô��������
            #endregion
            //builder.DataSource = "tcp:1116-SL\\MSSQLSERVER";//NG��ԭ���������޷�����ȷ����ΪIP��ַ
            builder.DataSource = "(local)";
            builder.InitialCatalog = "02ADO";
            builder.UserID = "xiaoliu";
            builder.Password = "123";
            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = 1000;
            SqlConnection conn = new SqlConnection(builder.ConnectionString);
            conn.Open();
            //����ʹ�����ݿ����ӽ��в���
            conn.Close();

            SqlConnectionStringBuilder builder1 = new SqlConnectionStringBuilder();
            builder1.IntegratedSecurity = true;
            builder1.InitialCatalog = "02ADO";
            builder1.TrustServerCertificate = true;

            //Ҳ����ʹ��using
            using (SqlConnection conn1 = new SqlConnection(builder1.ConnectionString))
            {
                try
                {
                    conn1.Open();
                    string cmdText = "SELECT COUNT(1) FROM [02ADO].[dbo].[Aticle]";

                    SqlCommand command = new SqlCommand(cmdText, conn1);//ҲҪ�ͷ�
                    object res = command.ExecuteScalar();//ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С� ���������л��С�����ķ��ؽ������Ϊ�ա�
                    if (res == DBNull.Value || res == null)
                        MessageBox.Show("��ѯ���Ϊ��");
                    else
                        MessageBox.Show(res.ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            try
            {
                //��ʹ�в����쳣�������Դ��Ȼ����������
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    //throw new Exception("sdsd");
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "";
                        command.Connection = conn;

                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void bt_SetCommandTimeOut_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    //throw new Exception("sdsd");
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //ʹ������ע��͹���ע��ûɶ����
                        command.Connection = conn;
                        command.CommandText = "select 1";
                        command.CommandTimeout = 10;//Ĭ����30��
                        object obj = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bt_SetCommandParas_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //ʹ������ע��͹���ע��ûɶ����
                        command.Connection = conn;
                        command.CommandText = "SELECT [Id], [Title], [AddTime] FROM [dbo].[Aticle] WHERE id = @id";
                        SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 16);//����Ҳ���Բ�ָ��,����������@id��ͷ
                        param.Direction = ParameterDirection.Input;//���������Ĭ����Input
                        param.Value = 1;
                        command.Parameters.Add(param);
                        command.CommandTimeout = 10;//Ĭ����30��
                        object obj = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
