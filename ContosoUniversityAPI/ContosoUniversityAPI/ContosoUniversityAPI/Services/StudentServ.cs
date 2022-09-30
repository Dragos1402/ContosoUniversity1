using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            
            string connectionString = "Data Source=LAPTOP-IIRNA9FS\\SQLEXPRESS01;Initial Catalog=ContosoUniversity;Trusted_Connection=True";
            string sql = $@"SELECT * FROM dbo.Student Where Student.ID = @ID";
            try
            {
                StudentID = new Student();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (var reader = cmd.ExecuteReader())
                        {     
                            while (reader.Read())
                            {
                                Student student = new Student();
                                if (!(reader["ID"] is DBNull))
                                    student.ID = Convert.ToInt32(reader["ID"]);
                                if (!(reader["LastName"] is DBNull))
                                    student.LastName = Convert.ToString(reader["LastName"]);
                                if (!(reader["FirstMidName"] is DBNull))
                                    student.FirstMidName = Convert.ToString(reader["FirstMidName"]);
                                if (!(reader["EnrollmentDate"] is DBNull))
                                    student.EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]);
                                StudentID = student;
                                
                            }
                            
                        }
                    }
                }
            }
            catch (Exception )
            {
            }
            return StudentID;
        }
    }

}
