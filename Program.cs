using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PRG182_Project
{
    internal class Program
    {
        enum Menu
        {
            CaptureMember = 1,
            CheckWellnessRewardQualification,
            ShowFitForgeStats,
            RemoveMember,
            Exit
        }

        static void Main(string[] args)
        {
            bool flag = true; // Flag to control the loop
            var members = new ArrayList();

            while (flag)
            {
                Console.WriteLine("Welcome to FitForge!");
                Console.WriteLine("===========================");
                Console.WriteLine("Select an option from the menu: ");
                foreach(Menu options in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{(int)options}. {options}");
                }
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case (int)Menu.CaptureMember:
                        var newMembers = memberDetails();
                        foreach (Hashtable member in newMembers)
                        {
                            members.Add(member);
                        }
                        break;
                    case (int)Menu.CheckWellnessRewardQualification:
                        checkWellnessRewardQualification(members);
                        break;
                    case (int)Menu.ShowFitForgeStats:
                        showFitForgeStats(members);
                        break;
                    case (int)Menu.RemoveMember:
                        removeMember(members);
                        break;
                    case (int)Menu.Exit:
                        Console.WriteLine("Exiting the program...");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        continue;
                }

                if (choice == (int)Menu.Exit) //Ensures the user can exit the program
                {
                    break;
                }

                Console.Write("Do you want to continue? (y/n): "); //Will continue to run forever if the user wants
                string continueChoice = Console.ReadLine().Trim().ToLower();
                if (continueChoice == "y")
                {
                    Console.WriteLine("Continuing the program...");
                    Console.Clear();
                    flag = true;
                }
                else if (continueChoice == "n")
                {
                    Console.WriteLine("Exiting the program...");
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option (y/n).");
                }
            }
        }

        static void removeMember(ArrayList members) //Removes the member from the arraylist using the name of the member to find the member
        {
            Console.Write("Enter the name of the member to remove: ");
            string removeName = Console.ReadLine().Trim();
            bool found = false;
            for (int i = 0; i < members.Count; i++)
            {
                Hashtable member = (Hashtable)members[i];
                string memberName = member["name"].ToString();
                if (memberName.Equals(removeName, StringComparison.OrdinalIgnoreCase))
                {
                    members.RemoveAt(i);
                    Console.WriteLine($"Member {removeName} has been removed.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"No member found with the name {removeName}.");
            }
        }


        static ArrayList memberDetails() //Captures the details of the member and stores them in an arraylist using a hashtable for a key value
        {
            var members = new ArrayList();
            string input;
            do
            {
                var member = new Hashtable();
                member["name"] = checkString("Enter your name: ");
                member["surname"] = checkString("Enter your surname: ");
                member["age"] = checkAge("Enter your age: ");
                member["membershipRank"] = checkMembership("Enter your membership rank: ");
                member["joinDate"] = checkDate("Enter your join date: ");
                member["numOfSP"] = checkNumSmoothies("Enter your number of SP: ");
                member["employmentStatus"] = checkEmployment("Are you employed? (y/n): ");
                member["bestDistance"] = checkDouble("Enter your best distance: ");
                member["favSmoothie"] = checkString("Enter your favorite smoothie: ");
                member["numOfSC"] = checkNumSmoothies("Enter your number of SC: ");

                members.Add(member);

                Console.Write("Do you want to enter another applicant (y/n): ");
                input = Console.ReadLine().Trim().ToLower();
                Console.Clear();
            } while (input == "y");
            return members;
        }

        static string checkString(string message) //Checks if the input is a string, allows only letters and not empty
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
                if (!input.All(char.IsLetter))
                {
                    Console.WriteLine("Can't be numbers. Please try again.");
                    input = null;
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        static int checkAge(string message) //Check the age is an int value and ensures no negative value is entered and above 100 years old
        {
            int input;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out input) && input >= 0 && input <= 100)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again. ");
                }
            }
        }

        static int checkMembership(string message) //Reads in a number between 0 and 9999, ensures int value and no negative value
        {
            int input;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < 10000)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static int checkNumSmoothies(string message) //Checks the value is int and not over 1000 or negative
        {
            int input;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < 1000)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static double checkDouble(string message) //Ensures it is a double value between 0 and 100 and not negative
        {
            double input;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out input) && input >= 0 && input <= 100)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static DateTime checkDate(string message) //Checks if the input is a date format of yyyy/MM/dd
        {
            DateTime input;
            while (true)
            {
                Console.Write(message);
                if (DateTime.TryParse(Console.ReadLine(), out input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static bool checkEmployment(string message) //Checks the user answers yes or no, can't enter anything except yes and no
        {
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine().Trim().ToLower();
                if (input == "yes" || input == "y")
                {
                    return true;
                }
                else if (input == "no" || input == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static ArrayList checkWellnessRewardQualification(ArrayList members) //Checks if the user is qualified for the wellness reward
        {
            var qualifiedMembers = new ArrayList();
            foreach (Hashtable member in members)
            {
                bool employed = (bool)member["employmentStatus"];
                int age = (int)member["age"];
                DateTime joinDate = (DateTime)member["joinDate"];
                int membershipRank = (int)member["membershipRank"];
                double bestDistance = (double)member["bestDistance"];
                int numOfSC = (int)member["numOfSC"];
                string favSmoothie = (string)member["favSmoothie"];
                double combinedAvg = (membershipRank + bestDistance) / 2.0; // Calculate the combined average
                if (age < 18 && !employed)
                {
                    continue;
                }
                if (age >= 18 && !employed)
                {
                    continue;
                }
                if ((DateTime.Now - joinDate).TotalDays > 365 * 2)
                {
                    continue;
                }
                if (!(membershipRank > 2000 || bestDistance > 20 || combinedAvg > 2000))
                {
                    continue;
                }

                int months = Math.Max(1, (int)((DateTime.Now - joinDate).TotalDays / 30.0)); // Calculate the number of months
                double avgSC = (double)numOfSC / months;
                if (avgSC < 4)
                {
                    continue;
                }
                if (avgSC > 20 || favSmoothie.Trim().Equals("ChocoChurned Chaos", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                qualifiedMembers.Add(member);
            }
            if (qualifiedMembers.Count == 0)
            {
                Console.WriteLine("No qualified members found.");
            }
            else
            {
                Console.WriteLine("Qualified members:");
                foreach (Hashtable member in qualifiedMembers)
                {
                    Console.WriteLine($"{member["name"]} {member["age"]} {member["membershipRank"]} {member["joinDate"]} {member["numOfSP"]} {member["bestDistance"]} {member["employmentStatus"]} {member["favSmoothie"]} {member["numOfSC"]}");
                }
            }
            return qualifiedMembers;
        }

        static void countQualifiedMembers(ArrayList members, ArrayList qualifiedMembers) //Counts the amount of qualified members and denied members
        {
            int qualifiedCount = qualifiedMembers.Count;
            int deniedCount = members.Count - qualifiedCount;
            Console.WriteLine($"Qualified Members: {qualifiedCount}\nDenied Members: {deniedCount}");
        }

        static void showFitForgeStats(ArrayList members) //Shows the statistics of the members
        {
            Console.Clear();
            int totalMembers = members.Count;
            Console.WriteLine("FitForge Stats:");
            Console.WriteLine("Total number of members: " + totalMembers);
            Console.WriteLine("Number of qualified members and non-qualified members: ");
            countQualifiedMembers(members, checkWellnessRewardQualification(members));
        }
    }
}

