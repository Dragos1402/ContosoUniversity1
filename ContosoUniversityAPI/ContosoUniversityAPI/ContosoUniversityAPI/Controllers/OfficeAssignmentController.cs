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

namespace ContosoUniversityAPI.Controllers
{
    [RoutePrefix("services")]
    public class OfficeAssignmentController : ApiController
    {
        IOfficeAssignmentServ _officeServ;
        public OfficeAssignmentController(IOfficeAssignmentServ officeAssignmentServ)
        {
            _officeServ = officeAssignmentServ;
        }
        [HttpGet]
        [Route("get_office_assignments/{id}")]
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
        public static String CreateJSON(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
        
    }
}
