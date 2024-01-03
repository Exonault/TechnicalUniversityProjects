using CourseWork.Contracts.Response;
using CourseWork.Data;
using CourseWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Service;

public class DataService : IDataService
{
    private readonly MyDbContext _dbContext;

    public DataService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<RequestStatusCountReportResponse>> RequestStatusCount()
    {
        var result = await _dbContext.Requests.GroupBy(x => x.RequestStatus)
            .Select(x => new RequestStatusCountReportResponse()
            {
                RequestStatus = x.Key,
                RequestStatusCount = x.Count(),
            }).ToListAsync();

        return result;
    }

    public async Task<List<ProductCountReportResponse>> ProductCount()
    {
        var result = await _dbContext.Requests.GroupBy(x => x.Product)
            .Select(x => new ProductCountReportResponse()
            {
                Product = x.Key,
                ProductCount = x.Count(),
            }).ToListAsync();

        return result;
    }

    public async Task<List<AddressRegionCountReportResponse>> AddressRegionCount()
    {
        var result = await _dbContext.Requests.GroupBy(x => x.AddressRegion)
            .Select(x => new AddressRegionCountReportResponse()
            {
                AddressRegion = x.Key,
                AddressRegionCount = x.Count(),
            }).ToListAsync();

        return result;
    }

    public async Task<List<ProductAmountsReportResponse>> ProductAmounts()
    {
        var result = await _dbContext.Requests.GroupBy(x => x.Product)
            .Select(x => new ProductAmountsReportResponse()
            {
                Product = x.Key,
                ApprovedAmount = x.Average(y => y.ApprovedAmount),
                LendedAmount = x.Average(y => y.LendedAmount),
                RepaidAmount = x.Average(y => y.RepaidAmount),
            }).ToListAsync();

        return result;
    }

    public async Task<List<RequestStatusAmountsReportResponse>> RequestStatusAmounts()
    {
        var result = await _dbContext.Requests.GroupBy(x => x.RequestStatus)
            .Select(x => new RequestStatusAmountsReportResponse()
            {
                RequestStatus = x.Key,
                ApprovedAmount = x.Average(y => y.ApprovedAmount),
                LendedAmount = x.Average(y => y.LendedAmount),
                RepaidAmount = x.Average(y => y.RepaidAmount),
            }).ToListAsync();

        return result;
    }

    public decimal AverageApprovedAmount()
    {
        return _dbContext.Requests.Average(x => x.ApprovedAmount);
    }

    public decimal AverageLendedAmount()
    {
        return _dbContext.Requests.Average(x => x.LendedAmount);
    }

    public decimal AverageRepaidAmount()
    {
        return _dbContext.Requests.Average(x => x.RepaidAmount);
    }

    // ReSharper disable once PossibleLossOfFraction
    public NewClientsReportResponse NewClients()
    {
        int count = _dbContext.Requests.Count(x => x.IsNewClient == true);
        return new NewClientsReportResponse()
        {
            NewClientsCount = count,
            NewClientsPercentage = Decimal.Divide(count, _dbContext.Requests.Count()) * 100.0m,
        };
    }

    // ReSharper disable once PossibleLossOfFraction
    public PaidOffReportResponse PaidOff()
    {
        int count = _dbContext.Requests.Count(x => x.IsPaidOff == true);
        return new PaidOffReportResponse()
        {
            PaidOffCount = count,
            PaidOffPercentage = Decimal.Divide(count, _dbContext.Requests.Count()) * 100.0m,
        };
    }

    // ReSharper disable once PossibleLossOfFraction
    public RefinanceReportResponse Refinance()
    {
        int count = _dbContext.Requests.Count(x => x.IsRefinance == true);
        return new RefinanceReportResponse()
        {
            RefinanceCount = count,
            RefinancePercentage = Decimal.Divide(count, _dbContext.Requests.Count()) * 100.0m,
        };
    }

    // ReSharper disable once PossibleLossOfFraction
    public RefinancedReportResponse Refinanced()
    {
        int count = _dbContext.Requests.Count(x => x.IsRefinanced == true);
        return new RefinancedReportResponse()
        {
            RefinancedCount = count,
            RefinancedPercentage = Decimal.Divide(count, _dbContext.Requests.Count()) * 100.0m,
        };
    }

    public async Task<List<ProductByRequestStatusYearMonthCountReportResponse>> ProductByRequestStatusCountYearMonth()
    {
        var result = await _dbContext.Requests.GroupBy(x => new
            {
                x.Product,
                x.RequestStatus,
                x.ApplicationDate.Year,
                x.ApplicationDate.Month,
            })
            .Select(x => new ProductByRequestStatusYearMonthCountReportResponse()
            {
                Product = x.Key.Product,
                RequestStatus = x.Key.RequestStatus,
                Year = x.Key.Month,
                Month = x.Key.Month,
                Count = x.Count(),
            })
            .OrderBy(x => x.Product)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ThenBy(x => x.RequestStatus)
            .ToListAsync();

        return result;
    }

    public async Task<List<ProductByRequestStatusYearCountReportResponse>> ProductByRequestStatusCountYear()
    {
        var result = await _dbContext.Requests.GroupBy(x => new
            {
                x.Product,
                x.RequestStatus,
                x.ApplicationDate.Year,
            })
            .Select(x => new ProductByRequestStatusYearCountReportResponse()
            {
                Product = x.Key.Product,
                RequestStatus = x.Key.RequestStatus,
                Year = x.Key.Year,
                Count = x.Count(),
            })
            .OrderBy(x => x.Product)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.RequestStatus)
            .ToListAsync();

        return result;
    }

    public async Task<List<ProductAddressRegionCountReportResponse>> ProductAddressRegionCountReport()
    {
        var result = await _dbContext.Requests.GroupBy(x => new
            {
                x.AddressRegion,
                x.Product
            }).Select(x => new ProductAddressRegionCountReportResponse()
            {
                AddressRegion = x.Key.AddressRegion,
                Product = x.Key.Product,
                Count = x.Count(),
            })
            .OrderBy(x => x.Product)
            .ThenBy(x => x.AddressRegion)
            .ToListAsync();

        return result;
    }
}