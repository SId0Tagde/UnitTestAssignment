using AutoMapper;
using Employeemanagement.ApiModels;
using Employeemanagement.ApiModels.ModelProfile;
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
    public class EmployeeTestsWithMockingLibrary
    {
        [Theory]
        [InlineData(1)]
        public async Task GetEmployee_IfEmployeeexists(int empId)
        {
            //Arrange

            var emp = A.Dummy<Employee>();

            var appRepository = A.Fake<IAppRepository>();
            A.CallTo(() => appRepository.existsEmployee(empId)).Returns(true);
            A.CallTo(() => appRepository.GetEmployee(empId)).Returns(Task.FromResult(emp));
            
            var mapper = A.Fake<IMapper>();

            var employeeModel = A.Fake<EmployeeModel>();
            employeeModel.Id = emp.Id;

            A.CallTo(() => mapper.Map<EmployeeModel>(emp)).Returns(employeeModel);
            
            var controller = A.Fake<EmployeeController>(opt => opt.WithArgumentsForConstructor(new object[] {appRepository, mapper}));
            
            //Act
            var actionResult = await controller.Get(empId);

            //Assert
            var okobjectresult = actionResult.Result as OkObjectResult;
           
            Assert.Equal(200, okobjectresult?.StatusCode);
        }

        [Fact]
        public async Task AddEmployee_WithDepartment()
        {
            //Arrange

            var employee = A.Fake<Employee>();
            var employeeModel = A.CollectionOfFake<EmployeeModel>(2);

            var apprepository = A.Fake<IAppRepository>();
            var mapper = A.Fake<IMapper>();
            
            A.CallTo(() => mapper.Map<Employee>(employeeModel[0])).Returns(employee);
            A.CallTo(() => mapper.Map<EmployeeModel>(employee)).Returns(employeeModel[1]);

            employeeModel[0].DepartmentName = "Adhyaay";

            A.CallTo(() => apprepository.SaveChangesAsync()).Returns(true);

            //var controller = new EmployeeController(apprepository, mapper); 
            var controller = A.Fake<EmployeeController>(opt => opt.WithArgumentsForConstructor(new object[] { apprepository, mapper }));

            //Act
            var actionResult = await controller.Post(employeeModel[0]);

            //Assert
            A.CallTo(() => apprepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddEmployee_WithoutDepartment()
        {
            //Arrange

            var employee = A.Fake<Employee>();
            var employeeModel = A.Fake<EmployeeModel>();

            var apprepository = A.Fake<IAppRepository>();
            var mapper = A.Fake<IMapper>();
            var controller = A.Fake<EmployeeController>(opt => opt.WithArgumentsForConstructor(new object[] {apprepository,mapper}));
            //var controller = new EmployeeController(apprepository, mapper);

            //Act and Assert
            await Assert.ThrowsAsync<InvalidDataException>(
                       async () => await controller.Post(employeeModel));
        }

    }
}
