using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;
using ModeloDatos.Modelos;
using ModeloDatos.Utilidades;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apis.Controllers
{
    /// <summary>
    /// Daniel Alejandro Lopez condicion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostulacionesController : Controller
    {

        private readonly ModeloDatos.IModelos.IPostulaciones _IPostulaciones;

        public PostulacionesController(IPostulaciones Postulaciones)
        {
            _IPostulaciones = Postulaciones;
        }

        #region Consultar Postulacion
        [HttpGet("Consultar_Postulacion")]
        [ProducesResponseType<DataResponse<List<PostulacionDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostulacion()
        {
            try
            {
                List<PostulacionDTO> postulacion = await _IPostulaciones.ConsultarPostulacion();
                if (postulacion != null)
                {
                    return Ok(new DataResponse<List<PostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaExitosa,
                        Datos = postulacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar postulacion por id convocatoria
        [HttpGet("Consultar_PostulacionConvocatoria")]
        [ProducesResponseType<DataResponse<PostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostulacionConvocatoria(int? IdConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(IdConvocatoria.ToString()) == null || IdConvocatoria==null)
                {
                    return BadRequest("Debe especificar el id de la convocatoria.");
                }

                List<PostulacionDTO> postulacion = await _IPostulaciones.ConsultarPostulacionConvocatoria(IdConvocatoria);
                if (postulacion != null)
                {

                    return Ok(new DataResponse<List<PostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaExitosa,
                        Datos = postulacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar postulacion por id
        [HttpGet("Consultar_PostulacionEspecifico")]
        [ProducesResponseType<DataResponse<PostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostulacionEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id == null)
                {
                    return BadRequest("Debe especificar la postulacion");
                }

                List<PostulacionDTO> postulacion = await _IPostulaciones.ConsultarPostulacionEspecifico(id);
                if (postulacion != null)
                {

                    return Ok(new DataResponse<List<PostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaExitosa,
                        Datos = postulacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar postulacion por filtros
        [HttpGet("Consultar_PostulacionGeneral")]
        [ProducesResponseType<DataResponse<PostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostulacionGeneral(string? Documento = null, string? NombreCompleto = null,
            DateOnly? FechaInicio = null, DateOnly? FechaFinal = null, int? IdModalidad = null)
        {
            try
            {

               

                List<PostulacionDTO> postulacion = await _IPostulaciones.ConsultarPostulacionGeneral(Documento, NombreCompleto, FechaInicio,
                    FechaFinal, IdModalidad);
                if (postulacion != null)
                {

                    return Ok(new DataResponse<List<PostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaExitosa,
                        Datos = postulacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar Bitacora postulacion por id
        [HttpGet("Consultar_PostulacionBitacora")]
        [ProducesResponseType<DataResponse<BitacoraPostulacionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostulacionBitacora(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id == null)
                {
                    return BadRequest("Debe especificar la postulacion");
                }

                List<BitacoraPostulacionesDTO> postulacion = await _IPostulaciones.ConsultarPostulacionBitacora(id);
                if (postulacion != null)
                {

                    return Ok(new DataResponse<List<BitacoraPostulacionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaExitosa,
                        Datos = postulacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Ingresar Postulacion
        [HttpPost("crear_Postulacion")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<PostulacionDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarPostulaciones([FromBody] PostulacionDTO nuevo)
        {
            try
            {
                bool result = await _IPostulaciones.IngresarPostulaciones(nuevo);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajePostulacion.IngresoExitosa);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajePostulacion.IngresoErrada,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.OcurrioError);
            }

        }
        #endregion
    }
}
