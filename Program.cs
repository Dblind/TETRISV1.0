using System;
using System.Threading;

namespace TETRISV1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run runGame = new Run();
            Run.RunGame();
            /*
            int sizeFild = 10;
            //char[,] FildGame = new char[sizeFild, sizeFild];
            Random rand = new Random();
            //char key = '+';
            //bool flag = true;
            Fild GameFild = new Fild(sizeFild, sizeFild);
            Console.Clear();
            GameFild.NewFigure();
            ConsoleKeyInfo key2;
            GameFild.Display();
            int count = 0;
            while (Fild.RunGame)
            {
                //key = flag ? '+' : '*';

                //key2 = Console.ReadKey(true); char str = key2.KeyChar;
                //Control.Push(key2, GameFild);
                //Move.Wait(0.2);
                //else GameFild.NewFigure();
                //Console.ReadKey();
                //Move.Wait(0.2);
                //flag = !flag;
                //System.Console.WriteLine();
                GameFild.Display();
                    TestMove();

                //System.Console.WriteLine("dfafd");
                Thread.Sleep(10);
                if (count > 90)
                {
                    count = 0;
                    if (Move.CheckDowd(GameFild)) Move.MoveDowd(GameFild);
                    else GameFild.NewFigure();
                }
                else count++;

            }
            void TestMove()
            {
                if (Console.KeyAvailable == true)
                {
                    key2 = Console.ReadKey(true);
                    char str = key2.KeyChar;
                    Control.Push(key2, GameFild);
                    GameFild.Display();
                    //TestMove();
                }
            }*/
        }

    }
}
