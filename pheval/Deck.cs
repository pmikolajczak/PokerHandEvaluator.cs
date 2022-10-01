using pheval;

public class Deck
{
    static readonly Card[] AllCards = Card.rankMap.Keys
    .SelectMany(rank => Card.suitMap.Keys
        .Select(suit => new Card($"{rank}{suit}")))
    .ToArray();

    public Deck()
    {
        Cards = new Stack<Card>(AllCards.Shuffle());
    }

    public Stack<Card> Cards { get; private init; }
}