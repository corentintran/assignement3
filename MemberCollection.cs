//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

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
            members = new Member[capacity];
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
        if(Search(member) == false)
        {
            // To be implemented by students in Phase 1
            if (!IsFull())
            {
                members[count] = (Member)member;
                count++;
            }
            // Sort List
            for (int i = 0; i < count - 1; i++)
            {
                for (int d = 0; d < count - i - 1; d++)
                {
                    if (members[d].CompareTo(members[d + 1]) > 0)
                    {
                        Member temp = (Member)members[d];
                        members[d] = members[d + 1];
                        members[d + 1] = temp;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("There is already a member with that name.");
        }
        
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
    // To be implemented by students in Phase 1
        //Find Member
        if(Search(aMember) == true)
        {
            //Console.WriteLine("A");
            for (int i = 0; i < members.Length; i++)
            {
                Member m = (Member)aMember;
                //Console.WriteLine("B");
                if (members[i].CompareTo(m) == 0)
                {
                    members[i] = members[count - 1];
                    count--;
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("There is no one in the list with that name.");
        }
        // Sort List
        for (int i = 0; i < count - 1; i++)
        {
            for (int d = 0; d < count - i - 1; d++)
            {
                if (members[d].CompareTo(members[d + 1]) > 0)
                {
                    Member temp = (Member)members[d];
                    members[d] = members[d + 1];
                    members[d + 1] = temp;
                }
            }
        }

    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        // To be implemented by students in Phase 1
        int low = 0;
        int high = count - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (member.CompareTo(members[mid]) == 0)
            {
                return true;
            }
            else if (member.CompareTo(members[mid]) < 0)
            {
                high = mid - 1;
            }
            else if (member.CompareTo(members[mid]) > 0)
            {
                low = mid + 1;
            }
        }
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

