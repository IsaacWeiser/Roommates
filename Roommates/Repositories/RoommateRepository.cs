using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roommates.Models;
using Roommates.Repositories;
using Microsoft.Data.SqlClient;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public List<Roommate> GetAll()
        {
            // initiate an sql connection thread
            using (SqlConnection conn = Connection)
            {
                //open the sql connection
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT r.Id, r.FirstName, r.LastName, r.MoveInDate, r.RentPortion, m.Name as 'roomname' FROM Roommate r LEFT JOIN Room m ON m.Id = r.RoomId ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Roommate> roomies = new List<Roommate>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);

                            int fnColumnPositon = reader.GetOrdinal("FirstName");
                            string fnValue = reader.GetString(fnColumnPositon);

                            int lnColumnPosition = reader.GetOrdinal("LastName");
                            string lnValue = reader.GetString(lnColumnPosition);

                            int midCoulumnPosition = reader.GetOrdinal("MoveInDate");
                            DateTime midValue = reader.GetDateTime(midCoulumnPosition);

                            int rpColumnPosition = reader.GetOrdinal("RentPortion");
                            int rpValue = reader.GetInt32(rpColumnPosition);

                            int rnColumnPosition = reader.GetOrdinal("roomname");
                            Room roomValue = new Room()
                            {
                                Name = reader.GetString(rnColumnPosition)
                            };

                            Roommate roommayte = new Roommate()
                            {
                                Id = idValue,
                                FirstName = fnValue,
                                LastName = lnValue,
                                MovedInDate = midValue,
                                RentPortion = rpValue,
                                Room = roomValue
                            };
                            roomies.Add(roommayte);

                        }
                        return roomies;
                    }
                }
            }
        }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT rm.id as 'roomieD', rm.FirstName, rm.RentPortion, r.Name as 'padName' FROM Roommate rm LEFT JOIN Room r on r.Id = rm.RoomId WHERE rm.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Roommate roomie = null;

                        if (reader.Read())
                        {
                            roomie = new Roommate()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("roomieD")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                Room = new Room()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("padName")) 
                                }
                            };
                        }
                        return roomie;
                    }
                }
            }
        }
    }
}
