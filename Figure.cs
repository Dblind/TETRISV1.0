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
    class LineSizeTwoFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Setting.keyBuild},    // +
                        {Setting.keyBuild}};   // +
        char[,] Form_2 = new char[,] {
                        {Setting.background,Setting.background},  // ..
                        {Setting.keyBuild,Setting.keyBuild}};     // ++


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
                    if (SupportMethods.Intersection(Form_0, fg))
                    {
                        Form = Form_0; rollCount = Next();
                    }
                    break;
                case (1):
                    Move.DeleteFig(fg);
                    bool flag2 = true, strf = false;
                    strf = Move.dotMove[1] == fg.FildGame.GetLength(1) - 1 ||
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Setting.keyBuild &&
                        Move.dotMove[1] != 0);
                    if (strf) Move.dotMove[1]--;
                    for (int i = 0; i < Form_2.GetLength(0); i++)
                    {
                        for (int j = 0; j < Form_2.GetLength(1); j++)
                        {
                            if (Form_2[i, j] == Setting.keyBuild)
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
    class LineFigure : IFigures
    {
        int rollCount = 1;
        char[,] Form_0 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild,Setting.keyBuild}};
        // ....
        // ....
        // ++++
        // ....
        char[,] Form_1 = new char[,] {
                        {Setting.keyBuild},    // .+..
                        {Setting.keyBuild},    // .+..
                        {Setting.keyBuild},    // .+.+
                        {Setting.keyBuild}};   // .+..

        public int Next() => rollCount > 0 ? 0 : 1;
        public char[,] Form { get; set; }
        public void RestorForm() => Form = Form_1;
        public void Roll(Fild fg) => fg.FigNow.RollPosition(Next(), fg);
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                //  0123456789
                //  -+----+--
                //  10b4-01b9
                case (0):
                    int flagOut0 = 0;
                    if (0 < Move.dotMove[1]) flagOut0 = 0b10000;
                    if (fg.FildGame.GetLength(1) - 2 > Move.dotMove[1]) flagOut0 |= 0b01000;//  9 - 2 (7) > 6 
                    if (Move.dotMove[1] > 0 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == Setting.keyBuild)
                        flagOut0 |= 0b00100;    // left block
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 1 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Setting.keyBuild)
                        flagOut0 |= 0b00010;    // right block +1
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Setting.keyBuild)
                        flagOut0 |= 0b00001;    // right block +2

                    if (Move.dotMove[1] == 0 || (flagOut0 & 0b11100) == 0b11100)    // zero, left
                    {
                        Move.dotMove[0] += 2;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else Move.dotMove[0] -= 2;
                    }
                    else if ((flagOut0 & 0b10010) == 0b10010)       //right +1
                    {
                        Move.dotMove[0] += 2; Move.dotMove[1] -= 3;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1] += 3; }
                    }
                    else if ((flagOut0 & 0b10001) == 0b10001)       //right +2
                    {
                        Move.dotMove[0] += 2; Move.dotMove[1] -= 2;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1] += 2; }
                    }
                    else if ((flagOut0 & 0b01000) != 0b01000)       //right wall
                    {
                        int boxCoordColumn = Move.dotMove[1];
                        Move.dotMove[0] += 2; Move.dotMove[1] = fg.FildGame.GetLength(1) - 4;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1] = boxCoordColumn; }
                    }
                    else        //default empty
                    {
                        Move.dotMove[0] += 2; Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1]++; }
                    }
                    break;
                case (1):
                    if (Move.dotMove[0] != fg.FildGame.GetLength(0) - 1)
                    {
                        Move.dotMove[0] -= 2; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else { Move.dotMove[0] += 2; Move.dotMove[1]--; }
                    }
                    break;
            }
        }
    }
    static class Angle
    {
        public static char[,] Form = new char[,] {
                {' ', Setting.keyBuild,' '},
                {Setting.keyBuild, Setting.keyBuild,Setting.keyBuild} };
    }
    class SFigure : IFigures
    {
        //  01 012 
        //  +-      +--
        //  ++ -++  ++-
        //  -+ ++-  -+-

        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Setting.keyBuild, Setting.background},
                        {Setting.keyBuild,Setting.keyBuild},
                        {Setting.background,Setting.keyBuild}};
        char[,] Form_1 = new char[,] {
                        {Setting.background,Setting.keyBuild,Setting.keyBuild},
                        {Setting.keyBuild,Setting.keyBuild,Setting.background}};
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
                    fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Setting.keyBuild)
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
                        {Setting.background, Setting.keyBuild},    // -+  
                        {Setting.keyBuild, Setting.keyBuild},      // ++  ++-
                        {Setting.keyBuild, Setting.background}};   // +-  -++
        char[,] Form_1 = new char[,] {
                        {Setting.keyBuild, Setting.keyBuild, Setting.background},
                        {Setting.background, Setting.keyBuild, Setting.keyBuild}};
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
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Setting.keyBuild)
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
                        {Setting.keyBuild,Setting.keyBuild},
                        {Setting.keyBuild,Setting.keyBuild}};
        }
        public void Roll(Fild fg) { }
        public void RollPosition(int rc, Fild fg) { }
        public char[,] Form { get; set; }

    }
    class LFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild},        // +++
                        {Setting.keyBuild,Setting.background,Setting.background}};   // +..
        char[,] Form_2 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild},      // ++9#
                        {Setting.background,Setting.keyBuild},    // .+89
                        {Setting.background,Setting.keyBuild}};   // .+..#
        char[,] Form_3 = new char[,] {
                        {Setting.background,Setting.background,Setting.keyBuild},    // ..+
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild}};       // +++
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
                    if (fg.FildGame[Move.dotMove[0], Move.dotMove[1]] == Setting.keyBuild &&
                        fg.FildGame[Move.dotMove[0] - 1, Move.dotMove[1]] == Setting.keyBuild)
                    {
                        Move.dotMove[0]--; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    }
                    else
                    {
                        Move.dotMove[0]--;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else Move.dotMove[0]++;
                    }
                    break;
                case (1):
                    if (fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 1] == Setting.keyBuild && Move.dotMove[1] > 1)
                    {
                        Move.dotMove[1] -= 2;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else Move.dotMove[1] += 2;
                    }
                    else if (((Move.dotMove[1] < fg.FildGame.GetLength(1) - 2) &&
                            fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 2] == Setting.keyBuild && Move.dotMove[1] > 0) ||
                             Move.dotMove[1] > fg.FildGame.GetLength(1) - 3)
                    {
                        Move.dotMove[1] -= 1;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else Move.dotMove[1] += 1;
                    }
                    else
                    {
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                    }
                    break;
                case (2):
                    if (Move.dotMove[1] > 0 && Move.dotMove[0] < fg.FildGame.GetLength(0) - 2 &&        // +++
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Setting.keyBuild ||   // +*.
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Setting.keyBuild))     // .*.
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = Next(); }
                        else { Move.dotMove[1]++; }
                    }
                    else if (Move.dotMove[0] < fg.FildGame.GetLength(0) - 2 &&                              // +++
                            (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Setting.keyBuild ||   // +.*
                            fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Setting.keyBuild))     // ..*
                    {
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = Next(); }
                    }
                    else
                    {
                        bool flagOut2 = false;
                        if (Move.dotMove[0] == fg.FildGame.GetLength(0) - 2) flagOut2 = true;
                        if (flagOut2) Move.dotMove[0]--;
                        Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = Next(); }
                        else
                        {
                            if (flagOut2) Move.dotMove[0]++;
                            else Move.dotMove[1]--;
                        }
                    }
                    break;
                case (3):
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 3 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == Setting.keyBuild)
                    {
                        Move.dotMove[0]++; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]--; }
                    }
                    else if ((Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 && Move.dotMove[1] > 0 &&
                         fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == Setting.keyBuild) ||
                         Move.dotMove[1] == 0)
                    {
                        Move.dotMove[0]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else Move.dotMove[0]--;
                    }
                    else
                    {
                        Move.dotMove[0]++; Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]++; }
                    }
                    break;
            }
        }
    }
    class LFigureV2 : IFigures      // have bug
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild},        // +++
                        {Setting.keyBuild,Setting.background,Setting.background}};   // +..
        char[,] Form_2 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild},      // ++9#
                        {Setting.background,Setting.keyBuild},    // .+89
                        {Setting.background,Setting.keyBuild}};   // .+..#
        char[,] Form_3 = new char[,] {
                        {Setting.background,Setting.background,Setting.keyBuild},    // ..+
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild}};       // +++
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
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Setting.keyBuild)
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Setting.keyBuild)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = Next(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Setting.keyBuild)
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
                        {Setting.background,Setting.keyBuild},    // .+
                        {Setting.background,Setting.keyBuild},    // .+
                        {Setting.keyBuild,Setting.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Setting.keyBuild,Setting.background,Setting.background},    // +..
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild}};       // +++
        char[,] Form_2 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild},      // ++
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.background}};   // +.
        char[,] Form_3 = new char[,] {                                               // .*.
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild},        // +++
                        {Setting.background,Setting.background,Setting.keyBuild}};   // .*+
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
                    if (Move.dotMove[0] < fg.FildGame.GetLength(0) - 2 &&
                        (fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Setting.keyBuild ||
                         fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Setting.keyBuild))
                    {
                        Move.dotMove[0]--; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    }
                    else if (Move.dotMove[0] > fg.FildGame.GetLength(0) - 3)
                    {
                        Move.dotMove[0]--; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    }
                    else
                    {
                        Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                        else Move.dotMove[1]--;
                    }
                    break;
                case (1):
                    if (Move.dotMove[1] == 0)
                    {
                        Move.dotMove[0]++;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else { Move.dotMove[0]--; }
                    }
                    else if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&       // offset right
                     (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Setting.keyBuild ||
                     fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == Setting.keyBuild))
                    {
                        Move.dotMove[0]++;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else Move.dotMove[0]--;
                    }
                    else
                    {
                        Move.dotMove[0]++; Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]++; }
                    }
                    break;
                case (2):
                    Move.dotMove[0]--;
                    if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = Next(); }
                    else Move.dotMove[0]++;
                    break;
                case (3):
                    if (Move.dotMove[1] == fg.FildGame.GetLength(1) - 2 &&
                         fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Setting.keyBuild)
                    {
                        Move.dotMove[0] -= 2;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else Move.dotMove[1] += 2;
                    }
                    else if (Move.dotMove[1] == fg.FildGame.GetLength(1) - 2)
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else { Move.dotMove[1]++; }
                    }
                    else if (Move.dotMove[1] > 0 && Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&
                        (fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 2] == Setting.keyBuild ||
                         fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Setting.keyBuild))
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else Move.dotMove[1]++;
                    }
                    else if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }

                    break;
            }
        }
    }
    class ReversLFigureV2 : IFigures        // have bug
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Setting.background,Setting.keyBuild},    // .+
                        {Setting.background,Setting.keyBuild},    // .+
                        {Setting.keyBuild,Setting.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Setting.keyBuild,Setting.background,Setting.background},    // +..
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild}};       // +++
        char[,] Form_2 = new char[,] {
                        {Setting.keyBuild,Setting.keyBuild},      // ++
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.background}};   // +.
        char[,] Form_3 = new char[,] {                                      // .*.
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild},        // +++
                        {Setting.background,Setting.background,Setting.keyBuild}};   // .*+
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Setting.keyBuild)
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == Setting.keyBuild)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = Next(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0], Move.dotMove[1] - 1] == Setting.keyBuild) flagOut3 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                    else if (!flagOut3) Move.dotMove[1]++;
                    break;
            }
        }
    }


    class TFigure : IFigures
    {
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Setting.background,Setting.background,Setting.background},  // ...
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild},        // +++
                        {Setting.background,Setting.keyBuild,Setting.background}};   // .+.
        char[,] Form_1 = new char[,] {
                        {Setting.background,Setting.keyBuild},    // .+
                        {Setting.keyBuild,Setting.keyBuild},      // ++
                        {Setting.background,Setting.keyBuild}};   // .+
        char[,] Form_2 = new char[,] {
                        {Setting.background,Setting.keyBuild,Setting.background},    // .+.
                        {Setting.keyBuild,Setting.keyBuild,Setting.keyBuild}};       // +++
        char[,] Form_3 = new char[,] {
                        {Setting.keyBuild,Setting.background},    // +.
                        {Setting.keyBuild,Setting.keyBuild},      // ++
                        {Setting.keyBuild,Setting.background}};   // +.
        public int Next() => rollCount < 3 ? rollCount + 1 : 0;
        public char[,] Form { get; set; }
        public void RestorForm() => Form = Form_0;
        public void Roll(Fild fg) => RollPosition(Next(), fg);
        public void RollPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    bool flagOut0 = false;
                    if (Move.dotMove[1] == 0 ||
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Setting.keyBuild)
                        flagOut0 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = Next(); }
                    else if (!flagOut0) Move.dotMove[1]++;
                    break;
                case (1):
                    if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                    break;
                case (2):
                    bool flagOut2 = false;
                    if (Move.dotMove[1] + 3 > fg.FildGame.GetLength(1) ||
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Setting.keyBuild)
                    { Move.dotMove[1]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = Next(); }
                    else { if (flagOut2) Move.dotMove[1]++; }
                    break;
                case (3):
                    if (fg.FildGame.GetLength(0) - 2 > Move.dotMove[0])
                    {
                        Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else Move.dotMove[1]--;
                    }
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
                    if ((form_x[i, j] == Setting.keyBuild) &&
                        Setting.keyBuild == fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]])
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
