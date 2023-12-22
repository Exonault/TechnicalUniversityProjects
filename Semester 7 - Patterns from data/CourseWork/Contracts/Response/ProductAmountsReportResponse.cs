namespace CourseWork.Contracts.Response;

public class ProductAmountsReportResponse
{
    public string Product { get; set; }

    public decimal ApprovedAmount { get; set; }
    
    public decimal LendedAmount { get; set; }

    public decimal RepaidAmount { get; set; }
    
    
}