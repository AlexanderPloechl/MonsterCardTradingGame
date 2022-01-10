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
            _unopendPackages = new List<Package>();
            //_stack = new Stack();
            _deck = new Deck();
        }
        private Random random = new Random();
        private Stack _stack { get; set; }
        private Deck _deck { get; set; }
        private string _name { get; set; }
        private int _elo { get; set; }
        private int _coins { get; set; }
        private List<Package> _unopendPackages;

        //public void OpenPackage()
        //{
        //    Console.WriteLine("Opening a Package...\n");
        //    Package package = _unopendPackages.ElementAt(0);
        //    foreach (ICard card in package.CardsInPackage)
        //    {
        //        _stack.cards.Add(card);
        //    }
        //    _unopendPackages.Remove(package);
        //}
        public void PrintUserData()
        {
            Console.WriteLine($"Username: {_name}\nCoins: {_coins}\nUnopend packages: {_unopendPackages.Count}\nCards in Stack: {_stack.cards.Count}\nCards in Deck: {_deck.cards.Count}\n");
        }
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
            Database.ShowCards(_name, false);
        }
        public void ShowDeck()
        {
            Database.ShowCards(_name, true);
        }
        public void BuildDeck()
        {
            if(Database.CountCardsInDeck(_name) >= 4)
            {
                Console.WriteLine(" Building a new Deck will delete your existing Deck!");
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
                Console.WriteLine("Type the name of the card you want to add!");
                string input = Console.ReadLine();
                Database.MoveCardToDeckOrStack(input, true, _name);
            }
        }

        public void BuyPackages()
        {
            Console.WriteLine(" How many Packages do you want to buy?");
            int amount = Convert.ToInt32(Console.ReadLine());
            Database.BuyPackages(_name, amount);
        }


        public void OpenPackage()
        {
            List<string> cards = Database.GetAllCardNames();
            int RandomIndex;
            string CurrentCard;
            if (Database.UserHasPackages(_name))
            {
                for (int i = 0; i < 5; i++)
                {
                    RandomIndex = random.Next(0, cards.Count);
                    CurrentCard = cards[RandomIndex];
                    Console.WriteLine(CurrentCard);
                    Database.AddCardToStack(_name, CurrentCard);
                }
                Database.DecrementNumberOfPackages(_name);
            }
            else
            {
                Console.WriteLine(" You don't have any Packages, buy some first!");
            }
        }
    }
}
