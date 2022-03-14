using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paylocity.CodingChallenge.Domain;
using Paylocity.CodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Api.Controllers
{
    [Route("CodeChallenge/api/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBusiness _employeeBusiness;

        public EmployeeController(IEmployeeBusiness employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public Response<List<Employee>> GetEmployees()
        {
            var response = new Response<List<Employee>>();
            response.Data = _employeeBusiness.GetEmployees();
            return response;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public Response<Employee> GetEmployee(int employeeId)
        {
            var response = new Response<Employee>();
            response.Data = _employeeBusiness.GetEmployee(employeeId);
            return response;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public ActionResult<Response<Employee>> AddEmployee(Request<Employee> request)
        {
            var response = new Response<Employee>();
            Employee employee = request.Data;
            if (!IsRequestValid(employee)) { return BadRequest(); }
            response.Data = _employeeBusiness.AddEmployee(employee);
            return response;
        }

        [HttpPost]
        [Route("AddDependent")]
        public ActionResult<Response<Employee>> AddDependent(Request<Dependent> request)
        {
            var response = new Response<Employee>();
            Dependent dependent = request.Data;
            if (!IsRequestValid(dependent) || dependent.EmployeeId <= 0) { return BadRequest(); }
            response.Data = _employeeBusiness.AddDependent(dependent);
            return response;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public string HealthCheck()
        {
            return "Healthy";
        }

        private bool IsRequestValid(Person data) => !string.IsNullOrWhiteSpace(data?.FirstName) && !string.IsNullOrWhiteSpace(data?.LastName);
    }
}
