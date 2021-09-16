using EmployeeDataAccess;
using LEAVEAPI.EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LEAVETRACKER.Repositories
{
    public class DbOperationsLeaves : ILeaveDb
    {

        public void SaveLeaves(int employeeId, string name, int managerId, string title, string description, DateTime startDate, DateTime endDate, string status)
        {
            string connectString = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";

            string query = "insert into LEAVES(Employee_id,Name,Manager_id,Title,Description,Startdate,Enddate,Status)" + "values (@Employee_id, @Name, @Manager_id, @Title, @Description,@Startdate,@Enddate, @Status) ";
            using (SqlConnection connection = new SqlConnection(connectString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Employee_id", SqlDbType.Int).Value = employeeId;
                command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = name;
                command.Parameters.Add("@Manager_id", SqlDbType.Int).Value = managerId;
                command.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = title;
                command.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = description;
                command.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = startDate;
                command.Parameters.Add("@Enddate", SqlDbType.DateTime).Value = endDate;
                command.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = status;
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception(ex.Message);

                }
                connection.Close();
            }
        }
        public List<LEAVE> GetLeaves(int employeeId)
        {
            Console.WriteLine("\t\t\t\t\t**********LIST OF OWN LEAVES*********");
            string connectString = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";
            string queryString = "select * from LEAVES where Employee_id=@EmployeeId ";
            List<LEAVE> List = new List<LEAVE>();
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand Command = new SqlCommand(queryString, connection);

                Command.Parameters.AddWithValue("@EmployeeId", employeeId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = Command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())

                        {
                            LEAVE add = new LEAVE();
                            add.id = Convert.ToInt32(reader.GetValue(0));
                            add.employeeId = Convert.ToInt32(reader.GetValue(1));
                            add.name = reader.GetValue(2).ToString();
                            var Id = reader.GetValue(3).ToString();
                            add.managerId = !string.IsNullOrWhiteSpace(Id) ? Convert.ToInt32(Id) : 0;
                            add.title = reader.GetValue(4).ToString();
                            add.description = reader.GetValue(5).ToString();
                            add.startDate = Convert.ToDateTime(reader.GetValue(6));
                            add.endDate = Convert.ToDateTime(reader.GetValue(7));
                            add.status = reader.GetValue(8).ToString();
                            List.Add(add);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Dont Have Own Leaves");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return List;
        }
        public List<LEAVE> AssignedLeaves(int ManagerId)
        {
            Console.WriteLine("\t\t\t\t\t***********ASSIGNEDLEAVES********");
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";
            string LqueryString = "select * from LEAVES where Manager_id=@ManagerId ";
            List<LEAVE> leaveList = new List<LEAVE>();
            using (SqlConnection connection = new SqlConnection(Connectstring))
            {
                SqlCommand command = new SqlCommand(LqueryString, connection);

                command.Parameters.AddWithValue("@ManagerId", ManagerId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        LEAVE add = new LEAVE();
                        add.id = Convert.ToInt32(reader.GetValue(0));
                        add.employeeId = Convert.ToInt32(reader.GetValue(1));
                        add.name = reader.GetValue(2).ToString();
                        var Id1 = reader.GetValue(3).ToString();
                        add.managerId = !string.IsNullOrWhiteSpace(Id1) ? Convert.ToInt32(Id1) : 0;
                        add.title = reader.GetValue(4).ToString();
                        add.description = reader.GetValue(5).ToString();
                        add.startDate = Convert.ToDateTime(reader.GetValue(6));
                        add.endDate = Convert.ToDateTime(reader.GetValue(7));
                        add.status = reader.GetValue(8).ToString();
                        leaveList.Add(add);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return leaveList;
        }
        public void updateleave(int Id, string Status)
        {
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";
            string New1queryString = " update  LEAVES set Status = '" + Status + "' where Id=" + Id + "; ";
            using (SqlConnection NewConnection =new SqlConnection(Connectstring))
            {
                SqlCommand NewCommand1 = new SqlCommand(New1queryString, NewConnection);
                NewConnection.Open();
                NewCommand1.ExecuteNonQuery();
                NewConnection.Close();
            }
        }
    }
}