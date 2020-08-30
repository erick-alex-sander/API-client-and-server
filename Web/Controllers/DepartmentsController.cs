using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:55847/api/"),
        };
           
        // GET: Departments
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Load()
        {
            IEnumerable<Department> departments = null;
            var readTask = client.GetAsync("Departments/");
            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(output);
                
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadId(int id)
        {
            Department departments = null;
            var readTask = client.GetAsync("Departments/" + id);
            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<Department>(output);

            }
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insert(int? id, Department department)
        {
            if (id != null)
            {
                var postTask = client.PutAsJsonAsync<Department>("departments/" + id, department);
                postTask.Wait();
                var result = postTask.Result;
                return Json(new { success = result.IsSuccessStatusCode }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var postTask = client.PostAsJsonAsync<Department>("departments", department);
                postTask.Wait();
                var result = postTask.Result;
                return Json(new { success = result.IsSuccessStatusCode }, JsonRequestBehavior.AllowGet);
            }
            
        }
        
        public JsonResult Delete(int id)
        {
            var deleteTask = client.DeleteAsync("departments/" + id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            return Json(new { success = result.IsSuccessStatusCode }, JsonRequestBehavior.AllowGet);
        }
    }
}