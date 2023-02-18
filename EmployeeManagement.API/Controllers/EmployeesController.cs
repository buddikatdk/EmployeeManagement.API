using EmployeeManagement.API.Data;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EMPDBContext context;
        public EmployeesController(EMPDBContext context) =>
            this.context = context;

        [HttpGet]
        public async Task<IQueryable<Employee>> GetAllEmployeesAsync() =>
            this.context.Employees.AsQueryable<Employee>();

        [HttpPost]
        public async ValueTask<Employee> AddEmployeeAsync([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await this.context.Employees.AddAsync(employee);
            await this.context.SaveChangesAsync();

            return employee;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee =
                await context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployeeRequest)
        {
            var employee = await context.Employees.FindAsync(id);

            if(employee is null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await context.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
