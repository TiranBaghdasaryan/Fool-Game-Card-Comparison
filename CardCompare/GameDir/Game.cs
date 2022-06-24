using System;
using System.Collections.Generic;
using System.Linq;
using CardCompare.Enums;
using Random = CardCompare.Common.Random;

namespace CardCompare.GameDir
{
    public class Game
    {
        public List<Card> Deck { get; private set; }
        public CardSuit TrumpSuit { get; private set; }

        private Player[] _players;

        private Player _attacker;
        private Player _cutter;

        public Game(int cardsCount, int playersCount)
        {
            CreateNewGame(cardsCount, playersCount);
        }

        private void CreateNewGame(int cardsCount, int playersCount)
        {
            CheckParameters(cardsCount, playersCount);
            SetGameTrump();
            DistributeCards(playersCount,cardsCount);
            Deck.Sort();
            ConsoleDeck();
            ConsolePlayersCards(playersCount);
            SetInitialAttackerAndCutter(cardsCount, playersCount);

            Console.WriteLine();
        }

        private void SetInitialAttackerAndCutter(int cardsCount, int playersCount)
        {
            Card[] eachPlayerMinCards = new Card[_players.Length];
            for (int i = 0; i < _players.Length; i++)
            {
                IEnumerable<Card> trumps = _players[i].Cards.Where(c => c.IsTrump);


                if (trumps.Any(c => c.IsTrump))
                {
                    eachPlayerMinCards[i] = _players[i].Cards.Where(c => c.IsTrump).Min();
                    continue;
                }

                eachPlayerMinCards[i] = new Card(CardSuit.Spade, CardValue.Joker, true);
            }

            if (eachPlayerMinCards.All(c => Equals(c.CardOwner, null)))
            {
                CreateNewGame(cardsCount, playersCount);
                return;
            }

            _attacker = eachPlayerMinCards.Min().CardOwner;


            for (int i = 0; i < _players.Length; i++)
                if (_players[i] == _attacker)
                {
                    _cutter = _players[_players.Length - 1 - i];
                    break;
                }
        }


        private static void CheckParameters(int cardsCount, int playersCount)
        {
            if (!(cardsCount == 36 || cardsCount == 24))
                throw new Exception($"Cards Count can't be {cardsCount}\nCards Count must be 36 or 24");
            if (playersCount < 2 || playersCount > 6)
                throw new Exception($"Players Count can't be more than 6 and less than 2");
        }

        private void ConsoleDeck()
        {
            foreach (var card in Deck) Console.WriteLine($"{card.Suit} - {card.Value} - {card.IsTrump}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void ConsolePlayersCards(int playersCount)
        {
            for (int i = 0; i < playersCount; i++)
            {
                List<Card> cards = _players[i].Cards;
                for (int j = 0; j < cards.Count; j++)
                    Console.WriteLine($"{cards[j].Suit} - {cards[j].Value} - {cards[j].IsTrump}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void DistributeCards(int playersCount, int cardsCount)
        {
            CreateGameDeck(cardsCount);
            SetPlayersCount(playersCount);
            for (int i = 0; i < _players.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int randomNumber = Random.Range(0, Deck.Count - 1);
                    Card randomCard = Deck[randomNumber];
                    randomCard.CardOwner = _players[i];
                    _players[i].Cards.Add(randomCard);
                    Deck.Remove(Deck[randomNumber]);
                }

                _players[i].Cards.Sort();
            }

            foreach (var player in _players)
                if (player.Cards.Any(c => c.IsTrump))
                    return;

            DistributeCards(playersCount,cardsCount);
        }

        private void SetGameTrump() => TrumpSuit = (CardSuit)Random.Range(0, 3);

        private void SetPlayersCount(int playersCount)
        {
            _players = new Player[playersCount];
            for (int i = 0; i < playersCount; i++)
                _players[i] = new Player();
        }

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