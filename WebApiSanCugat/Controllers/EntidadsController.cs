using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiSanCugat;
using WebApiSanCugat.Utilidades;

namespace WebApiSanCugat.Controllers
{
    public class EntidadsController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        [HttpGet]
        [Route("api/Entidads/nombre/{nombre}/password/{password}")]
        public IHttpActionResult GetEntidadByNombreAndPassword(String nombre, String password)
        {
            db.Configuration.LazyLoadingEnabled = false;
            IHttpActionResult result;

            Entidad entidad = (
                                from t in db.Entidad
                                where t.nombre.Equals(nombre) && t.contrasenya.Equals(password)
                                select t).FirstOrDefault();
            if (entidad == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(entidad);
            }


            return result;
        }
        // GET: api/Entidads
        public IQueryable<Entidad> GetEntidad()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Entidad;
        }

        // GET: api/Entidads/5
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult GetEntidad(int id)
        {

            db.Configuration.LazyLoadingEnabled = false;

            IHttpActionResult result;

            Entidad admin = (from a in db.Entidad
                             where a.id == id
                             select a).FirstOrDefault();
            if (admin == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(admin);
            }

            return result;
        }

        // PUT: api/Entidads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntidad(int id, Entidad entidad)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entidad.id)
            {
                return BadRequest();
            }

            db.Entry(entidad).State = EntityState.Modified;
            String mensaje = "";
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    SqlException sqlex = (SqlException)ex.InnerException.InnerException;
                    mensaje = Utilidad.MensajeError(sqlex);
                    return BadRequest(mensaje);
                }
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlex = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlex);
                return BadRequest(mensaje);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Entidads
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult PostEntidad(Entidad entidad)
        {
            db.Configuration.LazyLoadingEnabled = false;

            string mensaje = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entidad.Add(entidad);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlex = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlex);
                return BadRequest(mensaje);
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.ToString());

            }


            return CreatedAtRoute("DefaultApi", new { id = entidad.id }, entidad);
        }

        // DELETE: api/Entidads/5
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult DeleteEntidad(int id)
        {
            Entidad entidad = db.Entidad.Find(id);
            if (entidad == null)
            {
                return NotFound();
            }

            db.Entidad.Remove(entidad);
            string mensaje = "";
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlex = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlex);
                return BadRequest(mensaje);
            }

            return Ok(entidad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntidadExists(int id)
        {
            return db.Entidad.Count(e => e.id == id) > 0;
        }
    }
}