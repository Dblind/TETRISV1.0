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
        public static int ColorScreen = 1;
        public static ConsoleColor ConsColBrick { get; set; } = ConsoleColor.Cyan;
        public static ConsoleColor ConsColBackground { get; set; } = ConsoleColor.Black;
        public static ConsoleColor[] FigColor = new ConsoleColor[8];

        // SFig[0], RSFig[1], LFig[2], RLFig[3], Cube[4], Line[5], TFig[6], ColorScreen
        public static void ReadFileSetting()
        {
            string[] settingLines = File.ReadAllLines("setting.txt");
            keyBuild = settingLines[0][0];
            keyBottom = (char)settingLines[1][0];
            background = (char)settingLines[2][0];
            FildWidth = int.Parse(settingLines[3]);
            FildHeight = int.Parse(settingLines[4]);
            ConsColBrick = (ConsoleColor)int.Parse(settingLines[5]);
            ConsColBackground = (ConsoleColor)int.Parse(settingLines[6]);
            Speed = int.Parse(settingLines[7]);
            for (int i = 0; i < 7; i++)
            {
                FigColor[i] = (ConsoleColor)int.Parse(settingLines[i + 8]);
            }
            ColorScreen = int.Parse(settingLines[15]);
        }
        public static void WriteFileSetting()
        {
            StringBuilder sB = new StringBuilder();
            sB.Append($"{keyBuild}\n{keyBottom}\n{background}\n{FildWidth}\n{FildHeight}\n"); // 5
            sB.Append($"{(int)ConsColBrick}\n{(int)ConsColBackground}\n{Speed}\n");
            sB.Append($"{(int)FigColor[0]}\n{(int)FigColor[1]}\n{(int)FigColor[2]}\n{(int)FigColor[3]}\n");
            sB.Append($"{(int)FigColor[4]}\n{(int)FigColor[5]}\n{(int)FigColor[6]}\n{ColorScreen}\n");

            File.WriteAllText("setting.txt", sB.ToString());
        }
    }
}