using BenchmarkDotNet.Attributes;
using pheval;

public class RandomEvalFiveCardsBenchmark
{
    private readonly string hand;

    public RandomEvalFiveCardsBenchmark()
    {
        var deck = new Deck();
        hand = string.Join("", deck.Cards.Take(5));
    }

    [Benchmark]
    public int GetHandStrength() => Eval.Eval5String(hand);
}