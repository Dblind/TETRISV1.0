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
        public static int StepFall {get;set;} = 99;
        public static bool FlagFastFall{get;set;} = false;
        public static void RunGame()
        {
            Console.CursorVisible = false;
            Console.Clear();
            GameFild.NewFigure();
            GameFild.Display();
            
            while (Fild.RunGame)
            {
                GameFild.Display();
                TestMove();

                Thread.Sleep(10);
                if (count > StepFall)
                {
                    count = 0;
                    if (Move.CheckDowd(GameFild)) Move.MoveDowd(GameFild);
                    else GameFild.NewFigure();
                }
                else count++;

            }
        }
        static void TestMove()
        {
            if (Console.KeyAvailable == true)
            {
                key2 = Console.ReadKey(true);
                char str = key2.KeyChar;
                Control.Push(key2, GameFild);
                
                //TestMove();
            }
        }
    }
}
