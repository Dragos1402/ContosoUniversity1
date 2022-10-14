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

    public class OfficeAssignmentController : ApiController
    {
        IOfficeAssignmentServ _officeServ;
        public OfficeAssignmentController(IOfficeAssignmentServ officeAssignmentServ)
        {
            _officeServ = officeAssignmentServ;
        }
        [HttpGet]
        [Route("get_office_assignment/{id}")]
        public HttpResponseMessage GetOfficeAssignments(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<OfficeAssignment>();
            try
            {
                result = _officeServ.GetOfficeAssignments(id);
            }
            catch (Exception)
            {

            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpGet]
        [Route("get_office_assignments")]
        public HttpResponseMessage GetOfficeAssignment()
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                //if (Globals.CheckToken(Request.Headers.GetValues("Token").First().ToString()))
               //{
                    string result = _officeServ.GetOfficeAssignment();

                    if (result == Globals.SUCCESS)
                    {
                        response.data = _officeServ.OfficeAssignments;

                    }
                    response.SetResponse(result, currentMethodName);
                //}
                //else
                //    response.SetResponse(Globals.TOKEN_MISMATCH_OR_MISSING, currentMethodName);
            }
            catch (Exception ex)
            {
                response.SetResponse(Globals.GENERIC_CATCH_ERROR, currentMethodName, ex);
                LogServ.WriteError(currentMethodName,currentMethodName + "(controller) had an error", ex);
            }
            var resp = Request.CreateResponse();
            resp.Content= new StringContent(Globals.CreateJSON(response),System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpPost]
        [Route("add_office_assignment")]
        public HttpResponseMessage AddOfficeAssignment(AddOfficeAssignment officeAssignment)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                string result = _officeServ.AddOfficeAssignment(officeAssignment);
                if(result== Globals.SUCCESS)
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
        [HttpPut]
        [Route("update_office_assignment/{id}")]
        public HttpResponseMessage UpdateOfficeAssignment(AddOfficeAssignment officeAssignment, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                string result = _officeServ.UpdateOfficeAssignment(officeAssignment, id);
                if(result == Globals.SUCCESS)
                {
                    string json = JsonConvert.SerializeObject(response.data);
                }
                response.SetResponse(result, currentMethodName);
            }
            catch (Exception)
            {
            }
            var resp = Request.CreateResponse();
            resp.Content= new StringContent(Globals.CreateJSON(response),System.Text.Encoding.UTF8,"application/json");
            return resp;
        }
        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
        
    }
}
