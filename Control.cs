using System;
namespace TETRISV1
{
    static class Control
    {
        public static int Score { get; set; }
        public static void Push(ConsoleKeyInfo key, Fild GameFild)
        {
            char ch = key.KeyChar;

            switch (ch)
            {
                case ('k'):
                    if (Move.CheckRight(GameFild)) Move.MoveRight(GameFild);
                    break;
                case ('h'):
                    if (Move.CheckLeft(GameFild)) Move.MoveLeft(GameFild);
                    break;
                case ('j'):
                    if (Move.CheckDowd(GameFild))
                    {
                        if (!Run.FlagFastFall) Move.MoveDowd(GameFild); 
                        Run.FlagFastFall = !Run.FlagFastFall;
                        Run.count = 0;
                        Run.StepFall = Run.FlagFastFall ? 11 : 99;
                    }
                    else GameFild.NewFigure();
                    break;
                case ('u'):
                    GameFild.FigNow.Roll(GameFild);
                    break;
                case ('q'):
                    Fild.RunGame = false;
                    Console.CursorVisible = true;
                    break;
            }
        }

    }
}