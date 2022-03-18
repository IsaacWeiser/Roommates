using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;

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
                            int roomId = int.Parse(Console.ReadLine());
                            List<Roommate> roommatez = roommateRepo.GetAll();
                            foreach (Roommate roomer in roommatez)
                            {
                                Console.WriteLine($"{roomer.FirstName} {roomer.LastName}: {roomer.Id}");
                            }

                            Console.WriteLine("Select the ID of the roommate you want to assign to: ");
                            int roomieId = int.Parse(Console.ReadLine());

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