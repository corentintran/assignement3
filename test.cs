

using System;
using System.Collections.Generic;
using System.Text;
using CAB301_Assignment3;
//CAB301 assessment 1 - 2022
//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
//CAB301 assessment 2 - 2022
// Phase 2
// An implementation of MovieCollection ADT
// 2022


static class Globals
{
    public static IMemberCollection allMembers;
    public static IMovieCollection allMovies;
    public static IMember currentUser;
}


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









public class Movie : IMovie
{
    private string title;  // the titleof this movie
    private MovieGenre genre;  // the genre of this movie
    private MovieClassification classification; // the classification of this movie
    private int duration; // the duration of this movie in minutes
    private int availablecopies; // the number of copies that are currently available in the library
    private int totalcopies; // the total number of copies of this movie
    private int noborrows; // the number of times this movie has been borrowed so far
    private IMemberCollection borrowers;  // a collection of members that are currently borrowing a copy of this movie


    // a constructor 
    public Movie(string t, MovieGenre g, MovieClassification c, int d, int n)
    {
        title = t;
        genre = g;
        classification = c;
        duration = d;
        availablecopies = n;
        totalcopies = n;
        noborrows = 0;    
        borrowers = new MemberCollection(10);
    }

    // another constructor
    public Movie(string t)
    {
        title = t;
    }

    // get and set the tile of this movie
    public string Title { get { return title; } set { title = value; } }

    //get and set the genre of this movie
    public MovieGenre Genre { get { return genre; } set { genre = value; } }

    //get and set the classification of this movie
    public MovieClassification Classification { get { return classification; } set { classification = value; } }

    //get and set the duration of this movie
    public int Duration { get { return duration; } set { duration = value;  } }

    //get and set the number of DVDs of this movie currently available in the library
    public int AvailableCopies { get { return availablecopies; } set { availablecopies = value;  } }

    //get and set the total number of DVDs of this movie in the library
    public int TotalCopies { get { return totalcopies; } set { totalcopies = value;  } }

    //get and set the number of times that this movie has been borrowed so far
    public int NoBorrowings { get { return noborrows; } set { noborrows = value; } }

    //get all the members who are currently holding this movie
    public IMemberCollection Borrowers { get { return borrowers; } set { borrowers = value; } }


    //Add a member to the borrowers list of this movie
    //Pre-condition: number of available copies is greater than or equals to 1 
    //Post-condition:   if the member is not in the borrowers list, add the member to the borrower list,
    //                  number of available copies decreases by one, number of borrowed times increases by one, and return true;
    //                  if the member is in the borrowers list, do not add the member to the borrowers list and return false.  
    public bool AddBorrower(IMember member)
    {
        //To be completed 
        if (availablecopies >= 1) {
            if (!borrowers.Search(member)){
            //the member was not in the borrowers list we had the member
                borrowers.Add(member);
                availablecopies--;
                noborrows++;
                return true;
            } else {
            //the member is already in the borrowers list
                return false;
            }
        } else {
            Console.WriteLine("no available copies");
            return false;
        }
    }

    //Remove a member from the borrower list of this movie
    //Pre-condition:    nil 
    //Post-condition:   if the member is in the borrowers list, the member is removed from the borrowers list,
    //                  number of available copies increases by one, and return true;
    //                  otherwise, return false.
    public bool RemoveBorrower(IMember member)
    {
        int totalBorrowers = borrowers.Number;
        if (borrowers.Search(member)){
        // the member is in the list
            borrowers.Delete(member);
            availablecopies++;
            return true;
        } else return false; //the member is not in the list
    }

    //Define how to comapre two Movie objects
    //This movie's title is compared to another movie's title 
    //Pre-condition: nil
    //Post-condition:  return -1, if this movie's title is less than another movie's title by dictionary order
    //                 return 0, if this movie's title equals to another movie's title by dictionary order
    //                 return +1, if this movie's title is greater than another movie's title by dictionary order
    public int CompareTo(IMovie another)
    {
        int compareTitles = title.CompareTo(another.Title);
        if ( compareTitles < 0) {
            return -1;
        }
        else if (compareTitles == 0) {
            return 0;
        }
        else return 1;
    }

