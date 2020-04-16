using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Application.Models;

namespace Application.Controllers
{
    public class DateSheetController : Controller
    {
        // GET: DateSheet
        public ActionResult Index()
        {
            
            return View();
        }


         public ActionResult ViewDateSheet()
        {
            IEnumerable<DSModel> list;


            HttpResponseMessage httpResponse = GlobalVariables.WebApiClient.GetAsync("DateSheets").Result;
            list = httpResponse.Content.ReadAsAsync<IEnumerable<DSModel>>().Result;

            return View(list);


          
        }
        


        public ActionResult AddOrEditDateSheet(int id = 0)
        {
            if (id == 0)
            {
                return View(new DSModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("DateSheets/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<DSModel>().Result);
            }




        }

        [HttpPost]
        public ActionResult AddOrEditDateSheet(DSModel ds)
        {
            if (ds.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("DateSheets", ds).Result;
                TempData["SuccessMessage"] = "Saved Successfully";

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("DateSheets/" + ds.ID, ds).Result;
                TempData["SuccessMessage"] = "Update Successfully";

            }

            return RedirectToAction("ViewDateSheet");
        }
        public ActionResult DeleteDateSheet(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("DateSheets/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("ViewDateSheet");
        }




    

      
    }
}
 