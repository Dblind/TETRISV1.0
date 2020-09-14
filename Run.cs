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
            try { Setting.ReadFileSetting(); }
            catch { }
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
        static ConsoleKeyInfo key2;
        public static Fild GameFild;
        public static int count = 0;
        public static int StepFall { get; set; } = 99;
        public static bool FlagFastFall { get; set; } = false;
        public static void RunGame()
        {
            Console.CursorVisible = false;
            Console.Clear();
            GameFild = new Fild(Setting.FildHeight, Setting.FildWidth);
            if (Setting.ColorScreen == 1)
            {
                GameFild.FCScreen = new FildColor(GameFild);
                GameFild.AddBrickColor += Run.GameFild.FCScreen.AddBrick;
                GameFild.DelBrickColor += Run.GameFild.FCScreen.DelBrick;
                GameFild.DisplayType += ColorDisplay.ColorPrintDisplay;
                GameFild.FallWallColorScreen += GameFild.FCScreen.FallWall;
                GameFild.ClearUpLine += GameFild.FCScreen.ClearUpLine;
            }
            else GameFild.DisplayType += PrintScreen.PrintCharScreen;
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
                else count += Setting.Speed;//count++;

            }
        }
        static void TestMove()
        {
            if (Console.KeyAvailable == true)
            {
                key2 = Console.ReadKey(true);
                Control.Push(key2, GameFild);
                //TestDeleg = TM2;
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
                //Thread.Sleep(100);
                TestDeleg();
            }


        }
    }
}
