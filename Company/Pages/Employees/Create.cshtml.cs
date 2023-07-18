using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Company.Pages.Employees
{
    public class CreateModel : PageModel
    {
        public Employee employee = new Employee();
        public string errorMessage = "";
        public string succesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            employee.name = Request.Form["name"];
            employee.bDate = Request.Form["bDate"];
            employee.address = Request.Form["address"];
            employee.sex = Request.Form["sex"];
            employee.salary = Request.Form["salary"];
            employee.departmentId = Request.Form["departmentId"];
            
            if(employee.name.Length == 0 || employee.bDate.Length == 0 || employee.address.Length == 0
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
                    String sql = "INSERT INTO dbo.Employees" +
						"(FullName, Bdate, Addres, Sex, Salary, DepartmentId) VALUES" +
                        "(@name, @bDate, @address, @sex, @salary, @departmentId);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            employee.name = ""; employee.bDate = ""; employee.address = ""; employee.sex = "";
            employee.salary = ""; employee.departmentId = "";
            succesMessage = "New Client Added Correctly";

            Response.Redirect("/Employees/Index");
        }
    }
}
