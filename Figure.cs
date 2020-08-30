namespace TETRISV1
{
    interface IFigures
    {
        //int rollCount;
        public int Next();
        public void RestorForm();
        public void Roll(Fild fg);
        public void RollPosition(int rc, Fild fg);
        public char[,] Form { get; set; }

    }
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

    class LineSizeTwoFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Move.keyBuild},    // +
                        {Move.keyBuild}};   // +
        char[,] Form_2 = new char[,] {
                        {Move.background,Move.background},  // ..
                        {Move.keyBuild,Move.keyBuild}};     // ++


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
        public char[,] Form { get; set; }//= new char[,] { };
        public void RestorForm()
        {
            Form = Form_0;
        }
        public void Roll(Fild fg)
        {
            int cc = fg.FigNow.Next();
            fg.FigNow.RollPosition(cc, fg);
        }
        // +  --
        // +  ++
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    // bool flag = true;
                    // for (int i = 0; i < Form_0.GetLength(0); i++)
                    // {
                    //     for (int j = 0; j < Form_0.GetLength(1); j++)
                    //     {
                    //         if ((Form_0[i, j] == Move.keyBuild) &&
                    //             Move.keyBuild == fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]])
                    //         {
                    //             flag = false; break;
                    //         }
                    //     }
                    //     if (!flag) break;
                    // }
                    if (SupportMethods.Intersection(Form_0, fg))
                    {
                        Form = Form_0; rollCount = Next();
                    }
                    break;
                case (1):
                    Move.DeleteFig(fg);
                    bool flag2 = true, strf = false;
                    strf = Move.dotMove[1] == fg.FildGame.GetLength(1) - 1 ||
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Move.keyBuild &&
                        Move.dotMove[1] != 0);
                    if (strf) Move.dotMove[1]--;
                    for (int i = 0; i < Form_2.GetLength(0); i++)
                    {
                        for (int j = 0; j < Form_2.GetLength(1); j++)
                        {
                            if (Form_2[i, j] == Move.keyBuild)
                                if (Form_2[i, j] == fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]])
                                {
                                    flag2 = false; break;
                                }
                            if (!flag2) break;
                        }
                    }
                    if (!flag2)
                    {
                        if (strf) Move.dotMove[1]++;
                        Move.PrintFig(fg);
                    }
                    else
                    {
                        Form = Form_2; rollCount = Next(); Move.PrintFig(fg);
                    }
                    break;
            }
        }
    }
    static class Angle
    {
        public static char[,] Form = new char[,] {
                {' ', Move.keyBuild,' '},
                {Move.keyBuild, Move.keyBuild,Move.keyBuild} };
    }
    class SFigure : IFigures
    {
        //  01 012 
        //  +-      +--
        //  ++ -++  ++-
        //  -+ ++-  -+-

        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Move.keyBuild, Move.background},
                        {Move.keyBuild,Move.keyBuild},
                        {Move.background,Move.keyBuild}};
        char[,] Form_1 = new char[,] {
                        {Move.background,Move.keyBuild,Move.keyBuild},
                        {Move.keyBuild,Move.keyBuild,Move.background}};
        public int Next()
        {
            if (rollCount < 1) return rollCount + 1;
            else return 0;
        }
        public char[,] Form { get; set; }
        public void RestorForm() => Form = Form_0;
        public void Roll(Fild fg) => fg.FigNow.RollPosition(fg.FigNow.Next(), fg);
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                    else Move.dotMove[0]++;
                    break;
                case (1):
                    bool flagOut1 = false;
                    if (Move.dotMove[1] + fg.FigNow.Form.GetLength(1) == fg.FildGame.GetLength(1) ||
                    fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Move.keyBuild)
                    { Move.dotMove[0]++; Move.dotMove[1]--; flagOut1 = true; }
                    else Move.dotMove[0]++;
                    if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                    else if (flagOut1) { Move.dotMove[0]--; Move.dotMove[1]++; }
                    else Move.dotMove[0]--;
                    break;

            }
        }
    }
    class ReversSFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Move.background, Move.keyBuild},    // -+  
                        {Move.keyBuild, Move.keyBuild},      // ++  ++-
                        {Move.keyBuild, Move.background}};   // +-  -++
        char[,] Form_1 = new char[,] {
                        {Move.keyBuild, Move.keyBuild, Move.background},
                        {Move.background, Move.keyBuild, Move.keyBuild}};
        public int Next()
        {
            if (rollCount < 1) return rollCount + 1;
            else return 0;
        }
        public char[,] Form { get; set; }
        public void RestorForm() => Form = Form_0;
        public void Roll(Fild fg) => fg.FigNow.RollPosition(fg.FigNow.Next(), fg);
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--; Move.dotMove[1]++;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    break;

                case (1):
                    bool flagOut1 = false;
                    if (Move.dotMove[1] == 0 ||
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Move.keyBuild)
                    { Move.dotMove[0]++; flagOut1 = true; }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                    else if (flagOut1) { Move.dotMove[0]--; }
                    else { Move.dotMove[0]--; Move.dotMove[1]++; }
                    break;
            }
        }
    }
    class CubeFigure : IFigures
    {
        public int Next() { return 0; }
        public void RestorForm()
        {
            Form = new char[,] {
                        {Move.keyBuild,Move.keyBuild},
                        {Move.keyBuild,Move.keyBuild}};
        }
        public void Roll(Fild fg) { }
        public void RollPosition(int rc, Fild fg) { }
        public char[,] Form { get; set; }

    }
    class LFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Move.keyBuild,Move.background},    // +.
                        {Move.keyBuild,Move.background},    // +.
                        {Move.keyBuild,Move.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Move.keyBuild,Move.keyBuild,Move.keyBuild},        // +++
                        {Move.keyBuild,Move.background,Move.background}};   // +..
        char[,] Form_2 = new char[,] {
                        {Move.keyBuild,Move.keyBuild},      // ++
                        {Move.background,Move.keyBuild},    // .+
                        {Move.background,Move.keyBuild}};   // .+
        char[,] Form_3 = new char[,] {
                        {Move.background,Move.background,Move.keyBuild},    // ..+
                        {Move.keyBuild,Move.keyBuild,Move.keyBuild}};       // +++
        public int Next()
        {
            if (rollCount < 3) return rollCount + 1;
            else return 0;
        }
        public char[,] Form { get; set; }
        public void RestorForm() => Form = Form_0;
        public void Roll(Fild fg) => fg.FigNow.RollPosition(fg.FigNow.Next(), fg);
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--; Move.dotMove[1]++;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    break;
                case (1):
                    bool flagOut1 = false;
                    if ((Move.dotMove[1] > fg.FildGame.GetLength(1) - 3) ||
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Move.keyBuild)
                    { Move.dotMove[0]++; Move.dotMove[1]--; flagOut1 = true; }
                    else Move.dotMove[0]++;
                    if (SupportMethods.Intersection(Form_1, fg))
                    { Form = Form_1; rollCount = Next(); }
                    else
                    {
                        if (flagOut1) { Move.dotMove[0]--; Move.dotMove[1]++; }
                        else Move.dotMove[0]--;
                    }
                    break;
                case (2):
                    bool flagOut2 = false;
                    if (Move.dotMove[0] + fg.FigNow.Form.GetLength(0) == fg.FildGame.GetLength(0) ||
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Move.keyBuild)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = Next(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Move.keyBuild)
                        flagOut3 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                    else if (!flagOut3) Move.dotMove[1]++;
                    break;
            }
        }
    }
    class ReversLFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Move.background,Move.keyBuild},    // .+
                        {Move.background,Move.keyBuild},    // .+
                        {Move.keyBuild,Move.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Move.keyBuild,Move.background,Move.background},    // +..
                        {Move.keyBuild,Move.keyBuild,Move.keyBuild}};       // +++
        char[,] Form_2 = new char[,] {
                        {Move.keyBuild,Move.keyBuild},      // ++
                        {Move.keyBuild,Move.background},    // +.
                        {Move.keyBuild,Move.background}};   // +.
        char[,] Form_3 = new char[,] {                                      // .*.
                        {Move.keyBuild,Move.keyBuild,Move.keyBuild},        // +++
                        {Move.background,Move.background,Move.keyBuild}};   // .*+
        public int Next()
        {
            if (rollCount < 3) return rollCount + 1;
            else return 0;
        }
        public char[,] Form { get; set; }
        public void RestorForm() => Form = Form_0;
        public void Roll(Fild fg) => fg.FigNow.RollPosition(fg.FigNow.Next(), fg);
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--; Move.dotMove[1]++;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    break;
                case (1):
                    bool flagOut1 = false;
                    if ((Move.dotMove[1] > fg.FildGame.GetLength(1) - 3) ||
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Move.keyBuild)
                    { Move.dotMove[0]++; Move.dotMove[1]--; flagOut1 = true; }
                    else Move.dotMove[0]++;
                    if (SupportMethods.Intersection(Form_1, fg))
                    { Form = Form_1; rollCount = Next(); }
                    else
                    {
                        if (flagOut1) { Move.dotMove[0]--; Move.dotMove[1]++; }
                        else Move.dotMove[0]--;
                    }
                    break;
                case (2):
                    bool flagOut2 = false;
                    if (Move.dotMove[0] + fg.FigNow.Form.GetLength(0) == fg.FildGame.GetLength(0) ||
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == Move.keyBuild)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = Next(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0], Move.dotMove[1] - 1] == Move.keyBuild) flagOut3 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                    else if (!flagOut3) Move.dotMove[1]++;
                    break;
            }
        }
    }

    class SupportMethods
    {
        public static bool Intersection(char[,] form_x, Fild fg)
        {
            bool flag = true;
            for (int i = 0; i < form_x.GetLength(0); i++)
            {
                for (int j = 0; j < form_x.GetLength(1); j++)
                {
                    if ((form_x[i, j] == Move.keyBuild) &&
                        Move.keyBuild == fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]])
                    {
                        flag = false; break;
                    }
                }
                if (!flag) break;
            }
            return flag;
        }
    }
}
