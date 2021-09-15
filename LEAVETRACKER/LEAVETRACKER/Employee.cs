using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LEAVETRACKER.Repositories;

namespace LEAVETRACKER
{
    class EMPLOYEE: IEmployee
    {
        public int EmployeeId { set; get; }
        public int Id { set; get; }
        public string Name { set; get; }
        public int ManagerId { set; get; }
        public string Status = "open";

        public void LeavesOption()
        {


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
                List<EMPLOYEE> Employee = Db.GetEmployeeList();
                var matchingEmp = Employee.FindAll(x => (Convert.ToInt32(x.EmployeeId) == EmployeeId));
            if (matchingEmp.Count != 0)
            {
                foreach (EMPLOYEE l in matchingEmp) { this.Name = l.Name; this.ManagerId = Convert.ToInt32(l.ManagerId); }


                Console.WriteLine("Enter the Choice out of =\n 1)Create Leave \n 2)List of Your Own Leaves \n 3)List Assigned to You \n 4)Quit ");
            Choice:
                Console.WriteLine("Enter=");
                try
                {
                    int Choice = Convert.ToInt32(Console.ReadLine());
                    switch (Choice)
                    {
                        case 1:
                            ILeave leaves = new LEAVE();
                            leaves.CreateLeave();

                            goto Choice;

                        case 2:
                            ILeaveDb Db1 = new DbOperationsLeaves();
                            Db1.GetLeaves(EmployeeId);

                            goto Choice;

                        case 3:
                            ILeaveDb Db2 = new DbOperationsLeaves();
                            Db2.AssignedLeaves(EmployeeId, Id, Status);


                            goto Choice;
                        case 4:

                            break;

                        default:
                            Console.WriteLine("Invalid Choice");
                            goto Choice;
                    }
                }
                catch { Console.WriteLine("Inavalid Choice"); goto Choice; }
            }
            else
            {
                Console.WriteLine("Invalid Id");
                goto ID;
            }
        }



    }

}


