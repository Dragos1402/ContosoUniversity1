using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json;
using ContosoUniversityAPI.Services;
using ContosoUniversityAPI.HelperClasses;
using System.Web.Http.Cors;

namespace ContosoUniversity.APIControllers
{
    [RoutePrefix("services")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [HttpPost]
        [Route("add_student")]
        public HttpResponseMessage AddStudent(AddStudent student)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {

                string result = _studentServ.AddStudent(student);
                if (result == Globals.SUCCESS)
                {
                    string json = JsonConvert.SerializeObject(response.data);
                }
                response.SetResponse(result, currentMethodName);

            }
            catch (Exception)
            {
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(Globals.CreateJSON(response), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}