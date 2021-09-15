using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace leaves_tracker
{
    public class Employee
    {
        public string EmployeeId { set; get; }
        public string Name { set; get; }
        public string ManagerId { set; get; }
       
        public  List<Employee> ReadEmployeeFile()
        {
            var lines = File.ReadAllLines(@"C:\Users\Lenovo\Downloads\Employees.csv");
            var list = new List<Employee>();
           foreach (var line in lines)
            {
                var values = line.Split(',');
                var employees = new Employee() { EmployeeId = values[0], Name = values[1], ManagerId = values[2] };
                list.Add(employees);
            }
        }   
        return list;
    }

    public class Leaves 
    {
        public string EmployeeId;
        public string Name;
        public string ManagerId;
        public string Title;
        public string Description;
        public DateTime  Startdate;
        public DateTime Enddate;
        public string Status="open";

        public void CreateLeave()
            { 
                Console.WriteLine("Enter the Deatails");
                Console.WriteLine("Managerid=" + ManagerId);

                Console.WriteLine("title=");
                Title = Console.ReadLine();

                Console.WriteLine("description=");
                Description = Console.ReadLine();
                startdate:
                Console.WriteLine("Startdate in format [dd/mm/yyyy]");
                try { Startdate = Convert.ToDateTime(Console.ReadLine()); }
                catch { Console.WriteLine("Invalid Date");
                    goto startdate;
                }
                enddate:
                Console.WriteLine("Enddate in format [dd/mm/yyyy]");
                try { Enddate = Convert.ToDateTime(Console.ReadLine());
                    Enddate.CompareTo(Startdate);
                    if (Enddate <= Startdate)
                    {
                        Console.WriteLine("Enter Valid Date");
                        goto enddate;
                    }
                }
                catch { Console.WriteLine("Invalid Date");
                    goto enddate;
                }
                SaveLeaveFile();
            }
    public void employeschoice()
            {
                
              Console.WriteLine("Enter the id");
                ID:
                  var Id= Console.ReadLine();
                  EmployeeId = Id;
                
                
                    var Emp = new Employee();
                    List<Employee> employee = Emp.ReadEmployeeFile();
               var matchingEmp = employee.FirstOrDefault(x => x.EmployeeId == EmployeeId);
                if (matchingEmp != null)
                {
                    this.ManagerId = matchingEmp.ManagerId;
                    this.Name = matchingEmp.Name;

                    Console.WriteLine("Enter the Choice out of =\n 1)Create Leave \n 2)List of Your Own Leaves \n 3)List Assigned to You \n 4)Quit ");
                Choice:
                    Console.WriteLine("Enter=");
                    int Choice = Convert.ToInt32(Console.ReadLine());
                  switch (Choice)
                    {
                        case 1:
                            CreateLeave();
                            goto Choice;

                        case 2:
                            var Newlev = new Leaves();
                            List<Leaves> Newleaves = Newlev.ReadLeaveFile();
                            var matchingNewId = Newleaves.FindAll(x => x.EmployeeId == EmployeeId);


                            foreach (Leaves m in matchingNewId)
                            {
                                this.Description = m.Description;
                                this.Startdate = m.Startdate;
                                this.Enddate = m.Enddate;
                                this.Status = m.Status;
                                this.ManagerId = m.ManagerId;
                                Console.WriteLine("Leaves created by you=" + Description + " " + Startdate + " " + Enddate + " " + Status + "\n");
                            }
                            goto Choice;

                        case 3:
                            var lev = new Leaves();
                            List<Leaves> leaves = lev.ReadLeaveFile();
                            var matchingId = leaves.FindAll(x => x.ManagerId == EmployeeId);


                            foreach (Leaves l in matchingId)
                            {
                                this.EmployeeId = l.EmployeeId;
                                this.Name = l.Name;
                                this.Description = l.Description;
                                this.Title = l.Title;
                                this.Startdate = l.Startdate;
                                this.Enddate = l.Enddate;
                                this.Status = l.Status;
                                this.ManagerId = l.ManagerId;
                                string ldeatails = (EmployeeId + "," + Name + "," + ManagerId + "," + Title + "," + Description + "," + Startdate + "," + Enddate + "," + Status).ToString();
                                Console.WriteLine("Leaves Assigened to You=" + ldeatails);
                                Console.WriteLine("Enter the Choice =1)Accept \t 2)Reject ");
                                int NewChoice = Convert.ToInt32(Console.ReadLine());
                                switch (NewChoice)
                                {
                                    case 1:
                                        Status = "Accept";
                                        break;
                                    case 2:
                                        Status = "Reject";
                                        break;
                                    default:
                                        Status = "Open";
                                        break;
                                }

                                SaveLeaveFile();

                            }
                            goto Choice;
                        case 4:
                            break;

                        default:
                            Console.WriteLine("Invalid Choice");
                            goto Choice;
                    }
                }
                else
                {
                    Console.WriteLine("Enter the valid ID");
                    goto ID;
                }
                
          
       
     }
    public List<Leaves> ReadLeaveFile()
            {
                string filepath = @"C:\Users\Lenovo\source\repos\leaves tracker\leaves tracker\leaves.csv.txt";
                string deatails = (EmployeeId + "," + Name + "," + ManagerId + "," + Title + "," + Description + "," + Startdate + "," + Enddate + "," + Status + "\n").ToString();
               var docs = File.ReadAllLines(filepath);
                var list = new List<Leaves>();
                foreach (string doc in docs)
                {
                    var value = doc.Split(',');
                    var info = new Leaves() { EmployeeId = value[0], Name = value[1], ManagerId = value[2], Title = value[3], Description = value[4], Startdate = Convert.ToDateTime(value[5]), Enddate = Convert.ToDateTime(value[6]), Status = value[7] };

                    list.Add(info);
                }
                return list;

            }
    public List<Leaves> SaveLeaveFile()
            {
                string filepath = @"C:\Users\Lenovo\source\repos\leaves tracker\leaves tracker\leaves.csv.txt";
                string deatails = (EmployeeId + "," + Name + "," + ManagerId + "," + Title + "," + Description + "," + Startdate + "," + Enddate + "," + Status + "\n").ToString();
                File.AppendAllText(filepath, deatails);
                var docs = File.ReadAllLines(filepath);
                var list = new List<Leaves>();
                foreach (string doc in docs)
                {
                    var value = doc.Split(',');
                    var info = new Leaves() { EmployeeId = value[0], Name = value[1], ManagerId = value[2], Title = value[3], Description = value[4], Startdate = Convert.ToDateTime(value[5]), Enddate = Convert.ToDateTime(value[6]), Status = value[7] };
                        list.Add(info);
                    
                }
                return list;

            }

        }
       
        static void Main(string[] args)
        { 
           
         
            var lleaves = new Leaves();
           
            lleaves.employeschoice();
           

        }

    }
}
