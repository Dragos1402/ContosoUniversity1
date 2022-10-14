using ContosoUniversityAPI.HelperClasses;
using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.Common;
using System.Reflection;

namespace ContosoUniversityAPI.Services
{
    public class OfficeAssignmentServ : IOfficeAssignmentServ
    {
        public List<OfficeAssignment> OfficeAssignments { get; set; }

        public string AddOfficeAssignment(AddOfficeAssignment officeAssignment)
        {
            string sql = "INSERT INTO dbo.OfficeAssignment(InstructorID, Location) Values ( @InstructorID,@Location)";
            string currentMethodName = MethodBase.GetCurrentMethod().Name;

            try
            {
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        InstructorID = officeAssignment.InstructorID,
                        Location = officeAssignment.Location,
                    });
                }
                return Globals.SUCCESS;
            }
            catch (Exception ex)
            {
                LogServ.WriteError(currentMethodName, currentMethodName + "had an eerror reading from database", ex, sql);
                return Globals.DATABASE_READING_ERROR;
            }
        }

        public string GetOfficeAssignment()
        {
            string sql = "SELECT * FROM dbo.OfficeAssignment INNER JOIN dbo.Instructor ON dbo.OfficeAssignment.InstructorID= dbo.Instructor.ID";
            string currentMethodName = MethodBase.GetCurrentMethod().Name;

            try
            {
                OfficeAssignments = new List<OfficeAssignment>();
               using (var conn = new SqlConnection(Globals.conn))
               {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql))
                    {
                        while (reader.Read())
                        {
                            OfficeAssignment officeAssignment = new OfficeAssignment();

                            officeAssignment.InstructorID = CheckReader.GetValue(reader, "InstructorID", officeAssignment.InstructorID);
                            officeAssignment.Location = CheckReader.GetValue(reader, "Location", officeAssignment.Location);

                            InstructorCount instructorCount = new InstructorCount();
                            instructorCount.ID = CheckReader.GetValue(reader, "ID", instructorCount.ID);
                            instructorCount.LastName = CheckReader.GetValue(reader, "LastName", instructorCount.LastName);
                            instructorCount.FirstMidName = CheckReader.GetValue(reader, "FirstMidName", instructorCount.FirstMidName);
                            instructorCount.HireDate = CheckReader.GetValue(reader, "HireDate", instructorCount.HireDate);

                            officeAssignment.InstructorCount = instructorCount;
                            OfficeAssignments.Add(officeAssignment);
                        }

                        if (OfficeAssignments.Count > 0)
                        {
                            return Globals.SUCCESS;
                        }
                        else
                        {
                            return Globals.NO_RESULTS;
                        }
                        
                    }
               }
            }
            catch (Exception ex)
            {

                return Globals.DATABASE_READING_ERROR;
            }
        }

        public List<OfficeAssignment> GetOfficeAssignments(int id)
        {
            string sql = "SELECT * FROM dbo.OfficeAssignment Where OfficeAssignment.InstructorID = @ID";

            try
            {
                OfficeAssignments = new List<OfficeAssignment>();
                using (var conn = new SqlConnection(Globals.conn))
                {
                    using (var reader = (DbDataReader)conn.ExecuteReader(sql, new { Id = id }))
                    {
                        while (reader.Read())
                            {
                                OfficeAssignment officeAssignment = new OfficeAssignment();

                                officeAssignment.InstructorID = CheckReader.GetValue(reader, "InstructorID", officeAssignment.InstructorID);
                                officeAssignment.Location = CheckReader.GetValue(reader, "Location", officeAssignment.Location);

                                OfficeAssignments.Add(officeAssignment);

                            }
                        }
                }
            }
            catch (Exception)
            {
            }
            return OfficeAssignments;
        }

        public string UpdateOfficeAssignment(AddOfficeAssignment officeAssignment, int id)
        {
            string sql = "UPDATE dbo.OfficeAssignment " +
                "SET  Location=@ValueLocation " +
                "Where InstructorID=@ID";
            string currentMethodName = MethodBase.GetCurrentMethod().Name;

            try
            {
                
                using (var conn = new SqlConnection(Globals.conn))
                {
                    conn.Execute(sql, new
                    {
                        
                        ValueLocation = officeAssignment.Location,
                        ID= id
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