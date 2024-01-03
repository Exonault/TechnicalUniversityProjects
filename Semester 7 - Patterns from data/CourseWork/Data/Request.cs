namespace CourseWork.Data;

public class Request
{
    public int RequestId { get; set; }

    public bool IsPaidOff { get; set; }

    public string RequestStatus { get; set; }

    public string Product { get; set; }

    public DateOnly ApplicationDate { get; set; }

    public decimal ApprovedAmount { get; set; }

    public decimal LendedAmount { get; set; }

    public bool IsRefinance { get; set; }

    public bool IsRefinanced { get; set; }

    public bool IsNewClient { get; set; }

    public int Period { get; set; }

    public decimal RepaidAmount { get; set; }

    public int AddressRegion { get; set; }
    
}