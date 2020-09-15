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
                    System.Console.WriteLine("4 Setting");
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
                    case (4):
                        SettingsMenu.Setting();
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

        class SettingsMenu
        {
            static bool isEnd = true;
            public static void Setting()
            {
                isEnd = true;
                do
                {
                    Console.Clear();
                    // System.Console.WriteLine($"1 Char block: {Setting.keyBuild}");
                    // System.Console.WriteLine($"2 Block color: {Setting.ConsColBrick}");
                    // System.Console.WriteLine($"3 Char background: {Setting.background}");
                    // System.Console.WriteLine($"4 Background color: {Setting.ConsColBackground}");
                    // System.Console.WriteLine($"5 Widht: {Setting.FildWidth}");
                    // System.Console.WriteLine($"6 Height: {Setting.FildHeight}");
                    // System.Console.WriteLine($"7 Speed: {Setting.Speed}");
                    // System.Console.WriteLine("8 Back");
                    // System.Console.WriteLine($"9 Color Type: {Setting.ColorScreen}");
                    System.Console.WriteLine($"1 Video Option");
                    System.Console.WriteLine($"2 Game option");
                    System.Console.WriteLine("\n\n\n8 Back");
                    SettingHandler(ReturnKey());
                } while (isEnd);
            }
            static void SettingHandler(int v)
            {
                #region OldHandler

                /* switch (v)
                {

                    case (1):
                        System.Console.Write("Change new Key: ");
                        Setting.keyBuild = Console.ReadKey().KeyChar;
                        //MainMenuSetting();
                        break;
                    case (3):
                        System.Console.Write("Change new Key: ");
                        Setting.background = Console.ReadKey().KeyChar;
                        //MainMenuSetting();
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
                                Setting.ConsColBrick = ConsoleColor.Red;
                                break;
                            case (2):
                                Setting.ConsColBrick = ConsoleColor.Blue;
                                break;
                            case (3):
                                Setting.ConsColBrick = ConsoleColor.Green;
                                break;
                            case (4):
                                Setting.ConsColBrick = ConsoleColor.White;
                                break;
                            case (5):
                                Setting.ConsColBrick = ConsoleColor.Black;
                                break;
                            case (6):
                                Setting.ConsColBrick = ConsoleColor.Cyan;
                                break;
                        }
                        //MainMenuSetting();
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
                                Setting.ConsColBackground = ConsoleColor.Red;
                                break;
                            case (2):
                                Setting.ConsColBackground = ConsoleColor.Blue;
                                break;
                            case (3):
                                Setting.ConsColBackground = ConsoleColor.Green;
                                break;
                            case (4):
                                Setting.ConsColBackground = ConsoleColor.White;
                                break;
                            case (5):
                                Setting.ConsColBackground = ConsoleColor.Black;
                                break;
                        }
                        //MainMenuSetting();
                        break;
                    case (5):
                        Console.Write("Write new Width (3-100): ");
                        string strW = Console.ReadLine(), str2W = "";
                        foreach (var e in strW)
                            if (e >= '1' && e <= '9' || e == '0') str2W += e;
                        if (str2W.Length > 0 && str2W.Length < 9) Setting.FildWidth = int.Parse(str2W);
                        //MainMenuSetting();
                        break;
                    case (6):
                        Console.Write("Write new Height (3-100): ");
                        string strH = Console.ReadLine(), str2H = "";
                        foreach (var e in strH)
                            if (e >= '0' && e <= '9') str2H += e;
                        if (str2H.Length > 0 && str2H.Length < 9) Setting.FildHeight = int.Parse(str2H);
                        //MainMenuSetting();
                        break;
                    case (7):
                        System.Console.Write("Write new Game speed: ");
                        string strS = Console.ReadLine(), str2S = "";
                        foreach (var e in strS)
                            if (e >= '1' && e <= '9' || e == '0') str2S += e;
                        if (str2S.Length > 0 && str2S.Length < 9) Setting.Speed = int.Parse(str2S);
                        //MainMenuSetting();
                        break;
                    case (8):
                        isEnd = true;
                        break;
                    case (9):
                        if (Setting.ColorScreen == 0)
                        {
                            Setting.ColorScreen = 1;
                            //MainMenuSetting();
                        }
                        else
                        {
                            Setting.ColorScreen = 0;
                            //MainMenuSetting();
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
                    case (8):
                        TETRISV1.Setting.WriteFileSetting();
                        isEnd = false;
                        break;
                    default:
                        //MainMenuSetting();
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
                    System.Console.WriteLine($"1 Screen type: {(Setting.isColorScreen == 1 ? "Color" : "Text")}");
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
                        Setting.isColorScreen = Setting.isColorScreen == 1 ? 0 : 1;
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
                    System.Console.WriteLine($"1 Single color for blocks: {(TETRISV1.Setting.isSingleColorBlock ? "Yes" : "No")}");
                    System.Console.Write($"2 Color blocks: "); writeColor(Setting.ConsColBrick);
                    System.Console.Write($"3 Background color: "); writeColor(Setting.ConsColBackground);
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
                        TETRISV1.Setting.isSingleColorBlock = !TETRISV1.Setting.isSingleColorBlock;
                        break;
                    case (2):
                        Setting.ConsColBrick = ChosesColor();
                        break;
                    case (3):
                        Setting.ConsColBackground = ChosesColor();
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
                    System.Console.Write($"1 S:\t "); writeColor(Setting.FigColor[0]);
                    System.Console.Write($"2 RS:\t "); writeColor(Setting.FigColor[1]);
                    System.Console.Write($"3 L:\t "); writeColor(Setting.FigColor[2]);
                    System.Console.Write($"4 RL:\t "); writeColor(Setting.FigColor[3]);
                    System.Console.Write($"5 Cube:\t "); writeColor(Setting.FigColor[4]);
                    System.Console.Write($"6 Line:\t "); writeColor(Setting.FigColor[5]);
                    System.Console.Write($"7 T:\t "); writeColor(Setting.FigColor[6]);
                    System.Console.Write("\n8 Back");
                    ColorFigureHandler(ReturnKey());
                } while (isEnd);

            }
            static void ColorFigureHandler(int v)
            {
                switch (v)
                {
                    case (1):
                        Setting.FigColor[0] = ChosesColor();
                        break;
                    case (2):
                        Setting.FigColor[1] = ChosesColor();
                        break;
                    case (3):
                        Setting.FigColor[2] = ChosesColor();
                        break;
                    case (4):
                        Setting.FigColor[3] = ChosesColor();
                        break;
                    case (5):
                        Setting.FigColor[4] = ChosesColor();
                        break;
                    case (6):
                        Setting.FigColor[5] = ChosesColor();
                        break;
                    case (7):
                        Setting.FigColor[6] = ChosesColor();
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
                    System.Console.WriteLine($"1 Char block: {Setting.keyBuild}");
                    System.Console.Write($"2 Color block: "); writeColor(Setting.ConsColBrick);
                    System.Console.WriteLine($"3 Char background: {Setting.background}");
                    System.Console.Write($"4 Color background: "); writeColor(Setting.ConsColBackground);
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
                        Setting.keyBuild = Console.ReadKey(true).KeyChar;
                        break;
                    case (2):
                        Setting.ConsColBrick = ChosesColor();
                        break;
                    case (3):
                        System.Console.Write("Change new Key: ");
                        Setting.background = Console.ReadKey().KeyChar;
                        break;
                    case (4):
                        Setting.ConsColBackground = ChosesColor();
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
                    System.Console.WriteLine($"1 Widht: {Setting.FildWidth}");
                    System.Console.WriteLine($"2 Height: {Setting.FildHeight}");
                    System.Console.WriteLine($"3 Speed: {Setting.Speed}");
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
                        Console.Write("Write new Width (3-100): ");
                        string strW = Console.ReadLine(), str2W = "";
                        foreach (var e in strW)
                            if (e >= '1' && e <= '9' || e == '0') str2W += e;
                        if (str2W.Length > 0 && str2W.Length < 9)
                        {
                            num = int.Parse(str2W);
                            if (num > 3 && num < 101) Setting.FildWidth = num;
                        }
                        break;
                    case (2):
                        Console.Write("Write new Height (3-100): ");
                        string strH = Console.ReadLine(), str2H = "";
                        foreach (var e in strH)
                            if (e >= '0' && e <= '9') str2H += e;
                        if (str2H.Length > 0 && str2H.Length < 9)
                        {
                            num = int.Parse(str2H);
                            if (num > 3 && num < 101) Setting.FildHeight = num;
                        }
                        break;
                    case (3):
                        System.Console.Write("Write new Game speed: ");
                        string strS = Console.ReadLine(), str2S = "";
                        foreach (var e in strS)
                            if (e >= '1' && e <= '9' || e == '0') str2S += e;
                        if (str2S.Length > 0 && str2S.Length < 9)
                        {
                            num = int.Parse(str2S);
                            if (num > 3 && num < 101) Setting.Speed = num;
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

        static void writeColor(ConsoleColor c)
        {
            Console.BackgroundColor = c;
            System.Console.WriteLine(string.Format($"{c,12}"));
            Console.ResetColor();
        }
        static ConsoleColor ChosesColor()
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

