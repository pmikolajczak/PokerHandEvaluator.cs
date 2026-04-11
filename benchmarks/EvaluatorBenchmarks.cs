using BenchmarkDotNet.Attributes;
using PokerHandEvaluator;

[MemoryDiagnoser]
public class EvaluatorBenchmarks
{
    private readonly byte[] fiveIds = [48, 44, 40, 36, 32];
    private readonly byte[] sixIds = [48, 44, 40, 36, 32, 1];
    private readonly byte[] sevenIds = [48, 44, 40, 36, 32, 1, 2];

    private readonly Card[] fiveCards;

    public EvaluatorBenchmarks()
    {
        fiveCards = Card.Cards(fiveIds);
    }

    [Benchmark(Baseline = true)]
    public int Eval5_FixedIds() => Eval.Eval5(fiveIds[0], fiveIds[1], fiveIds[2], fiveIds[3], fiveIds[4]);

    [Benchmark]
    public int Eval5_IdSpan() => Eval.Eval5(fiveIds.AsSpan());

    [Benchmark]
    public int Eval5_ParamsIds() => Eval.Eval5Ids(fiveIds);

    [Benchmark]
    public int Eval5_Cards() => Eval.Eval5Cards(fiveCards);

    [Benchmark]
    public int Eval6_FixedIds() => Eval.Eval6(sixIds[0], sixIds[1], sixIds[2], sixIds[3], sixIds[4], sixIds[5]);

    [Benchmark]
    public int Eval6_IdSpan() => Eval.Eval6(sixIds.AsSpan());

    [Benchmark]
    public int Eval7_FixedIds() => Eval.Eval7(sevenIds[0], sevenIds[1], sevenIds[2], sevenIds[3], sevenIds[4], sevenIds[5], sevenIds[6]);

    [Benchmark]
    public int Eval7_IdSpan() => Eval.Eval7(sevenIds.AsSpan());
}
