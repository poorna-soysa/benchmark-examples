using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkExamples;

[Config(typeof(StyleConfig))]
[HideColumns(Column.RatioSD)]
[HideColumns(Column.Job, Column.RatioSD, Column.AllocRatio, Column.Gen0, Column.Gen1)]
[MemoryDiagnoser]
public class SelectManyBenchMark
{
    [Params(1000)]
    public int NumberOfRecords;


    [Benchmark(Baseline = true)]
    public List<OrderItem> ForLoop()
    {
        var orders = GetOrders(NumberOfRecords);
        List<OrderItem> orderItems = new();

        foreach (var order in orders)
        {
            foreach (var item in order.Items)
            {
                orderItems.Add(item);
            }
        }

        return orderItems;
    }

    [Benchmark]
    public List<OrderItem> SelectMany()
    {
        return GetOrders(NumberOfRecords)
            .SelectMany(order => order.Items)
            .ToList();
    }

    private List<Order> GetOrders(int numberOfRecords)
    {
        var random = new Random();

        return Enumerable.Range(1, numberOfRecords)
         .Select(i => new Order
         {
             OrderId = i,
             Items = Enumerable.Range(1, random.Next(1, 5)) // Generate 1-4 OrderItems
                 .Select(j => new OrderItem
                 {
                     ProductId = random.Next(1000), // Replace with actual product IDs
                     Qty = random.Next(1, 10)
                 })
                 .ToList()
         })
         .ToList();
    }

    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }

    private class StyleConfig : ManualConfig
    {
        public StyleConfig()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }
}



