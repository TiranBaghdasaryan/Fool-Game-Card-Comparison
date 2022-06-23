using System.Collections.Generic;

namespace CardCompare.GameDir
{
    public class Player
    {
        public List<Card> Cards;


        public Player()
        {
            Cards = new List<Card>(6);
        }
    }
}