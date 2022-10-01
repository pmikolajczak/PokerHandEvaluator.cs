using FluentAssertions;

public class DeckTests
{
    [Fact]
    public void deck_has_52_cards() {
        var deck = new Deck();

        deck.Cards.Count.Should().Be(52);
    }
}