using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace TETRISV1
{
    class Setting
    {
        public static char keyBuild = '$', keyBottom = '#', background = '.';
        public static int FildWidth = 10;
        public static int FildHeight = 10;
        public static int Speed = 10;
        public static int ColorScreen = 0;
        public static ConsoleColor ConsColBrick { get; set; } = ConsoleColor.Cyan;
        public static ConsoleColor ConsColBackground { get; set; } = ConsoleColor.Black;
        public static void ReadFileSetting()
        {
            string[] settingLines = File.ReadAllLines("setting.txt");
            keyBuild = (char)settingLines[0][0];
            keyBottom = (char)settingLines[1][0];
            background = (char)settingLines[2][0];
            FildWidth = int.Parse(settingLines[3]);
            FildHeight = int.Parse(settingLines[4]);
            ConsColBrick = (ConsoleColor)int.Parse(settingLines[5]);
            ConsColBackground = (ConsoleColor)int.Parse(settingLines[6]);
            Speed = int.Parse(settingLines[7]);
        }
        public static void WriteFileSetting()
        {
            StringBuilder sB = new StringBuilder();
            sB.Append($"{keyBuild}\n{keyBottom}\n{background}\n{FildWidth}\n{FildHeight}\n");
            sB.Append($"{(int)ConsColBrick}\n{(int)ConsColBackground}\n{Speed}");
            File.WriteAllText("setting.txt",sB.ToString());
        }
    }
}