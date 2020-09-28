using System.Threading;

namespace TETRISV1
{

    static class Move
    {
        public static int startMovePoint;
        public static void Wait(double T) => Thread.Sleep((int)(T * 1000));
        public static int[] dotMove = new int[2];
        public static bool CheckDowd(Fild fg)
        {
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
            Move.dotMove[0]++;
        }

        public static bool CheckLeft(Fild fg)
        {
            if (Move.dotMove[1] == 0) return false;
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
        public static void TakeOutFigForm(Fild fg)
        {
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == true)
                    {
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = false;
                        if (fg.DelBrickColor != null)
                            fg.DelBrickColor(i + dotMove[0], j + dotMove[1], Settings.ConsColBackground);
                    }
                }
            }
        }
        public static void SetFigForm(Fild fg)
        {
            for (int i = 0; i < fg.FigNow.Form.GetLength(0); i++)
            {
                for (int j = 0; j < fg.FigNow.Form.GetLength(1); j++)
                {
                    if (fg.FigNow.Form[i, j] == true)
                    {
                        fg.FildGame[i + dotMove[0], j + dotMove[1]] = true;
                        if (fg.AddBrickColor != null)
                            fg.AddBrickColor(dotMove[0] + i, dotMove[1] + j, fg.FigNow.FigureColor);
                    }
                }
            }
        }
        public static void CheckRows(Fild fg)
        {
            Move.SetFigForm(fg);
            var flag = 0b11;
            for (int i = 0; i < fg.FildGame.GetLength(0); i++)
            {
                flag = flag | 0b10;
                for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    if (fg.FildGame[i, j] == false) { flag = flag & 0b01; break; }
                }
                if ((flag & 0b10) == 0b10)
                {
                    FallWall(i); fg.Score += fg.FildGame.GetLength(1);
                }
            }
            void FallWall(int row)
            {
                for (int i = row; i > 0; i--)
                {
                    for (int j = 0; j < fg.FildGame.GetLength(1); j++)
                    {
                        fg.FildGame[i, j] = fg.FildGame[i - 1, j];
                        if (Run.GameFild.FallWallColorScreen != null)
                            Run.GameFild.FallWallColorScreen(i, j);
                    }
                }
                for (int i = 0, j = 0; j < fg.FildGame.GetLength(1); j++)
                {
                    fg.FildGame[i, j] = false;
                    if (Run.GameFild.ClearUpLine != null)
                        Run.GameFild.ClearUpLine(i, j);
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