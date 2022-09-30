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
    public class CourseController : ApiController
    {

        ICourseServ _courseServ;

        public CourseController(ICourseServ courseServ)
        {
            _courseServ = courseServ;
        }

        [HttpGet]
        [Route("get_courses")]

        public HttpResponseMessage GetCourses()
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<Course>();
            try
            {
                result = _courseServ.GetCourses();
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
