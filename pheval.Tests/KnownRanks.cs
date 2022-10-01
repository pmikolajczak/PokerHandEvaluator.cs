using FluentAssertions;

namespace pheval.Tests
{
  public class KnownRanks
  {
    [Fact]
    public void Value() {
      var a = Eval.Eval5String("9c4c4s9d4h");
      var b = Eval.Eval5String("9c4c4s9d9h");

      a.Should().Be(292);
      b.Should().Be(236);
    }

    [Fact]
    public void Comparison() {
      var a = Eval.Eval5String("9c4c4s9d4h");
      var b = Eval.Eval5String("9c4c4s9d9h");

      b.Should().BeLessThan(a);
      b.Should().BeLessThanOrEqualTo(a);
      a.Should().BeGreaterThan(b);
      a.Should().BeGreaterThanOrEqualTo(b);
      a.Should().NotBe(b);
      
      Assert.True(a != b);
    }

    [Theory,
    InlineData("AcKcQcJcTc", Rank.Category.StraightFlush, "Straight Flush"),
    InlineData("AcAdAsAhKs", Rank.Category.FourOfAKind, "Four of a Kind"),
    InlineData("AcAdKcKhKs", Rank.Category.FullHouse, "Full House"),
    InlineData("Ac2c7cJcTc", Rank.Category.Flush, "Flush"),
    InlineData("AcKdQsJhTc", Rank.Category.Straight, "Straight"),
    InlineData("AcAdAsJhTc", Rank.Category.ThreeOfAKind, "Three of a Kind"),
    InlineData("AcAdJsJhTc", Rank.Category.TwoPair, "Two Pair"),
    InlineData("AcAd7sJhTc", Rank.Category.OnePair, "One Pair"),
    InlineData("Ac2d7sJhTc", Rank.Category.HighCard, "High Card")]
    public void RankCategory(string hand, Rank.Category category, string categoryDescription) {
      var evaluation = Eval.Eval5String(hand);

      Rank.GetCategory(evaluation).Should().Be(category);
      Rank.DescribeRankCategory(evaluation).Should().Be(categoryDescription);
    }

    [Theory,
    InlineData("6s2s5s3s4s", "Six-High Straight Flush", "65432"),
    InlineData("AsAdAc2sAh", "Four Aces", "AAAA2"),
    InlineData("As2dAc2sAh", "Aces Full over Deuces", "AAA22"),
    InlineData("As2s3s7sTs", "Ace-High Flush", "AT732"),
    InlineData("AsKcQdJdTh", "Ace-High Straight", "AKQJT"),
    InlineData("As2sAdAcTs", "Three Aces", "AAAT2"),
    InlineData("As2sAdTcTs", "Aces and Tens", "AATT2"),
    InlineData("Qs2sQh3cTs", "Pair of Queens", "QQT32"),
    InlineData("6s7s2d3c4h", "Seven-High", "76432")]
    public void RankDescription(string hand, string rankDescription, string rankDescriptionShort) {
      var evaluation = Eval.Eval5String(hand);

      Rank.DescribeRank(evaluation).Should().Be(rankDescription);
      Rank.DescribeRankShort(evaluation).Should().Be(rankDescriptionShort);
    }
  }
}