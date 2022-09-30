using ContosoUniversityAPI.Models;
using ContosoUniversityAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace ContosoUniversityAPI.Controllers
{
    [RoutePrefix("services")]
    public class InstructorController : ApiController
    {
        IInstructorServ _instructorServ;
        public InstructorController(IInstructorServ instructorServ)
        {
            _instructorServ = instructorServ;
        }
        [HttpGet]
        [Route("get_instructors")]
        public HttpResponseMessage GetInstructors()
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<InstructorCount>();
            try
            {
                result = _instructorServ.GetInstructors();
            }
            catch (Exception)
            {
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpGet]
        [Route("get_instructor/{id}")]
        public HttpResponseMessage GetInstructorID(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new Instructor();
            try
            {
                result = _instructorServ.GetInstructorID(id);
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
