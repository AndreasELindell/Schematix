using Microsoft.Extensions.Logging;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Infrastructure.Context;

namespace Schematix.Infrastructure.Repositories;

public class BranchRepository
{
    private readonly DataContext _dataContext;
    private readonly ILogger _logger;

    public BranchRepository(DataContext dataContext, ILogger logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

   
}
