namespace Domain 
{
    public class Card
    {
        public string Url { get; set; }
        public CardState State { get; set; } = CardState.Hidden;
        public Card() {}
        public Card(string url)
        {
            Url = url;
        }

        public bool IsRevealed => State == CardState.Open || State == CardState.Found || State == CardState.Wrong;
        public bool IsFound => State == CardState.Found;
        public Card Copy() => new Card(Url);
        public bool Matches(Card card) => this.Url == card.Url;
    }

    public enum CardState
    {
        Hidden,
        Open,
        Wrong,
        Found
    }
}