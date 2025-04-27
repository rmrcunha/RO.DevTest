namespace RO.DevTest.Application.Features.Dashboard.Queries;

public class DashboardSummaryResult
{
    public int TotalUsers { get; set; }
    public int TotalProducts { get; set; }
    public int TotalSales { get; set; }
    public double TotalRevenue { get; set; }
    public double AverageSaleValue { get; set; }
    public double AverageProductPrice { get; set; }
}