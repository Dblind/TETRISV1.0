using System;
namespace TETRISV1
{
    delegate void DisplayVariable(Fild fg);
    delegate void DelegFallWallColor(int y, int x);
    class Fild
    {
        public static int RenderCounter = 0;

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
            numberFigNext = rand.Next(0, 7);
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
            FigNow.RestorForm();
            numberFigNext = rand.Next(0, 7);
            MakeNextFig();
            if (!SupportMethods.Intersection(FigNow.DefaultForm, this))
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
        public ConteinerFigures ContFig = new ConteinerFigures();
        public void MakeNextFig()
        {
            FigNext = ContFig[numberFigNext];
        }

        public void ScreenRender()
        {
            RenderType(Run.GameFild);
            Fild.RenderCounter++;
        }
    }

    class PrintScreen
    {
        static readonly ConsoleColor BackColor = ConsoleColor.Black;
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

            System.Console.WriteLine(Fild.RenderCounter);

            // print Score
            Console.CursorTop = 0;
            Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(String.Format($"{fg.Score,10}"));

            // clear place for view NextFig
            int CounterLines = 0;
            for (int i = 0; i < fg.FigNext.DefaultForm.GetLength(0); i++)
            {
                CounterLines++;
                ClearLine();
                for (int j = 0; j < fg.FigNext.DefaultForm.GetLength(1); j++)
                    if (fg.FigNext.DefaultForm[i, j]) System.Console.Write(Settings.keyBuild);
                    else System.Console.Write(Settings.keyBackground);
            }
            while (CounterLines < 4) { ClearLine(); CounterLines++; }

            // Task
            System.Console.WriteLine();
            System.Console.WriteLine();
            Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            System.Console.Write(" Q: quit.");

            void ClearLine()
            {
                System.Console.WriteLine();
                Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
                System.Console.Write($"    ");
                Console.CursorLeft = fg.FildGame.GetLength(1) + 3;
            }
        }
    }
}