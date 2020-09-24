using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Zork
{
    internal class Program
    {
        private static string CurrentRoom
        {
            get
            {
                return Rooms[LocationColumn];
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine("This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.");
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        else
                        {
                            Console.WriteLine($"You moved {command}.");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }

            }
        }

        private static bool Move(Commands command)
        {
            bool movePossible = true;
            switch (command)
            {
                case Commands.EAST when LocationColumn < Rooms.GetLength(0) - 1:
                    LocationColumn++;
                    break;

                case Commands.WEST when LocationColumn > 0:
                    LocationColumn--;
                    break;

                case Commands.NORTH:
                case Commands.SOUTH:
                    movePossible = false;
                    break;

                default:
                    movePossible = false;
                    break;
            }

            return movePossible;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static readonly string[] Rooms =
        {
             "Forest", "West of House", "Behind House", "Clearing", "Canyon View" 
        };

        private static int LocationColumn = 1;
    }
}
