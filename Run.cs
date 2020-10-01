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
            try { Settings.ReadSettingsFileAndApply(); }
            catch { }
            while (RunGameFlag)
            {
                Menu.MainMenu.MainMenuWriter();
            }
        }
    }
    delegate void FallDeleg();
    class Run
    {
        public static bool haveGame, isSave = true;
        public static event FallDeleg TestDeleg;
        static ConsoleKeyInfo key2;
        public static Fild GameFild;
        public static int count = 0;
        public static int StepFall { get; set; } = 99;
        public static bool FlagFastFall { get; set; } = false;
        public static void RunGame()
        {
            haveGame = true;
            isSave = false;
            Console.CursorVisible = false;
            Console.Clear();
            GameFild = new Fild(Settings.FildHeight, Settings.FildWidth);

            GameFild.NewFigure();

            bodyWhile();
        }
        public static void ContinueGame()
        {
            isSave = false;
            Console.CursorVisible = false;
            Console.Clear();

            if (haveGame) { GameFild.RunGame = true; bodyWhile(); }
            else
            {
                try { Load.LoadGame(); }
                catch { isSave = Console.CursorVisible = true; return; }
                haveGame = GameFild.RunGame = true;
                bodyWhile();
            }
        }

        static void TestMove()
        {
            if (Console.KeyAvailable == true)
            {
                key2 = Console.ReadKey(true);
                Control.Push(key2, GameFild);
            }
        }

        static void bodyWhile()
        {
            if (Settings.isColorScreen == 1)
            {

                GameFild.AddBrickColor += Run.GameFild.FCScreen.AddBrick;
                GameFild.DelBrickColor += Run.GameFild.FCScreen.DelBrick;
                GameFild.RenderType += ColorDisplay.ColorPrintDisplay;
                GameFild.FallWallColorScreen += GameFild.FCScreen.FallWall;
                GameFild.ClearUpLine += GameFild.FCScreen.ClearUpLine;
            }
            else GameFild.RenderType += PrintScreen.PrintCharScreen;

            GameFild.ScreenRender();
            while (GameFild.RunGame)
            {
                TestMove();

                if (count > StepFall)
                {
                    count = 0;
                    if (Move.CheckDowd(GameFild)) Move.MoveDowd(GameFild);
                    else GameFild.NewFigure();
                    GameFild.ScreenRender();
                }
                else count += Settings.Speed;//count++;
                Thread.Sleep(10);
            }
            Console.ResetColor();

            if (Settings.isColorScreen == 1)
            {

                GameFild.AddBrickColor -= Run.GameFild.FCScreen.AddBrick;
                GameFild.DelBrickColor -= Run.GameFild.FCScreen.DelBrick;
                GameFild.RenderType -= ColorDisplay.ColorPrintDisplay;
                GameFild.FallWallColorScreen -= GameFild.FCScreen.FallWall;
                GameFild.ClearUpLine -= GameFild.FCScreen.ClearUpLine;
            }
            else GameFild.RenderType -= PrintScreen.PrintCharScreen;
        }
    }
}
