using DM.Data;
using DM.Models.Models;
using DM.Service.IServices;
using DM.Web.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDMService _dmService;
        private Export _export;

        public HomeController(IDMService dmService, Export export)
        {
            this._dmService = dmService;
            this._export = export;
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

        public ActionResult DeleteClassStudents(int IdClass, int IdStudent)
        {
            _dmService.DeleteClassStudent(IdClass, IdStudent);
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

        public ActionResult Export()
        {
            // Gọi lại hàm để tạo file excel
            var stream = _export.CreateExcelClassFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=ExcelAllClass.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();

            return null;
        }
    }
}