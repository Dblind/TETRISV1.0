using System;
using System.Threading;

class program
{

    static void MainT()
    {
        ConsoleKeyInfo cki = new ConsoleKeyInfo();
        bool ff = true;
        while (true)
        {
            Console.Write('-');
            if (Console.KeyAvailable == true)
            {
                cki = Console.ReadKey(true);
                System.Console.Write(cki.Key);
                ff = true;
                //test();
            }
            //System.Console.WriteLine("dfafd");
            Thread.Sleep(10);
        }

        void test()
        {
            if (ff)
            {
                System.Console.Write(cki.Key);
                if (Console.KeyAvailable) test();
                else ff = false;
            }
        }
    }
}