    //Return a string containing the title, genre, classification, duration, and the number of copies of this movie currently in the  library 
    //Pre-condition: nil
    //Post-condition: A string containing the title, genre, classification, duration, and the number of available copies of this movie has been returned
    public override string ToString()
    {
        string s = title + "\n" + genre + "\n" + classification + "\n" + duration + "\n" + availablecopies + "\n";
        return s;
    }
}



public class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private IMember[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new IMember[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        
        if (IsFull()) Console.WriteLine("Cannot add member, list is full");

        else if (IsEmpty()) {
            count++;
            members[0] = member;
        }
        
        else {

            bool memberAdded = false;
            bool duplicate = false;
            int i = 0;

            while (i<count && !memberAdded) {

                int compareMember = member.CompareTo(members[i]);
                if  (compareMember < 0) { 
                //member has to be placed before members[i]
                    memberAdded = true; //we get out the while loop

                } else if (compareMember == 0) { 
                //the members are similar => duplicate
                    i = count; //we get out the while loop
                    duplicate = true;

                } else i++;  
                //member hast to be placed after members [i]
            }
            
            if (duplicate) Console.WriteLine("the member is already in the list");
            else { //the member position should be i
                for (int j = count; j>i; j--) {
                    members[j] = members[j-1];
                }
                members[i] = member; //the member is inserted in the right position
                count++; // we update count
            }
            
        } 
        
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        bool memberDeleted = false;

        for (int i=0; i<count-1; i++) {
            if (memberDeleted) members[i] = members[i+1];
            else if (aMember.CompareTo(members[i]) == 0){
                memberDeleted = true;
                members[i] = members[i+1];
            }
        }
        if (!memberDeleted ) {
            if (aMember.CompareTo(members[count-1]) == 0){
            // the case where the member to delete is the last one
                memberDeleted = true;
            } else Console.WriteLine("cannot delete member, the member is not in the collection");
            // the member to delete is not in the member collection
        }
        if (memberDeleted) {
            members[count-1] = null;
            count = count-1;
        }
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        if (IsEmpty()) return false;
        
        int start = 0;
        int end = count-1;

        int m = (int) start + (end-start)/2;
        while (start<end-1){
            int compareMember = member.CompareTo(members[m]);
            if ( compareMember == 0){
                return true;
            }
            else {
                if (compareMember< 0){
                    end = m;
                } else start = m;
                m = (int) start + (end-start)/2;
            }
        }
        //there are just 2 elements left, we test the 2 elements
        if (member.CompareTo(members[start])==0) return true;
        if (member.CompareTo(members[end])==0) return true;
        return false;
    }

    

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public override string  ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }

    
    // Find a given member in this member collection
    // Pre-condition: nil
    // Post-condition: return the reference of the member object in the member collection, if this
    // member is in the member collection; return null otherwise; member collection remains unchanged
    public IMember Find(IMember member)
    {
        if (IsEmpty()) return null;
        
        int start = 0;
        int end = count-1;

        int m = (int) start + (end-start)/2;
        while (start<end-1){
            int compareMember = member.CompareTo(members[m]);
            if ( compareMember == 0){
                return members[m];
            }
            else {
                if (compareMember< 0){
                    end = m;
                } else start = m;
                m = (int) start + (end-start)/2;
            }
        }
        //there are just 2 elements left, we test the 2 elements
        if (member.CompareTo(members[start])==0) return members[start];
        if (member.CompareTo(members[end])==0) return members[end];
        return null;
    }



}


public class Member : IMember
{
    // Fields
    private string firstName;
    private string lastName;
    private string contactNumber;
    private string pin;
    private IMovieCollection borrowings;


    // Properties
    public string FirstName { get { return firstName; } set { firstName = value; } }  // Get and set the first name of this member
    public string LastName { get { return lastName; } set { lastName = value; } }  // Get and set the last name of this member
    public string ContactNumber { get { return contactNumber; } set { contactNumber = value; } }  // Get and set the contact number of this member
    public string Pin { get { return pin; } set { pin = value; } }// Get and set a pin number
    public IMovieCollection Borrowings { get { return borrowings; } set { borrowings = value; } }// Get and set the borrowing movies



    // Constructor with member's first name and lastname
    public Member(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.borrowings = new MovieCollection();
    }

