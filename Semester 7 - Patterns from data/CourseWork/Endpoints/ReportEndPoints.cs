using System.Text.Json;
using CourseWork.Contracts.Response;
using CourseWork.Interfaces;
using CourseWork.Service;

namespace CourseWork.Endpoints;

public static class ReportEndPoints
{
    public static void MapReportEndPoints(this WebApplication app)
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
        app.MapGet("/productAddressRegion", ProductByAddressRegionCountReport);
    }

    public static void AddReportService(this IServiceCollection services)
    {
        services.AddScoped<IReportService, ReportService>();
    }

    internal static async Task<IResult> RequestStatusCountReport(IReportService service)
    {
        List<RequestStatusCountReportResponse> result = await service.RequestStatusCount();

        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductCountReport(IReportService service)
    {
        List<ProductCountReportResponse> result = await service.ProductCount();

        return Results.Ok(result);
    }

    internal static async Task<IResult> AddressRegionCountReport(IReportService service)
    {
        List<AddressRegionCountReportResponse> result = await service.AddressRegionCount();
        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductAmountsReport(IReportService service)
    {
        List<ProductAmountsReportResponse> result = await service.ProductAmounts();
        return Results.Ok(result);
    }

    internal static async Task<IResult> RequestStatusAmountsReport(IReportService service)
    {
        List<RequestStatusAmountsReportResponse> result = await service.RequestStatusAmounts();
        return Results.Ok(result);
    }

    internal static IResult AverageApprovedAmountReport(IReportService service)
    {
        decimal result = service.AverageApprovedAmount();
        return Results.Ok(new
        {
            AverageApprovedAmount = result
        });
    }

    internal static IResult AverageLendedAmountReport(IReportService service)
    {
        decimal result = service.AverageLendedAmount();
        return Results.Ok(new
        {
            AverageLendedAmount = result
        });
    }

    internal static IResult AverageRepaidAmountReport(IReportService service)
    {
        decimal result = service.AverageRepaidAmount();
        return Results.Ok(new
        {
            AverageRepaidAmount = result
        });
    }

    internal static IResult NewClientsReport(IReportService service)
    {
        NewClientsReportResponse result = service.NewClients();

        return Results.Ok(result);
    }

    internal static IResult PaidOffReport(IReportService service)
    {
        PaidOffReportResponse result = service.PaidOff();

        return Results.Ok(result);
    }

    internal static IResult RefinanceReport(IReportService service)
    {
        RefinanceReportResponse result = service.Refinance();

        return Results.Ok(result);
    }

    internal static IResult RefinancedReport(IReportService service)
    {
        RefinancedReportResponse result = service.Refinanced();
        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductByRequestStatusCountYearMonthReport(IReportService service)
    {
        List<ProductByRequestStatusYearMonthCountReportResponse> result =
            await service.ProductByRequestStatusCountYearMonth();

        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductByRequestStatusCountYearReport(IReportService service)
    {
        List<ProductByRequestStatusYearCountReportResponse> result = await service.ProductByRequestStatusCountYear();
        return Results.Ok(result);
    }

    internal static async Task<IResult> ProductByAddressRegionCountReport(IReportService service)
    {
        List<ProductAddressRegionCountReportResponse> result = await service.ProductAddressRegionCountReport();
        return Results.Ok(result);
    }
}