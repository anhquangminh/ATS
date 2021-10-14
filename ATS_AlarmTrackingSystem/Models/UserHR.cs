using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class UserHR
    {
        public string id { set; get; }
        public string mathe { set; get; }
        public string hoten { set; get; }
        public string nguoitailen { set; get; }
        public string thoigiantai { set; get; }
        public string ip { set; get; }

    }
    public class HRhelper
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
        public List<UserHR> getListUser()
        {
            List<UserHR> Listuser= new List<UserHR>();

            HRhelper helper = new HRhelper();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM UserHR order by thoigiantai desc";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserHR hr = new UserHR();
                    hr.id = dt.Rows[i][0].ToString().Trim();
                    hr.mathe = dt.Rows[i][1].ToString().Trim();
                    hr.hoten = dt.Rows[i][2].ToString().Trim();
                    hr.nguoitailen = dt.Rows[i][3].ToString().Trim();
                    hr.thoigiantai = dt.Rows[i][4].ToString().Trim();
                    hr.ip = dt.Rows[i][5].ToString().Trim();

                    Listuser.Add(hr);
                }

            }
            return Listuser;
        }
        public List<UserHR> getByID(string id)
        {
            List<UserHR> Listuser = new List<UserHR>();

            HRhelper helper = new HRhelper();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM UserHR where id='"+id+"'";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserHR hr = new UserHR();
                    hr.id = dt.Rows[i][0].ToString().Trim();
                    hr.mathe = dt.Rows[i][1].ToString().Trim();
                    hr.hoten = dt.Rows[i][2].ToString().Trim();
                    hr.nguoitailen = dt.Rows[i][3].ToString().Trim();
                    hr.thoigiantai = dt.Rows[i][4].ToString().Trim();
                    hr.ip = dt.Rows[i][5].ToString().Trim();

                    Listuser.Add(hr);
                }

            }
            return Listuser;
        }
        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                // create data adapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                    conn.Close();
                    da.Dispose();
                    conn.Close();
                    
                    return table;
                }

            }

        }
        public Boolean checkrow(string sql)
        {
            HRhelper helper = new HRhelper();
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
}