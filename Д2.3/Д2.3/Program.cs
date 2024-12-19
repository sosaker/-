using System;

class Program
{
    static int CountSteps(int n)
    {
        int count = 0;
        while (n != 1)
        {
            if (n % 2 == 0)
                n /= 2;
            else
                n = 3 * n + 1;
            count++;
        }
        return count;
    }

    static void Main()
    {
        Console.Write("число n: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int steps = CountSteps(n);
        Console.WriteLine($"требуется {steps} замен для числа {n}  для достижения 1");
    }
}