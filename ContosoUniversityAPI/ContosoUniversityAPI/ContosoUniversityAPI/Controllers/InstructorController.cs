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
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ContosoUniversityAPI.Controllers
{
    [RoutePrefix("services")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

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
            var result = new InstructorCount();
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
        [HttpPost]
        [Route("add_instructor")]
        public HttpResponseMessage AddInstructor(InstructorSimplu instructorSimplu)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                //if (Globals.CheckToken(Request.Headers.GetValues("Token").First().ToString()))
                {
                    //string result = _instructorServ.AddInstructor(instructorNou, Request.Headers.GetValues("Token").First().ToString());
                    string result = _instructorServ.AddInstructor(instructorSimplu);

                    if (result == Globals.SUCCESS)
                    {
                        response.data = new { result_id = _instructorServ.Instructors };
                        string json = JsonConvert.SerializeObject(response.data);
                        LogServ.WriteInfo(currentMethodName, currentMethodName + "(controller) was called with success", json);
                    }
                    response.SetResponse(result, currentMethodName);
                }
                //else
                //    response.SetResponse(Globals.TOKEN_MISMATCH_OR_MISSING, currentMethodName);
            }
            catch (Exception ex)
            {
                response.SetResponse(Globals.GENERIC_CATCH_ERROR, currentMethodName, ex);
                LogServ.WriteError(currentMethodName, currentMethodName + "(controller) had an error", ex);
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(Globals.CreateJSON(response), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpPut]
        [Route("update_instructor/{id}")]
        public HttpResponseMessage UpdateInstructor(InstructorSimplu instructorSimplu, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var response = new Response();

            try
            {
                string result = _instructorServ.UpdateInstructor(instructorSimplu, id);
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
        [HttpPost]
        [Route("delete_instructor/{id}")]
        public HttpResponseMessage DeleteInstructor(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var response = new Response();
            try
            {
                string result = _instructorServ.DeleteInstructor( id);
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
            resp.Content = new StringContent(Globals.CreateJSON(response), Encoding.UTF8, "application/json");
            return resp;
        }
        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }

    }
}
