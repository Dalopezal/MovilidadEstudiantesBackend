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
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciacionUCMController : Controller
    {
        private readonly ModeloDatos.IModelos.IFinanciacionUCM _IFinanciacionUCM;

        public FinanciacionUCMController(IFinanciacionUCM FinanciacionUCM)
        {
            _IFinanciacionUCM = FinanciacionUCM;
        }

        #region Consultar Financiacion UCM
        [HttpGet("Consultar_financiacionUCM")]
        [ProducesResponseType<DataResponse<List<FinanciacionUCMDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciaciones()
        {
            try
            {
                List<FinanciacionUCMDTO> financiacion = await _IFinanciacionUCM.ConsultarFinanciaciones();
                if (financiacion != null)
                {
                    return Ok(new DataResponse<List<FinanciacionUCMDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaExitosa,
                        Datos = financiacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar financiacion por documento del usuario
        [HttpGet("Consultar_financiacionesUsuario")]
        [ProducesResponseType<DataResponse<FinanciacionUCMDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciacionesUsuario(string? documento = null)
        {
            try
            {

                if (string.IsNullOrEmpty(documento) == null)
                {
                    return BadRequest("Debe especificar el documento del usuario");
                }

                List<FinanciacionUCMDTO> financiacion = await _IFinanciacionUCM.ConsultarFinanciacionesUsuario(documento);
                if (financiacion != null)
                {

                    return Ok(new DataResponse<List<FinanciacionUCMDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaExitosa,
                        Datos = financiacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar financiacion por Id
        [HttpGet("Consultar_financiacionesEspecifico")]
        [ProducesResponseType<DataResponse<FinanciacionUCMDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciacionesEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar la financiacion");
                }

                List<FinanciacionUCMDTO> financiacion = await _IFinanciacionUCM.ConsultarFinanciacionesEspecifico(id);
                if (financiacion != null)
                {

                    return Ok(new DataResponse<List<FinanciacionUCMDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaExitosa,
                        Datos = financiacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar financiacion por Postulacion
        [HttpGet("Consultar_FinanciacionesPostulacion")]
        [ProducesResponseType<DataResponse<FinanciacionUCMDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciacionesPostulacion(int? PostulacionId = null)
        {
            try
            {

                if (string.IsNullOrEmpty(PostulacionId.ToString()) == null && PostulacionId != null)
                {
                    return BadRequest("Debe especificar la postulacion");
                }

                List<FinanciacionUCMDTO> financiacion = await _IFinanciacionUCM.ConsultarFinanciacionesPostulacion(PostulacionId);
                if (financiacion != null)
                {

                    return Ok(new DataResponse<List<FinanciacionUCMDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaExitosa,
                        Datos = financiacion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Financiacion
        [HttpPost("crear_Financiacion")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<FinanciacionUCMDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarFinanciacionUCM([FromBody] FinanciacionUCMDTO nuevoFinanciacion)
        {
            try
            {
                bool result = await _IFinanciacionUCM.IngresarFinanciacionUCM(nuevoFinanciacion);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Financiacion
        [HttpPut("actualiza_Financiacion")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaFinanciacionUCM([FromBody] ModeloDatos.DTO.FinanciacionUCMDTO FinanciacionUCMDTO)
        {
            if (FinanciacionUCMDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            if (FinanciacionUCMDTO.Id == null)
            {

                return BadRequest("el id de la solicitud no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool financiacionExistente = await _IFinanciacionUCM.ActualizaFinanciacionUCM(FinanciacionUCMDTO);
                    if (financiacionExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeFinanciacionUCM.OcurrioError);
                }
            }

        }
        #endregion

    }
}
