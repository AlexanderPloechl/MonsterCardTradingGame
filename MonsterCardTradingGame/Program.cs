using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace MonsterCardTradingGame
{
    class Program
    {
        private static bool _isLoggedIn;
        private static char _menuInput;
        private static bool _quit;
        private static User _player;
        private static Database database = Database.GetDatabaseInstance();
        static void Main(string[] args)
        {
            
            while (!_quit)
            {
                PrintMenu();
                _menuInput = Console.ReadKey().KeyChar;
                switch (_menuInput)
                {
                    case 'L':
                        if (!_isLoggedIn)
                        {
                            _isLoggedIn = PrintLoginScreen();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case 'S':
                        if (!_isLoggedIn)
                        {
                            _isLoggedIn = PrintSignupScreen(); //signing up automatically logs you in
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case 'Q':
                        _quit = true;
                        break;
                    case '1':
                        if (_isLoggedIn)
                        {
                            _player.ShowStack();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '2':
                        if (_isLoggedIn)
                        {
                            _player.BuildDeck();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '3':
                        if (_isLoggedIn)
                        {
                            _player.BuyPackages();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '4':
                        if (_isLoggedIn)
                        {
                            _player.OpenPackage();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '5':
                        if (_isLoggedIn)
                        {
                            User Bot = new User("Bot");
                            _player.GetDeckFromDb();
                            Bot.GetDeckFromDb();
                            Battle battle = new Battle(_player, Bot);
                            User winner = battle.Fight();
                            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                            if (winner == null)
                            {
                                Console.WriteLine(" The battle ended in a draw.");
                            }
                            else
                            {
                                Console.WriteLine($" {winner.GetName()} won the game");
                                if(_player == winner)
                                {
                                    _player.SetElo(Database.UpdateElos(winner.GetName(), Bot.GetName(), _player.GetName()));
                                    if (_player.GetElo() >= 0)
                                    {
                                        Console.WriteLine($" You gained 3 ELO for winning! Your ELO: {_player.GetElo()}");
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Error while updating ELO");
                                    }
                                }
                                else if (Bot == winner)
                                {
                                    _player.SetElo(Database.UpdateElos(winner.GetName(), _player.GetName(), _player.GetName()));
                                    if (_player.GetElo() >= 0)
                                    {
                                        Console.WriteLine($" You lost 5 ELO for losing! Your ELO: {_player.GetElo()}");
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Error while updating ELO");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(" error while calling UpdateElos");
                                }
                            }
                            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '6':
                        if (_isLoggedIn)
                        {
                            Console.WriteLine(" This feature has not been implementet yet");
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '7':
                        if (_isLoggedIn)
                        {
                            Console.WriteLine(" This feature has not been implementet yet");
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '8':
                        if (_isLoggedIn)
                        {
                            _player.ShowProfile();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '9':
                        if (_isLoggedIn)
                        {
                            DisplayScoreboard();
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }

        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n ><><><><><><><><><><><><><><><><><><><><><");
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
                Console.WriteLine($"\n Logged in as {_player.GetName()} || ELO {_player.GetElo()}\n");
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

        private static void PrintUsage()
        {
            Console.WriteLine("\n There is no such command!");
            if (_isLoggedIn)
            {
                Console.WriteLine(" Use the numbers 1 to 9 to select a command!");
            }
            else
            {
                Console.WriteLine(" Use either \"L\" (uppercase) or \"S\" (uppercase) to selcet a command!");
            }
            Console.WriteLine(" Or use \"Q\" (uppercase) to QUIT");
        }

        private static void DisplayScoreboard()
        {
            Console.WriteLine("\n ><><><><><><><");
            Console.WriteLine(" > Scoreboard <");
            Console.WriteLine(" ><><><><><><><");
            Console.WriteLine(" Position - player: ELO");
            Database.GetScoreboard(_player.GetName());
        }

        private static bool PrintLoginScreen()
        {
            Console.Write("\n Username: ");
            string username = Console.ReadLine();
            Console.Write(" Password: ");
            string password = Console.ReadLine();
            bool success = Database.Login(username, password);
            if (success)
            {
                _player = new User(username);
                int elo = Database.GetElo(username);
                if(elo < 0)
                {
                    Console.WriteLine(" Error while trying to get elo");
                }
                else
                {
                    _player.SetElo(elo);
                }
            }
            return success;
        }

        private static bool PrintSignupScreen()
        {
            Console.Write("\n Username: ");
            string username = Console.ReadLine();
            Console.Write(" Password: ");
            string password = Console.ReadLine();
            bool success = Database.Signup(username, password);
            if (success)
            {
                success = Database.Login(username, password);
                if (success)
                {
                    _player = new User(username);
                }
            }
            return success;
        }
    }
}