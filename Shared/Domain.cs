using System.Collections.Generic;
using System.Linq;
using System;
namespace Domain {
    public class Card {
        public string Url { get; set; }
        public CardState State{get;set;} = CardState.Hidden;
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
    public class MemoryGame {
        public List<Card> cards;
        public MemoryGameState State;
        public Card firstGuess;
        public Card secondGuess;
        private Action onWin;
        public MemoryGame(List<Card> cards, Action onWin)
        {
            this.cards = cards;
            this.onWin = onWin;
        }
        public void Guess(Card card) {
            if(State == MemoryGameState.GuessFirst && card.State == CardState.Hidden) {
                card.State = CardState.Open;
                firstGuess = card;
                State = MemoryGameState.GuessSecond;
                return;
            }
            if(State == MemoryGameState.GuessSecond && card.State == CardState.Hidden) {
                card.State = CardState.Open;
                secondGuess = card;
                State = MemoryGameState.Continue;
                return;
            }

            if(State == MemoryGameState.Continue) {
                if(firstGuess.Url == secondGuess.Url){
                    firstGuess.State = CardState.Found;
                    secondGuess.State = CardState.Found;
                    
                } else {
                    firstGuess.State = CardState.Hidden;
                    secondGuess.State = CardState.Hidden;
                }
                State = MemoryGameState.GuessFirst;
                return;
            }

            if(cards.All(c => c.IsFound)){
                State = MemoryGameState.Win;
                onWin();
            }

        }


    }
    public enum MemoryGameState {
        GuessFirst,
        GuessSecond,
        Continue,
        Win,
        Lose
    }
}