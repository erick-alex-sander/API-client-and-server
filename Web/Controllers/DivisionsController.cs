using API.Models;
using API.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DivisionsController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:55847/api/"),
        };

        // GET: Divisions
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Load()
        {
            IEnumerable<DivisionVM> divisions = null;
            var readTask = client.GetAsync("Divisions/");
            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                divisions = JsonConvert.DeserializeObject<List<DivisionVM>>(output);

            }

            return Json(divisions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadId(int id)
        {
            DivisionVM divisions = null;
            var readTask = client.GetAsync("Divisions/" + id);
            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                divisions = JsonConvert.DeserializeObject<DivisionVM>(output);

            }
            return Json(divisions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insert(int? id, Division division)
        {
            if (id != null)
            {
                var postTask = client.PutAsJsonAsync<Division>("divisions/" + id, division);
                postTask.Wait();
                var result = postTask.Result;
                return Json(new { success = result.IsSuccessStatusCode }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var postTask = client.PostAsJsonAsync<Division>("divisions", division);
                postTask.Wait();
                var result = postTask.Result;
                return Json(new { success = result.IsSuccessStatusCode }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Delete(int id)
        {
            var deleteTask = client.DeleteAsync("divisions/" + id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            return Json(new { success = result.IsSuccessStatusCode }, JsonRequestBehavior.AllowGet);
        }
    }
}