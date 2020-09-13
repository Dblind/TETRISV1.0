using System;

namespace TETRISV1
{
    delegate void ReturnValue(int f);

    class Menu
    {

        public static ConsoleKeyInfo menuKeyInfo;
        public static char charMenuKey;
        static public void CallNewMenu(int f)
        {
            switch (f)
            {
                case (1):
                    Run.RunGame();
                    Fild.RunGame = true;
                    break;
                case (2):
                    Rellise.MainMenuSetting();
                    break;
                case (4):
                    RunNewGame.RunGameFlag = false;
                    break;

            }
        }
        //Deleg(3);
    }

    class Rellise
    {
        static ReturnValue Deleg = Menu.CallNewMenu;
        public static void MainMenuRun()
        {
            Console.Clear();
            System.Console.WriteLine("1 New Game");
            System.Console.WriteLine("2 Setting");
            System.Console.WriteLine();
            System.Console.WriteLine("4 Exit");
            Menu.CallNewMenu(ReturnKey());
        }
        public static void MainMenuSetting()
        {
            Console.Clear();
            System.Console.WriteLine($"1 Char block: {Setting.keyBuild}");
            System.Console.WriteLine($"2 Block color: {Setting.ConsColBrick}");
            System.Console.WriteLine($"3 Char background: {Setting.background}");
            System.Console.WriteLine($"4 Background color: {Setting.ConsColBackground}");
            System.Console.WriteLine($"5 Widht: {Setting.FildWidth}");
            System.Console.WriteLine($"6 Height: {Setting.FildHeight}");
            System.Console.WriteLine($"7 Speed: {Setting.Speed}");
            System.Console.WriteLine("8 Back");
            MainMenuSettingHandler(ReturnKey());
        }
        static void MainMenuSettingHandler(int v)
        {
            switch (v)
            {

                case (1):
                    System.Console.Write("Change new Key: ");
                    Setting.keyBuild = Console.ReadKey().KeyChar;
                    MainMenuSetting();
                    break;
                case (3):
                    System.Console.Write("Change new Key: ");
                    Setting.background = Console.ReadKey().KeyChar;
                    MainMenuSetting();
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
                    MainMenuSetting();
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
                    MainMenuSetting();
                    break;
                case (5):
                    Console.Write("Write new Width (3-100): ");
                    string strW = Console.ReadLine(), str2W = "";
                    foreach (var e in strW)
                        if (e >= '1' && e <= '9' || e == '0') str2W += e;
                    if (str2W.Length > 0 && str2W.Length < 9) Setting.FildWidth = int.Parse(str2W);
                    MainMenuSetting();
                    break;
                case (6):
                    Console.Write("Write new Height (3-100): ");
                    string strH = Console.ReadLine(), str2H = "";
                    foreach (var e in strH)
                        if (e >= '0' && e <= '9') str2H += e;
                    if (str2H.Length > 0 && str2H.Length < 9) Setting.FildHeight = int.Parse(str2H);
                    MainMenuSetting();
                    break;
                case (7):
                    System.Console.Write("Write new Game speed: ");
                    string strS = Console.ReadLine(), str2S = "";
                    foreach (var e in strS)
                        if (e >= '1' && e <= '9' || e == '0') str2S += e;
                    if (str2S.Length > 0 && str2S.Length < 9) Setting.Speed = int.Parse(str2S);
                    MainMenuSetting();
                    break;
                case (8):
                    Setting.WriteFileSetting();
                    break;
                default:
                    MainMenuSetting();
                    break;
            }

        }

        public static int ReturnKey()
        {
            string str = null;
            do
            {
                Menu.menuKeyInfo = Console.ReadKey(true);
                Menu.charMenuKey = Menu.menuKeyInfo.KeyChar;
                //    str = Console.ReadKey(true).ToString();//"" + Menu.MMKey.KeyChar;
                if ('0' < Menu.charMenuKey & Menu.charMenuKey < '9')
                {
                    str = Menu.charMenuKey.ToString();
                }
            } while (str == null);
            return (int.Parse(str));
        }
    }
}