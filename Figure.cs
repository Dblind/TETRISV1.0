using System;

namespace TETRISV1
{
    interface IFigures
    {
        //int rollCount;
        public ConsoleColor FigureColor { get; set; }//= ConsoleColor.Cyan;

        public int Next();
        public void RestorForm();
        public void Roll(Fild fg);
        public void RollPosition(int rc, Fild fg);
        public char[,] Form { get; set; }

    }
    class LineSizeTwoFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.keyBuild},    // +
                        {Settings.keyBuild}};   // +
        char[,] Form_2 = new char[,] {
                        {Settings.background,Settings.background},  // ..
                        {Settings.keyBuild,Settings.keyBuild}};     // ++


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
                    Move.TakeOutFigForm(fg);
                    bool flag2 = true, strf = false;
                    strf = Move.dotMove[1] == fg.FildGame.GetLength(1) - 1 ||
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Settings.keyBuild &&
                        Move.dotMove[1] != 0);
                    if (strf) Move.dotMove[1]--;
                    for (int i = 0; i < Form_2.GetLength(0); i++)
                    {
                        for (int j = 0; j < Form_2.GetLength(1); j++)
                        {
                            if (Form_2[i, j] == Settings.keyBuild)
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
                        Move.SetFigForm(fg);
                    }
                    else
                    {
                        Form = Form_2; rollCount = Next(); Move.SetFigForm(fg);
                    }
                    break;
            }
        }
    }
    class LineFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        public LineFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[5];
        }
        int rollCount = 1;
        char[,] Form_0 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild,Settings.keyBuild}};
        // ....
        // ....
        // ++++
        // ....
        char[,] Form_1 = new char[,] {
                        {Settings.keyBuild},    // .+..
                        {Settings.keyBuild},    // .+..
                        {Settings.keyBuild},    // .+.+
                        {Settings.keyBuild}};   // .+..

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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == Settings.keyBuild)
                        flagOut0 |= 0b00100;    // left block
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 1 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Settings.keyBuild)
                        flagOut0 |= 0b00010;    // right block +1
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Settings.keyBuild)
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
                {' ', Settings.keyBuild,' '},
                {Settings.keyBuild, Settings.keyBuild,Settings.keyBuild} };
    }
    class SFigure : IFigures
    {
        //  01 012 
        //  +-      +--
        //  ++ -++  ++-
        //  -+ ++-  -+-

        public ConsoleColor FigureColor { get; set; }
        public SFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[0];
        }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.keyBuild, Settings.background},
                        {Settings.keyBuild,Settings.keyBuild},
                        {Settings.background,Settings.keyBuild}};
        char[,] Form_1 = new char[,] {
                        {Settings.background,Settings.keyBuild,Settings.keyBuild},
                        {Settings.keyBuild,Settings.keyBuild,Settings.background}};
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
                    fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Settings.keyBuild)
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
        public ConsoleColor FigureColor { get; set; }
        public ReversSFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[1];
        }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.background, Settings.keyBuild},    // -+  
                        {Settings.keyBuild, Settings.keyBuild},      // ++  ++-
                        {Settings.keyBuild, Settings.background}};   // +-  -++
        char[,] Form_1 = new char[,] {
                        {Settings.keyBuild, Settings.keyBuild, Settings.background},
                        {Settings.background, Settings.keyBuild, Settings.keyBuild}};
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
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Settings.keyBuild)
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
        public ConsoleColor FigureColor { get; set; }
        public CubeFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[4];
        }
        public int Next() { return 0; }
        public void RestorForm()
        {
            Form = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild},
                        {Settings.keyBuild,Settings.keyBuild}};
        }
        public void Roll(Fild fg) { }
        public void RollPosition(int rc, Fild fg) { }
        public char[,] Form { get; set; }

    }
    class LFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        public LFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[2];
        }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild},        // +++
                        {Settings.keyBuild,Settings.background,Settings.background}};   // +..
        char[,] Form_2 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild},      // ++9#
                        {Settings.background,Settings.keyBuild},    // .+89
                        {Settings.background,Settings.keyBuild}};   // .+..#
        char[,] Form_3 = new char[,] {
                        {Settings.background,Settings.background,Settings.keyBuild},    // ..+
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild}};       // +++
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
                    if (fg.FildGame[Move.dotMove[0], Move.dotMove[1]] == Settings.keyBuild &&
                        fg.FildGame[Move.dotMove[0] - 1, Move.dotMove[1]] == Settings.keyBuild)
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
                    if (fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 1] == Settings.keyBuild && Move.dotMove[1] > 1)
                    {
                        Move.dotMove[1] -= 2;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = Next(); }
                        else Move.dotMove[1] += 2;
                    }
                    else if (((Move.dotMove[1] < fg.FildGame.GetLength(1) - 2) &&
                            fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 2] == Settings.keyBuild && Move.dotMove[1] > 0) ||
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
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Settings.keyBuild ||   // +*.
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Settings.keyBuild))     // .*.
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = Next(); }
                        else { Move.dotMove[1]++; }
                    }
                    else if (Move.dotMove[0] < fg.FildGame.GetLength(0) - 2 &&                              // +++
                            (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Settings.keyBuild ||   // +.*
                            fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Settings.keyBuild))     // ..*
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == Settings.keyBuild)
                    {
                        Move.dotMove[0]++; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]--; }
                    }
                    else if ((Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 && Move.dotMove[1] > 0 &&
                         fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == Settings.keyBuild) ||
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
        public ConsoleColor FigureColor { get; set; }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild},        // +++
                        {Settings.keyBuild,Settings.background,Settings.background}};   // +..
        char[,] Form_2 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild},      // ++9#
                        {Settings.background,Settings.keyBuild},    // .+89
                        {Settings.background,Settings.keyBuild}};   // .+..#
        char[,] Form_3 = new char[,] {
                        {Settings.background,Settings.background,Settings.keyBuild},    // ..+
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild}};       // +++
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
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Settings.keyBuild)
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Settings.keyBuild)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = Next(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Settings.keyBuild)
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
        public ConsoleColor FigureColor { get; set; }
        public ReversLFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[3];
        }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.background,Settings.keyBuild},    // .+
                        {Settings.background,Settings.keyBuild},    // .+
                        {Settings.keyBuild,Settings.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Settings.keyBuild,Settings.background,Settings.background},    // +..
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild}};       // +++
        char[,] Form_2 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild},      // ++
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.background}};   // +.
        char[,] Form_3 = new char[,] {                                               // .*.
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild},        // +++
                        {Settings.background,Settings.background,Settings.keyBuild}};   // .*+
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
                        (fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == Settings.keyBuild ||
                         fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Settings.keyBuild))
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
                     (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Settings.keyBuild ||
                     fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == Settings.keyBuild))
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
                         fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == Settings.keyBuild)
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
                        (fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 2] == Settings.keyBuild ||
                         fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Settings.keyBuild))
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
        public ConsoleColor FigureColor { get; set; }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.background,Settings.keyBuild},    // .+
                        {Settings.background,Settings.keyBuild},    // .+
                        {Settings.keyBuild,Settings.keyBuild}};     // ++
        char[,] Form_1 = new char[,] {
                        {Settings.keyBuild,Settings.background,Settings.background},    // +..
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild}};       // +++
        char[,] Form_2 = new char[,] {
                        {Settings.keyBuild,Settings.keyBuild},      // ++
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.background}};   // +.
        char[,] Form_3 = new char[,] {                                      // .*.
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild},        // +++
                        {Settings.background,Settings.background,Settings.keyBuild}};   // .*+
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == Settings.keyBuild)
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
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == Settings.keyBuild)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = Next(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0], Move.dotMove[1] - 1] == Settings.keyBuild) flagOut3 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = Next(); }
                    else if (!flagOut3) Move.dotMove[1]++;
                    break;
            }
        }
    }


    class TFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        public TFigure()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[6];
        }
        int rollCount = 0;
        char[,] Form_0 = new char[,] {
                        {Settings.background,Settings.background,Settings.background},  // ...
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild},        // +++
                        {Settings.background,Settings.keyBuild,Settings.background}};   // .+.
        char[,] Form_1 = new char[,] {
                        {Settings.background,Settings.keyBuild},    // .+
                        {Settings.keyBuild,Settings.keyBuild},      // ++
                        {Settings.background,Settings.keyBuild}};   // .+
        char[,] Form_2 = new char[,] {
                        {Settings.background,Settings.keyBuild,Settings.background},    // .+.
                        {Settings.keyBuild,Settings.keyBuild,Settings.keyBuild}};       // +++
        char[,] Form_3 = new char[,] {
                        {Settings.keyBuild,Settings.background},    // +.
                        {Settings.keyBuild,Settings.keyBuild},      // ++
                        {Settings.keyBuild,Settings.background}};   // +.
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
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == Settings.keyBuild)
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
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == Settings.keyBuild)
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
                    if ((form_x[i, j] == Settings.keyBuild) &&
                        Settings.keyBuild == fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]])
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
