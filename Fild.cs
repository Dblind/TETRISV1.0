using System;
namespace TETRISV1
{
    class Fild
    {
        public char[,] FildGame = new char[,] { { Move.background } };
        public static bool RunGame = true;
        public IFigures FigNow; //= new IFigures();
        Random rand = new Random();     //for NewFigure
        public IFigures FigNext { get; set; }

        int numberNextFig;              // for NewFigure
        public Fild(int rows, int columns)
        {
            Move.startMove = columns / 2 - 1;
            FildGame = new char[rows, columns];
            for (int i = 0; i < FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < FildGame.GetLength(1); j++)
                {
                    FildGame[i, j] = Move.background;
                }
            }
            numberNextFig = rand.Next(1, 7);
            MakeNextFig();
            // for (int i = rows - 1; i < rows; i++)
            // {
            //     for (int j = 0; j < columns; j++)
            //     {
            //         FildGame[i, j] = Move.keyBottom;
            //     }
            // }
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
            Move.dotMove[0] = 0; Move.dotMove[1] = Move.startMove;
            FigNow = FigNext;
            MakeNextFig();
            numberNextFig = rand.Next(1, 7);

            // FildGame[Move.dotMove[0], Move.dotMove[1]] = Move.keyBuild;
            // FildGame[Move.dotMove[0] + 1, Move.dotMove[1]] = Move.keyBuild;
            // for (int i = 0; i < FigNow.Form.GetLength(0); i++)
            // {
            //     for (int j = 0; j < FigNow.Form.GetLength(1); j++)
            //     {
            //         if (FigNow.Form[i, j] == Move.keyBuild)
            //         {
            //             FildGame[i + Move.dotMove[0], j + Move.dotMove[1]] = Move.keyBuild;
            //         }
            //     }
            // }
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
                    FigNext = new CubeFigure(); FigNext.RestorForm();
                    break;
                case (3):
                    FigNext = new LFigure(); FigNext.RestorForm();
                    break;
                case (4):
                    FigNext = new ReversLFigure(); FigNext.RestorForm();
                    break;
                case (5):
                    FigNext = new ReversSFigure(); FigNext.RestorForm();
                    break;
                case (6):
                    FigNext = new LineFigure(); FigNext.RestorForm();
                    break;
            }
        }

        public void Display()
        {
            Move.PrintFig(this);
            System.Console.CursorTop = 0; Console.CursorLeft = 0;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < FildGame.GetLength(1); j++)
                {
                    System.Console.Write(FildGame[i, j]);
                }
                System.Console.WriteLine();
            }
            Move.DeleteFig(this);

            // print Score
            Console.CursorTop = 0;
            Console.CursorLeft = FildGame.GetLength(1) + 3;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(String.Format($"{Control.Score,10}"));

            // clear place for view NextFig

            for (int i = 0; i < FigNext.Form.GetLength(0); i++)
            {
                ClearLine();
                for (int j = 0; j < FigNext.Form.GetLength(1); j++)
                    System.Console.Write(FigNext.Form[i, j]);
            }
            if(FigNext.Form.GetLength(0) < 4)
            {
                ClearLine();
            }
            void ClearLine()
            {
                System.Console.WriteLine();
                Console.CursorLeft = FildGame.GetLength(1) + 3;
                System.Console.Write("....");
                Console.CursorLeft = FildGame.GetLength(1) + 3;
            }

        }
    }
}