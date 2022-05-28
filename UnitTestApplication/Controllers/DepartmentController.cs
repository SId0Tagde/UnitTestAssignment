using AutoMapper;
using Employeemanagement.ApiModels;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employeemanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IAppRepository Repository;
        private readonly IMapper mapper;

        public DepartmentController(IAppRepository repository, IMapper mapper)
        {
            Repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gives Employees of Department
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        // GET: api/<DepartmentController>
        [HttpGet("{departmentName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeWithoutDepartmentModel[]>> Get(string departmentName)
        {
            if(await Repository.existsDepartment(departmentName))
            {
                return Ok(mapper.Map<EmployeeWithoutDepartmentModel[]>(await Repository.GetDepartmentEmployeesFrom(departmentName)));
            }
            return NotFound($"Department:{departmentName} does not exist");
        }

        
    }
}
