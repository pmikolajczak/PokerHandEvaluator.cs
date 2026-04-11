# PokerHandEvaluator

A .NET 10 poker hand evaluator based on [HenryRLee/PokerHandEvaluator](https://github.com/HenryRLee/PokerHandEvaluator), updated for a modern C# codebase with strong correctness coverage and zero-allocation hot paths.

## API Direction

The intended evaluator surface is:

- fixed-arity id APIs:
  - `Eval.Eval5(byte, byte, byte, byte, byte)`
  - `Eval.Eval6(byte, byte, byte, byte, byte, byte)`
  - `Eval.Eval7(byte, byte, byte, byte, byte, byte, byte)`
- span-based id APIs:
  - `Eval.Eval5(ReadOnlySpan<byte>)`
  - `Eval.Eval6(ReadOnlySpan<byte>)`
  - `Eval.Eval7(ReadOnlySpan<byte>)`

Card ids are in the range `0..51`, where:

- the low 2 bits are suit
- the remaining bits are rank

`Card` is available as a lightweight value type and parser helper, but the evaluator is designed around card ids rather than string parsing.

## Performance

Current hot-path benchmark results on this machine:

```text
Eval5_FixedIds   8.819 ns   0 B
Eval5_IdSpan    12.736 ns   0 B
Eval6_FixedIds  10.701 ns   0 B
Eval7_FixedIds  11.737 ns   0 B
```

Convenience wrappers still cost more:

```text
Eval5_Cards    18.274 ns   32 B
```

## Testing

Install the .NET 10 SDK and run:

```console
dotnet test
```

The test suite includes:

- upstream CSV corpus validation for 5, 6, and 7-card evaluation
- exhaustive 5-card category distribution checks
- order-invariance and rank-boundary tests
- invalid-input contract tests
- allocation baseline tests

## Benchmarks

Run:

```console
dotnet run -c Release --project benchmarks/PokerHandEvaluator.Benchmarks.csproj -- --filter "*EvaluatorBenchmarks*"
```
