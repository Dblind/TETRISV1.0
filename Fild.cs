using System;
namespace TETRISV1
{
    class Fild
    {
        public char[,] FildGame = new char[,] { { Move.background } };
        public static bool RunGame = true;
        public IFigures FigNow; //= new IFigures();
        Random rand = new Random();     //for NewFigure
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

            switch (numberNextFig)
            {
                case (0):
                    FigNow = new LineSizeTwoFigure(); FigNow.RestorForm();
                    // FigNow = new LineSizeTwo();
                    break;
                case (1):
                    FigNow = new SFigure(); FigNow.RestorForm();
                    break;
                case (2):
                    FigNow = new CubeFigure(); FigNow.RestorForm();
                    break;
                case (3):
                    FigNow = new LFigure(); FigNow.RestorForm();
                    break;
                case (4):
                    FigNow = new ReversLFigure(); FigNow.RestorForm();
                    break;
                case (5):
                    FigNow = new ReversSFigure(); FigNow.RestorForm();
                    break;
                case (6):
                    FigNow = new LineFigure(); FigNow.RestorForm();
                    break;
            }
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
            Console.CursorTop = 0;
            Console.CursorLeft = this.FildGame.GetLength(1) + 3;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(String.Format($"{Control.Score,10}"));

            for (int i = 0; i < this.FigNow.Form.GetLength(0); i++)
            {
                System.Console.WriteLine();
                Console.CursorLeft = this.FildGame.GetLength(1) + 3;// + j;
                for (int j = 0; j < this.FigNow.Form.GetLength(1); j++)
                {
                    //Console.CursorTop = 3 + i;
                    System.Console.Write(this.FigNow.Form[i, j]);
                }
            }
        }
    }
}