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
    public class AccountModel
    {
        public string IDAcount { set; get; }

        [Required(ErrorMessage = "UserName  is required")]
        [StringLength(30, ErrorMessage = "UserName can be no larger than 30 characters")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "PublicName  is required")]
        [StringLength(30, ErrorMessage = "PublicName can be no larger than 30 characters")]
        public string PublicName { set; get; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Password can be no larger than 30 characters")]
        public string Password { set; get; }
        public string Level { set; get; }
        public string Building { set; get; }
    }

    public class getAcount
    {
        DBConnection db;
        public getAcount()
        {
            db = new DBConnection();
        }
        public List<AccountModel> listAcount(string emp_no, string password)
        {
            string sql;
            if (string.IsNullOrEmpty(emp_no) && string.IsNullOrEmpty(password))
            {
                sql = "SELECT * FROM Acounts";
            }
            else
            {
                sql = "SELECT * FROM Acounts where UserName = '" + emp_no.Trim() + "' and Password='" + password.Trim() + "'";
            }

            List<AccountModel> Listac = new List<AccountModel>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            AccountModel tmpAcount;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpAcount = new AccountModel();
                tmpAcount.IDAcount = dt.Rows[i]["IDAcount"].ToString();
                tmpAcount.UserName = dt.Rows[i]["UserName"].ToString();
                tmpAcount.PublicName = dt.Rows[i]["PublicName"].ToString();
                tmpAcount.Password = dt.Rows[i]["Password"].ToString();
                tmpAcount.Level = dt.Rows[i]["Level"].ToString().Trim();
                tmpAcount.Building = dt.Rows[i]["Building"].ToString().Trim();

                Listac.Add(tmpAcount);
            }
            return Listac;
        }

        public void insertAcount(AccountModel ac)
        { 
            using (SqlConnection con = db.getConnection())
            {
                try {
                    string sql = "insert into Acounts (UserName,PublicName,Password,Level) values ('" + ac.UserName + "',N'" + ac.PublicName + "',N'" + ac.Password + "','1')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }
    }
}