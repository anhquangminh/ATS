using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class Message
    {
        public string id_ats { set; get; }
        public string ip_address { set; get; }
        public string owner_emp { set; get; }
        public string owner_name { set; get; }
        public string building { set; get; }
        public string team { set; get; }
        public string emp_user { set; get; }
        public string level_user { set; get; }
        public string description { set; get; }
        public string status { set; get; }
        public string time_start { set; get; }
        public string time_going { set; get; }
        public string time_end { set; get; }

    }
    public class Quantity
    {
        public string Name { set; get; }
        public string Number { set; get; }
    }
    public class getAllMessage
    {
        DBConnection db;
        public getAllMessage()
        {
            db = new DBConnection();
        }
        public List<Message> listATS(string excute)
        {
            string sql = "SELECT * FROM ATS_Content";
            switch (excute)
            {
                case "all":
                    sql = "SELECT * FROM ATS_Content ";
                    break;
                case "level":
                    sql = "SELECT * FROM ATS_Content order by status desc,level_user ";
                    break;
                case "time_start":
                    sql = "SELECT * FROM ATS_Content order by time_start desc ";
                    break;
                default:
                    sql = "SELECT * FROM ATS_Content";
                    break;
            }
            List<Message> ListMessages = new List<Message>();
            DataTable dt = new DataTable();
            using (SqlConnection con = db.getSQLConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    con.Open();
                    da.Fill(dt);
                    da.Dispose();
                    con.Close();
                    Message message;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        message = new Message();
                        message.id_ats = dt.Rows[i]["id_ats"].ToString().Trim();
                        message.ip_address = dt.Rows[i]["ip_address"].ToString().Trim();
                        message.owner_emp = dt.Rows[i]["owner_emp"].ToString().Trim();
                        message.owner_name = dt.Rows[i]["owner_name"].ToString().Trim();
                        message.building = dt.Rows[i]["building"].ToString().Trim();
                        message.team = dt.Rows[i]["team"].ToString().Trim();
                        message.emp_user = dt.Rows[i]["emp_user"].ToString().Trim();
                        message.level_user = dt.Rows[i]["level_user"].ToString().Trim();
                        message.description = dt.Rows[i]["description"].ToString().Trim();
                        message.status = dt.Rows[i]["status"].ToString().Trim();
                        message.time_start = dt.Rows[i]["time_start"].ToString().Trim();
                        message.time_going = dt.Rows[i]["time_going"].ToString().Trim();
                        message.time_end = dt.Rows[i]["time_end"].ToString().Trim();

                        ListMessages.Add(message);
                    }
                    return ListMessages;
                }
            }
        }

        public List<Message> listATSByBuilding(string sql)
        {
            List<Message> ListMessages = new List<Message>();
            DataTable dt = new DataTable();
            using (SqlConnection con = db.getSQLConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    con.Open();
                    da.Fill(dt);
                    da.Dispose();
                    con.Close();
                    Message message;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        message = new Message();
                        message.id_ats = dt.Rows[i]["id_ats"].ToString().Trim();
                        message.ip_address = dt.Rows[i]["ip_address"].ToString().Trim();
                        message.owner_emp = dt.Rows[i]["owner_emp"].ToString().Trim();
                        message.owner_name = dt.Rows[i]["owner_name"].ToString().Trim();
                        message.building = dt.Rows[i]["building"].ToString().Trim();
                        message.team = dt.Rows[i]["team"].ToString().Trim();
                        message.emp_user = dt.Rows[i]["emp_user"].ToString().Trim();
                        message.level_user = dt.Rows[i]["level_user"].ToString().Trim();
                        message.description = dt.Rows[i]["description"].ToString().Trim();
                        message.status = dt.Rows[i]["status"].ToString().Trim();
                        message.time_start = dt.Rows[i]["time_start"].ToString().Trim();
                        message.time_going = dt.Rows[i]["time_going"].ToString().Trim();
                        message.time_end = dt.Rows[i]["time_end"].ToString().Trim();

                        ListMessages.Add(message);
                    }
                    return ListMessages;
                }
            }
        }

        public List<Message> getMessageByID(string id_ats)
        {
            string sql = "SELECT * FROM ATS_Content where id_ats='" + id_ats.Trim()+"'";
            List<Message> ListMessages = new List<Message>();
            DataTable dt = new DataTable();
            using (SqlConnection con = db.getSQLConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    con.Open();
                    da.Fill(dt);
                    da.Dispose();
                    con.Close();
                    Message message;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        message = new Message();
                        message.id_ats = dt.Rows[i]["id_ats"].ToString();
                        message.ip_address = dt.Rows[i]["ip_address"].ToString();
                        message.owner_emp = dt.Rows[i]["owner_emp"].ToString();
                        message.owner_name = dt.Rows[i]["owner_name"].ToString();
                        message.building = dt.Rows[i]["building"].ToString();
                        message.team = dt.Rows[i]["team"].ToString();
                        message.emp_user = dt.Rows[i]["emp_user"].ToString();
                        message.level_user = dt.Rows[i]["level_user"].ToString();
                        message.description = dt.Rows[i]["description"].ToString();
                        message.status = dt.Rows[i]["status"].ToString();
                        message.time_start = dt.Rows[i]["time_start"].ToString();
                        message.time_going = dt.Rows[i]["time_going"].ToString();
                        message.time_end = dt.Rows[i]["time_end"].ToString();
                        ListMessages.Add(message);
                    }
                    return ListMessages;
                }
            }
        }

        public List<Message> getMessageByIP(string ip_address)
        {
            string sql = "SELECT * FROM ATS_Content where ip_address='" + ip_address.Trim() + "'";
            List<Message> ListMessages = new List<Message>();
            DataTable dt = new DataTable();
            using (SqlConnection con = db.getSQLConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    con.Open();
                    da.Fill(dt);
                    da.Dispose();
                    con.Close();
                    Message message;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        message = new Message();
                        message.id_ats = dt.Rows[i]["id_ats"].ToString();
                        message.ip_address = dt.Rows[i]["ip_address"].ToString();
                        message.owner_emp = dt.Rows[i]["owner_emp"].ToString();
                        message.owner_name = dt.Rows[i]["owner_name"].ToString();
                        message.building = dt.Rows[i]["building"].ToString();
                        message.team = dt.Rows[i]["team"].ToString();
                        message.emp_user = dt.Rows[i]["emp_user"].ToString();
                        message.level_user = dt.Rows[i]["level_user"].ToString();
                        message.description = dt.Rows[i]["description"].ToString();
                        message.status = dt.Rows[i]["status"].ToString();
                        message.time_start = dt.Rows[i]["time_start"].ToString();
                        message.time_going = dt.Rows[i]["time_going"].ToString();
                        message.time_end = dt.Rows[i]["time_end"].ToString();
                        ListMessages.Add(message);
                    }
                    return ListMessages;
                }
            }
        }

        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            SqlConnection conn = db.getSQLConnection();

            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(table);
            conn.Close();
            da.Dispose();
            return table;
        }
    }

    public class querySQL
    {
        DBConnection db;
        public querySQL()
        {
            db = new DBConnection();
        }
        public Boolean Execute(string query)
        {
            try
            {
                using (SqlConnection con = db.getSQLConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean Check(string row , string data)
        {
            try
            {
                string query = "select * from ATS_Content where " + row+" ='"+data+"' ";
                using (SqlConnection con = db.getSQLConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}