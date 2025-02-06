using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Practica1.Models
{
    public class ElOlivoContext : DbContext
    {
        public ElOlivoContext(DbContextOptions<ElOlivoContext> options) : base(options) 
        {
        }

        public DbSet<Empleado> empleado {  get; set; } 
    }
}
