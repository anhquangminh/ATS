using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class ConfigCanten
    {
        public string id { set; get; }
        public string position { set; get; }
        public string type { set; get; }
        public string user_config { set; get; }
        public string time_config { set; get; }
        public string time_edit { set; get; }
        public string ip { set; get; }
        public string NoteID { set; get; }
    }
    public class Cantenhelper
    {
        string conString = @"Data Source=10.224.81.131,3000;Initial Catalog=OWNCLOUD_DATA;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
        public Boolean Execute(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public List<ConfigCanten> getListUser()
        {
            List<ConfigCanten> Listuser = new List<ConfigCanten>();
            Cantenhelper helper = new Cantenhelper();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM CantenConfigF order by time_config desc";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ConfigCanten ct = new ConfigCanten();
                    ct.id = dt.Rows[i][0].ToString().Trim();
                    ct.position = dt.Rows[i][1].ToString().Trim();
                    ct.type = dt.Rows[i][2].ToString().Trim();
                    ct.user_config = dt.Rows[i][3].ToString().Trim();
                    ct.time_config = dt.Rows[i][4].ToString().Trim();
                    ct.time_edit = dt.Rows[i][5].ToString().Trim();
                    ct.ip = dt.Rows[i][6].ToString().Trim();
                    ct.NoteID = dt.Rows[i][7].ToString().Trim();

                    Listuser.Add(ct);
                }
            }
            return Listuser;
        }
        public List<ConfigCanten> getByID(string id)
        {
            List<ConfigCanten> Listuser = new List<ConfigCanten>();
            Cantenhelper helper = new Cantenhelper();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM CantenConfigF where id='" + id + "'";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ConfigCanten ct = new ConfigCanten();
                    ct.id = dt.Rows[i][0].ToString().Trim();
                    ct.position = dt.Rows[i][1].ToString().Trim();
                    ct.type = dt.Rows[i][2].ToString().Trim();
                    ct.user_config = dt.Rows[i][3].ToString().Trim();
                    ct.time_config = dt.Rows[i][4].ToString().Trim();
                    ct.time_edit = dt.Rows[i][5].ToString().Trim();
                    ct.ip = dt.Rows[i][6].ToString().Trim();
                    ct.NoteID = dt.Rows[i][7].ToString().Trim();

                    Listuser.Add(ct);
                }
            }
            return Listuser;
        }
        public DataTable queryDatatable(string query)
        {
            
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    da.Fill(table);
                    conn.Close();
                    da.Dispose();
                    conn.Close();
                    //for(int i =0;i < table.Rows.Count; i++)
                    //{
                    //    string name = table.Rows[i][0].ToString();
                    //    string sd = table.Rows[i][1].ToString();
                    //}
                    return table;
                }
            }

        }
        public Boolean checkrow(string sql)
        {
            Cantenhelper helper = new Cantenhelper();
            DataTable dt = new DataTable();

            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class UserCanten
    {
            public string empno { set; get; }
            public string empname { set; get; }
            public string position { set; get; }
            public string time { set; get; }
            public string ff { set; get; }
    }
    class MysqlHelper
    {
        public string VN_Record = "server=10.132.39.164;user=vnrecord;database=VN_Record;port=3306;password=aDtUAWy4KPqb;SslMode=none;charset=utf8;";
        public List<UserCanten> getList(string sql)
        {
            DataTable dt = new DataTable();
            List<UserCanten> list = new List<UserCanten>();
            using (MySqlConnection conn = new MySqlConnection(VN_Record))
            {
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserCanten cn = new UserCanten();
                        cn.empno= rdr[0].ToString();
                        cn.empname = rdr[1].ToString();
                        cn.position = rdr[2].ToString();
                        cn.time = rdr[3].ToString();
                        list.Add(cn);
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                conn.Close();
            }
            return list;
        }
        public List<UserCanten> getListErr(string sql)
        {
            DataTable dt = new DataTable();
            List<UserCanten> list = new List<UserCanten>();
            using (MySqlConnection conn = new MySqlConnection(VN_Record))
            {
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserCanten cn = new UserCanten();
                        cn.empno = rdr[0].ToString();
                        cn.empname = rdr[1].ToString();
                        cn.position = rdr[2].ToString();
                        cn.time = rdr[3].ToString();
                        cn.ff = rdr[4].ToString();
                        list.Add(cn);
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                conn.Close();
            }
            return list;
        }
        public DataTable getDataTable(string sql)
        {
            using (MySqlConnection con = new MySqlConnection(VN_Record))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                con.Close();
                                return dt;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            }
        }
        public bool checkCount(string sql)
        {
            DataTable mytable = new DataTable();
            using (MySqlConnection con = new MySqlConnection(VN_Record))
            {
                try
                {
                    con.Open();
                    MySqlDataAdapter dap = new MySqlDataAdapter(sql, con);
                    dap.Fill(mytable);
                    con.Close();
                    if (mytable.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
    }

    class CantenAnalis
    {
        public string F_SITE { get; set; }
        public string F_STATES { get; set; }
        public string F_MealType { get; set; }
        public string F_SCANLUNCH { get; set; }
        public string F_EMPNO { get; set; }
        public string F_EMPNAME { get; set; }
        public string F_BU { get; set; }
        public string MESS { get; set; }
    }
}