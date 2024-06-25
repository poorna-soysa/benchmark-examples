using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkExamples;

[Config(typeof(StyleConfig))]
[HideColumns(Column.RatioSD)]
public class YieldBenchmark
{
    [Params(100_000)]
    public int Number { get; set; }


    [Benchmark(Baseline = true)]
    public void GetEvenNumbers()
    {
        var results = GetEvenNumbers1();
    }

    [Benchmark]
    public void GetEvenNumbers_Yield()
    {
        var results = GetEvenNumbersYield1();
    }

    public IEnumerable<int> GetEvenNumbers1()
    {
        List<int> evenNumbers = new();

        for (int i = 1; i <= Number; i += 2)
        {
            evenNumbers.Add(i);
        }
        return evenNumbers;
    }

    public IEnumerable<int> GetEvenNumbersYield1()
    {
        for (int i = 1; i <= Number; i += 2)
        {
            yield return i;
        }
    }

}

public class StyleConfig : ManualConfig
{
    public StyleConfig()
    {
        SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
}