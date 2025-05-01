

using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

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
            ConnectTimeout = 30//���ӳ�ʱ
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
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //ʹ������ע��͹���ע��ûɶ����
                        command.Connection = conn;
                        command.CommandText = "INSERT INTO [dbo].[Aticle]([Title])VALUES(@title)";
                        SqlParameter param = new SqlParameter("@title", SqlDbType.NChar, 250);//����Ҳ���Բ�ָ��,����������@��ͷ
                        param.Direction = ParameterDirection.Input;//���������Ĭ����Input
                        param.Value = "�����µı���" + DateTime.Now;
                        command.Parameters.Add(param);
                        command.CommandTimeout = 10;//Ĭ����30��
                        int res = command.ExecuteNonQuery();//�ǲ�ѯ��������Ӱ���������
                        MessageBox.Show(res.ToString());
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
                        param.Value = 11;
                        command.Parameters.Add(param);
                        command.CommandTimeout = 10;//Ĭ����30��
                        object res = command.ExecuteScalar();
                        if (res == DBNull.Value || res == null)
                            MessageBox.Show("��ѯ���Ϊ��");
                        else
                            MessageBox.Show(res.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
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
                        command.CommandText = "UPDATE [dbo].[Aticle]SET [Title] = '�±���'\r\n WHERE Title='�±���1'";//���з���Ӱ��Sql����ִ��
                        command.CommandTimeout = 10;//Ĭ����30��
                        int res = command.ExecuteNonQuery();//��ʹ��������ǰ����ͬ��Ҳ�᷵����Ӱ������Ϊ1
                        MessageBox.Show(res.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int UpdateCommand<T>(T instance, string DbName = "") where T : class
        {

            // ��ȡ�����������
            Type type = typeof(T);

            //PropertyInfo idProperty =;
            //if (idProperty == null)
            //{
            //    throw new InvalidOperationException("δ�ҵ��������� 'Id'�����б���Ҫʵ�ִ���Id������");
            //}

            // ��ȡ�����������
            PropertyInfo[] PIs = type.GetProperties();

            // ����������Ϊ����
            string tableName = DbName ?? type.Name;

            // ���� SET �Ӿ�
            string setClause = string.Join(", ", PIs
               .Where(pi => pi.Name != "Id" || pi.GetValue(instance) != null) // ����������Ϊ Id������������
               .Select(pi => $"{pi.Name} = @{pi.Name}"));
            SqlParameterCollection sqlParameters = 
            string whereClause = $"Id = @Id";

            // ���������� SQL ���
            string sql = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        //ʹ������ע��͹���ע��ûɶ����
                        command.Connection = conn;
                        command.CommandText = sql;
                        command.CommandTimeout = 10;//Ĭ����30��
                        command.Parameters.Add();

                        int resCount = command.ExecuteNonQuery();
                        MessageBox.Show(resCount.ToString());
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
