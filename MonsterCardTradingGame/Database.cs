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
                string query = "select * from public.Users";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read() && position <= 100)
                {
                    if (reader.GetString(0) == username)
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
                    Console.WriteLine(" Login successfull!");
                    success = true;
                }
                else
                {
                    Console.WriteLine(" Login failed!\n Check your credentials!");
                    success = false;
                }
                //if (success)
                //{
                //    string query2 = @"select * from public.Users where username = @_username";
                //    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                //    cmd2.Parameters.AddWithValue("_username", username); 
                //    using NpgsqlDataReader reader = cmd2.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        _player = new User(reader.GetString(0).ToString());
                //    }
                //}
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
                        Console.WriteLine(" Signup successfull!");
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
                Console.WriteLine(winner);
                cmd.Parameters.AddWithValue("_username", winner);
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
                Console.WriteLine(loser);
                cmd3.Parameters.AddWithValue("_username3", loser);
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
    }
}
