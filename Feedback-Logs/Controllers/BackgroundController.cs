using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Feedback_Logs.Models;

namespace Feedback_Logs.Controllers
{
    public class BackgroundController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/Background
        public IQueryable<Background> GetBackgroundLogs()
        {
            return db.BackgroundLogs;
        }

        // GET: api/Background/5
        [ResponseType(typeof(Background))]
        public IHttpActionResult GetBackground(int id)
        {
            Background background = db.BackgroundLogs.Find(id);
            if (background == null)
            {
                return NotFound();
            }

            return Ok(background);
        }

        // PUT: api/Background/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBackground(int id, Background background)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != background.Id)
            {
                return BadRequest();
            }

            db.Entry(background).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackgroundExists(id))
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

        // POST: api/Background
        [ResponseType(typeof(Background))]
        public IHttpActionResult PostBackground(Background background)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var register = background.Register;

            if (BackgroundExists(register))
            {
                return Ok(GetBackgroundBy(register));
            }

            db.BackgroundLogs.Add(background);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = background.Id }, background);
        }

        // DELETE: api/Background/5
        [ResponseType(typeof(Background))]
        public IHttpActionResult DeleteBackground(int id)
        {
            Background background = db.BackgroundLogs.Find(id);
            if (background == null)
            {
                return NotFound();
            }

            db.BackgroundLogs.Remove(background);
            db.SaveChanges();

            return Ok(background);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Background GetBackgroundBy(String register)
        {
            return db.BackgroundLogs.Where(e => e.Register.Equals(register)).First();
        }

        private bool BackgroundExists(int id)
        {
            return db.BackgroundLogs.Count(e => e.Id == id) > 0;
        }

        private bool BackgroundExists(String register)
        {
            return db.BackgroundLogs.Count(e => e.Register.Equals(register)) > 0;
        }
    }
}