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
    public class RegisterLogsController : ApiController
    {
        private TraceDiff_LogsContext db = new TraceDiff_LogsContext();

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

            List<RegisterLog> allRegisterLogList = db.RegisterLogs.ToList();

            if (userRegisterLogList.Count == 0)
            {
                return FirstAssignmentConditionBalancer(allRegisterLogList);
            }
            else
            {
                return SecondAssignmentConditionBalancer(allRegisterLogList);
            }
        }

        private int FirstAssignmentConditionBalancer(List<RegisterLog> allRegisterLogList)
        {
            int countOne = 0;
            int countTwo = 0;

            foreach (RegisterLog register in allRegisterLogList)
            {
                if (register.Condition == 1)
                {
                    countOne++;
                }
                else
                {
                    countTwo++;
                }
            }

            return countOne > countTwo ? 2 : 1; ;
        }

        private int SecondAssignmentConditionBalancer(List<RegisterLog> allRegisterLogList)
        {
            int countThree = 0;
            int countFour = 0;

            foreach (RegisterLog register in allRegisterLogList)
            {
                if (register.Condition == 1)
                {
                    countThree++;
                }
                else
                {
                    countFour++;
                }
            }

            return countThree > countFour ? 4 : 3;
        }
    }
}