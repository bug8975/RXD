using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using RXD.pojo;

namespace RXD.common
{
    public abstract class MySqlHelper
    {
        //数据库连接字符串
        public static string Conn = "Database='device_manage';Data Source='localhost';User Id='root';Password='root';charset='utf8';pooling=true;Allow Zero Datetime=True";

        /// <summary>
        /// 给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;

        }

        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例:
        /// int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例:
        /// MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlCommand cmd = new MySqlCommand();
            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                //调用 MySqlCommand 的 ExecuteReader 方法
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlCommand cmd = new MySqlCommand();
            //因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                //调用 MySqlCommand 的 ExecuteReader 方法
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                //清除参数
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        /// 例如:
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        public static void InsertTable_DataView(List<pojo.DataView> list)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            string sqlInsert = @" insert into dataview (x,y,z,times,sensor_id) value (@x,@y,@z,@times,@sensor_id);";
            DataTable dt = new DataTable();
            dt.Columns.Add("x", typeof(double));
            dt.Columns.Add("y", typeof(double));
            dt.Columns.Add("z", typeof(double));
            dt.Columns.Add("times", typeof(DateTime));
            dt.Columns.Add("sensor_id", typeof(int));
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlTransaction transaction = conn.BeginTransaction();
            var mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.InsertCommand = new MySqlCommand(sqlInsert, conn);
            mySqlDataAdapter.InsertCommand.Parameters.Add("@x", MySqlDbType.Double, 20, "x");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@y", MySqlDbType.Double, 20, "y");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@z", MySqlDbType.Double, 20, "z");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@times", MySqlDbType.DateTime, 20, "times");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@sensor_id", MySqlDbType.Int32, 10, "sensor_id");
            mySqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                row["x"] = item.X;
                row["y"] = item.Y;
                row["z"] = item.Z;
                row["times"] = item.Time;
                row["sensor_id"] = item.Sensorid;
                dt.Rows.Add(row);
            }
            mySqlDataAdapter.UpdateBatchSize = 10000;
            mySqlDataAdapter.Update(dt);
            transaction.Commit();
            sp.Stop();
        }


        public static void InsertTable_SensorInfo(List<pojo.SensorInfo> list)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            string sqlInsert = @" insert into sensorinfo (name,type,longitude,latitude,noise,times,sensor_id) value (@name,@type,@longitude,@latitude,@noise,@times,@sensor_id);";
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("longitude", typeof(double));
            dt.Columns.Add("latitude", typeof(double));
            dt.Columns.Add("noise", typeof(int));
            dt.Columns.Add("times", typeof(DateTime));
            dt.Columns.Add("sensor_id", typeof(int));
            MySqlConnection conn = ConnectionPool.getPool().getConnection();
            MySqlTransaction transaction = conn.BeginTransaction();
            var mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.InsertCommand = new MySqlCommand(sqlInsert, conn);
            mySqlDataAdapter.InsertCommand.Parameters.Add("@name", MySqlDbType.String, 255, "name");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@type", MySqlDbType.String, 255, "type");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@longitude", MySqlDbType.Double, 10, "longitude");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@latitude", MySqlDbType.Double, 10, "latitude");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@noise", MySqlDbType.Int32, 10, "noise");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@times", MySqlDbType.DateTime, 0, "times");
            mySqlDataAdapter.InsertCommand.Parameters.Add("@sensor_id", MySqlDbType.Int32, 10, "sensor_id");
            mySqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                row["name"] = item.Name;
                row["type"] = item.Type;
                row["longitude"] = item.Longitude;
                row["latitude"] = item.Latitude;
                row["noise"] = item.Noise;
                row["times"] = item.Times;
                row["sensor_id"] = item.Sensorid;
                dt.Rows.Add(row);
            }
            mySqlDataAdapter.UpdateBatchSize = 10000;
            mySqlDataAdapter.Update(dt);
            transaction.Commit();
            sp.Stop();
        }

    }
}
