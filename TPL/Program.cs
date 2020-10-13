using System;
using System.Threading.Tasks;

namespace TPL
{
    // Даны две двумерных матрицы размерностью 100х100 каждая. 
    // Задача: написать приложение, производящее их параллельное умножение. 
    // Матрицы заполняются случайными целыми числами от 0 до 10.

    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix1 = new int[100, 100];
            int[,] matrix2 = new int[100, 100];

            Completion(matrix1);
            Completion(matrix2);

            Task<int[,]> task1 = new Task<int[,]>(() => Multiplay(matrix1, matrix1));
            Task<int[,]> task2 = new Task<int[,]>(() => Multiplay(matrix2, matrix2));
            Task<int[,]> task3 = new Task<int[,]>(() => Multiplay(matrix1, matrix2));

            task1.Start();
            task2.Start();
            task3.Start();

            int[,] r1 = task1.Result;
            int[,] r2 = task2.Result;
            int[,] r3 = task3.Result;
            
            Console.ReadKey();
        }

        /// <summary>
        /// Метод заполняющий матрицы случайными числами 
        /// </summary>
        /// <param name="matrix"></param>
        static void Completion(int[,] matrix)
        {
            
            var rnd = new Random();
            for (int i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    matrix[i, j] = rnd.Next(0, 10);
                }
            } 
        }

        /// <summary>
        /// Метод умножения матриц
        /// </summary>
        /// <param name="a">Матрица №1</param>
        /// <param name="b">Матрица №2</param>
        /// <returns>Результирующая матрица</returns>
        static int[,] Multiplay(int[,] a, int[,] b)
        {
            {
                if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
                int[,] r = new int[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        for (int k = 0; k < b.GetLength(0); k++)
                        {
                            r[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }
                return r;
            }
        }
    }
}
