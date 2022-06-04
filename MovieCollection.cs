// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		//To be completed
		return count == 0;
	}

	private bool InsertRecursive(Movie M, BTreeNode root)
    {
		if(M.CompareTo(root.Movie) == 0)
        {
			return false;
        }
        else if(M.CompareTo(root.Movie) < 0)
        {
			if(root.LChild == null)
            {
				root.LChild = new BTreeNode(M);
				count++;
            }
            else
            {
				InsertRecursive(M, root.LChild);
            }
        }
		else if(root.RChild == null)
        {
			root.RChild = new BTreeNode(M);
			count++;
        }
        else
        {
			InsertRecursive(M, root.RChild);
        }
		return true;
    }
	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		//To be completed
		if(root == null)
        {
			root = new BTreeNode(movie);
			count++;
			return true;
        }
		
		return InsertRecursive((Movie)movie, root);
	}


	
	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		//To be completed
		BTreeNode Ptr = root;
		BTreeNode Parent = null;
		BTreeNode P;
		BTreeNode PP;
		BTreeNode C;
		while (Ptr != null && movie.CompareTo(Ptr.Movie) != 0)
		{
			Parent = Ptr;
			if (movie.CompareTo(Ptr.Movie) < 0)
			{
				Ptr = Ptr.LChild;
				
			}
			else
			{
				Ptr = Ptr.RChild;
				
			}
		}
		if (Ptr != null)
		{
			if (Ptr.LChild != null && Ptr.RChild != null) // 2 Children
			{
				if (Ptr.LChild.RChild == null)
				{
					Ptr.Movie = Ptr.LChild.Movie;
					Ptr.LChild = Ptr.LChild.LChild;
					count--;
					return true;
					
				}
				else
				{
					P = Ptr.LChild;
					PP = Ptr;
					while (P.RChild != null)
					{
						PP = P;
						P = P.RChild;
					}
					Ptr.Movie = P.Movie;
					PP.RChild = P.LChild;
					count--;
					return true;
					
				}
			}
			else //No or 1 Child
			{
				if (Ptr.LChild != null)
                {
					C = Ptr.LChild;
					count--;
				}
                else 
				{
					C = Ptr.RChild;
					count--;
				}

				if (Ptr == root)
				{
					root = C;
					count--;
					return true;
					
				}
				else
				{
					if (Ptr == Parent.LChild)
                    {
						Parent.LChild = C;
					}
                    else
                    {
						Parent.RChild = C;
					}
					count--;
					return true;
					
				}

			}
		}
		return false;
		
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	private bool SearchRecursive(Movie M, BTreeNode root)
    {
		if(root != null)
        {
			if(M.CompareTo(root.Movie) == 0)
            {
				return true;
            }
            else if(M.CompareTo(root.Movie) < 0)
            {
				return SearchRecursive(M, root.RChild);
            }
            else
            {
				return SearchRecursive(M, root.LChild);
            }
        }
        else
        {
			return false;
        }
    }
	public bool Search(IMovie movie)
	{
		//To be completed
		return SearchRecursive((Movie)movie, root);
        
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.

	private IMovie SearchRecursiveString(String title, BTreeNode root)
	{
		if (root != null)
		{
			Movie M = new Movie(title);
			if (M.CompareTo(root.Movie) == 0)
			{
				return root.Movie;
			}
			else if (M.CompareTo(root.Movie) < 0)
			{
				return SearchRecursiveString(M.Title, root.RChild);
			}
			else
			{
				return SearchRecursiveString(M.Title, root.LChild);
			}
		}
		else
		{
			return null;
		}
	}
	public IMovie Search(string movietitle)
	{
		//To be completed
		return SearchRecursiveString(movietitle, root);
	}



	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		//To be completed
		IMovie[] Array = new IMovie[0];
		InOrderTraversal(root, Array);
		
		return Array.ToArray();
	}
	private void InOrderTraversal(BTreeNode root, IMovie[] Array)
    {
		if(root != null)
        {
			InOrderTraversal(root.LChild, Array);
			Console.WriteLine(root.Movie.ToString());
			InOrderTraversal(root.RChild, Array);
        }
    }



	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		//To be completed
		root = null;
		count = 0;
	}
}





