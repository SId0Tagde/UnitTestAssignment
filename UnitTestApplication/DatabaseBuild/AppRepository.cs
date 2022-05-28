using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Employeemanagement.DatabaseBuild
{
    public class AppRepository : IAppRepository
    {

        private readonly AppDbContext context;
        private readonly ILogger<AppRepository> logger;

        public AppRepository(AppDbContext context, ILogger<AppRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        //General
        public void Add<T>(T entity) where T : class
        {
            logger.LogInformation($"Adding object of type {entity.GetType}");
            context.Add<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            logger.LogInformation($"Removing an object of type {entity.GetType}");
            context.Remove<T>(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            logger.LogInformation($"Attempting to save changes");
            return ((await context.SaveChangesAsync()) > 0);
        }

        //Department
        public async Task<bool> existsDepartment(string departmentname)
        {
            logger.LogInformation($"Checking if {departmentname} exists");
            return await context.Departments
                                .Where(dp => dp.DepartmentName == departmentname)
                                .AnyAsync();
        }

        public async Task<Department> GetDepartment(string departmentname)
        {
            logger.LogInformation($"Getting Department Object with {departmentname}");
            return await context.Departments
                            .Where(dp => dp.DepartmentName == departmentname)
                            .FirstAsync();
        }

        //Employee
        public async Task<bool> existsEmployee(int empid)
        {
            logger.LogInformation($"Checking if {empid} exists");
            return await context.Employees
                                 .Where(emp => emp.Id == empid)
                                 .AnyAsync();
        }

        public async Task<Employee> GetEmployee(int empid)
        {
            logger.LogInformation($"Getting Employee having id={empid}");
            return await  context.Employees.Include(emp => emp.Department)
                                .Where(emp => emp.Id == empid)
                                .FirstAsync();
        }

        public async Task<Employee[]> GetDepartmentEmployeesFrom(string departmentname)
        {
            logger.LogInformation($"Getting Employees from department: {departmentname}");
            return await context.Employees
                                .Where(emp => emp.Department.DepartmentName == departmentname)
                                .ToArrayAsync();
        }
    }
}
