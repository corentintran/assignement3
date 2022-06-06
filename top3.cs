// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler
    
using System;
using System. Linq;

public class Top3
{
    public static int[] FindTop3(int[] randomArray)
    {
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
        }
        return top3;
    }
    public static void Main(string[] args)
    {
        int[] array = {2,6,4,1,52,8,52,2,4,5,22,525,5,55,5,52,54};
        foreach (int top in FindTop3(array)){
            Console.WriteLine(top+"\n");
        }
    }
}