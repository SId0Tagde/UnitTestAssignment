using Employeemanagement.DatabaseBuild.EntityModels;

namespace Employeemanagement.DatabaseBuild
{
    public interface IAppRepository
    {
        //General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();

        //Employee
        Task<Employee> GetEmployee(int empid);
        Task<bool> existsEmployee(int empid);
        Task<Employee[]> GetDepartmentEmployeesFrom(string departmentname);


        //Department
        Task<Department> GetDepartment(string departmentname);
        Task<bool> existsDepartment(string departmentname);


    }
}
