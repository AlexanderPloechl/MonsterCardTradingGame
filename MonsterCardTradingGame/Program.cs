using System;

namespace MonsterCardTradingGame
{
    class Program
    {
        private static bool _isLoggedIn;
        private static char _menuInput;
        private static bool _quit;
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Package testpackage = new Package();
            //testpackage.PrintPackageContents();
            /*User player1 = new User("player1", "12345678");
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
            }*/
            while (!_quit)
            {
                PrintMenu();
                _menuInput = Console.ReadKey().KeyChar;
                switch (_menuInput)
                {
                    case 'L':
                        if (!_isLoggedIn)
                        {

                        }
                        break;
                    case 'S':
                        if (!_isLoggedIn)
                        {

                        }
                        break;
                    case 'Q':
                        _quit = true;
                        break;
                    case '1':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '2':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '3':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '4':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '5':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '6':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '7':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '8':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    case '9':
                        if (_isLoggedIn)
                        {

                        }
                        break;
                    default:
                        Console.WriteLine("There is no such command!");
                        if (_isLoggedIn)
                        {
                            Console.WriteLine("Use the numbers 1 to 9 to select a command!");
                        }
                        else
                        {
                            Console.WriteLine("Use either \"L\" (uppercase) or \"S\" (uppercase) to selcet a command!");
                        }
                        Console.WriteLine("Or use \"Q\" (uppercase) to QUIT");
                        break;
                }
            }

        }
        private static void PrintMenu()
        {
            Console.WriteLine(" \n><><><><><><><><><><><><><><><><><><><><><");
            Console.WriteLine(" > ___      ___ _________   _____   _____ <");
            Console.WriteLine(" > |  \\    /  | |__   __|  / ___|  / ___| <");
            Console.WriteLine(" > |   \\  /   |    | |    | /     | / __  <");
            Console.WriteLine(" > | |\\ \\/ /| |    | |    | \\___  | \\_\\ | <");
            Console.WriteLine(" > |_| \\__/ |_|    |_|     \\____|  \\____| <");
            Console.WriteLine(" >                                        <");
            Console.WriteLine(" ><><><><><><><><><><><><><><><><><><><><><");
            if (!_isLoggedIn)
            {
                Console.WriteLine("\n Welcome to the MonsterTradingCardGame\n");
                Console.WriteLine(" Press \"L\" to LOGIN!");
                Console.WriteLine(" Press \"S\" to SIGN UP!");
                Console.WriteLine(" Press \"Q\" to QUIT!");
            }
            else
            {
                Console.WriteLine("\nLogged in as ... || ELO ...\n");
                Console.WriteLine(" Press \"1\" to VIEW your STACK!");
                Console.WriteLine(" Press \"2\" to BUILD a DECK!");
                Console.WriteLine(" Press \"3\" to BUY a PACKAGE!");
                Console.WriteLine(" Press \"4\" to OPEN a PACKAGE!");
                Console.WriteLine(" Press \"5\" to FIGHT against a BOT!");
                Console.WriteLine(" Press \"6\" to FIGHT against another PLAYER!");
                Console.WriteLine(" Press \"7\" to TRADE CARDS!");
                Console.WriteLine(" Press \"8\" to EDIT you Profile!");
                Console.WriteLine(" Press \"9\" to VIEW the SCOREBOARD!");
                Console.WriteLine(" Press \"Q\" to QUIT!");
            }
            
        }
    }
}
