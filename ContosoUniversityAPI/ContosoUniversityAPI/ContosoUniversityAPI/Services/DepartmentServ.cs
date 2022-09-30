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
    public class DepartmentServ : IDepartmentServ
    {
        public List<DepartmentCount> Departments { get; set; }
        public DepartmentCount DepartmentID { get; set; }

        public DepartmentCount GetDepartmentID (int id)
        {
            string sql = $@"";
            try
            {
                DepartmentID = new DepartmentCount();
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql))
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

        public List<DepartmentCount> GetDepartments()
        {
            string sql = "SELECT Department.*, (select COUNT(*) from Course Where Department.DepartmentID = Course.DepartmentID) AS TotalCourses FROM Department LEFT JOIN Course ON Department.DepartmentID = Course.CourseID;";

            try
            {
                Departments = new List<DepartmentCount>();

                using (var conn = new SqlConnection(Globals.conn))

                {
                    
                   using (var reader = (DbDataReader)conn.ExecuteReader(sql))
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
    }
}