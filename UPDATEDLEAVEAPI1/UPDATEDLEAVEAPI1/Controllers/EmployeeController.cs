using EmployeeDataAccess;
using LEAVEAPI.EmployeeDataAccess;
using LEAVETRACKER.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LEAVEAPI.Controllers
{
    [Route("/v1/employees")]
    public class EmployeesController : Controller

    {
        private IEmployeeDb _DbEmployee;
        public EmployeesController(IEmployeeDb DbOperationEmployee)
        {
            _DbEmployee = DbOperationEmployee;
        }

        public List<EMPLOYEE> Get()
        {
            var employee = _DbEmployee.GetEmployeeList();
            return employee;
        }
    }
}
