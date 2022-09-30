using ContosoUniversityAPI.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using ContosoUniversityAPI.Services;
using System.Web.Http.Results;

namespace ContosoUniversity.APIControllers
{
    [RoutePrefix("services")]
   // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        IStudentServ _studentServ;
        public StudentController(IStudentServ studentServ)
        {

            _studentServ=studentServ;
        }
        [HttpGet]
        [Route("get_students")]
        public HttpResponseMessage GetStudents()
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<StudentList>();
            try
            {
                result = _studentServ.GetStudents();
            }
            catch (Exception)
            {

                throw;
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpGet]
        [Route("get_student/{id}")]
        public HttpResponseMessage GetStudentByID(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new Student();
            try
            {
                result = _studentServ.GetStudentByID(id);
            }
            catch (Exception)
            {
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}

