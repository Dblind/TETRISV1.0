using System;
namespace TETRISV1
{
    static class Control
    {
        public static void Push(ConsoleKeyInfo key, Fild GameFild)
        {
            char ch = key.KeyChar;
            if (ch.Equals(Settings.controlsKeys[0])) ch = 'h';
            else if (ch.Equals(Settings.controlsKeys[1])) ch = 'k';
            else if (ch.Equals(Settings.controlsKeys[2])) ch = 'j';
            else if (ch.Equals(Settings.controlsKeys[3])) ch = 'u';
            else if (ch.Equals(Settings.controlsKeys[4])) ch = 'q';
            else return;

            switch (ch)
            {
                case ('h'):
                    if (Move.CheckLeft(GameFild))
                    {

                        Move.MoveLeft(GameFild);
                        GameFild.Display();
                    }
                    break;
                case ('k'):
                    if (Move.CheckRight(GameFild))
                    {
                        Move.MoveRight(GameFild);
                        GameFild.Display();
                    }
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
                    GameFild.Display();
                    break;
                case ('u'):
                    GameFild.FigNow.Roll(GameFild);
                    GameFild.Display();
                    break;
                case ('q'):
                    GameFild.RunGame = false;
                    Console.ResetColor();
                    Console.CursorVisible = true;
                    break;
            }
        }
    }
}