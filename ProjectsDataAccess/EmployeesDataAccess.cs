using ProjectEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsDataAccess
{
    public class EmployeesDataAccess
    {
        ProjectsDbContext _db;
        public EmployeesDataAccess(ProjectsDbContext db) => _db = db;
        
        public List<Employee> GetAllEmployees()
        {
            var items = _db.Employees.ToList(); 
            return items; 
        }
        public Employee GetEmployeeById(int id)
        {
            var item = _db.Employees.FirstOrDefault(c=>c.EmpId == id);
            return item; 
        }
        public void AddNewEmployee(Employee item)
        {
            _db.Employees.Add(item);
            _db.SaveChanges();
            return; 
        }
        
    }
}
