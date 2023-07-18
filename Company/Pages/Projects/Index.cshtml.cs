using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Company.Pages.Employees;

namespace Company.Pages.Projects
{
    public class IndexModel : PageModel
    {
        public List<Project> Projects = new List<Project>();
        public List<WorkOn> WorksOn = new List<WorkOn>();
        public void OnGet()
        {
            try
            {
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM dbo.Projects;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Project project = new Project();
                                project.id = reader.GetInt32(0);
                                project.projectName = reader.GetString(1);
                                project.projLocation = reader.GetString(2);
                                project.departmentId = "" + reader.GetInt32(3);

                                Projects.Add(project);
                            }
                        }
                    }
                }
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT FullName, ProjectName, Hours, dbo.Works_On.Id " +
                        "FROM dbo.Employees, dbo.Projects, dbo.Works_On " +
                        "WHERE dbo.Employees.Id = dbo.Works_On.EmployeeId AND dbo.Projects.Id = dbo.Works_On.ProjectId;";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
                        connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								WorkOn workOn = new WorkOn();
								workOn.employeeId = reader.GetString(0);
								workOn.projectId = reader.GetString(1);
								workOn.hours = "" + reader.GetInt32(2);
								workOn.id = reader.GetInt32(3);

								WorksOn.Add(workOn);
							}
						}
					}
				}
				
			}
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public class Project
        {
            public int id;
            public string projectName;
            public string projLocation;
            public string departmentId;
        }

        public class WorkOn
        {
            public int id;
            public string employeeId;
            public string projectId;
            public string hours;
        }
    }
}
