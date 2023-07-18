using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Company.Pages.Projects.IndexModel;

namespace Company.Pages.Projects
{
    public class CreateModel : PageModel
    {
        public Project project = new Project();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            project.projectName = Request.Form["projectName"];
            project.projLocation = Request.Form["projLocation"];
            project.departmentId = Request.Form["departmentId"];

            if (project.projectName.Length == 0 || project.projLocation.Length == 0 || project.departmentId.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO dbo.Projects (ProjectName, projLocation, DepartmentId) " +
                        "VALUES (@projectName, @projLocation, @departmentId);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@projectname", project.projectName);
                        command.Parameters.AddWithValue("@projLocation", project.projLocation);
                        command.Parameters.AddWithValue("@departmentId", project.departmentId);

                        command.ExecuteNonQuery();
                    }
                }
			}
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            project.projectName = ""; project.projLocation = ""; project.departmentId = "";

            Response.Redirect("/Projects/Index");
        }
    }
}
