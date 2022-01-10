using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class User
    {
        Database database = Database.GetDatabaseInstance();
        public User(string name)
        {
            _name = name;
            _coins = 20;
            _deck = new Deck();
        }
        private Random random = new Random();
        private Deck _deck { get; set; }
        private string _name { get; set; }
        private int _elo { get; set; }
        private int _coins { get; set; }

        public void PrintContents(List<ICard> CardList)
        {
            int index = 1;
            foreach (ICard card in CardList)
            {
                Console.WriteLine($"{index}: {card} - {card.name}");
                index++;
            }
        }

        public int GetNumberOfCardsInDeck()
        {
            return _deck.cards.Count;
        }

        public void GetDeckFromDb()
        {
            if (_deck.cards.Count != 4)
            {
                List<ICard> cards = Database.GetDeck(_name);
                _deck.FillDeck(cards);
            }
            if (_deck.cards.Count != 4)
            {
                Console.WriteLine(" You need to build a Deck first");
            }
        }

        public ICard GetRandomCardFromDeck()
        {
            int randomIndex = random.Next(_deck.cards.Count);
            return _deck.cards[randomIndex];
        }

        public void AddCardToDeck(ICard card)
        {
            _deck.cards.Add(card);
        }

        public void RemoveCardFromDeck(ICard card)
        {
            _deck.cards.Remove(card);
        }

        public string GetName()
        {
            return _name;
        }

        public void SetElo(int elo)
        {
            _elo = elo;
        }

        public int GetElo()
        {
            return _elo;
        }
        
        public void ShowStack()
        {
            Console.WriteLine("\n Stack:");
            Database.ShowCards(_name, false);
        }
        public void ShowDeck()
        {
            Console.WriteLine("\n Deck:");
            Database.ShowCards(_name, true);
        }
        public void BuildDeck()
        {
            if(Database.CountCardsInDeck(_name) >= 4)
            {
                Console.WriteLine("\n Building a new Deck will delete your existing Deck!");
                Console.WriteLine(" Press \"Y\" to build a new Deck!");
                Console.WriteLine(" Press any other key to keep your Deck!");
                char y = Console.ReadKey().KeyChar;
                if (y == 'Y')
                {
                    //delete old deck and move cards back to stack
                    Database.DeleteDeck(_name);
                }
            }
            while (Database.CountCardsInDeck(_name) < 4)
            {
                ShowStack();
                ShowDeck();
                Console.WriteLine("\n Each card can only once be chosen");
                Console.WriteLine(" Type the name of the card you want to add!");
                string input = Console.ReadLine();
                Database.MoveCardToDeckOrStack(input, true, _name);
            }
            ShowDeck();
        }

        public void BuyPackages()
        {
            Console.WriteLine("\n A package consits of 5 cards and costs 5 coins.");
            int coins = Database.GetNumberOfCoins(_name);
            if(coins < 0)
            {
                Console.WriteLine(" Error while counting money");
            }
            else
            {
                Console.WriteLine($" You have {coins} coins.");
                Console.WriteLine(" How many Packages do you want to buy?");
                int amount = Convert.ToInt32(Console.ReadLine());
                Database.BuyPackages(_name, amount, coins);
            }
            
        }


        public void OpenPackage()
        {
            List<string> cards = Database.GetAllCardNames();
            int RandomIndex;
            string CurrentCard;
            if (Database.UserHasPackages(_name))
            {
                Console.WriteLine("\n You opened a package, it contained the following Cards: \n");
                for (int i = 0; i < 5; i++)
                {
                    RandomIndex = random.Next(0, cards.Count);
                    CurrentCard = cards[RandomIndex];
                    Database.AddCardToStack(_name, CurrentCard);
                }
                Database.DecrementNumberOfPackages(_name);
            }
            else
            {
                Console.WriteLine(" You don't have any Packages, buy some first!");
            }
        }

        public void ShowProfile()
        {
            //name password elo packages coins
            Console.WriteLine("\n ________________");
            Console.WriteLine($" || {_name}'s Profile");
            Console.WriteLine($" || ELO: {GetElo()}");
            Console.WriteLine($" || coins: {Database.GetNumberOfCoins(_name)}");
            Console.WriteLine($" || unopened packages: {Database.GetNumberOfPackages(_name)}");
            Console.WriteLine("\n To Change your USERNAME press '1'!");
            Console.WriteLine(" To Change your PASSWORD press '2'!");
            Console.WriteLine(" To go back to the main menue press any other key!");
            char input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1':
                    string NewUsermame = Database.UpdateCredential(_name, "un");
                    _name = NewUsermame;
                    break;
                case '2':
                    string NewPassword = Database.UpdateCredential(_name, "pw");
                    break;
                default:
                    //nothing to do
                    break;
            }
        }
    }
}
