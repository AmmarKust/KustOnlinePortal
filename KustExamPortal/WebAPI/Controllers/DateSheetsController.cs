using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DateSheetsController : ApiController
    {
        private KustOnlinePortalEntities db = new KustOnlinePortalEntities();

        // GET: api/DateSheets
        public IQueryable<DateSheet> GetDateSheets()
        {
            return db.DateSheets;
        }

        // GET: api/DateSheets/5
        [ResponseType(typeof(DateSheet))]
        public IHttpActionResult GetDateSheet(int id)
        {
            DateSheet dateSheet = db.DateSheets.Find(id);
            if (dateSheet == null)
            {
                return NotFound();
            }

            return Ok(dateSheet);
        }

        // PUT: api/DateSheets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDateSheet(int id, DateSheet dateSheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dateSheet.ID)
            {
                return BadRequest();
            }

            db.Entry(dateSheet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateSheetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DateSheets
        [ResponseType(typeof(DateSheet))]
        public IHttpActionResult PostDateSheet(DateSheet dateSheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DateSheets.Add(dateSheet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dateSheet.ID }, dateSheet);
        }

        // DELETE: api/DateSheets/5
        [ResponseType(typeof(DateSheet))]
        public IHttpActionResult DeleteDateSheet(int id)
        {
            DateSheet dateSheet = db.DateSheets.Find(id);
            if (dateSheet == null)
            {
                return NotFound();
            }

            db.DateSheets.Remove(dateSheet);
            db.SaveChanges();

            return Ok(dateSheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DateSheetExists(int id)
        {
            return db.DateSheets.Count(e => e.ID == id) > 0;
        }
    }
}