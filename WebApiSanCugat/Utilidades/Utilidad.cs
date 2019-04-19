using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiSanCugat.Utilidades
{
    public class Utilidad
    {
        public static string MensajeError(SqlException sqlex)
        {
            string mensaje = "";
            switch (sqlex.Number)
            {
                case -1:
                    mensaje = "Error de conexion con el servidor";
                    break;
                case 547:
                    mensaje = "Tiene datos relacionados";
                    break;
                case 2601:
                    mensaje = "Datos duplicados";
                    break;
                case 2627:
                    mensaje = "Datos duplicados";
                    break;
                case 4060:
                    mensaje = "No se encuentra la base de datos";
                    break;
                case 18456:
                    mensaje = "Usuario o contraseña incorrectos";
                    break;
                default:
                    mensaje = sqlex.Number + " - " + sqlex.Message;
                    break;
            }

            return mensaje;
        }
    }
}