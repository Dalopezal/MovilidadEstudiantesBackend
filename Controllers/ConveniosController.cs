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

/// <summary>
/// Daniel Alejandro Lopez Beneficio COnvocatoria.
/// </summary>
namespace Apis.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosController : Controller
    {
        private readonly ModeloDatos.IModelos.IConvenio _IConvenio;

        public ConveniosController(IConvenio Beneficios)
        {
            _IConvenio = Beneficios;
        }

        #region Consultar Convenio
        [HttpGet("Consultar_Convenio")]
        [ProducesResponseType<DataResponse<List<ConvenioDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarConvenio()
        {
            try
            {
                List<ConvenioDTO> convenio = await _IConvenio.ConsultarConvenio();
                if (convenio != null)
                {
                    return Ok(new DataResponse<List<ConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvenios.BusquedaExitosa,
                        Datos = convenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Consultar Convenio por nombre
        [HttpGet("Consultar_ConvenioGeneral")]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarConvenioGeneral(string? nombreConvenio = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreConvenio) == null )
                {
                    return BadRequest("Debe especificar el nombre del convenio.");
                }

                List<ConvenioDTO> convenio = await _IConvenio.ConsultarConvenioGeneral(nombreConvenio);
                if (convenio != null)
                {

                    return Ok(new DataResponse<List<ConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvenios.BusquedaExitosa,
                        Datos = convenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Convenio Por Id
        [HttpGet("Consultar_ConvenioEspecifico")]
        [ProducesResponseType<DataResponse<ConvenioDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarConvenioEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el beneficio");
                }

                List<ConvenioDTO> convenio = await _IConvenio.ConsultarConvenioEspecifico(id);
                if (convenio != null)
                {

                    return Ok(new DataResponse<List<ConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvenios.BusquedaExitosa,
                        Datos = convenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Convenio
        [HttpPost("crear_Convenio")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<ConvenioDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarConvenio([FromBody] ConvenioDTO nuevoConvenio)
        {
            try
            {
                bool result = await _IConvenio.IngresarConvenio(nuevoConvenio);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvenios.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Convenio
        [HttpPut("Actualiza_Convenio")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaConvenio([FromBody] ModeloDatos.DTO.ConvenioDTO ConvenioDTO)
        {
            if (ConvenioDTO == null)
            {
                return BadRequest("Los datos del convenio no pueden ser nulos.");
            }
            if (ConvenioDTO.Id == null)
            {

                return BadRequest("el id del convenio no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool ConvenioExistente = await _IConvenio.ActualizaConvenio(ConvenioDTO);
                    if (ConvenioExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvenios.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.OcurrioError);
                }
            }

        }
        #endregion

        #region eliminar Convenio
        [HttpPatch("Eliminar_Entregable/{id}")]
        public async Task<IActionResult> EliminarConvenio(int id)
        {
            try
            {
                bool eliminado = await _IConvenio.EliminarConvenio(id);

                if (!eliminado)
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvenios.OcurrioError);
                }

                return Ok(new
                {
                    exito = ModeloDatos.Utilidades.Mensaje.MensajeConvenios.ActualizacionExitosa,
                    datos = eliminado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModeloDatos.Utilidades.Mensaje.MensajeConvenios.OcurrioError);
            }
        }
        #endregion
    }
}