    // Constructor with member's full details
    public Member(string firstName, string lastName, string contactNumber, string pin)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.contactNumber = contactNumber;
        this.pin = pin;
        this.borrowings = new MovieCollection();
    }



    // Define how to comapre two member objects
    // This member's full name is compared to another member's full name 
    // Pre-condition: nil
    // Post-condition: return -1 if this member's full name is less than another's full name in dictionary order
    //                 return 0, if this member's full name equals to another's full name in dictionary order
    //                 return +1, of this member's full name is greater than another's full name in dictionary order
    public int CompareTo(IMember member)
    {
        Member another = (Member)member;
        if (this.LastName.CompareTo(another.LastName) < 0)
        {
            return -1;
        }
        else
            if (this.LastName.CompareTo(another.LastName) == 0)
        {
            return this.FirstName.CompareTo(another.FirstName);
        }
        else
            return 1;
    }



    // Return a string containing the first name, last name and contact number of this memeber
    // Pre-condition: nil
    // Post-condition: a  string containing the first name, last name, and contact number of this member is returned
    public override string ToString()
    {
        return lastName + ", " + firstName;
    }
}
   //CAB301 assessment 1 - 2022
//The specification of Member ADT

public interface IMember
{

    // Get and set the first name of this member
    public string FirstName  
    {
        get;
        set;
    }
    // Get and set the last name of this member
    public string LastName 
    {
        get;
        set;
    }

    // Get and set the contact number of this member
    // A valid contact phone number has 10 digits and its first digit is 0
    public string ContactNumber 
        {
            get;
            set; //contact number must be valid 
        }

    // Get and set a pin for this member
    // A pin is valid if it is a number which has a minimal of 4 and a maximal of 6 digits
    public string Pin 
    {
        get;
        set; //pin must be valid 
    }
  
  public IMovieCollection Borrowings 
        {
            get;
            set; //pin must be valid 
        }


    // Define how to comapre two member objects
    // This member's full name is compared to another member's full name 
    // Pre-condition: nil
    // Post-condition: return -1 if this member's full name is less than another's full name in dictionary order
    //                 return 0, if this member's full name equals to another's full name in dictionary order
    //                 return +1, of this member's full name is greater than another's full name in dictionary order
    public int CompareTo(IMember member);

        


    // Check if a contact phone number is valid. A contact phone number is valid if it has 10 digits and the first digit is 0.
    // Pre-condition: nil
    // Post-condition: return true, if the phone number id valid; retuns false otherwise.
    
