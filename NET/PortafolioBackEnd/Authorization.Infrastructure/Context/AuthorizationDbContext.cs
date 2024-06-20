using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Authorization.Infrastructure.Context
{
    public class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext(DbContextOptions options) : base (options) { }
        public DbSet<Domain.Models.Authorization> Authorizations { get; set; }
    }
}
