using CAB301_Assignment3;
using System;
class StaffMenu
{
    public void AddDVDs(string movie_title)
    {
        //Search is the movie is new or not
        IMovie movie_reference = Globals.allMovies.Search(movie_title);
        if (movie_reference != null) //the movie is not new
        {
            //Console output indicating the movie is already in the system and the total copies within the system.
            Console.WriteLine("The movie already exists in the system. Available copies: {1}, Total copies: {0}", movie_reference.TotalCopies, movie_reference.AvailableCopies);
            //Add the new DVDs
            Console.WriteLine("How many DVDs do you want to add?");

            int newDVDs = Convert.ToInt32(Console.ReadLine());
            movie_reference.AvailableCopies += newDVDs;
            movie_reference.TotalCopies += newDVDs;

            // Console output indicating the processing is a success
            Console.WriteLine("Successfully added {0} DVDs to {1}. Available copies: {3}, Total copies: {2}", newDVDs, movie_reference.Title, movie_reference.TotalCopies, movie_reference.AvailableCopies);
            Console.Write("Press enter to continue...");
            Console.Read();
        }
        else
        { //the movie is new, we need to register the movie in the system
          //Ask the user to fill the imformations about the movie
            Console.WriteLine("The movie is new, please enter the informations about the movie");
            IMovie new_movie = new Movie(movie_title);
            Console.WriteLine("Enter the genre of the movie:");//genre
            Console.WriteLine("\t1. Action");
            Console.WriteLine("\t2. Comedy");
            Console.WriteLine("\t3. History");
            Console.WriteLine("\t4. Drama");
            Console.WriteLine("\t5. Western");
            Console.WriteLine("Enter your choice ==> 1/2/3/4/5");
            new_movie.Genre = (MovieGenre)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the classification of the movie:");//classification
            Console.WriteLine("\t1. G");
            Console.WriteLine("\t2. PG");
            Console.WriteLine("\t3. M");
            Console.WriteLine("\t4. M15Plus");
            Console.WriteLine("Enter your choice ==> 1/2/3/4");
            new_movie.Classification = (MovieClassification)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the duration of the movie:");//duration
            new_movie.Duration = Convert.ToInt32(Console.ReadLine());
            //Add the new DVDs
            Console.WriteLine("How many DVDs do you want to add ?");
            int newDVDs = Convert.ToInt32(Console.ReadLine());
            new_movie.AvailableCopies = newDVDs;
            new_movie.TotalCopies = newDVDs;

            //Add the new movie in the movie Collection
            if (Globals.allMovies.Insert(new_movie))
            {
                Console.WriteLine("The movie has been added to the collection! Available copies: {0}, Total Copies: {1}", new_movie.AvailableCopies, new_movie.TotalCopies);
                Console.Write("Press enter to continue...");
                Console.Read();
            }
            else
            {
                Console.WriteLine("The movie cannot be added to the collection, please try again");
                Console.Write("Press enter to continue...");
                Console.Read();
            }


        }
    }

    public void RemoveDVDs(string movie_title)
    {
        //Search is the movie is in the collection
        IMovie movie_reference = Globals.allMovies.Search(movie_title);
        if (movie_reference != null) //the movie is not new
        {
            Console.WriteLine("How many DVDs do you want to remove?");
            int DVDs_to_remove = Convert.ToInt32(Console.ReadLine());
            if (DVDs_to_remove > movie_reference.TotalCopies)
            //no more DVDs, we remove the movie from the collection
            {
                if (Globals.allMovies.Delete(movie_reference))
                {
                    Console.WriteLine("All the DVDs of this movie have been removed, this movie has been removed from the collection");
                }
                Globals.allMovies.Delete(movie_reference);
                Console.WriteLine("All the DVDs of this movie have been removed, this movie has been removed from the collection");
                Console.Write("Press enter to continue...");
                Console.Read();
            }
            else //Remove the new DVDs
            {
                movie_reference.AvailableCopies -= DVDs_to_remove;
                movie_reference.TotalCopies -= DVDs_to_remove;
                Console.WriteLine("{0} DVDs have been removed from the movie: {1}. Available copies: {2}, Total Copies: {3}", DVDs_to_remove, movie_reference.Title, movie_reference.AvailableCopies, movie_reference.TotalCopies);
                Console.Write("Press enter to continue...");
                Console.Read();
            }


        }
        else
        { //the movie is not in the collection
            Console.WriteLine("The movie is not in the collection, please verify the title and try again");
            Console.Write("Press enter to continue...");
            Console.Read();
        }
    }

