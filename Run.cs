using System;
using System.Threading;

namespace TETRISV1
{
    class Run
    {
        static int sizeFild = 10;
        static ConsoleKeyInfo key2;
        static Fild GameFild = new Fild(sizeFild, sizeFild);
        public static int count = 0;
        public static void RunGame()
        {

            Console.Clear();
            GameFild.NewFigure();
            GameFild.Display();

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
                //else count++;

            }
        }
        static void TestMove()
        {
            if (Console.KeyAvailable == true)
            {
                key2 = Console.ReadKey(true);
                char str = key2.KeyChar;
                Control.Push(key2, GameFild);
                GameFild.Display();
                //TestMove();
            }
        }
    }
}
