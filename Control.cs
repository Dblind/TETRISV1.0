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
                case ('k'):
                    if (Move.CheckRight(GameFild)) Move.MoveRight(GameFild);
                    break;
                case ('h'):
                    if (Move.CheckLeft(GameFild)) Move.MoveLeft(GameFild);
                    break;
                case ('j'):
                    if (Move.CheckDowd(GameFild)) {Move.MoveDowd(GameFild); Run.count = 0;}
                    else GameFild.NewFigure();
                    break;
                case ('u'):
                    GameFild.FigNow.Roll(GameFild);
                    break;
                case ('q'):
                    Fild.RunGame = false;
                    break;
            }
        }

    }
}