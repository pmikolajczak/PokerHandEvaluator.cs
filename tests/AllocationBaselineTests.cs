using AwesomeAssertions;

namespace PokerHandEvaluator.Tests;

public class AllocationBaselineTests
{
    [Fact]
    public void eval5_fixed_ids_does_not_allocate()
    {
        var allocatedBytes = MeasureAllocations(() => Eval.Eval5(48, 44, 40, 36, 32));

        allocatedBytes.Should().Be(0);
    }

    [Fact]
    public void eval6_fixed_ids_does_not_allocate()
    {
        var allocatedBytes = MeasureAllocations(() => Eval.Eval6(48, 44, 40, 36, 32, 1));

        allocatedBytes.Should().Be(0);
    }

    [Fact]
    public void eval7_fixed_ids_does_not_allocate()
    {
        var allocatedBytes = MeasureAllocations(() => Eval.Eval7(48, 44, 40, 36, 32, 1, 2));

        allocatedBytes.Should().Be(0);
    }

    [Fact]
    public void eval5_span_does_not_allocate()
    {
        var ids = new byte[] { 48, 44, 40, 36, 32 };
        var allocatedBytes = MeasureAllocations(() => Eval.Eval5(ids.AsSpan()));

        allocatedBytes.Should().Be(0);
    }

    [Fact]
    public void parsing_and_card_wrapper_allocate_more_than_fixed_ids()
    {
        var fixedIdAllocations = MeasureAllocations(() => Eval.Eval5(48, 44, 40, 36, 32));
        var parsedAllocations = MeasureAllocations(() => Eval.Eval5Cards(Card.Cards("AcKcQcJcTc")));

        parsedAllocations.Should().BeGreaterThan(fixedIdAllocations);
    }

    private static long MeasureAllocations(Action action)
    {
        const int iterations = 10_000;

        action();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var before = GC.GetAllocatedBytesForCurrentThread();

        for (var i = 0; i < iterations; i++)
        {
            action();
        }

        var after = GC.GetAllocatedBytesForCurrentThread();
        return after - before;
    }
}
