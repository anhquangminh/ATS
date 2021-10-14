using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class WIndowOS
    {
        public string Owner { set; get; }
        public string Package { set; get; }
        public string WindowOS  { set; get; }
        public string MSrelease { set; get; }
        public string ITrelease { set; get; }
        public string Input_date { set; get; }
    }
    public class WindowOShelper
    {
        string conString = @"Data Source=10.224.81.131,3000;Initial Catalog=findip;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
        public List<WIndowOS> getListWindowOS(string id)
        {
            List<WIndowOS> ListWIndowOS = new List<WIndowOS>();

            WindowOShelper helper = new WindowOShelper();
            DataTable dt = new DataTable();
            string sql;
            if (!string.IsNullOrEmpty(id))
            {
                sql = "SELECT * FROM PackagePublic_date where package='"+id.Trim()+"'";
            }
            else
            {
                sql = "SELECT * FROM PackagePublic_date";
            }
            
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WIndowOS os = new WIndowOS();
                    os.Owner = dt.Rows[i][0].ToString().Trim();
                    os.Package = dt.Rows[i][1].ToString().Trim();
                    os.WindowOS = dt.Rows[i][2].ToString().Trim();
                    os.MSrelease = dt.Rows[i][3].ToString().Trim();
                    os.ITrelease = dt.Rows[i][4].ToString().Trim();
                    os.Input_date = dt.Rows[i][5].ToString().Trim();

                    ListWIndowOS.Add(os);
                }

            }
            return ListWIndowOS;
        }
        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

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
    }
}