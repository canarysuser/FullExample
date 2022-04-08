using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectsDataAccess;
using ProjectEntities;

namespace ProjectSolutionTests
{
    public class ProjectsDataAccessTest
    {
        string connectionString = @"server=(local);database=projecttestdb;integrated security=true;TrustServerCertificate=true;trusted_connection=true;";

        [Test]
        public void ProjectsDALGetAllProjects_Test()
        {
            ProjectsDAL dal = new ProjectsDAL(connectionString);

            var expectedCount = dal.GetAllProjects().Count();
            var actualCount = dal.GetAllProjects().Count();
            Assert.AreEqual(expectedCount, actualCount);

        }

    }
}
