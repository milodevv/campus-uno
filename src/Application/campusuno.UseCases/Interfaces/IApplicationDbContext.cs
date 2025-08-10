using campusuno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace campusuno.UseCases;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<Students> Students { get; set; }
}
