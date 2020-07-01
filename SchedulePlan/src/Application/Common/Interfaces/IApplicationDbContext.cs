using SchedulePlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulePlan.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Schedule> Schedules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
