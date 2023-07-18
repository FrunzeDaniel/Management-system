using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Company.Pages.Employees
{
    public class EditModel : PageModel
    {
        public Employee employee = new Employee();
        public string errorMessage = "";
        public string successmMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM dbo.Employees WHERE Id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
								employee.id = reader.GetInt32(0);
								employee.name = reader.GetString(1);
								employee.bDate = reader.GetString(2);
								employee.address = reader.GetString(3);
								employee.sex = reader.GetString(4);
								employee.salary = "" + reader.GetInt32(5);
								employee.departmentId = "" + reader.GetInt32(6);

							}
						}
                    }
                }

			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            employee.id = int.Parse(Request.Form["id"]);
			employee.name = Request.Form["name"];
			employee.bDate = Request.Form["bDate"];
			employee.address = Request.Form["address"];
			employee.sex = Request.Form["sex"];
			employee.salary = Request.Form["salary"];
			employee.departmentId = Request.Form["departmentId"];
			if (employee.name.Length == 0 || employee.bDate.Length == 0 || employee.address.Length == 0
				|| employee.sex.Length == 0 || employee.salary.Length == 0 || employee.departmentId.Length == 0)
			{
				errorMessage = "All the fields are required";
				return;
			}
            try
            {
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE dbo.Employees " +
                        "SET FullName=@name, Bdate=@bDate, Addres=@address, Sex=@sex, Salary=@salary, DepartmentId=@departmentId " +
                        "WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", employee.id);
						command.Parameters.AddWithValue("@name", employee.name);
						command.Parameters.AddWithValue("@bDate", employee.bDate);
						command.Parameters.AddWithValue("@address", employee.address);
						command.Parameters.AddWithValue("@sex", employee.sex);
						command.Parameters.AddWithValue("@salary", employee.salary);
						command.Parameters.AddWithValue("@departmentId", employee.departmentId);

						command.ExecuteNonQuery();
					}

				}
			}
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Employees/Index");
		}
    }
}
