using System;

class StaffMenu
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
            string first_name, last_name;
            Console.WriteLine("Please enter the following informations about the new member:");
            Console.WriteLine("First name:");// fill first name
            first_name = Console.ReadLine();
            Console.WriteLine("Last name:");//fill last name
            last_name = Console.ReadLine();
            IMember member_to_remove = new Member(first_name, last_name);

            for IMovie m in Globals.movies {
                if (m.Borrowers.Search(member_to_remove)) { //the member has a movie DVD on loan
                    Console.WriteLine("The member has a movie DVD on loan and cannot be removed");
                    return false;
                }
            }
            
            //remove the member from the memberCollection
            Globals.members.Delete(member_to_remove);
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
            IMovie movie = Globals.movies.Search(movie_title);
            if (movie==null){
                Console.WriteLine("")
            }
            return members;
        }
    }
