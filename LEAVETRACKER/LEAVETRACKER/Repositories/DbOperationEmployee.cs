using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LEAVETRACKER.Repositories
{
    class DbOperationEmployee:IEmployeeDb
    {
        public List<EMPLOYEE> GetEmployeeList()
        {
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";

            string queryString = "select * from EMPLOYEE; ";
            List<EMPLOYEE> List = new List<EMPLOYEE>();

            using (SqlConnection connection =
            new SqlConnection(Connectstring))

            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        EMPLOYEE add = new EMPLOYEE();
                        add.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                        add.Name = reader.GetValue(1).ToString();
                        var Id = reader.GetValue(2).ToString();
                        add.ManagerId = !string.IsNullOrWhiteSpace(Id) ? Convert.ToInt32(Id) : 0;

                        List.Add(add);

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return List;
            }
        }
    }
}
