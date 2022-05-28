using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Employeemanagement.DatabaseBuild
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

    }
}
