
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.Http;
using schooldb.Models;
using MySql.Data.MySqlClient;

namespace schooldb.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext school = new SchoolDbContext();

        // This controller will access the Teacher table of school database

        // <summary>
        // returns the list if the teachers
        // </summary>
        // <example>Get api/TeachersData/ListTeachers</example>
        // <returns>A list of teachers </returns>

        [HttpGet]

        public IEnumerable<Teacher> ListTeachers()
        {
            // create an instance of the connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the server and the database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();
            
            // Sql query
            cmd.CommandText = "Select * from teachers";

            // Gather result set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher>{};

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;

                // Add the teacher name to the list
                Teachers. Add(NewTeacher);

            }

            // Close the connnection between the Mysql and webserver
            Conn.Close();

            // Return the final list
            return Teachers;
                
        }

        [HttpGet]

        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            // create an instance of the connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the server and the database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // Sql query
            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            // Gather result set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;

            }


            return NewTeacher;
        }
    }
}