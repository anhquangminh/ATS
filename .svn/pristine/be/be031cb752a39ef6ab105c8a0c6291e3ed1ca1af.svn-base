using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLink.Models;

namespace WEBIT_APP.Areas.Link.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        [HttpGet]
        public ActionResult Index(string id)
        {
            ViewBag.iddp = id;
            List<LinkModel> obj;
            if (string.IsNullOrEmpty(id))
            {
                getLink getlink = new getLink();
                obj = getlink.GetListLink(string.Empty).ToList();
            }
            else
            {
                getLink getlink = new getLink();
                obj = getlink.GetListLink(id).ToList();
            }

            getDepartment getdp = new getDepartment();
            var myList = getdp.getListDepartment().ToList();
            ViewBag.ErrorCategories = myList;

            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }

            return View(obj);
        }
        public ActionResult CreateDepartment()
        {
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }
            List<DepartmentModel> obj;
            getDepartment getdp = new getDepartment();
            obj = getdp.getListDepartment().ToList();
            return View(obj);
        }
        [HttpPost]
        public ActionResult CreateDepartment(string Name, string Note)
        {
            List<DepartmentModel> obj;
            getDepartment getdp = new getDepartment();
            obj = getdp.getListDepartment().ToList();
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Note))
            {
                ViewBag.Message = "Is empty";
                return View(obj);
            }
            else
            {
                getDepartment listct = new getDepartment();
                if (listct.CompareDeparment(Name) == false)
                {
                    listct.InsertDeparment(Name, Note);
                    ViewBag.Message = "Insert succes";
                    return View(obj);
                }

                ViewBag.Message = "Departmet already exist !";
                return View(obj);
            }
        }
        public ActionResult EditDepartment(string id)
        {
            List<DepartmentModel> obj;
            getDepartment getdp = new getDepartment();
            obj = getdp.getIDDepartment(id).ToList();

            return View(obj.FirstOrDefault());
        }

        public ActionResult DeleteDepartment(string id)
        {
            getDepartment getdp = new getDepartment();
            getdp.DeleteDeparment(id);
            TempData["shortMessage"] = "Delete success!";
            return RedirectToAction("CreateDepartment");
        }
        [HttpPost]
        public ActionResult EditDepartment(DepartmentModel departmentModel)
        {
            getDepartment getdp = new getDepartment();
            getdp.UpdateDeparment(departmentModel);
            TempData["shortMessage"] = "Edit success!";
            return RedirectToAction("CreateDepartment", new { link = "success" });
        }

        public ActionResult CreateLink()
        {
            getDepartment getdp = new getDepartment();
            var myList = getdp.getListDepartment().ToList();
            ViewBag.ErrorCategories = myList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateLink(LinkModel linkModel)
        {
            getDepartment getdp = new getDepartment();
            var myList = getdp.getListDepartment().ToList();
            ViewBag.ErrorCategories = myList;

            if (string.IsNullOrEmpty(linkModel.Contact) || string.IsNullOrEmpty(linkModel.Description) || string.IsNullOrEmpty(linkModel.Link))
            {
                ViewBag.Message = "Do not leave it blank ";
                return View();
            }
            else
            {
                getLink listct = new getLink();
                listct.InsertLink(linkModel);
                ViewBag.Message = "success";
                return View();
                //return RedirectToAction("index", new { link = "success" });
            }
        }
        public ActionResult EditLink(string id)
        {
            ViewBag.hiden = 1;
            getDepartment getdp = new getDepartment();
            var myList = getdp.getListDepartment().ToList();
            ViewBag.ErrorCategories = myList;
            getLink link = new getLink();
            List<LinkModel> obj = link.GetLinkByID(id).ToList();
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditLink(LinkModel lk)
        {
            getLink link = new getLink();
            link.UpdateLink(lk);
            TempData["shortMessage"] = "Edit success!";
            return RedirectToAction("Index");
        }
        public ActionResult DeleteLink(string id)
        {
            getLink link = new getLink();
            link.DeleteLink(id);
            TempData["shortMessage"] = "Delete success!";
            return RedirectToAction("Index");

        }
        public ActionResult AddAcount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAcount(AccountModel ac)
        {
            getAcount accList = new getAcount();
            accList.insertAcount(ac);
            return RedirectToAction("Index");
        }
    }
}