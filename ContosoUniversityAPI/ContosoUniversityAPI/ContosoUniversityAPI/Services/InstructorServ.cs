using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using ContosoUniversityAPI.HelperClasses;
using Dapper;

namespace ContosoUniversityAPI.Services
{
    public class InstructorServ : IInstructorServ
    {
        public List<InstructorCount> Instructors { get; set; }

        public Instructor InstructorID { get; set; }

        public List<InstructorCount> GetInstructors()
        {
            string sql = "SELECT * FROM dbo.Instructor INNER JOIN dbo.Department  ON dbo.Instructor.ID = dbo.Department.InstructorID INNER JOIN dbo.Course ON dbo.Department.DepartmentID = dbo.Course.DepartmentID;";
            string sqlCount = "SELECT Instructor.*,(Select COUNT(*) FROM Department Where Instructor.ID=Department.InstructorID) AS TotalDepartments,(SELECT COUNT(*) FROM Course WHERE Department.DepartmentID=Course.DepartmentID) AS TotalCourses FROM Instructor LEFT JOIN Department ON Instructor.ID=Department.InstructorID\r\n";
            try
            {
                Instructors = new List<InstructorCount>();

                using (var conn = new SqlConnection(Globals.conn))
                {
                    {
                        using (var reader = (DbDataReader)conn.ExecuteReader(sqlCount))
                        {
                            while (reader.Read())
                            {
                                InstructorCount instructor = new InstructorCount();

                                instructor.ID = CheckReader.GetValue(reader, "ID", instructor.ID);
                                instructor.LastName = CheckReader.GetValue(reader, "LastName", instructor.LastName);
                                instructor.FirstMidName = CheckReader.GetValue(reader, "FirstMidName", instructor.FirstMidName);
                                instructor.HireDate = CheckReader.GetValue(reader, "HireDate", instructor.HireDate);
                                instructor.TotalDepartments = CheckReader.GetValue(reader, "TotalDepartments", instructor.TotalDepartments);
                                instructor.TotalCourses = CheckReader.GetValue(reader, "TotalCourses", instructor.TotalCourses);

                                //DepartmentList department = new DepartmentList();

                                //department.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", department.DepartmentID);
                                //department.Name = CheckReader.GetValue(reader, "Name", department.Name);
                                //department.Budget = CheckReader.GetValue(reader, "Budget", department.Budget);
                                //department.StartDate = CheckReader.GetValue(reader, "StartDate", department.StartDate);
                                //department.InstructorID = CheckReader.GetValue(reader, "InstructorID", department.InstructorID);
                                //department.TotalCourses = CheckReader.GetValue(reader, "TotalCourses", department.TotalCourses);

                                //Course course = new Course();

                                //course.CourseID = CheckReader.GetValue(reader, "CourseID", course.CourseID);
                                //course.Title = CheckReader.GetValue(reader, "Title", course.Title);
                                //course.Credits = CheckReader.GetValue(reader, "Credits", course.Credits);
                                //course.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", course.DepartmentID);

                                //instructor.Courses.Add(course);
                                //instructor.Departments.Add(department);
                                Instructors.Add(instructor);

                            }
                        }
                    }
                }
            }
            catch (Exception)
            { 
            }
            return Instructors;
        }
        public Instructor GetInstructorID(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = $@"SELECT* FROM dbo.Instructor WHERE Instructor.ID = @Id";
            string sqlimprove = $@"SELECT* FROM dbo.Instructor INNER JOIN dbo.Department on dbo.Instructor.ID =Department.InstructorID WHERE Instructor.ID = @Id";

            try
            {
                InstructorID = new Instructor();
                using (var conn = new SqlConnection(Globals.conn))
                { 
                        using (var reader = (DbDataReader)conn.ExecuteReader(sqlimprove, new { Id = id }))
                    {
                            while (reader.Read())
                            {
                            Instructor instructor = new Instructor();

                            instructor.ID = CheckReader.GetValue(reader, "ID", instructor.ID);
                            instructor.LastName = CheckReader.GetValue(reader, "LastName", instructor.LastName);
                            instructor.FirstMidName = CheckReader.GetValue(reader, "FirstMidName", instructor.FirstMidName);
                            instructor.HireDate = CheckReader.GetValue(reader, "HireDate", instructor.HireDate);

                            DepartmentCount department = new DepartmentCount();

                            department.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", department.DepartmentID);
                            department.Name = CheckReader.GetValue(reader, "Name", department.Name);
                            department.Budget = CheckReader.GetValue(reader, "Budget", department.Budget);
                            department.StartDate = CheckReader.GetValue(reader, "Startdate", department.StartDate);
                            
                            
                            InstructorID = instructor;
                            instructor.Departments.Add(department);
                            }
                        }
                }
            }
            catch (Exception)
            {
            }
            return InstructorID;
        }
    }
}