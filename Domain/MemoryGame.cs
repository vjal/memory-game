using System;
using System.Collections.Generic;
using System.Linq;

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
            Shuffle(deck);
            for (var i = 0; i < 8; i++)
            {
                var card = deck[i];
                card.State = CardState.Hidden;
                Cards.Add(card);
                Cards.Add(card.Copy());
                deck.Remove(card);
            }

            State = GameState.GuessFirst;
        }

        void Shuffle<T>(IList<T> list)
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
}