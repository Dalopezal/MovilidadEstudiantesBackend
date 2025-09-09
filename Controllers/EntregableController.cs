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
    [EnableCors]
    public class EntregableController : Controller
    {

        private readonly ModeloDatos.IModelos.IEntregable _IEntregable;

        public EntregableController(IEntregable Entregable)
        {
            _IEntregable = Entregable;
        }

        #region Consultar Entregable
        [HttpGet("Consultar_Entregable")]
        [ProducesResponseType<DataResponse<List<EntregableDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarEntregable()
        {
            try
            {
                List<EntregableDTO> entregable = await _IEntregable.ConsultarEntregable();
                if (entregable != null)
                {
                    return Ok(new DataResponse<List<EntregableDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaExitosa,
                        Datos = entregable
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar entregable por nombre 
        [HttpGet("Consultar_EntregableGeneral")]
        [ProducesResponseType<DataResponse<EntregableDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarEntregableGeneral(string? nombre = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombre) == null)
                {
                    return BadRequest("Debe especificar ael nombre del entregable.");
                }

                List<EntregableDTO> entregable = await _IEntregable.ConsultarEntregableGeneral(nombre);
                if (entregable != null)
                {

                    return Ok(new DataResponse<List<EntregableDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaExitosa,
                        Datos = entregable
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar entregable por id
        [HttpGet("Consultar_EntregableEspecifico")]
        [ProducesResponseType<DataResponse<EntregableDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarEntregableEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el entregable");
                }

                List<EntregableDTO> entregable = await _IEntregable.ConsultarEntregableEspecifico(id);
                if (entregable != null)
                {

                    return Ok(new DataResponse<List<EntregableDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaExitosa,
                        Datos = entregable
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar entregable por id Convocatoria
        [HttpGet("Consultar_EntregableConvocataria")]
        [ProducesResponseType<DataResponse<EntregableDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarEntregableConvocataria(int? idConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idConvocatoria.ToString()) == null && idConvocatoria != null)
                {
                    return BadRequest("Debe especificar la convocatoria");
                }

                List<EntregableDTO> entregable = await _IEntregable.ConsultarEntregableConvocataria(idConvocatoria);
                if (entregable != null)
                {

                    return Ok(new DataResponse<List<EntregableDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaExitosa,
                        Datos = entregable
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Entregable
        [HttpPost("crear_Entregable")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarEntregable([FromBody] EntregableDTO nuevoEntregable)
        {
            try
            {
                bool result = await _IEntregable.IngresarEntregable(nuevoEntregable);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregable.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Entregable
        [HttpPut("Actualiza_Entregable")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaEntregable([FromBody] ModeloDatos.DTO.EntregableDTO EntregableDTO)
        {
            if (EntregableDTO == null)
            {
                return BadRequest("Los datos del entregable no pueden ser nulos.");
            }
            if (EntregableDTO.Id == null)
            {

                return BadRequest("el id del entregable no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool EntregableExistente = await _IEntregable.ActualizaEntregable(EntregableDTO);
                    if (EntregableExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregable.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregable.OcurrioError);
                }
            }

        }
        #endregion
    }
}
