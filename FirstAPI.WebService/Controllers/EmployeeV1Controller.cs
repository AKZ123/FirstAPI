using FirstAPI.Data;
using FirstAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstAPI.WebService.Controllers
{
    public class EmployeeV1Controller : ApiController
    {
        // GET: api/EmployeeV1
        public IEnumerable<Employee> Get()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Employees.ToList();
            }
        }

        // GET: api/EmployeeV1/5
        public Employee Get(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Employees.Find(id);
            }
        }

        // POST: api/EmployeeV1
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/EmployeeV1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EmployeeV1/5
        public void Delete(int id)
        {
        }
    }
}