    public bool RegisterNewMember()
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
        if (Globals.allMembers.Search(new_member))
        {
            Console.WriteLine("The member is already registered!");
            Console.Write("Press enter to continue...");
            Console.Read();
            return false;
        }
        else //the member is new, we add him in the collection
        {
            string contact_number, psw;
            // fill contact number
            Console.WriteLine("Contact Number:");
            contact_number = Console.ReadLine();
            while (!IMember.IsValidContactNumber(contact_number)) //check if the contact number is valid
            {
                Console.WriteLine("The contact number is not valid, please enter a valid contact number:");
                contact_number = Console.ReadLine();
            }
            // fill password
            Console.WriteLine("Password:");
            psw = Console.ReadLine();
            while (!IMember.IsValidPin(psw)) //check if the password is valid
            {
                Console.WriteLine("The password is not valid, please enter a valid password:");
                psw = Console.ReadLine();
            }
            new_member.ContactNumber = contact_number;
            new_member.Pin = psw;
            //Add the member in the collection
            Globals.allMembers.Add(new_member);

            Console.WriteLine("{0} {1} has been registered as a member, with the contact number {2} and password {3}.", new_member.FirstName, new_member.LastName, new_member.ContactNumber, new_member.Pin);
            Console.Write("Press enter to continue...");
            Console.Read();
            return true;
        }
    }
    public bool RemoveMember()
    {
        string first_name, last_name;
        Console.WriteLine("Please enter the following informations about the new member:");
        Console.WriteLine("First name:");// fill first name
        first_name = Console.ReadLine();
        Console.WriteLine("Last name:");//fill last name
        last_name = Console.ReadLine();
        IMember m = new Member(first_name, last_name);
        IMember member_to_remove = Globals.allMembers.Find(m);
        if (member_to_remove == null)
        {
            Console.WriteLine("The member is not registered");
            Console.Write("Press enter to continue...");
            Console.Read();
            return false;
        }
        else
        {

            if (member_to_remove.Borrowings.IsEmpty())
            {
                //remove the member from the memberCollection
                Globals.allMembers.Delete(member_to_remove);
                Console.WriteLine("Member successfully removed.");
                Console.Write("Press enter to continue...");
                Console.Read();
                return true;
            }
            else
            {
                Console.Write("Removal unsuccessful, member is currently borrowing: ");
                foreach (IMovie movie in member_to_remove.Borrowings.ToArray()) Console.Write(movie.Title + " ");
                Console.WriteLine();
                Console.Write("Press enter to continue...");
                Console.Read();
                return false;
            }
        }

    }
    public string DisplayPhoneNumber()
    {
        string phonenumber = "";
        string first_name, last_name;
        Console.WriteLine("Please enter the following informations about the new member:");
        Console.WriteLine("First name:");// fill first name
        first_name = Console.ReadLine();
        Console.WriteLine("Last name:");//fill last name
        last_name = Console.ReadLine();
        // Create temp Member object to use methods which utilise Member.CompareTo()
        IMember temp = new Member(first_name, last_name);
        // Reference of
        IMember result = Globals.allMembers.Find(temp);
        if (result != null) phonenumber = "The contact number of the member is :" + result.ContactNumber;
        if (result != null) phonenumber = "The contact number of the member is: " + result.ContactNumber;
        else phonenumber = "The member is not registered";
        Console.WriteLine(phonenumber);
        Console.Write("Press enter to continue...");
        Console.Read();
        return phonenumber;
    }

    public string DisplayMembers(string movie_title)
    {
        string members_list = "";
        IMovie movie = Globals.allMovies.Search(movie_title);
        //The movie is not registered in the system
        if (movie == null) members_list = "The movie is not registered in the system, please check the title";
        else members_list = movie.Borrowers.ToString();
        if (movie == null)
        {
            members_list = "The movie is not registered in the system, please check the title";
        }
        else
        {
            members_list = movie.Borrowers.ToString();
        }
        Console.WriteLine(members_list);
        Console.Write("Press enter to continue...");
        Console.Read();
        return members_list;
    }
}