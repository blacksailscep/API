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
    public class SexoesController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Sexoes
        public IQueryable<Sexo> GetSexo()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Sexo;
        }

        // GET: api/Sexoes/5
        [ResponseType(typeof(Sexo))]
        public IHttpActionResult GetSexo(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return NotFound();
            }

            return Ok(sexo);
        }

        // PUT: api/Sexoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSexo(int id, Sexo sexo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sexo.id)
            {
                return BadRequest();
            }

            db.Entry(sexo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexoExists(id))
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

        // POST: api/Sexoes
        [ResponseType(typeof(Sexo))]
        public IHttpActionResult PostSexo(Sexo sexo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sexo.Add(sexo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sexo.id }, sexo);
        }

        // DELETE: api/Sexoes/5
        [ResponseType(typeof(Sexo))]
        public IHttpActionResult DeleteSexo(int id)
        {
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return NotFound();
            }

            db.Sexo.Remove(sexo);
            db.SaveChanges();

            return Ok(sexo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SexoExists(int id)
        {
            return db.Sexo.Count(e => e.id == id) > 0;
        }
    }
}