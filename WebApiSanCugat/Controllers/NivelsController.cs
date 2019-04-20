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
    public class NivelsController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Nivels
        public IQueryable<Nivel> GetNivel()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Nivel;
        }

        // GET: api/Nivels/5
        [ResponseType(typeof(Nivel))]
        public IHttpActionResult GetNivel(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Nivel nivel = db.Nivel.Find(id);
            if (nivel == null)
            {
                return NotFound();
            }

            return Ok(nivel);
        }

        // PUT: api/Nivels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNivel(int id, Nivel nivel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nivel.id)
            {
                return BadRequest();
            }

            db.Entry(nivel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelExists(id))
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

        // POST: api/Nivels
        [ResponseType(typeof(Nivel))]
        public IHttpActionResult PostNivel(Nivel nivel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nivel.Add(nivel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nivel.id }, nivel);
        }

        // DELETE: api/Nivels/5
        [ResponseType(typeof(Nivel))]
        public IHttpActionResult DeleteNivel(int id)
        {
            Nivel nivel = db.Nivel.Find(id);
            if (nivel == null)
            {
                return NotFound();
            }

            db.Nivel.Remove(nivel);
            db.SaveChanges();

            return Ok(nivel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NivelExists(int id)
        {
            return db.Nivel.Count(e => e.id == id) > 0;
        }
    }
}