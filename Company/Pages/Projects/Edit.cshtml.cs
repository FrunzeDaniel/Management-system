using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Company.Pages.Projects.IndexModel;

namespace Company.Pages.Projects
{
    public class EditModel : PageModel
    {
        public Project project = new Project();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
				string id = Request.Query["id"];
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM dbo.Projects WHERE Id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								project.id = reader.GetInt32(0);
								project.projectName = reader.GetString(1);
								project.projLocation = reader.GetString(2);
								project.departmentId = "" + reader.GetInt32(3);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				errorMessage = ex.Message;
			}
            
		}

		public void OnPost()
		{
			project.id = int.Parse(Request.Form["id"]);
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
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE dbo.Projects " +
						"SET ProjectName=@projectName, projLocation=@projLocation, DepartmentId=@departmentId " +
						"WHERE Id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", project.id);
						command.Parameters.AddWithValue("@projectName", project.projectName);
						command.Parameters.AddWithValue("@projLocation", project.projLocation);
						command.Parameters.AddWithValue("@departmentId", project.departmentId);

						command.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Projects/Index");
		}
	}
}
