using EmployeeDataAccess;
using LEAVEAPI.EmployeeDataAccess;
using LEAVETRACKER.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace LEAVEAPI.Controllers
{
    public class LeaveController : ApiController
    {
        private ILeaveDb _DbLeave;
        public LeaveController(ILeaveDb DbOperationsLeaves)
        {
            _DbLeave = DbOperationsLeaves;
        }
        [HttpGet("ownleaves/{employeeId}")]
        public List<LEAVE> GetOwnLeaves(int employeeId)
        {

            return _DbLeave.GetLeaves(employeeId);
        }


        [HttpGet("/assignedleaves/{id}")]
        public List<LEAVE> GetAssignedLeaves(int Id)
        {
            var list = _DbLeave.AssignedLeaves(Id);
            return list;
        }


        [HttpPost("/createleave/{EmployeeId}")]
        public string Post(int EmployeeId, [FromBody] LEAVE leave)
        {
            IEmployeeDb db = new DbOperationEmployee();
            List<EMPLOYEE> employee = db.GetEmployeeList();
            var matchingEmp = employee.Find(x => (Convert.ToInt32(x.employeeId) == EmployeeId));
            if (leave == null)
            {
                return "Invalid Date OR Invalid Id!";
            }
            else
            {
                if (matchingEmp != null)
                {
                    leave.employeeId = matchingEmp.employeeId;
                    leave.name = matchingEmp.name;
                    leave.managerId = matchingEmp.managerId;
                    if (leave.startDate < leave.endDate)
                    {
                        try
                        {
                            _DbLeave.SaveLeaves(leave.employeeId, leave.name, leave.managerId, leave.title, leave.description, leave.startDate, leave.endDate, leave.status);
                        }
                        catch
                        {
                            return "Inavalid DateFormat";
                        }
                    }
                    else
                    {
                        return "Inavalid Date";
                    }
                }
                else
                {
                    return "Inavalid EmployeeId";
                }

            }
            return "Leave Created Suceesfully!";
        }


        [HttpPut("/updateleave/{ManagerId}/{Id}")]
        public string Put(int ManagerId, int Id, [FromBody] LEAVE lleave)
        {
            List<LEAVE> leaves = _DbLeave.AssignedLeaves(ManagerId);
            var matchingEmp = leaves.Find(x => (Convert.ToInt32(x.id) == Id));

            if (matchingEmp != null)
            {
                _DbLeave.updateleave(Id, lleave.status);
            }
            else
            {
                return "Invalid Id/ManagerId";
            }

            return "Updated leave SuceeFully!";
        }

    }
}

