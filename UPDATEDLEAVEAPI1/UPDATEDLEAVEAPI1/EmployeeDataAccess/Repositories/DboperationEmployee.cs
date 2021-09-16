using EmployeeDataAccess;
using LEAVEAPI.EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LEAVETRACKER.Repositories
{

    public class DbOperationEmployee : IEmployeeDb
    {
        public List<EMPLOYEE> GetEmployeeList()
        {
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";

            string queryString = "select * from EMPLOYEE; ";
            List<EMPLOYEE> list = new List<EMPLOYEE>();

            using (SqlConnection connection = new SqlConnection(Connectstring))

            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        EMPLOYEE add = new EMPLOYEE();
                        add.employeeId = Convert.ToInt32(reader.GetValue(0));
                        add.name = reader.GetValue(1).ToString();
                        var Id = reader.GetValue(2).ToString();
                        add.managerId = !string.IsNullOrWhiteSpace(Id) ? Convert.ToInt32(Id) : 0;
                        list.Add(add);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }
        }

    }
}

