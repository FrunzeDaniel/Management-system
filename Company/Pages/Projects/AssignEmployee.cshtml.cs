using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Company.Pages.Projects.IndexModel;

namespace Company.Pages.Projects
{
    public class AssignEmployeeModel : PageModel
    {
        public WorkOn workOn = new WorkOn();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            workOn.employeeId = Request.Form["employeeId"];
            workOn.projectId = Request.Form["projectId"];
            workOn.hours = Request.Form["hours"];

            if(workOn.employeeId.Length == 0 || workOn.projectId.Length == 0|| workOn.hours.Length == 0) 
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
					string sql = "INSERT INTO dbo.Works_On (EmployeeId, ProjectId, Hours)" +
                        " SELECT e.Id, p.Id, @hours " +
                        "FROM dbo.Employees e " +
                        "JOIN dbo.Projects p " +
                        "ON e.FullName = @employeeName AND p.ProjectName = @projectName;";
					using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@hours", workOn.hours);
                        command.Parameters.AddWithValue("@employeeName", workOn.employeeId);
                        command.Parameters.AddWithValue("@projectName", workOn.projectId);
                        command.ExecuteNonQuery();
                    }

				}
			}
			catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
			workOn.hours = ""; workOn.employeeId = ""; workOn.projectId = "";

			Response.Redirect("/Projects/Index");
		}

    }
}
