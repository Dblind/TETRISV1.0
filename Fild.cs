using System;
namespace TETRISV1
{
    delegate void DisplayVariable(Fild fg);
    delegate void DelegFallWallColor(int y, int x);
    class Fild
    {

        public MoveBrick AddBrickColor;
        public MoveBrick DelBrickColor;
        public DisplayVariable RenderType;
        public DelegFallWallColor FallWallColorScreen;
        public DelegFallWallColor ClearUpLine;
        public bool[,] FildGame = new bool[,] { { false } };
        public FildColor FCScreen;

        public bool RunGame = true;
        public IFigures FigNow; //= new IFigures();
        public IFigures FigNext { get; set; }
        public int numberFigNext, numberFigNow;              // for NewFigure
        public int Score = 0;
        Random rand = new Random();     //for NewFigure

        public Fild(int rows, int columns)
        {
            Move.startMovePoint = columns / 2 - 1;
            FildGame = new bool[rows, columns];
            for (int i = 0; i < FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < FildGame.GetLength(1); j++)
                {
                    FildGame[i, j] = false;
                }
            }
            FCScreen = new FildColor(this);
            numberFigNext = rand.Next(1, 8);
            MakeNextFig();

        }

        public void NewFigure()
        {
            Run.FlagFastFall = false;
            Run.StepFall = 99;
            try //first call is null
            {
                Move.CheckRows(this);
            }
            catch { }
            Move.dotMove[0] = 0; Move.dotMove[1] = Move.startMovePoint;
            FigNow = FigNext; numberFigNow = numberFigNext;
            numberFigNext = rand.Next(1, 8);
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
                this.RunGame = Run.haveGame = false;
                Run.isSave = true;
                Score = 0;
                Console.ResetColor();
                Console.CursorVisible = true;


            }
        }
        public void MakeNextFig()
        {
            switch (numberFigNext)
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
            public static int counter = 0;

        public void ScreenRender()
        {
            RenderType(Run.GameFild);
            Fild.counter++;
        }
    }

    class PrintScreen
    {
        public static void PrintCharScreen(Fild fg)
        {
            Move.SetFigForm(fg);
            System.Console.CursorTop = 0; Console.CursorLeft = 0;
            Console.BackgroundColor = Settings.ConsColBackground;
            Console.ForegroundColor = Settings.ConsColBrick;
            for (int i = 0; i < fg.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    if (fg.FildGame[i, j]) System.Console.Write(Settings.keyBuild);
                    else System.Console.Write(Settings.keyBackground);
                }
                System.Console.WriteLine();
            }
            Move.TakeOutFigForm(fg);

            Console.ResetColor();
            System.Console.WriteLine(Fild.counter);

            // print Score
            Console.CursorTop = 0;
            Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(String.Format($"{fg.Score,10}"));

            // clear place for view NextFig
            for (int i = 0; i < fg.FigNext.Form.GetLength(0); i++)
            {
                ClearLine();
                for (int j = 0; j < fg.FigNext.Form.GetLength(1); j++)
                    if (fg.FigNext.Form[i, j]) System.Console.Write(Settings.keyBuild);
                    else System.Console.Write(Settings.keyBackground);
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
                System.Console.Write($"    ");
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