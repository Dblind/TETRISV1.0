using System;

namespace TETRISV1._0
{
    class Program
    {
        static void Main2(string[] args)
        {


            int sizeFild = 8;
            //char[,] FildGame = new char[sizeFild, sizeFild];
            Random rand = new Random();
            char key = '+';
            bool flag = true;
            Fild GameFild = new Fild(sizeFild, sizeFild);
            Console.Clear();
            GameFild.NewFigure();
            ConsoleKeyInfo key2;
            GameFild.Display();
            while (Fild.StopGame)
            {
                //key = flag ? '+' : '*';

                key2 = Console.ReadKey(true); char str = key2.KeyChar;
                Control.Push(key2, GameFild);
                //Move.Wait(0.2);
                //else GameFild.NewFigure();
                //Console.ReadKey();
                //Move.Wait(0.2);
                flag = !flag;
                System.Console.WriteLine();
                GameFild.Display();

            }
        }
        static void Main(string[] args)
        {
            // System.Console.WriteLine("adfadf");
            // foreach (var e in weights)
            //     Console.Write(e + " ");
            //     System.Console.WriteLine();
            // MakeSubsets(new bool[weights.Length], 0);
            Students[0].FirstName = "dasf";
            System.Console.WriteLine(Students[0].FirstName);
            System.Console.WriteLine(Students[1].FirstName);

        }

        static void MakeSubsets(bool[] subset, int position)
        {
            if (position == subset.Length)
            {
                Evaluate(subset);
                return;
            }
            subset[position] = false;
            MakeSubsets(subset, position + 1);
            subset[position] = true;
            MakeSubsets(subset, position + 1);
        }
        static int[] weights = new int[] { 2, 5, 6, 12, 4, 7 };
        static void Evaluate(bool[] subset)
        {
            var delta = 0;
            for (int i = 0; i < subset.Length; i++)
            {
                if ((subset[i])) delta += weights[i];
                else delta -= weights[i];
            }
            foreach (var e in subset)
                Console.Write(e ? 1 : 0);
            Console.Write(" ");
            if (delta == 0) Console.Write("OK");
            System.Console.WriteLine();
        }
        static Student[] Students = new Student[2];
        class Student
        {
            public string FirstName;
            string LastName;
        }
    }
}
