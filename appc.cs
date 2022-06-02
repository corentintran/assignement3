using System;

namespace App
{
    static class Globals
    {
        public static IMemberCollection members; 
        public static IMovieCollection movies;
    }
    
    class App
    {
        static void Main(string[] args)
        {

            Globals.members = new MemberCollection(100);
            Globals.movies = new MovieCollection();
            bool endApp = false;

            while(!endApp)
            {
                Console.WriteLine("==============================================================");
                Console.WriteLine("Welcolme to community Librart Movie DVD Management System");
                Console.WriteLine("==============================================================\n");

                // Main Menu
                Console.WriteLine("======================= Main Menu =========================\n");
                // Ask the user to choose an option.
                Console.WriteLine("\t 1. Staff Login");
                Console.WriteLine("\t 2. Member Login");
                Console.WriteLine("\t 0. Exit\n");
                Console.Write("Enter your choice ==> (1/2/0)\n");


                // Use a switch statement to choose wich menu.
                switch (Console.ReadLine())
                {
                    case "1":
                        // Staff Menu
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

            // TODO : check is user is in the member collection

            return true;
        }

        /** Display the staff Menu
        */
        public static void DisplayStaffMenu()
        {

            bool backHome = false;

            while(!backHome){
                StaffSystem staffSystem = new StaffSystem();
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
                        staffSystem.AddDVDs(Console.ReadLine());
                        break;
                    case "2":
                        Console.WriteLine("Enter the title of the movie:");
                        staffSystem.RemoveDVDs(Console.ReadLine());
                        break;
                    case "3":
                        staffSystem.RegisterNewMember();
                        break;
                    case "4":
                        staffSystem.RemoveMember();
                        break;
                    case "5":
                        Console.WriteLine("The contact number of the member is :" + staffSystem.DisplayPhoneNumber());
                        break;
                    case "6":
                        Console.WriteLine("Enter the title of the movie:");
                        Console.WriteLine(staffSystem.DisplayMembers(Console.ReadLine()));
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

            while(!backHome){

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
                /*switch (Console.ReadLine())
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
    }

    class StaffSystem
    {
        
        public void AddDVDs(string movie_title)
        {
            //Search is the movie is new or not
            IMovie movie_reference = Globals.movies.Search(movie_title);
            if (movie_reference!=null) //the movie is not new
            {
                //Add the new DVDs
                Console.WriteLine("How many DVDs do you want to add ?");
                int newDVDs = Convert.ToInt32(Console.ReadLine());
                movie_reference.AvailableCopies += newDVDs;
                movie_reference.TotalCopies += newDVDs;

            } else { //the movie is new, we need to register the movie in the system
                //Ask the user to fill the imformations about the movie
                Console.WriteLine("The movie is new, please enter the informations about the movie") ;
                IMovie new_movie = new Movie(movie_title);
                Console.WriteLine("Enter the genre of the movie:");//genre
                new_movie.Genre = Console.ReadLine();
                Console.WriteLine("Enter the classification of the movie:");//classification
                new_movie.Classification = Console.ReadLine();
                Console.WriteLine("Enter the duration of the movie:");//duration
                new_movie.Duration = Convert.ToInt32(Console.ReadLine());

                //Add the new DVDs
                Console.WriteLine("How many DVDs do you want to add ?");
                int newDVDs = Convert.ToInt32(Console.ReadLine());
                movie_reference.AvailableCopies += newDVDs;
                movie_reference.TotalCopies += newDVDs;

                //Add the new movie in the movie Collection
                if (Globals.movies.Insert(new_movie)) Console.WriteLine("The movie has been added to the collection!");
                else Console.WriteLine("The movie cannot be add to the collection, please try again");
            }
        }

        public void RemoveDVDs(string movie_title)
        {
            //Search is the movie is in the collection
            IMovie movie_reference = Globals.movies.Search(movie_title);
            if (movie_reference!=null) //the movie is not new
            {
                
                Console.WriteLine("How many DVDs do you want to remove?");
                int DVDs_to_remove = Convert.ToInt32(Console.ReadLine());
                if (DVDs_to_remove>movie_reference.TotalCopies)
                //no more DVDs, we remove the movie from the collection
                {
                    if (Globals.movies.Delete(movie_reference))
                    {
                        Console.WriteLine("All the DVDs of this movie have been removed, this movie has been removed from the collection");
                    }
                } else //Remove the new DVDs
                {
                    movie_reference.AvailableCopies -= DVDs_to_remove;
                    movie_reference.TotalCopies -= DVDs_to_remove;
                }
                

            } else { //the movie is not in the collection
                Console.WriteLine("The movie is not in the collection, please verify the title and try again") ;
            }
        }

        public void RegisterNewMember()
        {
            string first_name, last_name;
            Console.WriteLine("Please enter the following informations about the new member:");
            // fill first name
            Console.WriteLine("First name:");
            first_name = Console.ReadLine();
            //fill last name
            Console.WriteLine("Last name:");
            last_name = Console.ReadLine();
            IMember new_member = new Member(first_name, last_name);
            //check if the member is new or not
            if (Globals.members.Search(new_member)) Console.WriteLine("The member is already registered!");
            else //the member is new, we add him in the collection
            {
                string contact_number, psw;
                // fill contact number
                Console.WriteLine("Contact Number:");
                contact_number = Console.ReadLine();
                while(!IMember.IsValidContactNumber(contact_number)) //check if the contact number is valid
                {
                    Console.WriteLine("The contact number is not valid, please enter a valid contact number:");
                    contact_number = Console.ReadLine();
                }
                // fill password
                Console.WriteLine("Password:");
                psw = Console.ReadLine();
                while(!IMember.IsValidPin(psw)) //check if the password is valid
                {
                    Console.WriteLine("The password is not valid, please enter a valid password:");
                    psw = Console.ReadLine();
                }

                new_member.ContactNumber = contact_number;
                new_member.Pin = psw;
                //Add the member in the collection
                Globals.members.Add(new_member);
            }
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
        
        public string DisplayMembers(string movie_title)
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
            string list = "";
            //TODO
            return list;
        }

        public string DisplayTop3()
        {
            string top3 = "";
            //TODO
            return top3;
        }
    }
}