using CleanArcTemp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArcTemp.Application.Common.Interfaces;

public interface ICleanArcTempDbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Policy> Policy { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public int SaveChangesAsync();
}