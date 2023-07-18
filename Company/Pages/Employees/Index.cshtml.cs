using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Company.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<Employee> EmployeeList = new List<Employee>();
		[BindProperty]
		public string search { get; set; }
		//public string searchText = "";
        
        public void OnGet()
        {
			try
            {
                String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM dbo.Employees";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();
                                employee.id = reader.GetInt32(0);
                                employee.name = reader.GetString(1);
                                employee.bDate = reader.GetString(2);
                                employee.address = reader.GetString(3);
                                employee.sex = reader.GetString(4);
                                employee.salary = "" + reader.GetInt32(5);
                                employee.departmentId ="" + reader.GetInt32(6);

                                EmployeeList.Add(employee);
                            }
                        }
                    }
                }

			} 
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class Employee
    {
        public int id;
        public string name;
        public string bDate;
        public string address;
        public string sex;
        public string salary;
        public string departmentId;
    }
}
