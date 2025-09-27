using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [DisableCors]
    public class BeneficioConvocatoriaController : Controller
    {
        
        private readonly ModeloDatos.IModelos.IBeneficios _IBeneficios;

        public BeneficioConvocatoriaController(IBeneficios Beneficios)
        {
            _IBeneficios = Beneficios;
        }

        #region Consultar Beneficios de la convocatoria
        [HttpGet("Consultar_BeneficiosConvocatoria")]
        [ProducesResponseType<DataResponse<List<BeneficiosDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosConvocatoria()
        {
            try
            {
                List<BeneficiosDTO> beneficios = await _IBeneficios.ConsultarBeneficios();
                if (beneficios != null)
                {
                    return Ok(new DataResponse<List<BeneficiosDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Beneficios por nombre o nombre de la convocatoria
        [HttpGet("Consultar_BeneficiosGeneral")]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosGeneral(string? nombreBeneficio = null, string? nombreConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreBeneficio) == null && string.IsNullOrEmpty(nombreConvocatoria))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros: nombre de ubicacion o nombre convocatoria.");
                }

                List<BeneficiosDTO> beneficios = await _IBeneficios.ConsultarBeneficiosGeneral(nombreBeneficio, nombreConvocatoria);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<BeneficiosDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Beneficios por nombre o nombre de la convocatoria
        [HttpGet("Consultar_BeneficiosEspecifico")]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id!=null)
                {
                    return BadRequest("Debe especificar el beneficio");
                }

                List<BeneficiosDTO> beneficios = await _IBeneficios.ConsultarBeneficiosEspecifico(id);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<BeneficiosDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Beneficios por nombre o nombre de la convocatoria
        [HttpGet("Consultar_BeneficiosIdConvocatoria")]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosIdConvocatoria(int? idConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idConvocatoria.ToString()) == null && idConvocatoria != null)
                {
                    return BadRequest("Debe especificar la convocatoria");
                }

                List<BeneficiosDTO> beneficios = await _IBeneficios.ConsultarBeneficiosIdConvocatoria(idConvocatoria);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<BeneficiosDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Beneficio
        [HttpPost("crear_Beneficio")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarBeneficios([FromBody] BeneficiosDTO nuevoBeneficio)
        {
            try
            {
                bool result = await _IBeneficios.IngresarBeneficios(nuevoBeneficio);

                if (result ==false)
                {
                 
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.OcurrioError);
            }
           
        }
        #endregion

        #region Actualizar Beneficio
        [HttpPut("actualiza_Beneficio")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaBeneficios([FromBody] ModeloDatos.DTO.BeneficiosDTO BeneficiosDTO)
        {
            if (BeneficiosDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            if (BeneficiosDTO.Id == null)
            {

                return BadRequest("el id de la solicitud no pueden ser nulos.");
            }
            else {
                try
                {

                    bool BeneficioExistente = await _IBeneficios.ActualizaBeneficios(BeneficiosDTO);
                    if (BeneficioExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.OcurrioError);
                }
            }
                
        }
        #endregion

        #region eliminar Benerficios
        [HttpPatch("Eliminar_Beneficios/{id}")]
        public async Task<IActionResult> EliminarBeneficios(int id)
        {
            try
            {
                bool eliminado = await _IBeneficios.EliminarBeneficios(id);

                if (!eliminado)
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.OcurrioError);
                }

                return Ok(new
                {
                    exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.ActualizacionExitosa,
                    datos = eliminado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.OcurrioError);
            }
        }
        #endregion

    }
}
