using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace MonsterCardTradingGame
{
    public class Database
    {
        private static Database _database; //singelton
        private const string _CONNECTION_STRING = "Server = localhost; Port=5432;User Id = postgres; Password=12345678;Database=postgres;";
        

        public static Database GetDatabaseInstance()
        {
            if (_database == null)
            {
                _database = new Database();
            }
            return _database;
        }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_CONNECTION_STRING);
        }

        public static void GetScoreboard(string username)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                int position = 1;
                int prev = 0;
                int counter = 0;
                string query = "select * from public.Users order by elo desc";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read() && counter < 100)
                {
                    if (reader.GetString(0) == username)
                    {
                        Console.WriteLine($" |{position} - {reader.GetString(0)}: {reader.GetInt32(2)} <= YOU");
                    }
                    else
                    {
                        Console.WriteLine($" |{position} - {reader.GetString(0)}: {reader.GetInt32(2)}");
                    }
                    if(prev > reader.GetInt32(2))
                    {
                        position++;
                    }
                    prev = reader.GetInt32(2);
                    counter++;
                }
            }
        }

        public static bool Login(string username, string password)
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
                    Console.WriteLine("\n ><><><><><><><><><><");
                    Console.WriteLine("  Login successfull!");
                    Console.WriteLine(" ><><><><><><><><><><");
                    success = true;
                }
                else
                {
                    Console.WriteLine(" Login failed!\n Check your credentials!");
                    success = false;
                }
                con.Close();
                return success;
            }
        }

        public static bool Signup(string username, string password)
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
                        Console.WriteLine("\n ><><><><><><><><><><");
                        Console.WriteLine("  Signup successfull!");
                        Console.WriteLine(" ><><><><><><><><><><");
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

        public static bool CheckIfUserExists(string username)
        {
            bool exists;
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select count (*) from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                int n = Int32.Parse(cmd.ExecuteScalar().ToString());
                if (n == 1)
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

        public static int UpdateElos(string winner, string loser, string player)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                //update winners elo
                int eloWinner = -1;
                string query = @"select * from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", winner);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    eloWinner = (int)reader.GetInt32(2);
                }
                if (eloWinner >= 0)
                {
                    eloWinner = eloWinner + 3;
                }
                con.Close();
                string query2 = @"update public.Users set elo = @_elo where username = @_username2";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("_username2", winner);
                cmd2.Parameters.AddWithValue("_elo", eloWinner);
                con.Open();
                int n = cmd2.ExecuteNonQuery();
                if (n != 1)
                {
                    Console.WriteLine("error while updating elo in database (winner)");
                }
                con.Close();
                //update losers elo
                int eloLoser = -1;
                string query3 = @"select * from public.Users where username = @_username3";
                NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con);
                cmd3.Parameters.AddWithValue("_username3", loser);
                con.Open();
                using NpgsqlDataReader reader2 = cmd3.ExecuteReader();
                while (reader2.Read())
                {
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
                cmd4.Parameters.AddWithValue("_username4", loser);
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

        public static void ShowCards(string owner, bool IsInDeck)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                //owned cards
                Dictionary<string, int> cards = new Dictionary<string, int>();
                string query = @"select * from public.CardsInInventory where owner = @_owner and IsInDeck = @_IsInDeck";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_owner", owner);
                cmd.Parameters.AddWithValue("_IsInDeck", IsInDeck);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cards.Add(reader.GetString(1), reader.GetInt32(3));
                }
                con.Close();
                //split into monsters and spells
                Dictionary<string, int> monsters = new Dictionary<string, int>();
                Dictionary<string, int> spells = new Dictionary<string, int>();
                string query2 = @"select * from public.Cards where cardname = @_cardname";
                foreach (var card in cards)
                {
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("_cardname", card.Key);
                    con.Open();
                    using NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        if(reader2.GetBoolean(0)){
                            monsters.Add(reader2.GetString(1), card.Value);
                        }
                        else
                        {
                            spells.Add(reader2.GetString(1), card.Value);
                        }
                    }
                    con.Close();
                }
                //spells
                Console.WriteLine($"\n Spells:");
                Console.WriteLine(" __________");
                string query3 = @"select * from public.Spells where cardname = @_cardname";
                foreach (var spell in spells)
                {
                    NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con);
                    cmd3.Parameters.AddWithValue("_cardname", spell.Key);
                    con.Open();
                    using NpgsqlDataReader reader3 = cmd3.ExecuteReader();
                    while (reader3.Read())
                    {
                        Console.WriteLine($" || {reader3.GetString(0)} | ElementType - {(ElementType)reader3.GetInt32(1)} | Damage - {reader3.GetInt32(2)} | {spell.Value}x");
                    }
                    con.Close();
                }
                //monsters
                Console.WriteLine($"\n Monsters:");
                Console.WriteLine(" __________");
                string query4 = @"select * from public.Monsters where cardname = @_cardname";
                foreach (var monster in monsters)
                {
                    NpgsqlCommand cmd4 = new NpgsqlCommand(query4, con);
                    cmd4.Parameters.AddWithValue("_cardname", monster.Key);
                    con.Open();
                    using NpgsqlDataReader reader4 = cmd4.ExecuteReader();
                    while (reader4.Read())
                    {
                        Console.WriteLine($" || {reader4.GetString(0)} | MonsterType - {(MonsterType)reader4.GetInt32(1)} | ElementType - {(ElementType)reader4.GetInt32(2)} | Damage - {reader4.GetInt32(3)} | {monster.Value}x");
                    }
                    con.Close();
                }
            }
        }
        public static int CountCardsInDeck(string username)
        {
            int count;
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select count (*) from public.CardsInInventory where owner = @_username and IsInDeck = true";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                count = Int32.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                return count;
            }
        }
        public static void MoveCardToDeckOrStack(string cardname, bool ToDeck, string owner)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"update public.CardsInInventory set IsInDeck = @_ToDeck where owner = @_owner and cardname = @_cardname";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_ToDeck", ToDeck);
                cmd.Parameters.AddWithValue("_owner", owner);
                cmd.Parameters.AddWithValue("_cardname", cardname);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if (n != 1)
                {
                    Console.WriteLine("\n This Card does not exist or you don't own it!");
                }
                con.Close();
            }
        }

        public static void DeleteDeck(string owner)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"update public.CardsInInventory set IsInDeck = false where owner = @_owner";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_owner", owner);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if (n <= 0)
                {
                    Console.WriteLine(" Error while deleting your deck");
                }
                con.Close();
            }
        }

        public static void BuyPackages(string username, int amount, int coins)
        {
            bool success = true;
            using (NpgsqlConnection con = GetConnection())
            {
                if (coins / amount < 5)
                {
                    Console.WriteLine(" You don't have enough coins.");
                }
                else
                {
                    string query2 = @"update public.Users set packages = packages + @_amount where username = @_username";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("_username", username);
                    cmd2.Parameters.AddWithValue("_amount", amount);
                    con.Open();
                    int n = cmd2.ExecuteNonQuery();
                    if (n != 1)
                    {
                        Console.WriteLine(" Error while receiving packages");
                        success = false;
                    }
                    con.Close();
                    string query3 = @"update public.Users set coins = coins - @_price where username = @_username";
                    NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con);
                    cmd3.Parameters.AddWithValue("_username", username);
                    cmd3.Parameters.AddWithValue("_price", amount * 5);
                    con.Open();
                    int n2 = cmd3.ExecuteNonQuery();
                    if (n2 != 1)
                    {
                        Console.WriteLine(" Error while paying for packages");
                        success = false;
                    }
                    con.Close();
                    if (success)
                    {
                        Console.WriteLine($"\n You bought {amount} packages.");
                    }
                }
            }
        }

        

        public static List<string> GetAllCardNames()
        {
            List<string> cards = new List<string>();
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select * from public.Cards";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cards.Add(reader.GetString(1));
                }
                con.Close();
            }
            return cards;
        }

        public static bool UserHasPackages(string username)
        {
            bool HasPackages = false;
            using (NpgsqlConnection con = GetConnection())
            {
                string query = "select * from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(4) > 0)
                    {
                        HasPackages = true;
                    }
                }
                con.Close();
            }
            return HasPackages;
        }

        public static void AddCardToStack(string username, string card)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"insert into public.CardsInInventory(owner, cardname, quantity) values(@_owner, @_cardname, '1') on conflict (owner, cardname) do update set quantity = CardsInInventory.quantity + 1 where excluded.owner = @_owner and excluded.cardname = @_cardname";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_owner", username);
                cmd.Parameters.AddWithValue("_cardname", card);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if (n == 1)
                {
                    Console.WriteLine($" {card}");
                }
                else
                {
                    Console.WriteLine(" Adding to stack failed!");
                }
                con.Close();
            }
        }

        public static void DecrementNumberOfPackages(string username)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"update public.Users set packages = packages - 1 where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if (n != 1)
                {
                    Console.WriteLine(" Decrementing Packages failed!");
                }
                con.Close();
            }
        }

        public static List<ICard> GetDeck(string username)
        {
            List<ICard> cards = new List<ICard>();
            List<string> cardnames = new List<string>();
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select * from public.CardsInInventory where owner = @_owner and IsInDeck = true";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_owner", username);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cardnames.Add(reader.GetString(1));
                }
                con.Close();
                //split into monsters and spells
                List<string> monsternames = new List<string>();
                List<string> spellnames = new List<string>();
                string query2 = @"select * from public.Cards where cardname = @_cardname";
                foreach (var card in cardnames)
                {
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("_cardname", card);
                    con.Open();
                    using NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        if (reader2.GetBoolean(0))
                        {
                            monsternames.Add(reader2.GetString(1));
                        }
                        else
                        {
                            spellnames.Add(reader2.GetString(1));
                        }
                    }
                    con.Close();
                }
                //spells
                string query3 = @"select * from public.Spells where cardname = @_cardname";
                foreach (var spell in spellnames)
                {
                    NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con);
                    cmd3.Parameters.AddWithValue("_cardname", spell);
                    con.Open();
                    using NpgsqlDataReader reader3 = cmd3.ExecuteReader();
                    while (reader3.Read())
                    {
                        cards.Add(new Spell(reader3.GetString(0), reader3.GetInt32(2), (ElementType)reader3.GetInt32(1)));
                    }
                    con.Close();
                }
                //monsters
                string query4 = @"select * from public.Monsters where cardname = @_cardname";
                foreach (var monster in monsternames)
                {
                    NpgsqlCommand cmd4 = new NpgsqlCommand(query4, con);
                    cmd4.Parameters.AddWithValue("_cardname", monster);
                    con.Open();
                    using NpgsqlDataReader reader4 = cmd4.ExecuteReader();
                    while (reader4.Read())
                    {
                        cards.Add(new Monster(reader4.GetString(0), reader4.GetInt32(3),(MonsterType)reader4.GetInt32(1), (ElementType)reader4.GetInt32(2)));
                    }
                    con.Close();
                }
            }
            return cards;
        }

        public static int GetElo(string username)
        {
            int elo = -1;
            using (NpgsqlConnection con = GetConnection())
            {
                string query = "select * from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    elo = reader.GetInt32(2);
                }
            }
            return elo;
        }

        public static int GetNumberOfCoins(string username)
        {
            int coins = -1;
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select * from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    coins = reader.GetInt32(3);
                }
                con.Close();
            }
            return coins;
        }

        public static int GetNumberOfPackages(string username)
        {
            int packages = -1;
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select * from public.Users where username = @_username";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("_username", username);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    packages = reader.GetInt32(4);
                }
                con.Close();
            }
            return packages;
        }

        public static string UpdateCredential(string username, string credential)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = "";
                string NewCredential = "";
                if (credential == "un")//username
                {
                    Console.Write("\n New Username: ");
                    NewCredential = Console.ReadLine();
                    bool exists = CheckIfUserExists(NewCredential);
                    if (!exists)
                    {
                        query = @"update public.Users set username = @_new where username = @_username";
                    }
                    else
                    {
                        Console.WriteLine(" Username is already in use!");
                    }
                }
                else if (credential == "pw")//password
                {
                    Console.Write("\n New Password: ");
                    NewCredential = Console.ReadLine();
                    Console.Write(" Retype Password: ");
                    string retype = Console.ReadLine();
                    if(NewCredential == retype)
                    {
                        query = @"update public.Users set password = @_new where username = @_username";
                    }
                    else
                    {
                        Console.WriteLine(" Your inputs did not match! Try again!");
                    }
                }
                else
                {
                    Console.WriteLine($" Error, cant update credential '{credential}'!");
                }
                if(query != "" && NewCredential != "")
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("_username", username);
                    cmd.Parameters.AddWithValue("_new", NewCredential);
                    con.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n != 1)
                    {
                        Console.WriteLine(" Updating credentials failed!");
                    }
                    con.Close();
                }
                return NewCredential;
            }
        }
    }
}