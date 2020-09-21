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
            for (int i = 0; i < Run.GameFild.FildGame.GetLength(0); i++)
            {
                for (int j = 0; j < Run.GameFild.FildGame.GetLength(1); j++)
                {
                    sw.Write(Run.GameFild.FildGame[i,j]);
                }
                sw.WriteLine();
            }
            sw.Close();
        }
    }

    class Load
    {

    }
}