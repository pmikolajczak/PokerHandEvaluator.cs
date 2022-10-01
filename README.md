# PokerHandEvaluator.cs 

A port of [HenryRLee/PokerHandEvaluator](https://github.com/HenryRLee/PokerHandEvaluator) from c++ to c#.  
This is a net6 version.


### Usage

Have a look in the [pheval.Tests](pheval.Tests) folder.  


### Testing

In monodevelop or visual studio, select the pheval.Tests project and choose Run -> 'Run Unit Tests'.  The tests depend on the presence of libpheval.so (or .dll).  This shared object was created as follows:

```console
$ git clone https://github.com/HenryRLee/PokerHandEvaluator
$ cd PokerHandEvaluator/cpp
$ # edit CMakeLists.txt changing STATIC to SHARED on line 16
$ mkdir build && cd build
$ cmake -DBUILD_TESTS=OFF -DBUILD_EXAMPLES=OFF ..
$ make
$ # now copy libpheval.so (or .dll) into the pheval.Tests directory of this project
```

If you're on windows, you'll have to add libpheval.dll to the pheval.Tests project and select 'Copy To Output Directory' (in monodevelop right-click the file -> 'Quick Properties')  

### Benchmark
``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
Intel Core i9-10850K CPU 3.60GHz, 1 CPU, 20 logical and 10 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
| GetHandStrength | 221.2 ns | 4.01 ns | 3.75 ns |

```