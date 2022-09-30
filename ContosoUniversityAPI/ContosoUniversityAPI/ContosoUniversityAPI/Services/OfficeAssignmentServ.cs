using ContosoUniversityAPI.HelperClasses;
using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.Common;

namespace ContosoUniversityAPI.Services
{
    public class OfficeAssignmentServ : IOfficeAssignmentServ
    {
        public List<OfficeAssignment> OfficeAssignments { get; set; }

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
    }
}