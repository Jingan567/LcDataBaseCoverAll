using System.Collections;
using System.Data.SQLite;
using System.Data;
using System.Text;
using System.Windows;
using System.IO;

namespace SqliteTemplate.参考类库
{
    public class mFilePath
    {
        public static string BZ_DatabasePath = @"..\\..\\Database\\Data_BG.db";
    }
    public class SQLiteHelper_BZ
    {
        //数据库连接字符串(web.config来配置)，可以动态更改SQLString支持多数据库.
        public static string SQLitePath = @"D:\BZProjectPath.txt";
        //public static string connectionString  = "Data Source ="+ @"..\\..\\Database\Data_BG.db" + "; Version = 3;";
        public static string connectionString = "Data Source =" + mFilePath.BZ_DatabasePath + "; Version = 3;";
        public SQLiteHelper_BZ()
        {

        }
        /// <summary>
        /// 保存相对路径转觉得路径
        /// </summary>
        public static void SaveAbsolutePath()
        {
            string AbsPath = "Data Source =" + System.IO.Path.GetFullPath(mFilePath.BZ_DatabasePath) + "; Version = 3;";
            string RootPath = System.IO.Path.GetFullPath(@"..\\..\\");
            string[] PathList = { AbsPath, RootPath };
            File.WriteAllLines(SQLitePath, PathList, Encoding.Default);
        }

        /// <summary>
        /// 获取保存的数据库实际路径
        /// </summary>
        /// <param name="RowIndex">0 获取数据库路径 1 获取程序跟目录</param>
        /// <returns></returns>
        public static string GetAbsolutePath(int RowIndex)
        {
            string Retpath = "";
            if (File.Exists(SQLitePath))
            {
                string[] AbsPath = File.ReadAllLines(SQLitePath, Encoding.Default);
                Retpath = AbsPath[RowIndex].Trim();
            }
            return Retpath;

        }



        #region 公用方法

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params SQLiteParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region  执行简单SQL语句

        static object objExe = new object();
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            lock (objExe)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                    {
                        try
                        {
                            connection.Open();
                            int rows = cmd.ExecuteNonQuery();
                            return rows;
                        }
                        catch (System.Data.SQLite.SQLiteException E)
                        {
                            connection.Close();
                            throw new Exception(E.Message);
                        }
                        finally
                        {
                            cmd.Dispose();
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，设置命令的执行等待时间
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SQLite.SQLiteException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SQLite.SQLiteException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SQLite.SQLiteException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@fs", DbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SQLite.SQLiteException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader(使用该方法切记要手工关闭SQLiteDataReader和连接)
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public static SQLiteDataReader ExecuteReader(string strSQL)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
            try
            {
                connection.Open();
                SQLiteDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (System.Data.SQLite.SQLiteException e)
            {
                throw new Exception(e.Message);
            }
            //finally //不能在此关闭，否则，返回的对象将无法使用
            //{
            //	cmd.Dispose();
            //	connection.Close();
            //}	
        }
        static object objQuery = new object();
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            lock (objQuery)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        connection.Open();
                        SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                        command.Fill(ds, "ds");
                    }
                    catch (System.Data.SQLite.SQLiteException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        public static DataTable GetDataTable(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(dt);
                    command.Dispose();
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();

                }
                return dt;
            }
        }

        public static DataSet Query(string SQLString, string TableName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, TableName);
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet,设置命令的执行等待时间
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static DataSet Query(string SQLString, int Times)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader (使用该方法切记要手工关闭SQLiteDataReader和连接)
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public static SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SQLiteDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SQLite.SQLiteException e)
            {
                throw new Exception(e.Message);
            }
            //finally //不能在此关闭，否则，返回的对象将无法使用
            //{
            //	cmd.Dispose();
            //	connection.Close();
            //}	

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SQLite.SQLiteException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        public static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 参数转换
        /// <summary>
        /// 返回一个SQLiteParameter
        /// </summary>
        /// <param name="name">参数名字</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns>SQLiteParameter的值</returns>
        public static SQLiteParameter MakeSQLiteParameter(string name, DbType type, int size, object value)
        {
            SQLiteParameter parm = new SQLiteParameter(name, type, size);
            parm.Value = value;
            return parm;
        }

