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
    public class DeportesController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Deportes
        public IQueryable<Deportes> GetDeportes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Deportes;
        }

        // GET: api/Deportes/5
        [ResponseType(typeof(Deportes))]
        public IHttpActionResult GetDeportes(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Deportes deportes = db.Deportes.Find(id);
            if (deportes == null)
            {
                return NotFound();
            }

            return Ok(deportes);
        }

        // PUT: api/Deportes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeportes(int id, Deportes deportes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deportes.id)
            {
                return BadRequest();
            }

            db.Entry(deportes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeportesExists(id))
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

        // POST: api/Deportes
        [ResponseType(typeof(Deportes))]
        public IHttpActionResult PostDeportes(Deportes deportes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Deportes.Add(deportes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deportes.id }, deportes);
        }

        // DELETE: api/Deportes/5
        [ResponseType(typeof(Deportes))]
        public IHttpActionResult DeleteDeportes(int id)
        {
            Deportes deportes = db.Deportes.Find(id);
            if (deportes == null)
            {
                return NotFound();
            }

            db.Deportes.Remove(deportes);
            db.SaveChanges();

            return Ok(deportes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeportesExists(int id)
        {
            return db.Deportes.Count(e => e.id == id) > 0;
        }
    }
}