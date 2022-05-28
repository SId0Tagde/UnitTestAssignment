using AutoMapper;
using Employeemanagement.ApiModels;
using Employeemanagement.ApiModels.ModelProfile;
using Employeemanagement.Controllers;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Tests
{
    public class EmployeeTests 
    {
        private readonly EmployeeController controller;
        private readonly IAppRepository appRepository;
        private readonly IMapper mapper;
        public EmployeeTests()
        {
            var employeeProfile = new EmployeeProfile();
            var configuration = new MapperConfiguration(cfg =>
                    cfg.AddProfile(employeeProfile));
            mapper = new Mapper(configuration);
            appRepository = new MockAppRepository();
            controller = new EmployeeController(appRepository,mapper);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetEmployee_IfEmployeeexists(int empId)
        {
            //Act
            var actionResult = await controller.Get(empId);

            //Assert
            var okobjectresult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okobjectresult);
            Assert.IsType<OkObjectResult>(okobjectresult);

            var employee = okobjectresult?.Value as EmployeeModel;
            Assert.NotNull(employee);
            Assert.Equal(empId, employee?.Id);
        }

        [Theory]
        [InlineData(23)]
        public async Task GetEmployee_IfNotExistThrowNotFound(int empId)
        {
            //Act
            var actionResult = await controller.Get(empId);
            var employeeid = empId;
            
            //Assert
            var notfound = actionResult.Result as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(notfound);
        }


        [Fact]
        public async Task AddEmployee_WithDepartment()
        {
            //Arrange
            
            var employee = new EmployeeModel()
            {
               Id = 245,
               Name = "Dhanjay",
               SurName = "Veer",
               Address = "Behind Sahiba Chowk,Guru Darbaar",
               DepartmentName = "Adhyaay",
               PhoneNumber = "3453452314",
               Qualification = "tanjnya"
            };

            //Act
            var actionResponse = await controller.Post(employee);

            //Assert
            var createdResult = actionResponse.Result as CreatedResult;
            Assert.IsType<CreatedResult>(createdResult);
        }

        [Fact]
        public async Task AddEmployee_WithoutDepartment()
        {
            //Arrange

            var employee = new EmployeeModel()
            {
               Id = 23,
               Name = "Dhanjay",
               SurName = "Veer",
               //DepartmentName = "  "
               //DepartmentName = null
               DepartmentName = "",
               Address = "Behind Sahiba Chowk,Guru Darbaar",
               PhoneNumber = "3453452314",
               Qualification = "tanjnya"
            };

            //Act and Assert
            await Assert.ThrowsAsync<InvalidDataException>(
                       async () => await controller.Post(employee));
        }


    }
}