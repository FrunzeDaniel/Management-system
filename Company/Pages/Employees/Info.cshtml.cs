using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Company.Pages.Employees
{
    public class InfoModel : PageModel
    {
		public Employee employee = new Employee();
		public List<Dependent> Dependents = new List<Dependent>();
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
					String sql = "SELECT * FROM dbo.Dependents WHERE EmployeeId = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								Dependent dependet = new Dependent();
								dependet.id = reader.GetInt32(0);
								dependet.employeeId = "" + reader.GetInt32(1);
								dependet.dependetnname = reader.GetString(2);
								dependet.sex = reader.GetString(3);
								dependet.bDate = reader.GetString(4);
								dependet.relationship = reader.GetString(5);

								Dependents.Add(dependet);
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
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
							if (reader.Read())
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

	}

	public class Dependent
	{
		public int id;
		public string employeeId;
		public string dependetnname;
		public string sex;
		public string bDate;
		public string relationship;
	}
}

