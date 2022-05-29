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
		return root == null;
	}

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if (root == null) {
			root = new BTreeNode(movie);
			count++;
			return true;
		}
		else if (Search(movie)) return false;
		else {
			Insert(movie, root);
			count++;
			return true;
		}
	}

	// Insert a movie on a BTreeNode
	// Post-condition: if the BTree node is empty the movie is set on this node
	// otherwise, the movie is inserted on the left child node or the right child note
	// according the lexicographic order 
	private void Insert(IMovie movie, BTreeNode node)
	{
		if (node == null) node = new BTreeNode(movie);
		else {
			if (movie.CompareTo(node.Movie) < 0) {
				if (node.LChild == null) node.LChild = new BTreeNode(movie);
				else Insert(movie, node.LChild);
			} else {
				if (node.RChild == null) node.RChild = new BTreeNode(movie);
				else Insert(movie, node.RChild);
			}
		}
	}

	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		BTreeNode node = root;
		BTreeNode parent = null;
		while (node != null && movie.CompareTo(node.Movie) != 0){
			parent = node;
			if (movie.CompareTo(node.Movie) < 0){ // move to left subtree
				node = node.LChild;
			} else { //move to right subtree
				node = node.RChild;
			} 
		}
		if (node != null){ //the search has been successful
			// case 1 node has 2 children
			if (node.LChild != null && node.RChild != null){
				if (node.LChild.RChild == null){
					node.Movie = node.LChild.Movie;
					node.LChild = node.LChild.LChild;
				} else {
					BTreeNode p = node.LChild;
					BTreeNode pp = node; //parent of p
					while (p.RChild != null){
						pp = p;
						p = p.RChild;
					}
					node.Movie = p.Movie;
					pp.RChild = p.LChild; // delete right-most node

				}
			} else { // case 2 node has no or only one child
				BTreeNode c = null;
				if (node.LChild != null) c = node.LChild;
				else c = node.RChild;

				if (node == root) root = c;
				else {
					if (node == parent.LChild){
						parent.LChild = c;
					} else {
						parent.RChild = c;
					}
				}
			}
			count--;
			return true;
		}
		return false;
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		return Search(movie, root);
	}

	private bool Search(IMovie movie, BTreeNode node)
	{
		if (node != null){
			int compareMovie = movie.CompareTo(node.Movie);
			if (compareMovie == 0) return true;
			else if (compareMovie < 0) {
				return Search(movie, node.LChild);
			} else {
				return Search(movie, node.RChild);
			}
		} else {
			return false;
		}
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		return Search(movietitle, root);
	}

	private IMovie Search(string movietitle, BTreeNode node)
	{
		if (node != null){
			int compareMovie = movietitle.CompareTo(node.Movie.Title);
			if (compareMovie == 0) return node.Movie;
			else if (compareMovie < 0) {
				return Search(movietitle, node.LChild);
			} else {
				return Search(movietitle, node.RChild);
			}
		} else {
			return null;
		}
	}

	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		//To be completed
		IMovie[] array = new IMovie[count];
		int index = 0;
		int finalindex = ToArray(array, root, index);
		if (finalindex != count) Console.WriteLine("error in ToArray");
		return array; 
	}

	// Store all the movie from a BTree Node node to an IMovie Array array
	// Return the index updated of hoow many movie have been added to the array
	private int ToArray(IMovie[] array, BTreeNode node, int index)
	{
		if (node == null) return index;
		else {
			int newindex = index;
			if (node.LChild != null){
				newindex = ToArray(array, node.LChild, index);
			}
			array[newindex] = node.Movie;
			newindex++;
			if (node.RChild != null){
				newindex = ToArray(array, node.RChild, newindex);
			}
			return newindex;	
		}
	}


	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		root = null;
		count = 0;
	}
}





