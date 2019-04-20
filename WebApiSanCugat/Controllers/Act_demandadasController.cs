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
using WebApiSanCugat;

namespace WebApiSanCugat.Controllers
{
    public class Act_demandadasController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Act_demandadas
        public IQueryable<Act_demandadas> GetAct_demandadas()
        {
            return db.Act_demandadas;
        }

        // GET: api/Act_demandadas/5
        [ResponseType(typeof(Act_demandadas))]
        public IHttpActionResult GetAct_demandadas(int id)
        {
            Act_demandadas act_demandadas = db.Act_demandadas.Find(id);
            if (act_demandadas == null)
            {
                return NotFound();
            }

            return Ok(act_demandadas);
        }

        // PUT: api/Act_demandadas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAct_demandadas(int id, Act_demandadas act_demandadas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act_demandadas.id)
            {
                return BadRequest();
            }

            db.Entry(act_demandadas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Act_demandadasExists(id))
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

        // POST: api/Act_demandadas
        [ResponseType(typeof(Act_demandadas))]
        public IHttpActionResult PostAct_demandadas(Act_demandadas act_demandadas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Act_demandadas.Add(act_demandadas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = act_demandadas.id }, act_demandadas);
        }

        // DELETE: api/Act_demandadas/5
        [ResponseType(typeof(Act_demandadas))]
        public IHttpActionResult DeleteAct_demandadas(int id)
        {
            Act_demandadas act_demandadas = db.Act_demandadas.Find(id);
            if (act_demandadas == null)
            {
                return NotFound();
            }

            db.Act_demandadas.Remove(act_demandadas);
            db.SaveChanges();

            return Ok(act_demandadas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Act_demandadasExists(int id)
        {
            return db.Act_demandadas.Count(e => e.id == id) > 0;
        }
    }
}