using System;
class Program
{
    static void Main()
    {
        //ввод размера массива
        Console.Write("размер массива: ");
        int size = Convert.ToInt32(Console.ReadLine());
        //создание массива из случайных чисел
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(-200,200);
        }

        Console.WriteLine("\n изначальный массив:");
        PrintArray(array);
        //пузырьковая сортировка
        BubbleSort(array);
        Console.WriteLine("\n пузырьковая сортировка массива:");
        PrintArray(array);
    }

    static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    static void BubbleSort(int[] arr)
    {
        int temp;
        for (int i = 0; i < arr.Length - 1; i++) 
        {
            for (int j = 0; j < arr.Length - 1 - i; j++) 
            {
                if (arr[j] > arr[j + 1]) 
                {
                    temp = arr[j];       
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }
}