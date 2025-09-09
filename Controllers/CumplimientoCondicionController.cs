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

    public class CumplimientoCondicionController : Controller
    {
        private readonly ModeloDatos.IModelos.ICumplimientoCondiciones _ICumplimientoCondiciones;

        public CumplimientoCondicionController(ICumplimientoCondiciones CumplimientoCondicion)
        {
            _ICumplimientoCondiciones = CumplimientoCondicion;
        }

        #region Consultar cumplimiento
        [HttpGet("Consultar_CumplimientoCondiciones")]
        [ProducesResponseType<DataResponse<List<CumplimientoCondicionesDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCumplimientoCondiciones()
        {
            try
            {
                List<CumplimientoCondicionesDTO> cumplimiento = await _ICumplimientoCondiciones.ConsultarCumplimientoCondiciones();
                if (cumplimiento != null)
                {
                    return Ok(new DataResponse<List<CumplimientoCondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaExitosa,
                        Datos = cumplimiento
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Cumplimiento condicion por nombre o nombre de la convocatoria
        [HttpGet("Consultar_CumplimientoCondicionesGeneral")]
        [ProducesResponseType<DataResponse<CumplimientoCondicionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCumplimientoCondicionesGeneral(string? NombreConvocatoria = null, string? NombreCondicion = null)
        {
            try
            {
                if (string.IsNullOrEmpty(NombreConvocatoria) == null && string.IsNullOrEmpty(NombreCondicion))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros: nombre de convocatoria o nombre condicion.");
                }

                List<CumplimientoCondicionesDTO> cumplimiento = await _ICumplimientoCondiciones.ConsultarCumplimientoCondicionesGeneral(NombreConvocatoria, NombreCondicion);
                if (cumplimiento != null)
                {
                    return Ok(new DataResponse<List<CumplimientoCondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaExitosa,
                        Datos = cumplimiento
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar cumplimiento por nombre o nombre de la convocatoria
        [HttpGet("Consultar_CumplimientoCondicionesEspecifico")]
        [ProducesResponseType<DataResponse<CumplimientoCondicionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosEspecifico(int? id = null)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el cumplimiento");
                }

                List<CumplimientoCondicionesDTO> beneficios = await _ICumplimientoCondiciones.ConsultarCumplimientoCondicionesEspecifico(id);
                if (beneficios != null)
                {
                    return Ok(new DataResponse<List<CumplimientoCondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar cumplimiento por nombre o nombre de la convocatoria
        [HttpGet("Consultar_CumplimientoCondicionesPostulacion")]
        [ProducesResponseType<DataResponse<CumplimientoCondicionesDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCumplimientoCondicionesPostulacion(int? idPostulacion = null)
        {
            try
            {
                if (string.IsNullOrEmpty(idPostulacion.ToString()) == null && idPostulacion == null)
                {
                    return BadRequest("Debe especificar el cumplimiento");
                }

                List<CumplimientoCondicionesDTO> beneficios = await _ICumplimientoCondiciones.ConsultarCumplimientoCondicionesPostulacion(idPostulacion);
                if (beneficios != null)
                {
                    return Ok(new DataResponse<List<CumplimientoCondicionesDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Cumplimiento condicion
        [HttpPost("crear_CumplimientoCondiciones")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<CumplimientoCondicionesDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarCumplimientoCondiciones([FromBody] CumplimientoCondicionesDTO nuevoCumplimiento)
        {
            try
            {
                bool result = await _ICumplimientoCondiciones.IngresarCumplimientoCondiciones(nuevoCumplimiento);
                if (result == false)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.CreacionExitosa,
                    Datos = result
                });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.OcurrioError);
            }

        }
        #endregion


        #region Actualizar Cumplimiento
        [HttpPut("Actualiza_CumplimientoCondiciones")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCumplimientoCondiciones([FromBody] ModeloDatos.DTO.CumplimientoCondicionesDTO CumplimientoCondicionesDTO)
        {

            if (CumplimientoCondicionesDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            if (CumplimientoCondicionesDTO.Id == null)
            {

                return BadRequest("el id de la solicitud no pueden ser nulos.");
            }
            else
            {
                try
                {
                    bool CumplimientoExistente = await _ICumplimientoCondiciones.ActualizaCumplimientoCondiciones(CumplimientoCondicionesDTO);
                    if (CumplimientoExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCumplimientoCondicion.OcurrioError);
                }
            }

        }
        #endregion
    }
}
