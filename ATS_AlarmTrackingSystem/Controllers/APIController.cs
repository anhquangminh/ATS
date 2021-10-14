using ATS_AlarmTrackingSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ATS_AlarmTrackingSystem.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class APIController : ApiController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// get owner by factory config by CRC
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/getOwnerDepart")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getOwnerDepart()
        {
            excuteCRC extCRC = new excuteCRC();
            return Json(extCRC.getOwnerDepart());
        }

        /// <summary>
        /// get infor message ID_ATs 
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/getId_ats")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getId_ats(string id_ats)
        {
            DataTable table = new DataTable();
            getAllMessage getdata = new getAllMessage();
            table = getdata.queryDatatable("SELECT * FROM ATS_Content where id_ats='" + id_ats.Trim() + "'");
            return Json(table);
        }

        /// <summary>
        /// get count group by owner 
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/getDataTable")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getDataTable()
        {
            DataTable table = new DataTable();
            getAllMessage getdata = new getAllMessage();
            table = getdata.queryDatatable("SELECT count(*) as count,owner_emp FROM Owner_Depart group by owner_emp");
            return Json(table);
        }

        /// <summary>
        /// get all message
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/getAllData")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getAllData()
        {
            getAllMessage getdata = new getAllMessage();
            //getdata.listATS("all");
            return Json(getdata.listATS("all"));
        }

        [System.Web.Http.Route("api/getIssuesByBuilding")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getIssuesByBuilding(string? building)
        {
            getAllMessage getdata = new getAllMessage();
            string sql = "SELECT * FROM ATS_Content";
            if (!string.IsNullOrEmpty(building))
            {
                sql = "SELECT * FROM ATS_Content where building=N'"+building+"'";
            }
            //getdata.listATSByBuilding(sql);
            return Json(getdata.listATSByBuilding(sql));
        }

        [System.Web.Http.Route("api/getOpen")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getOpen()
        {
            List<Quantity> listQ = new List<Quantity>();
            getAllMessage getdata = new getAllMessage();
            string sql = "select distinct building FROM ATS_Content order by building asc";
            DataTable tb = getdata.queryDatatable(sql);
            for(int i = 0; i < tb.Rows.Count; i++)
            {
                string building = tb.Rows[i]["building"].ToString();
                string sqlCount = "SELECT * FROM ATS_Content where building=N'" + building+"' and status ='OPEN'";
                DataTable dtCount = getdata.queryDatatable(sqlCount);
                string count = dtCount.Rows.Count.ToString();
                Quantity qt = new Quantity();
                qt.Name = building;
                qt.Number = count;
                listQ.Add(qt);

            }
            //getdata.listATSByBuilding(sql);
            return Json(listQ);
        }

        [System.Web.Http.Route("api/getOnGoing")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getOnGoing()
        {
            List<Quantity> listQ = new List<Quantity>();
            getAllMessage getdata = new getAllMessage();
            string sql = "select distinct building FROM ATS_Content order by building asc";
            DataTable tb = getdata.queryDatatable(sql);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                string building = tb.Rows[i]["building"].ToString();
                string sqlCount = "SELECT * FROM ATS_Content where building=N'" + building + "' and status ='ON_GOING'";
                DataTable dtCount = getdata.queryDatatable(sqlCount);
                string count = dtCount.Rows.Count.ToString();
                Quantity qt = new Quantity();
                qt.Name = building;
                qt.Number = count;
                listQ.Add(qt);

            }
            //getdata.listATSByBuilding(sql);
            return Json(listQ);
        }

        [System.Web.Http.Route("api/getClose")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getClose()
        {
            List<Quantity> listQ = new List<Quantity>();
            getAllMessage getdata = new getAllMessage();
            string sql = "select distinct building FROM ATS_Content order by building asc";
            DataTable tb = getdata.queryDatatable(sql);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                string building = tb.Rows[i]["building"].ToString();
                string sqlCount = "SELECT * FROM ATS_Content where building=N'" + building + "' and status ='CLOSE'";
                DataTable dtCount = getdata.queryDatatable(sqlCount);
                string count = dtCount.Rows.Count.ToString();
                Quantity qt = new Quantity();
                qt.Name = building;
                qt.Number = count;
                listQ.Add(qt);

            }  
            //getdata.listATSByBuilding(sql);
            return Json(listQ);
        }

        //date chart
        [System.Web.Http.Route("api/DataLineChart")]
        [System.Web.Http.HttpGet]
        
        public IHttpActionResult DataLineChart()
        {
            double count = 20, y = 10;

            Random random = new Random(DateTime.Now.Millisecond);

            List<DataPoint> dataPoints;

            dataPoints = new List<DataPoint>();

            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);

                dataPoints.Add(new DataPoint("V0991291" + i.ToString(), y));
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            string json = JsonConvert.SerializeObject(dataPoints);
            return Json(dataPoints);
        }
        
        //date chart

        /// <summary>
        /// POST json ip_address,team,emp_user,description send data message
        /// {
        ///     "ip_address": "10.224.41.71",
        ///     "building": "B04",
        ///     "team": "SFIS",
        ///     "emp_user": "V09912xx",
        ///     "description": "Lỗi chương trình sfis không xao được"
        /// }
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/sendData")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult sendData([FromBody] Message message)
        {
            try
            {
                if (!ModelState.IsValid || string.IsNullOrEmpty(message.ip_address))
                {
                    return BadRequest("Erro: Empty");
                }
                else
                {
                    WebServiceInfor.PostmanService serviceInfor = new WebServiceInfor.PostmanService();
                    string level_user = serviceInfor.GetEmpLevel(message.emp_user);
                    string year = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Random r = new Random();
                    string id_ats = year + r.Next(10, 99).ToString();
                    string time_start = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string query = "Insert into ATS_Content(id_ats,ip_address,building,team,emp_user,level_user,description,status ,time_start) values ";
                    query += "(N'" + id_ats + "',N'" + message.ip_address + "',N'" + message.building + "',N'" + message.team + "',N'" + message.emp_user + "',N'" + level_user + "',N'" + message.description + "','OPEN',getdate())";
                    querySQL queyrysql = new querySQL();
                    if (queyrysql.Execute(query) == true)
                    {
                        return Json<String>("Id_ats :" + id_ats);
                    }
                    else
                    {
                        return BadRequest("Can not ExecuteNonQuery");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// POST json send owner department
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/sendOwnerDepart")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult sendOwnerDepart([FromBody] OwnerDepart ownerdepart)
        {
            try
            {
                if (!ModelState.IsValid || string.IsNullOrEmpty(ownerdepart.emp))
                {
                    return BadRequest("Erro: Empty");
                }
                else
                {
                    WebServiceInfor.PostmanService serviceInfor = new WebServiceInfor.PostmanService();

                    DataTable dtjson = serviceInfor.GetEmpInfomation(ownerdepart.emp);
                    string namecn = dtjson.Rows[0]["USER_NAME"].ToString();
                    string time_start = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string query = "Insert into Owner_Depart(emp,name_cn,name_vn,building,team) values ";
                    query += "(N'" + ownerdepart.emp + "',N'" + namecn + "',N'" + ownerdepart.name_vn + "',N'" + ownerdepart.building + "',N'" + ownerdepart.team + "')";
                    querySQL queyrysql = new querySQL();
                    if (queyrysql.Execute(query) == true)
                    {
                        return Json("Sussces");
                    }
                    else
                    {
                        return BadRequest("Can not ExecuteNonQuery");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// POST json send data fedback
        /// {
        ///       "id_ats": "2021042308300149",
        ///       "star": "3"
        /// }
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        [System.Web.Http.Route("api/sendFedback")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult sendFedback([FromBody] Fedback fedback)
        {
            try
            {
                if (!ModelState.IsValid || string.IsNullOrEmpty(fedback.id_ats))
                {
                    return BadRequest("Erro: Empty");
                }
                else
                {
                    DataTable table = new DataTable();
                    getAllMessage getdata = new getAllMessage();
                    table = getdata.queryDatatable("SELECT * FROM ATS_Content where id_ats='" + fedback.id_ats + "'");

                    if (table.Rows.Count > 0)
                    {
                        string owner_emp = table.Rows[0]["owner_emp"].ToString();
                        if (!string.IsNullOrEmpty(owner_emp.Trim()))
                        {
                            string time_start = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            string query = "Insert into Fedback(id_ats,owner_emp,star) values ";
                            query += "(N'" + fedback.id_ats + "',N'" + owner_emp + "',N'" + fedback.star + "'";
                            querySQL queyrysql = new querySQL();
                            if (queyrysql.Execute(query) == true)
                            {
                                return Json("Sussces");
                            }
                            else
                            {
                                return BadRequest("Can not ExecuteNonQuery");
                            }
                        }
                        else
                        {
                            return BadRequest("Can not find owner_emp ");
                        }
                    }
                    else
                    {
                        return BadRequest("Can not find id_ats");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// update status by id_ats method get
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        ///<exception cref="System.OverflowException">Thrown when one parameter is max
        /// and the other is greater than zero.</exception>

        [System.Web.Http.Route("api/updateStatus")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult updateStatus(string id_ats, string status, string execute)
        {
            try
            {
                if (string.IsNullOrEmpty(id_ats) || string.IsNullOrEmpty(execute) || string.IsNullOrEmpty(status))
                {
                    return BadRequest("Erro: Empty");
                }
                else
                {
                    DataTable tb = new DataTable();
                    getAllMessage getdata = new getAllMessage();
                    tb = getdata.queryDatatable("SELECT * FROM ATS_Content where id_ats='" + id_ats.Trim() + "'");
                    string is_status = null;
                    string is_time_end = null;
                    string is_id_ats = null;
                    foreach (DataRow row in tb.Rows)
                    {
                        is_status = row["status"].ToString();
                        is_time_end = row["time_end"].ToString();
                        is_id_ats = row["id_ats"].ToString();
                    }
                    if (is_status != "OPEN" && id_ats != is_id_ats)
                    {
                        return BadRequest(" Status issue is Close ");
                    }

                    string time_end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string query;
                    switch (execute.ToLower())
                    {
                        case "update":
                            query = "update ATS_Content set status =N'" + status.Trim().ToUpper() + "',time_end ='" + time_end + "' where id_ats ='" + id_ats.Trim() + "'";
                            break;
                        case "delete":
                            query = "delete from ATS_Content where ip_address='" + id_ats.Trim() + "'";
                            break;
                        default:
                            query = null;
                            break;
                    }
                    if (query == null || query == "")
                    {
                        return BadRequest("Can not ExecuteNonQuery");
                    }
                    else
                    {
                        querySQL queyrysql = new querySQL();
                        if (queyrysql.Execute(query) == true)
                        {
                            return Json("Success");
                        }
                        else
                        {
                            return BadRequest("Can not ExecuteNonQuery");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/updateOwner")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult updateOwner(string id_ats, string owner_emp, string owner_name)
        {

            //Infor inforuser = new Infor();
            //string URL = "http://10.224.69.100/postman/api/hr/getEmpObj";
            //string urlParameters = "?id=" + emp;
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(URL);
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var customerJsonString = await response.Content.ReadAsStringAsync();


            //        if (customerJsonString.ToString() != "\"\"" && !string.IsNullOrEmpty(customerJsonString.ToString()))
            //        {
            //            Console.WriteLine("Your response data is: " + customerJsonString);
            //            inforuser = JsonConvert.DeserializeObject<Infor>(custome‌​rJsonString);
            //        }
            //    }

            try
            {
                if (string.IsNullOrEmpty(id_ats) || string.IsNullOrEmpty(owner_emp))
                {
                    return BadRequest("Erro: Empty");
                }
                else
                {
                    string time_going = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string query;
                    query = "update ATS_Content set owner_emp =N'" + owner_emp.Trim().ToUpper() + "', owner_name =N'" + owner_name.Trim() + "' ,status='ON_GOING' , time_going='"+ time_going + "' where id_ats ='" + id_ats.Trim() + "'";

                    if (query == null || query == "")
                    {
                        return BadRequest("Can not ExecuteNonQuery");
                    }
                    else
                    {
                        querySQL queyrysql = new querySQL();
                        if (queyrysql.Execute(query) == true)
                        {
                            return Json("Success");
                        }
                        else
                        {
                            return BadRequest("Can not ExecuteNonQuery");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/getF0")]
        [System.Web.Http.HttpGet]
        public JArray GetF0(string emp, string timeStart, string timeEnd)
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
            return JArray.FromObject(listPos);
        }

        [System.Web.Http.Route("api/getF1")]
        [System.Web.Http.HttpGet]
        public JArray GetF1(string position, string time)
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
            sql = "select EmpNo,EmpName,Position, date_format(TIME,'%Y-%m-%d %T') AS time FROM record_vn WHERE POSITION LIKE 'NA-%' and Time between DATE_ADD('{0}',INTERVAL - 15 MINUTE) and DATE_ADD('{0}',INTERVAL 15 MINUTE) and Position = '{1}' order by time desc; ";
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

            return JArray.FromObject(res);
        }

        [System.Web.Http.Route("api/getHistorySite")]
        [System.Web.Http.HttpGet]
        public JArray getHistorySite(DateTime timestart, DateTime timeend, string select, string site)
        {
            MysqlHelper mysql = new MysqlHelper();
            List<UserCanten> listUC = new List<UserCanten>();
            try
            {
                string siteselect;
                string queryok = null;
                string queryerr = null;
                string time1 = timestart.ToString("yyyy-MM-dd HH:mm:ss");
                string time2 = timeend.ToString("yyyy-MM-dd HH:mm:ss");
                if (select == "GW,HT,GZ")
                {
                    siteselect = "Position like '" + site + "%'";
                }
                else
                {
                    siteselect = "Position like '" + select + "-" + site + "%'";
                }

                queryok = "select  ScanEmpNo,ScanEmpName,Position,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time  from VN_Record.record_vn where " + siteselect + " and time between '" + time1 + "' and '" + time2 + "'";
                queryerr = "select  ScanEmpNo,ScanEmpName,Position,DATE_ADD(date_format(Time,'%Y-%m-%d %T'),INTERVAL - 1 HOUR) AS time,Message  from VN_Record.record_vn_error where " + siteselect + " and time between '" + time1 + "' and '" + time2 + "' ";

                List<UserCanten> listOK = new List<UserCanten>();
                List<UserCanten> listERR = new List<UserCanten>();

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
            return JArray.FromObject(listUC);
        }

        [System.Web.Http.Route("api/findMealEatdata")]
        [System.Web.Http.HttpGet]
        public JArray findMealEatdata(string emp)
        {

            MysqlHelper mysql = new MysqlHelper();
            string sqlQuey = @"SELECT *from VN_Record.dorminfo_vn where EmpNo='" + emp.ToUpper().Trim() + "'";
            List<MealEat> list = new List<MealEat>();
            MealEat model = new MealEat();
            model.IcivetBu = "";
            model.IcivetName = "";
            model.QRMoveVehicle = "";
            model.IcivetNo = emp;
            DataTable table = mysql.getDataTable(sqlQuey);
            if (table.Rows.Count > 0)
            {
                model.QRLive = table.Rows[0]["RoomCOde"].ToString();
            }
            else model.QRLive = "N/A";
            sqlQuey = @"SELECT *from VN_Record.empposition_vn WHERE ENABLE='Y' and EmpNo='" + emp.ToUpper().Trim() + "' ;";
            DataTable table1 = mysql.getDataTable(sqlQuey);
            if (table1.Rows.Count > 0) model.QRMove = table1.Rows[0]["position"].ToString();
            else model.QRMove = "N/A";

            sqlQuey = @"SELECT A.*,B.POSITIONCODE FROM(SELECT * FROM VN_Record.empnaposition_vn  WHERE isvalid='Y' AND EMPNO='" + emp.ToUpper().Trim() + "' ) A LEFT JOIN (SELECT POSITIONCODE,belongBU,areacode FROM VN_Record.napositioninfo_vn WHERE isvalid='Y' ) B ON A.AREACODE = B.areacode AND A.belongBU = B.belongBU; ";


            table = new DataTable();
            table = mysql.getDataTable(sqlQuey);
            List<string> listTime = new List<string>();
            DataTable tb2 = new DataTable();
            string queryRee = "";

            if (table.Rows.Count > 0)
            {
                if (table.Rows[0]["MealBatch1"].ToString() == "不管控")
                {
                    model.MealBatch1 = "早餐/ Bữa Sáng";
                    model.MealTime1 = "不管控";
                }
                else
                {
                    model.MealBatch1 = "早餐/ Bữa Sáng";
                    queryRee = " SELECT * FROM VN_Record.mealbatch_vn where Mealtype=N'早餐' and MealBatch=N'" + table.Rows[0]["MealBatch1"].ToString() + "' ";
                    //queryRee = "SELECT * FROM VN_Record.mealbatch_vn where MealType = '早餐' ;";
                    tb2 = mysql.getDataTable(queryRee);
                    model.MealTime1 = tb2.Rows[0]["StartTime"].ToString() + " ~ " + tb2.Rows[0]["EndTime"].ToString();
                }
                if (table.Rows[0]["MealBatch2"].ToString() == "不管控")
                {
                    model.MealBatch2 = "中餐/ Bữa Trưa";
                    model.MealTime2 = "不管控";
                }
                else
                {
                    model.MealBatch2 = "中餐/ Bữa Trưa";
                    //listTime = dataTableMysql("早餐", table.Rows[0]["MealBatch2"].ToString());
                    queryRee = " SELECT * FROM VN_Record.mealbatch_vn where Mealtype=N'中餐' and MealBatch=N'" + table.Rows[0]["MealBatch2"].ToString() + "' ";
                    tb2 = mysql.getDataTable(queryRee);
                    model.MealTime2 = tb2.Rows[0]["StartTime"].ToString() + " ~ " + tb2.Rows[0]["EndTime"].ToString();
                }
                if (table.Rows[0]["MealBatch3"].ToString() == "不管控")
                {
                    model.MealBatch3 = "晚餐/ Bữa Tối";
                    model.MealTime3 = "不管控";
                }
                else
                {
                    model.MealBatch3 = "晚餐/ Bữa Tối";
                    // listTime = dataTableMysql("中餐", table.Rows[0]["MealBatch3"].ToString());
                    queryRee = " SELECT * FROM VN_Record.mealbatch_vn where Mealtype=N'夜宵' and MealBatch=N'" + table.Rows[0]["MealBatch3"].ToString() + "' ";
                    tb2 = mysql.getDataTable(queryRee);
                    model.MealTime3 = tb2.Rows[0]["StartTime"].ToString() + " ~ " + tb2.Rows[0]["EndTime"].ToString();
                }
                if (table.Rows[0]["MealBatch4"].ToString() == "不管控")
                {
                    model.MealBatch4 = "夜宵/ Bữa Đêm";
                    model.MealTime4 = "不管控";
                }
                else
                {
                    model.MealBatch4 = "夜宵/ Bữa Đêm";
                    // listTime = dataTableMysql("夜宵", table.Rows[0]["MealBatch4"].ToString());
                    queryRee = " SELECT * FROM VN_Record.mealbatch_vn where Mealtype=N'夜宵' and MealBatch=N'" + table.Rows[0]["MealBatch4"].ToString() + "' ";
                    tb2 = mysql.getDataTable(queryRee);
                    //model.MealTime4 = tb2.Rows[0]["StartTime"].ToString() + " ~ " + tb2.Rows[0]["EndTime"].ToString();
                }
                model.QRMeal = table.Rows[0]["POSITIONCODE"].ToString();

                model.IcivetBu = model.QRMeal.Substring(6, 3);
            }
            else
            {
                model.QRMeal = "N/A";

                model.MealBatch1 = "N/A";
                model.MealTime1 = "N/A";

                model.MealBatch2 = "N/A";
                model.MealTime2 = "N/A";

                model.MealBatch3 = "N/A";
                model.MealTime3 = "N/A";

                model.MealBatch4 = "N/A";
                model.MealTime4 = "N/A";

            }

            list.Add(model);
            return JArray.FromObject(list);
        }
    }
}
