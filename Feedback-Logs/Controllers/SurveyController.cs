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
using Feedback_Logs.Models;

namespace Feedback_Logs.Controllers
{
    public class SurveyController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/Survey
        public IQueryable<Survey> GetSurveyLogs()
        {
            return db.SurveyLogs;
        }

        // GET: api/Survey/5
        [ResponseType(typeof(Survey))]
        public IHttpActionResult GetSurvey(int id)
        {
            Survey survey = db.SurveyLogs.Find(id);
            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // PUT: api/Survey/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSurvey(int id, Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.Id)
            {
                return BadRequest();
            }

            db.Entry(survey).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: api/Survey
        [ResponseType(typeof(Survey))]
        public IHttpActionResult PostSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SurveyLogs.Add(survey);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = survey.Id }, survey);
        }

        // DELETE: api/Survey/5
        [ResponseType(typeof(Survey))]
        public IHttpActionResult DeleteSurvey(int id)
        {
            Survey survey = db.SurveyLogs.Find(id);
            if (survey == null)
            {
                return NotFound();
            }

            db.SurveyLogs.Remove(survey);
            db.SaveChanges();

            return Ok(survey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SurveyExists(int id)
        {
            return db.SurveyLogs.Count(e => e.Id == id) > 0;
        }
    }
}