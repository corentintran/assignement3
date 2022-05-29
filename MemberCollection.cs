//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


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
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }


}

