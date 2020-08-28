using System;
namespace TETRISV1
{
    static class Control
    {
        public static void Push(ConsoleKeyInfo key, Fild GameFild)
        {
            char ch = key.KeyChar;

            switch (ch)
            {
                case ('d'):
                    if (Move.CheckRight(GameFild)) Move.MoveRight(GameFild);
                    break;
                case ('a'):
                    if (Move.CheckLeft(GameFild)) Move.MoveLeft(GameFild);
                    break;
                case ('s'):
                    if (Move.CheckDowd(GameFild)) Move.MoveDowd(GameFild);
                    else GameFild.NewFigure();
                    break;
                case (' '):
                    GameFild.FigNow.Roll(GameFild);
                    break;
                case ('q'):
                    Fild.RunGame = false;
                    break;
            }
        }

    }
}