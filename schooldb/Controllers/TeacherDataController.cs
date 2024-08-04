
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.Http;
using schooldb.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            // create an instance of the connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the server and the database
            Conn.Open();

            // Establish a new command for our database
            MySqlCommand cmd = Conn.CreateCommand();
            
            // Sql query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@Key) or lower(teacherlname) like lower(@Key) or lower (concat(teacherfname, ' ', teacherlname)) like lower(@Key)";
            cmd.Parameters.AddWithValue("@Key", "%" +  SearchKey + "%");
            cmd.Prepare();

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
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST: /api/TeacherData/DeleteTeacher/2</example>

        [HttpPost]
       
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = school.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber) values(@TeacherFname,@TeacherLname,@EmployeeNumber)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}