using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS_AlarmTrackingSystem.Models
{
    public class InsertExcel
    {
        public DataTable DataExcel(string filePath, string extension)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(filePath))
            {
                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }
                conString = string.Format(conString, filePath);
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
            }
            return dt;
        }
    }
    public class ExportExcel
    {
        public void ToExcel(HttpResponseBase Response, object clientsList)
        {
            GridView grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = null;
            grid.DataBind();
            grid.DataSource = clientsList;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Minh-IT.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
        }
        public void ExportToExcel(HttpResponseBase Response, DataTable tb)
        {
            GridView grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = null;
            grid.DataBind();
            grid.DataSource = tb;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Minh-IT.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
        }
    }
    public class Temp
    {
        public string recid { set; get; }
        public string device_ip { set; get; }
        public string device_name { set; get; }
        public string emp_no { set; get; }
        public string emp_name { set; get; }
        public string face_time { set; get; }
        public string temperature { set; get; }
        public string is_get { set; get; }

        //recid,device_ip,device_name,emp_no,emp_name,face_time,temperature,is_get
    }
    public class getData
    {
        
        public List<Temp> getListTemp(string sql)
        {
            string strMysql = "Server=10.134.196.123;Database=devices;port=3306;User Id=cnsbg;password=Cnsbgit_0918;Charset=utf8";

            List<Temp> listTemp = new List<Temp>();
            using (MySqlConnection con = new MySqlConnection(strMysql))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Temp temp = new Temp();
                            //recid,device_ip,device_name,emp_no,emp_name,face_time,temperature,is_get
                            temp.recid = dr[0].ToString();
                            temp.device_ip = dr[1].ToString();
                            temp.device_name = dr[2].ToString();
                            temp.emp_no = dr[3].ToString();
                            temp.face_time = dr[4].ToString();
                            temp.temperature = dr[5].ToString();
                            temp.is_get = dr[6].ToString();
                            

                            listTemp.Add(temp);
                        }
                    }
                    // close and dispose the objects
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                    string datetimenow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                   
                }
                catch (MySqlException ex) // catches only Oracle errors
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return listTemp;


        }

        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            string strcon = @"Data Source=10.224.81.131,3000;Initial Catalog=WechatDB;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
            SqlConnection conn = new SqlConnection(strcon);
            //"select * from [dbo].[download_excel]"
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            da.Dispose();
            conn.Close();
            return table;
        }
        public Boolean Execute(string query)
        {
            try
            {
                string strcon = @"Data Source=10.224.81.131,3000;Initial Catalog=WechatDB;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
                using (SqlConnection con = new SqlConnection(strcon))
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
    }
}