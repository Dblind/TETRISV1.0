using System;
using System.Threading;

class program
{

    delegate void deleg(int f);
    class run
    {

    }
    static void MainT()
    {
        deleg del1 = Sum;
        del1 +=Sum;
        del1(3);
        void Sum(int f)
        {
            System.Console.WriteLine($"{f}+2=5");
               }
               var ffasf = 99;
        // int Score = 23230;
        // void Testf(int f)
        // {
        //     System.Console.WriteLine(String.Format($"{f:000000000}", Score));
        //     System.Console.Write(String.Format($"{f,10}", Score)); System.Console.WriteLine("|");
        //     //System.Console.WriteLine("test");
        // }
        // Testf(0); Testf(3012); Testf(int.MaxValue);
    }
}