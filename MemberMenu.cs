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

    public string DisplayTop3()
    {
        string top3 = "";
        //TODO
        return top3;
    }

}