    public static bool IsValidContactNumber(string phonenumber)
    {
    // To be implemented by students in Phase 1
        if(phonenumber.Length == 10 && phonenumber.StartsWith("0") == true)
        {
            foreach (char d in phonenumber)
            {
                if (d < '0' || d > '9')
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
        
    }

    // Check if a pin is valid. A pin is valid if it is a number which has a minimal of 4 and a maximal of 6 digits.
    // Pre-condition: nil
    // Post-condition: return true, if the pin valid; retuns false otherwise.
    public static bool IsValidPin(string pin)
    {
    // To be implemented by students in Phase 1
        if(pin.Length >= 4 && pin.Length <= 6)
        {
            foreach (char d in pin)
            {
                if (d < '0' || d > '9')
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
        
    }


    // Return a string containing the first name, last name and contact number of this memeber
    // Pre-condition: nil
    // Post-condition: a  string containing the first name, last name, and contact number of this member is returned
    public string ToString();  
}





//Movie genre type
public enum MovieGenre
{
    Action = 1,
    Comedy = 2,
    History = 3,
    Drama = 4,
    Western = 5
}


//Movie classification type 
public enum MovieClassification
{
    G = 1,
    PG = 2,
    M = 3,
    M15Plus = 4
}

public interface IMovie
{
    // get and set the tile of this movie
    string Title 
    {
        get;
        set;
    }

    //get and set the genre of this movie
    MovieGenre Genre 
    {
        get;
        set;
    }

    //get and set the classification of this movie
    MovieClassification Classification 
    {
        get;
        set;
    }

    //get and set the duration of this movie
    int Duration 
    {
        get;
        set;
    }

    //get the number of DVDs of this movie currently available in the library
    int AvailableCopies 
    {
        get;
        set;
    }

    //get and set the total number of DVDs of this movie in the library
    public int TotalCopies 
    {
        get;
        set;
    }

    //get and set the number of times that this movie has been borrowed so far
    int NoBorrowings 
    {
        get;
        set;
     }

    //get all the members who are currently holding this movie
    IMemberCollection Borrowers  
    {
        get;
    }

    //Add a member to the borrowers list of this movie
    //Pre-condition: number of available copies is greater than or equals to 1 
    //Post-condition:   if the member is not in the borrowers list, add the member to the borrower list,
    //                  number of available copies decreases by one, number of borrowed times increases by one, and return true;
    //                  if the member is in the borrowers list, do not add the member to the borrowers list and return false.      
    bool AddBorrower(IMember member);

    //Remove a member from the borrower list of this movie
    //Pre-condition:    nil 
    //Post-condition:   if the member is in the borrowers list, the member is removed from the borrowers list,
    //                  number of available copies increases by one, and return true;
    //                  otherwise, return false.
    bool RemoveBorrower(IMember member); 


    //Define how to comapre two Movie objects
    //This movie's title is compared to another movie's title 
    //Pre-condition: nil
    //Post-condition:  return -1, if this movie's title is less than another movie's title by dictionary order
    //                 return 0, if this movie's title equals to another movie's title by dictionary order
    //                 return +1, if this movie's title is greater than another movie's title by dictionary order
    public int CompareTo(IMovie another);

    //Return a string containing the title, genre, classification, duration, and the number of copies of this movie currently in the  library 
    //Pre-condition: nil
    //Post-condition: A string containing the title, genre, classification, duration, and the number of available copies of this movie has been returned
    string ToString();

}


//CAB301 project - Phase 2 
//The specification of MovieCollection ADT
//2022


// invariant: no duplicates in this movie collection
public interface IMovieCollection
{


	// get the number of (different) movies in this movie collection
	int Number 
	{
		get;
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	bool IsEmpty();


	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	bool Insert(IMovie movie);

	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	bool Delete(IMovie movie);

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	bool Search(IMovie movie);

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string title);


	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	IMovie[] ToArray();

	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	void Clear();

}




public interface IMemberCollection
{
    public int Capacity // get the capacity of this member collection 
    {
        get;
    }
    public int Number // get the number of members in this collection
    {
        get;
    }

    // Check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull();


    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty();


    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate has been added into this the member collection
    public void Add(IMember member);

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection and the remaining members remain sorted by their full name.
    public void Delete(IMember aMember);


    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged.
    public bool Search(IMember member);

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear();

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString();

    // Find a given member in this member collection
    // Pre-condition: nil
    // Post-condition: return the reference of the member object in the member collection, if this
    // member is in the member collection; return null otherwise; member collection remains unchanged
    public IMember Find(IMember member);


}


namespace CAB301_Assignment3
{

    class App
    {
        static void Main(string[] args)
        {

            Globals.allMembers = new MemberCollection(100);
            Globals.allMovies = new MovieCollection();
            //Tests
            Console.WriteLine(Globals.allMovies.Insert(new Movie("Superman Returns", MovieGenre.Action, MovieClassification.M, 120, 1)));
            Console.WriteLine(Globals.allMovies.Insert(new Movie("A", MovieGenre.Comedy, MovieClassification.PG, 100, 3)));
            Console.WriteLine(Globals.allMovies.Insert(new Movie("B")));
            Console.WriteLine(Globals.allMovies.Insert(new Movie("C")));
           
            bool endApp = false;

            while (!endApp)
            {
                Console.WriteLine("==============================================================");
                Console.WriteLine("Welcome to the community Library Movie DVD Management System");
                Console.WriteLine("==============================================================\n");

                // Main Menu
                Console.WriteLine("======================= Main Menu =========================\n");
                // Ask the user to choose an option.
                Console.WriteLine("\t 1. Staff Login");
                Console.WriteLine("\t 2. Member Login");
                Console.WriteLine("\t 0. Exit\n");
                Console.Write("Enter your choice ==> (1/2/0)\n");


                // Use a switch statement to choose which menu.
                switch (Console.ReadLine())
                {
                    case "1":
                        // Staff Menu
                        Console.Clear();
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
                Console.Clear();
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
            else
            {
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

            IMember login = new Member(firstname, lastname);
            Globals.currentUser = Globals.allMembers.Find(login);
            if (Globals.currentUser==null) {
                Console.WriteLine("The username or password are incorrect\n");
                return false;
            } else return true;
        }

        /** Display the staff Menu
        */
        public static void DisplayStaffMenu()
        {

            bool backHome = false;

            while (!backHome)
            {
                Console.Clear();
                StaffMenu staffMenu = new StaffMenu();
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
                        staffMenu.AddDVDs(Console.ReadLine());
                        break;
                    case "2":
                        Console.WriteLine("Enter the title of the movie:");
                        staffMenu.RemoveDVDs(Console.ReadLine());
                        break;
                    case "3":
                        if (staffMenu.RegisterNewMember()) Console.WriteLine("The member has been registered successfully!");
                        break;
                    case "4":
                        if (staffMenu.RemoveMember()) Console.WriteLine("The member has been removed successfully!");
                        break;
                    case "5":
                        Console.WriteLine(staffMenu.DisplayPhoneNumber());
                        break;
                    case "6":
                        Console.WriteLine("Enter the title of the movie:");
                        Console.WriteLine(staffMenu.DisplayMembers(Console.ReadLine()));
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

            while (!backHome)
            {

                Console.Clear();
                MemberMenu memberMenu = new MemberMenu();


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

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Listing all movies in the library\n");
                        memberMenu.DisplayAllMovies();
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter the title of the movie.\n");
                        string neededTitle = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Movie Information\n");
                        Console.WriteLine(memberMenu.DisplayInfo(neededTitle));
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter the title of the movie you wish to borrow.\n");
                        if (memberMenu.BorrowDVD(Console.ReadLine())) Console.WriteLine("Movie Borrowed.");
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter the title of the movie you wish to return.\n");
                        if (memberMenu.ReturnDVD(Console.ReadLine())) Console.WriteLine("Movie Returned.");
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "5":
                        Console.Clear();
                        memberMenu.ListMoviesBorrowed();
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine(memberMenu.DisplayTop3());
                        Console.WriteLine("\nPress a key to return to the member menu.");
                        Console.ReadLine();
                        break;
                    case "0":
                        backHome = true;
                        break;
                }
                
            }
        }
    }
}


class StaffMenu
{

    public void AddDVDs(string movie_title)
    {
        //Search is the movie is new or not
        IMovie movie_reference = Globals.allMovies.Search(movie_title);
        if (movie_reference != null) //the movie is not new
        {
            //Add the new DVDs
            Console.WriteLine("How many DVDs do you want to add ?");
            int newDVDs = Convert.ToInt32(Console.ReadLine());
            movie_reference.AvailableCopies += newDVDs;
            movie_reference.TotalCopies += newDVDs;

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
            if (Globals.allMovies.Insert(new_movie)) Console.WriteLine("The movie has been added to the collection!");
            else Console.WriteLine("The movie cannot be add to the collection, please try again");
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
            }
            else //Remove the new DVDs
            {
                movie_reference.AvailableCopies -= DVDs_to_remove;
                movie_reference.TotalCopies -= DVDs_to_remove;
            }


        }
        else
        { //the movie is not in the collection
            Console.WriteLine("The movie is not in the collection, please verify the title and try again");
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
        if (member_to_remove == null) {
            Console.WriteLine("The member is not registered");
            return false;
        } else {

            if (member_to_remove.Borrowings.IsEmpty())
            {
                //remove the member from the memberCollection
                Globals.allMembers.Delete(member_to_remove);
                return true;
            } else {
                Console.WriteLine("Impossible to remove member, he is currently renting a movie");
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
        if (result != null) phonenumber = result.ContactNumber;
        return phonenumber;
    }

    public string DisplayMembers(string movie_title)
    {
        string members_list = "";
        IMovie movie = Globals.allMovies.Search(movie_title);
        //The movie is not registered in the system
        if (movie == null) members_list = "The movie is not registered in the system, please check the title";
        else members_list = movie.Borrowers.ToString();
        return members_list;
    }
}


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
