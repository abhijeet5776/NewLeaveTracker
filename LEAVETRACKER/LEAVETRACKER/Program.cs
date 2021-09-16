using LEAVETRACKER.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace LEAVETRACKER
{
    interface IEmployee { void LeavesOption(); }
    interface ILeave { void CreateLeave(); }
    interface ILeaveDb
    {
        void SaveLeaves(int EmployeeId, string Name, int ManagerId, string Title, string Description, DateTime Startdate, DateTime Enddate, string Status);
        void GetLeaves(int EmployeeId);
        void AssignedLeaves(int EmployeeId, int Id, string Status);
    }
    interface IEmployeeDb { List<EMPLOYEE> GetEmployeeList(); }
    class Program
    {
        static void Main(string[] args)
        {
            IEmployee employee = new EMPLOYEE();
            employee.LeavesOption();
        }
    }

}




