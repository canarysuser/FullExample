using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectEntities;
using ProjectsDataAccess;

namespace ProjectSolutionTests
{
    public class EmployeeDataAccessTest
    {
        EmployeesDataAccess dataAccess; 
        [SetUp] // called before every test
        public void Setup()
        {
            DbContextOptionsBuilder<ProjectsDbContext> bldr = new DbContextOptionsBuilder<ProjectsDbContext>();
            bldr.UseSqlServer(connectionString: @"server=(local);database=projecttestdb;integrated security=true;TrustServerCertificate=true;trusted_connection=true;");
            ProjectsDbContext context = new ProjectsDbContext(bldr.Options);
            dataAccess = new EmployeesDataAccess(context);
        }
        [Test]
        public void GetAllEmployees_Test()
        {
            //Arrange  -  arrange objects for the test 
            //Act  - perform the test, call the method to test 
            //Assert - that the test has returned the expected results. 

            //Arrange Stage
            var expectedCount = 0;

            //Act Stage 
            var employees = dataAccess.GetAllEmployees();
            var actualCount = employees.Count;

            //Assert 
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CreateEmployeeTest()
        {
            //Arrange 
            var expectedCount = dataAccess.GetAllEmployees().Count;
            var actualCount = 0;
            var item = new Employee { EmpName = "ABC", EmailId = "ABC",
                BirthDate = new System.DateTime(2000, 01, 01),
                JoinDate = new System.DateTime(2021, 01, 01),
                IsActive = true,
                Experience = 1,
                Salary = 25000
            };
            //Act 
            dataAccess.AddNewEmployee(item);

            actualCount = dataAccess.GetAllEmployees().Count;

            //Assert 
            Assert.AreEqual(expectedCount + 1, actualCount);
        }
    }
}