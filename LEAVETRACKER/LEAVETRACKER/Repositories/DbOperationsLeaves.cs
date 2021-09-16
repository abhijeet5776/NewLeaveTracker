using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace LEAVETRACKER.Repositories
{
    class DbOperationsLeaves : ILeaveDb
    {
        public void SaveLeaves(int EmployeeId, string Name, int ManagerId, string Title, string Description, DateTime Startdate, DateTime Enddate, string Status)
        {
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";
            string Query = "insert into LEAVES(Employee_id,Name,Manager_id,Title,Description,Startdate,Enddate,Status)" + "values (@Employee_id, @Name, @Manager_id, @Title, @Description,@Startdate,@Enddate, @Status) ";
            using (SqlConnection connection = new SqlConnection(Connectstring))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@Employee_id", SqlDbType.Int).Value = EmployeeId;
                command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Name;
                command.Parameters.Add("@Manager_id", SqlDbType.Int).Value = ManagerId;
                command.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = Title;
                command.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Description;
                command.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Startdate;
                command.Parameters.Add("@Enddate", SqlDbType.DateTime).Value = Enddate;
                command.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = Status;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void GetLeaves(int EmployeeId)
        {
            Console.WriteLine("\t\t\t\t\t**********LIST OF OWN LEAVES*********");
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";
            string queryString = "select * from LEAVES where Employee_id=@EmployeeId ";
            using (SqlConnection connection = new SqlConnection(Connectstring))
            {
                SqlCommand Command = new SqlCommand(queryString, connection);
                Command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = Command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())

                        {
                            Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                            reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]);
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
        }
        public void AssignedLeaves(int EmployeeId, int Id, string Status)
        {
            Console.WriteLine("\t\t\t\t\t***********ASSIGNEDLEAVES********");
            string Connectstring = "Data Source=Localhost;Initial Catalog=LEAVETRACKER;Integrated Security=True";
            string LqueryString = "select * from LEAVES where Manager_id=@ManagerId ";
            List<LEAVE> Llist = new List<LEAVE>();
            using (SqlConnection connection = new SqlConnection(Connectstring))
            {
                SqlCommand command = new SqlCommand(LqueryString, connection);
                command.Parameters.AddWithValue("@ManagerId", EmployeeId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                            reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]);
                        LEAVE add = new LEAVE();
                        add.Id = Convert.ToInt32(reader.GetValue(0));
                        add.EmployeeId = Convert.ToInt32(reader.GetValue(1));
                        add.Name = reader.GetValue(2).ToString();
                        add.ManagerId = Convert.ToInt32(reader.GetValue(3));
                        Llist.Add(add);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        ch:
            if (Llist.Count != 0)
            {
            lch:
                Console.WriteLine("Enter the Choice of Change Status =1)Yes  2)no");
                try
                {
                    int lChoice = Convert.ToInt32(Console.ReadLine());
                    switch (lChoice)
                    {
                        case 1:
                            using (SqlConnection NewConnection = new SqlConnection(Connectstring))
                            {
                            NewId:
                                Console.WriteLine("Enter the Id");
                                Id = Convert.ToInt32(Console.ReadLine());
                                var matchingId = Llist.FindAll(x => x.Id == Id);
                                if (matchingId.Count != 0)
                                {
                                    Console.WriteLine("Enter the Choice =1)Accept \t 2)Reject ");
                                    int NewChoice = Convert.ToInt32(Console.ReadLine());
                                    switch (NewChoice)
                                    {
                                        case 1:

                                            Status = "Accept";
                                            string New1queryString = " update  LEAVES set Status = '" + Status + "' where Id=" + Id + "; ";
                                            SqlCommand NewCommand1 = new SqlCommand(New1queryString, NewConnection);
                                            NewConnection.Open();
                                            NewCommand1.ExecuteNonQuery();
                                            NewConnection.Close();
                                            goto lch;
                                        case 2:

                                            Status = "Reject";
                                            string New2queryString = " update  LEAVES set Status = '" + Status + "' where Id =" + Id + ";";
                                            SqlCommand NewCommand2 = new SqlCommand(New2queryString, NewConnection);
                                            NewConnection.Open();
                                            NewCommand2.ExecuteNonQuery();
                                            NewConnection.Close();
                                            goto lch;
                                        default:

                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Id");
                                    goto NewId;
                                }
                            }
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Invalid Choice");
                            goto ch;
                    }
                }
                catch { Console.WriteLine("Inavalid Choice"); goto lch; }
            }
            else
            {
                Console.WriteLine("You dont Have any Assigned leaves");
            }

        }
    }
}