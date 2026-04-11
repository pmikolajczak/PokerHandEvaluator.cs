using System.Collections.Generic;
using AwesomeAssertions;

namespace PokerHandEvaluator.Tests;

public class EvaluatorCharacterizationTests
{
    [Fact]
    public void eval5_exhaustive_category_distribution_matches_canonical_counts()
    {
        var categoryCounts = new int[10];
        var uniqueRanks = new HashSet<int>();
        var totalHands = 0;

        for (byte a = 0; a < 48; a++)
        {
            for (byte b = (byte)(a + 1); b < 49; b++)
            {
                for (byte c = (byte)(b + 1); c < 50; c++)
                {
                    for (byte d = (byte)(c + 1); d < 51; d++)
                    {
                        for (byte e = (byte)(d + 1); e < 52; e++)
                        {
                            var rank = Eval.Eval5(a, b, c, d, e);

                            rank.Should().BeInRange(1, 7462);

                            categoryCounts[(int)Rank.GetCategory(rank)]++;
                            uniqueRanks.Add(rank);
                            totalHands++;
                        }
                    }
                }
            }
        }

        totalHands.Should().Be(2_598_960);
        uniqueRanks.Count.Should().Be(7_462);

        categoryCounts[(int)Rank.Category.StraightFlush].Should().Be(40);
        categoryCounts[(int)Rank.Category.FourOfAKind].Should().Be(624);
        categoryCounts[(int)Rank.Category.FullHouse].Should().Be(3_744);
        categoryCounts[(int)Rank.Category.Flush].Should().Be(5_108);
        categoryCounts[(int)Rank.Category.Straight].Should().Be(10_200);
        categoryCounts[(int)Rank.Category.ThreeOfAKind].Should().Be(54_912);
        categoryCounts[(int)Rank.Category.TwoPair].Should().Be(123_552);
        categoryCounts[(int)Rank.Category.OnePair].Should().Be(1_098_240);
        categoryCounts[(int)Rank.Category.HighCard].Should().Be(1_302_540);
    }

    [Theory]
    [InlineData("AcKcQcJcTc")]
    [InlineData("AsAdAc2sAh")]
    [InlineData("Ac2c7cJcTc")]
    [InlineData("AcKdQsJhTc")]
    public void eval5_is_order_invariant(string hand)
    {
        var cards = Card.Cards(hand);
        var expected = Eval.Eval5Cards(cards);

        Eval.Eval5(cards[4].id, cards[3].id, cards[2].id, cards[1].id, cards[0].id).Should().Be(expected);
        Eval.Eval5(cards[1].id, cards[3].id, cards[0].id, cards[4].id, cards[2].id).Should().Be(expected);
    }

    [Theory]
    [InlineData("AcKcQcJcTc9d")]
    [InlineData("AsAdAc2sAhKd")]
    [InlineData("Ac2c7cJcTc3d")]
    public void eval6_is_order_invariant(string hand)
    {
        var cards = Card.Cards(hand);
        var expected = Eval.Eval6Cards(cards);

        Eval.Eval6(cards[5].id, cards[4].id, cards[3].id, cards[2].id, cards[1].id, cards[0].id).Should().Be(expected);
        Eval.Eval6(cards[1].id, cards[3].id, cards[5].id, cards[0].id, cards[2].id, cards[4].id).Should().Be(expected);
    }

    [Theory]
    [InlineData("AcKcQcJcTc9d8h")]
    [InlineData("AsAdAc2sAhKdQd")]
    [InlineData("Ac2c7cJcTc3d4h")]
    public void eval7_is_order_invariant(string hand)
    {
        var cards = Card.Cards(hand);
        var expected = Eval.Eval7Cards(cards);

        Eval.Eval7(cards[6].id, cards[5].id, cards[4].id, cards[3].id, cards[2].id, cards[1].id, cards[0].id).Should().Be(expected);
        Eval.Eval7(cards[1].id, cards[3].id, cards[5].id, cards[0].id, cards[2].id, cards[4].id, cards[6].id).Should().Be(expected);
    }

    [Theory]
    [InlineData(1, Rank.Category.StraightFlush)]
    [InlineData(10, Rank.Category.StraightFlush)]
    [InlineData(11, Rank.Category.FourOfAKind)]
    [InlineData(166, Rank.Category.FourOfAKind)]
    [InlineData(167, Rank.Category.FullHouse)]
    [InlineData(322, Rank.Category.FullHouse)]
    [InlineData(323, Rank.Category.Flush)]
    [InlineData(1599, Rank.Category.Flush)]
    [InlineData(1600, Rank.Category.Straight)]
    [InlineData(1609, Rank.Category.Straight)]
    [InlineData(1610, Rank.Category.ThreeOfAKind)]
    [InlineData(2467, Rank.Category.ThreeOfAKind)]
    [InlineData(2468, Rank.Category.TwoPair)]
    [InlineData(3325, Rank.Category.TwoPair)]
    [InlineData(3326, Rank.Category.OnePair)]
    [InlineData(6185, Rank.Category.OnePair)]
    [InlineData(6186, Rank.Category.HighCard)]
    [InlineData(7462, Rank.Category.HighCard)]
    public void rank_category_boundaries_are_stable(int rank, Rank.Category expectedCategory)
    {
        Rank.GetCategory(rank).Should().Be(expectedCategory);
    }
}
