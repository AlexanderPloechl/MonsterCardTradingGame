using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
//postgre pw 12345678
//port 5432
namespace MonsterCardTradingGame
{
    class Program
    {
        private static bool _isLoggedIn;
        private static char _menuInput;
        private static bool _quit;
        private static User player;
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
                            Console.Write("\n Username: ");
                            string username = Console.ReadLine();
                            Console.Write(" Password: ");
                            string password = Console.ReadLine();
                            _isLoggedIn = Login(username, password);
                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case 'S':
                        if (!_isLoggedIn)
                        {
                            Console.Write("\n Username: ");
                            string username = Console.ReadLine();
                            Console.Write(" Password: ");
                            string password = Console.ReadLine();
                            _isLoggedIn = Signup(username, password); //signing up automatically logs you in
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

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '2':
                        if (_isLoggedIn)
                        {

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '3':
                        if (_isLoggedIn)
                        {

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '4':
                        if (_isLoggedIn)
                        {

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '5':
                        if (_isLoggedIn)
                        {
                            User player1 = player;
                            player1.PrintUserData();
                            player1.BuyPackage();
                            player1.PrintUserData();
                            player1.OpenPackage();
                            player1.PrintUserData();
                            player1.BuildDeck();
                            player1.PrintUserData();

                            User player2 = new User("ProGamer");
                            player2.PrintUserData();
                            player2.BuyPackage();
                            player2.PrintUserData();
                            player2.OpenPackage();
                            player2.PrintUserData();
                            player2.BuildDeck();
                            player2.PrintUserData();
                            Battle battle = new Battle(player1, player2);
                            User winner = battle.Fight();
                            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                            if (winner == null)
                            {
                                Console.WriteLine("battle ended in a draw");
                            }
                            else
                            {
                                Console.WriteLine($" {winner.GetName()}  won the game");
                                int elo;
                                if(player1 == winner)
                                {
                                    elo = UpdateElos(winner, player2);
                                    if(elo >= 0)
                                    {
                                        Console.WriteLine($" You gained 3 ELO for winning! Your ELO: {elo}");
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Error while updating ELO");
                                    }
                                }
                                else if (player2 == winner)
                                {
                                    elo = UpdateElos(winner, player1);
                                    if (elo >= 0)
                                    {
                                        Console.WriteLine($" You lost 5 ELO for winning! Your ELO: {elo}");
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

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '7':
                        if (_isLoggedIn)
                        {

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '8':
                        if (_isLoggedIn)
                        {

                        }
                        else
                        {
                            PrintUsage();
                        }
                        break;
                    case '9':
                        if (_isLoggedIn)
                        {
                            DisplayScoreboard(player);
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

        private static void DisplayScoreboard(User player)
        {
            Console.WriteLine("\n ><><><><><><><");
            Console.WriteLine(" > Scoreboard <");
            Console.WriteLine(" ><><><><><><><");
            Console.WriteLine(" Position - Player: ELO");
            using (NpgsqlConnection con = GetConnection())
            {
                int position = 1;
                string query = "select * from public.Users";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read() && position <= 100)
                {
                    if (reader.GetString(0) == player.GetName())
                    {
                        Console.WriteLine($" |{position} - {reader.GetString(0)}: {reader.GetInt32(2)} <= YOU");
                    }
                    else
                    {
                        Console.WriteLine($" |{position} - {reader.GetString(0)}: {reader.GetInt32(2)}");
                    }
                    position++;
                }
            }
        }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=12345678;Database=postgres;");
        }

        private static bool Login(string username, string password)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                bool success;
                string query = @"select * from UserLogin(:_username,:_password)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                cmd.Parameters.AddWithValue("_password", password);
                con.Open();
                int n = (int)cmd.ExecuteScalar();
                if (n == 1)
                {
                    Console.WriteLine(" Login successfull!");
                    success = true;
                }
                else
                {
                    Console.WriteLine(" Login failed!\n Check your credentials!");
                    success = false;
                }
                if (success)
                {
                    string query2 = @"select * from public.Users where username = @_username";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("_username", username); 
                    using NpgsqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        player = new User(reader.GetString(0).ToString());
                    }
                }
                con.Close();
                return success;
            }
        }

        private static bool Signup(string username, string password)
        {
            bool success;
            if (!CheckIfUserExists(username))
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    string query = @"insert into public.Users(username, password) values(@_username, @_password)";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("_username", username);
                    cmd.Parameters.AddWithValue("_password", password);
                    con.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        Console.WriteLine(" Signup successfull!");
                        Login(username, password);
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine(" Signup failed!");
                        success = false;
                    }
                    con.Close();
                }
            }
            else
            {
                Console.WriteLine($" The name {username} is already in use. Please choose a different name.");
                success = false;
            }
            return success;
        }

        private static bool CheckIfUserExists(string username)
        {
            bool exists;
            using(NpgsqlConnection con = GetConnection())
            {
                string query = @"select count (*) from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                int n = Int32.Parse(cmd.ExecuteScalar().ToString());
                if(n == 1)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }
                con.Close();
                return exists;
            }
        }
        private static int UpdateElos(User winner, User loser)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                //update winners elo
                int eloWinner = -1;
                string query = @"select * from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                Console.WriteLine(winner.GetName());
                cmd.Parameters.AddWithValue("_username", winner.GetName());
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"winner {(int)reader.GetInt32(2)}");
                    eloWinner = (int)reader.GetInt32(2);
                }
                if (eloWinner >= 0)
                {
                    eloWinner = eloWinner + 3;
                }
                con.Close();
                string query2 = @"update public.Users set elo = @_elo where username = @_username2";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("_username2", winner.GetName());
                cmd2.Parameters.AddWithValue("_elo", eloWinner);
                con.Open();
                int n = cmd2.ExecuteNonQuery();
                if(n != 1)
                {
                    Console.WriteLine("error while updating elo in database (winner)");
                }
                con.Close();
                //update losers elo
                int eloLoser = -1;
                string query3 = @"select * from public.Users where username = @_username3";
                NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con);
                Console.WriteLine(loser.GetName());
                cmd3.Parameters.AddWithValue("_username3", loser.GetName());
                con.Open();
                using NpgsqlDataReader reader2 = cmd3.ExecuteReader();
                while (reader2.Read())
                {
                    Console.WriteLine($"loser {(int)reader2.GetInt32(2)}");
                    eloLoser = (int)reader2.GetInt32(2);
                }
                if (eloLoser > 5)
                {
                    eloLoser = eloLoser - 5;
                }
                else
                {
                    eloLoser = 0;
                }
                con.Close();
                string query4 = @"update public.Users set elo = @_elo where username = @_username4";
                NpgsqlCommand cmd4 = new NpgsqlCommand(query4, con);
                cmd4.Parameters.AddWithValue("_username4", loser.GetName());
                cmd4.Parameters.AddWithValue("_elo", eloLoser);
                con.Open();
                int n2 = cmd4.ExecuteNonQuery();
                if (n2 != 1)
                {
                    Console.WriteLine("error while updating elo in database (loser)");
                }
                con.Close();
                if (player == winner)
                {
                    return eloWinner;
                }
                else if (player == loser)
                {
                    return eloLoser;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}