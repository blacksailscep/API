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
    public class Categoria_edadController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Categoria_edad
        public IQueryable<Categoria_edad> GetCategoria_edad()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Categoria_edad;
        }

        // GET: api/Categoria_edad/5
        [ResponseType(typeof(Categoria_edad))]
        public IHttpActionResult GetCategoria_edad(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Categoria_edad categoria_edad = db.Categoria_edad.Find(id);
            if (categoria_edad == null)
            {
                return NotFound();
            }

            return Ok(categoria_edad);
        }

        // PUT: api/Categoria_edad/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoria_edad(int id, Categoria_edad categoria_edad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria_edad.id)
            {
                return BadRequest();
            }

            db.Entry(categoria_edad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Categoria_edadExists(id))
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

        // POST: api/Categoria_edad
        [ResponseType(typeof(Categoria_edad))]
        public IHttpActionResult PostCategoria_edad(Categoria_edad categoria_edad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categoria_edad.Add(categoria_edad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria_edad.id }, categoria_edad);
        }

        // DELETE: api/Categoria_edad/5
        [ResponseType(typeof(Categoria_edad))]
        public IHttpActionResult DeleteCategoria_edad(int id)
        {
            Categoria_edad categoria_edad = db.Categoria_edad.Find(id);
            if (categoria_edad == null)
            {
                return NotFound();
            }

            db.Categoria_edad.Remove(categoria_edad);
            db.SaveChanges();

            return Ok(categoria_edad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Categoria_edadExists(int id)
        {
            return db.Categoria_edad.Count(e => e.id == id) > 0;
        }
    }
}