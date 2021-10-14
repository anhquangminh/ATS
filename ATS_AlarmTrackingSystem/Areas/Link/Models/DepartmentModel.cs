using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebLink.Models
{
    public class DepartmentModel
    {
        public string IDdp { set; get; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { set; get; }
        public string Note { set; get; }
        public string STT { set; get; }

    }
    public class getDepartment
    {
        DBConnection db;
        public getDepartment()
        {
            db = new DBConnection();
        }
        public List<DepartmentModel> getListDepartment()
        {
            string sql = "SELECT * from Department";

            List<DepartmentModel> Listdp = new List<DepartmentModel>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            DepartmentModel tmpdp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpdp = new DepartmentModel();
                tmpdp.IDdp = dt.Rows[i]["IDdp"].ToString();
                tmpdp.Name = dt.Rows[i]["Name"].ToString();
                tmpdp.Note = dt.Rows[i]["Note"].ToString();
                tmpdp.STT = (i+1).ToString();

                Listdp.Add(tmpdp);
            }
            return Listdp;
        }
        public void UpdateDeparment(DepartmentModel dp)
        {
            string sql = "UPDATE Department SET [Name] =N'" + dp.Name.ToUpper() + "',[Note] = '" + dp.Note+ "' WHERE [IDdp] ='" + dp.IDdp + "' ";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void DeleteDeparment(string id)
        {
            string sql = " delete from Department where IDdp = " + id;
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void InsertDeparment(string Name,string Note)
        {
            string sql = "insert into Department(Name,Note) values ('" + Name.ToUpper() + "','" + Note + "')";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Boolean CompareDeparment(string Name)
        {
            string sql = "SELECT * from Department where Name ='"+Name.ToUpper()+"'";

            List<DepartmentModel> Listdp = new List<DepartmentModel>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            if(dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        public List<DepartmentModel> getIDDepartment(string id)
        {
            string sql = "SELECT * from Department where IDdp ='"+id+"'";

            List<DepartmentModel> Listdp = new List<DepartmentModel>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            DepartmentModel tmpdp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpdp = new DepartmentModel();
                tmpdp.IDdp = dt.Rows[i]["IDdp"].ToString();
                tmpdp.Name = dt.Rows[i]["Name"].ToString();
                tmpdp.Note = dt.Rows[i]["Note"].ToString();
                tmpdp.STT = (i + 1).ToString();

                Listdp.Add(tmpdp);
            }
            return Listdp;
        }
    }
}