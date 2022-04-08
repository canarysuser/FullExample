using Microsoft.Data.SqlClient;
using ProjectEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsDataAccess
{
    public class ProjectsDAL : BaseDataAccess
    {
        public ProjectsDAL(string connectionString) : base(connectionString) { }

        public List<Project> GetAllProjects()
        {
            string sql = " SELECT ProjectId, ProjectName Description, StartDate, EndDate, ClientName " +
                " FROM Projects "; 
            List<Project> list = new List<Project>();
            try
            {
                OpenConnection();
                var reader = ExecuteReader(sql, System.Data.CommandType.Text);
                while (reader.Read())
                {
                    Project item = new() 
                    { 
                        ProjectId = reader.GetInt32(0),
                        ProjectName = reader.GetString(1),
                        Description = reader.GetString(2),
                        StartDate = reader.GetDateTime(3),
                        EndDate = reader.GetDateTime(4),
                        ClientName = reader.GetString(5),
                    };
                    list.Add(item);
                }
                if(!reader.IsClosed) reader.Close();

                //Retrieve all the child details for the project 
                sql = "SELECT PM.MemberId, PM.ProjectId, E.EmployeeId, e.EmpName, PM.JoinedOn, PM.IsActive " +
                    " FROM ProjectMembers PM INNER JOIN Employees E ON PM.EmployeeId=E.EmpId " +
                    " WHERE PM.ProjectId = @Id";

                list.ForEach(item =>
                {
                    var reader2 = ExecuteReader(
                            sql,
                            CommandType.Text,
                            new[] { new SqlParameter("@Id", item.ProjectId) });
                    while (reader2.Read())
                    {
                        ProjectMember pm = new ProjectMember
                        {
                            MemberId = reader2.GetInt32(0),
                            ProjectId = reader2.GetInt32(1),
                            EmployeeId = reader2.GetInt32(2),
                            JoinedOn = reader2.GetDateTime(4),
                            IsActive = reader2.GetBoolean(5),
                        };
                        item.Members.Add(pm);
                        if (!reader2.IsClosed) reader2.Close();
                    }
                });

            } catch (Exception ex) { throw;  }
            finally { CloseConnection(); }
            return list;
        }
      /*  public List<Project> GetAllProjectsUsingContext()
        {
            var db = new ProjectsDbContext();
            var items = db.Projects.Include(c => c.Members).ToList();
        }*/


    }
}
