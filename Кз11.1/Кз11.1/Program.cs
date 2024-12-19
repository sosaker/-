using System;
using System.IO;
class Program
{
    static void Main()
    {
        //объект StreamWriter для записи в файл
        using (StreamWriter writer = new StreamWriter("f.txt"))
        {
            //заголовок
            writer.WriteLine("x\tsin(x)");

            //вычисления значений sin(x)
            for (double x = 0; x <= 1; x += 0.1)
            {
                double sinX = Math.Sin(x);
                writer.WriteLine($"{x}\t{sinX}");
            }
        }
        Console.WriteLine("таблица значений записана в файл 'f.txt' (файл находится: к3 11.1\\bin\\Debug)");
    }
}