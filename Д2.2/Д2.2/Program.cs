using System;
using System.Diagnostics;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Тестирование факториалов:");
        int factorialSlowdownIndex = TestFactorialPerformance();
        Console.WriteLine($"\nрекурсивный метод стал заметно медленнее с индекса {factorialSlowdownIndex}");

        Console.WriteLine("\nТестирование последовательности Фибоначчи:");
        int fibonacciSlowdownIndex = TestFibonacciPerformance();
        Console.WriteLine($"\nрекурсивный метод стал заметно медленнее с индекса {fibonacciSlowdownIndex}");
    }

    private static int TestFactorialPerformance()
    {
        int slowdownIndex = 0;

        for (int i = 500; i <= 5000; i += 500)
        {
            var stopwatchIterative = Stopwatch.StartNew();
            BigInteger iterativeResult = FactorialIterative(i);
            stopwatchIterative.Stop();

            var stopwatchRecursive = Stopwatch.StartNew();
            BigInteger recursiveResult = FactorialRecursive(i);
            stopwatchRecursive.Stop();

            if (iterativeResult != recursiveResult)
                throw new Exception($"результаты отличаются при i={i}");

            Console.Write($"итерационный метод для {i}:  ");
            Console.WriteLine($"\tвремя выполнения: {stopwatchIterative.ElapsedMilliseconds} мс");

            Console.Write($"рекурсивный метод для {i}:  ");
            Console.WriteLine($"\tвремя выполнения: {stopwatchRecursive.ElapsedMilliseconds} мс");

            if (slowdownIndex == 0 && stopwatchRecursive.ElapsedMilliseconds - stopwatchIterative.ElapsedMilliseconds >=2)
            {
                slowdownIndex = i;
            }
            
        }
        return slowdownIndex;
    }

    private static int TestFibonacciPerformance()
    {
        int slowdownIndex = 0;

        for (int i = 0; i <= 32; i += 4)
        {
            var stopwatchIterative = Stopwatch.StartNew();
            BigInteger iterativeResult = FibonacciIterative(i);
            stopwatchIterative.Stop();

            var stopwatchRecursive = Stopwatch.StartNew();
            BigInteger recursiveResult = FibonacciRecursive(i);
            stopwatchRecursive.Stop();

            if (iterativeResult != recursiveResult)
                throw new Exception($"результаты отличаются при i={i}");

            Console.Write($"итерационный метод для {i}:  ");
            Console.WriteLine($"\tвремя выполнения: {stopwatchIterative.ElapsedMilliseconds} мс");
            Console.Write($"рекурсивный метод для {i}:  ");
            Console.WriteLine($"\tвремя выполнения: {stopwatchRecursive.ElapsedMilliseconds} мс");

            if (slowdownIndex == 0 && stopwatchRecursive.ElapsedMilliseconds - stopwatchIterative.ElapsedMilliseconds >= 2)
            {
                slowdownIndex = i;
            }
        }

        return slowdownIndex;
    }

    public static BigInteger FactorialRecursive(int n)
    {
        if (n == 0 || n == 1)
        {
            return 1;
        }

        else
        {
            return n * FactorialRecursive(n - 1);
        }
    }

    public static BigInteger FactorialIterative(int n)
    {
        BigInteger result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static BigInteger FibonacciRecursive(int n)
    {
        if (n == 0)
        {
            return 0;
        }
            
        if (n == 1) 
        {
            return 1;
        }

        return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
    }
    public static BigInteger FibonacciIterative(int n)
    {
        if (n == 0)
            return 0;
        if (n == 1)
            return 1;

        BigInteger a = 0;
        BigInteger b = 1;
        BigInteger temp;

        for (int i = 2; i <= n; i++)
        {
            temp = a + b;
            a = b;
            b = temp;
        }

        return b;
    }
}