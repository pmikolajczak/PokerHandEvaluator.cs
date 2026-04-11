using System.Globalization;
using AwesomeAssertions;

namespace PokerHandEvaluator.Tests;

public class UpstreamCorpusTests
{
    [Fact]
    public void upstream_id_corpus_for_five_cards_matches()
    {
        ValidateIdCorpusForFiveCards(
            Path.Combine(AppContext.BaseDirectory, "TestData", "five", "id_input_tests.csv"));
    }

    [Fact]
    public void upstream_id_corpus_for_six_cards_matches()
    {
        ValidateIdCorpusForSixCards(
            Path.Combine(AppContext.BaseDirectory, "TestData", "six", "id_input_tests.csv"));
    }

    [Fact]
    public void upstream_id_corpus_for_seven_cards_matches()
    {
        ValidateIdCorpusForSevenCards(
            Path.Combine(AppContext.BaseDirectory, "TestData", "seven", "id_input_tests.csv"));
    }

    [Fact]
    public void upstream_string_corpus_for_five_cards_matches()
    {
        ValidateStringCorpus(
            Path.Combine(AppContext.BaseDirectory, "TestData", "five", "string_input_tests.csv"),
            5,
            cards => Eval.Eval5Cards(cards));
    }

    [Fact]
    public void upstream_string_corpus_for_six_cards_matches()
    {
        ValidateStringCorpus(
            Path.Combine(AppContext.BaseDirectory, "TestData", "six", "string_input_tests.csv"),
            6,
            cards => Eval.Eval6Cards(cards));
    }

    [Fact]
    public void upstream_string_corpus_for_seven_cards_matches()
    {
        ValidateStringCorpus(
            Path.Combine(AppContext.BaseDirectory, "TestData", "seven", "string_input_tests.csv"),
            7,
            cards => Eval.Eval7Cards(cards));
    }

    private static void ValidateIdCorpusForFiveCards(string path)
    {
        using var reader = File.OpenText(path);
        _ = reader.ReadLine();
        string? line;
        var lineNumber = 1;

        while ((line = reader.ReadLine()) is not null)
        {
            lineNumber++;
            var span = line.AsSpan();
            var a = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var b = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var c = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var d = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var e = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var expectedRank = int.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var actualRank = Eval.Eval5(a, b, c, d, e);

            actualRank.Should().Be(expectedRank, $"Mismatch at {path}:{lineNumber} for row '{line}'.");
        }
    }

    private static void ValidateIdCorpusForSixCards(string path)
    {
        using var reader = File.OpenText(path);
        _ = reader.ReadLine();
        string? line;
        var lineNumber = 1;

        while ((line = reader.ReadLine()) is not null)
        {
            lineNumber++;
            var span = line.AsSpan();
            var a = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var b = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var c = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var d = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var e = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var f = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var expectedRank = int.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var actualRank = Eval.Eval6(a, b, c, d, e, f);

            actualRank.Should().Be(expectedRank, $"Mismatch at {path}:{lineNumber} for row '{line}'.");
        }
    }

    private static void ValidateIdCorpusForSevenCards(string path)
    {
        using var reader = File.OpenText(path);
        _ = reader.ReadLine();
        string? line;
        var lineNumber = 1;

        while ((line = reader.ReadLine()) is not null)
        {
            lineNumber++;
            var span = line.AsSpan();
            var a = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var b = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var c = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var d = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var e = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var f = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var g = byte.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var expectedRank = int.Parse(NextToken(ref span), CultureInfo.InvariantCulture);
            var actualRank = Eval.Eval7(a, b, c, d, e, f, g);

            actualRank.Should().Be(expectedRank, $"Mismatch at {path}:{lineNumber} for row '{line}'.");
        }
    }

    private static void ValidateStringCorpus(string path, int cardCount, Func<Card[], int> evaluator)
    {
        using var reader = File.OpenText(path);

        _ = reader.ReadLine();

        string? line;
        var lineNumber = 1;
        var handBuffer = new char[14];

        while ((line = reader.ReadLine()) is not null)
        {
            lineNumber++;

            var remaining = line.AsSpan();

            for (var i = 0; i < cardCount; i++)
            {
                var token = NextToken(ref remaining);
                handBuffer[i * 2] = token[0];
                handBuffer[(i * 2) + 1] = char.ToLowerInvariant(token[1]);
            }

            var expectedRank = int.Parse(NextToken(ref remaining), CultureInfo.InvariantCulture);
            var actualRank = evaluator(Card.Cards(new string(handBuffer, 0, cardCount * 2)));

            actualRank.Should().Be(expectedRank, $"Mismatch at {path}:{lineNumber} for row '{line}'.");
        }
    }

    private static ReadOnlySpan<char> NextToken(ref ReadOnlySpan<char> remaining)
    {
        var commaIndex = remaining.IndexOf(',');

        if (commaIndex < 0)
        {
            var token = remaining;
            remaining = ReadOnlySpan<char>.Empty;
            return token;
        }

        var result = remaining[..commaIndex];
        remaining = remaining[(commaIndex + 1)..];
        return result;
    }
}
