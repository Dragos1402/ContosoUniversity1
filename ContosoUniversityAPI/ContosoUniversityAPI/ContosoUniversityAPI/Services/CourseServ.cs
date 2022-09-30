using ContosoUniversityAPI.HelperClasses;
using ContosoUniversityAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContosoUniversityAPI.Services
{
    public class CourseServ : ICourseServ
    {
        public List<Course> Courses { get; set; }

        public List<Course> GetCourses()
        {
            string sql = "SELECT * FROM dbo.Course ";

            try
            {
                Courses = new List<Course>();
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql))
                    {
                        while (reader.Read())
                        {
                            Course course = new Course();

                            course.CourseID = CheckReader.GetValue(reader, "CourseID", course.CourseID);
                            course.Title = CheckReader.GetValue(reader, "Title", course.Title);
                            course.Credits = CheckReader.GetValue(reader, "Credits", course.Credits);
                            course.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", course.DepartmentID);

                            Courses.Add(course);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return Courses;
        }
    }
}