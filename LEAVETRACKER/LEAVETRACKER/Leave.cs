using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LEAVETRACKER.Repositories;

namespace LEAVETRACKER
{
    class LEAVE :ILeave
    {
        public int EmployeeId { set; get; }
        public string Name { set; get; }
        public int ManagerId { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public DateTime Startdate { set; get; }
        public DateTime Enddate { set; get; }
        public int Id { get; internal set; }

        public string Status = "open";

        public void CreateLeave()
        {

            List<LEAVE> LIST = new List<LEAVE>();
        ID:
            Console.WriteLine("Enter the Id=");
            try
            {
                EmployeeId = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid Id");
                goto ID;
            }
                IEmployeeDb Db = new DbOperationEmployee();

                List<EMPLOYEE> employee = Db.GetEmployeeList();
                var matchingEmp = employee.FindAll(x => (Convert.ToInt32(x.EmployeeId) == EmployeeId));
            if (matchingEmp != null)
            {
                foreach (EMPLOYEE l in matchingEmp) { this.Name = l.Name; this.ManagerId = Convert.ToInt32(l.ManagerId); }
                Console.WriteLine("Enter the Deatails");
                Console.WriteLine("Managerid=" + ManagerId);
                if (ManagerId != 0 || EmployeeId == 100)
                {

                    Console.WriteLine("Title=");
                    Title = Console.ReadLine();

                    Console.WriteLine("Description=");
                    Description = Console.ReadLine();
                startdate:
                    Console.WriteLine("Startdate in format [dd/mm/yyyy]");
                    try { Startdate = Convert.ToDateTime(Console.ReadLine()); }
                    catch
                    {
                        Console.WriteLine("Invalid Date");
                        goto startdate;
                    }
                enddate:
                    Console.WriteLine("Enddate in format [dd/mm/yyyy]");
                    try
                    {
                        Enddate = Convert.ToDateTime(Console.ReadLine());
                        Enddate.CompareTo(Startdate);
                        if (Enddate <= Startdate)
                        {
                            Console.WriteLine("Enter Valid Date");
                            goto enddate;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Date");
                        goto enddate;
                    }

                    ILeaveDb Db2 = new DbOperationsLeaves();
                    Db2.SaveLeaves(EmployeeId, Name, ManagerId, Title, Description, Startdate, Enddate, Status);

                }
                else
                {
                    Console.WriteLine("Invalid Id");
                    goto ID;
                }

            }
        }

    }


}