class MemberMenu
{
    public void DisplayAllMovies()
    {
        foreach (IMovie movie in Globals.allMovies.ToArray())
        {
            Console.WriteLine(movie.ToString());
        }
        return;
    }

    public string DisplayInfo(string movietitle)
    {
        string movie_info = "";
        IMovie movie = Globals.allMovies.Search(movietitle);
        if(movie != null) movie_info = movie.ToString();
        else movie_info = "There is currently no movie of that name in the library.";
        return movie_info;
    }

    public bool BorrowDVD(string movietitle)
    {
        IMovie movie = Globals.allMovies.Search(movietitle);
        if(movie != null)
        {
            movie.AddBorrower(Globals.currentUser);
            Globals.currentUser.Borrowings.Insert(movie);
            return true;
        }
        else
        {
            Console.WriteLine("The movie is not in the collection, please check the title");
            return false;
        }  
    }

    public bool ReturnDVD(string movietitle)
    {
        IMovie movie = Globals.allMovies.Search(movietitle);
        if (movie != null)
        {
            movie.RemoveBorrower(Globals.currentUser);
            Globals.currentUser.Borrowings.Delete(movie);
            return true;
        }
        else
        {
            Console.WriteLine("The movie is not in the collection, please check the title");
            return false;
        }
    }

    public void ListMoviesBorrowed()
    {
        if (Globals.currentUser.Borrowings.IsEmpty()) Console.WriteLine("You don't have any movie on loan");
        foreach (IMovie movie in Globals.currentUser.Borrowings.ToArray())
        {
            Console.WriteLine(movie.Title);
        }
        return;
    }

    public void DisplayTop3()
    {
        IMovie[] top3 = new IMovie[3];
        IMovie[] movies_array = Globals.allMovies.ToArray();
        IMovie first, second, third = null;
        //Basic case: no movie in the collection
        if (movies_array.Length == 0) {
            Console.WriteLine("There is no movie registered in the collection");
        }
        else { 
            foreach (IMovie m in movies_array){
                if (first == null || m.NoBorrowings > first.NoBorrowings){
                    third = second;
                    second = first;
                    first = m;
                } else if (second == null || m.NoBorrowings > second.NoBorrowings){
                    third = second;
                    second = m;
                } else if (third == null || m.NoBorrowings > third.NoBorrowings){
                    third = m;
                }
            }
            top3 = {first, second, third};
            //Display title and frequency of borrowings
            Console.WriteLine("Top 3 movies :");
            foreach (IMovie m in top3){
                if (m!=null){
                    Console.WriteLine(m.Title);
                    Console.WriteLine("Borrowed " + m.NoBorrowings + " times\n");
                }
            }
        }
        return;
    }
}
