using cumalative1.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cumalative1.Controllers
{
    public class TeacherdataController : ApiController
    {
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the a table of our blog database.
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/AuthorData/ListAuthors</example>
        /// <returns>
        /// A list of authors (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeacher()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Teacher> Teacher = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teacherId = (int)ResultSet["teacherid"];
                string teacherFname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                string hiredate = ResultSet["hiredate"].ToString();
                string salary = ResultSet["salary"].ToString();



                Teacher Newteacher = new Teacher();
                Newteacher.teacherId = teacherId;
                Newteacher.teacherFname = teacherFname;
                Newteacher.teacherlname = teacherlname;
                Newteacher.employeenumber = employeenumber;
                Newteacher.hiredate = hiredate;
                Newteacher.salary = salary;

                //Add the Author Name to the List
                Teacher.Add(Newteacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teacher;
        }


        /// <summary>
        /// Finds an author in the system given an ID
        /// </summary>
        /// <param name="id">The author primary key</param>
        /// <returns>An author object</returns>
        [HttpGet]
        public Teacher Findteacher(int id)
        {
            Teacher Newteacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teacherId = (int)ResultSet["teacherid"];
                string teacherFname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                string hiredate = ResultSet["hiredate"].ToString();
                string salary = ResultSet["salary"].ToString();

                Teacher NewtTeacher = new Teacher();
                Newteacher.teacherId = teacherId;
                Newteacher.teacherFname = teacherFname;
                Newteacher.teacherlname = teacherlname;
                Newteacher.employeenumber = employeenumber;
                Newteacher.hiredate = hiredate;
                Newteacher.salary = salary;
            }


            return Newteacher;
        }
        /// <summary>
        /// Deletes an Author from the connected MySQL Database if the ID of that author exists. Does NOT maintain relational integrity.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <example>POST /api/AuthorData/DeleteAuthor/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        /// <summary>
        /// Adds an Author to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the author's table.</param>
        /// <example>
        /// POST api/AuthorData/AddAuthor 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Likes Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
       // [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherId, teacherFname, teacherlname, employeenumber, hiredate, salary) values " +
                "(@teacherId, @teacherFname, @teacherlname, @employeenumber, @hiredate, @salary)";
            cmd.Parameters.AddWithValue("@teacherId", NewTeacher.teacherId);
            cmd.Parameters.AddWithValue("@teacherFname", NewTeacher.teacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }

    }
}
