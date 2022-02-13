using Frozen.Api;
using Frozen.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Frozen.Api.Data
{
    public class FrozenDbContext: DbContext, IFrozenDbContext
    {
        public DbSet<Character> Characters { get; private set; }
        public FrozenDbContext(DbContextOptions options)
            :base(options) { }

    }
}
