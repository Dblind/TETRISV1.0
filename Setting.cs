using System;

namespace TETRISV1
{
    class Setting
    {
        public static char keyBuild = '$', keyBottom = '#', background = '.';
        public static ConsoleColor ConsColBrick { get; set; } =ConsoleColor.Cyan; 
        public static ConsoleColor ConsColBackground { get; set; } =  ConsoleColor.Black;
    }
}