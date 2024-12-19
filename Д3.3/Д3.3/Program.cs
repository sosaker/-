using System;

class Program
{
    static void Main(string[] args)
    {
        
        Console.Write("действительная часть для c1: ");
        double realC1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("мнимая часть для c1: ");
        double imagC1 = Convert.ToDouble(Console.ReadLine());
        
        var c1 = new ComplexNumber(realC1, imagC1);
        
        Console.Write("действительная часть для c2: ");
        double realC2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("мнимая часть для c2: ");
        double imagC2 = Convert.ToDouble(Console.ReadLine());
        
        var c2 = new ComplexNumber(realC2, imagC2);

        Console.WriteLine($"c1 = {c1}");
        Console.WriteLine($"c2 = {c2}");

        
        var sum = c1 + c2;
        var difference = c1 - c2;
        var product = c1 * c2;
        var quotient = c1 / c2;

        Console.WriteLine($"сумма = {sum}");
        Console.WriteLine($"разность = {difference}");
        Console.WriteLine($"произведение = {product}");
        Console.WriteLine($"частное = {quotient}");
        Console.WriteLine($"модуль c1 = {c1.Magnitude()}");
        Console.WriteLine($"угол c1 = {c1.Angle()} радиан");
        Console.WriteLine($"квадратный корень из c1 = {c1.Sqrt()}");
        Console.WriteLine($"возведение c1 в куб = {c1.Pow(3)}");
    }
}

public class ComplexNumber
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    
    public ComplexNumber()
    {
        this.Real = 0;
        this.Imaginary = 0;
    }

    
    public ComplexNumber(double real, double imaginary)
    {
        this.Real = real;
        this.Imaginary = imaginary;
    }

    //сложение
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    //вычитание
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    //умножение
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        var real = a.Real * b.Real - a.Imaginary * b.Imaginary;
        var imag = a.Real * b.Imaginary + a.Imaginary * b.Real;
        return new ComplexNumber(real, imag);
    }

    //деление
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        if (b.Real == 0 && b.Imaginary == 0)
            throw new DivideByZeroException("Деление на ноль");

        var denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        var real = (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator;
        var imag = (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator;
        return new ComplexNumber(real, imag);
    }

    //возведение в степень
    public ComplexNumber Pow(int exponent)
    {
        if (exponent == 0)
            return new ComplexNumber(1, 0); 

        if (exponent == 1)
            return this; 

        if (exponent < 0)
            return new ComplexNumber(1, 0) / this.Pow(-exponent); 

        var result = this;
        for (int i = 1; i < exponent; i++)
            result *= this;
        return result;
    }

    //извлечение квадратного корня
    public ComplexNumber Sqrt()
    {
        var r = Math.Sqrt(this.Magnitude());
        var theta = this.Angle() / 2;
        return new ComplexNumber(r * Math.Cos(theta), r * Math.Sin(theta));
    }

    //модуль
    public double Magnitude()
    {
        return Math.Sqrt(this.Real * this.Real + this.Imaginary * this.Imaginary);
    }

    //угол компл. числа
    public double Angle()
    {
        return Math.Atan2(this.Imaginary, this.Real);
    }

    
    public override string ToString()
    {
        return $"{this.Real} + {this.Imaginary}i";
    }
}