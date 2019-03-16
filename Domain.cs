using System.Collections.Generic;
using System.Linq;
using System;

namespace Domain
{
    public class MemoryGame
    {
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Card> Cards { get; set; } = new List<Card>();
        Card firstGuess;
        Card secondGuess;
        Random random = new Random();

        public MemoryGame() {}

        public MemoryGame(List<Card> deck)
        {
            Deck = deck;
            Start();
        }

        public GameState State { get; private set; }

        public void Start()
        {
            Cards.Clear();
            var deck = Deck.ToList();
            for (var i = 0; i < 8; i++)
            {
                var index = random.Next(deck.Count - 1);
                var randomCard = deck[index];
                randomCard.State = CardState.Hidden;
                Cards.Add(randomCard);
                Cards.Add(randomCard.Copy());
                deck.Remove(randomCard);
            }
            Shuffle(Cards);
            State = GameState.GuessFirst;
        }

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        public void Play(Card card)
        {
            if (State == GameState.Win)
            {
                Start();
                return;
            }

            if (State == GameState.GuessFirst && card.State == CardState.Hidden)
            {
                card.State = CardState.Open;
                firstGuess = card;
                State = GameState.GuessSecond;
                return;
            }

            if (State == GameState.GuessSecond && card.State == CardState.Hidden)
            {
                card.State = CardState.Open;
                secondGuess = card;
                if (firstGuess.Url == secondGuess.Url)
                {
                    firstGuess.State = CardState.Found;
                    secondGuess.State = CardState.Found;

                    if (HasWon())
                    {
                        State = GameState.Win;
                        return;
                    }


                    State = GameState.GuessFirst;
                    return;
                }
                firstGuess.State = CardState.Wrong;
                secondGuess.State = CardState.Wrong;
                State = GameState.Continue;
                return;
            }

            if (State == GameState.Continue)
            {

                firstGuess.State = CardState.Hidden;
                secondGuess.State = CardState.Hidden;
                State = GameState.GuessFirst;
                return;
            }
        }

        bool HasWon() => Cards.All(c => c.IsFound);
    }

    public enum GameState
    {
        GuessFirst,
        GuessSecond,
        Continue,
        Win,
        Lose
    }

    public class Card
    {
        public string Url { get; set; }
        public CardState State { get; set; } = CardState.Hidden;
        public Card(string url)
        {
            Url = url;
        }

        public bool IsRevealed => State == CardState.Open || State == CardState.Found || State == CardState.Wrong;
        public bool IsFound => State == CardState.Found;
        public Card Copy() => new Card(Url);
    }



    public enum CardState
    {
        Hidden,
        Open,
        Wrong,
        Found
    }
}