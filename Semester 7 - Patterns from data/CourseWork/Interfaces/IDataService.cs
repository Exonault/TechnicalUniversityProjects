using CourseWork.Contracts.Response;

namespace CourseWork.Interfaces;

public interface IDataService
{
   public Task<List<RequestStatusCountReportResponse>> RequestStatusCount();

   public Task<List<ProductCountReportResponse>> ProductCount();

   public Task<List<AddressRegionCountReportResponse>> AddressRegionCount();
   
   public Task<List<ProductAmountsReportResponse>> ProductAmounts();
   
   public Task<List<RequestStatusAmountsReportResponse>> RequestStatusAmounts();
  
   public decimal AverageApprovedAmount();
   
   public decimal AverageLendedAmount();
   
   public decimal AverageRepaidAmount();

   public NewClientsReportResponse NewClients();

   public PaidOffReportResponse PaidOff();

   public RefinanceReportResponse Refinance();

   public RefinancedReportResponse Refinanced();

   public Task<List<ProductByRequestStatusYearMonthCountReportResponse>> ProductByRequestStatusCountYearMonth();
   
   public Task<List<ProductByRequestStatusYearCountReportResponse>> ProductByRequestStatusCountYear();

   public Task<List<ProductAddressRegionCountReportResponse>> ProductAddressRegionCountReport();





}