using System.Threading;

namespace TETRISV1
{

    static class Move
    {
        public static int startMove;
        public static void Wait(double T) => Thread.Sleep((int)(T * 1000));
        public static int[] dotMove = new int[2];
        public static char keyBuild = '$', keyBottom = '#', background = '.';
        public static bool CheckDowd(Fild fg)
        {
            // [j, q]
            // var ch = ' ';
            // for (int q = 0; q < fg.FigNow.Form.GetLength(1); q++)
            // {
            //     for (int j = fg.FigNow.Form.GetLength(0) - 1; j > -1; j--)
            //     {
            //         ch = fg.FigNow.Form[j, q];
            //         if (ch == keyBuild)
            //         {
            //             ch = fg.FildGame[Move.dotMove[0] + j + 1, Move.dotMove[1] + q];
            //             if (ch == keyBuild || ch == keyBottom) return false;
            //             j = 0;
            //         }
            //     }
            // }
            // return true;
            if (Move.dotMove[0] + fg.FigNow.Form.GetLength(0) == fg.FildGame.GetLength(0))
                return false;
            else
            {
                Move.dotMove[0]++;
                if (SupportMethods.Intersection(fg.FigNow.Form, fg))
                { Move.dotMove[0]--; return true; }
                else { Move.dotMove[0]--; return false; }

            }
        }
        public static void MoveDowd(Fild fg)
        {
            // [i, j]            
            Move.dotMove[0]++;
        }

        public static bool CheckLeft(Fild fg)
        {
            //var ch = ' ';
            if (Move.dotMove[1] == 0) return false;
            // for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            // {
            //     for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
            //     {
            //         ch = fg.FildGame[Move.dotMove[0] + i, Move.dotMove[1] + j];  //(?)
            //         if (ch == keyBuild)
            //         {
            //             ch = fg.FildGame[Move.dotMove[0] + i, Move.dotMove[1] + j - 1];
            //             if (ch == keyBuild) return false;
            //             j = fg.FigNow.Form.GetLength(1);
            //         }
            //     }
            // }
            // return true;
            else
            {
                Move.dotMove[1]--;
                if (SupportMethods.Intersection(fg.FigNow.Form, fg))
                {
                    Move.dotMove[1]++; return true;
                }
                else { Move.dotMove[1]++; return false; }

            }
        }
        public static void MoveLeft(Fild fg)
        {
            Move.dotMove[1]--;
        }
        public static bool CheckRight(Fild fg)
        {
            if (Move.dotMove[1] + fg.FigNow.Form.GetLength(1) == fg.FildGame.GetLength(1)) return false;
            //var ch = ' ';
            // for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            // {
            //     for (int j = fg.FigNow.Form.GetLength(1) - 1; j > -1; j--)
            //     {
            //         ch = fg.FigNow.Form[i, j];
            //         if (ch == keyBuild)
            //         {
            //             ch = fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1] + 1];
            //             if (ch == keyBuild) return false;
            //             j = 0;
            //         }
            //     }
            // }
            // return true;
            else
            {
                Move.dotMove[1]++;
                if (SupportMethods.Intersection(fg.FigNow.Form, fg))
                {
                    Move.dotMove[1]--; return true;
                }
                else { Move.dotMove[1]--; return false; }

            }
        }
        public static void MoveRight(Fild fg)
        {
            Move.dotMove[1]++;
        }
        // public static bool CheckRoll(Fild fg)
        // {
        //     DeleteFig(fg);
        //     char[,] fig = fg.FigNow.RollPosition(fg.FigNow.Next());
        //     for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
        //     {
        //         for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
        //         {
        //             if(fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]] == fig[i,j])
        //             {
        //                 PrintFig(fg);
        //                 return false;
        //             }
        //         }                
        //     }
        //     return true;
        // }

        public static void DeleteFig(Fild fg)
        {
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = background;
                }
            }
        }
        public static void PrintFig(Fild fg)
        {
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == keyBuild)
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = keyBuild;
                }
            }
        }
        public static void CheckRows(Fild fg)
        {
            Move.PrintFig(fg);
            var flag = 0b11;
            for (int i = 0; i < fg.FildGame.GetLength(0); i++)
            {
                flag = flag | 0b10;
                for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    if (fg.FildGame[i, j] == Move.background) { flag = flag & 0b01; break; }
                }
                if ((flag & 0b10) == 0b10)
                {
                    FallWall(i); Control.Score += 100;
                }
            }
            void FallWall(int row)
            {
                for (int i = row; i > 0; i--)
                {
                    for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                    {
                        fg.FildGame[i, j] = fg.FildGame[i - 1, j];
                    }
                }
                for (int i = 0, j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    fg.FildGame[i, j] = Move.background;
                }
            }
        }
    }
}

/*
    01234567
   0-------- 1
   1-**--**- 2
   2-*---*-- 3
   3-*------ 4
   4++++++++

*/