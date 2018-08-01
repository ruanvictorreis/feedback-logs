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
                return Ok(GetRegisterLogBy(register, assignment));
            }

            registerLog.Condition = AssignmentConditionBalancer(register);
            registerLog.Permission = true;

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

        private bool RegisterLogExists(String register, String assignment)
        {
            return db.RegisterLogs.Count(e => e.Register.Equals(register) &&
                e.Assignment.Equals(assignment)) > 0;
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
            int countOne = 0;
            int countTwo = 0;
            int countThree = 0;

            List<RegisterLog> allRegisterLogList = db.RegisterLogs.ToList();

            foreach (RegisterLog register in allRegisterLogList)
            {
                if (register.Condition == 1)
                {
                    countOne++;
                }

                if (register.Condition == 2)
                {
                    countTwo++;
                }

                if (register.Condition == 3)
                {
                    countThree++;
                }
            }

            Dictionary<int, int> balancer = new Dictionary<int, int>();
            balancer.Add(1, countOne);
            balancer.Add(2, countTwo);
            balancer.Add(3, countThree);

            foreach (RegisterLog register in userRegisterLogList)
            {
                balancer.Remove(register.Condition);
            }

            return balancer.FirstOrDefault(x => x.Value == balancer.Values.Min()).Key;
        }
    }
}