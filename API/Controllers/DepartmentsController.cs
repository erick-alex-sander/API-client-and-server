using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class DepartmentsController : ApiController
    {
        private readonly DepartmentsRepository repo = new DepartmentsRepository();
        // GET: api/Departments
        //[AcceptVerbs("GET")]
        public Task<IEnumerable<Department>> Get()
        {
            return repo.Get();
        }

        // GET: api/Departments/5
        public async Task<Department> Get(int id)
        {
            try
            {
                return await repo.Get(id);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        // POST: api/Departments
        public IHttpActionResult Post(Department department)
        {
            try
            {
                var create = repo.Create(department);
                return Ok("Successfully Added");
            }
            catch(Exception)
            {
                return BadRequest("Failed Insert Data");
            }
            
        }

        // PUT: api/Departments/5
        public IHttpActionResult Put(int id, Department department)
        {
            try
            {
                var update = repo.Update(id, department);
                return Ok("Successfully updated");
            }
            catch (Exception)
            {
                return BadRequest("Failed update data");
            }
        }

        // DELETE: api/Departments/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var delete = repo.Delete(id);
                return Ok("Successfully deleted");
            }
            catch (Exception)
            {
                return BadRequest("Failed delete Data");
            }
        }
    }
}
