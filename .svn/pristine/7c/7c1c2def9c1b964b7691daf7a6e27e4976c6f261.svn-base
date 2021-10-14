using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace WebLink.Models
{
    public class LinkModel
    {
        public string IDLink { set; get; }
        public string IDdp { set; get; }
        public string NameDP { set; get; }
        public string SystemName { set; get; }
        public string Description { set; get; }
        public string Owner { set; get; }
        public string Contact { set; get; }
        public string Email { set; get; }
        public string Link { set; get; }
        public string STT { set; get; }

    }

    public class getLink
    {
        DBConnection db;
        public getLink()
        {
            db = new DBConnection();
        }
        public List<LinkModel> GetListLink(string txtsearch)
        {
            string sql;
            if (string.IsNullOrEmpty(txtsearch))
            {
                sql = "SELECT Link.*,Department.Name FROM Link,Department where Link.IDdp=Department.IDdp";
            }
            else
            {
                sql = "SELECT Link.*,Department.Name FROM Link,Department where Link.IDdp=Department.IDdp and link.IDdp='" + txtsearch+"'";
            }

            List<LinkModel> Listlink = new List<LinkModel>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            LinkModel tmpLink;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpLink = new LinkModel();
                tmpLink.IDLink = dt.Rows[i]["IDLink"].ToString();
                tmpLink.IDdp = dt.Rows[i]["IDdp"].ToString();
                tmpLink.Description = dt.Rows[i]["Description"].ToString();
                tmpLink.Contact = dt.Rows[i]["Contact"].ToString();
                tmpLink.Link = dt.Rows[i]["Link"].ToString();
                tmpLink.NameDP = dt.Rows[i]["Name"].ToString();
                tmpLink.SystemName = dt.Rows[i]["SystemName"].ToString();
                tmpLink.Owner = dt.Rows[i]["Owner"].ToString();
                tmpLink.Email = dt.Rows[i]["Email"].ToString();
                tmpLink.STT = (i + 1).ToString();
                Listlink.Add(tmpLink);
            }
            return Listlink;
        }
        public List<LinkModel> GetLinkByID(string id)
        {
            string sql = "SELECT * FROM Link where IDLink='" + id+ "'";

            List<LinkModel> Listlink = new List<LinkModel>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            LinkModel tmpLink;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpLink = new LinkModel();
                tmpLink.IDLink = dt.Rows[i]["IDLink"].ToString();
                tmpLink.IDdp = dt.Rows[i]["IDdp"].ToString();
                tmpLink.Description = dt.Rows[i]["Description"].ToString();
                tmpLink.Contact = dt.Rows[i]["Contact"].ToString();
                tmpLink.Link = dt.Rows[i]["Link"].ToString();
                tmpLink.SystemName = dt.Rows[i]["SystemName"].ToString();
                tmpLink.Owner = dt.Rows[i]["Owner"].ToString();
                tmpLink.Email = dt.Rows[i]["Email"].ToString();

                Listlink.Add(tmpLink);
            }
            return Listlink;
        }

        public void InsertLink(LinkModel linhModel)
        {
            //string sql = "insert into Link(IDdp,Description,Contact,Link) values ('" + IDdp + "',N'" + Description + "',N'" + Contact + "',N'" + Link + "')";
            string sql = "insert into Link(IDdp,SystemName,Description,Owner,Contact,Email,Link) values";
            sql += "('"+linhModel.IDdp+"',N'"+linhModel.SystemName+"',N'"+linhModel.Description+"',N'"+linhModel.Owner+"',N'"+linhModel.Contact+"',N'"+linhModel.Email + "',N'"+linhModel.Link+"')";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void UpdateLink(LinkModel lm)
        {
            string sql = "UPDATE Link SET IDdp='"+lm.IDdp+"', Description =N'" + lm.Description + "',Owner=N'"+lm.Owner+ "',Email=N'"+lm.Email+"',Contact = N'" + lm.Contact + "',Link= N'"+lm.Link + "' WHERE IDLink = '" + lm.IDLink+"'";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteLink(string id)
        {
            string sql = "delete from Link where IDLink=" + id;
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}