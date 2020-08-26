using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAcess;

namespace DemoWebAPI.Controllers
{
	public class EmployeesController : ApiController
	{
		public IEnumerable<Employee> Get(string gender="All")
		{
			using (EmployeeDBEntities entities = new EmployeeDBEntities())
			{
				return entities.Employees.ToList();
			}
		}

		public Employee Get(int id)
		{
			using (EmployeeDBEntities entities = new EmployeeDBEntities())
			{
				return entities.Employees.FirstOrDefault(e => e.ID == id);
			}
		}

		public HttpResponseMessage Post([FromBody]Employee employee)
		{
			try
			{
				using (EmployeeDBEntities entities = new EmployeeDBEntities())
				{
					entities.Employees.Add(employee);
					entities.SaveChanges();

					var message = Request.CreateResponse(HttpStatusCode.OK, employee);
					message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
					return message;
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

			}
		}

		public void Delete(int id)
		{
			using (EmployeeDBEntities entities = new EmployeeDBEntities())
			{
				entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
				entities.SaveChanges();
			}
		}

		public void Put(int id, [FromBody]Employee employee)
		{
			using (EmployeeDBEntities entities = new EmployeeDBEntities())
			{
				var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
				entity.FirstName = employee.FirstName;
				entity.LastName = employee.LastName;
				entity.Gender = employee.Gender;
				entity.Salary = employee.Salary;
				entities.SaveChanges();
			}
		}

	}
}
