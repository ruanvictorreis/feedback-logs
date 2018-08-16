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
    public class AgreementController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/Agreement
        public IQueryable<AgreementRegister> GetAgreementLogs()
        {
            return db.AgreementLogs;
        }

        // GET: api/Agreement/5
        [ResponseType(typeof(AgreementRegister))]
        public IHttpActionResult GetAgreementRegister(int id)
        {
            AgreementRegister agreementRegister = db.AgreementLogs.Find(id);
            if (agreementRegister == null)
            {
                return NotFound();
            }

            return Ok(agreementRegister);
        }

        // PUT: api/Agreement/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgreementRegister(int id, AgreementRegister agreementRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agreementRegister.Id)
            {
                return BadRequest();
            }

            db.Entry(agreementRegister).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgreementRegisterExists(id))
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

        // POST: api/Agreement
        [ResponseType(typeof(AgreementRegister))]
        public IHttpActionResult PostAgreementRegister(AgreementRegister agreementRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var register = agreementRegister.Register;
            var email = agreementRegister.Email;

            if (AgreementRegisterExists(register, email))
            {
                return Ok(GetAgreementBy(register, email));
            }

            db.AgreementLogs.Add(agreementRegister);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agreementRegister.Id }, agreementRegister);
        }

        // DELETE: api/Agreement/5
        [ResponseType(typeof(AgreementRegister))]
        public IHttpActionResult DeleteAgreementRegister(int id)
        {
            AgreementRegister agreementRegister = db.AgreementLogs.Find(id);
            if (agreementRegister == null)
            {
                return NotFound();
            }

            db.AgreementLogs.Remove(agreementRegister);
            db.SaveChanges();

            return Ok(agreementRegister);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private AgreementRegister GetAgreementBy(String register, String email)
        {
            return db.AgreementLogs.Where(e => e.Register.Equals(register) &&
                e.Email.Equals(email)).First();
        }

        private bool AgreementRegisterExists(int id)
        {
            return db.AgreementLogs.Count(e => e.Id == id) > 0;
        }

        private bool AgreementRegisterExists(String register, String email)
        {
            return db.AgreementLogs.Count(e => e.Register.Equals(register) &&
                e.Email.Equals(email)) > 0;
        }
    }
}