

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
                        command.CommandText = "UPDATE [dbo].[Aticle] SET [Title] = @title\r\n WHERE id=@id";
                        //���з���Ӱ��Sql����ִ��,�����ַ�����Ҫ�ֺţ����Ǳ���@Title����Ҫ��
                        command.CommandTimeout = 10;//Ĭ����30��

                        SqlParameter param = new SqlParameter("@Title", SqlDbType.NChar, 250);//@title���������ϸ����ִ�Сд
                        param.Direction = ParameterDirection.Input;
                        param.Value = "�����¡���''�ı���" + DateTime.Now.ToString();//����ƴ��Sql����Ҫת��
                        command.Parameters.Add(param);

                        //command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt).Value = 4);//���������УҲ̫low����
                        SqlParameter param1 = new SqlParameter("@Id", SqlDbType.BigInt, 250);
                        param.Direction = ParameterDirection.Input;
                        param1.Value = 4;
                        command.Parameters.Add(param1);

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

        private void bt_Transaction_Click(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        using (SqlTransaction st = conn.BeginTransaction())
                        {

                            SqlParameter param = new SqlParameter("@Title", SqlDbType.NChar, 250);//ͬһ�������б������ܶ������
                            param.Direction = ParameterDirection.Input;
                            command.Parameters.Add(param);
                            command.Transaction = st;//���������ֵ��ȥ��
                            try
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    string temp = string.Empty;
                                    //if (i == 5)
                                    //{
                                    //    temp = "shgfdsyhgfhjsd";//���챨��
                                    //}
                                    command.Connection = conn;
                                    command.CommandText = temp + "INSERT INTO [dbo].[Aticle]([Title])VALUES(@title)";
                                    param.Value = i + "�����¡���''�ı���" + DateTime.Now.ToString();

                                    int res = command.ExecuteNonQuery();
                                    result += $"��{i}����Ӱ��������{res}\r\n";
                                }
                                st.Commit();
                                MessageBox.Show(result);
                            }
                            catch (Exception ex)
                            {
                                st.Rollback();
                                string msg = $"�������ʧ�ܣ����ݿ��Ѿ��ع��������쳣����{ex.GetType().FullName}���쳣��Ϣ:{ex.Message}";
                                MessageBox.Show(msg);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void bt_SqlDataReader_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM [02ADO].[dbo].[Aticle]";
                    command.Connection = conn;
                    SqlParameter param = new SqlParameter("@Title", SqlDbType.NChar, 250);//ͬһ�������б������ܶ������
                    param.Direction = ParameterDirection.Input;
                    param.Value = "�����¡���''�ı���" + DateTime.Now.ToString();
                    //command.Parameters.Add(param);
                    var res = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (res.Read())//����¼ǰ������һ�������
                                      //res.NextResult()����ȡ������ Transact-SQL ���Ľ��ʱ��ʹ���ݶ�ȡ��ǰ������һ�������
                    {
                        MessageBox.Show(string.Format("{0},{1},{2}", res[0], res[1], res[2]));
                    }
                    res.Close();
                }
            }
        }

        private void bt_SqlDataAdapter_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter sd = new SqlDataAdapter("select * FROM [02ADO].[dbo].[Aticle]", conn);
                sd.Fill(ds, "newtable");//newtable��DataTable�ı�����
                //this.dataGridView1.DataSource = ds.Tables[0];//����
                DataTable? dataTable = ds.Tables["newtable"];
                if (dataTable != null)
                {
                    this.dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void bt_�洢����_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "myProc";//myProc�Ǵ洢���̵�����
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;//�洢������Ҫ�������
                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 8);
                    param.Direction = ParameterDirection.Input;
                    param.Value = 4;
                    command.Parameters.Add(param);

                    var res = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (res.Read())
                    {
                        MessageBox.Show(string.Format("{0},{1},{2}", res[0], res[1], res[2]));
                    }
                    res.Close();
                }
            }
        }

        private void bt_�洢����_DataSet_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "myProc";//myProc�Ǵ洢���̵�����
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;//�洢������Ҫ�������
                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 8);
                    param.Direction = ParameterDirection.Input;
                    param.Value = 4;
                    command.Parameters.Add(param);

                    DataSet ds = new DataSet();
                    SqlDataAdapter sd = new SqlDataAdapter();
                    sd.SelectCommand = command;

                    sd.Fill(ds, "newtable");//newtable��DataTable�ı�����
                                            //this.dataGridView1.DataSource = ds.Tables[0];//����
                    DataTable? dataTable = ds.Tables["newtable"];
                    if (dataTable != null)
                    {
                        this.dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void bt_�洢����_Ӱ������_Click(object sender, EventArgs e)
        {

        }

        private int UpdateCommand<T>(T instance, string DbName = "") where T : class
        {
            return 0;
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
                        //PIs.Select(pi => command.Parameters.Add($"@{pi.Name}",)) ;

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
