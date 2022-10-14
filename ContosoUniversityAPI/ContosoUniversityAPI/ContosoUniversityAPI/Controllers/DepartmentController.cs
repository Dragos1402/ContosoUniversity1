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
    public class DepartmentController : ApiController
    {
        IDepartmentServ _departmentServ;

        public DepartmentController(IDepartmentServ departmentServ)
        {
            _departmentServ = departmentServ;
        }

        [HttpGet]
        [Route("get_departments")]
        public HttpResponseMessage GetDepartments()
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<Department>();
            try
            {
                result = _departmentServ.GetDepartments();
            }
            catch (Exception)
            {

            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpGet]
        [Route("get_department/{id}")]
        public HttpResponseMessage GetDepartmentID(int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var result = new DepartmentCount();
            try
            {
                result = _departmentServ.GetDepartmentID(id);
            }
            catch (Exception)
            {
            }
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(CreateJSON(result), System.Text.Encoding.UTF8, "application/json");
            return resp;
        }
        [HttpPost]
        [Route("add_department")]
        public HttpResponseMessage AddDepartment(AddDepartment department)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            Response response = new Response();
            try
            {
                //if (Globals.CheckToken(Request.Headers.GetValues("Token").First().ToString()))
                {
                    //string result = _instructorServ.AddInstructor(instructorNou, Request.Headers.GetValues("Token").First().ToString());
                    string result = _departmentServ.AddDepartment(department);

                    if (result == Globals.SUCCESS)
                    {
                        response.data = new { result_id = _departmentServ.AddDepartment(department) };
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
        [Route("update_department/{id}")]
        public HttpResponseMessage UpdateDepartment(AddDepartment updateDepartment, int id)
        {
            string currentMethodName = MethodBase.GetCurrentMethod().Name;
            var response = new Response();

            try
            {
                string result = _departmentServ.UpdateDepartment(updateDepartment, id);
                if(result==Globals.SUCCESS)
                {
                    string json = JsonConvert.SerializeObject(response.data);
                }
                response.SetResponse(result,currentMethodName);
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
