using API.Models;
using API.Repositories;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class DivisionsController : ApiController
    {
        DivisionsRepository repo = new DivisionsRepository();
        // GET: api/Divisions
        public async Task<IEnumerable<DivisionVM>> Get()
        {
            try
            {
                var get = await repo.Get();
                
                var listVM = new List<DivisionVM>();
                foreach (var item in get)
                {
                    var getVM = new DivisionVM();
                    getVM.Id = item.Id;
                    getVM.Name = item.Name;
                    getVM.Department_Id = item.Department.Id;
                    getVM.Department_Name = item.Department.Name;
                    listVM.Add(getVM);
                }
                return listVM;
            }
            catch(Exception)
            {
                return null;
            }
        }

        // GET: api/Divisions/5
        public async Task<DivisionVM> GetAsync(int id)
        {
            try
            {
                var get = await repo.Get(id);
                var getVM = new DivisionVM();
                getVM.Id = get.Id;
                getVM.Name = get.Name;
                getVM.Department_Id = get.Department.Id;
                getVM.Department_Name = get.Department.Name;
                return getVM;
            }
            catch(Exception)
            {
                return null;
            }
        }

        // POST: api/Divisions
        public IHttpActionResult Post(Division division)
        {
            try
            {
                var insert = repo.Create(division);
                return Ok("Successfully inserted division");
            }
            catch (Exception)
            {
                return BadRequest("Failed insert division");
            }
            
        }

        // PUT: api/Divisions/5
        public IHttpActionResult Put(int id, Division division)
        {
            try
            {
                var update = repo.Update(id, division);
                return Ok("Successfully update division");
            }
            catch (Exception)
            {
                return BadRequest("Failed update division");
            }
        }

        // DELETE: api/Divisions/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var delete = repo.Delete(id);
                return Ok("Successfully delete division");
            }
            catch(Exception)
            {
                return BadRequest("Failed delete division");
            }
        }
    }
}
