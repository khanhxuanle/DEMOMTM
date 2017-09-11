using DM.Data;
using DM.Models.Models;
using DM.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDMService _dmService;

        public HomeController(IDMService dmService)
        {
            this._dmService = dmService;
        }
        public ActionResult Index()
        {
            var listClass = _dmService.getAllClass();
            
            return View(listClass);
        }

        [HttpGet]
        public ActionResult EditClass(int IdClass)
        {
            var classModelObjectById = _dmService.getClassById(IdClass);
            return View(classModelObjectById);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClass(ClassModel classModelObject)
        {
            if (ModelState.IsValid)
            {
                _dmService.EditClass(classModelObject);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult NewClass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewClass(ClassModel newClassModelObject)
        {
            if (ModelState.IsValid)
            {
                var classModelOject = new ClassModel
                {
                    ClassName = newClassModelObject.ClassName,
                    ClassDescription = newClassModelObject.ClassDescription
                };

                _dmService.InsertClass(classModelOject);

                return RedirectToAction("Index", "Home");
            }

            return View();

        }

        public ActionResult DeleteClass(int IdClass)
        {
            _dmService.DeleteClass(IdClass);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult CheckExistClass()
        {
            string result = string.Empty;
            var listClass = _dmService.getAllClass();

            result = listClass.Count == 0 ? "false" : "true";

            return Json(new { exist = result });
        }

        public ActionResult Detail(int IdClass)
        {
            var classModelObject = _dmService.getDetailClassById(IdClass);

            return View(classModelObject);
        }
    }
}