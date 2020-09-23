using System;

namespace TETRISV1
{
    delegate void ReturnValue(int f);

    class Menu
    {
        public static ConsoleKeyInfo menuKeyInfo;
        public static char charMenuKey;
        public static int ReturnKey()
        {
            string strNumber = null;
            do
            {
                menuKeyInfo = Console.ReadKey(true);
                charMenuKey = menuKeyInfo.KeyChar;
                if ('0' <= charMenuKey & charMenuKey < '9' + 1)
                {
                    strNumber = charMenuKey.ToString();
                }
            } while (strNumber == null);
            return (int.Parse(strNumber));
        }
        public class MainMenu
        {

            static bool isEnd;
            public static void MainMenuWriter()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    System.Console.WriteLine("1 New Game");
                    System.Console.WriteLine("2 Save game");
                    System.Console.WriteLine("3 Load game");
                    System.Console.WriteLine("4 Settings");
                    System.Console.WriteLine();
                    System.Console.WriteLine("0 Exit");
                    Menu.MainMenu.MainMenuHandler(ReturnKey());
                } while (isEnd);
            }
            static public void MainMenuHandler(int f)
            {
                switch (f)
                {
                    case (1):
                        Run.RunGame();
                        //Fild.RunGame = true;
                        break;
                    case (3):
                        ContinueMenu.Continue();
                        break;
                    case (4):
                        SettingsMenu.Settings();
                        break;
                    case (0):
                        RunNewGame.RunGameFlag = isEnd = false;
                        break;

                }
            }
        }

        class Rellise
        {
            static ReturnValue Deleg = MainMenu.MainMenuHandler;

        }
        class ContinueMenu
        {
            public static void Continue()
            {
                Run.ContinueGame();
            }
        }

        class SettingsMenu
        {
            static bool isEnd = true;
            public static void Settings()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    // System.Console.WriteLine($"1 Char block: {Settings.keyBuild}");
                    // System.Console.WriteLine($"2 Block color: {Settings.ConsColBrick}");
                    // System.Console.WriteLine($"3 Char background: {Settings.background}");
                    // System.Console.WriteLine($"4 Background color: {Settings.ConsColBackground}");
                    // System.Console.WriteLine($"5 Widht: {Settings.FildWidth}");
                    // System.Console.WriteLine($"6 Height: {Settings.FildHeight}");
                    // System.Console.WriteLine($"7 Speed: {Settings.Speed}");
                    // System.Console.WriteLine("8 Back");
                    // System.Console.WriteLine($"9 Color Type: {Settings.ColorScreen}");
                    System.Console.WriteLine($"1 Video Option");
                    System.Console.WriteLine($"2 Game option");
                    System.Console.WriteLine($"3 Control option");
                    System.Console.WriteLine("\n\n\n8 Back");
                    SettingsHandler(ReturnKey());
                } while (isEnd);
            }
            static void SettingsHandler(int v)
            {
                #region OldHandler

                /* switch (v)
                {

                    case (1):
                        System.Console.Write("Change new Key: ");
                        Settings.keyBuild = Console.ReadKey().KeyChar;
                        //MainMenuSettings();
                        break;
                    case (3):
                        System.Console.Write("Change new Key: ");
                        Settings.background = Console.ReadKey().KeyChar;
                        //MainMenuSettings();
                        break;
                    case (2):
                        Console.Clear();
                        System.Console.WriteLine("1. Red");
                        System.Console.WriteLine("2. Blue");
                        System.Console.WriteLine("3. Green");
                        System.Console.WriteLine("4. White");
                        System.Console.WriteLine("5. Black");
                        System.Console.WriteLine("6. Cyan");
                        //break;
                        switch (ReturnKey())
                        {
                            case (1):
                                Settings.ConsColBrick = ConsoleColor.Red;
                                break;
                            case (2):
                                Settings.ConsColBrick = ConsoleColor.Blue;
                                break;
                            case (3):
                                Settings.ConsColBrick = ConsoleColor.Green;
                                break;
                            case (4):
                                Settings.ConsColBrick = ConsoleColor.White;
                                break;
                            case (5):
                                Settings.ConsColBrick = ConsoleColor.Black;
                                break;
                            case (6):
                                Settings.ConsColBrick = ConsoleColor.Cyan;
                                break;
                        }
                        //MainMenuSettings();
                        break;
                    case (4):
                        Console.Clear();
                        System.Console.WriteLine("1. Red");
                        System.Console.WriteLine("2. Blue");
                        System.Console.WriteLine("3. Green");
                        System.Console.WriteLine("4. White");
                        System.Console.WriteLine("5. Black");
                        //break;
                        switch (ReturnKey())
                        {
                            case (1):
                                Settings.ConsColBackground = ConsoleColor.Red;
                                break;
                            case (2):
                                Settings.ConsColBackground = ConsoleColor.Blue;
                                break;
                            case (3):
                                Settings.ConsColBackground = ConsoleColor.Green;
                                break;
                            case (4):
                                Settings.ConsColBackground = ConsoleColor.White;
                                break;
                            case (5):
                                Settings.ConsColBackground = ConsoleColor.Black;
                                break;
                        }
                        //MainMenuSettings();
                        break;
                    case (5):
                        Console.Write("Write new Width (3-100): ");
                        string strW = Console.ReadLine(), str2W = "";
                        foreach (var e in strW)
                            if (e >= '1' && e <= '9' || e == '0') str2W += e;
                        if (str2W.Length > 0 && str2W.Length < 9) Settings.FildWidth = int.Parse(str2W);
                        //MainMenuSettings();
                        break;
                    case (6):
                        Console.Write("Write new Height (3-100): ");
                        string strH = Console.ReadLine(), str2H = "";
                        foreach (var e in strH)
                            if (e >= '0' && e <= '9') str2H += e;
                        if (str2H.Length > 0 && str2H.Length < 9) Settings.FildHeight = int.Parse(str2H);
                        //MainMenuSettings();
                        break;
                    case (7):
                        System.Console.Write("Write new Game speed: ");
                        string strS = Console.ReadLine(), str2S = "";
                        foreach (var e in strS)
                            if (e >= '1' && e <= '9' || e == '0') str2S += e;
                        if (str2S.Length > 0 && str2S.Length < 9) Settings.Speed = int.Parse(str2S);
                        //MainMenuSettings();
                        break;
                    case (8):
                        isEnd = true;
                        break;
                    case (9):
                        if (Settings.ColorScreen == 0)
                        {
                            Settings.ColorScreen = 1;
                            //MainMenuSettings();
                        }
                        else
                        {
                            Settings.ColorScreen = 0;
                            //MainMenuSettings();
                        }
                        break; */
                #endregion
                switch (v)
                {
                    case (1):
                        VideoOptionMenu.VideoOption();
                        break;
                    case (2):
                        GameOptionMenu.GameOption();
                        break;
                    case (3):
                        ControlOptionMenu.ControlOption();
                        break;
                    case (8):
                        TETRISV1.Settings.WriteSettingsInFile();
                        isEnd = false;
                        break;
                    default:
                        //MainMenuSettings();
                        break;
                }
            }

        }


        class VideoOptionMenu
        {
            static bool isEnd;
            public static void VideoOption()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    System.Console.WriteLine($"1 Screen type: {(Settings.isColorScreen == 1 ? "Color" : "Text")}");
                    System.Console.WriteLine($"2 Screen color options:");
                    System.Console.WriteLine($"3 Screen text options:");
                    System.Console.WriteLine("\n\n8 Back");
                    VideoOptionHandler(ReturnKey());
                } while (isEnd);

            }
            static void VideoOptionHandler(int v)
            {
                switch (v)
                {
                    case (1):
                        Settings.isColorScreen = Settings.isColorScreen == 1 ? 0 : 1;
                        break;
                    case (2):
                        ScreenColorOptionMenu.ScreenColorOption();
                        break;
                    case (3):
                        ScreenTextOptionMenu.ScreenTextOption();
                        break;
                    case (8):
                        isEnd = false;
                        break;

                    default:
                        break;
                }
            }
        }

        class ScreenColorOptionMenu
        {
            static bool isEnd;
            public static void ScreenColorOption()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    System.Console.WriteLine($"1 Single color for blocks: {(TETRISV1.Settings.isSingleColorBlock ? "Yes" : "No")}");
                    System.Console.Write($"2 Color blocks: "); writeWithColor(Settings.ConsColBrick);
                    System.Console.Write($"3 Background color: "); writeWithColor(Settings.ConsColBackground);
                    System.Console.WriteLine($"4 Color figures: ");
                    System.Console.WriteLine("\n8 Back");
                    ScreenColorOptionHandler(ReturnKey());
                } while (isEnd);
            }
            static void ScreenColorOptionHandler(int v)
            {
                switch (v)
                {
                    case (1):
                        TETRISV1.Settings.isSingleColorBlock = !TETRISV1.Settings.isSingleColorBlock;
                        break;
                    case (2):
                        Settings.ConsColBrick = ChooseColor();
                        break;
                    case (3):
                        Settings.ConsColBackground = ChooseColor();
                        break;
                    case (4):
                        ColorFiguresMenu.ColorFigures();
                        break;
                    case (8):
                        isEnd = false;
                        break;
                    default:
                        break;
                }
            }
        }

        class ColorFiguresMenu
        {
            static bool isEnd;
            public static void ColorFigures()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    System.Console.Write($"1 S:\t "); writeWithColor(Settings.FigColor[0]);
                    System.Console.Write($"2 RS:\t "); writeWithColor(Settings.FigColor[1]);
                    System.Console.Write($"3 L:\t "); writeWithColor(Settings.FigColor[2]);
                    System.Console.Write($"4 RL:\t "); writeWithColor(Settings.FigColor[3]);
                    System.Console.Write($"5 Cube:\t "); writeWithColor(Settings.FigColor[4]);
                    System.Console.Write($"6 Line:\t "); writeWithColor(Settings.FigColor[5]);
                    System.Console.Write($"7 T:\t "); writeWithColor(Settings.FigColor[6]);
                    System.Console.Write("\n8 Back");
                    ColorFigureHandler(ReturnKey());
                } while (isEnd);

            }
            static void ColorFigureHandler(int v)
            {
                switch (v)
                {
                    case (1):
                        Settings.FigColor[0] = ChooseColor();
                        break;
                    case (2):
                        Settings.FigColor[1] = ChooseColor();
                        break;
                    case (3):
                        Settings.FigColor[2] = ChooseColor();
                        break;
                    case (4):
                        Settings.FigColor[3] = ChooseColor();
                        break;
                    case (5):
                        Settings.FigColor[4] = ChooseColor();
                        break;
                    case (6):
                        Settings.FigColor[5] = ChooseColor();
                        break;
                    case (7):
                        Settings.FigColor[6] = ChooseColor();
                        break;
                    case (8):
                        isEnd = false;
                        break;
                        // case():
                        // break;
                }
            }
        }

        class ScreenTextOptionMenu
        {
            static bool isEnd;
            public static void ScreenTextOption()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    System.Console.WriteLine($"1 Char block: {Settings.keyBuild}");
                    System.Console.Write($"2 Color block: "); writeWithColor(Settings.ConsColBrick);
                    System.Console.WriteLine($"3 Char background: {Settings.background}");
                    System.Console.Write($"4 Color background: "); writeWithColor(Settings.ConsColBackground);
                    System.Console.WriteLine("\n8 Back");
                    ScreenTextOptionHandler(ReturnKey());
                } while (isEnd);
            }
            static void ScreenTextOptionHandler(int v)
            {
                switch (v)
                {
                    case (1):
                        System.Console.Write("Change new Key: ");
                        Settings.keyBuild = Console.ReadKey(true).KeyChar;
                        break;
                    case (2):
                        Settings.ConsColBrick = ChooseColor();
                        break;
                    case (3):
                        System.Console.Write("Change new Key: ");
                        Settings.background = Console.ReadKey().KeyChar;
                        break;
                    case (4):
                        Settings.ConsColBackground = ChooseColor();
                        break;
                    case (8):
                        isEnd = false;
                        break;
                }
            }
        }
        class GameOptionMenu
        {
            static bool isEnd;
            public static void GameOption()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    System.Console.WriteLine($"1 Widht: {Settings.FildWidth}");
                    System.Console.WriteLine($"2 Height: {Settings.FildHeight}");
                    System.Console.WriteLine($"3 Speed: {Settings.Speed}");
                    System.Console.WriteLine("\n\n8 Back");
                    GameOptionHandler(ReturnKey());
                }
                while (isEnd);
            }
            static void GameOptionHandler(int v)
            {
                int num;
                switch (v)
                {
                    case (1):
                        Console.Write("Choise new Width (3-100): ");
                        string strW = Console.ReadLine(); //str2W = "";
                                                          // foreach (var e in strW)
                                                          //     if (e >= '1' && e <= '9' || e == '0') str2W += e;
                                                          // if (str2W.Length > 0 && str2W.Length < 9)
                                                          // {
                                                          // num = int.Parse(str2W);
                        int.TryParse(strW, out num);
                        if (num > 3 && num < 101) Settings.FildWidth = num;
                        // }
                        break;
                    case (2):
                        Console.Write("Choise new Height (3-100): ");
                        string strH = Console.ReadLine(), str2H = "";
                        foreach (var e in strH)
                            if (e >= '0' && e <= '9') str2H += e;
                        if (str2H.Length > 0 && str2H.Length < 9)
                        {
                            num = int.Parse(str2H);
                            if (num > 3 && num < 101) Settings.FildHeight = num;
                        }
                        break;
                    case (3):
                        System.Console.Write("Choise new Game speed: ");
                        string strS = Console.ReadLine(), str2S = "";
                        foreach (var e in strS)
                            if (e >= '1' && e <= '9' || e == '0') str2S += e;
                        if (str2S.Length > 0 && str2S.Length < 9)
                        {
                            num = int.Parse(str2S);
                            if (num > 0 && num < 101) Settings.Speed = num;
                        }
                        break;
                    case (8):
                        isEnd = false;
                        break;
                    default:
                        break;
                }
            }
        }

        class ControlOptionMenu
        {
            static bool isEnd = true;
            public static void ControlOption()
            {
                do
                {
                    Console.Clear();
                    System.Console.WriteLine($"1 Left: {Settings.controlKeys[0]}");
                    System.Console.WriteLine($"2 Right: {Settings.controlKeys[1]}");
                    System.Console.WriteLine($"3 Down: {Settings.controlKeys[2]}");
                    System.Console.WriteLine($"4 Rotate: {Settings.controlKeys[3]}");
                    System.Console.WriteLine($"5 Quit: {Settings.controlKeys[4]}");
                    System.Console.WriteLine("\n8 Back");
                    ControlOptionHandler(ReturnKey());
                } while (isEnd);
            }
            static void ControlOptionHandler(int v)
            {
                switch (v)
                {
                    case (1):
                        Settings.controlKeys[0] = Console.ReadKey(true).KeyChar;
                        break;
                    case (2):
                        Settings.controlKeys[1] = Console.ReadKey(true).KeyChar;
                        break;
                    case (3):
                        Settings.controlKeys[2] = Console.ReadKey(true).KeyChar;
                        break;
                    case (4):
                        Settings.controlKeys[3] = Console.ReadKey(true).KeyChar;
                        break;
                    case (5):
                        Settings.controlKeys[4] = Console.ReadKey(true).KeyChar;
                        break;
                    case (8):
                        isEnd = false;
                        break;
                }
            }
        }

        static void writeWithColor(ConsoleColor c)
        {
            Console.BackgroundColor = c;
            System.Console.WriteLine(string.Format($"{c,12}"));
            Console.ResetColor();
        }
        static ConsoleColor ChooseColor()
        {
            Console.Clear();
            PrintColorValue(0, 7);
            System.Console.WriteLine("0 More");
            int returnedNum = ReturnKey();
            if (returnedNum == 0)
            {
                Console.Clear();
                PrintColorValue(8, 15);
                returnedNum = ReturnKey();
                if (returnedNum < 9)
                {
                    return (ConsoleColor)returnedNum + 7;
                }
                else return ConsoleColor.Black;
            }
            else
            {
                if (returnedNum < 9)
                {
                    return (ConsoleColor)returnedNum - 1;
                }
                else return ConsoleColor.Black;
            }

            static void PrintColorValue(int current, int ended)
            {
                for (int i = current, count = 1; i <= ended; i++, count++)
                {
                    Console.BackgroundColor = (ConsoleColor)i;
                    System.Console.WriteLine($"{count} {(ConsoleColor)i,10}\t");
                }
                Console.ResetColor();

            }
        }
        // }
    }
}

