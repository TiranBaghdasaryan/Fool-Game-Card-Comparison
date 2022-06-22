using System;
using System.Collections.Generic;
using CardCompare.Enums;
using Random = CardCompare.Common.Random;

namespace CardCompare.GameDir
{
    public class Game
    {
        public List<Card> Deck { get; private set; }
        public CardSuit TrumpSuit { get; private set; }

        public Game(int cardsCount)
        {
            if (!(cardsCount == 36 || cardsCount == 24))
                throw new Exception($"Cards Count can't be {cardsCount}\nCards Count must be 36 or 24");

            SetGameTrump();
            CreateGameDeck(cardsCount);

            Deck.Sort();

            foreach (var card in Deck)  Console.WriteLine($"{card.Suit} - {card.Value} - {card.IsTrump}");
               
        }

        private void SetGameTrump() => TrumpSuit = (CardSuit)Random.Range(0, 3);
        private List<Card> CreateGameDeck(int cardsCount)
        {
            Deck = new List<Card>(cardsCount);
            List<CardValue> cardValues = new List<CardValue>()
            {
                CardValue.T,
                CardValue.K,
                CardValue.Q,
                CardValue.J,
                CardValue.Ten,
                CardValue.Nine,
                CardValue.Eight,
                CardValue.Seven,
                CardValue.Six,
            };

            for (int i = 0; i < cardsCount / 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    CardSuit cardSuit = (CardSuit)j;
                    bool isTrump = cardSuit == TrumpSuit;
                    CardValue cardValue = cardValues[i];

                    Deck.Add(new Card(cardSuit, cardValue, isTrump));
                }
            }

            return Deck;
        }

        public Card GetStrongestCard(Card card1, Card card2) => card1 > card2 ? card1 : card2;
    }
}