using ATS_AlarmTrackingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATS_AlarmTrackingSystem.Controllers
{
    public class MinhApiController : ApiController
    {
        DBOhelper dbo = new DBOhelper();

        #region LockUnlockCMC
        [System.Web.Http.Route("api/SearchCMC")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult SearchCMC(string emp)
        {
            LockCMC lcm =new LockCMC();
            Result result = lcm.SearchEMP(emp);
            if (result != null)
            {
                string Message="";
                switch (result.Code)
                {
                    case "ok":
                        Message = "有管控信息(có thông tin kiểm soát)";
                        break;
                    case "0":
                        Message = "未查到管控信息(Không tìm thấy thông tin kiểm soát)";
                        break;
                    case "UserOrPassworderror":
                        Message = "賬號或密碼錯誤(tài khoản hoặc mật khẩu lỗi)";
                        break;
                    case "IPNoAccess":
                        Message = "此IP無效訪問權限(IP này không hợp lệ để truy cập)";
                        break;
                    case "Systemerror":
                        Message = "系統異常(Ngoại lệ)";
                        break;
                }
                string json = JsonConvert.SerializeObject(result);
                var unserialize = JsonConvert.DeserializeObject(json);
                result.Message = Message;
                return Json(result);
            }
            else
            {
                return BadRequest("No result ");
            }
            
        }
        
        [System.Web.Http.Route("api/UnlockCMC")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult UnlockCMC(string emp,string reason)
        {
            emp = emp.Trim().ToUpper();
            
            List<InforLock> list = new List<InforLock>();
            
            string[] arrEmp = emp.Split(',');
            for(int i=0 ; i< arrEmp.Length ; i++)
            {
                InforLock inforLock = new InforLock();
                string empno = arrEmp[i].Trim();
                LockCMC lcm = new LockCMC();
                Result result = lcm.UnlockEMP(empno, reason);
                string Message = "";
                switch (result.Code)
                {
                    case "ok":
                        Message = "恢復成功 (Phục hồi thành công ).";
                        break;
                    case "error":
                        Message = "恢復失敗 (Phục hồi thất bại ).";
                        break;
                    case "UserOrPassworderror":
                        Message = "賬號或密碼錯誤 (Tên đăng nhập hoặc mật khẩu không chính xác).";
                        break;
                    case "IPNoAccess":
                        Message = "此IP無效訪問權限 (IP này không hợp lệ để truy cập).";
                        break;
                    case "Systemerror":
                        Message = "Message : 系統異常 (Hệ thống bất thường).";
                        break;
                }
                string ip = lcm.getIPCom();
                inforLock.STT = (i + 1).ToString();
                inforLock.Emp = empno;
                inforLock.Reason = reason;
                inforLock.Code = result.Code;
                inforLock.Message = Message;
                inforLock.IP = ip;
                list.Add(inforLock);

                string datenow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DataTable dt = new DataTable();
                dt = dbo.queryDatatable("select * from [dbo].[LockUnLockCMC] where Emp ='"+ arrEmp[i].Trim() + "' ");
                Boolean check = false;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "lock")
                    {
                        string sqlUpdate = "update LockUnLockCMC set Status='Unlock', Message='Unlock yeu cau',StatusCheck='Unlock', MessageCheck=N'Unlock yeu cau, "+ip+"',TimeCheck = N'"+datenow+"' where Emp=N'" + arrEmp[i].Trim() + "'";
                        //dbo.Execute(sqlUpdate);
                    }
                    if(dt.Rows[0]["Status"].ToString() == "Unlock")
                    {
                        check = true;
                    }
                }
                else
                {
                    if (check == false)
                    {
                        string sqlInsert = "insert into LockUnLockCMC(Emp,Code,CodeCheck,Status,StatusCheck ,Message,DateInsert,TimeCheck,MessageCheck)";
                        sqlInsert += "values(N'" + arrEmp[i].Trim() + "','ok','ok','Unlock','Unlock','Unlock yeu cau','" + datenow + "','" + datenow + "','Unlock yeu cau ,"+ip+"')";
                        //dbo.Execute(sqlInsert);
                    }
                   
                }
               
            }
            return Json(list);

        }
        #endregion LockUnlockCMC

        //Lay ra danh sach file excel upload 
        [System.Web.Http.Route("api/ListFileUpload")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult ListFileUpload()
        {
            DBOhelper dbo = new DBOhelper();
            try
            {
                string sql = "SELECT MaThe,HoTen,XuongLV,Bu,CFT,BoPhan,DaTiem,NgayTiem1,NgayTiem2,DiaDiemTiem,DiaChiTiem,Loai,BangChung,NguyenNhan,NgayTaiLen,NguoiTaiLen FROM Vaccine_Servey";

                DataTable tbposition;
                tbposition = dbo.queryDatatable(sql);
                string json = JsonConvert.SerializeObject(tbposition);
                var unserialize = JsonConvert.DeserializeObject(json);
                return Json(unserialize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
