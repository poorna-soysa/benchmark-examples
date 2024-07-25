using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkExamples;

[Config(typeof(StyleConfig))]
[HideColumns(Column.RatioSD)]
public class YieldBenchmark
{
    private List<int> precomputedNumbers;

    [Params(100_000)]
    public int NumberOfElements { get; set; }


    [GlobalSetup]
    public void InitializeBenchmark()
    {
        precomputedNumbers = new List<int>();
        for (var index = 0; index < NumberOfElements; index++)
        {
            precomputedNumbers.Add(index);
        }
    }

    [Benchmark]
    public void SquareNumbersUsingYield()
    {
        foreach (var number in GenerateNumbersUsingYield(NumberOfElements))
        {
            var squaredNumber = number * number;
        }
    }

    [Benchmark]
    public void SquareNumbersUsingList()
    {
        foreach (var number in precomputedNumbers)
        {
            var squaredNumber = number * number;
        }
    }

    private IEnumerable<int> GenerateNumbersUsingYield(int maximumValue)
    {
        for (int index = 0; index < maximumValue; index++)
        {
            yield return index;
        }
    }

    private class StyleConfig : ManualConfig
    {
        public StyleConfig()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }

}

//public class StyleConfig : ManualConfig
//{
//    public StyleConfig()
//    {
//        SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
//    }
//}