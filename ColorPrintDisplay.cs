using System;

namespace TETRISV1
{
    class ColorDisplay
    {
        public static void ColorPrintDisplay(Fild fg)
        {
            Move.SetFigForm(fg);
            System.Console.CursorTop = 0; Console.CursorLeft = 0;
            Console.BackgroundColor = Settings.ConsColBackground;
            for (int i = 0; i < fg.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    Console.BackgroundColor = fg.FCScreen.FildColorArray[i, j];
                    System.Console.Write("  ");
                }
                System.Console.WriteLine();
            }
            Move.TakeOutFigForm(fg);

            Console.ResetColor();
            System.Console.WriteLine(Fild.counter);


            // print Score
            Console.CursorTop = 0;
            Console.CursorLeft = 2 * fg.FildGame.GetLength(1) + 3;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = Settings.ConsColBackground;
            System.Console.WriteLine(String.Format($"{fg.Score,10}"));

            // clear place for view NextFig
            for (int i = 0; i < fg.FigNext.Form.GetLength(0); i++)
            {
                ClearLine();
                for (int j = 0; j < fg.FigNext.Form.GetLength(1); j++)
                    if (fg.FigNext.Form[i, j] == true)
                    {
                        Console.BackgroundColor = fg.FigNext.FigureColor;
                        System.Console.Write("  ");
                    }
                    else
                    {
                        Console.BackgroundColor = Settings.ConsColBackground;
                        Console.Write("  ");
                    }
            }
            if (fg.FigNext.Form.GetLength(0) < 3)
            {
                ClearLine(); ClearLine();
            }
            else if (fg.FigNext.Form.GetLength(0) < 4) ClearLine();
            void ClearLine()
            {
                Console.BackgroundColor = Settings.ConsColBackground;
                System.Console.WriteLine();
                Console.CursorLeft = 2 * fg.FildGame.GetLength(1) + 3;
                System.Console.Write($"        ");
                Console.CursorLeft = 2 * fg.FildGame.GetLength(1) + 3;
            }

            // Task
            System.Console.WriteLine();
            System.Console.WriteLine();
            Console.BackgroundColor = Settings.ConsColBackground;
            Console.CursorLeft = 2 * fg.FildGame.GetLength(1) + 3;
            System.Console.Write("Q: quit.");

        }
    }
    delegate void MoveBrick(int x, int y, ConsoleColor col);
    class FildColor
    {

        public ConsoleColor[,] FildColorArray;

        public void AddBrick(int y, int x, ConsoleColor col)
        {
            FildColorArray[y, x] = col;
        }
        public void DelBrick(int y, int x, ConsoleColor col)
        {
            FildColorArray[y, x] = col;
        }
        public void FallWall(int y, int x)
        {
            FildColorArray[y, x] = FildColorArray[y - 1, x];
        }
        public void ClearUpLine(int y, int x)
        {
            FildColorArray[y, x] = Settings.ConsColBackground;
        }

        public FildColor(Fild fg)
        {
            FildColorArray = new ConsoleColor[fg.FildGame.GetLength(0), fg.FildGame.GetLength(1)];
            for (int y = 0; y < fg.FildGame.GetLength(0); y++)
            {
                for (int x = 0; x < fg.FildGame.GetLength(1); x++)
                {
                    FildColorArray[y, x] = Settings.ConsColBackground;
                }

            }

        }
    }
}