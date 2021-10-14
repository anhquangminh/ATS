using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class DataMessage
    {
        public string KEY { set; get; }
        public string GROUP_NAME { set; get; }
        public string CONTENTS { set; get; }
        public string USERSEND { set; get; }
        public string REPLY { set; get; }
    }

    public class Chat
    {
        public string str_con359 = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS =(PROTOCOL = TCP)(HOST =10.225.35.9)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = vnap)));User Id=ap2;Password=NSDAP2LOGPD0522";
        public void getAllMessage()
        {
            using (OracleConnection con = new OracleConnection(str_con359))
            {
                try
                {
                    con.Open();
                    string sql = "select * from MES4.R_MESSAGEWECHAT where STATUS=0 and ROW1='ZALO' ";
                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        using (OracleDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                int count = 0;
                                while (dr.Read())
                                {
                                    count++;
                                    string datetime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");

                                    string group = dr[1].ToString();
                                    string Message = dr[2].ToString();
                                  
                                    string query_update = "update MES4.R_MESSAGEWECHAT set STATUS ='1' ,DATE_SEND=sysdate where ID_MESSAGE = '" + dr[0].ToString() + "'";
                                    OracleCommand cmd1 = con.CreateCommand();
                                    cmd1.CommandType = CommandType.Text;
                                    cmd1.CommandText = query_update;
                                    cmd1.ExecuteNonQuery();
                                    OracleTransaction transaction;
                                    transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                                    cmd1.Transaction = transaction;
                                    transaction.Commit();
                                }
                               
                            }
                        }
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (OracleException ex) // catches only Oracle errors
                {
                     Console.WriteLine(ex.Message);
                }
            }
        }

        public Boolean ExcuteSQL(string sql)
        {
            using (OracleConnection con = new OracleConnection(str_con359))
            {
                try
                {
                    con.Open();
                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        OracleTransaction transaction;
                        transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Transaction = transaction;
                        transaction.Commit();
                        con.Close();
                        con.Dispose();
                        return true;
                    }
                }
                catch (OracleException ex) // catches only Oracle errors
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public DataTable getDataTable(string sql)
        {
            using (OracleConnection conn = new OracleConnection(str_con359))
            {
                //Open the connection to the database
                conn.Open();
                DataTable dt = new DataTable();
                using (OracleCommand cmd = new OracleCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                    {
                        dataAdapter.SelectCommand = cmd;
                        dataAdapter.Fill(dt);
                        conn.Close();
                        return dt;
                    }
                }
                 
            }
        }

        public bool CompareProgram(string query)
        {
            bool checkFlag = false;
            DataTable tbDa = new DataTable();
            try
            {
                using (OracleConnection con = new OracleConnection(str_con359))
                {
                    using (OracleDataAdapter da = new OracleDataAdapter(query, con))
                    {
                        da.Fill(tbDa);
                        if (tbDa.Rows.Count > 0)
                        {
                            checkFlag = true;
                        }
                    }
                }
                return checkFlag;
            }
            catch
            {
                return checkFlag;
            }
        }
    }
}