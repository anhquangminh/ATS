using ATS_AlarmTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS_AlarmTrackingSystem.Controllers
{
    public class ShareController : Controller
    {
        [SessionCheck]
        public ActionResult InsertFile()
        {
            Session["Message"] = null;
            return View();
        }

        DBOhelper dbo = new DBOhelper();
        DBCMMHelper dbcmc = new DBCMMHelper();

        [SessionCheck]
        [HttpPost]
        public ActionResult InsertFile(HttpPostedFileBase file)
        {
            //HttpPostedFileBase file = Request.Files[0];

            InsertExcel insertExcel = new InsertExcel();
            string nguoitailen = (string)Session["UserName"];
            string Level = (string)Session["Level"];
            string Message = null;
            string ngaytai = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string filePath = string.Empty;

            try {
                if (Level == "3")
                {
                    if (file != null)
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        if (extension != ".xls" || extension != ".xlsx")
                        {
                            file.SaveAs(filePath);

                            DataTable dt = insertExcel.DataExcel(filePath, extension);
                            //insert vao database
                            if (dt.Rows.Count > 0)
                            {
                                string sqlLog = @"insert into  Vaccine_Servey_Log(MaThe,HoTen,XuongLV,Bu,CFT,BoPhan,DaTiem,NgayTiem1,NgayTiem2,DiaDiemTiem,DiaChiTiem,Loai,BangChung,NguyenNhan,NgayTaiLen,NguoiTaiLen) 
                                            select MaThe,HoTen,XuongLV,Bu,CFT,BoPhan,DaTiem,NgayTiem1,NgayTiem2,DiaDiemTiem,DiaChiTiem,Loai,BangChung,NguyenNhan,NgayTaiLen,NguoiTaiLen from Vaccine_Servey where NguoiTaiLen ='" + nguoitailen + "'";
                                dbo.Execute(sqlLog);
                                string sqlDelete = @"delete from Vaccine_Servey where NguoiTaiLen='" + nguoitailen + "'";
                                dbo.Execute(sqlDelete);
                                int count = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if(!string.IsNullOrEmpty(dt.Rows[i][0].ToString()) && !string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                                    {
                                        count++;
                                        string sqlValues = @"values(N'" + dt.Rows[i][0].ToString() + "',N'" + dt.Rows[i][1].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "'," +
                                            "N'" + dt.Rows[i][4].ToString() + "',N'" + dt.Rows[i][5].ToString() + "',N'" + dt.Rows[i][6].ToString() + "',N'" + dt.Rows[i][7].ToString() + "',N'" + dt.Rows[i][8].ToString() + "'" +
                                            ",N'" + dt.Rows[i][9].ToString() + "',N'" + dt.Rows[i][10].ToString() + "',N'" + dt.Rows[i][11].ToString() + "',N'" + dt.Rows[i][12].ToString() + "',N'" + dt.Rows[i][13].ToString() + "',N'" + ngaytai + "',N'" + nguoitailen + "')";

                                        string sqlInsert = @"insert into Vaccine_Servey (MaThe,HoTen,XuongLV,Bu,CFT,BoPhan,DaTiem,NgayTiem1,NgayTiem2,DiaDiemTiem,DiaChiTiem,Loai,BangChung,NguyenNhan,NgayTaiLen,NguoiTaiLen) ";
                                        sqlInsert += sqlValues;
                                        dbo.Execute(sqlInsert);
                                    }
                                }
                                Message = "Upload succes : " + count + " rows";
                            }
                            else
                            {
                                Message = "File excel no data.";
                            }
                        }
                        else
                        {
                            Message = "Please choose file .xls or .xlsx!";
                        }
                    }
                    //else
                    //{
                    //    Message = "File empty !";
                    //}
                }
                else
                {
                    Message = "The account: " + nguoitailen + " does not have permission to upload to the system !";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
           
            Session["Message"] = Message;
            return View();
        }

        public ActionResult DownloadFileExcel ()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Download/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Vaccine_Servey.xlsx");
            string fileName = "Vaccine_Servey.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult SearchInject()
        {
            string Message = null;
            return View();
        }

        [HttpGet]
        public ActionResult SearchInject(string emp)
        {
          
            string Message = "";
            List<VACCINE> listvc = new List<VACCINE>();
            if (!string.IsNullOrEmpty(emp))
            {
                emp = emp.Trim().ToUpper();
                string sql = @"SELECT distinct emp_no,emp_name,mac,ip,fbdesc,max(getcardtime) as scan_out_time  from [dbo].[VIEW_COVID] 
                        where mac in('2C:52','2C:60','2D:0A') and emp_no='" + emp + "'  and DATEPART(day,getcardtime)=DATEPART(day,getdate()) " +
                        "group by  emp_no,emp_name,mac,ip,fbdesc ";


                string sqlMap = "select * from EMP_MAPPING where EmpNo_HR=N'"+emp+"'";
                DataTable dtm = new DataTable();
                dtm = dbo.queryDatatable(sqlMap);
                string empCN;
                if(dtm.Rows.Count > 0)
                {
                    empCN = dtm.Rows[0][2].ToString();
                    sql = @"SELECT distinct emp_no,emp_name,mac,ip,fbdesc,max(getcardtime) as scan_out_time  from [dbo].[VIEW_COVID] 
                        where mac in('2C:52','2C:60','2D:0A') and emp_no='" + empCN + "'  and DATEPART(day,getcardtime)=DATEPART(day,getdate()) " +
                        "group by  emp_no,emp_name,mac,ip,fbdesc ";
                }

                DataTable dt = dbcmc.queryDatatable(sql);
                if(dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        VACCINE vc = new VACCINE();
                        vc.emp_no = dt.Rows[i]["emp_no"].ToString();
                        vc.emp_name = dt.Rows[i]["emp_name"].ToString();
                        vc.mac = dt.Rows[i]["mac"].ToString();
                        vc.ip = dt.Rows[i]["ip"].ToString();
                        vc.fbdesc = dt.Rows[i]["fbdesc"].ToString();
                        vc.scan_out_time = dt.Rows[i]["scan_out_time"].ToString();
                        listvc.Add(vc);
                    }
                }
                else
                {
                    Message = "No search results : "+emp+"./ Khong tim thay ket qua nao :"+emp;
                }
                
            }
            Session["Message"] = Message;
            return View(listvc);
        }
        public ActionResult Test()
        {
            return View();
        }

    }
}