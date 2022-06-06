using System;

namespace CAB301_Assignment3
{

    class App
    {
        static void Main(string[] args)
        {

            Globals.allMembers = new MemberCollection(100);
            Globals.allMovies = new MovieCollection();
            //Tests
            Console.WriteLine(Globals.allMovies.Insert(new Movie("Superman Returns", MovieGenre.Action, MovieClassification.M, 120, 1)));
            Console.WriteLine(Globals.allMovies.Insert(new Movie("A", MovieGenre.Comedy, MovieClassification.PG, 100, 3)));
            Console.WriteLine(Globals.allMovies.Insert(new Movie("B")));
            Console.WriteLine(Globals.allMovies.Insert(new Movie("C")));
           
            bool endApp = false;

            while (!endApp)
            {
                Console.WriteLine("==============================================================");
                Console.WriteLine("Welcome to the community Library Movie DVD Management System");
                Console.WriteLine("==============================================================\n");

                // Main Menu
                Console.WriteLine("======================= Main Menu =========================\n");
                // Ask the user to choose an option.
                Console.WriteLine("\t 1. Staff Login");
                Console.WriteLine("\t 2. Member Login");
                Console.WriteLine("\t 0. Exit\n");
                Console.Write("Enter your choice ==> (1/2/0)\n");


                // Use a switch statement to choose which menu.
                switch (Console.ReadLine())
                {
                    case "1":
                        // Staff Menu
                        Console.Clear();
                        if (StaffLogin()) DisplayStaffMenu();
                        break;
                    case "2":
                        // Member Menu
                        if (MemberLogin()) DisplayMemberMenu();
                        break;
                    case "0":
                        endApp = true;
                        break;
                }
                Console.Clear();
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
            else
            {
                Console.WriteLine("The username or password are incorrect\n");
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

            IMember login = new Member(firstname, lastname);
            Globals.currentUser = Globals.allMembers.Find(login);
            if (Globals.currentUser==null) {
                Console.WriteLine("The username or password are incorrect\n");
                return false;
            } else return true;
        }

        /** Display the staff Menu
        */
        public static void DisplayStaffMenu()
        {

            bool backHome = false;

            while (!backHome)
            {
                Console.Clear();
                StaffMenu staffMenu = new StaffMenu();
                Console.WriteLine("======================= Staff Menu =========================\n");
                // Ask the user to choose an option.
                Console.WriteLine("\t1. Add new DVDs of a movie to the system");
                Console.WriteLine("\t2. Remove DVDs of a movie from the system");
                Console.WriteLine("\t3. Register a new member to the system");
                Console.WriteLine("\t4. Remove a registered member from the system");
                Console.WriteLine("\t5. Display a member's contact phone number, given the member's name");
                Console.WriteLine("\t6. Display all members who are currently renting a particular movie");
                Console.WriteLine("\t0. Return to the main menu\n");
                Console.Write("Enter your choice ==> (1/2/3/4/5/6/0)\n");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Enter the title of the movie:");
                        staffMenu.AddDVDs(Console.ReadLine());
                        break;
                    case "2":
                        Console.WriteLine("Enter the title of the movie:");
                        staffMenu.RemoveDVDs(Console.ReadLine());
                        break;
                    case "3":
                        if (staffMenu.RegisterNewMember()) Console.WriteLine("The member has been registered successfully!");
                        break;
                    case "4":
                        if (staffMenu.RemoveMember()) Console.WriteLine("The member has been removed successfully!");
                        break;
                    case "5":
                        Console.WriteLine(staffMenu.DisplayPhoneNumber());
                        break;
                    case "6":
                        Console.WriteLine("Enter the title of the movie:");
                        Console.WriteLine(staffMenu.DisplayMembers(Console.ReadLine()));
                        break;
                    case "0":
                        backHome = true;
                        break;
                }
            }

        }

        public static void DisplayMemberMenu()
        {
            bool backHome = false;

            while (!backHome)
            {

                Console.Clear();
                MemberMenu memberMenu = new MemberMenu();


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

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Listing all movies in the library\n");
                        memberMenu.DisplayAllMovies();
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter the title of the movie.\n");
                        string neededTitle = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Movie Information\n");
                        Console.WriteLine(memberMenu.DisplayInfo(neededTitle));
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter the title of the movie you wish to borrow.\n");
                        if (memberMenu.BorrowDVD(Console.ReadLine())) Console.WriteLine("Movie Borrowed.");
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter the title of the movie you wish to return.\n");
                        if (memberMenu.ReturnDVD(Console.ReadLine())) Console.WriteLine("Movie Returned.");
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "5":
                        Console.Clear();
                        memberMenu.ListMoviesBorrowed();
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine(memberMenu.DisplayTop3());
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "0":
                        backHome = true;
                        break;
                }
                
            }
        }
    }
}
