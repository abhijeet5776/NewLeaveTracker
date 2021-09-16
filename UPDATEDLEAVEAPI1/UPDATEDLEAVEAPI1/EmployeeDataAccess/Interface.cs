using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEAVEAPI.EmployeeDataAccess
{
  public  interface ILeaveDb
    {
        void SaveLeaves(int employeeId, string name, int managerId, string title, string description, DateTime startDate, DateTime endDate, string status);
        List<LEAVE> GetLeaves(int employeeId);
        List<LEAVE> AssignedLeaves(int managerId);
        void updateleave(int id, string status);
    }
    public interface IEmployeeDb
    {
        List<EMPLOYEE> GetEmployeeList();
    }
}
