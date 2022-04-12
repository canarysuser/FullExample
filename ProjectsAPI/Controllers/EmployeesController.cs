using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectEntities;
using ProjectsDataAccess;

namespace ProjectsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authentication.Authorization]
    public class EmployeesController : ControllerBase
    {
        EmployeesDataAccess da;
        ProjectsDAL dal; 
        public EmployeesController(IConfiguration configuration, EmployeesDataAccess dataAccess)
        {
            da = dataAccess;
            dal = new ProjectsDAL(configuration.GetConnectionString("ProjectsDbConnection"));
        }

        [HttpGet]
        //URL:  api/employees
        public IActionResult Get()
        {
            return Ok(da.GetAllEmployees());
        }

        [HttpGet("{empId}")]
        //URL:  api/employees
        public IActionResult Get(int empId)
        {
            return Ok(da.GetEmployeeById(empId));
        }

        [HttpPost]
        //URL:  api/employees
        public IActionResult CreateEmployee(Employee model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            da.AddNewEmployee(model);
            return Ok(model);
        }

    }
}
