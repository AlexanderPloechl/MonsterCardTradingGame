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
        }

        private Stack _stack { get; set; }
        private Deck _deck { get; set; }
        private string _name { get; set; }
        private string _password { get; set; }
        private int _coins { get; set; }
        private List<Package> _unopendPackages;
        void BuyPackage()
        {
            _unopendPackages.Add(new Package());
        }
        void OpenPackage()
        {
            Package package = _unopendPackages.ElementAt(0);
            foreach (ICard card in package.CardsInPackage)
            {
                _stack.cards.Add(card);
                package.CardsInPackage.Remove(card);
            }
            _unopendPackages.Remove(package);
        }
    }
}
