using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLink.Models;

namespace WEBIT_APP.Areas.Link.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Link/Home1
        public ActionResult Index(string id)
        {
            List<DepartmentModel> obj;
            getDepartment getdp = new getDepartment();
            obj = getdp.getListDepartment().ToList();

            if (!string.IsNullOrEmpty(id))
            {
                List<LinkModel> obj1;
                getLink getlink = new getLink();
                obj1 = getlink.GetListLink(id).ToList();
                ViewBag.data = getlink.GetListLink(id).ToList();
            }
            return View(obj);
        }

        public void getSubmenu(string id)
        {
            List<LinkModel> obj;
            getLink getlink = new getLink();
            obj = getlink.GetListLink(id).ToList();
            Session["submenu"] = obj;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password, string OTP)
        {
            if (!string.IsNullOrEmpty(OTP))
            {
                string result = CheckOPT(OTP);
                if (result == "0")
                {
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
                            return RedirectToAction("Index", "Manager");

                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Wrong username or password ";
                            return View("Login");
                        }

                    }
                }
                else
                {
                    string message = "";
                    switch (result)
                    {
                        case "-1":
                            message = "用户账号错误(工号错误)";
                            break;
                        case "-2":
                            message = "OpenId(认证错误)";
                            break;
                        case "-3":
                            message = "密钥错误(请重新注册Token)";
                            break;
                        case "-4":
                            message = "Token日期错误(请验证手机时间)";
                            break;
                        case "-5":
                            message = "Token错误(输入Token错误)";
                            break;
                        case "-6":
                            message = "Token过期";
                            break;
                        case "-7":
                            message = "此systemName系统没有权限使用token (请在systemPrivilege表中加入记录)";
                            break;
                        case "-8":
                            message = "没有注册Token(请登香信注册Token)";
                            break;
                        case "-9":
                            message = "子系统的密码错误";
                            break;
                        case "-10":
                            message = "子系统权限过期(SystemPrivilege)";
                            break;
                        case "-11":
                            message = "此用户无权限使用Token系统(systemUserInfo无纪录)";
                            break;
                        case "-12":
                            message = "此用户无权限使用Token系统(systemUserInfo中 status=0)";
                            break;
                        case "-13":
                            message = "此用户token过期";
                            break;
                        case "-20":
                            message = "无数据";
                            break;
                        case "-100":
                            message = "执行异常";
                            break;

                    }
                    ViewBag.ErrorMessage = message;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please enter OTP Icivet ";
            }

            return View();

        }
        public static string CheckOPT(string OPT)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://civetinterface.foxconn.com/FxTokenWeb/api/Validate/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"appid\":\"2XA5CKB9d.hjpf1qNhznJA2\",\"password\":\"Foxconn0517\",\"account\" :\"V0991291\",\"token\":\"" + OPT + "\",\"type\":0}";

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
    }
}