﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace CricketClubDAL
{
    public class Db
    {
        private static OleDbConnection OpenConnection()
        {
            var conn = new OleDbConnection(GetScorebookConnectionString());
            conn.Open();
            return conn;
        }


        private static string GetScorebookConnectionString()
        {
            string key = "ProdDB";
            if (Environment.MachineName.Contains("BIG-PC") || Environment.MachineName.Contains("LAPTOP"))
            {
                key = "LocalDB";
            }
            Console.Out.WriteLine("Connecting to: " + key);
            ConnectionStringSettings cnxStr = ConfigurationManager.ConnectionStrings[key];
            if (cnxStr == null)
                throw new ConfigurationErrorsException("ConnectionString '" + key +
                                                       "' was not found in the configuration file.");
            return cnxStr.ConnectionString;
        }

        public DataRow ExecuteSQLAndReturnFirstRow(string sql)
        {
            try
            {
                using (OleDbConnection connection = OpenConnection())
                {
                    var data = new DataSet();
                    var adaptor = new OleDbDataAdapter(sql, connection);
                    adaptor.Fill(data);
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                    {
                        return data.Tables[0].Rows[0];
                    }
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error executing: " + sql, exception);
            }
        }

        public object ExecuteSQLAndReturnSingleResult(string sql)
        {
            try
            {
                using (OleDbConnection conn = OpenConnection())
                {
                    using (var command = new OleDbCommand(sql, conn))
                    {
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error executing: " + sql, exception);
            }
        }

        public int ExecuteInsertOrUpdate(string sql)
        {
            try
            {
                using (OleDbConnection conn = OpenConnection())
                {
                    using (var command = new OleDbCommand(sql, conn))
                    {
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error executing: " + sql, exception);
            }
        }

        public DataSet ExecuteSqlAndReturnAllRows(string sql)
        {
            try
            {
                using (OleDbConnection conn = OpenConnection())
                {
                    var data = new DataSet();
                    var adaptor = new OleDbDataAdapter(sql, conn);
                    adaptor.Fill(data);
                    return data;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error executing: " + sql, exception);
            }
        }

        public Row QueryOne(string sql)
        {
            return new Row(ExecuteSQLAndReturnFirstRow(sql));
        }

        public IEnumerable<Row> QueryMany(string sql)
        {
            return ExecuteSqlAndReturnAllRows(sql).Tables[0].Rows.Cast<DataRow>().Select(row => new Row(row));
        }
    }

    public class Row
    {
        private readonly DataRow dataRow;

        public Row(DataRow dataRow)
        {
            this.dataRow = dataRow;
        }

        public int GetInt(int index)
        {
            return GetInt(index, 0);
        }

        public int GetInt(string columnName)
        {
            return Convert.ToInt32(dataRow[columnName]);
        }

        public string GetString(string columnName)
        {
            return dataRow[columnName].ToString();
        }

        public int GetInt(int index, int valueIfNull)
        {
            object value = dataRow[index];
            if (value is DBNull)
            {
                return valueIfNull;
            }
            return Convert.ToInt32(value);
        }
    }
}