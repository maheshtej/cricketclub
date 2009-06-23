using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.OleDb;


namespace CricketClubDAL
{
    public class DB
    {
        internal static OleDbConnection OpenConnection()
        {
            OleDbConnection conn = new OleDbConnection(GetScorebookConnectionString());
            conn.Open();
            return conn;
        }

        
        private static string GetScorebookConnectionString()
        {
            
            ConnectionStringSettings cnxStr = ConfigurationManager.ConnectionStrings["ScorebookDB"];
            if (cnxStr == null)
                throw new ConfigurationErrorsException("ConnectionString 'ScorebookDB' was not found in the configuration file.");
            return cnxStr.ConnectionString;
        }

        public DataRow ExecuteSQLAndReturnFirstRow(string sql)
        {
            try
            {
                using (var Conn = OpenConnection())
                {
                    using (var Command = new OleDbCommand(sql, Conn))
                    {
                        DataSet data = new DataSet();
                        OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, Conn);
                        adaptor.Fill(data);
                        if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                        {
                            return data.Tables[0].Rows[0];
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public object ExecuteSQLAndReturnSingleResult(string sql)
        {
            try
            {
                using (var Conn = OpenConnection())
                {
                    using (var Command = new OleDbCommand(sql, Conn))
                    {

                        return Command.ExecuteScalar();

                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public int ExecuteInsertOrUpdate(string sql)
        {
            try
            {
                using (var Conn = OpenConnection())
                {
                    using (var Command = new OleDbCommand(sql, Conn))
                    {
                           
                           return Command.ExecuteNonQuery();


                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public DataSet ExecuteSQLAndReturnAllRows(string sql)
        {
            try
            {
                using (var Conn = OpenConnection())
                {
                    using (var Command = new OleDbCommand(sql, Conn))
                    {
                        DataSet data = new DataSet();
                        OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, Conn);
                        adaptor.Fill(data);
                        return data;        
                    }
                }
                
            }
            catch
            {
                return null;
            }
        }

    }
}
