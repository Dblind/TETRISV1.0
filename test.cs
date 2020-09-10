using System;
using System.Threading;
using System.Text;
using System.IO;

class program
{

    delegate void deleg(int f);
    class run
    {
    }
    static void MainT()
    {
        System.Console.WriteLine(Environment.CurrentDirectory);
        System.Console.WriteLine();
        //File.WriteAllText("test.txt","HW!");
        var rwf = new RWF();
        //File.WriteAllBytes(rwf,"set.o");
        string str = File.ReadAllText("test.txt");
        System.Console.WriteLine(str);
        string[] lines = File.ReadAllLines("test.txt");
        foreach(var e in lines) System.Console.WriteLine(e);
    }
    
    class RWF
    {
        int r = 0;
        string s = "a";
    }
}