using System.Threading;

namespace TETRISV1
{

    static class Move
    {
        public static int startMove;
        public static void Wait(double T) => Thread.Sleep((int)(T * 1000));
        public static int[] dotMove = new int[2];
        public static char keyBuild = '$', keyBottom = '#', background = ' ';
        public static bool CheckDowd(Fild fg)
        {
            // [j, q]
            var ch = ' ';
            for (int q = 0; q < fg.FigNow.Form.GetLength(1); q++)
            {
                for (int j = fg.FigNow.Form.GetLength(0) - 1; j > -1; j--)
                {
                    ch = fg.FigNow.Form[j, q];
                    if (ch == keyBuild)
                    {
                        ch = fg.FildGame[Move.dotMove[0] + j + 1, Move.dotMove[1] + q];
                        if (ch == keyBuild || ch == keyBottom) return false;
                        j = 0;
                    }
                }
            }
            return true;
        }
        public static void MoveDowd(Fild fg)
        {
            // [i, j]
            // bool flag = false;
            // for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
            // {
            //     for (int i = fg.FigNow.Form.GetLength(0) - 1; i > -1; i--)
            //     {
            //         if (!flag) if (fg.FigNow.Form[i, j] == keyBuild) flag = true;
            //         if (flag) fg.FildGame[i + Move.dotMove[0] + 1, j + Move.dotMove[1]] =
            //                   fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]];
            //     }
            //     flag = false;
            // }
            // for (int i = 0; i < fg.FigNow.Form.GetLength(1); i++)
            // {
            //     fg.FildGame[Move.dotMove[0], i + Move.dotMove[1]] = Move.background;
            // }
            // Move.dotMove[0]++;
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = background;
                }
            }
            Move.dotMove[0]++;
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = keyBuild;
                }
            }
        }

        public static bool CheckLeft(Fild fg)
        {
            var ch = ' ';
            if (Move.dotMove[1] == 0) return false;
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    ch = fg.FildGame[Move.dotMove[0] + i, Move.dotMove[1] + j];
                    if (ch == keyBuild)
                    {
                        ch = fg.FildGame[Move.dotMove[0] + i, Move.dotMove[1] + j - 1];
                        if (ch == keyBuild) return false;
                        j = fg.FigNow.Form.GetLength(1);
                    }
                }
            }
            return true;
        }
        public static void MoveLeft(Fild fg)
        {
            // bool flag = false;
            // for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            // {
            //     for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
            //     {
            //         if (!flag) if (fg.FigNow.Form[i, j] == keyBuild) flag = true;
            //         if (flag) fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1] - 1] =
            //                    fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]];
            //     }
            //     flag = false;
            // }
            // for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            // {
            //     fg.FildGame[i + Move.dotMove[0],
            //                 Move.dotMove[1] + fg.FigNow.Form.GetLength(1) - 1] = Move.background;
            // }
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = background;
                }
            }
            Move.dotMove[1]--;
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = keyBuild;
                }
            }
        }
        public static bool CheckRight(Fild fg)
        {
            if (Move.dotMove[1] + fg.FigNow.Form.GetLength(1) == fg.FildGame.GetLength(1)) return false;
            var ch = ' ';
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = fg.FigNow.Form.GetLength(1) - 1; j > -1; j--)
                {
                    ch = fg.FigNow.Form[i, j];
                    if (ch == keyBuild)
                    {
                        ch = fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1] + 1];
                        if (ch == keyBuild) return false;
                        j = 0;
                    }
                }
            }
            return true;
        }
        public static void MoveRight(Fild fg)
        {
            bool flag = false;
            // for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            // {
            //     for (int j = fg.FigNow.Form.GetLength(1) - 1; j > -1; j--)
            //     {
            //         if (!flag) if (fg.FigNow.Form[i, j] == keyBuild) flag = true;
            //         if (flag) if ((fg.FigNow.Form[i, j] == keyBuild))
            //                             fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1] + 1] =
            //                               fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]];
            //     }
            // }
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = background;
                }
            }
            // for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            // {
            //     fg.FildGame[i + Move.dotMove[0],
            //                 Move.dotMove[1]] = Move.background;
            // }
            Move.dotMove[1]++;
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = keyBuild;
                }
            }
        }
    }
}

/*
    01234567
   0--------
   1-**--**-
   2-*---*--
   3-*------
   4++++++++

*/