namespace TETRISV1
{
    // abstract class Figures
    // {
    //     abstract public string name { get; set; }

    //     //abstract public char[,] Form;
    // }

    // class Squire : Figures
    // {
    //     override public string name { get { return name; } set { value = "Squire"; } }

    //     //override public char[,] Form;//{ get; set; }
    //     // {
    //     //     get { return Form; }
    //     //     set
    //     //     {
    //     //         char[,] a = {
    //     //             {'+', '+'},
    //     //             {'+', '+'}
    //     //             };
    //     //         value = a;
    //     //     }
    //     // }

    //     //public Squire()
    //     // {
    //     //     Form = new char[,] {
    //     //             {'+', '+'},
    //     //             {'+', '+'} };


    //     // }
    // }

    class LineSizeTwo// : Figures
    {
        public string name { get; set; }

        public char[,] Form;
        public LineSizeTwo()
        {
            Form = new char[,] {
                    {Move.keyBuild},
                    {Move.keyBuild} };
        }
    }
    static class Angle
    {
        public static char[,] Form = new char[,] {
                {' ', Move.keyBuild,' '},
                {Move.keyBuild, Move.keyBuild,Move.keyBuild} };
    }

    class SFigure
    {
        // --- -+-  +--
        // -++ -++  ++-
        // ++- --+  -+-

        int rollCount = 0;
        public int Next()
        {
            if (rollCount < 1)
            {
                return rollCount + 1;
            }
            else
            {
                return 0;
            }
        }
        public char[,] Form = new char[,] { };
        public void RestorForm()
        {
            Form = new char[,] {
                        {Move.background, Move.background, Move.background},
                        {Move.background,Move.keyBuild,Move.keyBuild},
                        {Move.keyBuild,Move.keyBuild,Move.background}};
        }
        public void Roll(Fild fg)
        {
            int cc = fg.FigNow.Next();
            fg.FigNow.RollPosition(cc, fg);
        }
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    var f1 = new char[,] {
                        {Move.background, Move.background, Move.background},
                        {Move.background,Move.keyBuild,Move.keyBuild},
                        {Move.keyBuild,Move.keyBuild,Move.background}};
                    Move.DeleteFig(fg);
                    int check0 = Move.dotMove[1];
                    if (check0 != 0) Move.dotMove[1]--;
                    bool flag = true; char ch = ' ';
                    for (int i = 0; i < f1.GetLength(0); i++)
                    {
                        for (int j = 0; j < f1.GetLength(1); j++)
                        {
                            ch = fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]];
                            if (ch == Move.keyBuild && ch == f1[i, j])
                            {
                                flag = false; break;
                            }
                        }
                        if (!flag) break;
                    }
                    if (!flag)
                    {
                        if (check0 != 0) Move.dotMove[1]++;
                        Move.PrintFig(fg);
                    }
                    else
                    {
                        Form = f1; rollCount = Next();
                        Move.PrintFig(fg);
                    }
                    break;

                case (1):
                    var f2 = new char[,] {
                        {Move.keyBuild, Move.background},
                        {Move.keyBuild,Move.keyBuild},
                        {Move.background,Move.keyBuild}};
                    Move.DeleteFig(fg);
                    Move.dotMove[1]++;
                    bool flag2 = true;
                    for (int i = 0; i < f2.GetLength(0); i++)
                    {
                        for (int j = 0; j < f2.GetLength(1); j++)
                        {
                            if (fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]] == Move.keyBuild)
                            {
                                flag = false; break;
                            }
                        }
                        if (!flag2) break;
                    }
                    if (!flag2)
                    {
                        Move.dotMove[1]--;
                        Move.PrintFig(fg);
                    }
                    else
                    {
                        Form = f2;
                        rollCount = Next();
                        Move.PrintFig(fg);
                    }
                    break;
            }
        }
    }
}
