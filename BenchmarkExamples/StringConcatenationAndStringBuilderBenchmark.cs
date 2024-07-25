using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using System.Text;

namespace BenchmarkExamples;

[Config(typeof(StyleConfig))]
//[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
//[SimpleJob(RuntimeMoniker.Net80)]
//[SimpleJob(RuntimeMoniker.Net90)]
[HideColumns(Column.Job, Column.RatioSD, Column.AllocRatio, Column.Gen0, Column.Gen1)]
[MemoryDiagnoser]
public class StringConcatenationAndStringBuilderBenchmark
{
    [Params(1000)]
    public int NumberOfRecords { get; set; }

    [Benchmark(Baseline = true)]
    public string StringConcatenation()
    {
        string concatenationString = string.Empty;

        for (int i = 0; i < NumberOfRecords; i++)
        {
            concatenationString += i + ",";
        }

        return concatenationString;
    }

    [Benchmark]
    public string StringBuilder()
    {
        StringBuilder sb = new();

        for (int i = 0; i < NumberOfRecords; i++)
        {
            sb.Append(i);
            sb.Append(", ");
        }

        return sb.ToString();
    }

    private class StyleConfig : ManualConfig
    {
        public StyleConfig()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }
}


