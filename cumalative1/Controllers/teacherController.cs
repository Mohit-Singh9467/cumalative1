using cumalative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cumalative1.Controllers
{
    public class teacherController : Controller
    {
        // GET: teacher
        public ActionResult Index()
        {
            return View();


        }
           

            //GET : /Author/List
            public ActionResult List()
            {
                TeacherdataController controller = new TeacherdataController();
                IEnumerable<Teacher> Teacher = controller.ListTeacher();
                return View(Teacher);
            }

            //GET : /Author/Show/{id}
            public ActionResult Show(int id)
            {
                TeacherdataController controller = new TeacherdataController();
                Teacher NewTeacher = controller.Findteacher(id);


                return View(NewTeacher);
            }

        }
    }
