using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using ContosoUniversityAPI.HelperClasses;
using Dapper;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.Ajax.Utilities;
using System.Data.Entity;

namespace ContosoUniversityAPI.Services
{
    public class InstructorServ : IInstructorServ
    {


        public List<InstructorCount> Instructors { get; set; }

        public Instructor InstructorID { get; set; }
        public InstructorCount InstructorTotal { get; set; }
        public List<InstructorCount> GetInstructors()
        {
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
        public InstructorCount GetInstructorID(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sqlcounts = $@"SELECT Instructor.*,(Select COUNT(*) FROM Department Where Instructor.ID=Department.InstructorID) AS TotalDepartments,

                                 (SELECT COUNT(*) FROM Course WHERE Department.DepartmentID=Course.DepartmentID)

                                  AS TotalCourses FROM Instructor LEFT JOIN Department ON Instructor.ID=Department.InstructorID WHERE dbo.Instructor.ID = @Id";

            try
            {
                InstructorTotal = new InstructorCount();
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sqlcounts, new { Id = id }))
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

                            InstructorTotal = instructor;

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return InstructorTotal;
        }

        public string AddInstructor(InstructorSimplu instructorSimplu)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = "SET IDENTITY_INSERT dbo.Instructor ON" +
                " INSERT INTO dbo.Instructor (ID,LastName,FirstMidName,HireDate) " +
                "VALUES ( @ID,@LastName, @FirstMidName, @HireDate)";
            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        ID = instructorSimplu.ID,
                        LastName = instructorSimplu.LastName,
                        FirstMidname = instructorSimplu.FirstMidName,
                        HireDate = instructorSimplu.HireDate,

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
        public string UpdateInstructor(InstructorSimplu instructorSimplu, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sql = "UPDATE dbo.Instructor " +
                " SET LastName=@LastName," +
                " FirstMidName=@FirstMidname, " +
                " HireDate=@HireDate" +
                " Where dbo.Instructor.ID=@ID";
            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        LastName = instructorSimplu.LastName,
                        FirstMidName = instructorSimplu.FirstMidName,
                        HireDate = instructorSimplu.HireDate,
                        ID = id,
                    });
                }
            }
            catch (Exception ex)
            {
                LogServ.WriteError(currentMethodName, currentMethodName + "database reading error", ex, sql);
                return Globals.DATABASE_READING_ERROR;
            }
            return Globals.SUCCESS;
        }
        public string DeleteInstructor(int id)
        //{
        //    string currentMethodName = MethodBase.GetCurrentMethod().Name;
        //    string sql = "DELETE FROM dbo.Instructor WHERE ID= @ID";
        //    try
        //    {
        //        using (var conn = new SqlConnection(Globals.conn))
        //        {
        //            conn.Execute(sql, new
        //            {
        //                ID=id,
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogServ.WriteError(currentMethodName, currentMethodName + "database reading error", ex, sql);
        //        return Globals.DATABASE_UPDATE_ERROR;
        //    }
        //    return Globals.SUCCESS;
        //}
        //public static void DeleteInstructor()
        {
            using (var conn = new SqlConnection(Globals.conn))
            {
                conn.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = "Delete From dbo.Instructor Where ID=2";
                sqlCommand.ExecuteNonQuery();
            }
            return Globals.SUCCESS;
        }

    }
}