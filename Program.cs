﻿using System;
using app.Matrix;

namespace program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Matrix2D Matrix1 = new Matrix2D(3, 3, 10);
            Matrix2D Matrix2 = Matrix1.Clone() as Matrix2D;

            Console.WriteLine($"Матрицы:\n{Matrix1}\n{Matrix2}\n");

            Console.WriteLine("Сравнение:");
            Console.WriteLine($"Matrix1 > Matrix2 = {Matrix1 > Matrix2}");
            Console.WriteLine($"Matrix1 >= Matrix2 = {Matrix1 >= Matrix2}");
            Console.WriteLine($"Matrix1 < Matrix2 = {Matrix1 < Matrix2}");
            Console.WriteLine($"Matrix1 <= Matrix2 = {Matrix1 <= Matrix2}\n");
            Console.WriteLine($"Matrix1 == Matrix2 = {Matrix1 == Matrix2}\n");

            Matrix2 += Matrix1;
            Console.WriteLine($"Сложение двух матриц:\n{Matrix2}\n");

            Matrix2 -= Matrix1;
            Console.WriteLine($"Вычитание двух матриц:\n{Matrix2}\n");

            Matrix2 *= Matrix1;
            Console.WriteLine($"Умножение двух матриц:\n{Matrix2}\n");

            Matrix2 *= 3;
            Console.WriteLine($"Умножение матрицы на число:\n{Matrix2}\n");

            Console.WriteLine($"Детерминанты:\n{Matrix1.GetDeterminant()}\n{Matrix2.GetDeterminant()}\n");

            Console.WriteLine("Сравнение (после всех операций):");
            Console.WriteLine($"Matrix1 > Matrix2 = {Matrix1 > Matrix2}");
            Console.WriteLine($"Matrix1 >= Matrix2 = {Matrix1 >= Matrix2}");
            Console.WriteLine($"Matrix1 < Matrix2 = {Matrix1 < Matrix2}");
            Console.WriteLine($"Matrix1 <= Matrix2 = {Matrix1 <= Matrix2}\n");
            Console.WriteLine($"Matrix1 == Matrix2 = {Matrix1 == Matrix2}\n");

            Console.WriteLine($"Обратная матрица (при отрицательном детерминанте просто выводит текущую матрицу):\n{Matrix1.Reverse()}\n");

            Console.ReadLine();
        }
    }

}
