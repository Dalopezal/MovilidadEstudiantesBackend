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
    public class InstitucionConvenioController : Controller
    {
        private readonly ModeloDatos.IModelos.IInstitucionConvenio _IInstitucionConvenio;

        public InstitucionConvenioController(IInstitucionConvenio InstitucionConvenio)
        {
            _IInstitucionConvenio = InstitucionConvenio;
        }

        #region Consultar Institucion Convenio
        [HttpGet("Consultar_ConsultarInstitucionConvenio")]
        [ProducesResponseType<DataResponse<List<InstitucionConvenioDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarInstitucionConvenio()
        {
            try
            {
                List<InstitucionConvenioDTO> institucionConvenio = await _IInstitucionConvenio.ConsultarInstitucionConvenio();
                if (institucionConvenio != null)
                {
                    return Ok(new DataResponse<List<InstitucionConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.BusquedaExitosa,
                        Datos = institucionConvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Institucion Convenio por nombre institucion o nombre convenio
        [HttpGet("Consultar_InstitucionConvenioGeneral")]
        [ProducesResponseType<DataResponse<InstitucionConvenioDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosGeneral(string? nombreInstitucion = null, string? nombreConvenio = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreInstitucion) == null && string.IsNullOrEmpty(nombreConvenio))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros: nombre de institucion o nombre convenio.");
                }

                List<InstitucionConvenioDTO> institucionConvenio = await _IInstitucionConvenio.ConsultarInstitucionConvenioGeneral(nombreInstitucion, nombreConvenio);
                if (institucionConvenio != null)
                {

                    return Ok(new DataResponse<List<InstitucionConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.BusquedaExitosa,
                        Datos = institucionConvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Institucion Convenio  por id
        [HttpGet("Consultar_InstitucionConvenioEspecifico")]
        [ProducesResponseType<DataResponse<InstitucionConvenioDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarInstitucionConvenioEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la institucion convenio");
                }

                List<InstitucionConvenioDTO> institucionConvenio = await _IInstitucionConvenio.ConsultarInstitucionConvenioEspecifico(id);
                if (institucionConvenio != null)
                {

                    return Ok(new DataResponse<List<InstitucionConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.BusquedaExitosa,
                        Datos = institucionConvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Institucion Convenio
        [HttpPost("crear_InstitucionConvenio")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarInstitucionConvenio([FromBody] InstitucionConvenioDTO nuevoInstitucionConvenio)
        {
            try
            {
                bool result = await _IInstitucionConvenio.IngresarInstitucionConvenio(nuevoInstitucionConvenio);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Beneficio
        [HttpPut("actualiza_Beneficio")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaInstitucionConvenio([FromBody] ModeloDatos.DTO.InstitucionConvenioDTO InstitucionConvenioDTO)
        {
            if (InstitucionConvenioDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            if (InstitucionConvenioDTO.Id == null)
            {

                return BadRequest("el id de la solicitud no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool institucionConvenioExistente = await _IInstitucionConvenio.ActualizaInstitucionConvenio(InstitucionConvenioDTO);
                    if (institucionConvenioExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucionConvenio.OcurrioError);
                }
            }

        }
        #endregion
    }
}
