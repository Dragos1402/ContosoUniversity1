using ContosoUniversityAPI.HelperClasses;
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
using System.Web.Http.Cors;

namespace ContosoUniversityAPI.Controllers
{
    [RoutePrefix("services")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

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
        [HttpGet]
        [Route("get_course/{id}")]
        public HttpResponseMessage GetCourseID(int id)
        {
            var result = new List<CourseOnly>();
            try
            {
                result = _courseServ.GetCourseID(id);
            }
            catch (Exception)
            {
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpPost]
        [Route("add_course")]
        public HttpResponseMessage AddCourse(AddCourse course)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                    string result = _courseServ.AddCourse(course);
                    if (result == Globals.SUCCESS)
                    {
                        //response.data = new { result_id = _orderServ.ResultId };
                        string json = JsonConvert.SerializeObject(response.data);
                        LogServ.WriteInfo(currentMethodName, currentMethodName + "(controller) was called with success", json);
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
        [HttpPut]
        [Route("update_course/{id}")]
        public HttpResponseMessage UpdateCourse(AddCourse addCourse, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                string result = _courseServ.UpdateCourse(addCourse, id);
                if (result==Globals.SUCCESS)
                {
                    string json = JsonConvert.SerializeObject(response.data);
                    LogServ.WriteInfo(currentMethodName, currentMethodName + "(controller) was called with succes", json);
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
