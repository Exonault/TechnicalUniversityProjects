using System.Text.Json;
using CourseWork.Contracts.Response;
using CourseWork.Interfaces;
using CourseWork.Service;

namespace CourseWork.Endpoints;

public static class DataSetEndpoints
{
    public static void MapDataEndPoints(this WebApplication app)
    {
        app.MapGet("/requestStatusCountReport", RequestStatusCountReport);
        app.MapGet("/productCountReport", ProductCountReport);
        app.MapGet("/addressRegionCountReport", AddressRegionCountReport);
        app.MapGet("/productAmountsReport", ProductAmountsReport);
        app.MapGet("/requestStatusAmountsReport", RequestStatusAmountsReport);
        app.MapGet("/averageApprovedAmountReport", AverageApprovedAmountReport);
        app.MapGet("/averageLendedAmountReport", AverageLendedAmountReport);
        app.MapGet("/averageRepaidAmountReport", AverageRepaidAmountReport);
        app.MapGet("/newClientsReport", NewClientsReport);
        app.MapGet("/paidOffReport", PaidOffReport);
        app.MapGet("/refinanceReport", RefinanceReport);
        app.MapGet("/refinancedReport", RefinancedReport);
        app.MapGet("/productByRequestStatusCountYearMonthReport", ProductByRequestStatusCountYearMonthReport);
        app.MapGet("/productByRequestStatusCountYearReport", ProductByRequestStatusCountYearReport);
        app.MapGet("/productAddressRegion", ProductByRequestStatusCountYearReport);
        
    }

    public static void AddDataService(this IServiceCollection services)
    {
        services.AddScoped<IDataService, DataService>();
    }

    internal static async Task<IResult> RequestStatusCountReport(IDataService service)
    {
        List<RequestStatusCountReportResponse> result = await service.RequestStatusCount();

        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductCountReport(IDataService service)
    {
        List<ProductCountReportResponse> result = await service.ProductCount();

        return Results.Ok(result);
    }

    internal static async Task<IResult> AddressRegionCountReport(IDataService service)
    {
        List<AddressRegionCountReportResponse> result = await service.AddressRegionCount();
        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductAmountsReport(IDataService service)
    {
        List<ProductAmountsReportResponse> result = await service.ProductAmounts();
        return Results.Ok(result);
    }

    internal static async Task<IResult> RequestStatusAmountsReport(IDataService service)
    {
        List<RequestStatusAmountsReportResponse> result = await service.RequestStatusAmounts();
        return Results.Ok(result);
    }

    internal static async Task<IResult> AverageApprovedAmountReport(IDataService service)
    {
        decimal result = service.AverageApprovedAmount();
        return Results.Ok(new
        {
            AverageApprovedAmount = result
        });
    }
    
    internal static async Task<IResult> AverageLendedAmountReport(IDataService service)
    {
        decimal result = service.AverageLendedAmount();
        return Results.Ok(new
        {
            AverageLendedAmount = result
        });
    }
    
    internal static async Task<IResult> AverageRepaidAmountReport(IDataService service)
    {
        decimal result = service.AverageRepaidAmount();
        return Results.Ok(new
        {
            AverageRepaidAmount = result
        });
    }
    
    internal static async Task<IResult> NewClientsReport(IDataService service)
    {
        NewClientsReportResponse result = service.NewClients();

        return Results.Ok(result);
    }
    
    internal static async Task<IResult> PaidOffReport(IDataService service)
    {
        PaidOffReportResponse result = service.PaidOff();

        return Results.Ok(result);
    }
    
    internal static async Task<IResult> RefinanceReport(IDataService service)
    {
        RefinanceReportResponse result = service.Refinance();

        return Results.Ok(result);
    }
    
    internal static async Task<IResult> RefinancedReport(IDataService service)
    {
        RefinancedReportResponse result = service.Refinanced();
        return Results.Ok(result);
    }
    
    internal static async Task<IResult> ProductByRequestStatusCountYearMonthReport(IDataService service)
    {
        List<ProductByRequestStatusYearMonthCountReportResponse> result = await service.ProductByRequestStatusCountYearMonth();
        
        return Results.Ok(result);
    }
    
    internal static async Task<IResult> ProductByRequestStatusCountYearReport(IDataService service)
    {
        List<ProductByRequestStatusYearCountReportResponse> result = await service.ProductByRequestStatusCountYear();
        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductByAddressRegionCountReport(IDataService service)
    {
        List<ProductAddressRegionCountReportResponse> result = await service.ProductAddressRegionCountReport();
        return Results.Ok(result);
    }
}