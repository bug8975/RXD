﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;

namespace OneNet.common
{
    class ConnectionPool
    {
        private static ConnectionPool cpool = null;//池管理对象
        private static Object objlock = typeof(ConnectionPool);//池管理对象实例
        private int size = 1;//池中连接数
        private int useCount = 0;//已经使用的连接数
        private ArrayList pool = null;//连接保存的集合
        private String ConnectionStr = "";//连接字符串

        public ConnectionPool()
        {
            //数据库连接字符串
            ConnectionStr = ConfigurationManager.ConnectionStrings["OneNet.Properties.Settings.onenetConnectionString"].ConnectionString;
            //创建可用连接的集合
            pool = new ArrayList();
        }

        #region 创建获取连接池对象
        public static ConnectionPool getPool()
        {
            lock (objlock)
            {
                if (cpool == null)
                {
                    cpool = new ConnectionPool();
                }
                return cpool;
            }
        }
        #endregion

        #region 获取池中的连接
        public MySqlConnection getConnection()
        {
            lock (pool)
            {
                MySqlConnection tmp = null;
                //可用连接数量大于0
                if (pool.Count > 0)
                {
                    //取第一个可用连接
                    tmp = (MySqlConnection)pool[0];
                    //在可用连接中移除此链接
                    pool.RemoveAt(0);
                    //不成功
                    if (!isUserful(tmp))
                    {
                        //可用的连接数据已去掉一个
                        useCount--;
                        tmp = getConnection();
                    }
                }
                else
                {
                    //可使用的连接小于连接数量
                    if (useCount <= size)
                    {
                        try
                        {
                            //创建连接
                            tmp = CreateConnection(tmp);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
                //连接为null
                if (tmp == null)
                {
                    //达到最大连接数递归调用获取连接否则创建新连接
                    if (useCount <= size)
                    {
                        tmp = getConnection();
                    }
                    else
                    {
                        tmp = CreateConnection(tmp);
                    }
                }
                return tmp;
            }
        }
        #endregion

        #region 创建连接
        private MySqlConnection CreateConnection(MySqlConnection tmp)
        {
            //创建连接
            MySqlConnection conn = new MySqlConnection(ConnectionStr);
            conn.Open();
            //可用的连接数加上一个
            useCount++;
            tmp = conn;
            return tmp;
        }
        #endregion

        #region 关闭连接,加连接回到池中
        public void closeConnection(MySqlConnection con)
        {
            lock (pool)
            {
                if (con != null)
                {
                    //将连接添加在连接池中
                    pool.Add(con);
                }
            }
        }
        #endregion

        #region 目的保证所创连接成功,测试池中连接
        private bool isUserful(MySqlConnection con)
        {
            //主要用于不同用户
            bool result = true;
            if (con != null)
            {
                string sql = "select 1";//随便执行对数据库操作
                MySqlCommand cmd = new MySqlCommand(sql, con);
                try
                {
                    cmd.ExecuteScalar().ToString();
                }
                catch
                {
                    result = false;
                }

            }
            return result;
        }
        #endregion
    }
}
