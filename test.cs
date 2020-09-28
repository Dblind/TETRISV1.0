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
        var rwf = new W1();
        //File.WriteAllBytes(rwf,"set.o");
        string str = File.ReadAllText("test.txt");
        System.Console.WriteLine(str);
        string[] lines = File.ReadAllLines("test.txt");
        foreach (var e in lines) System.Console.WriteLine(e);

        /* str = W1.ss;
        int r1 = W1.r1;

        str = W2.ss;
        r1 = W2.r2; */
    }

    class W1
    {
        //public static int r1 = 0;
        public static string ss = "a";
    }

    class W2
    {
        //public static int r2 = 0;
        public static string ss = "f";
    }
}