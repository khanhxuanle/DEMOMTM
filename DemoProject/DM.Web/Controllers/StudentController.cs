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
    public class StudentController : Controller
    {
        private IDMService _dMService;
        public StudentController(IDMService dMService)
        {
            this._dMService = dMService;
        }
        // GET: Student
        public ActionResult Index()
        {
            var listStudentModel = _dMService.getAllStudent();
            return View(listStudentModel);
        }

        [HttpGet]
        public ActionResult NewStudent()
        {
            var listClass = _dMService.getAllClass();


            if(listClass.Count > 0)
            {
                //var listItems = new List<SelectListItem>();
                //foreach (var item in listClass)
                //{
                //    listItems.Add(new SelectListItem { Text = item.ClassName, Value = item.Id.ToString() });
                //}

                ViewBag.lissClassItems = listClass;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStudent(StudentModel newStudent, string listID = null)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(listID))
            {
                string[] listStringID = listID.Split(',');
                newStudent.ClassId = new List<int>();

                foreach (var item in listStringID)
                {
                    newStudent.ClassId.Add(int.Parse(item));
                }

                _dMService.InsertStudent(newStudent);
                return RedirectToAction("Index", "Student");
            }

            var listClass = _dMService.getAllClass();


            if (listClass.Count > 0)
            {
                //var listItems = new List<SelectListItem>();
                //foreach (var item in listClass)
                //{
                //    listItems.Add(new SelectListItem { Text = item.ClassName, Value = item.Id.ToString() });
                //}

                ViewBag.lissClassItems = listClass;
            }


            return View();
        }

        [HttpGet]
        public ActionResult EditStudent(int IdStudent)
        {
            var studentModelObject = _dMService.getStudentById(IdStudent);

            var listClassStudentById = _dMService.getClassStudentById(IdStudent);

            ViewBag.listClassStudentById = listClassStudentById;

            var listAllClass = _dMService.getAllClass();

            if (listAllClass.Count > 0)
            {
                ViewBag.lissAllClass = listAllClass;
            }

            return View(studentModelObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(StudentModel studentModelObject, string listID = null)
        {
            if(ModelState.IsValid && !string.IsNullOrEmpty(listID))
            {
                string[] listStringID = listID.Split(',');
                studentModelObject.ClassId = new List<int>();

                foreach (var item in listStringID)
                {
                    studentModelObject.ClassId.Add(int.Parse(item));
                }

                _dMService.UpdateStudent(studentModelObject);
                return RedirectToAction("Index", "Student");
            }

            var listClassStudentById = _dMService.getClassStudentById(studentModelObject.id);

            ViewBag.listClassStudentById = listClassStudentById;

            var listAllClass = _dMService.getAllClass();

            if (listAllClass.Count > 0)
            {
                ViewBag.lissAllClass = listAllClass;
            }

            return View();
        }

        public ActionResult DeleteStudent(int IdStudent)
        {
            _dMService.DeleteStudent(IdStudent);

            return RedirectToAction("Index", "Student");
        }
    } 
}