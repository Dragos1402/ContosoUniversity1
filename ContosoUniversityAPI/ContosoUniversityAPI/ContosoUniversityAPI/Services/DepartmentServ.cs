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
    public class DepartmentServ : IDepartmentServ
    {
        public List<Department> Departments { get; set; }
        public DepartmentCount DepartmentID { get; set; }

        public string AddDepartment(AddDepartment department)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = "SET IDENTITY_INSERT dbo.Department ON " +
                "INSERT INTO dbo.Department(DepartmentID,Name,Budget,StartDate,InstructorID) VALUES (@DepartmentID,@Name,@Budget,@StartDate,@InstructorID)";
            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        DepartmentID = department.DepartmentID,
                        Name = department.Name,
                        Budget = department.Budget,
                        StartDate = department.StartDate,
                        InstructorID = department.InstructorID,
                    });
                }
                return Globals.SUCCESS;
            }
            catch (Exception)
            {
                return Globals.DATABASE_READING_ERROR;
            }

        }
        public DepartmentCount GetDepartmentID(int id)
        {
            string sql = $@"SELECT* FROM dbo.Department Where dbo.Department.DepartmentID= @ID";
            try
            {
                DepartmentID = new DepartmentCount();
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql, new { ID = id }))
                    {

                        while (reader.Read())
                        {
                            DepartmentCount department = new DepartmentCount();

                            department.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", department.DepartmentID);
                            department.Name = CheckReader.GetValue(reader, "Name", department.Name);
                            department.Budget = CheckReader.GetValue(reader, "Budget", department.Budget);
                            department.StartDate = CheckReader.GetValue(reader, "StartDate", department.StartDate);
                            department.InstructorID = CheckReader.GetValue(reader, "InstructorID", department.InstructorID);
                            department.TotalCourses = CheckReader.GetValue(reader, "TotalCourses", department.TotalCourses);

                            DepartmentID = department;

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return DepartmentID;
        }

        public List<Department> GetDepartments()
        {
            string sql = "SELECT * FROM Department INNER JOIN Course ON Department.DepartmentID = Course.DepartmentID";

            try
            {
                Departments = new List<Department>();

                using (var conn = new SqlConnection(Globals.conn))

                {

                    using (var reader = (DbDataReader)conn.ExecuteReader(sql))
                    {

                        while (reader.Read())
                        {
                            Department department = new Department();

                            department.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", department.DepartmentID);
                            department.Name = CheckReader.GetValue(reader, "Name", department.Name);
                            department.Budget = CheckReader.GetValue(reader, "Budget", department.Budget);
                            department.StartDate = CheckReader.GetValue(reader, "StartDate", department.StartDate);
                            department.InstructorID = CheckReader.GetValue(reader, "InstructorID", department.InstructorID);


                            Course course = new Course();
                            course.CourseID = CheckReader.GetValue(reader, "CourseID", course.CourseID);
                            course.Title = CheckReader.GetValue(reader, "Title", course.Title);
                            course.Credits = CheckReader.GetValue(reader, "Credits", course.Credits);
                            course.DepartmentID = CheckReader.GetValue(reader, "DepartmentID", course.DepartmentID);

                            department.Courses.Add(course);
                            Departments.Add(department);


                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return Departments;
        }

        public string UpdateDepartment(AddDepartment updateDepartment, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = "UPDATE dbo.Department SET Name= @Name, Budget=@Budget, StartDate = @StartDate" +
                " WHERE DepartmentID=@ID";
            try
            {

                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        Name = updateDepartment.Name,
                        Budget = updateDepartment.Budget,
                        StartDate = updateDepartment.StartDate,
                        ID = id
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

