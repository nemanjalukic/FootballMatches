using System;
using System.Runtime.InteropServices.ComTypes;

namespace LuckySix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 5, 2, 3, 2, 2, 4, 3 };
            int[] arr1 = { 1, 2, 5, 1, 3, 2, 1, 4, 3 };
            Console.WriteLine(luckySix(arr));
            Console.WriteLine(luckySix(arr1));
        }

        public static bool luckySix(int [] array)
        {
            bool t = false;
            if (array.Length < 3)
                return t;
            for(int i=0; i<array.Length-3;i++)
            {
                if ((array[i] + array[i + 1] + array[i + 2]) == 6)
                    return true;
            }
            
            
            return t;
        }
    }

   
}
