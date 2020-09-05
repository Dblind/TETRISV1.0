using System;

namespace TETRISV1
{
    delegate void ReturnValue(int f);

    class Menu
    {

        public static ConsoleKeyInfo MMKey;
        public static char charMMKey;
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
            System.Console.WriteLine($"1 Char block: {Move.keyBuild}");
            System.Console.WriteLine($"2 Block color: ");
            System.Console.WriteLine($"3 Char background: {Move.background}");
            System.Console.WriteLine($"4 Background color\n");
            System.Console.WriteLine("6 To back");
            MainMenuSettingHandler(ReturnKey());
        }
        static void MainMenuSettingHandler(int v)
        {
            switch (v)
            {

                case (1):
                    System.Console.Write("Change new Key: ");
                    Move.keyBuild = Console.ReadKey().KeyChar;
                    MainMenuSetting();
                    break;
                case (3):
                    System.Console.Write("Change new Key: ");
                    Move.background = Console.ReadKey().KeyChar;
                    MainMenuSetting();
                    break;
                case (2):
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
            }

        }

        public static int ReturnKey()
        {
            string str = null;
            do
            {
                Menu.MMKey = Console.ReadKey(true);
                Menu.charMMKey = Menu.MMKey.KeyChar;
                //    str = Console.ReadKey(true).ToString();//"" + Menu.MMKey.KeyChar;
                if ('0' < Menu.charMMKey & Menu.charMMKey < '9')
                {
                    str = Menu.charMMKey.ToString();
                }
            } while (str == null);
            return (int.Parse(str));
        }
    }
}