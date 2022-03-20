using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;
using System.Linq;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                while (runProgram)
                {
                    string selection = GetMenuSelection();

                    switch (selection)
                    {
                        case ("Show all rooms"):
                            List<Room> rooms = roomRepo.GetAll();
                            foreach (Room r in rooms)
                            {
                                Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                            }
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Show all chores"):
                            List<Chore> chores = choreRepo.GetAll();
                            foreach (Chore chore in chores)
                            {
                                Console.WriteLine($"{chore.Name} has an id of {chore.Id}");
                            }
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Show all unassigned chores"):
                            List<Chore> choores = choreRepo.GetUnassigned();
                            foreach(Chore chore in choores)
                            {
                                Console.WriteLine($"{chore.Name} has an id of {chore.Id}");
                            }
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Search for room"):
                            Console.Write("Room Id: ");
                            int id = int.Parse(Console.ReadLine());

                            Room room = roomRepo.GetById(id);

                            Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Search for chore"):
                            Console.Write("Chore id: ");
                            int eyeDee = int.Parse(Console.ReadLine());

                            Chore choire = choreRepo.GetById(eyeDee);

                            Console.WriteLine($"{choire.Name} has an id of {choire.Id}");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Search for roommate"):
                            Console.Write("Roommate ID: ");
                            int eyeD =int.Parse(Console.ReadLine());

                            Roommate roommate = roommateRepo.GetById(eyeD);

                            Console.WriteLine($"{roommate.FirstName} has an id of {roommate.Id} and lives in {roommate.Room.Name}");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Add a room"):
                            Console.Write("Room name: ");
                            string name = Console.ReadLine();

                            Console.Write("Max occupancy: ");
                            int max = int.Parse(Console.ReadLine());

                            Room roomToAdd = new Room()
                            {
                                Name = name,
                                MaxOccupancy = max
                            };

                            roomRepo.Insert(roomToAdd);

                            Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Add a chore"):
                            Console.Write("Chore Name: ");
                            string nayme = Console.ReadLine();

                            Chore choreToAdd = new Chore()
                            {
                                Name = nayme
                            };

                            choreRepo.Insert(choreToAdd);

                            Console.WriteLine($"{choreToAdd.Name} has been added with an Id of {choreToAdd.Id}");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Assign chore to roommate"):
                            List<Chore> chorez = choreRepo.GetAll();
                            foreach (Chore chore in chorez)
                            {
                                Console.WriteLine($"{chore.Name} has an id of {chore.Id}");
                            }

                            Console.WriteLine("Select the ID of the chore you want: ");
                            int chooreId = int.Parse(Console.ReadLine());
                            List<Roommate> roommatez = roommateRepo.GetAll();
                            foreach (Roommate roomer in roommatez)
                            {
                                Console.WriteLine($"{roomer.FirstName} {roomer.LastName}: {roomer.Id}");
                            }

                            Console.WriteLine("Select the ID of the roommate you want to assign to: ");
                            int roomieId = int.Parse(Console.ReadLine());
                            choreRepo.AssignChore(roomieId, chooreId);

                            Console.WriteLine($" SUCCESS!: {roommateRepo.GetById(roomieId).FirstName} has been assigned to {choreRepo.GetById(chooreId).Name}");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Update a room"):
                            List<Room> roomOptions = roomRepo.GetAll();
                            foreach (Room r in roomOptions)
                            {
                                Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                            }

                            Console.Write("Which room would you like to update? ");
                            int selectedRoomId = int.Parse(Console.ReadLine());
                            Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                            Console.Write("New Name: ");
                            selectedRoom.Name = Console.ReadLine();

                            Console.Write("New Max Occupancy: ");
                            selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                            roomRepo.Update(selectedRoom);

                            Console.WriteLine("Room has been successfully updated");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Update a chore"):
                            List<Chore> chorezz = choreRepo.GetAll();
                            foreach (Chore chore in chorezz)
                            {
                                Console.WriteLine($"{chore.Name} has an id of {chore.Id}");
                            }
                            Console.Write("Which chore would you like to update? ");
                            int selectedChoreId = int.Parse(Console.ReadLine());

                            Chore cChoice = chorezz.FirstOrDefault(c => c.Id == selectedChoreId);

                            Console.WriteLine("What should the chore be called? ");
                            string choreName = Console.ReadLine();
                            cChoice.Name = choreName;

                            choreRepo.Update(cChoice);

                            Console.WriteLine("Chore has been successfully updated");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Delete a room"):
                            List<Room> roooms = roomRepo.GetAll();
                            foreach (Room r in roooms)
                            {
                                Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                            }

                            Console.WriteLine($"Selesct a room by its ID to delete: ");
                            int roomChoice = int.Parse(Console.ReadLine());

                            roomRepo.Delete(roomChoice);
                            Console.WriteLine("Success! room delted");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Delete a chore"):
                            List<Chore> chorezzz = choreRepo.GetAll();
                            foreach (Chore chore in chorezzz)
                            {
                                Console.WriteLine($"{chore.Name} has an id of {chore.Id}");
                            }
                            Console.Write("Which chore would you like to delete? ");
                            selectedChoreId = int.Parse(Console.ReadLine());

                            choreRepo.Delete(selectedChoreId);
                            Console.WriteLine("Success! chore delted");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Show chore report"):
                            choreRepo.GetChoreCount();
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case ("Exit"):
                            runProgram = false;
                            break;
                    }
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Search for roommate",
                "Show all unassigned chores",
                "Assign chore to roommate",
                "Update a room",
                "Delete a room",
                "Update a chore",
                "Delete a chore",
                "Show chore report",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}