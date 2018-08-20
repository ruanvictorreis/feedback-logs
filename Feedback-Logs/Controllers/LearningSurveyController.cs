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
    public class LearningSurveyController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/LearningSurvey
        public IQueryable<LearningSurvey> GetLearningSurveyLogs()
        {
            return db.LearningSurveyLogs;
        }

        // GET: api/LearningSurvey/5
        [ResponseType(typeof(LearningSurvey))]
        public IHttpActionResult GetLearningSurvey(int id)
        {
            LearningSurvey learningSurvey = db.LearningSurveyLogs.Find(id);
            if (learningSurvey == null)
            {
                return NotFound();
            }

            return Ok(learningSurvey);
        }

        // PUT: api/LearningSurvey/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLearningSurvey(int id, LearningSurvey learningSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != learningSurvey.Id)
            {
                return BadRequest();
            }

            db.Entry(learningSurvey).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearningSurveyExists(id))
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

        // POST: api/LearningSurvey
        [ResponseType(typeof(LearningSurvey))]
        public IHttpActionResult PostLearningSurvey(LearningSurvey learningSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var register = learningSurvey.Register;
            var assignment = learningSurvey.Assignment;

            if (LearningSurveyExists(register, assignment))
            {
                return Ok(GetLearningSurveyBy(register, assignment));
            }

            db.LearningSurveyLogs.Add(learningSurvey);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = learningSurvey.Id }, learningSurvey);
        }

        // DELETE: api/LearningSurvey/5
        [ResponseType(typeof(LearningSurvey))]
        public IHttpActionResult DeleteLearningSurvey(int id)
        {
            LearningSurvey learningSurvey = db.LearningSurveyLogs.Find(id);
            if (learningSurvey == null)
            {
                return NotFound();
            }

            db.LearningSurveyLogs.Remove(learningSurvey);
            db.SaveChanges();

            return Ok(learningSurvey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private LearningSurvey GetLearningSurveyBy(String register, String assignment)
        {
            return db.LearningSurveyLogs.Where(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)).First();
        }

        private bool LearningSurveyExists(int id)
        {
            return db.LearningSurveyLogs.Count(e => e.Id == id) > 0;
        }

        private bool LearningSurveyExists(String register, String assignment)
        {
            return db.LearningSurveyLogs.Count(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)) > 0;
        }
    }
}