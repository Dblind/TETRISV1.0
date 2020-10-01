using System;
using System.IO;

namespace TETRISV1
{
    class Save
    {
        public static char brick = '+';
        public static void SeveGame()
        {
            StreamWriter sw = new StreamWriter("Save.txt");

            sw.WriteLine(Run.GameFild.FildGame.GetLength(0));
            sw.WriteLine(Run.GameFild.FildGame.GetLength(1));
            sw.WriteLine(Move.dotMove[1]);
            sw.WriteLine(Move.dotMove[0]);
            sw.WriteLine($"{Run.GameFild.numberFigNow}\n{Run.GameFild.numberFigNext}");
            sw.WriteLine(Run.GameFild.Score);

            for (int i = 0; i < Run.GameFild.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < Run.GameFild.FildGame.GetLength(1); j++)
                {
                    if (Run.GameFild.FildGame[i, j] == true) sw.Write(brick);
                    else sw.Write(' ');
                }
                sw.WriteLine();
            }
            sw.WriteLine($"\n\n now {Run.GameFild.FigNow.ToString()}\n next {Run.GameFild.FigNext.ToString()}");
            sw.Close();

        }
    }

    class Load
    {
        public static void LoadGame()
        {
            //Run.GameFild;
            StreamReader sR = new StreamReader("Save.txt");

            Run.GameFild = new Fild(int.Parse(sR.ReadLine()), int.Parse(sR.ReadLine()));
            Move.dotMove[1] = int.Parse(sR.ReadLine());
            Move.dotMove[0] = int.Parse(sR.ReadLine());
            Run.GameFild.numberFigNext = int.Parse(sR.ReadLine());
            Run.GameFild.MakeNextFig();
            Run.GameFild.FigNow = Run.GameFild.FigNext;
            Run.GameFild.numberFigNow = Run.GameFild.numberFigNext;
            Run.GameFild.numberFigNext = int.Parse(sR.ReadLine());
            Run.GameFild.MakeNextFig();
            Run.GameFild.Score = int.Parse(sR.ReadLine());

            string rowFild;
            for (int y = 0; y < Run.GameFild.FildGame.GetLength(0); y++)
            {
                rowFild = sR.ReadLine();
                for (int x = 0; x < Run.GameFild.FildGame.GetLength(1); x++)
                {
                    if (rowFild[x] == Save.brick) Run.GameFild.FildGame[y, x] = true;
                    else Run.GameFild.FildGame[y, x] = false;

                }
            }

            if (Settings.isColorScreen == 1)
                for (int i = 0; i < Run.GameFild.FCScreen.FildColorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < Run.GameFild.FCScreen.FildColorArray.GetLength(1); j++)
                    {
                        if (Run.GameFild.FildGame[i, j] == true)
                            Run.GameFild.FCScreen.FildColorArray[i, j] = Settings.ConsColBrick;
                    }
                }
                
            sR.Close();
        }
    }
}