        public static SQLiteParameter MakeSQLiteParameter(string name, DbType type, object value)
        {
            SQLiteParameter parm = new SQLiteParameter(name, type);
            parm.Value = value;
            return parm;
        }

        /// <summary>
        /// 根据时间端查询小时报警统计
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public static DataTable GetTotalSpanTime(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            string sql = "select * from  DownTime where " +
                        " StartTime>= '" + StartTime + "' and StartTime<= '" + EndTime + "'  and SpanTimeMinutes is not null and SpanTimeMinutes!=''";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        /// <summary>
        /// 判断报警代码是否关闭，如果没关闭。不在插入新的同样报警
        /// </summary>
        /// <param name="ErrIndex"></param>
        /// <returns></returns>
        public static bool ErrIsClose(string ErrIndex)
        {

            DataTable dt = new DataTable();
            string sql = "select * from  DownTime where " +
                       " ErrIndex='" + ErrIndex + "' and (CloseTime is  null or CloseTime='')";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            if (dt.Rows.Count >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 临时存储SN进入时间
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        public static int MES_InputSn(string SN)
        {
            string sql = "delete from MES_SN where SN='" + SN.Trim() + "'";

            SQLiteHelper_BZ.ExecuteSql(sql); //删除原来的报警信息对照表数据


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MES_SN(");
            strSql.Append("SN,InputTime)");
            strSql.Append(" values (");
            strSql.Append("@SN,@InputTime)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@SN", DbType.String),
                    new SQLiteParameter("@InputTime", DbType.String)
                  };
            parameters[0].Value = SN.Trim().Trim();
            parameters[1].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);


            sql = "delete from MES_SN where InputTime<='" + DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            return SQLiteHelper_BZ.ExecuteSql(sql);


        }
        /// <summary>
        /// 查询SN进入时间
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        public static DateTime MES_FindTime(string SN)
        {
            DateTime InpuTime = DateTime.Now.AddMinutes(-2);
            DataTable dt = new DataTable();
            string sql = "select InputTime from  MES_SN where " + " SN='" + SN.Trim() + "'";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            if (dt.Rows.Count > 0)
            {
                InpuTime = Convert.ToDateTime(dt.Rows[0][0].ToString());

            }
            sql = "delete from MES_SN where SN='" + SN.Trim() + "'";
            SQLiteHelper_BZ.ExecuteSql(sql); //删除原来的报警信息对照表数据
            return InpuTime;
        }



        /// <summary>
        /// 获取报警信息
        /// </summary>
        /// <param name="ErrIndex"></param>
        /// <returns></returns>
        public static DataTable GetError()
        {

            DataTable dt = new DataTable();
            string sql = "select ErrCode,MessageCn,MessageEn,StartTime,CloseTime,DownTime.ErrIndex from  ErrCode,DownTime where  DownTime.ErrIndex= ErrCode.ErrIndex and Flag='0' and ErrCode.ErrCode<>''";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;
        }



        /// <summary>
        /// 对于错误时间跨度按小时进行插补
        /// </summary>
        /// <param name="ErrIndex">错误代码</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="CloseTime">关闭时间</param>
        public static void UpdateFlag(string ErrIndex, DateTime StartTime, DateTime CloseTime)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update  DownTime ");
            strSql.Append("Set Flag='1' where StartTime='" + StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  CloseTime='" + CloseTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and ErrIndex='" + ErrIndex + "'");

            //更新错误关闭时间
            SQLiteHelper_BZ.ExecuteSql(strSql.ToString());
        }
        public static DataTable GetErrDataRecent32()
        {
            DataTable dt = new DataTable();
            string sql = "select starttime,errcode.plcaddr,errindex,errclass2,messagecn,severity from DownTime,ErrCode where ErrCode.PLCAddr=DownTime.PLCAddr " +
                            " order by  starttime desc limit 32";
            /*
            "select InDate as '日期',ErrClass1 as '报警类别1',ErrClass2 as '报警类别2',
            StopClassCode as '停机分类代码',ErrCode as '报警代码',
            MessageCN as '中文描述',MessageEN as '英文描述',StartTime as '开始时间',
            CloseTime as '结束时间',SpanTime as '报警时长',LineName as '线名',StationName as '工位名',
            Severity as '报警级别',ErrCode.PLCAddr as 'PLC地址'
            from DownTime,ErrCode 
            where ErrCode.PLCAddr=DownTime.PLCAddr and StartTime>= '2022-04-27 00:00:00'and StartTime<= '2022-04-28 14:06:42' order by  ErrClass2 "

            */
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        public static DataTable GetErrData(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            string sql = "select starttime,errcode.plcaddr,errindex,errclass2,messagecn,severity from DownTime,ErrCode where ErrCode.PLCAddr=DownTime.PLCAddr " +
                            "and StartTime>= '" + StartTime + "'and StartTime<= '" + EndTime + "' order by  starttime desc";
            /*
            "select InDate as '日期',ErrClass1 as '报警类别1',ErrClass2 as '报警类别2',
            StopClassCode as '停机分类代码',ErrCode as '报警代码',
            MessageCN as '中文描述',MessageEN as '英文描述',StartTime as '开始时间',
            CloseTime as '结束时间',SpanTime as '报警时长',LineName as '线名',StationName as '工位名',
            Severity as '报警级别',ErrCode.PLCAddr as 'PLC地址'
            from DownTime,ErrCode 
            where ErrCode.PLCAddr=DownTime.PLCAddr and StartTime>= '2022-04-27 00:00:00'and StartTime<= '2022-04-28 14:06:42' order by  ErrClass2 "

            */
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        /// <summary>
        /// 根据时间端查询报警数据
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public static DataTable GetErrDatatable(string StartTime, string EndTime, string sqlwhere)
        {
            DataTable dt = new DataTable();
            string sql = "select " + sqlwhere + " from DownTime,ErrCode where ErrCode.PLCAddr=DownTime.PLCAddr " +
                            "and StartTime>= '" + StartTime + "'and StartTime<= '" + EndTime + "' order by  ErrClass2 ";
            /*
            "select InDate as '日期',ErrClass1 as '报警类别1',ErrClass2 as '报警类别2',
            StopClassCode as '停机分类代码',ErrCode as '报警代码',
            MessageCN as '中文描述',MessageEN as '英文描述',StartTime as '开始时间',
            CloseTime as '结束时间',SpanTime as '报警时长',LineName as '线名',StationName as '工位名',
            Severity as '报警级别',ErrCode.PLCAddr as 'PLC地址'
            from DownTime,ErrCode 
            where ErrCode.PLCAddr=DownTime.PLCAddr and StartTime>= '2022-04-27 00:00:00'and StartTime<= '2022-04-28 14:06:42' order by  ErrClass2 "


            */
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        public static DataTable GetErrDatatable(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            string sql = "select ErrClass1,ErrClass2 from DownTime,ErrCode where ErrCode.PLCAddr=DownTime.PLCAddr " +
                            "and StartTime>= '" + StartTime + "'and StartTime<= '" + EndTime + "' order by  ErrClass2";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        /// <summary>
        /// 获取前5报警次数
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static DataTable GetErrDatatableAlarmTimesTop5(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            string sql = "select ErrIndex,ErrCode,count(Errindex) as count from DownTime,ErrCode where ErrCode.PLCAddr=DownTime.PLCAddr and StartTime>= '" + StartTime + "'and StartTime<= '" + EndTime + "' group by  ErrIndex order by count desc limit 5";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        /// <summary>
        /// 获取前5报警时间
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static DataTable GetErrDatatableAlarmSpanTimeTop5(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            string sql = "select ErrIndex,ErrCode,sum(Spantime) as count from DownTime,ErrCode where ErrCode.PLCAddr=DownTime.PLCAddr and StartTime>= '" + StartTime + "'and StartTime<= '" + EndTime + "' group by  ErrIndex order by count desc limit 5";
            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            return dt;

        }

        /// <summary>
        /// 得到每组报警多少次
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable DataTableSpanTimeGourpSum(DataTable dt, string GroupByColumnName, string QuantityColumnName = "")
        {
            dt.Columns.Add("TotalTimes");
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                dt.Rows[i]["TotalTimes"] = "1";
            }

            if (dt != null && GroupByColumnName != "")
            {
                //初始化列
                DataRow[] rows = dt.Select(null, GroupByColumnName);
                int length = rows.Length;
                string ProductCode = null;
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (GroupByColumnName != "")
                    {
                        if (dt.Rows[i][GroupByColumnName].ToString() != ProductCode)//如果1个列中有不同的，则为新的分组，重新记录1个列的值到临时变量
                        {
                            ProductCode = dt.Rows[i][GroupByColumnName].ToString();
                        }
                        else
                        {
                            if (QuantityColumnName == "")
                            {
                                dt.Rows[i]["Count"] = Convert.ToInt32(dt.Rows[i + 1]["Count"].ToString()) + Convert.ToInt32(dt.Rows[i]["Count"].ToString());//将上一个行的Quantity列和当前行的Quantity相加，记录到当前行中。
                            }
                            else
                            {
                                dt.Rows[i][QuantityColumnName] = Convert.ToInt32(dt.Rows[i + 1][QuantityColumnName].ToString()) + Convert.ToInt32(dt.Rows[i][QuantityColumnName].ToString());//将上一个行的Quantity列和当前行的Quantity相加，记录到当前行中。
                            }
                            dt.Rows.RemoveAt(i + 1);//将上一行从原来的DataTable中移除。
                        }
                    }
                    else
                    {
                        //MessageBox.Show("CircleChart控件执行错误！ GroupByColumnName 分组列名称 和 QuantityColumnName 数量列名称 两个属性不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 插入报警信号
        /// </summary>
        /// <param name="ErrIndex">报警索引号</param>
        /// <returns></returns>
        public static int InsertErrCode(string ErrIndex)
        {
            if (ErrIsClose(ErrIndex))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into DownTime(");
                strSql.Append("[InDate],StartTime,CloseTime,SpanTime,ErrIndex)");
                strSql.Append(" values (");
                strSql.Append("@InDate,@StartTime,@CloseTime,@SpanTime,@ErrIndex)");
                SQLiteParameter[] parameters = {
                    new SQLiteParameter("@InDate", DbType.String),
                    new SQLiteParameter("@StartTime", DbType.String),
                    new SQLiteParameter("@CloseTime", DbType.String),
                    new SQLiteParameter("@SpanTime", DbType.String),
                    new SQLiteParameter("@ErrIndex", DbType.String)
                  };
                parameters[0].Value = DateTime.Now.ToString("yyyy-MM-dd");
                parameters[1].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                parameters[2].Value = "";
                parameters[3].Value = "";
                parameters[4].Value = ErrIndex;
                return SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 对于错误时间跨度按小时进行插补
        /// </summary>
        /// <param name="ErrIndex">错误代码</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="CloseTime">关闭时间</param>
        public static void ErrTimerRunin(string ErrIndex, DateTime StartTime, DateTime CloseTime)
        {
            string NewInDate = "";
            string NewStartTime = "";
            string NewCloseTime = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update  DownTime ");
            strSql.Append("Set CloseTime=@CloseTime,SpanTime=@SpanTime,Flag=@Flag where CloseTime is null or CloseTime ='' and ErrIndex=@ErrIndex");

            SQLiteParameter[] parameters = {
                                        new SQLiteParameter("@CloseTime", DbType.String),
                                        new SQLiteParameter("@SpanTime", DbType.String),
                                        new SQLiteParameter("@ErrIndex", DbType.String),
                                        new SQLiteParameter("@Flag", DbType.String)
                                                        };
            TimeSpan ts = CloseTime - StartTime;
            if ((StartTime.Hour != CloseTime.Hour) || ts.TotalDays > 1)
            {
                int Hours_ = Convert.ToInt16(ts.TotalHours.ToString("0"));
                for (int i = 0; i <= Hours_; i++)
                {
                    DateTime temptime = StartTime;

                    if (i == 0)
                    {
                        NewStartTime = StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                        NewCloseTime = StartTime.ToString("yyyy-MM-dd HH:59:59");
                        NewInDate = StartTime.ToString("yyyy-MM-dd");

                        parameters[0].Value = NewCloseTime;
                        parameters[1].Value = "";
                        parameters[2].Value = ErrIndex;
                        parameters[3].Value = "0";
                        //更新错误关闭时间
                        SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);
                    }
                    else if (i == Hours_)
                    {
                        NewStartTime = CloseTime.ToString("yyyy-MM-dd HH:00:00");
                        NewCloseTime = CloseTime.ToString("yyyy-MM-dd HH:mm:ss");
                        NewInDate = CloseTime.ToString("yyyy-MM-dd");
                        InsertErrCode(ErrIndex, DateTime.Parse(NewInDate), DateTime.Parse(NewStartTime), DateTime.Parse(NewCloseTime), "0");
                    }
                    else
                    {
                        temptime = temptime.AddHours(i);
                        NewStartTime = temptime.ToString("yyyy-MM-dd HH:00:00");
                        NewCloseTime = temptime.ToString("yyyy-MM-dd HH:59:59");
                        NewInDate = temptime.ToString("yyyy-MM-dd");
                        InsertErrCode(ErrIndex, DateTime.Parse(NewInDate), DateTime.Parse(NewStartTime), DateTime.Parse(NewCloseTime), "0");
                    }
                }
            }
            else
            {
                NewStartTime = StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                NewCloseTime = CloseTime.ToString("yyyy-MM-dd HH:mm:ss");
                NewInDate = StartTime.ToString("yyyy-MM-dd");
                parameters[0].Value = NewCloseTime;
                parameters[1].Value = "";
                parameters[2].Value = ErrIndex;
                parameters[3].Value = "0";
                //更新错误关闭时间
                SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);
            }

        }

        public static int InsertErrCode(string ErrIndex, DateTime InDate, DateTime StartTime, DateTime CloseTime, string Flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DownTime(");
            strSql.Append("[InDate],StartTime,CloseTime,SpanTime,ErrIndex,Flag)");
            strSql.Append(" values (");
            strSql.Append("@InDate,@StartTime,@CloseTime,@SpanTime,@ErrIndex,@Flag)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@InDate", DbType.String),
                    new SQLiteParameter("@StartTime", DbType.String),
                    new SQLiteParameter("@CloseTime", DbType.String),
                    new SQLiteParameter("@SpanTime", DbType.String),
                    new SQLiteParameter("@ErrIndex", DbType.String),
                    new SQLiteParameter("@Flag", DbType.String)
                  };
            parameters[0].Value = InDate.ToString("yyyy-MM-dd");
            parameters[1].Value = StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[2].Value = CloseTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[3].Value = "";
            parameters[4].Value = ErrIndex;
            parameters[5].Value = Flag;
            return SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 关闭报警信号
        /// </summary>
        /// <param name="ErrIndex">报警索引号</param>
        /// <returns></returns>
        public static int CloseErrCode(string ErrIndex, out DateTime StartTime, out DateTime EndTime)
        {
            DataTable dt = new DataTable();
            DateTime StartTime_ = new DateTime();
            DateTime CloseTime_ = new DateTime();


            string sql = "select StartTime from DownTime where CloseTime is null or CloseTime ='' and ErrIndex='" + ErrIndex + "'";


            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());

            if (dt.Rows.Count >= 1) //判断有无需要关闭的报警记录
            {
                StartTime_ = DateTime.Parse(dt.Rows[0][0].ToString());

                CloseTime_ = DateTime.Now;
                //插补跨时间
                ErrTimerRunin(ErrIndex, StartTime_, CloseTime_);
                //计算时间跨度

                StartTime = StartTime_;
                EndTime = CloseTime_;

                return UpdateSpanTime(ErrIndex);

            }
            else
            {
                StartTime = StartTime_;
                EndTime = CloseTime_;
                return 0;
            }

        }
        /// <summary>
        /// 更新数据库SpanTime
        /// </summary>
        /// <param name="ErrIndex">报警索引号</param>
        /// <returns></returns>
        public static int UpdateSpanTime(string ErrIndex)
        {

            DataTable dt = new DataTable();

            string sql = "select *, StartTime, CloseTime  from DownTime where ErrIndex='" + ErrIndex + "' and (SpanTime is null or SpanTime ='')";

            dt = SQLiteHelper_BZ.GetDataTable(sql.Trim());
            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string StartTime_ = dt.Rows[i]["StartTime"].ToString().Trim();
                string CloseTime_ = dt.Rows[i]["CloseTime"].ToString().Trim();
                if (CloseTime_ != "" && CloseTime_ != null)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update  DownTime ");
                    strSql.Append("Set SpanTime=@SpanTime,SpanTimeMinutes=@SpanTimeMinutes where StartTime =@StartTime and ErrIndex=@ErrIndex");

                    SQLiteParameter[] parameters = {
                    new SQLiteParameter("@SpanTime", DbType.String),
                     new SQLiteParameter("@SpanTimeMinutes", DbType.String),
                    new SQLiteParameter("@StartTime", DbType.String),
                    new SQLiteParameter("@ErrIndex", DbType.String)
                  };

                    DateTime Star = DateTime.Parse(StartTime_);
                    DateTime Close = DateTime.Parse(CloseTime_);
                    TimeSpan ts = Close - Star;

                    parameters[0].Value = ts.TotalSeconds.ToString();
                    parameters[1].Value = ts.TotalMinutes.ToString("0.0");
                    parameters[2].Value = StartTime_;
                    parameters[3].Value = ErrIndex;

                    SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);
                    count = count + 1;
                }

            }
            return count;

        }

        /// <summary>
        /// 导入ErrCode报警信息到数据库表
        /// </summary>
        /// <param name="dt"></param>
        public static int InputErrCode(DataTable dt)
        {
            string sql = "delete from ErrCode";

            SQLiteHelper_BZ.ExecuteSql(sql); //删除原来的报警信息对照表数据

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ErrCode(");
                strSql.Append("ErrIndex,ErrCode,ErrTypeCn,ErrTypeEn,MessageCn,MessageEn,Severity,ErrFormat,LineName,StationName)");
                strSql.Append(" values (");
                strSql.Append("@ErrIndex,@ErrCode,@ErrTypeCn,@ErrTypeEn,@MessageCn,@MessageEn,@Severity,@ErrFormat,@LineName,@StationName)");
                SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ErrIndex", DbType.String),
                    new SQLiteParameter("@ErrCode", DbType.String),
                    new SQLiteParameter("@ErrTypeCn", DbType.String),
                    new SQLiteParameter("@ErrTypeEn", DbType.String),
                    new SQLiteParameter("@MessageCn", DbType.String),
                    new SQLiteParameter("@MessageEn", DbType.String),
                    new SQLiteParameter("@Severity", DbType.String),
                    new SQLiteParameter("@ErrFormat", DbType.String),
                     new SQLiteParameter("@LineName", DbType.String),
                    new SQLiteParameter("@StationName", DbType.String)
                  };
                parameters[0].Value = dr["Index"].ToString().Trim();
                parameters[1].Value = dr["ErrorCode"].ToString().Trim();
                parameters[2].Value = dr["CodeTypeCn"].ToString().Trim();
                parameters[3].Value = dr["CodeTypeEn"].ToString().Trim();
                parameters[4].Value = dr["CnMessage"].ToString().Trim();
                parameters[5].Value = dr["EnMessage"].ToString().Trim();
                parameters[6].Value = dr["Severity"].ToString().Trim();
                parameters[7].Value = dr["ErrorFormat"].ToString().Trim();
                parameters[8].Value = dr["LineName"].ToString().Trim();
                parameters[9].Value = dr["StationName"].ToString().Trim();

                SQLiteHelper_BZ.ExecuteSql(strSql.ToString(), parameters);

                i = i + 1;
            }
            return i;

        }

        /// <summary>
        /// 删除过去的报警数据
        /// </summary>
        /// <param name="day">时间按天数</param>
        /// <returns></returns>
        public static int DeleteOldALMData(int day, string PathFlie)
        {
            string StartTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd 00:00:00");
            DateTime dtime = DateTime.Now.AddDays(day);
            string oldtime = dtime.ToString("yyyy-MM-dd 00:00:00");
            //DataTable dt = GetErrDatatable(StartTime, oldtime);

            //if (!Directory.Exists(PathFlie))
            //{
            //    Directory.CreateDirectory(PathFlie);
            //}

            //if (dt.Rows.Count > 0)
            //{

            //    string PathFliecsv = PathFlie + "\\" + dtime.ToString("yyyy-MM-dd") + ".csv";

            //    CSVHelper.CSVHelper.DTTOCSV(PathFliecsv, dt);//导出历史数据到CSV文件

            string sql = "delete from DownTime where StartTime<='" + oldtime + "'";

            return SQLiteHelper_BZ.ExecuteSql(sql);

            //}
            //else
            //{
            //    return 0;
            //}

        }

        /// <summary>
        ///  存储UPH和Yield到数据库
        /// </summary>
        /// <param name="UPH">UPH数据字符串数组</param>
        /// <param name="Yield">Yied数据字符串数组</param>
        /// <returns></returns>
        public static int SaveUPHYield(int[] UPH, float[] Yield)
        {
            DateTime NowTime = DateTime.Now.AddHours(-1);
            string Date = NowTime.ToString("yyyy-MM-dd");
            string sql = "select count(*) from UPHYield where Date_='" + Date + "'";
            int Rows = Convert.ToInt32(SQLiteHelper_BZ.GetDataTable(sql).Rows[0][0].ToString());
            if (Rows == 0)
            {
                sql = "insert into UPHYield(Date_," +
                       "H0,H1,H2,H3,H4,H5,H6,H7,H8,H9,H10,H11,H12," +
                       "H13,H14,H15,H16,H17,H18,H19,H20,H21,H22,H23," +
                       "OKH0,OKH1,OKH2,OKH3,OKH4,OKH5,OKH6,OKH7,OKH8,OKH9,OKH10,OKH11,OKH12," +
                       "OKH13,OKH14,OKH15,OKH16,OKH17,OKH18,OKH19,OKH20,OKH21,OKH22,OKH23,AllDay,AllOK)" +
                       " values('" + Date + "'," +
                       "'0','0','0','0','0','0','0','0','0','0','0','0','0'," +
                       "'0','0','0','0','0','0','0','0','0','0','0','0','0'," +
                       "'0','0','0','0','0','0','0','0','0','0','0','0','0'," +
                       "'0','0','0','0','0','0','0','0','0','0','0')";
                SQLiteHelper_BZ.ExecuteSql(sql);
            }
            int SaveH = NowTime.Hour;
            sql = "update  UPHYield set H" + SaveH.ToString() + "='" + UPH[SaveH].ToString().Trim() + "',OKH" + SaveH.ToString() + "='" + Yield[0].ToString("0.00").Trim() + "' where Date_='" + Date + "'";
            return SQLiteHelper_BZ.ExecuteSql(sql);
        }


        /// <summary>
        /// 获取UPHYield数据
        /// </summary>
        public static DataTable GetUPHYield(DateTime day)
        {
            string Date = day.ToString("yyyy-MM-dd");
            string sql = "select * from UPHYield where Date_>='" + Date + "' and Date_<='" + Date + "'";
            DataTable dt = SQLiteHelper_BZ.GetDataTable(sql); //删除原来的报警信息对照表数据

            return dt;

        }

        /// <summary>
        /// 获取UPHYield数据
        /// </summary>
        public static int DeleteUPHYield(DateTime day)
        {
            string Date = day.ToString("yyyy-MM-dd");
            string sql = "delete from UPHYield where Date_<='" + Date + "'";
            return SQLiteHelper_BZ.ExecuteSql(sql); //删除原来的报警信息对照表数据
        }

        #endregion

        #region


        /// <summary>
        /// 创建OEE表
        /// </summary>
        public static void CreateOEE()
        {
            string sql = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='OEE'";
            DataTable dt = SQLiteHelper_BZ.GetDataTable(sql); //查询OEE表是否存在
            int OEE = 0;
            if (dt.Rows.Count > 0)
            {
                OEE = Convert.ToInt16(dt.Rows[0][0].ToString().Trim());
            }
            if (OEE == 0)
            {
                sql = "CREATE TABLE OEE(State TEXT, StartTime TEXT, EndTime TEXT)";
                SQLiteHelper_BZ.ExecuteSql(sql);
            }
        }

        /// <summary>
        /// 创建MES_SN表
        /// </summary>
        public static void CreateMESSN()
        {
            string sql = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='MES_SN'";
            DataTable dt = SQLiteHelper_BZ.GetDataTable(sql); //查询OEE表是否存在
            int MESSN = 0;
            if (dt.Rows.Count > 0)
            {
                MESSN = Convert.ToInt16(dt.Rows[0][0].ToString().Trim());
            }
            if (MESSN == 0)
            {
                sql = "CREATE TABLE MES_SN(SN TEXT, InputTime TEXT)";
                SQLiteHelper_BZ.ExecuteSql(sql);

            }
        }

        /// <summary>
        /// 插入和更新设备状态
        /// </summary>
        /// <param name="state">当前设备状态</param>
        public static void InsetUpdateOEE(int state)
        {
            if (state > 0)
            {
                string sql = "delete from OEE where StartTime<='" + DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd") + "'";

                SQLiteHelper_BZ.ExecuteSql(sql);

                sql = "update OEE set EndTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where EndTime = 'T' ";

                SQLiteHelper_BZ.ExecuteSql(sql);


                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into OEE(");
                strSql.Append("State,StartTime,EndTime)");
                strSql.Append(" values (");
                strSql.Append("'" + state.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','T')");

                SQLiteHelper_BZ.ExecuteSql(strSql.ToString());

            }
        }


        public static void AddOneOEE(int state)
        {
            //if (state > 0)
            //{
            //    string sql = "delete from OEE where StartTime<='" + DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd") + "'";

            //    SQLiteHelper_BZ.ExecuteSql(sql);

            //    sql = "update OEE set EndTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where EndTime = 'T' ";

            //    SQLiteHelper_BZ.ExecuteSql(sql);


            //    StringBuilder strSql = new StringBuilder();
            //    strSql.Append("insert into OEE(");
            //    strSql.Append("State,StartTime,EndTime)");
            //    strSql.Append(" values (");
            //    strSql.Append("'" + state.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','T')");

            //    SQLiteHelper_BZ.ExecuteSql(strSql.ToString());

            //}


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OEE(");
            strSql.Append("State,StartTime,EndTime)");
            strSql.Append(" values (");
            strSql.Append("'" + state.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','T')");

            int i = SQLiteHelper_BZ.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获取OEE数据
        /// </summary>
        /// <param name="sttime">需要计算的OEE时间</param>
        /// <returns></returns>
        public static DataTable GetOEE(DateTime sttime)
        {
            string sql = "select * from OEE where StartTime>='" + sttime.ToString("yyyy-MM-dd") + "' and StartTime<='" + sttime.AddDays(1).ToString("yyyy-MM-dd") + "'";
            DataTable dt = SQLiteHelper_BZ.GetDataTable(sql); //查询OEE表是否存在
            return dt;
        }



        #endregion


        #region B589-ADD

        /// <summary>
        /// 创建OEE表
        /// </summary>
        public static void CreateNewUPH()
        {
            string sql = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='UPH'";
            DataTable dt = SQLiteHelper_BZ.GetDataTable(sql); //查询OEE表是否存在
            int uph = 0;
            if (dt.Rows.Count > 0)
            {
                uph = Convert.ToInt16(dt.Rows[0][0].ToString().Trim());
            }
            if (uph == 0)
            {
                sql = "CREATE TABLE UPH(Date DATE, ";
                for (int i = 0; i < 24; i++)
                {
                    sql += $"H{i} TEXT";
                    if (i < 24 - 1)
                    {
                        sql += ", ";
                    }
                }

                sql += ")";

                SQLiteHelper_BZ.ExecuteSql(sql);
            }
        }

        public static int SaveNewUPHData(int[] uphData)
        {
            CreateNewUPH();
            DateTime NowTime = DateTime.Now.AddHours(-1);
            string Date = NowTime.ToString("yyyy-MM-dd");
            int SaveH = NowTime.Hour;
            string sql = string.Empty;
            sql = $"select * from UPH where Date = '{Date}'";
            DataTable dt = SQLiteHelper_BZ.GetDataTable(sql);
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
            {
                sql = "insert into UPH (Date,H0,H1,H2,H3,H4,H5,H6,H7,H8,H9,H10,H11,H12,H13,H14,H15,H16,H17,H18,H19,H20,H21,H22,H23) " +
                    $"values ('{Date}',{uphData[0]},{uphData[1]},{uphData[2]},{uphData[3]},{uphData[4]},{uphData[5]},{uphData[6]},{uphData[7]},{uphData[8]},{uphData[9]},{uphData[10]},{uphData[11]},{uphData[12]},{uphData[13]},{uphData[14]},{uphData[15]},{uphData[16]},{uphData[17]},{uphData[18]},{uphData[19]},{uphData[20]},{uphData[21]},{uphData[22]},{uphData[23]})";
            }
            else
            {
                sql = "update  UPH set ";
                for (int i = 0; i < uphData.Length; i++)
                {
                    sql += $"H{i} = {uphData[i]} ";
                    if (i < uphData.Length - 1)
                    {
                        sql += ", ";
                    }
                }
                sql += $"where Date = '{Date}'";
            }
            return SQLiteHelper_BZ.ExecuteSql(sql);
        }

        #endregion
    }
}
