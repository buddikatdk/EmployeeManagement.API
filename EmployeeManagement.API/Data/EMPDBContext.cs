using EmployeeManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Data
{
    public class EMPDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EMPDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
