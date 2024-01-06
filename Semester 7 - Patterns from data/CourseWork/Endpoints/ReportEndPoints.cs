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
        try
        {
            List<RequestStatusCountReportResponse> result = await service.RequestStatusCount();
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> ProductCountReport(IReportService service)
    {
        try
        {
            List<ProductCountReportResponse> result = await service.ProductCount();

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> AddressRegionCountReport(IReportService service)
    {
        try
        {
            List<AddressRegionCountReportResponse> result = await service.AddressRegionCount();
           
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> ProductAmountsReport(IReportService service)
    {
        try
        {
            List<ProductAmountsReportResponse> result = await service.ProductAmounts();
            
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> RequestStatusAmountsReport(IReportService service)
    {
        try
        {
            List<RequestStatusAmountsReportResponse> result = await service.RequestStatusAmounts();
           
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult AverageApprovedAmountReport(IReportService service)
    {
        try
        {
            decimal result = service.AverageApprovedAmount();
            
            return Results.Ok(new
            {
                AverageApprovedAmount = result
            });
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult AverageLendedAmountReport(IReportService service)
    {
        try
        {
            decimal result = service.AverageLendedAmount();
           
            return Results.Ok(new
            {
                AverageLendedAmount = result
            });
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult AverageRepaidAmountReport(IReportService service)
    {
        try
        {
            decimal result = service.AverageRepaidAmount();
           
            return Results.Ok(new
            {
                AverageRepaidAmount = result
            });
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult NewClientsReport(IReportService service)
    {
        try
        {
            NewClientsReportResponse result = service.NewClients();

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult PaidOffReport(IReportService service)
    {
        try
        {
            PaidOffReportResponse result = service.PaidOff();

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult RefinanceReport(IReportService service)
    {
        try
        {
            RefinanceReportResponse result = service.Refinance();

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static IResult RefinancedReport(IReportService service)
    {
        try
        {
            RefinancedReportResponse result = service.Refinanced();
            
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> ProductByRequestStatusCountYearMonthReport(IReportService service)
    {
        try
        {
            List<ProductByRequestStatusYearMonthCountReportResponse> result =
                await service.ProductByRequestStatusCountYearMonth();

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> ProductByRequestStatusCountYearReport(IReportService service)
    {
        try
        {
            List<ProductByRequestStatusYearCountReportResponse>
                result = await service.ProductByRequestStatusCountYear();
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    internal static async Task<IResult> ProductByAddressRegionCountReport(IReportService service)
    {
        try
        {
            List<ProductCountReportResponse> result = await service.ProductCount();

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}