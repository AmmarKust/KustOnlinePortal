using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult ViewSubjects()
        {
            IEnumerable<SubjectModel> sm;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Subjects").Result;
            sm = response.Content.ReadAsAsync<IEnumerable<SubjectModel>>().Result;
            return View(sm);
        }

        public ActionResult AddSubject(int? id)
        {
            if(id==0)
            {
                return View(new SubjectModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Subjects/" + id.ToString()).Result;
                
                return View(response.Content.ReadAsAsync<SubjectModel>().Result);
            }

           
        }
        [HttpPost]
         public ActionResult AddSubject(SubjectModel sm)
        {
            if (sm.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Subjects", sm).Result;
                TempData["SuccessMessage"] = "Saved Successfully";

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Subjects/" + sm.ID, sm).Result;
                TempData["SuccessMessage"] = "Update Successfully";

            }

            return RedirectToAction("ViewSubjects");
        }
        public ActionResult DeleteSubject(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Subjects/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("ViewSubjects");
        }
    }
}