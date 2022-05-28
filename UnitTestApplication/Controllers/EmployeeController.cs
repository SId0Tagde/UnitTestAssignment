using AutoMapper;
using Employeemanagement.ApiModels;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Employeemanagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IAppRepository Repository;

        private readonly IMapper mapper;

        public EmployeeController(IAppRepository repository, IMapper mapper)
        {
            Repository = repository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Returns the Employee from EmployeeId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeModel>> Get(int employeeId)
        {
            if(await Repository.existsEmployee(employeeId))
            {
                var emp = await Repository.GetEmployee(employeeId);
                return Ok(mapper.Map<EmployeeModel>(emp));
            }
            return NotFound($"Employee with {employeeId} not exists");
        }

        /// <summary>
        /// Post a Employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeModel>> Post([FromBody] EmployeeModel model)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(model);
            }
            
            //Does employee exists
            if (await Repository.existsEmployee(model.Id))
            {
                return BadRequest($"EmployeeId : {model.Id} already exists");
            }

            if(string.IsNullOrWhiteSpace(model.DepartmentName) )
            {
                throw new InvalidDataException($"{model.DepartmentName}");
            }

            Employee emp;
            if (await Repository.existsDepartment(model.DepartmentName))
            {
                var dpmnt = await Repository.GetDepartment(model.DepartmentName);
                
                 emp = mapper.Map<Employee>(model);
                emp.Department = dpmnt;
                Repository.Add(emp);

            }
            else
            {
                emp = mapper.Map<Employee>(model);
                Repository.Add(emp);   
            }

            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/employee/{emp.Id}", mapper.Map<EmployeeModel>(emp));
            }
            else
            {
                return BadRequest("Unable to Save");
            }

        }

    }
}