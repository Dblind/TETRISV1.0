﻿using System;

namespace TETRISV1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int sizeFild = 12;
            //char[,] FildGame = new char[sizeFild, sizeFild];
            Random rand = new Random();
            //char key = '+';
            bool flag = true;
            Fild GameFild = new Fild(sizeFild, sizeFild);
            Console.Clear();
            GameFild.NewFigure();
            ConsoleKeyInfo key2;
            GameFild.Display();
            while (Fild.RunGame)
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
        
    }
}