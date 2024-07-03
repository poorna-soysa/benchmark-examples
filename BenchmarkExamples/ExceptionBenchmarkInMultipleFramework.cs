using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;

namespace BenchmarkExamples;

[Config(typeof(StyleConfig))]
[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[HideColumns(Column.Job, Column.RatioSD, Column.AllocRatio, Column.Gen0, Column.Gen1)]
[MemoryDiagnoser]
public class ExceptionBenchmarkInMultipleFramework
{
    [Params(100_000)]
    public int NumberOfRecords { get; set; }

    [Benchmark]
    public void ThrowException()
    {
        for (int i = 0; i < NumberOfRecords; i++)
        {
            try
            {
                throw new Exception("Exception");
            }
            catch
            {
                
            }
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


