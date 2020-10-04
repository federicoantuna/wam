using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WAM.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }
    }
}