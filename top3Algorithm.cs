// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

// This file contains a test version of the algorithm used for DisplayTop3() that functions the same way. This file is for testing purposes to
// analyse the efficiency of the algorithm implemented.
    
using System;
using System. Linq;
using System.Diagnostics;

public class Top3Algorithm
{
    public static int[] FindTop3(int[] randomArray)
    {
        var ST = new Stopwatch();
        ST.Start();
        int[] top3;
        int first, second, third;
        //Basic case: empty array
        if (randomArray.Length == 0) {
            Console.WriteLine("Array empty");
            top3 = new int[] {};
        } else if (randomArray.Length == 1){
            top3 = new int[] {randomArray[0]}; 
        } else if (randomArray.Length == 2){
            if (randomArray[0] > randomArray[1]) {
                first = randomArray[0];
                second = randomArray[1];
            } else {
                first = randomArray[1];
                second = randomArray[0];
            }
            top3 = new int[] {first, second}; 
        } 
        else { 
            
            int min = randomArray.Min();
            first = min; second = min; third = min;
            foreach (int element in randomArray){
                if (element > first){
                    third = second;
                    second = first;
                    first = element;
                } else if (element > second){
                    third = second;
                    second = element;
                } else if (element > third){
                    third = element;
                }
            }
            top3 = new int[] {first, second, third};
            ST.Stop();

            Console.WriteLine(randomArray.Length + " Length");
            Console.WriteLine(ST.ElapsedMilliseconds + " Milliseconds");
        }
        return top3;
    }
    public static int[] RandomIntArray(int arrayLength)
    {
        Random random = new Random();
        int[] array = new int[arrayLength];
        for (int i = 0; i < array.Length; i++) {
            array[i] = random.Next();
        }
        return array;
    }

    public static void Main(string[] args)
    {
        int[] array = {2,6,4,1,52,8,52,2,4,5,22,525,5,55,5,52,54};
        
        Console.WriteLine("Test 1");
        FindTop3(RandomIntArray(250000));
        FindTop3(RandomIntArray(250000));
        FindTop3(RandomIntArray(250000));
        Console.WriteLine("Test 2");
        FindTop3(RandomIntArray(500000));
        FindTop3(RandomIntArray(500000));
        FindTop3(RandomIntArray(500000));
        Console.WriteLine("Test 3");
        FindTop3(RandomIntArray(750000));
        FindTop3(RandomIntArray(750000));
        FindTop3(RandomIntArray(750000));
        Console.WriteLine("Test 4");
        FindTop3(RandomIntArray(1000000));
        FindTop3(RandomIntArray(1000000));
        FindTop3(RandomIntArray(1000000));
        Console.WriteLine("Test 5");
        FindTop3(RandomIntArray(2500000));
        FindTop3(RandomIntArray(2500000));
        FindTop3(RandomIntArray(2500000));
        Console.WriteLine("Test 6");
        FindTop3(RandomIntArray(5000000));
        FindTop3(RandomIntArray(5000000));
        FindTop3(RandomIntArray(5000000));
        Console.WriteLine("Test 7");
        FindTop3(RandomIntArray(7500000));
        FindTop3(RandomIntArray(7500000));
        FindTop3(RandomIntArray(7500000));
        Console.WriteLine("Test 8");
        FindTop3(RandomIntArray(10000000));
        FindTop3(RandomIntArray(10000000));
        FindTop3(RandomIntArray(10000000));
        Console.WriteLine("Test 9");
        FindTop3(RandomIntArray(25000000));
        FindTop3(RandomIntArray(25000000));
        FindTop3(RandomIntArray(25000000));
        Console.WriteLine("Test 10");
        FindTop3(RandomIntArray(50000000));
        FindTop3(RandomIntArray(50000000));
        FindTop3(RandomIntArray(50000000));
        Console.WriteLine("Test 11");
        FindTop3(RandomIntArray(75000000));
        FindTop3(RandomIntArray(75000000));
        FindTop3(RandomIntArray(75000000));
        Console.WriteLine("Test 12");
        FindTop3(RandomIntArray(100000000));
        FindTop3(RandomIntArray(100000000));
        FindTop3(RandomIntArray(100000000));
       
    }
}