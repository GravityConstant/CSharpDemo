using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace FirstMVC.Utils
{
    public class MySqlHelpers
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        /// <summary>
        /// 执行不带参数sql语句,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdstr">相应的sql语句</param>
        /// <returns>返回结果集首行首列的值object</returns>
        public static object ExecuteScalar(string sql)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Transaction = null;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;

                object result = cmd.ExecuteScalar();

                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("execute scalar error:" + e.Message);
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Transaction = null;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;

                da.SelectCommand = cmd;

                DataTable table = new DataTable();
                da.Fill(table);

                return table;
            }
            catch (Exception e)
            {
                Debug.WriteLine("execute data table error:" + e.Message);
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行一个返回结果集的数据库操作
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="sql">查询字符串</param>
        /// <returns>数据结果集</returns>
        public static MySqlDataReader ExecuteReader(string sql)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Transaction = null;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;

                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                cmd.Parameters.Clear();
                
                return rdr;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ExecuteReader error." + e.Message);
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行不带参数sql语句，返回所影响的行数
        /// </summary>
        /// <param name="sql">增，删，改sql语句</param>
        /// <returns>返回所影响的行数</returns>
        public static int ExecuteNonQuery(string sql)
        {
            int count;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Transaction = null;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;

                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception e)
            {
                Debug.WriteLine("ExecuteNonQuery error." + e.Message);
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return count;
        }

    }
}