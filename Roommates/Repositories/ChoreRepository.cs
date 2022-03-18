using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roommates.Models;
using Microsoft.Data.SqlClient;

namespace Roommates.Repositories
{
    internal class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }

        public List<Chore> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Chore";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Chore> rooms = new List<Chore>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");

                            int idValue = reader.GetInt32(idColumnPosition);

                            int nameColumnPosition = reader.GetOrdinal("Name");
                            string nameValue = reader.GetString(nameColumnPosition);

                          

                            Chore room = new Chore
                            {
                                Id = idValue,
                                Name = nameValue,
                              
                            };
                            rooms.Add(room);
                        }
                        return rooms;
                    }
                }
            }

        }
            public List<Chore> GetUnassigned()
            {
                //start by setting sql connection
                using (SqlConnection conn = Connection)
                {
                //open sql connection
                conn.Open();

                //set up stream to send command to sql command
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                    //set command query
                    cmd.CommandText = "SELECT c.Id, c.Name FROM Chore c LEFT JOIN RoommateChore rc on rc.ChoreId =c.Id LEFT JOIN roommate r ON r.Id = rc.RoommateId";

                    //set up a reader thread
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        //set up list to hold chores
                        List<Chore> chores = new List<Chore>();

                            //loop through table while there is more data to read
                            while(reader.Read())
                            {
                            //obtain and assign the value of the column name for name
                            int nameCoulmnPosition = reader.GetOrdinal("Name");
                            string nameValue = reader.GetString(nameCoulmnPosition);

                            //obtain and assign the value of the column name for id
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);

                            //build object and add to list
                            Chore chore = new Chore()
                            {
                                Id = idValue,
                                Name = nameValue
                            };
                            chores.Add(chore);
                            }
                        return chores;
                        }
                    }
                }
            }

        public Chore GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select Name, Id from Chore Where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Chore room = null;

                        if (reader.Read())
                        {
                            room = new Chore
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                
                            };
                        }
                        return room;
                    }
                }
            }
        }

        public void Insert(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Chore (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    
                    int id = (int)cmd.ExecuteScalar();

                    chore.Id = id;
                }
            }
        }

        public void AssignChore(int roommateId, int choreId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO RoommateChore (RoommateId, ChoreId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@RoommateId, @ChoreId)";
                    cmd.Parameters.AddWithValue("@RoommateId", roommateId);
                    cmd.Parameters.AddWithValue("@ChoreId", choreId);

                    int id = (int)cmd.ExecuteScalar();
                  //  RoommateChore.id = id;

                }
            }
        }

        public void Update(Chore chore)
        {
            using (SqlConnection conn =Connection)
            {
                conn.Open();
                using (SqlCommand cmd= conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Chore
                                      SET Name = @name
                                      WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    cmd.Parameters.AddWithValue("@id", chore.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Chore WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
