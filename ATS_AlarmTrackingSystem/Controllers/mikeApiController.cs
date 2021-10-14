using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using ATS_AlarmTrackingSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ATS_AlarmTrackingSystem.Controllers
{
    public class mikeApiController : ApiController
    {
        string connection_str = "Data Source=10.224.81.131,3000;Initial Catalog=OWNCLOUD_DATA;Uid=bn3;Pwd=Foxconn2021@!";
        MysqlHelper mysql = new MysqlHelper();

        #region Api Mike
        [System.Web.Http.Route("api/Covid_MapOverView")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Covid_MapOverView(DateTime dateSearch, string QRinput = "", string emp = "", int EatTime = 1)
        {
            if (dateSearch == null)
                dateSearch = DateTime.Now;
            DateTime time1, time2;
            string time = dateSearch.ToString("yyyy-MM-dd");
            switch (EatTime)
            {
                case 1:// An Sang
                    time1 = Convert.ToDateTime(time + " 06:00");
                    time2 = Convert.ToDateTime(time + " 09:00");
                    break;
                case 2:// An Trua
                    time1 = Convert.ToDateTime(time + " 10:00");
                    time2 = Convert.ToDateTime(time + " 13:30");
                    break;
                case 3:// An toi
                    time1 = Convert.ToDateTime(time + " 16:20");
                    time2 = Convert.ToDateTime(time + " 21:00");
                    break;
                case 4:// An Dem
                    time1 = Convert.ToDateTime(time + " 23:00");
                    time2 = Convert.ToDateTime(time + " 02:00");
                    break;
                default:
                    time1 = Convert.ToDateTime(time + " 06:00");
                    time2 = Convert.ToDateTime(time + " 09:00");
                    break;
            }
            string sql = "select empno,empname,Position from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' and Position like 'NA%' limit 1";

            DataTable tbposition;
            tbposition = mysql.getDataTable(sql);
            string QRCode_Postion = "";
            if (tbposition.Rows.Count > 0)
            {
                QRCode_Postion = tbposition.Rows[0]["Position"].ToString();
            }
            else
                return Json("");
            try
            {
                DBConnection db = new DBConnection();

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection_str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stGetLayoutByPosition", conn);
                    cmd.Parameters.AddWithValue("@action", "Layout");
                    cmd.Parameters.AddWithValue("@QRCode_str", string.IsNullOrEmpty(QRinput) ? QRCode_Postion : QRinput);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    rd.Close();
                    conn.Close();
                    conn.Dispose();
                }
                string json = JsonConvert.SerializeObject(dt);
                var unserialize = JsonConvert.DeserializeObject(json);
                return Json(unserialize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/Finding_patient")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Finding_patient(DateTime dateSearch, string emp = "", string QRinput = "", int EatTime = 1)// Finding F1 2 3 
        {
            if (dateSearch == null)
                dateSearch = DateTime.Now;
            DateTime time1, time2;
            string time = dateSearch.ToString("yyyy-MM-dd");
            switch (EatTime)
            {
                case 1:// An Sang
                    time1 = Convert.ToDateTime(time + " 06:00");
                    time2 = Convert.ToDateTime(time + " 09:00");
                    break;
                case 2:// An Trua
                    time1 = Convert.ToDateTime(time + " 10:00");
                    time2 = Convert.ToDateTime(time + " 13:30");
                    break;
                case 3:// An toi
                    time1 = Convert.ToDateTime(time + " 16:20");
                    time2 = Convert.ToDateTime(time + " 21:00");
                    break;
                case 4:// An Dem
                    time1 = Convert.ToDateTime(time + " 23:00");
                    time2 = Convert.ToDateTime(time + " 02:00");
                    break;
                default:
                    time1 = Convert.ToDateTime(time + " 06:00");
                    time2 = Convert.ToDateTime(time + " 09:00");
                    break;
            }
            string sql = "select Position  from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' and Position like 'NA%' limit 1;";

            DataTable tbposition;
            tbposition = mysql.getDataTable(sql);
            string QRCode_Postion = "";
            if (tbposition.Rows.Count > 0)
            {
                QRCode_Postion = tbposition.Rows[0]["Position"].ToString();
            }
            try
            {
                DBConnection db = new DBConnection();

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection_str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stGetF1_2_3_FromF0", conn);
                    QRinput = string.IsNullOrEmpty(QRinput) ? QRCode_Postion : QRinput;
                    if (QRinput != "")
                        cmd.Parameters.AddWithValue("@F0_Name_Position", QRinput);
                    else if (emp != "")
                        cmd.Parameters.AddWithValue("@EMP_NO", emp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    rd.Close();
                    conn.Close();
                    conn.Dispose();
                }

                List<Position_QR_Code> Position_by_QRs = new List<Position_QR_Code>();// Get List Positions
                foreach (DataRow row in dt.Rows)
                {
                    Position_QR_Code sp = new Position_QR_Code();
                    sp.Name_Position = row["Name_Position"].ToString();
                    sp.Position_intX = int.Parse(row["Position_intX"].ToString());
                    sp.Position_intY = int.Parse(row["Position_intY"].ToString());
                    sp.Patient = row["Patient"].ToString();
                    Position_by_QRs.Add(sp);
                }
                var QR_F0 = Position_by_QRs.Where(m => m.Patient == "F0").FirstOrDefault();

                // Filter From outside to inside
                for (int i = Position_by_QRs.Where(m => m.Patient == "F2" || m.Patient == "F3").ToList().Count - 1; i > -1; i--)
                {
                    var currentQR = Position_by_QRs.ElementAt(i);
                    var RangeFromCurrentQRx = QR_F0.Position_intX - currentQR.Position_intX;// Current Position with Center
                    var RangeFromCurrentQRy = QR_F0.Position_intY - currentQR.Position_intY;// Current Position with Center

                    var obj = Position_by_QRs.Where(m => m.Position_intX == RangeFromCurrentQRx * -1 + 1 && m.Position_intY == RangeFromCurrentQRy * -1 + 1).FirstOrDefault();
                    if (obj != null && obj.Name_Position == "N/A")
                    {
                        if (Position_by_QRs.ElementAt(i).Patient == "F2")
                            Position_by_QRs.ElementAt(i).Patient = "F1";
                        else
                            Position_by_QRs.ElementAt(i).Patient = "F2";
                    }
                }


                return Json(Position_by_QRs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/ListUpPatient")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Finding_patient_displayAsList(DateTime dateSearch, string emp = "", string QRinput = "", int aroundTime = 15, int EatTime = 1)// Finding F1 2 3 
        {
            try
            {
                if (dateSearch == null)
                    dateSearch = DateTime.Now;
                DateTime time1, time2;
                string time = dateSearch.ToString("yyyy-MM-dd");
                switch (EatTime)
                {
                    case 1:// An Sang
                        time1 = Convert.ToDateTime(time + " 06:00");
                        time2 = Convert.ToDateTime(time + " 09:00");
                        break;
                    case 2:// An Trua
                        time1 = Convert.ToDateTime(time + " 10:00");
                        time2 = Convert.ToDateTime(time + " 13:30");
                        break;
                    case 3:// An toi
                        time1 = Convert.ToDateTime(time + " 16:20");
                        time2 = Convert.ToDateTime(time + " 21:00");
                        break;
                    case 4:// An Dem
                        time1 = Convert.ToDateTime(time + " 23:00");
                        time2 = Convert.ToDateTime(time + " 02:00");
                        break;
                    default:
                        time1 = Convert.ToDateTime(time + " 06:00");
                        time2 = Convert.ToDateTime(time + " 09:00");
                        break;
                }
                string sql = "select Position, Time  from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' and Position like 'NA%' limit 1;";

                DataTable tbposition;
                tbposition = mysql.getDataTable(sql);
                string QRCode_Postion = "";
                if (tbposition.Rows.Count > 0)
                {
                    QRCode_Postion = tbposition.Rows[0]["Position"].ToString();

                    DateTime timeOrg = Convert.ToDateTime(tbposition.Rows[0]["Time"].ToString());
                    DateTime timeTo = timeOrg.AddMinutes(aroundTime);
                    timeOrg = timeOrg.AddMinutes(-1 * aroundTime);
                    time1 = timeOrg;
                    time2 = timeTo;
                }

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection_str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stGetF1_2_3_FromF0", conn);
                    if (QRCode_Postion != "")
                        cmd.Parameters.AddWithValue("@F0_Name_Position", QRCode_Postion);
                    else if (emp != "")
                        cmd.Parameters.AddWithValue("@EMP_NO", emp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    rd.Close();
                    conn.Close();
                    conn.Dispose();
                }
                Queue<string> F1 = new Queue<string>();
                Queue<string> F2 = new Queue<string>();
                Queue<string> F3 = new Queue<string>();
                string list_position1 = "";
                string list_position2 = "";
                string list_position3 = "";
                foreach (DataRow row in dt.Rows)
                {

                    switch (row["Patient"].ToString())
                    {
                        case "F1":
                            F1.Enqueue(row["Name_Position"].ToString());
                            list_position1 += "'" + row["Name_Position"].ToString() + "',";
                            break;
                        case "F2":
                            F2.Enqueue(row["Name_Position"].ToString());
                            list_position2 += "'" + row["Name_Position"].ToString() + "',";
                            break;
                        case "F3":
                            F3.Enqueue(row["Name_Position"].ToString());
                            list_position3 += "'" + row["Name_Position"].ToString() + "',";
                            break;
                        default:
                            break;
                    }
                }

                list_position1 = list_position1.Substring(0, list_position1.Length - 1);
                list_position2 = list_position2.Substring(0, list_position2.Length - 1);
                list_position3 = list_position3.Substring(0, list_position3.Length - 1);


                string mysql_query = "select empno,empname,Position, 'F1' as Patient from VN_Record.record_vn where Position in (" + list_position1 + ") " + " and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "'  group by empno, empname, Position union \n";// Get List Emp F1, F2, F3
                mysql_query += "select empno,empname,Position, 'F2' as Patient from VN_Record.record_vn where Position in (" + list_position2 + ") " + " and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' group by empno, empname, Position union \n";// Get List Emp F1, F2, F3
                mysql_query += "select empno,empname,Position, 'F3' as Patient from VN_Record.record_vn where Position in (" + list_position3 + ")  " + " and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' group by empno, empname, Position";// Get List Emp F1, F2, F3
                tbposition = mysql.getDataTable(mysql_query);


                string json = JsonConvert.SerializeObject(tbposition);
                var unserialize = JsonConvert.DeserializeObject(json);
                return Json(unserialize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [System.Web.Http.Route("api/GetRangeLayout")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetRangeLayout(string postition, int minX, int maxX, int minY, int maxY)
        {
            try
            {
                DBConnection db = new DBConnection();

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection_str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stGetRangeLayout", conn);
                    cmd.Parameters.AddWithValue("@Name_Position", postition);
                    cmd.Parameters.AddWithValue("@minX", minX);
                    cmd.Parameters.AddWithValue("@maxX", maxX);
                    cmd.Parameters.AddWithValue("@minY", minY);
                    cmd.Parameters.AddWithValue("@maxY", maxY);

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    rd.Close();
                    conn.Close();
                    conn.Dispose();
                }
                string json = JsonConvert.SerializeObject(dt);
                var unserialize = JsonConvert.DeserializeObject(json);
                return Json(unserialize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/TreePatient")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Finding_TreePatient(DateTime dateSearch, string emp = "", string QRinput = "", int aroundTime = 15, int EatTime = 1)// Finding F1 2 3 
        {
            try
            {
                if (dateSearch == null)
                    dateSearch = DateTime.Now;
                DateTime time1, time2;
                string time = dateSearch.ToString("yyyy-MM-dd");
                switch (EatTime)
                {
                    case 1:// An Sang
                        time1 = Convert.ToDateTime(time + " 06:00");
                        time2 = Convert.ToDateTime(time + " 09:00");
                        break;
                    case 2:// An Trua
                        time1 = Convert.ToDateTime(time + " 10:00");
                        time2 = Convert.ToDateTime(time + " 13:30");
                        break;
                    case 3:// An toi
                        time1 = Convert.ToDateTime(time + " 16:20");
                        time2 = Convert.ToDateTime(time + " 21:00");
                        break;
                    case 4:// An Dem
                        time1 = Convert.ToDateTime(time + " 23:00");
                        time2 = Convert.ToDateTime(time + " 02:00");
                        break;
                    default:
                        time1 = Convert.ToDateTime(time + " 06:00");
                        time2 = Convert.ToDateTime(time + " 09:00");
                        break;
                }
                string sql = "select EmpNo,empname, Position, Time  from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' and Position like 'NA%' limit 1;";
                EmpInfor f0 = new EmpInfor();
                f0.EmpNo = emp;
                f0.F = "F0";
                DataTable tbposition;
                tbposition = mysql.getDataTable(sql);
                string QRCode_Postion = "";
                if (tbposition.Rows.Count > 0)
                {
                    QRCode_Postion = tbposition.Rows[0]["Position"].ToString();
                    emp = tbposition.Rows[0]["EmpNo"].ToString();
                    DateTime timeOrg = Convert.ToDateTime(tbposition.Rows[0]["Time"].ToString());
                    DateTime timeTo = timeOrg.AddMinutes(aroundTime);
                    timeOrg = timeOrg.AddMinutes(-aroundTime);
                    time1 = timeOrg;
                    time2 = timeTo;
                    f0.EmpName = tbposition.Rows[0]["empname"].ToString();
                    f0.Position = QRCode_Postion;
                    f0.Time = tbposition.Rows[0]["Time"].ToString();
                }

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection_str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stGetF1_2_3_FromF0", conn);
                    if (QRCode_Postion != "")
                        cmd.Parameters.AddWithValue("@F0_Name_Position", QRCode_Postion);
                    else if (emp != "")
                        cmd.Parameters.AddWithValue("@EMP_NO", emp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    rd.Close();
                    conn.Close();
                    conn.Dispose();
                }
                Queue<Position_QR_Code> F1 = new Queue<Position_QR_Code>();
                Queue<Position_QR_Code> F2 = new Queue<Position_QR_Code>();
                Queue<Position_QR_Code> F3 = new Queue<Position_QR_Code>();

                string list_position1 = "";
                string list_position2 = "";
                string list_position3 = "";
                foreach (DataRow row in dt.Rows)
                {
                    Position_QR_Code sp = new Position_QR_Code();
                    sp.Name_Position = row["Name_Position"].ToString();
                    sp.Position_intX = int.Parse(row["Position_intX"].ToString());
                    sp.Position_intY = int.Parse(row["Position_intY"].ToString());
                    switch (row["Patient"].ToString())
                    {
                        case "F1":
                            F1.Enqueue(sp);
                            list_position1 += "'" + row["Name_Position"].ToString() + "',";
                            break;
                        case "F2":
                            F2.Enqueue(sp);
                            list_position2 += "'" + row["Name_Position"].ToString() + "',";
                            break;
                        case "F3":
                            F3.Enqueue(sp);
                            list_position3 += "'" + row["Name_Position"].ToString() + "',";
                            break;
                        default:
                            break;
                    }

                }

                list_position1 = list_position1.Substring(0, list_position1.Length - 1);
                list_position2 = list_position2.Substring(0, list_position2.Length - 1);
                list_position3 = list_position3.Substring(0, list_position3.Length - 1);


                string mysql_query = "select empno,empname,Position,time, 'F1' as Patient from VN_Record.record_vn where Position in (" + list_position1 + ") " + " and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "'  group by empno, empname, Position,time union \n";// Get List Emp F1, F2, F3
                mysql_query += "select empno,empname,Position,time, 'F2' as Patient from VN_Record.record_vn where Position in (" + list_position2 + ") " + " and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' group by empno, empname, Position,time union \n";// Get List Emp F1, F2, F3
                mysql_query += "select empno,empname,Position,time, 'F3' as Patient from VN_Record.record_vn where Position in (" + list_position3 + ")  " + " and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' group by empno, empname, Position,time";// Get List Emp F1, F2, F3
                tbposition = mysql.getDataTable(mysql_query);
                List<EmpInfor> Emps_by_F = new List<EmpInfor>();
                List<EmpInfor> Emps_by_F1 = new List<EmpInfor>();
                List<EmpInfor> Emps_by_F2 = new List<EmpInfor>();

                Emps_by_F.Add(f0);

                foreach (DataRow row in tbposition.Rows)
                {
                    EmpInfor sp = new EmpInfor();
                    sp.EmpNo = row["empno"].ToString();
                    sp.EmpName = row["empname"].ToString();
                    sp.F = row["Patient"].ToString();
                    sp.Position = row["Position"].ToString();
                    sp.Time = row["time"].ToString();
                    Emps_by_F.Add(sp);
                    if (sp.F == "F1")
                    {
                        Emps_by_F1.Add(sp);
                    }
                    else if (sp.F == "F2")
                    {
                        Emps_by_F2.Add(sp);
                    }
                }

                while (F1.Count + F2.Count + F3.Count > 0)// set position for F1 F2 F3
                {
                    if (F1.Count > 0)
                    {
                        var obj = F1.Dequeue();
                        foreach (var item in Emps_by_F)
                        {
                            if (item.Position == obj.Name_Position)
                            {
                                item.x = obj.Position_intX;
                                item.y = obj.Position_intY;
                            }
                        }

                    }
                    else if (F2.Count > 0)
                    {
                        var obj = F2.Dequeue();
                        foreach (var item in Emps_by_F)
                        {
                            if (item.Position == obj.Name_Position)
                            {
                                item.x = obj.Position_intX;
                                item.y = obj.Position_intY;
                            }
                        }
                    }
                    else if (F3.Count > 0)
                    {
                        var obj = F3.Dequeue();
                        foreach (var item in Emps_by_F)
                        {
                            if (item.Position == obj.Name_Position)
                            {
                                item.x = obj.Position_intX;
                                item.y = obj.Position_intY;
                            }
                        }
                    }
                }

                foreach (var item in Emps_by_F)// this function Loop to set the parent for build the tree
                {
                    if (item.F == "F1")// F1 only have parent is F0
                    {
                        item.Parent = emp;
                    }
                    else if (item.F == "F2")// F2 can have F1 is parent
                    {
                        foreach (var item1 in Emps_by_F1)
                        {
                            if (item1.F == "F1" && (Math.Abs(item.x - item1.x) < 2) && Math.Abs(item.y - item1.y) < 2)// Range from P2P is less than 1
                            {
                                item.Parent = item1.EmpNo;
                            }
                        }
                    }
                    else if (item.F == "F3")// F3 parent is F2
                    {
                        foreach (var item1 in Emps_by_F2)
                        {
                            if (item1.F == "F2" && (Math.Abs(item.x - item1.x) < 2) && Math.Abs(item.y - item1.y) < 2)
                            {
                                item.Parent = item1.EmpNo;
                            }
                        }
                    }

                }
                foreach (var item in Emps_by_F)// Loop find F without 
                {
                    if (item.Parent == "")
                    {
                        foreach (var item1 in Emps_by_F2)
                        {
                            if (item1.F == "F2" && (Math.Abs(item.x - item1.x) < 3) && Math.Abs(item.y - item1.y) < 3)
                            {
                                item.Parent = item1.EmpNo;
                            }
                        }
                    }
                }

                #region Sample

                //EmpInfor sp1 = new EmpInfor();
                //EmpInfor sp1 = new EmpInfor();
                //sp1.EmpNo ="V123456";
                //sp1.Parent = "";
                //sp1.F = "F0";
                //EmpInfor sp2 = new EmpInfor();
                //sp2.EmpNo ="V123457";
                //sp2.Parent = "V123456";
                //sp2.F = "F1";
                //EmpInfor sp3 = new EmpInfor();
                //sp3.EmpNo ="V123458";
                //sp3.Parent = "V123456";
                //sp3.F = "F1";
                //EmpInfor sp4 = new EmpInfor();
                //sp4.EmpNo ="V123459";
                //sp4.Parent = "V123456";
                //sp4.F = "F1";
                //EmpInfor sp5 = new EmpInfor();
                //sp5.EmpNo ="V123410";
                //sp5.Parent = "V123459";
                //sp5.F = "F2";
                //EmpInfor sp6 = new EmpInfor();
                //sp6.EmpNo ="V123411";
                //sp6.Parent = "V123459";
                //sp6.F = "F2";
                //EmpInfor sp7 = new EmpInfor();
                //sp7.EmpNo ="V123412";
                //sp7.Parent = "V123458";
                //sp7.F = "F3";
                //EmpInfor sp8 = new EmpInfor();
                //sp8.EmpNo ="V123411";
                //sp8.Parent = "";
                //sp8.F = "F3";
                //EmpInfor sp9 = new EmpInfor();
                //sp9.EmpNo ="V123411";
                //sp9.Parent = "";
                //sp9.F = "F3";
                //EmpInfor sp10 = new EmpInfor();
                //sp10.EmpNo ="V123411";
                //sp10.Parent = "";
                //sp10.F = "F3";
                //List<EmpInfor> emp_test = new List<EmpInfor>();
                //emp_test.Add(sp1);
                //emp_test.Add(sp2);
                //emp_test.Add(sp3);
                //emp_test.Add(sp4);
                //emp_test.Add(sp5);
                //emp_test.Add(sp6);
                //emp_test.Add(sp7);
                //emp_test.Add(sp8);
                //emp_test.Add(sp9);
                //emp_test.Add(sp10);
                #endregion


                //string json = JsonConvert.SerializeObject(tbposition);
                //var unserialize = JsonConvert.DeserializeObject(json);
                //var unserialize = JsonConvert.SerializeObject<EmpInfor>(emp_test);
                return Json(Emps_by_F);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/GetListScanQRCode")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetListScanQRCode(DateTime dateSearch, string emp = "", string QRinput = "", int EatTime = 1)
        {
            try
            {
                if (dateSearch == null)
                    dateSearch = DateTime.Now;
                DateTime time1, time2;
                string time = dateSearch.ToString("yyyy-MM-dd");
                switch (EatTime)
                {
                    case 1:// An Sang
                        time1 = Convert.ToDateTime(time + " 06:00");
                        time2 = Convert.ToDateTime(time + " 09:00");
                        break;
                    case 2:// An Trua
                        time1 = Convert.ToDateTime(time + " 10:00");
                        time2 = Convert.ToDateTime(time + " 13:30");
                        break;
                    case 3:// An toi
                        time1 = Convert.ToDateTime(time + " 16:20");
                        time2 = Convert.ToDateTime(time + " 21:00");
                        break;
                    case 4:// An Dem
                        time1 = Convert.ToDateTime(time + " 23:00");
                        time2 = Convert.ToDateTime(time + " 02:00");
                        break;
                    default:
                        time1 = Convert.ToDateTime(time + " 06:00");
                        time2 = Convert.ToDateTime(time + " 09:00");
                        break;
                }
                string sql = "select EmpNo, empname, Position, Time  from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' and Position like 'NA%' limit 1;";
                EmpInfor f0 = new EmpInfor();
                f0.EmpNo = emp;
                f0.F = "F0";
                DataTable tbposition;
                tbposition = mysql.getDataTable(sql);

                string json = JsonConvert.SerializeObject(tbposition);
                var unserialize = JsonConvert.DeserializeObject(json);
                return Json(unserialize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Minh

        [System.Web.Http.Route("api/getPosF0")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getPosF0(DateTime dateSearch, string emp = "", int EatTime = 1)
        {
            if (dateSearch == null)
                dateSearch = DateTime.Now;
            DateTime time1, time2;
            string time = dateSearch.ToString("yyyy-MM-dd");
            switch (EatTime)
            {
                case 1:// An Sang
                    time1 = Convert.ToDateTime(time + " 06:00");
                    time2 = Convert.ToDateTime(time + " 09:00");
                    break;
                case 2:// An Trua
                    time1 = Convert.ToDateTime(time + " 10:00");
                    time2 = Convert.ToDateTime(time + " 13:30");
                    break;
                case 3:// An toi
                    time1 = Convert.ToDateTime(time + " 16:20");
                    time2 = Convert.ToDateTime(time + " 21:00");
                    break;
                case 4:// An Dem
                    time1 = Convert.ToDateTime(time + " 23:00");
                    time2 = Convert.ToDateTime(time + " 02:00");
                    break;
                default:
                    time1 = Convert.ToDateTime(time + " 06:00");
                    time2 = Convert.ToDateTime(time + " 09:00");
                    break;
            }

            string sql = "select empno,empname,Position,date_format(TIME,'%Y-%m-%d %T') AS time from VN_Record.record_vn where  EmpNo ='" + emp.ToUpper().Trim() + "' and  time >= '" + time1.ToString("yyyy-MM-dd HH:mm:ss") + "' and time <= '" + time2.ToString("yyyy-MM-dd HH:mm:ss") + "' and Position like 'NA%' limit 1";
            string position = null;
            string empname = null;

            DataTable tbposition;
            tbposition = mysql.getDataTable(sql);
            if (tbposition.Rows.Count > 0)
            {
                position = tbposition.Rows[0]["Position"].ToString();
                empname = tbposition.Rows[0]["empname"].ToString();
                time = tbposition.Rows[0]["time"].ToString();
            }
            return Ok(new { position, empname, time });

        }

        [System.Web.Http.Route("api/getPosF0Day")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getPosF0Day(DateTime dateStart, string emp)
        {
            string date1 = dateStart.ToString("yyyy-MM-dd 00:00:00");
            string date2 = dateStart.ToString("yyyy-MM-dd 23:59:59");

            string sql = "select empno,empname,Position, date_format(MAX(TIME),'%Y-%m-%d %T') AS time  from VN_Record.record_vn where EmpNo = '" + emp.Trim().ToUpper() + "' and time >= '" + date1 + "' and time <= '" + date2 + "' and Position like 'NA%' group by empno,empname,Position,Time ";
            string position = null;
            string empname = null;
            string time = null;

            DataTable tbposition;
            tbposition = mysql.getDataTable(sql);
            if (tbposition.Rows.Count > 0)
            {
                position = tbposition.Rows[0]["Position"].ToString();
                empname = tbposition.Rows[0]["empname"].ToString();

                for (int i = 0; i < tbposition.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        time += tbposition.Rows[0]["time"].ToString();
                    }
                    else
                    {
                        time = time + "," + tbposition.Rows[i]["time"].ToString();
                    }
                }
                return Ok(new { position, empname, time });
            }
            else
            {
                return null;
            }


        }

        [System.Web.Http.Route("api/getPosName")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getPosName(string timef0, string pos, string Patient)
        {

            //string time = timef0.ToString("yyyy-MM-dd HH:mm:ss");
            string[] arrtime = timef0.Split(',');
            string[] position = pos.Split(',');

            MysqlHelper mysql = new MysqlHelper();
            List<UserCanten> listuc = new List<UserCanten>();
            DataTable dt = new DataTable();

            if (arrtime.Length > 0)
            {
                for (int u = 0; u < arrtime.Length; u++)
                {
                    if (arrtime[u] != ",")
                    {
                        string time = arrtime[u];
                        if (position.Length > 0)
                        {
                            for (int j = 0; j < position.Length; j++)
                            {
                                if (position[j].Trim() != "")
                                {
                                    string sql = "select EmpNo,EmpName,Position, date_format(TIME,'%Y-%m-%d %T') AS time FROM record_vn WHERE POSITION LIKE 'NA-%' and Time between DATE_ADD('" + time + "',INTERVAL - 15 MINUTE) and DATE_ADD('" + time + "',INTERVAL 15 MINUTE) and Position = '" + position[j].Trim() + "' order by time desc";
                                    dt = mysql.getDataTable(string.Format(sql));
                                    if (dt != null && dt.Rows.Count > 0)
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
                                }
                            }
                        }
                    }
                }
            }

            return Json(listuc);

        }


        //lay so nhuong nguoi trong phong ki tuc xa
        [System.Web.Http.Route("api/getDormRoom")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getDormRoom(string emp)
        {
            try
            {
                string getpost = "SELECT * FROM VN_Record.dorminfo_vn where DeleteTime is null and EmpNo=N'"+emp+"'";
                DataTable tbpos = new DataTable();
                tbpos = mysql.getDataTable(getpost);
                string pos = tbpos.Rows[0]["RoomCode"].ToString();
                if (!string.IsNullOrEmpty(pos))
                {
                    //HT-C02-319-1
                    string[] arr = pos.Split('-');
                    string site = arr[0];
                    string building = arr[1];
                    string room = arr[2];
                    string poslike = site + "-" + building + "-" + room;
                    string sql = "select * FROM VN_Record.dorminfo_vn WHERE DeleteTime is null and RoomCode like '" + poslike.ToUpper() + "%' ";
                    DataTable tbposition;
                    tbposition = mysql.getDataTable(sql);

                    string json = JsonConvert.SerializeObject(tbposition);
                    var unserialize = JsonConvert.DeserializeObject(json);
                    return Json(unserialize);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //lay so nhuong nguoi trong phong ki tuc xa

        //lay all data vi tri quet the cong
        [System.Web.Http.Route("api/ListScanPort")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult ListScanPort()
        {
            DBOhelper dbo = new DBOhelper();
            try
            {
                string sql = "SELECT TOP 200 * FROM ViTriQuetThe order by NgayTaiLen desc";

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
        
        //lay all data vi tri quet the cong

        //get limit tin nhan Zalo
        [System.Web.Http.Route("api/getLimitMessage")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getLimitMessage()
        {
            Chat chat = new Chat();
            try
            {
                string sql = "SELECT ID_MESSAGE,GROUP_NAME,CONTENTS,STATUS,ROW1,ROW2,ROW3,to_char(DATE_INPUT,'YYYY-mm-dd HH:MI:SS') as DateSend FROM (select * from  MES4.R_MESSAGEWECHAT where ROW1='ZALO'  order by DATE_SEND desc )WHERE ROWNUM < 15 order by DATE_SEND asc ";

                DataTable tbposition;
                tbposition = chat.getDataTable(sql);
                string json = JsonConvert.SerializeObject(tbposition);
                var unserialize = JsonConvert.DeserializeObject(json);
                return Json(unserialize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("apizalo/SendMessage")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult SendMessage([FromBody] DataMessage dtm)
        {
            try
            {
                Chat db = new Chat();
                string sql = "select * from MES4.R_PROGRAMWECHAT where KEY = '" + dtm.KEY.Trim() + "' and GROUP_NAME='" + dtm.GROUP_NAME.Trim() + "' and ROW1='ZALO'";
                if (string.IsNullOrEmpty(dtm.GROUP_NAME) || string.IsNullOrEmpty(dtm.CONTENTS) || string.IsNullOrEmpty(dtm.KEY))
                {
                    return BadRequest("Erro: Empty");
                }
                else
                {
                    if (db.CompareProgram(sql))
                    {
                        string removableChars = Regex.Escape(@"@&'()<>#");
                        string pattern = "[" + removableChars + "]";
                        string contents = Regex.Replace(dtm.CONTENTS, pattern, "");
                        contents = contents.Replace('\"', ' ');
                        string id_message = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Random r = new Random();
                        id_message += r.Next(00, 99);
                        string query;
                        if (!string.IsNullOrEmpty(dtm.REPLY))
                        {
                            query = "insert into MES4.R_MESSAGEWECHAT(ID_MESSAGE,GROUP_NAME,CONTENTS,STATUS,DATE_INPUT,ROW1,ROW2,ROW3) VALUES ('" + id_message + "', N'" + dtm.GROUP_NAME + "', N'" + contents + "', '0', sysdate,'ZALO','" + dtm.USERSEND + "','" + dtm.REPLY + "')";
                        }
                        else
                        {
                            query = "insert into MES4.R_MESSAGEWECHAT(ID_MESSAGE,GROUP_NAME,CONTENTS,STATUS,DATE_INPUT,ROW1,ROW2) VALUES ('" + id_message + "', N'" + dtm.GROUP_NAME + "', N'" + contents + "', '0', sysdate,'ZALO','" + dtm.USERSEND + "')";
                        }

                        if (db.ExcuteSQL(query) == true)
                        {
                            return Json("ID_MESSAGE:" + id_message);
                        }
                        else
                        {
                            return BadRequest("Can not ExecuteNonQuery");
                        }
                    }
                    else
                    {
                        return BadRequest("KEY OR GROUP_NAME ERRO");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //lay site VW_DETECT_LOCATION_DOW
        [System.Web.Http.Route("api/ListSite")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult ListSite()
        {
            DBOhelper dbo = new DBOhelper();
            try
            {
                string sql = "select distinct(SITES) from [VW_DETECT_LOCATION_DOW]";

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
        
        [System.Web.Http.Route("api/ListIsolation")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult ListIsolation()
        {
            DBOhelper dbo = new DBOhelper();
            try
            {
                string sql = "select * from [dbo].[ThongTinCL]";

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

        #endregion Minh


    }
    public class EmpInfor
    {
        public string EmpNo = "";
        public string EmpName = "";
        public string Position = "";
        public string Time = "";
        public string F = "";
        public string Parent = "";
        public int x = 0;
        public int y = 0;
    }
    public class Position_QR_Code
    {
        public string Name_Position = "";//QR
        public int Position_intX = 0;
        public int Position_intY = 0;
        public string Patient = "";
    }
}
