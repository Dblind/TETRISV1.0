using System;

namespace TETRISV1
{
    class ConteinerFigures
    {
        IFigures[] indexedFigs = {
            new SFigure(),
            new ReversSFigure(),
            new LFigure(),
            new ReversLFigure(),
            new CubeFigure(),
            new LineFigure(),
            new TFigure()
        };

        public IFigures this[int indx]
        {
            get { return indexedFigs[indx]; }
            set { }
        }
        public void ResetColor()
        {
            foreach (var e in indexedFigs)
            {
                e.ResetColorFig();
            }
        }
    }

    interface IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        public int NextPosition();
        public void RestorForm();
        public void ResetColorFig();
        public void Rotate(Fild fg);
        public void RotationPosition(int rc, Fild fg);
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get; set; }

    }
    class LineSizeTwoFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {true},    // +
                        {true}};   // +
        bool[,] Form_2 = new bool[,] {
                        {false,false},      // ..
                        {true,true}};       // ++


        public int NextPosition()
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
        public bool[,] Form { get; set; }//= new bool [,] { };
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public void RestorForm()
        {
            Form = Form_0;
            rollCount = 0;
        }
        public void ResetColorFig()
        {

        }
        public void Rotate(Fild fg)
        {
            int cc = fg.FigNow.NextPosition();
            fg.FigNow.RotationPosition(cc, fg);
        }
        // +  --
        // +  ++
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    if (SupportMethods.Intersection(Form_0, fg))
                    {
                        Form = Form_0; rollCount = NextPosition();
                    }
                    break;
                case (1):
                    Move.TakeOutFigForm(fg);
                    bool flag2 = true, strf = false;
                    strf = Move.dotMove[1] == fg.FildGame.GetLength(1) - 1 ||
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == true &&
                        Move.dotMove[1] != 0);
                    if (strf) Move.dotMove[1]--;
                    for (int i = 0; i < Form_2.GetLength(0); i++)
                    {
                        for (int j = 0; j < Form_2.GetLength(1); j++)
                        {
                            if (Form_2[i, j] == true)
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
                        Form = Form_2; rollCount = NextPosition(); Move.SetFigForm(fg);
                    }
                    break;
            }
        }
    }
    class LineFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {true,true,true,true}};
        // ....
        // ....
        // ++++
        // ....
        bool[,] Form_1 = new bool[,] {
                        {true},    // .+..
                        {true},    // .+..
                        {true},    // .+.+
                        {true}};   // .+..
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public LineFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition() => rollCount > 0 ? 0 : 1;
        public void RestorForm() { Form = Form_1; rollCount = 1; }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[5];
        }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                //  0123456789 78910
                //  -+----+--  +--
                //  10b4-01b9
                case (0):
                    int flagOut0 = 0;
                    if (0 < Move.dotMove[1]) flagOut0 = 0b10000;
                    if (fg.FildGame.GetLength(1) - 2 > Move.dotMove[1]) flagOut0 |= 0b01000;//  9 - 2 (7) > 6 
                    if (Move.dotMove[1] > 0 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == true)
                        flagOut0 |= 0b00100;    // left block
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 1 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == true)
                        flagOut0 |= 0b00010;    // right block +1
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == true)
                        flagOut0 |= 0b00001;    // right block +2

                    if (Move.dotMove[1] == 0 || ((flagOut0 & 0b11100) == 0b11100 &&
                            Move.dotMove[1] < fg.FigNow.Form.GetLength(1) - 3))   // zero, left
                    {
                        Move.dotMove[0] += 2;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else Move.dotMove[0] -= 2;
                    }
                    else if ((flagOut0 & 0b10010) == 0b10010)       //right +1
                    {
                        Move.dotMove[0] += 2; Move.dotMove[1] -= 3;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1] += 3; }
                    }
                    else if ((flagOut0 & 0b10001) == 0b10001)       //right +2
                    {
                        Move.dotMove[0] += 2; Move.dotMove[1] -= 2;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1] += 2; }
                    }
                    else if ((flagOut0 & 0b01000) != 0b01000)       //right wall
                    {
                        int boxCoordColumn = Move.dotMove[1];
                        Move.dotMove[0] += 2; Move.dotMove[1] = fg.FildGame.GetLength(1) - 4;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1] = boxCoordColumn; }
                    }
                    else        //default empty
                    {
                        Move.dotMove[0] += 2; Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0] -= 2; Move.dotMove[1]++; }
                    }
                    break;
                case (1):
                    if (Move.dotMove[0] != fg.FildGame.GetLength(0) - 1)
                    {
                        Move.dotMove[0] -= 2; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                        else { Move.dotMove[0] += 2; Move.dotMove[1]--; }
                    }
                    break;
            }
        }
    }
    /* static class Angle
    {
        public static bool [,] Form = new bool [,] {
                {' ', true,' '},
                {true, true,true} };
    } */
    class SFigure : IFigures
    {
        //  01 012 
        //  +-      +--
        //  ++ -++  ++-
        //  -+ ++-  -+-

        public ConsoleColor FigureColor { get; set; }

        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {true, false},
                        {true,true},
                        {false,true}};
        bool[,] Form_1 = new bool[,] {
                        {false,true,true},
                        {true,true,false}};
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public SFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition()
        {
            if (rollCount < 1) return rollCount + 1;
            else return 0;
        }
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[0];
        }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(fg.FigNow.NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                    else Move.dotMove[0]++;
                    break;
                case (1):
                    bool flagOut1 = false;
                    if (Move.dotMove[1] + fg.FigNow.Form.GetLength(1) == fg.FildGame.GetLength(1) ||
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == true &&
                        Move.dotMove[1] > 0))
                    { Move.dotMove[0]++; Move.dotMove[1]--; flagOut1 = true; }
                    else Move.dotMove[0]++;
                    if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                    else if (flagOut1) { Move.dotMove[0]--; Move.dotMove[1]++; }
                    else Move.dotMove[0]--;
                    break;

            }
        }
    }
    class ReversSFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {false, true},      // -+  
                        {true, true},       // ++  ++-
                        {true, false}};     // +-  -++
        bool[,] Form_1 = new bool[,] {
                        {true, true, false},
                        {false, true, true}};
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public ReversSFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition()
        {
            if (rollCount < 1) return rollCount + 1;
            else return 0;
        }
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[1];
        }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(fg.FigNow.NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--; Move.dotMove[1]++;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    break;

                case (1):
                    bool flagOut1 = false;
                    if (Move.dotMove[1] == 0 ||
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == true &&
                        Move.dotMove[1] + 2 < fg.FildGame.GetLength(1)))
                    { Move.dotMove[0]++; flagOut1 = true; }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                    else if (flagOut1) { Move.dotMove[0]--; }
                    else { Move.dotMove[0]--; Move.dotMove[1]++; }
                    break;
            }
        }
    }
    class CubeFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        bool[,] Form_0 = new bool[,] {
                        {true,true},
                        {true,true}};
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form; } set { } }
        public CubeFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition() { return 0; }
        public void RestorForm()
        {
            Form = Form_0;
        }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[4];
        }
        public void Rotate(Fild fg) { }
        public void RotationPosition(int rc, Fild fg) { }
    }
    class LFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {true,false},    // +.
                        {true,false},    // +.
                        {true,true}};     // ++
        bool[,] Form_1 = new bool[,] {
                        {true,true,true},        // +++
                        {true,false,false}};   // +..
        bool[,] Form_2 = new bool[,] {
                        {true,true},      // ++9#
                        {false,true},    // .+89
                        {false,true}};   // .+..#
        bool[,] Form_3 = new bool[,] {
                        {false,false,true},    // ..+
                        {true,true,true}};       // +++
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public LFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition()
        {
            if (rollCount < 3) return rollCount + 1;
            else return 0;
        }
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[2];
        }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(fg.FigNow.NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    if (fg.FildGame[Move.dotMove[0], Move.dotMove[1]] == true &&
                        fg.FildGame[Move.dotMove[0] - 1, Move.dotMove[1]] == true)
                    {
                        Move.dotMove[0]--; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    }
                    else
                    {
                        Move.dotMove[0]--;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else Move.dotMove[0]++;
                    }
                    break;
                case (1):
                    if (fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 1] == true && Move.dotMove[1] > 1)
                    {
                        Move.dotMove[1] -= 2;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                        else Move.dotMove[1] += 2;
                    }
                    else if (((Move.dotMove[1] < fg.FildGame.GetLength(1) - 2) &&
                            fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 2] == true && Move.dotMove[1] > 0) ||
                             Move.dotMove[1] > fg.FildGame.GetLength(1) - 3)
                    {
                        Move.dotMove[1] -= 1;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                        else Move.dotMove[1] += 1;
                    }
                    else
                    {
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                    }
                    break;
                case (2):
                    if (Move.dotMove[1] > 0 && Move.dotMove[0] < fg.FildGame.GetLength(0) - 2 &&        // +++
                        (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == true ||   // +*.
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == true))     // .*.
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = NextPosition(); }
                        else { Move.dotMove[1]++; }
                    }
                    else if (Move.dotMove[0] < fg.FildGame.GetLength(0) - 2 &&                              // +++
                            (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == true ||   // +.*
                            fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == true))     // ..*
                    {
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = NextPosition(); }
                    }
                    else
                    {
                        bool flagOut2 = false;
                        if (Move.dotMove[0] == fg.FildGame.GetLength(0) - 2) flagOut2 = true;
                        if (flagOut2) Move.dotMove[0]--;
                        Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = NextPosition(); }
                        else
                        {
                            if (flagOut2) Move.dotMove[0]++;
                            else Move.dotMove[1]--;
                        }
                    }
                    break;
                case (3):
                    if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 3 &&
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == true)
                    {
                        Move.dotMove[0]++; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]--; }
                    }
                    else if ((Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 && Move.dotMove[1] > 0 &&
                         fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == true) ||
                         Move.dotMove[1] == 0)
                    {
                        Move.dotMove[0]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else Move.dotMove[0]--;
                    }
                    else
                    {
                        Move.dotMove[0]++; Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]++; }
                    }
                    break;
            }
        }
    }
    class LFigureV2 //: IFigures      // have bug
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount = 0;
        bool[,] Form_0 = new bool[,] {
                        {true,false},    // +.
                        {true,false},    // +.
                        {true,true}};     // ++
        bool[,] Form_1 = new bool[,] {
                        {true,true,true},        // +++
                        {true,false,false}};   // +..
        bool[,] Form_2 = new bool[,] {
                        {true,true},      // ++9#
                        {false,true},    // .+89
                        {false,true}};   // .+..#
        bool[,] Form_3 = new bool[,] {
                        {false,false,true},    // ..+
                        {true,true,true}};       // +++
        public int NextPosition()
        {
            if (rollCount < 3) return rollCount + 1;
            else return 0;
        }
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(fg.FigNow.NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--; Move.dotMove[1]++;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }

                    break;
                case (1):
                    bool flagOut1 = false;
                    if ((Move.dotMove[1] > fg.FildGame.GetLength(1) - 3) ||
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == true)
                    { Move.dotMove[0]++; Move.dotMove[1]--; flagOut1 = true; }
                    else Move.dotMove[0]++;
                    if (SupportMethods.Intersection(Form_1, fg))
                    { Form = Form_1; rollCount = NextPosition(); }
                    else
                    {
                        if (flagOut1) { Move.dotMove[0]--; Move.dotMove[1]++; }
                        else Move.dotMove[0]--;
                    }
                    break;
                case (2):
                    bool flagOut2 = false;
                    if (Move.dotMove[0] + fg.FigNow.Form.GetLength(0) == fg.FildGame.GetLength(0) ||
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == true)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = NextPosition(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == true)
                        flagOut3 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                    else if (!flagOut3) Move.dotMove[1]++;
                    break;
            }
        }
    }
    class ReversLFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {false,true},    // .+
                        {false,true},    // .+
                        {true,true}};    // ++
        bool[,] Form_1 = new bool[,] {
                        {true,false,false},     // +..
                        {true,true,true}};      // +++
        bool[,] Form_2 = new bool[,] {
                        {true,true},      // ++
                        {true,false},    // +.
                        {true,false}};   // +.
        bool[,] Form_3 = new bool[,] {          // .*.
                        {true,true,true},       // +++
                        {false,false,true}};    // .*+
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public ReversLFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition()
        {
            if (rollCount < 3) return rollCount + 1;
            else return 0;
        }
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[3];
        }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(fg.FigNow.NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    if (Move.dotMove[0] == fg.FildGame.GetLength(0) - 2)
                    {
                        Move.dotMove[0]--; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    }
                    else if (Move.dotMove[0] > 0 &&
                         (fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 1] == true ||
                          fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == true))
                    {
                        Move.dotMove[0]--; Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    }
                    else
                    {
                        Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                        else Move.dotMove[1]--;
                    }
                    break;
                case (1):
                    if (Move.dotMove[1] == 0)
                    {
                        Move.dotMove[0]++;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                        else { Move.dotMove[0]--; }
                    }
                    else if (Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&       // offset right
                     (fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == true ||
                     fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] - 1] == true))
                    {
                        Move.dotMove[0]++;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                        else Move.dotMove[0]--;
                    }
                    else
                    {
                        Move.dotMove[0]++; Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                        else { Move.dotMove[0]--; Move.dotMove[1]++; }
                    }
                    break;
                case (2):
                    Move.dotMove[0]--;
                    if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = NextPosition(); }
                    else Move.dotMove[0]++;
                    break;
                case (3):
                    if (Move.dotMove[1] == fg.FildGame.GetLength(1) - 2 &&
                         fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 1] == true)
                    {
                        Move.dotMove[1] -= 2;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else Move.dotMove[1] += 2;
                    }
                    else if (Move.dotMove[1] == fg.FildGame.GetLength(1) - 2)
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else { Move.dotMove[1]++; }
                    }
                    else if (Move.dotMove[1] > 0 && Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 &&
                        (fg.FildGame[Move.dotMove[0], Move.dotMove[1] + 2] == true ||
                         fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == true))
                    {
                        Move.dotMove[1]--;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else Move.dotMove[1]++;
                    }
                    else if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }

                    break;
            }
        }
    }
    class ReversLFigureV2 //: IFigures        // have bug
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount = 0;
        bool[,] Form_0 = new bool[,] {
                        {false,true},    // .+
                        {false,true},    // .+
                        {true,true}};     // ++
        bool[,] Form_1 = new bool[,] {
                        {true,false,false},    // +..
                        {true,true,true}};       // +++
        bool[,] Form_2 = new bool[,] {
                        {true,true},      // ++
                        {true,false},    // +.
                        {true,false}};   // +.
        bool[,] Form_3 = new bool[,] {                                      // .*.
                        {true,true,true},        // +++
                        {false,false,true}};   // .*+
        public int NextPosition()
        {
            if (rollCount < 3) return rollCount + 1;
            else return 0;
        }
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void Rotate(Fild fg) => fg.FigNow.RotationPosition(fg.FigNow.NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    Move.dotMove[0]--; Move.dotMove[1]++;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                    else { Move.dotMove[0]++; Move.dotMove[1]--; }
                    break;
                case (1):
                    bool flagOut1 = false;
                    if ((Move.dotMove[1] > fg.FildGame.GetLength(1) - 3) ||
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1] + 2] == true)
                    { Move.dotMove[0]++; Move.dotMove[1]--; flagOut1 = true; }
                    else Move.dotMove[0]++;
                    if (SupportMethods.Intersection(Form_1, fg))
                    { Form = Form_1; rollCount = NextPosition(); }
                    else
                    {
                        if (flagOut1) { Move.dotMove[0]--; Move.dotMove[1]++; }
                        else Move.dotMove[0]--;
                    }
                    break;
                case (2):
                    bool flagOut2 = false;
                    if (Move.dotMove[0] + fg.FigNow.Form.GetLength(0) == fg.FildGame.GetLength(0) ||
                        fg.FildGame[Move.dotMove[0] + 2, Move.dotMove[1]] == true)
                    { Move.dotMove[0]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg))
                    { Form = Form_2; rollCount = NextPosition(); }
                    else if (flagOut2) Move.dotMove[0]++;
                    break;
                case (3):
                    bool flagOut3 = false;
                    if (Move.dotMove[1] == 0 || fg.FildGame[Move.dotMove[0], Move.dotMove[1] - 1] == true) flagOut3 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                    else if (!flagOut3) Move.dotMove[1]++;
                    break;
            }
        }
    }

    class TFigure : IFigures
    {
        public ConsoleColor FigureColor { get; set; }
        int rollCount;
        bool[,] Form_0 = new bool[,] {
                        {false,false,false},  // ...
                        {true,true,true},        // +++
                        {false,true,false}};   // .+.
        bool[,] Form_1 = new bool[,] {
                        {false,true},    // .+
                        {true,true},      // ++
                        {false,true}};   // .+
        bool[,] Form_2 = new bool[,] {
                        {false,true,false},    // .+.
                        {true,true,true}};       // +++
        bool[,] Form_3 = new bool[,] {
                        {true,false},    // +.
                        {true,true},      // ++
                        {true,false}};   // +.
        public bool[,] Form { get; set; }
        public bool[,] DefaultForm { get { return Form_0; } set { } }

        public TFigure()
        {
            ResetColorFig();
            RestorForm();
        }
        public int NextPosition() => rollCount < 3 ? rollCount + 1 : 0;
        public void RestorForm() { Form = Form_0; rollCount = 0; }
        public void ResetColorFig()
        {
            if (Settings.isSingleColorBlock) FigureColor = Settings.ConsColBrick;
            else FigureColor = Settings.FigColor[6];
        }
        public void Rotate(Fild fg) => RotationPosition(NextPosition(), fg);
        public void RotationPosition(int rc, Fild fg)
        {
            switch (rc)
            {
                case (0):
                    bool flagOut0 = false;
                    if (Move.dotMove[1] == 0 ||
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] - 1] == true &&
                        Move.dotMove[1] < fg.FigNow.Form.GetLength(1) - 2)
                        flagOut0 = true;
                    else Move.dotMove[1]--;
                    if (SupportMethods.Intersection(Form_0, fg)) { Form = Form_0; rollCount = NextPosition(); }
                    else if (!flagOut0) Move.dotMove[1]++;
                    break;
                case (1):
                    if (SupportMethods.Intersection(Form_1, fg)) { Form = Form_1; rollCount = NextPosition(); }
                    break;
                case (2):
                    bool flagOut2 = false;
                    if (Move.dotMove[1] == fg.FildGame.GetLength(1) - 2 ||
                        (Move.dotMove[1] < fg.FildGame.GetLength(1) - 2 && Move.dotMove[1] > 0 &&
                        fg.FildGame[Move.dotMove[0] + 1, Move.dotMove[1] + 2] == true))
                    { Move.dotMove[1]--; flagOut2 = true; }
                    if (SupportMethods.Intersection(Form_2, fg)) { Form = Form_2; rollCount = NextPosition(); }
                    else { if (flagOut2) Move.dotMove[1]++; }
                    break;
                case (3):
                    if (fg.FildGame.GetLength(0) - 2 > Move.dotMove[0])
                    {
                        Move.dotMove[1]++;
                        if (SupportMethods.Intersection(Form_3, fg)) { Form = Form_3; rollCount = NextPosition(); }
                        else Move.dotMove[1]--;
                    }
                    break;
            }
        }

    }
    class SupportMethods
    {
        public static bool Intersection(bool[,] form_x, Fild fg)
        {
            bool flag = true;
            for (int i = 0; i < form_x.GetLength(0); i++)
            {
                for (int j = 0; j < form_x.GetLength(1); j++)
                {
                    if ((form_x[i, j] == true) &&
                        true == fg.FildGame[i + Move.dotMove[0], j + Move.dotMove[1]])
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
