using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpSQLTools
{
    class Batch
    {
        public static string RemoteExec(SqlConnection Conn, String Command, Boolean Flag)
        {
            String value = String.Empty;
            try
            {
                //TODO:发送Command命令
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;

                //查询数据记录
                cmd.CommandText = Command;
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Flag)
                        {
                            value += String.Format("\r\n{0}", reader[0].ToString());
                        }
                        else
                        {
                            value = reader[0].ToString();
                        }
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                //Conn.Close();
                Console.WriteLine("[!] Error log: \r\n" + ex.Message);
            }
            return null;
        }

        public static void CLRExec(SqlConnection Conn, String Command)
        {
            try
            {
                //TODO:发送Command命令
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;

                //查询数据记录
                cmd.CommandText = Command;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Conn.Close();
                Console.WriteLine("[!] Error log: \r\n" + ex.Message);
            }
        }
    }
}
