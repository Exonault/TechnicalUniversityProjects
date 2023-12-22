namespace CourseWork.Contracts.Response;

public class ProductByRequestStatusMonthCountReportResponse
{
    public string Product { get; set; }

    public string RequestStatus { get; set; }

    public int Month { get; set; }

    public int Count { get; set; }
}