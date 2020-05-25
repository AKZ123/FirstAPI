using FirstAPI.Data;
using FirstAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FirstAPI.WebService.Controllers
{
    public class EmployeeV1Controller : ApiController
    {
        ////Part: 4
        //// GET: api/EmployeeV1
        //[HttpGet]    //P:10
        //public IEnumerable<Employee> LoadAllEmployee()
        ////public IEnumerable<Employee> Get()
        //{
        //    using (ApplicationDbContext context = new ApplicationDbContext())
        //    {
        //        return context.Employees.ToList();
        //    }
        //}

        //Part:11
        //public HttpResponseMessage Get(string gender = "All")
        public IHttpActionResult Get(string gender = "All")
        {
            using (ApplicationDbContext entitys = new ApplicationDbContext())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        //return Request.CreateResponse(HttpStatusCode.OK, entitys.Employees.ToList());
                        return Ok(entitys.Employees.ToList());
                    case "male":
                        //return Request.CreateResponse(HttpStatusCode.OK, entitys.Employees.Where(x => x.Gender == "male").ToList());
                        return Ok(entitys.Employees.Where(x => x.Gender == "male").ToList());
                    case "female":
                        //return Request.CreateResponse(HttpStatusCode.OK, entitys.Employees.Where(x => x.Gender == "female").ToList());
                        return Ok(entitys.Employees.Where(x => x.Gender == "female").ToList());
                    default:
                        //return Request.CreateResponse(HttpStatusCode.BadRequest, "Value for gender must be All, Male or Female." + gender + "is invalid.");
                        return BadRequest("Value for gender must be All, Male or Female." + gender + "is invalid.");
                }

            }
        }

        ////Part: 4
        //// GET: api/EmployeeV1/5
        //public Employee Get(int id)
        //{
        //    using (ApplicationDbContext context = new ApplicationDbContext())
        //    {
        //        //return context.Employees.Find(id);
        //        return context.Employees.FirstOrDefault(x => x.ID == id);
        //    }
        //}


        //Part: 7.2, 34
        // GET: api/EmployeeV1/5
        [Route(Name ="GetEmployeeById")]          //Part:33.1
        //public HttpResponseMessage Get(int id)  //Part: 7
        public IHttpActionResult Get(int id)      //Part: 34
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var entity = context.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    //return Request.CreateResponse(HttpStatusCode.OK, entity);  //p:7
                    return Ok(entity);        //P:34
                }
                else
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found"); //P:7
                    return Content(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found");                       //P:34
                }
            }
        }

        //Part: 7.1, 34, 12
        // POST: api/EmployeeV1
        //public void Post([FromBody]string value)
        //public HttpResponseMessage Post([FromBody]Employee employee) //P:7, 12
        public IHttpActionResult Post([FromBody]Employee employee)     //P:34
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();

                    //var message = Request.CreateResponse(HttpStatusCode.Created, employee);           //P:7
                    //message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    //return message;

                    //return Created(new Uri(Request.RequestUri + employee.ID.ToString()), employee);        //P:34
                    return Created(new Uri(Url.Link("GetEmployeeById", new { id = employee.ID})), employee); //P:33.2
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);  //P:7
                //return InternalServerError(ex);   //P:34
                return BadRequest(ex.ToString());
            }
        }


        //Part:9
        // PUT: api/EmployeeV1/5
        //public void Put(int id, [FromBody]string value)
        //public HttpResponseMessage Put(int id, [FromBody]Employee employee) //p:9
        public IHttpActionResult Put(int id, [FromBody]Employee employee)      //P:34
        {
            try
            {
                using (ApplicationDbContext entities = new ApplicationDbContext())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found to update");   //p:9
                        return Content(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found to update");                  //P:34 
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();
                        //return Request.CreateResponse(HttpStatusCode.OK, entity); //p:9
                        return Ok(entity);   //P:34
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ex);  //p:9
                return BadRequest(ex.ToString());   //P:34
            }
        }

        //P:8
        // DELETE: api/EmployeeV1/5
        //public void Delete(int id)
        //public HttpResponseMessage Delete(int id)      //p:8
        public IHttpActionResult Delete(int id)    //p:34
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var entity = context.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found to delete"); //P:8
                        return Content(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found to delete");                       //P:34 
                    }
                    else
                    {
                        context.Employees.Remove(entity);
                        context.SaveChanges();
                        //return Request.CreateResponse(HttpStatusCode.OK);   //p:8
                        return Ok();   //P:34
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);  //p:8
                return InternalServerError(ex); //p:34
            }
        }


    }
}
