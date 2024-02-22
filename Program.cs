using System;
using System.Collections.Generic;
using app.Matrix;

namespace program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Matrix2D Matrix1 = new Matrix2D(3, 3, 10);
            Matrix2D Matrix2 = Matrix1.Clone() as Matrix2D;

            Console.WriteLine($"�������:\n{Matrix1}\n{Matrix2}\n");

            Console.WriteLine($"�������� ������� (��� ������������� ������������ ������ ������� ������� �������):\n{Matrix1.Reverse()}\n");

            Console.WriteLine("���������:");
            Console.WriteLine($"Matrix1 > Matrix2 = {Matrix1 > Matrix2}");
            Console.WriteLine($"Matrix1 >= Matrix2 = {Matrix1 >= Matrix2}");
            Console.WriteLine($"Matrix1 < Matrix2 = {Matrix1 < Matrix2}");
            Console.WriteLine($"Matrix1 <= Matrix2 = {Matrix1 <= Matrix2}\n");
            Console.WriteLine($"Matrix1 == Matrix2 = {Matrix1 == Matrix2}\n");

            Matrix2 += Matrix1;
            Console.WriteLine($"�������� ���� ������:\n{Matrix2}\n");

            Matrix2 -= Matrix1;
            Console.WriteLine($"��������� ���� ������:\n{Matrix2}\n");

            Matrix2 *= Matrix1;
            Console.WriteLine($"��������� ���� ������:\n{Matrix2}\n");

            Matrix2 *= 3;
            Console.WriteLine($"��������� ������� �� �����:\n{Matrix2}\n");

            Console.WriteLine($"������������:\n{Matrix1.GetDeterminant()}\n{Matrix2.GetDeterminant()}\n");

            Console.WriteLine("��������� (����� ���� ��������):");
            Console.WriteLine($"Matrix1 > Matrix2 = {Matrix1 > Matrix2}");
            Console.WriteLine($"Matrix1 >= Matrix2 = {Matrix1 >= Matrix2}");
            Console.WriteLine($"Matrix1 < Matrix2 = {Matrix1 < Matrix2}");
            Console.WriteLine($"Matrix1 <= Matrix2 = {Matrix1 <= Matrix2}\n");
            Console.WriteLine($"Matrix1 == Matrix2 = {Matrix1 == Matrix2}\n");

            Matrix1.RemoveColumnAt(0);
            Matrix2.RemoveRowAt(0);
            Console.WriteLine($"�������������� ���������� ������ � �������������:\n{Matrix1}\n{Matrix2}\n");
                        
            Console.WriteLine($"��������� 1-�� ������� �� 2-��:\n{Matrix1 * Matrix2}\n");
            Console.WriteLine($"��������� 2-�� ������� �� 1-��:\n{Matrix2 * Matrix1}\n");

            Console.WriteLine("�������� ������������ ������: ");
            try
            {
                Matrix1 += Matrix2;
                Console.WriteLine(Matrix1);
            }
            catch(Exception Error)
            {
                Console.WriteLine(Error.Message + "\n");
            }

            Console.WriteLine("����������� ������������ �������: ");
            try
            {                
                Console.WriteLine(Matrix1.GetDeterminant());
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
            }

            Console.ReadLine();
        }
    }

}
