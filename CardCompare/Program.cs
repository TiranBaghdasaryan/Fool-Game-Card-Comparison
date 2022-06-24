using System;
using CardCompare.GameDir;

namespace CardCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(36,6);
            
            Console.ReadKey();
        }
    }
}