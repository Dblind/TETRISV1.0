using System;
using System.Threading;

namespace TETRISV1
{
    class RunNewGame
    {
        //public Menu NewGame = new Menu();
        public static bool RunGameFlag = true;
        public void RunGame()
        {
            Setting.ReadFileSetting();
            while (RunGameFlag)
            {
                Rellise.MainMenuRun();
            }
        }
    }
    delegate void FallDeleg();
    class Run
    {
        public static event FallDeleg TestDeleg;
        static int sizeFild = 10;
        static ConsoleKeyInfo key2;
        static Fild GameFild;
        public static int count = 0;
        public static int StepFall { get; set; } = 99;
        public static bool FlagFastFall { get; set; } = false;
        public static void RunGame()
        {
            Console.CursorVisible = false;
            Console.Clear();
            GameFild = new Fild(sizeFild, sizeFild);
            GameFild.NewFigure();
            GameFild.Display();
            // FigNow = FigNext;

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
                TestDeleg = TM2;
                //TestDeleg();
                //TM2();
                //TestMove();
            }
        }
        static void TM2()
        {
            if (Console.KeyAvailable == true)
            {
                if (Move.CheckRight(GameFild)) Move.MoveRight(GameFild);
                //Control.Push(key2, GameFild);
                //TM2();
                Thread.Sleep(100);
                TestDeleg();
            }


        }
    }
}
