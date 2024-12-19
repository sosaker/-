using System;

class Program
{
    static void Main()
    {
        double a, b, c;

        Console.Write("Введите коэффициент a: ");
        a = double.Parse(Console.ReadLine());

        if (a == 0)
        {
            Console.Write("Введите коэффициент b: ");
            b = double.Parse(Console.ReadLine());

            if (b == 0)
            {
                Console.Write("Введите коэффициент c: ");
                c = double.Parse(Console.ReadLine());

                if (c == 0)
                {
                    Console.WriteLine("Решение: x - любое число");
                }
                else
                {
                    Console.WriteLine("Нет решений");
                }
            }
            else
            {
                Console.Write("Введите коэффициент c: ");
                c = double.Parse(Console.ReadLine());

                SolveLinearEquation(b, c);
            }
        }
        else
        {
            Console.Write("Введите коэффициент b: ");
            b = double.Parse(Console.ReadLine());

            Console.Write("Введите коэффициент c: ");
            c = double.Parse(Console.ReadLine());

            SolveQuadraticEquation(a, b, c);
        }
    }

    static void SolveLinearEquation(double b, double c)
    {
        double solution = -c / b;
        Console.WriteLine($"Решение: x = {solution}");
    }

    static void SolveQuadraticEquation(double a, double b, double c)
    {
        double discriminant = b * b - 4 * a * c;

        if (discriminant > 0)
        {
            double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            Console.WriteLine($"Два решения: x1 = {root1}, x2 = {root2}");
        }
        else if (discriminant == 0)
        {
            double root = -b / (2 * a);
            Console.WriteLine($"Одно решение: x = {root}");
        }
        else
        {
            Console.WriteLine("Нет решений");
        }
    }
}