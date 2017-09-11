using DM.Data;
using DM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Service.IServices
{
    public interface IDMService
    {
        string HellWord();

        void InsertClass(ClassModel classObject);

        List<ClassModel> getAllClass();

        ClassModel getClassById(int IdClass);


        ClassModel getDetailClassById(int IdClass);

        void EditClass(ClassModel classObject);

        void DeleteClass(int IdClass);

        void InsertStudent(StudentModel studentModelObject);

        List<StudentModel> getAllStudent();

        string ClassName(int classID);

        StudentModel getStudentById(int IdStudent);

        void UpdateStudent(StudentModel classObject);

        void DeleteStudent(int IdStudent);

        List<int> getClassStudentById(int IdStudent);

    }
}
