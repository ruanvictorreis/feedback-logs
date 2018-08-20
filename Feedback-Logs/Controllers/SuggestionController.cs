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
    public class SuggestionController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/Suggestion
        public IQueryable<Suggestion> GetSuggestions()
        {
            return db.SuggestionLogs;
        }

        // GET: api/Suggestion/5
        [ResponseType(typeof(Suggestion))]
        public IHttpActionResult GetSuggestion(int id)
        {
            Suggestion suggestion = db.SuggestionLogs.Find(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }

        // PUT: api/Suggestion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSuggestion(int id, Suggestion suggestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != suggestion.Id)
            {
                return BadRequest();
            }

            db.Entry(suggestion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuggestionExists(id))
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

        // POST: api/Suggestion
        [ResponseType(typeof(Suggestion))]
        public IHttpActionResult PostSuggestion(Suggestion suggestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var register = suggestion.Register;

            if (SuggestionExists(register))
            {
                return Ok(GetSuggestionBy(register));
            }

            db.SuggestionLogs.Add(suggestion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = suggestion.Id }, suggestion);
        }

        // DELETE: api/Suggestion/5
        [ResponseType(typeof(Suggestion))]
        public IHttpActionResult DeleteSuggestion(int id)
        {
            Suggestion suggestion = db.SuggestionLogs.Find(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            db.SuggestionLogs.Remove(suggestion);
            db.SaveChanges();

            return Ok(suggestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SuggestionExists(int id)
        {
            return db.SuggestionLogs.Count(e => e.Id == id) > 0;
        }

        private Suggestion GetSuggestionBy(String register)
        {
            return db.SuggestionLogs.Where(e => e.Register.Equals(register)).First();
        }

        private bool SuggestionExists(String register)
        {
            return db.SuggestionLogs.Count(e => e.Register.Equals(register)) > 0;
        }
    }
}