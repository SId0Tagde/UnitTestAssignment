using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class RepositoryTests : TestWithInMemory
    {
        private AppRepository mockAppRepository;
        private ILogger<AppRepository> logger;

        public RepositoryTests()
        {
            var departmenta = new Department()
            {
                DepartmentName = "Tajnya"
            };
            var employee1 = new Employee()
            {
                Id = 1,
                Name = "Dhananjay",
                SurName = "Veer",
                Address = "Behind Smaarak,Raj Niwaas ",
                Department = departmenta,
                PhoneNumber = "3423423454",
                Qualification = "Visheshjajnya"
            };
            var employee2 = new Employee()
            {
                Id = 2,
                Name = "Tanmay",
                SurName = "Vyom",
                Address = "Behind Smaarak,Raj Niwaas ",
                Department = departmenta,
                PhoneNumber = "3423423454",
                Qualification = "Visheshjajnya"
            };

            context.Departments.Add(departmenta);
            context.SaveChanges();
            context.Employees.Add(employee1);
            context.Employees.Add(employee2);
            context.SaveChanges();
            logger = A.Fake<ILogger<AppRepository>>();
            mockAppRepository = A.Fake<AppRepository>(opt => opt.WithArgumentsForConstructor(new object[] {context,logger}));

        }

        
        //Department

        [Theory]
        [InlineData(true, "Tajnya")]
        [InlineData(false, "Vijnyaan")]
        public async Task IfDepartmentExists_ReturnTrue_OtherwiseFalse(bool exists,string departmentName)
        {
            //Act
            var existornot = await mockAppRepository.existsDepartment(departmentName);
            //Assert
           Assert.Equal(exists,existornot);
        }

        [Theory]
        [InlineData(true, "Tajnya")]
        [InlineData(false, "Vijnyaan")]
        public async Task getDepartmentIfExists_OtherwiseThrowInvalidException(bool exists,string depmntName)
        {         
            //Act and Assert
            if(exists)
            {
                var getDepmnt = await mockAppRepository.GetDepartment(depmntName);
                Assert.Equal(depmntName, getDepmnt.DepartmentName);
            }
            else
            {
                await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await mockAppRepository.GetDepartment(depmntName));
            }
        }

        [Theory]
        [InlineData(true, "Tajnya")]
        [InlineData(false, "Vijnyaan")]
        public async Task GetDepartmentEmployees_IfDepartmentExists(bool exists, string depmntName)
        {
            //Act
            var employees = await mockAppRepository.GetDepartmentEmployeesFrom(depmntName);
            //Asssert
            if(exists)
            {
                Assert.NotEmpty(employees);
            }
            else
            {
                Assert.Empty(employees);
            }
        }

        //Employees

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 23)]
        public async Task IfEmployeeExistsReturnTrue_OtherwiseFalse(bool exists, int empId)
        {
            //Act
            var existOrNot = await mockAppRepository.existsEmployee(empId);
            //Assert
            Assert.Equal(exists, existOrNot);
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 23)]
        public async Task getEmployeeIfExists(bool exists, int empId)
        {
            //Act and Assert
            if (exists)
            {
                var emp = await mockAppRepository.GetEmployee(empId);
                Assert.Equal(empId,emp.Id);
            }
            else
            {
                await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await mockAppRepository.GetEmployee(empId));
            }
        }
    }
}
