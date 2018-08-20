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
    public class FixingSurveyController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/FixingSurvey
        public IQueryable<FixingSurvey> GetFixingSurveyLogs()
        {
            return db.FixingSurveyLogs;
        }

        // GET: api/FixingSurvey/5
        [ResponseType(typeof(FixingSurvey))]
        public IHttpActionResult GetFixingSurvey(int id)
        {
            FixingSurvey fixingSurvey = db.FixingSurveyLogs.Find(id);
            if (fixingSurvey == null)
            {
                return NotFound();
            }

            return Ok(fixingSurvey);
        }

        // PUT: api/FixingSurvey/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFixingSurvey(int id, FixingSurvey fixingSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fixingSurvey.Id)
            {
                return BadRequest();
            }

            db.Entry(fixingSurvey).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixingSurveyExists(id))
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

        // POST: api/FixingSurvey
        [ResponseType(typeof(FixingSurvey))]
        public IHttpActionResult PostFixingSurvey(FixingSurvey fixingSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var register = fixingSurvey.Register;
            var assignment = fixingSurvey.Assignment;

            if (FixingSurveyExists(register, assignment))
            {
                return Ok(GetFixingSurveyBy(register, assignment));
            }

            db.FixingSurveyLogs.Add(fixingSurvey);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fixingSurvey.Id }, fixingSurvey);
        }

        // DELETE: api/FixingSurvey/5
        [ResponseType(typeof(FixingSurvey))]
        public IHttpActionResult DeleteFixingSurvey(int id)
        {
            FixingSurvey fixingSurvey = db.FixingSurveyLogs.Find(id);
            if (fixingSurvey == null)
            {
                return NotFound();
            }

            db.FixingSurveyLogs.Remove(fixingSurvey);
            db.SaveChanges();

            return Ok(fixingSurvey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private FixingSurvey GetFixingSurveyBy(String register, String assignment)
        {
            return db.FixingSurveyLogs.Where(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)).First();
        }

        private bool FixingSurveyExists(int id)
        {
            return db.FixingSurveyLogs.Count(e => e.Id == id) > 0;
        }

        private bool FixingSurveyExists(String register, String assignment)
        {
            return db.FixingSurveyLogs.Count(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)) > 0;
        }
    }
}