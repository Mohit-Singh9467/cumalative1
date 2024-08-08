using cumalative1.Models;
using Mysqlx.Datatypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cumalative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: teacher
        public ActionResult Index()
        {
            return View();


        }


        //GET : /Teacher/Listm
        public ActionResult List()
            {
                TeacherdataController controller = new TeacherdataController();
                IEnumerable<Teacher> Teacher = controller.ListTeacher();
                return View(Teacher);
            }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
            {
                TeacherdataController controller = new TeacherdataController();
                Teacher NewTeacher = controller.Findteacher(id);


                return View(NewTeacher);
            }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherdataController controller = new TeacherdataController();
            Teacher NewTeacher = controller.Findteacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherdataController controller = new TeacherdataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(int teacherId, string teacherFname, string teacherlname, string employeenumber, string hiredate, string salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(teacherId);
            Debug.WriteLine(teacherFname);
            Debug.WriteLine(teacherlname);
            Debug.WriteLine(employeenumber);
            Debug.WriteLine(hiredate);
            Debug.WriteLine(salary);

            Teacher NewTeacher = new Teacher();

            NewTeacher.teacherId = teacherId;
            NewTeacher.teacherFname = teacherFname;
            NewTeacher.teacherlname = teacherlname;
            NewTeacher.employeenumber = employeenumber;
            NewTeacher.hiredate = hiredate;
            NewTeacher.salary = salary;

            TeacherdataController controller = new TeacherdataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}
