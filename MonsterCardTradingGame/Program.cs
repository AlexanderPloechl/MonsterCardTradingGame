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
            User player1 = new User("player1", "12345678");
            player1.PrintUserData();
            player1.BuyPackage();
            player1.PrintUserData();
            player1.OpenPackage();
            player1.PrintUserData();
            player1.BuildDeck();
            player1.PrintUserData();

            User player2 = new User("player2", "12345678");
            player2.PrintUserData();
            player2.BuyPackage();
            player2.PrintUserData();
            player2.OpenPackage();
            player2.PrintUserData();
            player2.BuildDeck();
            player2.PrintUserData();

            Battle battle = new Battle(player1, player2);
            User winner = battle.Fight();
            if(winner == null)
            {
                Console.WriteLine("battle ended in a draw");
            }
            else
            {
                Console.WriteLine(winner.GetName() + "won the game");
            }
           

        }
    }
}
