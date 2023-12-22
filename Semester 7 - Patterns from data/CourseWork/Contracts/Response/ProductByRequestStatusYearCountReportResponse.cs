namespace CourseWork.Contracts.Response;

public class ProductByRequestStatusYearCountReportResponse
{
    public string Product { get; set; }

    public string RequestStatus { get; set; }

    public int Year { get; set; }

    public int Count { get; set; }
}