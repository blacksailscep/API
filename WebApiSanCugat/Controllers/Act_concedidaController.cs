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
    public class Act_concedidaController : ApiController
    {
        private EntiEspaiEntities db = new EntiEspaiEntities();

        // GET: api/Act_concedida
        public IQueryable<Act_concedida> GetAct_concedida()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Act_concedida;
        }

        // GET: api/Act_concedida/5
        [ResponseType(typeof(Act_concedida))]
        public IHttpActionResult GetAct_concedida(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Act_concedida act_concedida = db.Act_concedida.Find(id);
            if (act_concedida == null)
            {
                return NotFound();
            }

            return Ok(act_concedida);
        }

        // PUT: api/Act_concedida/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAct_concedida(int id, Act_concedida act_concedida)
        {
            String mensaje = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act_concedida.id)
            {
                return BadRequest();
            }

            db.Entry(act_concedida).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Act_concedidaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    SqlException sqlex = (SqlException)ex.InnerException.InnerException;
                    mensaje = Utilidad.MensajeError(sqlex);
                    return BadRequest(mensaje);
                    ///throw;
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

        // POST: api/Act_concedida
        [ResponseType(typeof(Act_concedida))]
        public IHttpActionResult PostAct_concedida(Act_concedida act_concedida)
        {
            String mensaje = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Act_concedida.Add(act_concedida);
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

            return CreatedAtRoute("DefaultApi", new { id = act_concedida.id }, act_concedida);
        }

        // DELETE: api/Act_concedida/5
        [ResponseType(typeof(Act_concedida))]
        public IHttpActionResult DeleteAct_concedida(int id)
        {
            String mensaje = "";
            Act_concedida act_concedida = db.Act_concedida.Find(id);
            if (act_concedida == null)
            {
                return NotFound();
            }

            db.Act_concedida.Remove(act_concedida);
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

            return Ok(act_concedida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Act_concedidaExists(int id)
        {
            return db.Act_concedida.Count(e => e.id == id) > 0;
        }
    }
}