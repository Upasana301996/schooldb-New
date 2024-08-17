using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schooldb.Models;
using System.Diagnostics;

namespace schooldb.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        // Get : /Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // GET : /Author/Show/{i}

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            
            return View(SelectedTeacher);
        }

        // Get : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //Post : /Teacher/Delete/{id}

        public ActionResult Delete(int id) {

            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        // Get: /Teacher/New

        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/Create

        [HttpPost]

        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber)
        {
            Debug.WriteLine("I have accessed the create Method!");
            
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //Get : /Teacher/Update/{id}

        /// <summary>
        /// Routes to the dynamically generated "Teacher Update" page. Gathers informaton from the database
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>
        /// a dynamic "Update Teacher" webpage whhich provides cureent information about the Teacher and asked the
        /// user about the new information</returns>


        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }



        /// <summary>
        /// Receives a post request containing information about the existing teacher in the system with new values
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated forst name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher</param>
        /// <returns>A web page which provides the current information of the teacher</returns>
        /// <example>
        /// POST: /Author/Update/12
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// "TeacherFname": "Upasana",
        /// "TeacherLname": "Paul",
        /// "EmployeeNumber": "T377"
        /// }
        /// </example>

        [HttpPost]
        public ActionResult Upadte(int id, string TeacherFname, string TeacherLname, string EmployeeNumber)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}