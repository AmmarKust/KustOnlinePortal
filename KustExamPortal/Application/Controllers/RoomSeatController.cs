using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Models;
using System.Net.Http;

namespace Application.Controllers
{
    public class RoomSeatController : Controller
    {
        // GET: RoomSeat
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult ViewRoom()
        {
            IEnumerable<RoomsModel> rm;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Rooms").Result;
            rm = response.Content.ReadAsAsync<IEnumerable<RoomsModel>>().Result;
            return View(rm);
        }
        public ActionResult AddRoom(int id=0)
        {
          
            if (id==0)
            {
                return View(new RoomsModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Rooms/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<RoomsModel>().Result);
            }
            
        }
        [HttpPost]
        public ActionResult AddRoom(RoomsModel rm)
        {
           if (rm.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Rooms", rm).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
              
           }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Rooms/"+ rm.ID,rm).Result;
                TempData["SuccessMessage"] = "Update Successfully";
               
            }

            return RedirectToAction("ViewRoom");
        }
        public ActionResult DeleteRoom(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Rooms/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("ViewRoom");
        }

       

        public ActionResult ViewSeats()
        {
            IEnumerable<SeatModel> sm;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Seats").Result;
            sm = response.Content.ReadAsAsync<IEnumerable<SeatModel>>().Result;
            return View(sm);
        }

        public ActionResult AddSeat(int id=0)
        {
            if (id == 0)
            {
                return View(new SeatModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Seats/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<SeatModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddSeat(SeatModel sm)
        {
            if (sm.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Seats", sm).Result;
                TempData["SuccessMessage"] = "Saved Successfully";

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Seats/" + sm.ID, sm).Result;
                TempData["SuccessMessage"] = "Update Successfully";

            }

            return RedirectToAction("ViewSeats");
        }
        public ActionResult DeleteSeat(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Seats/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("ViewSeats");
        }
    }
}