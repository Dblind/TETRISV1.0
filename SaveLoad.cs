using System;
using System.IO;

namespace TETRISV1
{
    class Save
    {
        public static void SeveGame()
        {
            StreamWriter sw = new StreamWriter("Save.txt");
            //Run.GameFild.FildGame[1,2]; sw.Write;
            sw.WriteLine(Run.GameFild.FildGame.GetLength(0));
            sw.WriteLine(Run.GameFild.FildGame.GetLength(1));
            sw.WriteLine(Move.dotMove[1]);
            sw.WriteLine(Move.dotMove[0]);
            sw.WriteLine($"{Run.GameFild.numberFigNow}\n{Run.GameFild.numberFigNext}");
            for (int i = 0; i < Run.GameFild.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < Run.GameFild.FildGame.GetLength(1); j++)
                {
                    sw.Write(Run.GameFild.FildGame[i, j]);
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

            string rowFild;
            for (int y = 0; y < Run.GameFild.FildGame.GetLength(0); y++)
            {
                rowFild = sR.ReadLine();
                for (int x = 0; x < Run.GameFild.FildGame.GetLength(1); x++)
                {
                    Run.GameFild.FildGame[y, x] = rowFild[x];

                }
            }

            if (Settings.isColorScreen == 0) return;
            Run.GameFild.FCScreen = new FildColor(Run.GameFild);
            for (int i = 0; i < Run.GameFild.FCScreen.FildColorArray.GetLength(0); i++)
            {
                for (int j = 0; j < Run.GameFild.FCScreen.FildColorArray.GetLength(1); j++)
                {
                    if (Run.GameFild.FildGame[i, j] == Settings.keyBuild)
                        Run.GameFild.FCScreen.FildColorArray[i, j] = Settings.ConsColBrick;
                }
            }
        }
    }
}