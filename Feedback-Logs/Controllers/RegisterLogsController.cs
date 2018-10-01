using System;
using System.Collections.Generic;
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
    public class RegisterLogsController : ApiController
    {
        private Feedback_LogsContext db = new Feedback_LogsContext();

        // GET: api/RegisterLogs
        public IQueryable<RegisterLog> GetRegisterLogs()
        {
            return db.RegisterLogs;
        }

        // GET: api/RegisterLogs/5
        [ResponseType(typeof(RegisterLog))]
        public IHttpActionResult GetRegisterLog(int id)
        {
            RegisterLog registerLog = db.RegisterLogs.Find(id);
            if (registerLog == null)
            {
                return NotFound();
            }

            return Ok(registerLog);
        }

        // PUT: api/RegisterLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegisterLog(int id, RegisterLog registerLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registerLog.Id)
            {
                return BadRequest();
            }

            db.Entry(registerLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterLogExists(registerLog.Register, registerLog.Assignment))
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

        // POST: api/RegisterLogs
        [ResponseType(typeof(RegisterLog))]
        public IHttpActionResult PostRegisterLog(RegisterLog registerLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var register = registerLog.Register;
            var assignment = registerLog.Assignment;

            if (RegisterLogExists(register, assignment))
            {
                RegisterLog oldRegister = GetRegisterLogBy(register, assignment);
                oldRegister.AgreementRequired = IsAgreementRequired(register);
                oldRegister.BackgroundRequired = IsBackgroundRequired(register);
                return Ok(oldRegister);
            }

            //registerLog.Condition = Bias(assignment);
            registerLog.Condition = AssignmentConditionBalancer(register);

            registerLog.AgreementRequired = IsAgreementRequired(register);
            registerLog.BackgroundRequired = IsBackgroundRequired(register);
            registerLog.Counter = GetCounter(register);

            db.RegisterLogs.Add(registerLog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RegisterLogExists(registerLog.Register, registerLog.Assignment))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = registerLog.Register }, registerLog);
        }

        // DELETE: api/RegisterLogs/5
        [ResponseType(typeof(RegisterLog))]
        public IHttpActionResult DeleteRegisterLog(int id)
        {
            RegisterLog registerLog = db.RegisterLogs.Find(id);
            if (registerLog == null)
            {
                return NotFound();
            }

            db.RegisterLogs.Remove(registerLog);
            db.SaveChanges();

            return Ok(registerLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsAgreementRequired(String register)
        {
            return db.AgreementLogs.Count(e => e.Register.Equals(register)) == 0;
        }

        private bool IsBackgroundRequired(String register)
        {
            return db.BackgroundLogs.Count(e => e.Register.Equals(register)) == 0;
        }


        private bool RegisterLogExists(String register, String assignment)
        {
            return db.RegisterLogs.Count(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)) > 0;
        }

        private int GetCounter(String register)
        {
            return db.RegisterLogs.Count(e => e.Register.Equals(register)) + 1;
        }

        private RegisterLog GetRegisterLogBy(String register, String assignment)
        {
            return db.RegisterLogs.Where(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)).First();
        }

        private int AssignmentConditionBalancer(String register)
        {
            List<RegisterLog> userRegisterLogList = db.RegisterLogs.Where(
                e => e.Register.Equals(register)).ToList();

            return FindBestCondition(userRegisterLogList);
        }

        private int FindBestCondition(List<RegisterLog> userRegisterLogList)
        {
            List<int> allConditions = new List<int> { 1, 2, 3 };

            foreach (RegisterLog register in userRegisterLogList)
            {
                allConditions.Remove(register.Condition);
            }

            Random rnd = new Random();
            return allConditions[rnd.Next(allConditions.Count)];
        }

        private int Bias(String assignment)
        {
            if (assignment.Equals("sum_of_squares"))
            {
                return 1;
            }

            if (assignment.Equals("is_prime_number"))
            {
                return 2;
            }

            if (assignment.Equals("fibonacci"))
            {
                return 3;
            }

            return 0;
        }
    }
}