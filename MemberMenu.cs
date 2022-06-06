class MemberMenu
{
    public void DisplayAllMovies()
    {
        string allmovies = "";
        //TODO
        foreach (IMovie movie in Globals.allMovies.ToArray())
        {
            Console.WriteLine(movie.ToString());
        }
        return;
    }

    public string DisplayInfo(string movietitle)
    {
        string movieinfos = "";
        //TODO
        IMovie movie = Globals.allMovies.Search(movietitle);
        if(movie != null)
        {
            Console.WriteLine(movie.ToString());
        }
        else
        {
            Console.WriteLine("There is currently no movie of that name in the library.");
        }
        return movieinfos;
    }

    public bool BorrowDVD(string movietitle, IMember borrower)
    {
        //TODO
        IMovie movie = Globals.allMovies.Search(movietitle);
        if(movie != null && borrower != null)
        {
            movie.AddBorrower(borrower);
            Console.WriteLine("Movie Borrowed.");
            return true;
        }
        else
        {
            return false;
        }  
    }

    public bool ReturnDVD(string movietitle, IMember borrower)
    {
        //TODO
        IMovie movie = Globals.allMovies.Search(movietitle);
        if (movie != null && borrower != null)
        {
            movie.RemoveBorrower(borrower);
            Console.WriteLine("Movie Returned.");
            return true;
        }
        else
        {
            return false;
        }
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
