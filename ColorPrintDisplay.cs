using System;

namespace TETRISV1
{
    class ColorDisplay
    {
        public static void ColorPrintDisplay(FildColor fc)
        {
            Move.PrintFig(fc);
            System.Console.CursorTop = 0; Console.CursorLeft = 0;
            Console.BackgroundColor = Setting.ConsColBackground;
            Console.ForegroundColor = Setting.ConsColBrick;
            for (int i = 0; i < fc.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < fc.FildGame.GetLength(1); j++)
                {
                    if (fc.FildGame[i, j] == Setting.keyBuild)
                    {
                        Console.BackgroundColor = Setting.ConsColBrick;
                    }
                    else Console.BackgroundColor = Setting.ConsColBackground;
                    System.Console.Write(fc.FildGame[i, j]);System.Console.Write(fc.FildGame[i, j]);
                }
                System.Console.WriteLine();
            }
            Move.DeleteFig(fc);
/* 
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
            } */

            // Task
            System.Console.WriteLine();
            System.Console.WriteLine();
            Console.CursorLeft = fc.FildGame.GetLength(1) + 3;
            System.Console.Write("Q: quit.");

        }
    }
    delegate void MoveBrick(int x, int y, ConsoleColor col);
    class FildColor
    {
        
        ConsoleColor[,] FildColorArray;

        public void AddBrick(int y, int x, ConsoleColor col)
        {
            FildColorArray[y, x] = col;
        }
        public void DelBrick(int y, int x, ConsoleColor col)
        {
            FildColorArray[y, x] = col;
        }

        public FildColor(Fild fg)
        {
            FildColorArray = new ConsoleColor[fg.FildGame.GetLength(0),fg.FildGame.GetLength(1)];
            for (int y = 0; y < fg.FildGame.GetLength(0); y++)
            {
                for (int x = 0; x < fg.FildGame.GetLength(1); x++)
                {
                    FildColorArray[y, x] = Setting.ConsColBackground;
                }
                 
            }
            
        }
    }
}