using AutoMapper;
using Employeemanagement.ApiModels;
using Employeemanagement.Controllers;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class DepartmentTestsWithMockingLibrary
    {
        [Theory]
        [InlineData("Tajnya")]
        public async Task GetDepartmentEmployees_IfDepartmentExists(string departmentname)
        {
            //Arrange
            var employeeArray = A.CollectionOfDummy<Employee>(2).ToArray();
            var appRepository = A.Fake<IAppRepository>();
            var mapper = A.Fake<IMapper>();
            var employeeWithoutDepartmentModelArray = A.CollectionOfDummy<EmployeeWithoutDepartmentModel>(2).ToArray();

            A.CallTo(() => appRepository.existsDepartment(departmentname)).Returns(true);
            A.CallTo(() => appRepository.GetDepartmentEmployeesFrom(departmentname)).Returns(Task.FromResult(employeeArray));
            A.CallTo(() => mapper.Map<EmployeeWithoutDepartmentModel[]>(employeeArray)).Returns(employeeWithoutDepartmentModelArray);

            var controller = A.Fake<DepartmentController>(opt => opt.WithArgumentsForConstructor(new object[] { appRepository, mapper }));
            var actionResult = await controller.Get(departmentname);

            //Assert
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.Equal(200, okObjectResult?.StatusCode);
            
        }

        [Theory]
        [InlineData("Vivaan")]
        public async Task GetDepartmentEmployees_ifDepartmentNotExists_ThrowNotFound(string departmentname)
        {
            //Arrange
            var appRepository = A.Fake<IAppRepository>();
            var mapper = A.Fake<IMapper>();
            var controller = A.Fake<DepartmentController>(opt => opt.WithArgumentsForConstructor(new object[] { appRepository, mapper }));

            //Act
            var actionResult = await controller.Get(departmentname);

            //Assert
            var notfound = actionResult.Result as NotFoundObjectResult;
            Assert.Equal(404,notfound?.StatusCode);
        }

    }
}
