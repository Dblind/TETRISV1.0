using System;
namespace TETRISV1
{
    class Fild
    {
        public char[,] FildGame;
        public static bool RunGame = true;
        public IFigures FigNow; //= new IFigures();
        public Fild(int rows, int columns)
        {
            Move.startMove = columns / 2 - 1;
            FildGame = new char[rows, columns];
            for (int i = 0; i < FildGame.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < FildGame.GetLength(1); j++)
                {
                    FildGame[i, j] = Move.background;
                }
            }
            for (int i = rows - 1; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    FildGame[i, j] = Move.keyBottom;
                }
            }
        }

        public void NewFigure()
        {
            Move.CheckRows(this);
            Move.dotMove[0] = 0; Move.dotMove[1] = Move.startMove;
            Random rand = new Random();
            switch (rand.Next(2))
            {
                case (0):
                    FigNow = new LineSizeTwo(); FigNow.RestorForm();
                    // FigNow = new LineSizeTwo();
                    break;
                case (1):
                    FigNow = new SFigure();FigNow.RestorForm();
                    break;
            }
            // FildGame[Move.dotMove[0], Move.dotMove[1]] = Move.keyBuild;
            // FildGame[Move.dotMove[0] + 1, Move.dotMove[1]] = Move.keyBuild;
            for (int i = 0; i < FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < FigNow.Form.GetLength(1); j++)
                {
                    if (FigNow.Form[i, j] == Move.keyBuild)
                    {
                        FildGame[i + Move.dotMove[0], j + Move.dotMove[1]] = Move.keyBuild;
                    }
                }
            }
        }

        public void Display()
        {
            System.Console.CursorTop = 0;
            for (int i = 0; i < FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < FildGame.GetLength(1); j++)
                {
                    System.Console.Write(FildGame[i, j]);
                }
                System.Console.WriteLine();
            }
        }
    }
}