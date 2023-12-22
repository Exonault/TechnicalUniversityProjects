namespace CourseWork.Contracts.Response;

public class RequestStatusAmountsReportResponse
{
    public string RequestStatus { get; set; }

    public decimal ApprovedAmount { get; set; }
    
    public decimal LendedAmount { get; set; }

    public decimal RepaidAmount { get; set; }
}