using Microsoft.EntityFrameworkCore;
using ProjectEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsDataAccess
{
    public class ProjectsDbContext : DbContext
    {
        public ProjectsDbContext(DbContextOptions<ProjectsDbContext> ctx) : base(ctx) { }
        public DbSet<Employee> Employees { get; set; }  //Employees - TableName, Employee- Entity/Row
    }
}
