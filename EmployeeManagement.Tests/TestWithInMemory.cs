using Employeemanagement.DatabaseBuild;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public abstract class TestWithInMemory : IDisposable
    {
        private const string connectionString = "Data Source=:memory:";
        private readonly SqliteConnection connection;

        protected readonly AppDbContext context;

        protected TestWithInMemory()
        {
            connection = new SqliteConnection(connectionString);
            connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            context = new AppDbContext(options);
            context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
