using ContosoUniversityAPI.HelperClasses;
using ContosoUniversityAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ContosoUniversityAPI.Services
{
    public class CourseServ : ICourseServ
    {
        public List<Course> Courses { get; set; }

        public List<CourseOnly> CoursesOnly { get; set; }

        public string AddCourse(AddCourse course)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = $@"INSERT INTO dbo.Course(CourseID,Title,Credits,DepartmentID) 
                        
                        VALUES (@CourseID,@Title,@Credits,@DepartmentID)";
            try
            {
                
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        CourseID=course.CourseID,
                        Title=course.Title,
                        Credits=course.Credits,
                        DepartmentID=course.DepartmentID,

                    });
                    
                }
                return Globals.SUCCESS;
            }
            catch (Exception ex)
            {
                LogServ.WriteError(currentMethodName, currentMethodName + " had an error reading from database", ex, sql);
                return Globals.DATABASE_READING_ERROR;
            }
        }

        public List<CourseOnly> GetCourseID(int id)
        {
            string sql = $@"SELECT Student.ID,LastName,FirstMidName, dbo.Course.*,

        (SELECT COUNT (*) FROM dbo.Enrollment WHERE dbo.Enrollment.CourseID=Course.CourseID) AS TotalEnrollments

        FROM Student, dbo.Course, dbo.Enrollment WHERE dbo.Student.ID=dbo.Enrollment.StudentID

        AND dbo.Enrollment.CourseID=Course.CourseID AND Course.CourseID = @ID

        ORDER BY dbo.Student.LastName";

            try
            {
                CoursesOnly = new List<CourseOnly>();
               
               
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql, new { ID = id }))
                    {
                        int idcourse = 1;
                        int idDistinct = 1;

                        while (reader.Read())
                        {
                            

                            CourseOnly course = new CourseOnly();
                            course.CourseID = CheckReader.GetValue(reader, "CourseID", course.CourseID);
                            course.Title = CheckReader.GetValue(reader, "Title", course.Title);
                            course.Credits = CheckReader.GetValue(reader, "Credits", course.Credits);
                            course.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", course.DepartmentID);

                            StudentList studentList= new StudentList();
                            studentList.ID=CheckReader.GetValue(reader,"ID", studentList.ID);
                            studentList.LastName = CheckReader.GetValue(reader, "LastName", studentList.LastName);
                            studentList.FirstMidName = CheckReader.GetValue(reader, "FirstMidName", studentList.FirstMidName);
                            studentList.TotalEnrollments = CheckReader.GetValue(reader, "TotalEnrollments", studentList.TotalEnrollments);

                            
                            if(course.CourseID==idDistinct)
                            {
                                   //De Facut treaba asta cu ID-ul       
                            }
                            else
                            {
                                idDistinct = course.CourseID;
                                course.StudentLists.Add(studentList);
                                CoursesOnly.Add(course);
                                idcourse++;
                            }

                            
                        }
                    }
                }
            }
            catch(Exception)
            {
            }
            return CoursesOnly;
        }

        public List<Course> GetCourses()
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
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
                        LogServ.WriteInfo(currentMethodName, currentMethodName + "was called with succes anda got all the data", sql);
                        //if (Courses.Count > 0)
                        //{
                        //    return Globals.SUCCESS;
                        //}
                        //else
                        //{
                        //    return Globals.NO_RESULTS;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogServ.WriteError(currentMethodName, currentMethodName + " had an error reading from database", ex, sql);

            }
            return Courses;
        }

        public string UpdateCourse(AddCourse course, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = "UPDATE dbo.Course SET Title=@Title, Credits=@Credits, DepartmentID=@DepartmentID WHERE CourseID=@ID";

            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        Title = course.Title,
                        Credits = course.Credits,
                        DepartmentID = course.DepartmentID,
                        ID=id,
                    });
                }
                return Globals.SUCCESS;
            }
            catch (Exception ex)
            {
                LogServ.WriteError(currentMethodName, currentMethodName + "database reading error", ex, sql);
                return Globals.DATABASE_READING_ERROR;
            }
        }
    }
}