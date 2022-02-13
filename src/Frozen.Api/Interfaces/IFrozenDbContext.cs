using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Frozen.Api.Interfaces
{
    public interface IFrozenDbContext
    {
        DbSet<Character> Characters { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
