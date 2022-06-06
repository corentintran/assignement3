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
        /*
        IMovie[] top3 = new IMovie[3];
        IMovie[] movies_array = Globals.allMovies.ToArray();
        Imovie first = 

        //Basic case : no movie in the collection
        int no_movies = movies_array.Length;
        if (no_movies == 0) {
            Console.WriteLine("There is no movie registered in the collection");
        }
        else if (no_movies == 1) {
            
            
        }
        else {


        foreach (Imovie m in movies_array){
            if (m.NoBorrowings > first.NoBorrowings){
                third = second;
                second = first;
                first = m;
            } else if (m.NoBorrowings > second.NoBorrowings){
                third = second;
                second = m;
            } else if (m.NoBorrowings > third.NoBorrowings){
                third = m;
            }
        }


        top3[0] = first;
        top3[1] = second;
        top3[2] = third;
        foreach (Imovie m in top3){
            Console.WriteLine(movie.Title);
            Console.WriteLine("Borrowed " + movie.NoBorrowings + " times");
        }
        }*/
        return
    }

}
