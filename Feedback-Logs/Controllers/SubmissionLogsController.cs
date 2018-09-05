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
    [RoutePrefix("api/submissionLogs")]
    public class SubmissionLogsController : ApiController
    {

        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/SubmissionLogs
        [Route("")]
        public IQueryable<SubmissionLog> GetSubmissionLogs()
        {
            return db.SubmissionLogs;
        }

        // GET: api/SubmissionLogs/register/111210442
        [Route("register/{register}")]
        public IQueryable<SubmissionLog> GetSubmissionLogsByRegister(String register)
        {
            return db.SubmissionLogs.Where(e => e.Register.Equals(register));
        }

        // GET: api/SubmissionLogs/5
        [Route("{id:int}")]
        [ResponseType(typeof(SubmissionLog))]
        public IHttpActionResult GetSubmissionLog(int id)
        {
            SubmissionLog submissionLog = db.SubmissionLogs.Find(id);
            if (submissionLog == null)
            {
                return NotFound();
            }

            return Ok(submissionLog);
        }

        // PUT: api/SubmissionLogs/5
        [Route("{id:int}"), HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubmissionLog(int id, SubmissionLog submissionLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != submissionLog.Id)
            {
                return BadRequest();
            }

            db.Entry(submissionLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmissionLogExists(id))
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

        // POST: api/SubmissionLogs
        [Route(""), HttpPost]
        [ResponseType(typeof(SubmissionLog))]
        public IHttpActionResult PostSubmissionLog(SubmissionLog submissionLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubmissionLogs.Add(submissionLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = submissionLog.Id }, submissionLog);
        }

        // DELETE: api/SubmissionLogs/5
        [Route("{id:int}"), HttpDelete]
        [ResponseType(typeof(SubmissionLog))]
        public IHttpActionResult DeleteSubmissionLog(int id)
        {
            SubmissionLog submissionLog = db.SubmissionLogs.Find(id);
            if (submissionLog == null)
            {
                return NotFound();
            }

            db.SubmissionLogs.Remove(submissionLog);
            db.SaveChanges();

            return Ok(submissionLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubmissionLogExists(int id)
        {
            return db.SubmissionLogs.Count(e => e.Id == id) > 0;
        }

        /**
        private String SerializeInteractionLogs(List<String> logsInteractionList)
        {
            String serializedLogs = "";

            foreach (String log in logsInteractionList)
            {
                serializedLogs = string.Concat(serializedLogs, string.Concat(log, ";"));
            }

            return serializedLogs.Remove(serializedLogs.Length - 1);
        }*/
    }
}