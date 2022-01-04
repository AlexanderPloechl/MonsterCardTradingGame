using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class User
    {
        public User(string name, string password)
        {
            _name = name;
            _password = password;
            _coins = 20;
            _unopendPackages = new List<Package>();
            _stack = new Stack();
            _deck = new Deck();
        }
        private Random random = new Random();
        private Stack _stack { get; set; }
        private Deck _deck { get; set; }
        private string _name { get; set; }
        private string _password { get; set; }
        private int _coins { get; set; }
        private List<Package> _unopendPackages;
        public void BuyPackage()
        {
            Console.WriteLine("Buying a Package...\n");
            _coins -= Package.price;
            _unopendPackages.Add(new Package());
        }
        public void OpenPackage()
        {
            Console.WriteLine("Opening a Package...\n");
            Package package = _unopendPackages.ElementAt(0);
            foreach (ICard card in package.CardsInPackage)
            {
                _stack.cards.Add(card);
            }
            _unopendPackages.Remove(package);
        }
        public void PrintUserData()
        {
            Console.WriteLine($"Username: {_name}\nPassword: {_password}\nCoins: {_coins}\nUnopend packages: {_unopendPackages.Count}\nCards in Stack: {_stack.cards.Count}\nCards in Deck: {_deck.cards.Count}\n");
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
        public void BuildDeck()
        {
            for(int i = 0; i < _deck.NumberOfCardsInDeck; i++)
            {
                Console.WriteLine("Enter the number of a card to add it to your deck!\nStack:");
                PrintContents(_stack.cards);
                string input = Console.ReadLine();
                int ChosenCard = Convert.ToInt32(input);
                _deck.cards.Add(_stack.cards[ChosenCard - 1]);
                _stack.cards.RemoveAt(ChosenCard - 1);
                Console.WriteLine("Deck:");
                PrintContents(_deck.cards);
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
    }
}
