using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class LockCMC
    {
        public Result UnlockEMP(string emp, string reason)
        {
            string res = "";
            Result result = null;
            try
            {
                string uri = "http://10.224.54.13:8001//api/DMGCardInfo/RecoveryDMGCardInfo";
                NameValueCollection myValues = new NameValueCollection();
                myValues.Add("EMPNO", emp);
                myValues.Add("REASON", reason);
                myValues.Add("USER", "COVIDMjControl");
                myValues.Add("PASSWORD", "mj$202108");
                using (WebClient wc = new WebClient())
                {
                    byte[] resValue = wc.UploadValues(uri, myValues);
                    res = Encoding.UTF8.GetString(resValue);

                    if (res != null)
                    {
                        Console.WriteLine(res);
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(res);
                        return result;
                    }
                    return result;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }

        }

        public Result SearchEMP(string emp)
        {
            string res = "";
            Result result = null;
            try
            {
                string uri = "http://10.224.54.13:8001//api/DMGCardInfo/GetControlInfo";
                NameValueCollection myValues = new NameValueCollection();
                myValues.Add("EMPNO", emp);
                myValues.Add("USER", "COVIDMjControl");
                myValues.Add("PASSWORD", "mj$202108");
                using (WebClient wc = new WebClient())
                {
                    byte[] resValue = wc.UploadValues(uri, myValues);
                    res = Encoding.UTF8.GetString(resValue);

                    if (res != null)
                    {
                        Console.WriteLine(res);
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(res);
                        return result;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }

        public string getIPCom()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }

    public class Result
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class InforLock
    {
        public string STT { get; set; }
        public string Emp { get; set; }
        public string Reason { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }
    }
    public class DBWhelper
    {
        string conStringw = @"Data Source=10.224.81.131,3000;Initial Catalog=WechatDB;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
        public Boolean Execute(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStringw))
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

        public DataTable getDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(conStringw))
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

    }
   

}