using System.Collections.Generic;
using System.Linq;
using System;

namespace Domain 
{
    public class MemoryGame 
    {
        List<Card> cards;
        Card firstGuess;
        Card secondGuess;
        public event EventHandler OnWin;

        public MemoryGame(List<Card> cards)
        {
            this.cards = cards;
        }

        public GameState State { get; private set; }

        public void Guess(Card card) 
        {
            if(State == GameState.GuessFirst && card.State == CardState.Hidden) 
            {
                card.State = CardState.Open;
                firstGuess = card;
                State = GameState.GuessSecond;
                return;
            }

            if(State == GameState.GuessSecond && card.State == CardState.Hidden) 
            {
                card.State = CardState.Open;
                secondGuess = card;
                State = GameState.Continue;
                return;
            }

            if(State == GameState.Continue) 
            {
                if(firstGuess.Url == secondGuess.Url)
                {
                    firstGuess.State = CardState.Found;
                    secondGuess.State = CardState.Found;
                    
                } 
                else 
                {
                    firstGuess.State = CardState.Hidden;
                    secondGuess.State = CardState.Hidden;
                }
                State = GameState.GuessFirst;
                return;
            }

            if (cards.All(c => c.IsFound))
            {
                State = GameState.Win;
                OnWin(this, new EventArgs());
            }
        }
    }
    
    public enum GameState {
        GuessFirst,
        GuessSecond,
        Continue,
        Win,
        Lose
    }

    public class Card 
    {
        public string Url { get; set; }
        public CardState State{ get;set; } = CardState.Hidden;

        public Card(string url)
        {
            Url = url;
        }

        public void Clicked()  
        {
            if(State == CardState.Hidden)
                State = CardState.Open;
        }

        public bool IsFlipped => State == CardState.Open || State == CardState.Found;
        public bool IsFound => State == CardState.Found;
    }

    public enum CardState {
        Hidden,
        Open,
        Found
    }
}