using AwesomeAssertions;

namespace PokerHandEvaluator.Tests;

public class InputValidationTests
{
    [Fact]
    public void eval5_rejects_duplicate_ids()
    {
        var act = () => Eval.Eval5(0, 0, 1, 2, 3);

        act.Should().Throw<ArgumentException>().WithMessage("*distinct*");
    }

    [Fact]
    public void eval6_rejects_out_of_range_ids()
    {
        var act = () => Eval.Eval6(0, 1, 2, 3, 4, 255);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void eval7_ids_requires_exact_card_count()
    {
        var act = () => Eval.Eval7Ids(0, 1, 2, 3, 4, 5);

        act.Should().Throw<ArgumentException>().WithMessage("*exactly 7*");
    }

    [Fact]
    public void eval5_cards_rejects_duplicate_entries()
    {
        var act = () => Eval.Eval5Cards(new Card(0), new Card(0), new Card(2), new Card(3), new Card(4));

        act.Should().Throw<ArgumentException>().WithMessage("*distinct*");
    }

    [Fact]
    public void parsed_eval6_requires_exact_card_count()
    {
        var act = () => Eval.Eval6Cards(Card.Cards("AcKdQsJhTc"));

        act.Should().Throw<ArgumentException>().WithMessage("*exactly 6*");
    }

    [Fact]
    public void parsed_eval7_rejects_duplicate_cards()
    {
        var act = () => Eval.Eval7Cards(Card.Cards("AcAcQsJhTc9d8h"));

        act.Should().Throw<ArgumentException>().WithMessage("*distinct*");
    }

    [Fact]
    public void card_rejects_invalid_rank()
    {
        var act = () => new Card("1c");

        act.Should().Throw<ArgumentException>().WithMessage("*Invalid card rank*");
    }

    [Fact]
    public void card_rejects_invalid_suit()
    {
        var act = () => new Card("Ax");

        act.Should().Throw<ArgumentException>().WithMessage("*Invalid card suit*");
    }

    [Fact]
    public void cards_reject_odd_length_hand_text()
    {
        var act = () => Card.Cards("AcK");

        act.Should().Throw<ArgumentException>().WithMessage("*even number of characters*");
    }

    [Fact]
    public void cards_reject_null_hand_text()
    {
        var act = () => Card.Cards((string)null!);

        act.Should().Throw<ArgumentNullException>();
    }
}
