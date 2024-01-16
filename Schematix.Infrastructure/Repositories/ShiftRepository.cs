using Microsoft.Extensions.Logging;
using Schematix.Infrastructure.Context;

namespace Schematix.Infrastructure.Repositories;

public class ShiftRepository
{
    private readonly DataContext _dataContext;
    private readonly ILogger _logger;

    public ShiftRepository(DataContext dataContext, ILogger logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }
}
