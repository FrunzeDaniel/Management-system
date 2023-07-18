using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Company.Pages.Departments
{
    public class IndexModel : PageModel
    {
        public List<Department> Departments = new List<Department>();
        public void OnGet()
        {
            try
            {
				String connectionString = "Data Source=SITNICDUMITRU;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Departments.*, COUNT(Employees.DepartmentId) as EmployeeCount, " +
                        "SUM(CASE WHEN Employees.Sex = 'M' THEN 1 ELSE 0 END) as MaleCount, " +
                        "SUM(CASE WHEN Employees.Sex = 'F' THEN 1 ELSE 0 END) as FemaleCount, " +
                        "AVG(Employees.Salary) as AverageSalary " +
                        "FROM Departments " +
                        "LEFT JOIN Employees ON Departments.Id = Employees.DepartmentId " +
                        "GROUP BY Departments.Id, Departments.DepartmentName, Departments.DeptLocation, Departments.DepartmentPhoneNUmber, Departments.DirectorName;";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
								Department department = new Department();
								department.id = reader.GetInt32(0);
								department.departmentName = reader.GetString(1);
								department.departmentLocation = reader.GetString(2);
								department.phoneNumber = "" + reader.GetInt32(3);
								department.director = reader.GetString(4);
								department.totalEmployees = "" + reader.GetInt32(5);
								department.maleEmployees = "" + reader.GetInt32(6);
								department.femaleEmployees = "" + reader.GetInt32(7);
								department.averageSalary = "" + reader.GetInt32(8);
								Departments.Add(department);
							}                           
                        }
                    }
                }
			}
			catch (Exception ex)
            {
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}

        public class Department
        {
            public int id;
            public string departmentName;
            public string departmentLocation;
            public string phoneNumber;
            public string director;
            public string totalEmployees;
            public string femaleEmployees;
            public string maleEmployees;
            public string averageSalary;
        }
    }
}
