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
