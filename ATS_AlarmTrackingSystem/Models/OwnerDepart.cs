using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class OwnerDepart
    {
        public string emp { set; get; }
        public string name_cn { set; get; }
        public string name_vn { set; get; }
        public string building { set; get; }
        public string team { set; get; }
        public string trusted { set; get; }
    }
    public class Fedback
    {
        public string id_ats { set; get; }
        public string owner_emp { set; get; }
        public string star { set; get; }
    }

    public class excuteCRC
    {
        DBConnection db;
        public excuteCRC()
        {
            db = new DBConnection();
        }
        public List<OwnerDepart> getOwnerDepart()
        {
            List<OwnerDepart> listOwner = new List<OwnerDepart>();
            DataTable dtowner = new DataTable();
            string query = "SELECT emp,name_cn,name_vn,building,team,trusted FROM [dbo].[Owner_Depart] ";

            getAllMessage getMessage = new getAllMessage();

            dtowner = getMessage.queryDatatable(query);
            if (dtowner.Rows.Count > 0)
            {
                foreach (DataRow row in dtowner.Rows)
                {
                    OwnerDepart owner = new OwnerDepart();
                    owner.emp = row["Emp"].ToString();
                    owner.name_cn = row["name_cn"].ToString();
                    owner.name_vn = row["name_vn"].ToString();
                    owner.building = row["building"].ToString();
                    owner.team = row["team"].ToString();
                    owner.trusted = row["trusted"].ToString();
                    listOwner.Add(owner);
                }
            }
            return listOwner;
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

        public List<OwnerDepart> getOwnerByEMP(string emp)
        {
            List<OwnerDepart> listOwner = new List<OwnerDepart>();
            DataTable dtowner = new DataTable();
            string query = "SELECT emp,name_cn,name_vn,building,team,trusted FROM [dbo].[Owner_Depart] where emp= '"+emp.Trim()+"'";
            getAllMessage getMessage = new getAllMessage();

            dtowner = getMessage.queryDatatable(query);
            if (dtowner.Rows.Count > 0)
            {
                foreach (DataRow row in dtowner.Rows)
                {
                    OwnerDepart owner = new OwnerDepart();
                    owner.emp = row["Emp"].ToString();
                    owner.name_cn = row["name_cn"].ToString();
                    owner.name_vn = row["name_vn"].ToString();
                    owner.building = row["building"].ToString();
                    owner.team = row["team"].ToString();
                    owner.trusted = row["trusted"].ToString();
                    listOwner.Add(owner);
                }
            }
            return listOwner;
        }
    }
}