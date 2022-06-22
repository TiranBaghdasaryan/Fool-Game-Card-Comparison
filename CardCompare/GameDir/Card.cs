using System;
using CardCompare.Enums;

namespace CardCompare.GameDir
{
    public struct Card : IComparable<Card>
    {
        public CardSuit Suit { get; }
        public CardValue Value { get; }
        public bool IsTrump { get; }


        public Card(CardSuit suit, CardValue value, bool isTrump)
        {
            Suit = suit;
            Value = value;
            IsTrump = isTrump;
        }

        public int CompareTo(Card other)
        {
            var trumpComparison = IsTrump.CompareTo(other.IsTrump);
            var valueComparison = Value.CompareTo(other.Value);

            if (
                (valueComparison == 1 && trumpComparison == 1) ||
                (valueComparison == -1 && trumpComparison == 1) ||
                (valueComparison == 0 && trumpComparison == 1) ||
                (valueComparison == 1 && trumpComparison == 0)
            ) return 1;

            if (
                (valueComparison == 1 && trumpComparison == -1) ||
                (valueComparison == -1 && trumpComparison == -1) ||
                (valueComparison == 0 && trumpComparison == -1) ||
                (valueComparison == -1 && trumpComparison == 0)
            ) return -1;


            return 0;
        }


        public static bool operator >(Card card1, Card card2) => card1.CompareTo(card2) > 0;
        public static bool operator <(Card card1, Card card2) => card1.CompareTo(card2) < 0;
    }
}