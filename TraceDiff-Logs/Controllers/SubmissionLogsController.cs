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
using TraceDiff_Logs.Models;

namespace TraceDiff_Logs.Controllers
{
    public class SubmissionLogsController : ApiController
    {
        private TraceDiff_LogsContext db = new TraceDiff_LogsContext();

        // GET: api/SubmissionLogs
        public IQueryable<SubmissionLog> GetSubmissionLogs()
        {
            return db.SubmissionLogs;
        }

        // GET: api/SubmissionLogs/5
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
        [ResponseType(typeof(SubmissionLog))]
        public IHttpActionResult PostSubmissionLog(SubmissionLog submissionLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            submissionLog.DateTime = DateTime.Now;

            var logsInteractionList = submissionLog.LogsInteractionList;

            if (logsInteractionList != null && logsInteractionList.Count > 0)
            {
                submissionLog.LogsInteractionStr = SerializeInteractionLogs(logsInteractionList);
            }

            db.SubmissionLogs.Add(submissionLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = submissionLog.Id }, submissionLog);
        }

        // DELETE: api/SubmissionLogs/5
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

        private String SerializeInteractionLogs(List<String> logsInteractionList)
        {
            String serializedLogs = "";

            foreach (String log in logsInteractionList)
            {
                serializedLogs = string.Concat(serializedLogs, string.Concat(log, ";"));
            }

            return serializedLogs.Remove(serializedLogs.Length - 1);
        }
    }
}