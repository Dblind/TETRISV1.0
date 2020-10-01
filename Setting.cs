using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace TETRISV1
{
    class Settings
    {
        public static char keyBuild = '$', keyBottom = '#', keyBackground = '.';
        public static int FildWidth = 10;
        public static int FildHeight = 10;
        public static int Speed = 10;
        public static int isColorScreen = 1;
        public static ConsoleColor ConsColBrick { get; set; } = ConsoleColor.Cyan;
        public static ConsoleColor ConsColBackground { get; set; } = ConsoleColor.Black;
        public static ConsoleColor[] FigColor = new ConsoleColor[8];
        public static bool isSingleColorBlock = false;
        public static char[] controlsKeys = new char[5];

        // SFig[0], RSFig[1], LFig[2], RLFig[3], Cube[4], Line[5], TFig[6], isColorScreen
        public static void ReadSettingsFileAndApply()
        {
            string[] settingsStrings = File.ReadAllLines("settings.txt");
            keyBuild = settingsStrings[0][0];
            keyBottom = (char)settingsStrings[1][0];
            keyBackground = (char)settingsStrings[2][0];
            FildWidth = int.Parse(settingsStrings[3]);
            FildHeight = int.Parse(settingsStrings[4]);
            ConsColBrick = (ConsoleColor)int.Parse(settingsStrings[5]);
            ConsColBackground = (ConsoleColor)int.Parse(settingsStrings[6]);
            Speed = int.Parse(settingsStrings[7]);
            //8 - 14
            for (int i = 0; i < 7; i++)
            {
                FigColor[i] = (ConsoleColor)int.Parse(settingsStrings[i + 8]);
            }
            isColorScreen = int.Parse(settingsStrings[15]);
            isSingleColorBlock = int.Parse(settingsStrings[16]) == 1 ? true : false;
            //17-21
            for (int i = 0; i < 5; i++) controlsKeys[i] = char.Parse(settingsStrings[i+17]);
            
        }
        public static void WriteSettingsInFile()
        {
            StringBuilder sB = new StringBuilder();
            sB.Append($"{keyBuild}\n{keyBottom}\n{keyBackground}\n{FildWidth}\n{FildHeight}\n"); // 5
            sB.Append($"{(int)ConsColBrick}\n{(int)ConsColBackground}\n{Speed}\n");
            sB.Append($"{(int)FigColor[0]}\n{(int)FigColor[1]}\n{(int)FigColor[2]}\n{(int)FigColor[3]}\n");
            sB.Append($"{(int)FigColor[4]}\n{(int)FigColor[5]}\n{(int)FigColor[6]}\n{isColorScreen}\n");
            // 16
            sB.Append($"{(isSingleColorBlock ? 1 : 0)}\n");
            //controls: left right down roll quit
            sB.Append($"{controlsKeys[0]}\n{controlsKeys[1]}\n{controlsKeys[2]}\n{controlsKeys[3]}\n{controlsKeys[4]}\n");

            File.WriteAllText("settings.txt", sB.ToString());
        }
    }
}