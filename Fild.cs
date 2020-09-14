using System;
namespace TETRISV1
{
    delegate void DisplayVariable(Fild fg);
    delegate void DelegFallWallColor(int y, int x);
    class Fild
    {

        public MoveBrick AddBrickColor;
        public MoveBrick DelBrickColor;
        public DisplayVariable DisplayType;
        public DelegFallWallColor FallWallColorScreen;
        public DelegFallWallColor ClearUpLine;
        public char[,] FildGame = new char[,] { { Setting.background } };
        public FildColor FCScreen;// = new FildColor(GameFild);

        public static bool RunGame = true;
        public IFigures FigNow; //= new IFigures();
        Random rand = new Random();     //for NewFigure
        public IFigures FigNext { get; set; }

        int numberNextFig;              // for NewFigure
        public Fild(int rows, int columns)
        {
            Move.startMovePoint = columns / 2 - 1;
            FildGame = new char[rows, columns];
            for (int i = 0; i < FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < FildGame.GetLength(1); j++)
                {
                    FildGame[i, j] = Setting.background;
                }
            }
            numberNextFig = rand.Next(1, 8);
            MakeNextFig();

        }

        public void NewFigure()
        {
            Run.FlagFastFall = false;
            Run.StepFall = 99;
            try //first null
            {
                Move.CheckRows(this);
            }
            catch { }
            Move.dotMove[0] = 0; Move.dotMove[1] = Move.startMovePoint;
            FigNow = FigNext;
            numberNextFig = rand.Next(1, 8);
            MakeNextFig();
            if (!SupportMethods.Intersection(FigNow.Form, this))
            {
                Console.CursorTop = 3; Console.CursorLeft = 4;
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("< Game Over >");
                bool flag = true;
                do
                {
                    if ('q' == Console.ReadKey(true).KeyChar) flag = false;
                }
                while (flag);
                Fild.RunGame = false;
                Control.Score = 0;
                Console.ResetColor();
                Console.CursorVisible = true;


            }
        }
        void MakeNextFig()
        {
            switch (numberNextFig)
            {
                case (0):
                    FigNext = new LineSizeTwoFigure(); FigNext.RestorForm();
                    // FigNow = new LineSizeTwo();
                    break;
                case (1):
                    FigNext = new SFigure(); FigNext.RestorForm();
                    break;
                case (2):
                    FigNext = new ReversSFigure(); FigNext.RestorForm();
                    break;
                case (3):
                    FigNext = new LFigure(); FigNext.RestorForm();
                    break;
                case (4):
                    FigNext = new ReversLFigure(); FigNext.RestorForm();
                    break;
                case (5):
                    FigNext = new CubeFigure(); FigNext.RestorForm();
                    break;
                case (6):
                    FigNext = new LineFigure(); FigNext.RestorForm();
                    break;
                case (7):
                    FigNext = new TFigure(); FigNext.RestorForm();
                    break;
            }
        }

        public void Display()
        {
            DisplayType(Run.GameFild);
        }
    }

    class PrintScreen
    {
        public static void PrintCharScreen(Fild fg)
        {
            Move.PrintFig(fg);
            System.Console.CursorTop = 0; Console.CursorLeft = 0;
            Console.BackgroundColor = Setting.ConsColBackground;
            Console.ForegroundColor = Setting.ConsColBrick;
            for (int i = 0; i < fg.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    System.Console.Write(fg.FildGame[i, j]);
                }
                System.Console.WriteLine();
            }
            Move.DeleteFig(fg);

            // print Score
            Console.CursorTop = 0;
            Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(String.Format($"{Control.Score,10}"));

            // clear place for view NextFig

            for (int i = 0; i < fg.FigNext.Form.GetLength(0); i++)
            {
                ClearLine();
                for (int j = 0; j < fg.FigNext.Form.GetLength(1); j++)
                    System.Console.Write(fg.FigNext.Form[i, j]);
            }
            if (fg.FigNext.Form.GetLength(0) < 3)
            {
                ClearLine(); ClearLine();
            }
            else if (fg.FigNext.Form.GetLength(0) < 4) ClearLine();
            void ClearLine()
            {
                System.Console.WriteLine();
                Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
                System.Console.Write($"{Setting.background}{Setting.background}{ Setting.background}{ Setting.background}");
                Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            }

            // Task
            System.Console.WriteLine();
            System.Console.WriteLine();
            Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            System.Console.Write("Q: quit.");



        }
    }
}