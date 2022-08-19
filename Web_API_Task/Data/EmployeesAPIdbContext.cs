using Microsoft.EntityFrameworkCore;
using Web_API_Task.Modules;

namespace Web_API_Task.Data
{
    public class EmployeesAPIdbContext : DbContext
    {
        public EmployeesAPIdbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }

    }
}
