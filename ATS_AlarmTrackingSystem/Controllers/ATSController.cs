using ATS_AlarmTrackingSystem.Models;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebLink.Models;

namespace ATS_AlarmTrackingSystem.Controllers
{
    public class ATSController : Controller
    {
        // infor ATS
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password, string OTP)
        {
            //if (!string.IsNullOrEmpty(OTP))
            //{
            //    string result = CheckOPT(OTP, UserName);
            //    if (result == "0")
            //    {
            getAcount accList = new getAcount();
            List<AccountModel> data;
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                data = accList.listAcount(UserName, Password).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["IDAcount"] = data.FirstOrDefault().IDAcount;
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    Session["PublicName"] = data.FirstOrDefault().PublicName;
                    Session["Password"] = data.FirstOrDefault().Password;
                    Session["Level"] = data.FirstOrDefault().Level;
                    Session["Building"] = data.FirstOrDefault().Building;

                    return RedirectToAction("Index", "ATS");

                }
                else
                {
                    ViewBag.ErrorMessage = "Wrong username or password ";
                    return View("Login");
                }

            }
            else
            {
                ViewBag.ErrorMessage = "User name or password is empty !";
            }
            //    }
            //    else
            //    {
            //        string message = "";
            //        switch (result)
            //        {
            //            case "-1":
            //                message = "??????(????)";
            //                break;
            //            case "-2":
            //                message = "OpenId(????)";
            //                break;
            //            case "-3":
            //                message = "????(?????Token)";
            //                break;
            //            case "-4":
            //                message = "Token????(???????)";
            //                break;
            //            case "-5":
            //                message = "Token??(??Token??)";
            //                break;
            //            case "-6":
            //                message = "Token??";
            //                break;
            //            case "-7":
            //                message = "?systemName????????token (??systemPrivilege??????)";
            //                break;
            //            case "-8":
            //                message = "????Token(??????Token)";
            //                break;
            //            case "-9":
            //                message = "????????";
            //                break;
            //            case "-10":
            //                message = "???????(SystemPrivilege)";
            //                break;
            //            case "-11":
            //                message = "????????Token??(systemUserInfo???)";
            //                break;
            //            case "-12":
            //                message = "????????Token??(systemUserInfo? status=0)";
            //                break;
            //            case "-13":
            //                message = "???token??";
            //                break;
            //            case "-20":
            //                message = "???";
            //                break;
            //            case "-100":
            //                message = "????";
            //                break;

            //        }
            //        ViewBag.ErrorMessage = message;
            //    }
            //}
            //else
            //{
            //    ViewBag.ErrorMessage = "Please enter OTP Icivet ";
            //}

            return View();

        }
        public static string CheckOPT(string OPT, string account)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://civetinterface.foxconn.com/FxTokenWeb/api/Validate/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"appid\":\"2XA5CKB9d.hjpf1qNhznJA2\",\"password\":\"Foxconn0517\",\"account\" :\"" + account + "\",\"token\":\"" + OPT + "\",\"type\":0}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                dynamic stuff = JsonConvert.DeserializeObject(responseText);

                string result = stuff.result;

                return result;
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("index");
        }
        public ActionResult Index()
        {

            //List<DataPoint> dataPoints = new List<DataPoint>();

            //dataPoints.Add(new DataPoint("V0944125", 121));
            //dataPoints.Add(new DataPoint("V0943312", 67));
            //dataPoints.Add(new DataPoint("V0954254", 70));
            //dataPoints.Add(new DataPoint("V0984552", 56));
            //dataPoints.Add(new DataPoint("V0985517", 42));
            //dataPoints.Add(new DataPoint("V0985521", 41));
            //dataPoints.Add(new DataPoint("V0365421", 42));
            //dataPoints.Add(new DataPoint("V0998424", 21));

            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            //getAllMessage getAll = new getAllMessage();
            //getAll.listATS("level")
            return View();
        }

        //Canten
        public ActionResult Test()
        {
            return View();
        }
        [SessionCheck]
        public ActionResult MealTrace()
        {
            return View();
        }
        Cantenhelper ct = new Cantenhelper();
        [SessionCheck]
        public ActionResult SearchNear()
        {
            return View();
        }
        [SessionCheck]
        public ActionResult TBView()
        {
            return View();
        }
        public ActionResult FindMealEat()
        {
            return View();
        }
        public ActionResult SearchHistoryCanten()
        {
            return View();
        }
        public ActionResult findDorm()
        {
            return View();
        }
        public ActionResult CanteenAnalysis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CanteenAnalysis(string datestart, string dateend)
        {
            string Message = null;
            try
            {
                string sql = @"SELECT DISTINCT F_SITE,F_STATES,F_MealType,SUBSTRING(F_SCANLUNCH,1,10) AS F_SCANLUNCH,F_EMPNO,F_EMPNAME,
                            (CASE  WHEN(F_DEPARTNAME IS NULL OR F_DEPARTNAME = '') AND(F_BU = 'NULL') THEN 'No FG'
                            WHEN(F_DEPARTNAME IS NOT NULL OR F_DEPARTNAME <> '') AND(F_BU != 'NULL') THEN F_BU
                            WHEN(F_DEPARTNAME IS NOT NULL OR F_DEPARTNAME <> '') AND(F_BU = 'NULL') THEN 'HR not config'
                            else 'no' end ) F_BU , 
                            (CASE WHEN F_MESSAGE LIKE N'%未查到您的座位信息%' THEN 'NO_POSITION'
                            WHEN F_MESSAGE LIKE N'%用餐區域錯誤%' THEN 'WRONG_AERA'
                            WHEN F_MESSAGE LIKE N'%您未提報%' THEN 'NO_MEAL'
                            WHEN F_MESSAGE LIKE N'%用餐時段錯誤%' THEN 'WRONG_TIME'
                            WHEN F_MESSAGE LIKE N'%總務未維護此用餐位置%' THEN  'POSITION_IVALID'
                            WHEN F_STATES = 'NO SCAN' AND(F_MESSAGE IS NULL or F_MESSAGE = '') THEN 'NO_SCAN'
                            WHEN F_STATES = 'OK' AND(F_MESSAGE IS NULL  or F_MESSAGE = '') THEN 'OK'
                            ELSE 'NO'
                            END ) MESS
                            FROM SCAN_QR_LUNCH where  F_SCANLUNCH BETWEEN '" + datestart + "' and '" + dateend + "'";
                List<CantenAnalis> list = new List<CantenAnalis>();
                DataTable tb = new DataTable();
                tb = helper.queryDatatable(sql);
                if (tb.Rows.Count > 0)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        CantenAnalis ca = new CantenAnalis();
                        ca.F_SITE = tb.Rows[i]["F_SITE"].ToString();
                        ca.F_STATES = tb.Rows[i]["F_STATES"].ToString();
                        ca.F_MealType = tb.Rows[i]["F_MealType"].ToString();
                        ca.F_SCANLUNCH = tb.Rows[i]["F_SCANLUNCH"].ToString();
                        ca.F_EMPNO = tb.Rows[i]["F_EMPNO"].ToString();
                        ca.F_EMPNAME = tb.Rows[i]["F_EMPNAME"].ToString();
                        ca.F_BU = tb.Rows[i]["F_BU"].ToString();
                        ca.MESS = tb.Rows[i]["MESS"].ToString();
                        list.Add(ca);
                    }
                }
                if (list.Count > 0)
                {
                    ExportExcel export = new ExportExcel();
                    export.ToExcel(Response, list);
                }
                else
                {
                    Message = "No find data";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            ViewBag.Message = Message;
            return View();
        }
        //Canten

        KTXhelper helper = new KTXhelper();
        HRhelper hr = new HRhelper();
        [SessionCheck]
        public ActionResult ExcelHR()
        {
            ViewBag.Data = hr.getListUser();
            return View();
        }
        public ActionResult SearchInfor()
        {
            //http://10.224.69.100/postman/api/hr/getEmpObj?id
            return View();
        }
        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Download/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Employ.xlsx");
            string fileName = "Employ.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [SessionCheck]
        [HttpPost]
        public ActionResult ExcelHR(HttpPostedFileBase file)
        {
            string datainsert = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");
            string sql = "select * from [dbo].[UserHR] where thoigiantai > '" + datainsert + "' and  filename like'%.xlsx'";
            if (hr.checkrow(sql))
            {
                Session["Message"] = "Da up du lieu ngay " + datainsert;
                ViewBag.Message = "Da up du lieu ngay " + datainsert;
            }
            else
            {
                DateTime dateTimeC = DateTime.Now;

                string UserName = (string)Session["UserName"];

                string filePath = string.Empty;
                if (file != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

                    string filename = UserName + "_" + file.FileName;

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

                    DataTable dt = new DataTable();
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

                    List<UserHR> Listuser = new List<UserHR>();

                    int count = 0;
                    string inputDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    if (dt.Rows.Count > 0)
                    {
                        string query_history = "insert into [dbo].[UserHR_History](mathe,hoten,nguoitailen,thoigiantai,filename) select mathe,hoten,nguoitailen,thoigiantai,filename from [dbo].[UserHR]";
                        hr.Execute(query_history);
                        string query_delete = "delete from [dbo].[UserHR]";
                        if (hr.Execute(query_delete))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                count++;

                                UserHR uhr = new UserHR();

                                uhr.mathe = dt.Rows[i][0].ToString().Trim();
                                uhr.hoten = dt.Rows[i][1].ToString().Trim();
                                uhr.nguoitailen = UserName;
                                uhr.thoigiantai = inputDate;
                                uhr.ip = getIPCom();

                                string query = "insert into UserHR(mathe,hoten,nguoitailen,thoigiantai,ip,filename)";
                                query += "  values ";
                                query += "(N'" + uhr.mathe + "',N'" + uhr.hoten + "', N'" + uhr.nguoitailen + "',N'" + uhr.thoigiantai + "',N'" + uhr.ip + "',N'" + filename + "')";
                                hr.Execute(query);

                                Listuser.Add(uhr);
                            }
                        }
                        else
                        {
                            Session["Message"] = "Loi truy van ";
                        }

                    }
                    else
                    {
                        Session["Message"] = "Khong co du lieu ";
                        ViewBag.Message = "Khong co du lieu";
                    }

                    if (Listuser != null)
                    {
                        ViewBag.Data = Listuser;
                    }
                }
                else
                {
                    Session["Message"] = "File khong co du lieu";
                    ViewBag.Message = "File khong co du lieu";
                }
            }
            return RedirectToAction("ExcelHR");

        }
        public ActionResult Network(string USERNAME)
        {
            GetInforPC infPC = new GetInforPC();
            USERNAME = "10.224.41.152";
            List<PCIssue> list = new List<PCIssue>();
            if (!string.IsNullOrEmpty(USERNAME))
            {
                USERNAME = USERNAME.Replace(".", "-");
                PCIssue pcissue = new PCIssue();
                pcissue.USERNAME = USERNAME.Trim();
                pcissue.SYSTEM = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='SYSTEM' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.Port1433 = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='1433' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.Port445 = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='445' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.Port3389 = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='3389' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.USB = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='USB' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.USBPROTECT = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='USBPROTECT' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.SYMANTECTUPDATE = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='SYMANTECTUPDATE' ORDER BY INPUT_DATE DESC", "IPS");
                pcissue.VIRUS_PROTECTION = infPC.GetQuery(" USERNAME='" + USERNAME + "' and hr ='VIRUS_PROTECTION' ORDER BY INPUT_DATE DESC", "IPS");
                list.Add(pcissue);
            }

            return View(list);
        }

        //date chart
        public ContentResult LineChart()
        {
            getAllMessage getdata = new getAllMessage();
            List<DataPoint> dataPoints = new List<DataPoint>();

            string sql = "select distinct owner_emp FROM ATS_Content where owner_emp is not null";
            DataTable tb = getdata.queryDatatable(sql);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                string emp = tb.Rows[i]["owner_emp"].ToString();
                string sqlCount = "select * FROM ATS_Content where owner_emp='"+emp+"'";
                DataTable dtCount = getdata.queryDatatable(sqlCount);
                int count = dtCount.Rows.Count;
                dataPoints.Add(new DataPoint(emp, count));
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
        public ContentResult PieChart()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            getAllMessage getdata = new getAllMessage();
           
            string sqlOpen = "select * FROM ATS_Content where status ='OPEN'";
            DataTable tbOpen = getdata.queryDatatable(sqlOpen); 

            string sqlClose = "select * FROM ATS_Content where status ='CLOSE'";
            DataTable tbClose = getdata.queryDatatable(sqlClose);

            string sqlOn = "select * FROM ATS_Content where status ='ON_GOING'";
            DataTable tbOn = getdata.queryDatatable(sqlOn);
            
            dataPoints.Add(new DataPoint("Open", tbOpen.Rows.Count));
            dataPoints.Add(new DataPoint("On going", tbOn.Rows.Count));
            dataPoints.Add(new DataPoint("Close", tbClose.Rows.Count));
            
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");

        }
        //date chart

        //user insert layout
        public ActionResult InsertPos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertPos(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

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

                DataTable dt = new DataTable();
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
                string ext = "";
                string Group_Pos = "1";
                string Name_Position, Position_intX, Position_intY, Position_int;

                string strcon = @"Data Source=10.224.81.131,3000;Initial Catalog=OWNCLOUD_DATA;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (dt.Rows.Count > 0)
                    {
                        con.Open();
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[r][c].ToString() != "")
                                {
                                    string strName = "NA";
                                    string[] strNamesArray = dt.Rows[r][c].ToString().Split('-');

                                    if (strNamesArray.Any(x => x == strName))
                                    {
                                        Name_Position = dt.Rows[r][c].ToString();
                                    }
                                    else
                                    {
                                        Name_Position = "N/A";
                                    }
                                    string[] arr = Name_Position.Split('-');
                                    //Group_Pos = arr[2];
                                }
                                else
                                {
                                    Name_Position = "N/A";
                                }
                                Position_int = r.ToString() + c.ToString();
                                Position_intX = r.ToString();
                                Position_intY = c.ToString();

                                string query = "insert into LayoutExcel(Name_Position,Group_Pos,Position_intX,Position_intY,Position_int) values (N'" + Name_Position + "',N'" + Group_Pos + "',N'" + Position_intX + "',N'" + Position_intY + "',N'" + Position_int + "')";
                               
                                using (SqlCommand cmd = new SqlCommand(query, con))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                Console.WriteLine(query);
                            }
                        }
                        con.Close();
                    } 
                }
            }
            return View();
        }
        //user insert layout

        //upload danh sach nguoi khong cho ra cong A2
        [SessionCheck]
        public ActionResult InsertScanPort()
        {
            //DBOhelper dbo = new DBOhelper();
            //dbo.getListScanPort()
            return View();
        }

        [SessionCheck]
        [HttpPost]
        public ActionResult InsertScanPort(HttpPostedFileBase file)
        {
            string Message = "";
            DBOhelper dbo = new DBOhelper();
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

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

                DataTable dt = new DataTable();
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

                string nguoitailen = (string)Session["UserName"];
                string ngaytailen = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                if (dt.Rows.Count > 0)
                {
                    string sqlbackup = @"insert into ViTriQuetTheBackUp(MaThe,HoTen,Site,PhanNhan,BU,CFT,BoPhan,XuongLV,ViTriQuet,NguoiTaiLen,NgayTaiLen) 
                     select MaThe, HoTen, Site, PhanNhan, BU, CFT, BoPhan, XuongLV, ViTriQuet, NguoiTaiLen, NgayTaiLen from ViTriQuetThe where NguoiTaiLen = '"+ nguoitailen + "'";
                    if (dbo.Execute(sqlbackup))
                    {
                        string sqldelete = "delete from ViTriQuetThe WHERE NguoiTaiLen=N'" + nguoitailen.Trim().ToUpper() + "'";
                        if (dbo.Execute(sqldelete))
                        {
                            int count = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                                {
                                    count++;
                                    string query = @"insert into ViTriQuetThe(MaThe,HoTen,Site,PhanNhan,BU,CFT,BoPhan,XuongLV,ViTriQuet,NguoiTaiLen,NgayTaiLen) values 
                                            (N'" + dt.Rows[i][1].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "',N'" + dt.Rows[i][4].ToString() + "',N'" + dt.Rows[i][5].ToString() + "'," +
                                                    "N'" + dt.Rows[i][6].ToString() + "',N'" + dt.Rows[i][7].ToString() + "',N'" + dt.Rows[i][8].ToString() + "',N'" + dt.Rows[i][9].ToString() + "',N'" + nguoitailen + "',N'" + ngaytailen + "')";
                                    dbo.Execute(query);
                                }

                            }
                            Message = "Insert Succes : " + count+" rows.";
                        }
                    }
                }
                else
                {
                    Message = "File is null";
                }
            }
            else
            {
                Message = "No file";
            }
            Session["Message"] = Message;
            return View();
        }
       
        public ActionResult DownloadFileScanPort()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Download/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Format_ViTriQuetThe.xlsx");
            string fileName = "Format_ViTriQuetThe.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        //upload danh sach nguoi khong cho ra cong A2


        //Isolation cach ly
        [SessionCheck]
        public ActionResult InsertInforF()
        {
            return View();
        }

        [SessionCheck]
        [HttpPost]
        public ActionResult InsertInforF(HttpPostedFileBase file)
        {
            string nguoitailen = (string)Session["UserName"];
            string Level = (string)Session["Level"];
            string Message = null;
            string thoigiantai = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string filePath = string.Empty;
            if(Level == "3")
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
                    file.SaveAs(filePath);

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

                    DataTable dt = new DataTable();
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
                    //check cot inh trang xem co bi trung k 
                    Boolean checktinhtrang = false;
                    Boolean colunm = false;
                    if(dt.Columns.Count >= 37)
                    {
                        colunm = true;
                    }

                    string rowerro = "0";
                    if(dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string tinhtrang = dt.Rows[i][36].ToString().Trim();
                            if (!string.IsNullOrEmpty(dt.Rows[i][10].ToString()) || !string.IsNullOrEmpty(tinhtrang) )
                            {
                                if (tinhtrang == "隔離中" || tinhtrang == "解除" || tinhtrang == "新增")
                                {
                                    checktinhtrang = true;
                                }
                                else
                                {
                                    checktinhtrang = false;
                                    rowerro = (i + 1).ToString();
                                }
                            }
                            else
                            {
                                checktinhtrang = false;
                                rowerro = (i + 1).ToString();
                            }
                        }

                    }

                   if(checktinhtrang == true && colunm==true)
                    {
                        //insert vao database
                        string strcon = @"Data Source=10.224.81.131,3000;Initial Catalog=OWNCLOUD_DATA;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DBOhelper dbo = new DBOhelper();
                                string sqlbk = @"INSERT INTO ThongTinCLBackUp(NhaXuong,PhapNhan,ToaXuong,BU,CFT,BoPhan,Chuyen,CaLV,IDL,MaNhanVien,TenTV,TenTQ,GioiTinh,NgaySinh,DienThoai,PhuongTien,DCTamTru,DCThuongTru,LoaiHinhCNV,NgayLVCuoi,NgayTiepXuc,CachLyNgay,DuKienKetThucCL,ChonDoTX,NguyenNhanCT,LoaiHinhCL,PhuongThucCL,NgayXN,DiaDiemXN,TiemVaccien,ThoiGianTiem,DiaDiemTiem,DaKetThucCL,TinhTrangCL,NgayKTCL,TinhTrang,NguoiTaiLen,NgayTaiLen) 
                                    SELECT  NhaXuong,PhapNhan,ToaXuong,BU,CFT,BoPhan,Chuyen,CaLV,IDL,MaNhanVien,TenTV,TenTQ,GioiTinh,NgaySinh,DienThoai,PhuongTien,DCTamTru,DCThuongTru,LoaiHinhCNV,NgayLVCuoi,NgayTiepXuc,CachLyNgay,DuKienKetThucCL,ChonDoTX,NguyenNhanCT,LoaiHinhCL,PhuongThucCL,NgayXN,DiaDiemXN,TiemVaccien,ThoiGianTiem,DiaDiemTiem,DaKetThucCL,TinhTrangCL,NgayKTCL,TinhTrang,NguoiTaiLen,NgayTaiLen FROM ThongTinCL WHERE NguoiTaiLen='" + nguoitailen.Trim().ToUpper() + "'";

                                if (dbo.Execute(sqlbk))
                                {

                                    string sqldelete = "delete from ThongTinCL WHERE NguoiTaiLen='" + nguoitailen.Trim().ToUpper() + "'";
                                    if (dbo.Execute(sqldelete))
                                    {
                                        con.Open();
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (!string.IsNullOrEmpty(dt.Rows[i][10].ToString()) || !string.IsNullOrEmpty(dt.Rows[i][11].ToString()))
                                            {

                                                string sql = @"insert into ThongTinCL(NhaXuong,PhapNhan,ToaXuong,BU,CFT,BoPhan,Chuyen,CaLV,IDL,MaNhanVien,TenTV,TenTQ,GioiTinh,NgaySinh,DienThoai,PhuongTien,DCTamTru,DCThuongTru,LoaiHinhCNV,NgayLVCuoi
                                                ,NgayTiepXuc,CachLyNgay,DuKienKetThucCL,ChonDoTX,NguyenNhanCT,LoaiHinhCL,PhuongThucCL,NgayXN,DiaDiemXN,TiemVaccien,ThoiGianTiem,DiaDiemTiem,DaKetThucCL,TinhTrangCL,NgayKTCL,TinhTrang,NguoiTaiLen,NgayTaiLen)
                        	                    values(N'" + dt.Rows[i][1].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "',N'" + dt.Rows[i][4].ToString() + "',N'" + dt.Rows[i][5].ToString() + "'," +
                                                "N'" + dt.Rows[i][6].ToString() + "',N'" + dt.Rows[i][7].ToString() + "',N'" + dt.Rows[i][8].ToString() + "',N'" + dt.Rows[i][9].ToString() + "',N'" + dt.Rows[i][10].ToString() + "',N'" + dt.Rows[i][11].ToString() + "',N'" + dt.Rows[i][12].ToString() + "'," +
                                                "N'" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][14].ToString() + "',N'" + dt.Rows[i][15].ToString() + "',N'" + dt.Rows[i][16].ToString() + "',N'" + dt.Rows[i][17].ToString() + "',N'" + dt.Rows[i][18].ToString() + "',N'" + dt.Rows[i][19].ToString() + "'," +
                                                "N'" + dt.Rows[i][20].ToString() + "',N'" + dt.Rows[i][21].ToString() + "',N'" + dt.Rows[i][22].ToString() + "',N'" + dt.Rows[i][23].ToString() + "',N'" + dt.Rows[i][24].ToString() + "',N'" + dt.Rows[i][25].ToString() + "',N'" + dt.Rows[i][26].ToString() + "'," +
                                                "N'" + dt.Rows[i][27].ToString() + "',N'" + dt.Rows[i][28].ToString() + "',N'" + dt.Rows[i][29].ToString() + "',N'" + dt.Rows[i][30].ToString() + "',N'" + dt.Rows[i][31].ToString() + "',N'" + dt.Rows[i][32].ToString() + "',N'" + dt.Rows[i][33].ToString() + "'," +
                                                "N'" + dt.Rows[i][34].ToString() + "',N'" + dt.Rows[i][35].ToString() + "',N'" + dt.Rows[i][36].ToString().Trim() + "',N'" + nguoitailen + "',N'" + thoigiantai + "')";

                                                using (SqlCommand cmd = new SqlCommand(sql, con))
                                                {
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        con.Close();
                                    }
                                }
                            }
                        }
                        Message = "Upload succes : " + dt.Rows.Count + " rows";
                    }
                    else
                    {
                        Message = "Erro : Colunm 隔離中/ TinhTrang is different : 隔離中, 解除, 新增 | Please check file excel .";
                    }

                }
                else
                {
                    Message = "File empty !";
                }
                
            }
            else
            {
                Message = "The account: "+ nguoitailen + " does not have permission to upload to the system !";
            }
            
            Session["Message"] = Message;
            return View();
        }

       

        public JsonResult ListIsolation()
        {
            DBOhelper dbo = new DBOhelper();
            return Json(dbo.getListIsolation(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteIsolation(string id)
        {
            DBOhelper dbo = new DBOhelper();
            string query = "Delete from [dbo].[ThongTinCL] where id='" + id.Trim() + "'";
            return Json(dbo.Execute(query), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyIDIsolation(string id)
        {
            DBOhelper dbo = new DBOhelper();
            return Json(dbo.getByID(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateIsolation(Isolation io)
        {
            DBOhelper dbo = new DBOhelper();
            string query = "update [dbo].[ThongTinCL] set NhaXuong=N'" + io.NhaXuong + "',PhapNhan=N'" + io.PhapNhan + "',ToaXuong=N'" + io.ToaXuong + "',BU=N'" + io.BU + "',CFT=N'" + io.CFT + "',";
            query += "BoPhan=N'" + io.BoPhan + "',Chuyen=N'" + io.Chuyen + "',CaLV=N'" + io.CaLV + "',IDL=N'" + io.IDL + "',MaNhanVien=N'" + io.MaNhanVien + "',TenTV=N'" + io.TenTV + "'";
            query += ",TenTQ=N'" + io.TenTQ + "',GioiTinh=N'" + io.GioiTinh + "',NgaySinh=N'" + io.NgaySinh + "',DienThoai=N'" + io.DienThoai + "',PhuongTien=N'" + io.PhuongTien + "',DCTamTru=N'" + io.DCTamTru + "'";
            query += ",DCThuongTru=N'" + io.DCThuongTru + "',LoaiHinhCNV=N'" + io.LoaiHinhCNV + "',NgayLVCuoi=N'" + io.NgayLVCuoi + "',NgayTiepXuc=N'" + io.NgayTiepXuc + "',CachLyNgay=N'" + io.CachLyNgay + "'";
            query += ",DuKienKetThucCL=N'" + io.DuKienKetThucCL + "',ChonDoTX=N'" + io.ChonDoTX + "',NguyenNhanCT=N'" + io.NguyenNhanCT + "',LoaiHinhCL=N'" + io.LoaiHinhCL + "',PhuongThucCL=N'" + io.PhuongThucCL + "',NgayXN=N'" + io.NgayXN + "'"; 
             query += ",DiaDiemXN=N'" + io.DiaDiemXN + "',TiemVaccien=N'" + io.TiemVaccien + "',ThoiGianTiem=N'" + io.ThoiGianTiem + "',DiaDiemTiem=N'" + io.DiaDiemTiem + "',DaKetThucCL=N'" + io.DaKetThucCL + "',TinhTrangCL=N'" + io.TinhTrangCL + "',NgayKTCL=N'" + io.NgayKTCL + "',TinhTrang=N'" + io.TinhTrang + "' ";
            query += "where id='" + io.id + "'";
            if (dbo.Execute(query))
            {
                return Json("Update succes", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Erro query ", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DownloadFileIsolation()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Download/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "FormatCachLy.xlsx");
            string fileName = "FormatCachLy.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //Isolation cach ly

        // upload goi package window os
        WindowOShelper oshelper = new WindowOShelper();
        [SessionCheck]
        public ActionResult WindowOS()
        {
            return View();
        }
        [SessionCheck]
        [HttpPost]
        public ActionResult WindowOS(HttpPostedFileBase file)
        {
            DateTime dateTimeC = DateTime.Now;
            string nameU = "";
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string fileN = nameU + "_" + file.FileName;
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

                DataTable dt = new DataTable();
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

                conString = @"Data Source=10.224.81.131,3000;Initial Catalog=findip;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string mathe = "";
                    try
                    {
                        string Owner = ""; string Package = ""; string WindowOS = ""; string MSrelease = ""; string ITrelease = "";
                        string Inputdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        int count = 0;
                        if (dt.Rows.Count > 0)
                        {
                            con.Open();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Owner = dt.Rows[i][0].ToString().Trim();
                                Package = dt.Rows[i][1].ToString().Trim();
                                WindowOS = dt.Rows[i][2].ToString().Trim();
                                MSrelease = dt.Rows[i][3].ToString().Trim();
                                ITrelease = dt.Rows[i][4].ToString().Trim();
                                string str = MSrelease.Replace("/", "-");
                                string a = str.Split(' ')[0];
                                string str1 = ITrelease.Replace("/", "-");
                                string b = str1.Split(' ')[0];


                                if (!string.IsNullOrEmpty(MSrelease))
                                {
                                    DateTime dMSrelease = DateTime.ParseExact(a, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                    DateTime dITrelease = DateTime.ParseExact(b, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                    int mm = dMSrelease.Month;
                                    int month = DateTime.Now.Month - 1;
                                    //if (mm == month)
                                    //{
                                    count++;
                                    string query = "insert into PackagePublic_date(owner,package,windowversion,MS_Release,IT_improve_Date,input_date) values  ";
                                    query += "(N'" + Owner + "',N'" + Package + "',N'" + WindowOS + "',N'" + dMSrelease + "',N'" + dITrelease + "','" + Inputdate + "')";

                                    using (SqlCommand cmd = new SqlCommand(query, con))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                    //}
                                    //else
                                    //{
                                    //    ViewBag.Message = " Date row MSrelease in month " + month;
                                    //}
                                }
                                else
                                {
                                    ViewBag.Message = " Date row MSrelease null";
                                }

                            }
                            con.Close();
                            if (count > 0)
                            {
                                ViewBag.Message = "Insert " + count + " rows in datable";
                            }

                        }
                        string ipIm = getIPCom();

                        if (con.State != ConnectionState.Closed) con.Close();

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Import data fail " + ex.Message;
                    }
                }
            }
            return View();
        }
        //upload goi OS theo xuong

        // onfig owner theo xuong cho CRC
        excuteCRC extCRC = new excuteCRC();
        public ActionResult ConfigOwner()
        {

            return View();
        }
        // onfig owner theo xuong cho CRC

        //Download excel
        public ActionResult DownLoadExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DownLoadExcel(string date, string selecttime)
        {
            string Message = null;
            if (!string.IsNullOrEmpty(date))
            {

                string dateinput = date.Replace("-", "/");
                string day = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

                DateTime dateIn = Convert.ToDateTime(dateinput);
                DateTime datenow = DateTime.Now;
                TimeSpan dateTotal = datenow - dateIn;

                if (dateTotal.Days >= 0)
                {
                    string query = "";
                    switch (selecttime)
                    {
                        case "8":
                            query = "SELECT* from cnsbg_rlcw_records WHERE device_name IN ('GZF073F1','GZF073F2','GZF073F3','GZF073F4')  AND face_time LIKE '%" + date.Trim() + "%' AND HOUR(face_time) >8 AND HOUR(face_time)<=13 ;";
                            break;
                        case "13":
                            query = "SELECT* from cnsbg_rlcw_records WHERE device_name IN ('GZF073F1','GZF073F2','GZF073F3','GZF073F4') AND face_time LIKE '%" + date.Trim() + "%' AND HOUR(face_time) <= 8 ";
                            break;
                        case "19":
                            query = "SELECT* from cnsbg_rlcw_records WHERE device_name IN ('GZF073F1','GZF073F2','GZF073F3','GZF073F4') AND face_time LIKE '%" + date.Trim() + "%' AND HOUR(face_time) > 13 AND HOUR(face_time)<= 19 ";
                            break;
                    }

                    getData getdata = new getData();
                    List<Temp> model = getdata.getListTemp(query);
                    if (model.Count > 0)
                    {
                        ExportExcel export = new ExportExcel();
                        export.ToExcel(Response, model);
                    }
                    else
                    {
                        Message = "Khong co du lieu cua ngay :" + day;
                    }

                }
                else
                {
                    Message = "Ngay lay du lieu phai nho hon ngay hien tai ! :" + datenow;
                }
            }
            else Message = "Vui long nhap vao ngay lay du lieu ! ";

            ViewBag.Message = Message;
            return View();
        }
        [SessionCheck]
        public ActionResult DownLoadScanIcivet()
        {
            return View();
        }
        [SessionCheck]
        [HttpPost]
        public ActionResult DownLoadScanIcivet(string date,string Site)
        {
            string Message = null;
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(Site))
            {

                string dateinput = date.Replace("-", "/");
                string day = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

                DateTime dateIn = Convert.ToDateTime(dateinput);
                DateTime datenow = DateTime.Now;
                TimeSpan dateTotal = datenow - dateIn;

                if (dateTotal.Days >= 0)
                {

                    string date1 = dateIn.ToString("yyyy-MM-dd 00:00:00.000");
                    string date2 = dateIn.ToString("yyyy-MM-dd 23:59:59.000");
                    string query = "select * from [VW_DETECT_LOCATION_DOW] where date >= '"+date1+"' and date <= '"+ date2 + "' and SITES='"+Site+"'";

                    DBOhelper dbo = new DBOhelper();
                    DataTable tb = dbo.queryDatatable(query);
                   
                    if (tb.Rows.Count > 0)
                    {
                        ExportExcel export = new ExportExcel();
                        export.ExportToExcel(Response, tb);
                    }
                    else
                    {
                        Message = "Khong co du lieu cua ngay :" + day;
                    }

                }
                else
                {
                    Message = "Ngay lay du lieu phai nho hon ngay hien tai ! :" + datenow;
                }
            }
            else Message = "Vui long nhap vao ngay lay du lieu ! ";

            ViewBag.Message = Message;
            return View();
        }
        //Download excel

        //tra cuu list thong tin 

        //tra cuu list thong tin 


        //Json config owner theo xuong cho CRC
        public JsonResult ListOwner()
        {
            return Json(extCRC.getOwnerDepart(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddOwner(OwnerDepart ownerDepart)
        {
            WebServiceInfor.PostmanService serviceInfor = new WebServiceInfor.PostmanService();
            DataTable data = new DataTable();
            data = serviceInfor.GetEmpInfomation(ownerDepart.emp);
            string name_cn = data.Rows[0]["USER_NAME"].ToString();
            if (data.Rows.Count > 0)
            {
                List<OwnerDepart> owner = new List<OwnerDepart>();
                owner = extCRC.getOwnerByEMP(ownerDepart.emp);
                if (owner.Count <= 0)
                {
                    string query = "insert into Owner_Depart (emp,name_cn,name_vn,building,team) values";
                    query += "(N'" + ownerDepart.emp + "',N'" + name_cn + "',N'" + ownerDepart.name_vn + "',N'" + ownerDepart.building + "',N'" + ownerDepart.team + "')";
                    if (extCRC.Execute(query))
                    {
                        return Json("Add succes !", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Erro query !", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("Emp already exist! ");
                }

            }
            else
            {
                return Json("Emp erro ");
            }


        }
        public JsonResult GetbyOwnerID(string emp)
        {
            return Json(extCRC.getOwnerByEMP(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateOwner(OwnerDepart ownerDepart)
        {
            WebServiceInfor.PostmanService serviceInfor = new WebServiceInfor.PostmanService();
            DataTable data = serviceInfor.GetEmpInfomation(ownerDepart.emp);
            string name_cn = data.Rows[0]["USER_NAME"].ToString();
            string query = "update Owner_Depart set emp=N'" + ownerDepart.emp + "',name_vn=N'" + ownerDepart.name_vn + "',building=N'" + ownerDepart.building + "',team=N'" + ownerDepart.team + "' where emp='" + ownerDepart.emp + "'";
            if (extCRC.Execute(query))
            {
                return Json("Update succes", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Erro query ", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Delete(string emp)
        {
            string query = "Delete Owner_Depart where emp='" + emp + "'";
            return Json(extCRC.Execute(query), JsonRequestBehavior.AllowGet);
        }
        //Json config owner theo xuong cho CRC


        //Json upload goi package window os
        public JsonResult JsonWindowOS()
        {
            return Json(oshelper.getListWindowOS(null), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteOS(string id)
        {
            string query = "Delete PackagePublic_date where package='" + id.Trim() + "'";
            return Json(oshelper.Execute(query), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOSID(string id)
        {
            return Json(oshelper.getListWindowOS(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateOS(WIndowOS os)
        {
            string query = "update PackagePublic_date set owner=N'" + os.Owner + "',package=N'" + os.Package + "',windowversion=N'" + os.WindowOS + "',MS_Release=N'" + os.MSrelease + "'";
            query += ",IT_improve_Date=N'" + os.ITrelease + "' where Package='" + os.Input_date + "'";
            if (oshelper.Execute(query))
            {
                return Json("Update succes", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Erro query ", JsonRequestBehavior.AllowGet);
            }

        }
        //json upload goi package window os


        // Json tai len excel thong tin nguoi HR
        public JsonResult GetbyIDHR(string id)
        {
            return Json(hr.getByID(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateHR(UserHR uhr)
        {

            string query = "update UserHR set mathe=N'" + uhr.mathe + "',hoten=N'" + uhr.hoten + "' where id='" + uhr.id + "'";

            string nguoisuadoi = (string)Session["UserName"];
            string ip = getIPCom();
            string mathe = uhr.ip + "->" + uhr.mathe;
            string query_log = "insert into UserHR_Log(mathe,nguoisuadoi,thoigiansua,ip) values(N'" + mathe + "',N'" + nguoisuadoi + "',getdate(),N'" + ip + "')";
            hr.Execute(query_log);

            if (hr.Execute(query))
            {
                return Json("Update succes", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Erro query ", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DeleteHR(string id, string mathe)
        {
            string query = "Delete FROM UserHR where id='" + id.Trim() + "'";
            string nguoisuadoi = (string)Session["UserName"];
            string ip = getIPCom();
            string query_log = "insert into UserHR_Log(mathe,nguoisuadoi,thoigiansua,ip) values(N'" + mathe + "',N'" + nguoisuadoi + "',getdate(),N'" + ip + "')";
            hr.Execute(query_log);
            return Json(hr.Execute(query), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUserHR(UserHR user)
        {
            string nguoitailen = (string)Session["UserName"];
            string inputDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string ip = getIPCom();
            string query = "insert into UserHR(mathe,hoten,nguoitailen,thoigiantai,ip,filename)";
            query += "  values ";
            query += "(N'" + user.mathe + "',N'" + user.hoten + "', N'" + nguoitailen + "',N'" + inputDate + "',N'" + ip + "',N'B?ng tay')";
            if (hr.Execute(query) == true)
            {
                return Json("Them Thenh cong", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Them that bai ", JsonRequestBehavior.AllowGet);
            }


        }
        //Json tai len excel thong tin nguoi HR


        // Lay IP may user
        public string getIPCom()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        // Lay IP may user

        //Canten
        MysqlHelper mysql = new MysqlHelper();
        public JsonResult getUserCanten(string emp)
        {
            string sql = " SELECT empno,empname,position, date_format(MAX(TIME),'%Y-%m-%d %T') AS time FROM VN_Record.record_vn WHERE POSITION LIKE 'NA-%' AND empno IN(sELECT DISTINCT employeecode from VN_Record.consumedate_vn WHERE ConsumeRestaurant in ('GJ','GW','HT'))  and empno = '" + emp + "' GROUP BY empno,empname,position order by time desc;";
            return Json(mysql.getList(sql), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPositionF(string emp, string time)
        {
            DataTable tb = new DataTable();
            List<UserCanten> list = new List<UserCanten>();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPositionFS(string emp, string position, string time, int select)
        {
            List<UserCanten> list = new List<UserCanten>();
            DataTable tbF1;
            DataTable tbF2;
            DataTable tbF3;
            string query_positon_f1;
            string query_positon_f2;
            string query_positon_f3;
            string position_f1 = null;
            string position_f2 = null;
            string position_f3 = null;

            UserCanten ctr = new UserCanten();
            ctr.empno = emp;

            ctr.position = position;
            ctr.time = time;
            ctr.ff = "Range";
            DataTable tb = new DataTable();
            tb = mysql.getDataTable("select EmpName from VN_Record.record_vn where EmpNo='" + emp.Trim() + "' limit 1");
            if (tb.Rows.Count > 0)
            {
                ctr.empname = tb.Rows[0][0].ToString();
            }
            else
            {
                ctr.empname = emp;
            }
            list.Add(ctr);

            switch (select)
            {
                case 0:
                    query_positon_f1 = "SELECT * FROM CantenConfigF where NoteID='" + position.Trim() + "' and type='F1' ";
                    query_positon_f2 = "SELECT * FROM CantenConfigF where NoteID='" + position.Trim() + "' and type='F2' ";
                    query_positon_f3 = "SELECT * FROM CantenConfigF where NoteID='" + position.Trim() + "' and type='F3' ";
                    tbF1 = ct.queryDatatable(query_positon_f1);
                    tbF2 = ct.queryDatatable(query_positon_f2);
                    tbF3 = ct.queryDatatable(query_positon_f3);

                    if (tbF1.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbF1.Rows.Count; i++)
                        {
                            if (i == (tbF1.Rows.Count - 1))
                            {
                                position_f1 += "'" + tbF1.Rows[i][1].ToString().Replace("\r\n", string.Empty) + "'";
                            }
                            else
                            {
                                position_f1 += "'" + tbF1.Rows[i][1].ToString().Replace("\r\n", string.Empty) + "',";
                            }

                        }
                    }
                    if (tbF2.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbF2.Rows.Count; i++)
                        {
                            if (i == (tbF2.Rows.Count - 1))
                            {
                                position_f2 += "'" + tbF2.Rows[i][1].ToString().Replace("\r\n", string.Empty) + "'";
                            }
                            else
                            {
                                position_f2 += "'" + tbF2.Rows[i][1].ToString().Replace("\r\n", string.Empty) + "',";
                            }

                        }
                    }
                    if (tbF3.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbF3.Rows.Count; i++)
                        {
                            if (i == (tbF3.Rows.Count - 1))
                            {
                                position_f3 += "'" + tbF3.Rows[i][1].ToString().Replace("\r\n", string.Empty) + "'";
                            }
                            else
                            {
                                position_f3 += "'" + tbF3.Rows[i][1].ToString().Replace("\r\n", string.Empty) + "',";
                            }

                        }
                    }

                    if (position_f1 != null)
                    {
                        string sqlF1 = "select  EmpNo,EmpName,Position,date_format(TIME,'%Y-%m-%d %T') AS time  FROM record_vn WHERE POSITION LIKE 'NA-%' and Position in (" + position_f1 + ") and Time between DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL - 15 MINUTE) and DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL 15 MINUTE) order by time desc ";
                        DataTable tbr1 = new DataTable();
                        tbr1 = mysql.getDataTable(sqlF1);
                        if (tbr1.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbr1.Rows.Count; i++)
                            {
                                UserCanten ct = new UserCanten();
                                ct.empno = tbr1.Rows[i][0].ToString();
                                ct.empname = tbr1.Rows[i][1].ToString();
                                ct.position = tbr1.Rows[i][2].ToString();
                                ct.time = tbr1.Rows[i][3].ToString();
                                ct.ff = "Range 1";

                                list.Add(ct);
                            }
                        }
                    }
                    if (position_f2 != null)
                    {
                        string sqlF2 = "select  EmpNo,EmpName,Position,date_format(TIME,'%Y-%m-%d %T') AS time  FROM record_vn WHERE POSITION LIKE 'NA-%' and Position in (" + position_f2 + ") and Time between DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL - 15 MINUTE) and DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL 15 MINUTE) order by time desc ";
                        DataTable tbr2 = new DataTable();
                        tbr2 = mysql.getDataTable(sqlF2);
                        if (tbr2.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbr2.Rows.Count; i++)
                            {
                                UserCanten ct = new UserCanten();
                                ct.empno = tbr2.Rows[i][0].ToString();
                                ct.empname = tbr2.Rows[i][1].ToString();
                                ct.position = tbr2.Rows[i][2].ToString();
                                ct.time = tbr2.Rows[i][3].ToString();
                                ct.ff = "Range 2";

                                list.Add(ct);
                            }
                        }
                    }
                    if (position_f3 != null)
                    {
                        string sqlF3 = "select  EmpNo,EmpName,Position,date_format(TIME,'%Y-%m-%d %T') AS time  FROM record_vn WHERE POSITION LIKE 'NA-%' and Position in (" + position_f3 + ") and Time between DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL - 15 MINUTE) and DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL 15 MINUTE) order by time desc";
                        DataTable tbr3 = new DataTable();
                        tbr3 = mysql.getDataTable(sqlF3);
                        if (tbr3.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbr3.Rows.Count; i++)
                            {
                                UserCanten ct = new UserCanten();
                                ct.empno = tbr3.Rows[i][0].ToString();
                                ct.empname = tbr3.Rows[i][1].ToString();
                                ct.position = tbr3.Rows[i][2].ToString();
                                ct.time = tbr3.Rows[i][3].ToString();
                                ct.ff = "Range 3";

                                list.Add(ct);
                            }
                        }
                    }
                    break;
                case 1:
                    query_positon_f1 = "SELECT * FROM CantenConfigF where NoteID='" + position.Trim() + "' and type='F1' ";
                    tbF1 = ct.queryDatatable(query_positon_f1);
                    if (tbF1.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbF1.Rows.Count; i++)
                        {
                            if (i == (tbF1.Rows.Count - 1))
                            {
                                position_f1 += "'" + tbF1.Rows[i][1].ToString() + "'";
                            }
                            else
                            {
                                position_f1 += "'" + tbF1.Rows[i][1].ToString() + "',";
                            }

                        }
                    }
                    if (position_f1 != null)
                    {
                        string sqlF1 = "select  EmpNo,EmpName,Position,date_format(TIME,'%Y-%m-%d %T') AS time  FROM record_vn WHERE POSITION LIKE 'NA-%' and Position in (" + position_f1 + ") and Time between DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL - 15 MINUTE) and DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL 15 MINUTE) order by time desc";
                        DataTable tbr1 = new DataTable();
                        tbr1 = mysql.getDataTable(sqlF1);
                        if (tbr1.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbr1.Rows.Count; i++)
                            {
                                UserCanten ct = new UserCanten();
                                ct.empno = tbr1.Rows[i][0].ToString();
                                ct.empname = tbr1.Rows[i][1].ToString();
                                ct.position = tbr1.Rows[i][2].ToString();
                                ct.time = tbr1.Rows[i][3].ToString();
                                ct.ff = "Range 1";

                                list.Add(ct);
                            }
                        }
                    }
                    break;
                case 2:
                    query_positon_f2 = "SELECT * FROM CantenConfigF where NoteID='" + position.Trim() + "' and type='F2' ";
                    tbF2 = ct.queryDatatable(query_positon_f2);
                    if (tbF2.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbF2.Rows.Count; i++)
                        {
                            if (i == (tbF2.Rows.Count - 1))
                            {
                                position_f2 += "'" + tbF2.Rows[i][1].ToString() + "'";
                            }
                            else
                            {
                                position_f2 += "'" + tbF2.Rows[i][1].ToString() + "',";
                            }

                        }
                    }
                    if (position_f2 != null)
                    {
                        string sqlF2 = "select  EmpNo,EmpName,Position,date_format(TIME,'%Y-%m-%d %T') AS time  FROM record_vn WHERE POSITION LIKE 'NA-%' and Position in (" + position_f2 + ") and Time between DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL - 15 MINUTE) and DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL 15 MINUTE) order by time desc ";
                        DataTable tbr2 = new DataTable();
                        tbr2 = mysql.getDataTable(sqlF2);
                        if (tbr2.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbr2.Rows.Count; i++)
                            {
                                UserCanten ct = new UserCanten();
                                ct.empno = tbr2.Rows[i][0].ToString();
                                ct.empname = tbr2.Rows[i][1].ToString();
                                ct.position = tbr2.Rows[i][2].ToString();
                                ct.time = tbr2.Rows[i][3].ToString();
                                ct.ff = "Range 2";

                                list.Add(ct);
                            }
                        }
                    }
                    break;
                case 3:
                    query_positon_f3 = "SELECT * FROM CantenConfigF where NoteID='" + position.Trim() + "' and type='F3' ";
                    tbF3 = ct.queryDatatable(query_positon_f3);
                    if (tbF3.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbF3.Rows.Count; i++)
                        {
                            if (i == (tbF3.Rows.Count - 1))
                            {
                                position_f3 += "'" + tbF3.Rows[i][1].ToString() + "'";
                            }
                            else
                            {
                                position_f3 += "'" + tbF3.Rows[i][1].ToString() + "',";
                            }

                        }
                    }
                    if (position_f3 != null)
                    {
                        string sqlF3 = "select  EmpNo,EmpName,Position,date_format(TIME,'%Y-%m-%d %T') AS time  FROM record_vn WHERE POSITION LIKE 'NA-%' and Position in (" + position_f3 + ") and Time between DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL - 15 MINUTE) and DATE_ADD(date_format('" + time.Trim() + "','%Y-%m-%d %T'),INTERVAL 15 MINUTE) order by time desc";
                        DataTable tbr3 = new DataTable();
                        tbr3 = mysql.getDataTable(sqlF3);
                        if (tbr3.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbr3.Rows.Count; i++)
                            {
                                UserCanten ct = new UserCanten();
                                ct.empno = tbr3.Rows[i][0].ToString();
                                ct.empname = tbr3.Rows[i][1].ToString();
                                ct.position = tbr3.Rows[i][2].ToString();
                                ct.time = tbr3.Rows[i][3].ToString();
                                ct.ff = "Range 3";

                                list.Add(ct);
                            }
                        }
                    }
                    break;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDate(string emp, DateTime timestart, DateTime timeend, int select)
        {
            List<UserCanten> list = new List<UserCanten>();
            try
            {
                string time1 = timestart.ToString("yyyy-MM-dd 00:00:00");
                string time2 = timeend.ToString("yyyy-MM-dd 23:59:00");
                string sql = "select empno,empname,Position, date_format(MAX(TIME),'%Y-%m-%d %T') AS time  from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1 + "' and time <= '" + time2 + "' and Position like 'NA%' group by empno,empname,Position order by time desc;";
                DataTable tbposition;
                tbposition = mysql.getDataTable(sql);

                if (tbposition.Rows.Count > 0)
                {
                    //var jsonString =getPositionF("V0939994", "NA-QW-B11-1F-01-23", "2021-07-15 09:58:21");
                    //list.AddRange((List<UserCanten>)jsonString.Data);
                    for (int i = 0; i < tbposition.Rows.Count; i++)
                    {
                        string emp1 = tbposition.Rows[i][0].ToString().Trim();
                        string position1 = tbposition.Rows[i][2].ToString().Trim();
                        string time11 = tbposition.Rows[i][3].ToString().Trim();
                        var jsonString = getPositionFS(emp1, position1, time11, select);
                        list.AddRange((List<UserCanten>)jsonString.Data);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDataTree(string emp, DateTime timestart, DateTime timeend, int select)
        {
            List<Position> list = new List<Position>();
            List<UserCanten> listct = new List<UserCanten>();
            try
            {
                string time1 = timestart.ToString("yyyy-MM-dd 00:00:00");
                string time2 = timeend.ToString("yyyy-MM-dd 23:59:00");
                var jsonString = GetF0(emp, time1, time2);
                list.AddRange((List<Position>)jsonString.Data);

                if (list.Count > 0)
                {
                    for (int i =0;i< list.Count;i++)
                    {
                        UserCanten ct = new UserCanten();
                        ct.empno = list[i].EmpNo;
                        ct.empname = list[i].EmpName;
                        ct.position = list[i].Pos;
                        ct.time = list[i].Time;
                        ct.ff = "F0";
                        listct.Add(ct);
                        List<Position> listF1 = new List<Position>();
                        var jsonF1 = GetF1(list[0].Pos, list[0].Time);
                        listF1.AddRange((List<Position>)jsonF1.Data);
                        if (listF1.Count > 0)
                        {
                            for (int i1 = 0; i1 < listF1.Count; i1++)
                            {
                                UserCanten ct1 = new UserCanten();
                                ct1.empno = listF1[i].EmpNo;
                                ct1.empname = listF1[i].EmpName;
                                ct1.position = listF1[i].Pos;
                                ct1.time = listF1[i].Time;
                                ct1.ff = "F1";
                                listct.Add(ct1);

                                List<Position> listF2 = new List<Position>();
                                var jsonF2 = GetF1(listF1[i1].Pos,listF1[i1].Time);
                                listF2.AddRange((List<Position>)jsonF2.Data);
                                if (listF2.Count > 0)
                                {
                                    for (int i2 = 0; i2 < listF2.Count; i2++)
                                    {
                                        UserCanten ct2 = new UserCanten();
                                        ct2.empno = listF2[i].EmpNo;
                                        ct2.empname = listF2[i].EmpName;
                                        ct2.position = listF2[i].Pos;
                                        ct2.time = listF2[i].Time;
                                        ct2.ff = "F2";
                                        listct.Add(ct2);
                                        List<Position> listF3 = new List<Position>();
                                        var jsonF3 = GetF1(listF2[i2].Pos, listF2[i2].Time);
                                        listF3.AddRange((List<Position>)jsonF3.Data);
                                        if(listF3.Count > 0)
                                        {
                                            for (int i3 = 0; i3 < listF3.Count; i3++)
                                            {
                                                UserCanten ct3 = new UserCanten();
                                                ct3.empno = listF3[i].EmpNo;
                                                ct3.empname = listF3[i].EmpName;
                                                ct3.position = listF3[i].Pos;
                                                ct3.time = listF3[i].Time;
                                                ct3.ff = "F3";
                                                listct.Add(ct3);
                                            }
                                        }

                                    }

                                }

                            }

                        }
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<UserCanten> lct = listct.Distinct().ToList();
            return Json(lct, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetF0(string emp, string timeStart, string timeEnd)
        {
            MysqlHelper mysql = new MysqlHelper();
            //Get emp location
            DataTable tb = new DataTable();

            string sql = string.Format("SELECT Position,date_format(TIME,'%Y-%m-%d %T') AS time, EmpNo,EmpName FROM VN_Record.record_vn where EmpNo='{0}' and time between '{1}' and '{2}' and Position like 'NA%'", emp, timeStart, timeEnd);
            tb = mysql.getDataTable(sql);
            List<Position> listPos = new List<Position>();
            if (tb != null)
            {
                foreach (DataRow dr in tb.Rows)
                {
                    listPos.Add(new Position(dr["Position"].ToString(), dr["Time"].ToString(), dr["EmpNo"].ToString(), dr["EmpName"].ToString()));
                }
            }
            
            return Json(listPos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetF1(string position, string time)
        {
            Cantenhelper ct = new Cantenhelper();
            MysqlHelper mysql = new MysqlHelper();
            position = position.Trim();
            List<Position> res = new List<Position>();
            string sql = string.Format("SELECT Position FROM CanteenConf where NoteID='{0}'", position);
            DataTable dt = ct.queryDatatable(sql);
            string listNearPos = "";
            if (dt.Rows.Count > 0)
            {
                listNearPos = dt.Rows[0][0].ToString();
            }
            sql = "select EmpNo,EmpName,Position, date_format(TIME,'%Y-%m-%d %T') AS time FROM record_vn WHERE POSITION LIKE 'NA-%' and Time between DATE_ADD('{0}',INTERVAL - 15 MINUTE) and DATE_ADD('{0}',INTERVAL 15 MINUTE) and Position = '{1}' order by time desc";
            foreach (string pos in listNearPos.Split(';'))
            {
                dt = mysql.getDataTable(string.Format(sql, time, pos.Trim()));
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        res.Add(new Position(dr["Position"].ToString(), dr["Time"].ToString(), dr["EmpNo"].ToString(), dr["EmpName"].ToString()));
                    }
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //Canten

        //get dư lieu theo thoi gian a va b cua F0,f1,f2,f3
        public JsonResult getTstartTend(string emp, DateTime timestart, DateTime timeend, int selectaround)
        {
            List<UserCanten> list = new List<UserCanten>();
            try
            {
                string time1 = timestart.ToString("yyyy-MM-dd 00:00:00");
                string time2 = timeend.ToString("yyyy-MM-dd 23:59:59");
                string sql = "select empno,empname,Position, date_format(MAX(TIME),'%Y-%m-%d %T') AS time  from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1 + "' and time <= '" + time2 + "' and Position like 'NA%' group by empno,empname,Position,Time order by time desc;";
                DataTable tbposition;
                tbposition = mysql.getDataTable(sql);

                if (tbposition.Rows.Count > 0)
                {

                    for (int i = 0; i < tbposition.Rows.Count; i++)
                    {
                        string emp1 = tbposition.Rows[i][0].ToString().Trim();
                        string position1 = tbposition.Rows[i][2].ToString().Trim();
                        string time11 = tbposition.Rows[i][3].ToString().Trim();
                        var jsonString = PositonNear(emp1, position1, time11, selectaround);
                        list.AddRange((List<UserCanten>)jsonString.Data);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //list.Sort((x, y) => x.time.CompareTo(y.time));
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PositonNear(string emp,string position,string time,int selectaround)
        {
            List<UserCanten> luc = new List<UserCanten>();
            List<Positonx> list = new List<Positonx>();
            try
            {
                list = helper.getlistPos(position);
                if(list.Count > 0)
                {
                    for(int i =0;i < list.Count; i++)
                    {
                        if(list[i].Name_Positon != "N/A")
                        {

                            if (list[i].Patient == "F0")
                            {
                                var jsonString = GetInforF0(list[i].Name_Positon, time, selectaround, list[i].Patient, emp);
                                luc.AddRange((List<UserCanten>)jsonString.Data);
                            }
                            else
                            {
                                //int row = luc.Count-1;
                                //string timer = luc[row].time.ToString();
                                var jsonString = GetInfor(list[i].Name_Positon, time, selectaround, list[i].Patient);
                                luc.AddRange((List<UserCanten>)jsonString.Data);
                            }
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(luc, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetInforF0(string position, string time ,int selectaround,string Patient,string emp)
        {
            MysqlHelper mysql = new MysqlHelper();
            List<UserCanten> listuc = new List<UserCanten>();
            DataTable dt = new DataTable();

            string sql = "select EmpNo,EmpName,Position, date_format(TIME,'%Y-%m-%d %T') AS time FROM record_vn WHERE EmpNo='"+emp.ToUpper().Trim()+"' and POSITION LIKE 'NA-%' and Time between DATE_ADD('" + time+"',INTERVAL - "+ selectaround + " MINUTE) and DATE_ADD('"+time+"',INTERVAL "+ selectaround + " MINUTE) and Position = '"+position+"' order by time desc";
            dt = mysql.getDataTable(string.Format(sql));
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserCanten ct = new UserCanten();
                    ct.empno = dt.Rows[i][0].ToString();
                    ct.empname = dt.Rows[i][1].ToString();
                    ct.position = dt.Rows[i][2].ToString();
                    ct.time = dt.Rows[i][3].ToString();
                    ct.ff = Patient;

                    listuc.Add(ct);
                }
            }

            return Json(listuc, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInfor(string position, string time, int selectaround, string Patient)
        {
            MysqlHelper mysql = new MysqlHelper();
            List<UserCanten> listuc = new List<UserCanten>();
            DataTable dt = new DataTable();

            string sql = "select EmpNo,EmpName,Position, date_format(TIME,'%Y-%m-%d %T') AS time FROM record_vn WHERE POSITION LIKE 'NA-%' and Time between DATE_ADD('" + time + "',INTERVAL - " + selectaround + " MINUTE) and DATE_ADD('" + time + "',INTERVAL " + selectaround + " MINUTE) and Position = '" + position + "' order by time desc";
            dt = mysql.getDataTable(string.Format(sql));
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserCanten ct = new UserCanten();
                    ct.empno = dt.Rows[i][0].ToString();
                    ct.empname = dt.Rows[i][1].ToString();
                    ct.position = dt.Rows[i][2].ToString();
                    ct.time = dt.Rows[i][3].ToString();
                    ct.ff = Patient;

                    listuc.Add(ct);
                }
            }

            return Json(listuc, JsonRequestBehavior.AllowGet);
        }
        //get dư lieu theo thoi gian a va b cua F0,f1,f2,f3


        public JsonResult getHistory(string emp, DateTime timestart, DateTime timeend, int select)
        {
            List<UserCanten> listUC = new List<UserCanten>();
            try
            {
                string time1 = timestart.ToString("yyyy-MM-dd HH:mm:ss");
                string time2 = timeend.ToString("yyyy-MM-dd HH:mm:ss");

                string queryok = null;
                string queryerr = null;

                List<UserCanten> listOK = new List<UserCanten>();
                List<UserCanten> listERR = new List<UserCanten>();
                //canteen
                if (select == 0)
                {
                    queryok = "select  ScanEmpNo,ScanEmpName,Position,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time  from VN_Record.record_vn where EmpNo='" + emp.Trim() + "'and Position like 'NA%' and time between '" + time1 + "' and '" + time2 + "'";
                    queryerr = "select  ScanEmpNo,ScanEmpName,Position,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time,Message  from VN_Record.record_vn_error where EmpNo = '" + emp.Trim() + "' and Position like 'NA%' and time between '" + time1 + "' and '" + time2 + "' ";
                }
                //car
                if (select == 1)
                {
                    queryok = "select  ScanEmpNo,ScanEmpName,Position,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time  from VN_Record.record_vn where EmpNo='" + emp.Trim() + "'and Position like 'BS%' and time between '" + time1 + "' and '" + time2 + "'";
                    queryerr = "select  ScanEmpNo,ScanEmpName,Position,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time,Message  from VN_Record.record_vn_error where EmpNo = '" + emp.Trim() + "' and Position like 'BS%' and time between '" + time1 + "' and '" + time2 + "' ";
                }
                //dorm
                if (select == 2)
                {
                    queryok = "select  ScanEmpNo,ScanEmpName,Position,date_format(Time,'%Y-%m-%d %T') AS time  from VN_Record.record_vn where EmpNo='" + emp.Trim() + "' and (Position like 'GW%' or Position like 'HT%' or Position like 'GZ%') and time between '" + time1 + "' and '" + time2 + "'";
                    queryerr = "select  ScanEmpNo,ScanEmpName,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time,Message  from VN_Record.record_vn_error where EmpNo = '" + emp.Trim() + "' and (Position like 'GW%' or Position like 'HT%' or Position like 'GZ%') and time between '" + time1 + "' and '" + time2 + "' ";
                }

                if (queryok != null)
                {
                    listOK = mysql.getList(queryok);
                    listUC.AddRange(listOK);
                }

                if (queryerr != null)
                {
                    listERR = mysql.getListErr(queryerr);
                    listUC.AddRange(listERR);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            listUC.Sort((x, y) => x.time.CompareTo(y.time));

            return Json(listUC, JsonRequestBehavior.AllowGet);

        }

        public JsonResult PositonF0(string postion)
        {
            List<Positonx> list = new List<Positonx>();
            try
            {
                list = helper.getlistPos(postion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }

    }
}