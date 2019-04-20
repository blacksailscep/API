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
    public class CompeticionsController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Competicions
        public IQueryable<Competicion> GetCompeticion()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Competicion;
        }

        // GET: api/Competicions/5
        [ResponseType(typeof(Competicion))]
        public IHttpActionResult GetCompeticion(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Competicion competicion = db.Competicion.Find(id);
            if (competicion == null)
            {
                return NotFound();
            }

            return Ok(competicion);
        }

        // PUT: api/Competicions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompeticion(int id, Competicion competicion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competicion.id)
            {
                return BadRequest();
            }

            db.Entry(competicion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompeticionExists(id))
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

        // POST: api/Competicions
        [ResponseType(typeof(Competicion))]
        public IHttpActionResult PostCompeticion(Competicion competicion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competicion.Add(competicion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = competicion.id }, competicion);
        }

        // DELETE: api/Competicions/5
        [ResponseType(typeof(Competicion))]
        public IHttpActionResult DeleteCompeticion(int id)
        {
            Competicion competicion = db.Competicion.Find(id);
            if (competicion == null)
            {
                return NotFound();
            }

            db.Competicion.Remove(competicion);
            db.SaveChanges();

            return Ok(competicion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompeticionExists(int id)
        {
            return db.Competicion.Count(e => e.id == id) > 0;
        }
    }
}