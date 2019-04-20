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
    public class EquipoesController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Equipoes
        public IQueryable<Equipo> GetEquipo()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Equipo;
        }

        // GET: api/Equipoes/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult GetEquipo(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        // PUT: api/Equipoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipo(int id, Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipo.id)
            {
                return BadRequest();
            }

            db.Entry(equipo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
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

        // POST: api/Equipoes
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult PostEquipo(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipo.Add(equipo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipo.id }, equipo);
        }

        // DELETE: api/Equipoes/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult DeleteEquipo(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            db.Equipo.Remove(equipo);
            db.SaveChanges();

            return Ok(equipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoExists(int id)
        {
            return db.Equipo.Count(e => e.id == id) > 0;
        }
    }
}