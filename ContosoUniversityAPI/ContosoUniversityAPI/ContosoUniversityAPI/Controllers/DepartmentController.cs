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
            var result = new List<DepartmentCount>();
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
        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}
