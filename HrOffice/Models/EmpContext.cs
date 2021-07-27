using Microsoft.EntityFrameworkCore;

namespace HrOffice.Models
{
    public class EmpContext : DbContext
    {
        public EmpContext(DbContextOptions<EmpContext> options) : base(options)
        {

        }

        public DbSet<Emp> Employee { get; set; }
    }
}
