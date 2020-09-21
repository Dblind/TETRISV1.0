using System;
namespace TETRISV1
{
    static class Control
    {
        public static int Score { get; set; }
        public static void Push(ConsoleKeyInfo key, Fild GameFild)
        {
            char ch = key.KeyChar;
            if (ch.Equals(Settings.controlKeys[0])) ch = 'h';
            else if (ch.Equals(Settings.controlKeys[1])) ch = 'k';
            else if (ch.Equals(Settings.controlKeys[2])) ch = 'j';
            else if (ch.Equals(Settings.controlKeys[3])) ch = 'u';
            else if (ch.Equals(Settings.controlKeys[4])) ch = 'q';
            else return;

            switch (ch)
            {
                case ('h'):
                    if (Move.CheckLeft(GameFild)) Move.MoveLeft(GameFild);
                    break;
                case ('k'):
                    if (Move.CheckRight(GameFild)) Move.MoveRight(GameFild);
                    break;
                case ('j'):
                    if (Move.CheckDowd(GameFild))
                    {
                        if (!Run.FlagFastFall) Move.MoveDowd(GameFild);
                        Run.FlagFastFall = !Run.FlagFastFall;
                        Run.count = 0;
                        Run.StepFall = Run.FlagFastFall ? 10 : 99;
                    }
                    else GameFild.NewFigure();
                    break;
                case ('u'):
                    GameFild.FigNow.Roll(GameFild);
                    break;
                case ('q'):
                    GameFild.RunGame = false;
                    Score = 0;
                    Console.ResetColor();
                    Console.CursorVisible = true;
                    Save.SeveGame();
                    break;
            }
        }

    }
}