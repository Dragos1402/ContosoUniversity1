using ContosoUniversityAPI.HelperClasses;
using ContosoUniversityAPI.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Results;

namespace ContosoUniversityAPI.Services
{
    public class StudentServ : IStudentServ
    {
        public List<StudentList> Students { get; set; }
        public Student StudentID { get; set; }

        public List<StudentList> GetStudents()
        {
            string connectionString = "Data Source=LAPTOP-IIRNA9FS\\SQLEXPRESS01;Initial Catalog=ContosoUniversity;Trusted_Connection=True";
            string sql = "SELECT * FROM dbo.Student studentel INNER JOIN dbo.Enrollment enrollmentel ON studentel.ID=enrollmentel.StudentID;";
            string sqlCount = "SELECT Distinct Student.*, (select  COUNT(*) from Enrollment Where Student.ID = Enrollment.StudentID) AS Total FROM Student LEFT JOIN Enrollment ON Student.ID = Enrollment.StudentID";
            try
            {
                Students = new List<StudentList>();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sqlCount, connection);
                    {
                        using (var reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                StudentList student = new StudentList();
                                if (!(reader["ID"] is DBNull))
                                    student.ID = Convert.ToInt32(reader["ID"]);
                                if (!(reader["LastName"] is DBNull))
                                    student.LastName = Convert.ToString(reader["LastName"]);
                                if (!(reader["FirstMidName"] is DBNull))
                                    student.FirstMidName = Convert.ToString(reader["FirstMidName"]);
                                if (!(reader["EnrollmentDate"] is DBNull))
                                    student.EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]);
                                if (!(reader["Total"] is DBNull))
                                    student.TotalEnrollments = Convert.ToInt32(reader["Total"]);

                                Students.Add(student);
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return Students;
        }

       public Student GetStudentByID(int id)
       {
            string sql = $@"SELECT * FROM dbo.Student Where Student.ID = @ID";
            try
            {
                StudentID = new Student();

                using (var conn = new SqlConnection(Globals.conn))
                {
                        using (var reader = (DbDataReader)conn.ExecuteReader(sql, new { ID = id }))
                        {     
                            while (reader.Read())
                            {
                                Student student = new Student();

                                student.ID = CheckReader.GetValue(reader, "ID", student.ID);
                                student.LastName = CheckReader.GetValue(reader, "LastName", student.LastName);
                                student.FirstMidName = CheckReader.GetValue(reader, "FirstMidName", student.FirstMidName);
                                student.EnrollmentDate = CheckReader.GetValue(reader, "EnrollmentDate", student.EnrollmentDate);

                                StudentID = student;
                                
                            }
                            
                        }
                }
            }
            catch (Exception )
            {
              
            }
            return StudentID;
       }

        public string AddStudent(AddStudent student)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            string sqladd = $@"INSERT INTO dbo.Student ( LastName, FirstMidName , EnrollmentDate )  VALUES (@lastname,@firstmidname,@enrollmentdate)";

            try
            {
                
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sqladd, new
                    {
                        
                        lastname = student.LastName,
                        firstmidname = student.FirstMidName,
                        enrollmentdate = student.EnrollmentDate
                    });
                        
                }
                return Globals.SUCCESS;
            }
            catch (Exception ex)
            {
                return Globals.DATABASE_WRITING_ERROR;
            }
        }

        //public string UpdateStudent(Student student)
        //{
        //    string sql = $@"SELECT * FROM dbo.Student Where Student.ID = @ID";
        //    try
        //    {
        //        StudentID = new Student();

        //        using (var conn = new SqlConnection(Globals.conn))
        //        {
        //            using (var reader = (DbDataReader)conn.ExecuteReader(sql))
        //            {
        //                while (reader.Read())
        //                {
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }

}
