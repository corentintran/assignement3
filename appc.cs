using System;

namespace App
{
    class App
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            while(!endApp)
            {
                Console.WriteLine("==============================================================\r");
                Console.WriteLine("Welcolme to community Librart Movie DVD Management System\r");
                Console.WriteLine("==============================================================\n");

                // Main Menu
                Console.WriteLine("======================= Main Menu =========================\n");
                // Ask the user to choose an option.
                Console.WriteLine("\t1. Staff Login");
                Console.WriteLine("\t2. Member Login");
                Console.WriteLine("\t0. Exit\n");
                Console.Write("Enter your choice ==> (1/2/0)\n");


                // Use a switch statement to choose wich menu.
                switch (Console.ReadLine())
                {
                    case "1":
                        // Staff Menu
                        if (Menu.StaffLogin()) Menu.DisplayStaffMenu();
                        break;
                    case "2":
                        // Member Menu
                        if (Menu.MemberLogin()) Menu.DisplayMemberMenu();
                        break;
                    case "0":
                        endApp = true;
                        break;
                }


                
            }
            // Wait for the user to respond before closing.
        Console.WriteLine(" Press any key to close the app ...");
        Console.ReadKey();
        return;
        }


        /** Display the login interface for the staff
        * Return true if the username and password are correct, false otherwise
        */
        public static bool StaffLogin()
        {
            string username = "";
            string password = "";
            Console.WriteLine("======================= Staff Login =========================\n");
            Console.WriteLine("Enter your username:");
            username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            password = Console.ReadLine();

            if (username == "staff" && password == "today123") return true;
            else {
                Console.WriteLine("The username or password are incorrect");
                return false;
            }
        }

        /** Display the login interface for the staff
        * Return true if the user is registered and the password is correct, false otherwise
        */
        public static bool MemberLogin()
        {
            string firstname = "";
            string lastname = "";
            string password = "";
            Console.WriteLine("======================= Member Login =========================\n");
            Console.WriteLine("Enter your first name:");
            firstname = Console.ReadLine();
            Console.WriteLine("Enter your last name:");
            lastname = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            password = Console.ReadLine();

            // TODO : check is user is in the member collection

            return true;
        }

        /** Display the staff Menu
        */
        public static void DisplayStaffMenu()
        {
            Console.WriteLine("======================= Staff Menu =========================\n");
            // Ask the user to choose an option.
            Console.WriteLine("\t1. Add new DVDs of a new movie to the system");
            Console.WriteLine("\t2. Remove DVDs of a movie from the system");
            Console.WriteLine("\t3. Register a new member to the system");
            Console.WriteLine("\t4. Remove a registered member from the system");
            Console.WriteLine("\t5. Display a member's contact phone number, given the member's name");
            Console.WriteLine("\t6. Display all members who are currently renting a particular movie");
            Console.WriteLine("\t0. Return to the main menu\n");
            Console.Write("Enter your choice ==> (1/2/3/4/5/6/0)\n");
            /*Switch (Console.ReadLine())
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "0":
            }*/
        }

        public static void DisplayMemberMenu()
        {
            Console.WriteLine("======================= Member Menu =========================\n");
            // Ask the user to choose an option.
            Console.WriteLine("\t1. Browse all the movies");
            Console.WriteLine("\t2. Display all informations about a movie, given the title of the movie");
            Console.WriteLine("\t3. Borrow a movie DVD");
            Console.WriteLine("\t4. Return a movie DVD");
            Console.WriteLine("\t5. List current borrowing movies");
            Console.WriteLine("\t6. Display the top 3 movies rented by the members");
            Console.WriteLine("\t0. Return to the main menu\n");
            Console.Write("Enter your choice ==> (1/2/3/4/5/6/0)\n");
            /*Switch (Console.ReadLine())
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "0":
            }*/
        }
    }

    class StaffSystem
    {
        
        public void AddDVDs()
        {
            //TODO
        }

        public void RemoveDVDs()
        {
            //TODO
        }

        public void RegisterNewMember()
        {
            //TODO
        }

        public bool RemoveMember()
        {
            //TODO
            return true;
        }

        public string DisplayPhoneNumber()
        {
            string phonenumber = "";
            //TODO
            return phonenumber;
        }
        
        public string DisplayMembers()
        {
            string members = "";
            //TODO
            return members;
        }
    }

    class MemberSystem
    {
        public string DisplayAllMovies()
        {
            string allmovies = "";
            //TODO
            return allmovies;
        }

        public string DisplayInfo()
        {
            string movieinfos = "";
            //TODO
            return movieinfos;
        }

        public bool BorrowDVD()
        {
            //TODO
            return true;
        }

        public bool ReturnDVD()
        {
            //TODO
            return true;
        }

        public string ListMoviesBorrowed()
        {
            list = "";
            //TODO
            return list;
        }

        public string Display top3()
        {
            string top3 = "";
            //TODO
            return top3;
        }
    }
}