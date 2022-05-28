using AutoMapper;
using Employeemanagement.ApiModels;
using Employeemanagement.ApiModels.ModelProfile;
using Employeemanagement.Controllers;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class DepartmentTests
    {
        private readonly IAppRepository Repository;
        private readonly DepartmentController Controller;
        private readonly IMapper mapper;

        public DepartmentTests()
        {
            var employeeProfile = new EmployeeProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
            mapper = new Mapper(configuration);
            Repository = new MockAppRepository();
            Controller = new DepartmentController(Repository,mapper);
        }

        [Theory]
        [InlineData("Tajnya")]
        public async Task GetDepartmentEmployees_IfDepartmentExists( string departmentname)
        {
           //Arrange
           var actionResult = await Controller.Get(departmentname);

           //Assert
           var okObjectResult = actionResult.Result as OkObjectResult;
           Assert.NotNull(okObjectResult);
           Assert.IsType<OkObjectResult>(okObjectResult);

           var employeeArray = okObjectResult?.Value as EmployeeWithoutDepartmentModel[];
           Assert.IsType<EmployeeWithoutDepartmentModel[]>(employeeArray);
        }

        [Theory]
        [InlineData("Vivaan")]
        public async Task GetDepartmentEmployees_ifDepartmentNotExists_ThrowNotFound( string departmentname)
        {
          //Act
          var actionResult = await Controller.Get(departmentname);

          //Assert
          var notfound = actionResult.Result as NotFoundObjectResult;
          Assert.IsType<NotFoundObjectResult>(notfound);
        }

    }
}
