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
    public class CondicionController : Controller
    {
        private readonly ModeloDatos.IModelos.ICondicion _ICondicion;

        public CondicionController(ICondicion Condicion)
        {
            _ICondicion = Condicion;
        }

        #region Consultar Condicion
        [HttpGet("Consultar_Condicion")]
        [ProducesResponseType<DataResponse<List<CondicionesDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCondicion()
        {
            try
            {
                List<CondicionesDTO> Condiciones = await _ICondicion.ConsultarCondicion();
                if (Condiciones != null)
                {
                    return Ok(new DataResponse<List<CondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicion.BusquedaExitosa,
                        Datos = Condiciones
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar condicion por nombre
        [HttpGet("Consultar_CondicionGeneral")]
        [ProducesResponseType<DataResponse<CondicionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCondicionGeneral(string? nombreCondicion= null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreCondicion) == null )
                {
                    return BadRequest("Debe especificar el nombre de la condición");
                }

                List<CondicionesDTO> beneficios = await _ICondicion.ConsultarCondicionGeneral(nombreCondicion);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<CondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar condicion por id
        [HttpGet("Consultar_CondicionEspecifico")]
        [ProducesResponseType<DataResponse<CondicionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCondicionEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la condición");
                }

                List<CondicionesDTO> beneficios = await _ICondicion.ConsultarCondicionEspecifico(id);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<CondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Condicion
        [HttpPost("crear_Condicion")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarCondicion([FromBody] CondicionesDTO nuevoCondicion)
        {
            try
            {
                bool result = await _ICondicion.IngresarCondicion(nuevoCondicion);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicion.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Condicion
        [HttpPut("actualiza_Condicion")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCondicion([FromBody] ModeloDatos.DTO.CondicionesDTO CondicionesDTO)
        {
            if (CondicionesDTO == null)
            {
                return BadRequest("La condicion  no pueden ser nulos.");
            }
            if (CondicionesDTO.Id == null)
            {

                return BadRequest("el id de la Condiciones no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool CondicionExistente = await _ICondicion.ActualizaCondicion(CondicionesDTO);
                    if (CondicionExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicion.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicion.OcurrioError);
                }
            }

        }
        #endregion
    }
}
