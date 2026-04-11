using AwesomeAssertions;

namespace PokerHandEvaluator.Tests;

public class SpanApiTests
{
    [Fact]
    public void eval5_span_matches_fixed_arity_api()
    {
        ReadOnlySpan<byte> ids = [48, 44, 40, 36, 32];

        Eval.Eval5(ids).Should().Be(Eval.Eval5(48, 44, 40, 36, 32));
    }

    [Fact]
    public void eval6_span_matches_fixed_arity_api()
    {
        ReadOnlySpan<byte> ids = [48, 44, 40, 36, 32, 1];

        Eval.Eval6(ids).Should().Be(Eval.Eval6(48, 44, 40, 36, 32, 1));
    }

    [Fact]
    public void eval7_span_matches_fixed_arity_api()
    {
        ReadOnlySpan<byte> ids = [48, 44, 40, 36, 32, 1, 2];

        Eval.Eval7(ids).Should().Be(Eval.Eval7(48, 44, 40, 36, 32, 1, 2));
    }

    [Fact]
    public void eval5_span_rejects_wrong_card_count()
    {
        var ids = new byte[] { 48, 44, 40, 36 };
        var act = () => Eval.Eval5(ids.AsSpan());

        act.Should().Throw<ArgumentException>().WithMessage("*exactly 5*");
    }

    [Fact]
    public void eval6_span_rejects_duplicate_ids()
    {
        var ids = new byte[] { 48, 44, 40, 36, 32, 32 };
        var act = () => Eval.Eval6(ids.AsSpan());

        act.Should().Throw<ArgumentException>().WithMessage("*distinct*");
    }

    [Fact]
    public void eval7_span_rejects_out_of_range_ids()
    {
        var ids = new byte[] { 48, 44, 40, 36, 32, 1, 255 };
        var act = () => Eval.Eval7(ids.AsSpan());

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
