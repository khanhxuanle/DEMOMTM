using DM.Data;
using DM.Models.Models;
using DM.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Service.Services
{
    public class DMService : IDMService
    {
        private readonly DMContext _dmContext;

        public DMService(DMContext dmContext)
        {
            this._dmContext = dmContext;
        }
        public string HellWord()
        {
            return "Hello Word";
        }

        public void InsertClass(ClassModel classModelObject)
        {
            var classObject = new Class
            {
                ClassName = classModelObject.ClassName,
                ClassDescription = classModelObject.ClassDescription
            };

            _dmContext.Classes.Add(classObject);
            _dmContext.SaveChanges();
        }

        public List<ClassModel> getAllClass()
        {
            var listClass = _dmContext.Classes.ToList();
            var listClassModelObject = new List<ClassModel>();

            foreach(var item in listClass)
            {
                listClassModelObject.Add(new ClassModel { Id =  item.Id,  ClassName = item.ClassName, ClassDescription = item.ClassDescription });
            }

            return listClassModelObject;
        }

        public ClassModel getClassById(int IdClass)
        {
            var classObjectById = _dmContext.Classes.FirstOrDefault(m => m.Id == IdClass);

            var classModelObjectById = new ClassModel { Id = classObjectById.Id, ClassName = classObjectById.ClassName, ClassDescription = classObjectById.ClassDescription };

            return classModelObjectById;
        }

        public void EditClass(ClassModel classModelObject)
        {
            var classEditObject = _dmContext.Classes.FirstOrDefault(m => m.Id == classModelObject.Id);

            if(classEditObject != null)
            {
                classEditObject.ClassName = classModelObject.ClassName;
                classEditObject.ClassDescription = classModelObject.ClassDescription;
            }

            _dmContext.Entry(classEditObject).State = System.Data.Entity.EntityState.Modified;

            _dmContext.SaveChanges();
        }

        public void DeleteClass(int IdClass)
        {
            var classDeleteObject = _dmContext.Classes.FirstOrDefault(m => m.Id == IdClass);

            foreach (var item in classDeleteObject.Students.ToList())
            {
                classDeleteObject.Students.Remove(item);
            }

            //_dmContext.Entry(classDeleteObject).State = System.Data.Entity.EntityState.Deleted;
            _dmContext.Classes.Remove(classDeleteObject);

            _dmContext.SaveChanges();
            
        }

        public void DeleteClassStudent(int IdClass, int IdStudent)
        {
            var classDeleteObject = _dmContext.Classes.FirstOrDefault(m => m.Id == IdClass);

            var studentObjectById = _dmContext.Students.FirstOrDefault(m => m.id == IdStudent);

            classDeleteObject.Students.Remove(studentObjectById);

            _dmContext.SaveChanges();
        }

        public void InsertStudent(StudentModel studentModelObject)
        {
            var studentObject = new Student
            {
                StudentName = studentModelObject.StudentName,
                StudentAddress = studentModelObject.StudentAddress,
                StudentAge = studentModelObject.StudentAge,
                StudentSex = studentModelObject.StudentSex
            };

            foreach(var item in studentModelObject.ClassId)
            {
                var _classObject = _dmContext.Classes.FirstOrDefault(m => m.Id == item);
                studentObject.Classes.Add(_classObject);
            }

            _dmContext.Students.Add(studentObject);
            _dmContext.SaveChanges();
        }

        public List<StudentModel> getAllStudent()
        {
            var listStudent = _dmContext.Students.ToList();

            var listStudentModel = new List<StudentModel>();

            foreach(var item in listStudent)
            {
                var studentModel = new StudentModel
                {
                    id = item.id,
                    StudentAddress = item.StudentAddress,
                    StudentAge = item.StudentAge,
                    StudentName = item.StudentName,
                    StudentSex = item.StudentSex                 
                };

                studentModel.Classes = new List<ClassModel>();

                foreach(var classItem in item.Classes)
                {
                    studentModel.Classes.Add(new ClassModel {
                        Id = classItem.Id,
                        ClassDescription = classItem.ClassDescription,
                        ClassName = classItem.ClassName
                    });
                }

                listStudentModel.Add(studentModel);
            }

            return listStudentModel;
        }

        public string ClassName(int classID)
        {
            var className = _dmContext.Classes.Where(m => m.Id == classID).Select(m => m.ClassName).SingleOrDefault();
            return className;
        }

        public StudentModel getStudentById(int IdStudent)
        {
            var studentObjectById = _dmContext.Students.FirstOrDefault(m => m.id == IdStudent);

            var studentModelObjectById = new StudentModel {
                id = studentObjectById.id,
                ClassId = studentObjectById.Classes.Select(m => m.Id).ToList(),
                StudentAddress = studentObjectById.StudentAddress,
                StudentAge = studentObjectById.StudentAge,
                StudentName = studentObjectById.StudentName,
                StudentSex = studentObjectById.StudentSex,
            };

            studentModelObjectById.Classes = new List<ClassModel>();

            foreach (var classItem in studentObjectById.Classes)
            {
                studentModelObjectById.Classes.Add(new ClassModel
                {
                    Id = classItem.Id,
                    ClassDescription = classItem.ClassDescription,
                    ClassName = classItem.ClassName
                });
            }

            return studentModelObjectById;
        }

        public void UpdateStudent(StudentModel studentModelObject)
        {
            var studentObject = _dmContext.Students.FirstOrDefault(m => m.id == studentModelObject.id);

            if (studentObject != null)
            {
                studentObject.StudentName = studentModelObject.StudentName;
                studentObject.StudentAge = studentModelObject.StudentAge;
                studentObject.StudentAddress = studentModelObject.StudentAddress;
                studentObject.StudentSex = studentModelObject.StudentSex;
            }

            foreach(var item in studentObject.Classes.ToList())
            {
                studentObject.Classes.Remove(item);
            }

            foreach(var id in studentModelObject.ClassId)
            {
                var classOject = _dmContext.Classes.FirstOrDefault(m => m.Id == id);
                studentObject.Classes.Add(classOject);
            }

            _dmContext.Entry(studentObject).State = System.Data.Entity.EntityState.Modified;

            _dmContext.SaveChanges();

        }

        public void DeleteStudent(int IdStudent)
        {
            var studentDeleteObject = _dmContext.Students.FirstOrDefault(m => m.id == IdStudent);

            foreach(var item in studentDeleteObject.Classes.ToList())
            {
                studentDeleteObject.Classes.Remove(item);
            }

            //_dmContext.Entry(studentDeleteObject).State = System.Data.Entity.EntityState.Deleted;

            _dmContext.Students.Remove(studentDeleteObject);

            _dmContext.SaveChanges();
        }

        public ClassModel getDetailClassById(int IdClass)
        {
            var classObjectById = _dmContext.Classes.FirstOrDefault(m => m.Id == IdClass);

            var classModelObjectById = new ClassModel
            {
                Id = classObjectById.Id,
                ClassName = classObjectById.ClassName,
                ClassDescription = classObjectById.ClassDescription,
            };

            classModelObjectById.Students = new List<StudentModel>();

            foreach (var item in classObjectById.Students)
            {
                classModelObjectById.Students.Add(new StudentModel
                {
                    id = item.id,
                    StudentName = item.StudentName,
                    StudentAddress = item.StudentAddress,
                    StudentAge = item.StudentAge,
                    StudentSex = item.StudentSex,
                    ClassId = item.Classes.Select(m => m.Id).ToList()
                });
            }

            return classModelObjectById;
        }

        public List<int> getClassStudentById(int IdStudent)
        {
            var studentObject = _dmContext.Students.FirstOrDefault(m => m.id == IdStudent);
            return studentObject.Classes.Select(m => m.Id).ToList();
        }

    }
}
