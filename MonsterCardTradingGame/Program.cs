using System;

namespace MonsterCardTradingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Package testpackage = new Package();
            //testpackage.PrintPackageContents();
            User player = new User("Alex", "12345678");
            player.PrintUserData();
            player.BuyPackage();
            player.PrintUserData();
            player.OpenPackage();
            player.PrintUserData();
            player.BuildDeck();
            player.PrintUserData();
;        }
    }
}
