using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Task.Data;
using Web_API_Task.Modules;

namespace Web_API_Task.Controllers
{
    //Main rout for this application*********************** 
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly EmployeesAPIdbContext dbContext;
        public ActivityController(EmployeesAPIdbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get Methods****************************************************************
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await dbContext.Employees.ToListAsync());

        }

        //Post MEthod****************************************************************
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            var employee = new Employees()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Address = addEmployeeRequest.Address,
                Position = addEmployeeRequest.Position,
                Salary = addEmployeeRequest.Salary
            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        //Update MEthod *************************************************************
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id, UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.Name = updateEmployeeRequest.Name;
                employee.Address = updateEmployeeRequest.Address;
                employee.Position = updateEmployeeRequest.Position;
                employee.Salary = updateEmployeeRequest.Salary;

                await dbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();


        }

        //Get One record****************************************
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                dbContext.Remove(employee);
                await dbContext.SaveChangesAsync();
            }
            return NotFound();

        }

    }
    
}
