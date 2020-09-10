using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace TETRISV1
{
    class Setting
    {
        public static char keyBuild = '$', keyBottom = '#', background = '.';
        public static ConsoleColor ConsColBrick { get; set; } = ConsoleColor.Cyan;
        public static ConsoleColor ConsColBackground { get; set; } = ConsoleColor.Black;
        public static void ReadFileSetting()
        {
            string[] settingLines = File.ReadAllLines("s/setting.txt");
            keyBuild = (char)settingLines[0][0];
            keyBottom = (char)settingLines[1][0];
            background = (char)settingLines[2][0];
            ConsColBrick = (ConsoleColor)int.Parse(settingLines[3]);
            ConsColBackground = (ConsoleColor)int.Parse(settingLines[4]);
        }
        public static void WriteFileSetting()
        {
            StringBuilder sB = new StringBuilder();
            sB.Append($"{keyBuild}\n{keyBottom}\n{background}\n");
            sB.Append((int)ConsColBrick);
            sB.Append("\n");
            sB.Append((int)ConsColBackground);
            File.WriteAllText("s/setting.txt",sB.ToString());
        }
    }
}