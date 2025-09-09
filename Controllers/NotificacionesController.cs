using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    /// <summary>
    /// Daniel Alejandro Lopez condicion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class NotificacionesController : Controller
    {
        private readonly ModeloDatos.IModelos.INotificaciones _INotificaciones;

        public NotificacionesController(INotificaciones Notificaciones)
        {
            _INotificaciones = Notificaciones;
        }

        #region Consultar Beneficios por nombre o nombre de la convocatoria
        [HttpGet("Consultar_Notificaciones")]
        [ProducesResponseType<DataResponse<NotificacionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarNotificaciones(int? idPostulacion = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idPostulacion.ToString()) == null && idPostulacion == null)
                {
                    return BadRequest("Debe especificar la postulacion");
                }

                List<NotificacionesDTO> notificaciones = await _INotificaciones.ConsultarNotificaciones(idPostulacion);
                if (notificaciones != null)
                {

                    return Ok(new DataResponse<List<NotificacionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeNotificaciones.BusquedaExitosa,
                        Datos = notificaciones
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeNotificaciones.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear notificaciones
        [HttpPost("Ingresar_Notificaciones")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarNotificaciones([FromBody] NotificacionesDTO nuevo)
        {
            try
            {
                bool result = await _INotificaciones.IngresarNotificaciones(nuevo);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeNotificaciones.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeNotificaciones.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeNotificaciones.OcurrioError);
            }

        }
        #endregion

    }
}
