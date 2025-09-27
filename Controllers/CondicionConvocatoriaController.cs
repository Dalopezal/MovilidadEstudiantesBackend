using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    /// <summary>
    /// Daniel Alejandro Lopez condicion COnvocatoria.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CondicionConvocatoriaController : Controller
    {
        private readonly ModeloDatos.IModelos.ICondicionesConvocatoria _ICondicionesConvocatoria;

        public CondicionConvocatoriaController(ICondicionesConvocatoria CondicionesConvocatoria)
        {
            _ICondicionesConvocatoria = CondicionesConvocatoria;
        }

        #region Consultar Condiciones Convocatoria
        [HttpGet("Consultar_CondicionesConvocatoria")]
        [ProducesResponseType<DataResponse<List<CondicionesConvocatoriaDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCondicionesConvocatoria()
        {
            try
            {
                List<CondicionesConvocatoriaDTO> condicionesConvocatoria = await _ICondicionesConvocatoria.ConsultarCondicionesConvocatoria();
                if (condicionesConvocatoria != null)
                {
                    return Ok(new DataResponse<List<CondicionesConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaExitosa,
                        Datos = condicionesConvocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar condicion convocatoria por nombre condicion o nombre de la convocatoria
        [HttpGet("Consultar_CondicionesConvocatoriaGeneral")]
        [ProducesResponseType<DataResponse<CondicionesConvocatoriaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CondicionesConvocatoriaGeneral(string? nombreCondicion = null, string? nombreConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreCondicion) == null && string.IsNullOrEmpty(nombreConvocatoria))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros: nombre de condición o nombre convocatoria.");
                }

                List<CondicionesConvocatoriaDTO> condicionesConvocatoria = await _ICondicionesConvocatoria.CondicionesConvocatoriaGeneral(nombreCondicion, nombreConvocatoria);
                if (condicionesConvocatoria != null)
                {

                    return Ok(new DataResponse<List<CondicionesConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaExitosa,
                        Datos = condicionesConvocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar condicion convocatoria por id
        [HttpGet("Consultar_CondicionesConvocatoriaEspecifico")]
        [ProducesResponseType<DataResponse<CondicionesConvocatoriaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CondicionesConvocatoriaEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la condicon convocatoria");
                }

                List<CondicionesConvocatoriaDTO> condicionesConvocatoria = await _ICondicionesConvocatoria.CondicionesConvocatoriaEspecifico(id);
                if (condicionesConvocatoria != null)
                {

                    return Ok(new DataResponse<List<CondicionesConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaExitosa,
                        Datos = condicionesConvocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar condicion convocatoria por id convocatoria
        [HttpGet("Consultar_CondicionesConvocatoriaConvocatoria")]
        [ProducesResponseType<DataResponse<CondicionesConvocatoriaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CondicionesConvocatoriaConvocatoria(int? idConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idConvocatoria.ToString()) == null && idConvocatoria != null)
                {
                    return BadRequest("Debe especificar el id de la convocatoria");
                }

                List<CondicionesConvocatoriaDTO> condicionesConvocatoria = await _ICondicionesConvocatoria.CondicionesConvocatoriaConvocatoria(idConvocatoria);
                if (condicionesConvocatoria != null)
                {

                    return Ok(new DataResponse<List<CondicionesConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaExitosa,
                        Datos = condicionesConvocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar condicion convocatoria por id condicion
        [HttpGet("Consultar_CondicionesConvocatoriaCondicion")]
        [ProducesResponseType<DataResponse<CondicionesConvocatoriaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CondicionesConvocatoriaCondicion(int? idCondicion = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idCondicion.ToString()) == null && idCondicion != null)
                {
                    return BadRequest("Debe especificar el id de la condición");
                }

                List<CondicionesConvocatoriaDTO> condicionesConvocatoria = await _ICondicionesConvocatoria.CondicionesConvocatoriaCondicion(idCondicion);
                if (condicionesConvocatoria != null)
                {

                    return Ok(new DataResponse<List<CondicionesConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaExitosa,
                        Datos = condicionesConvocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Crear condicion convocatoria
        [HttpPost("crear_CondicionesConvocatoria")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<CondicionesConvocatoriaDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarCondicionesConvocatoria([FromBody] CondicionesConvocatoriaDTO nuevoCondicionesConvocatoria)
        {
            try
            {
                bool result = await _ICondicionesConvocatoria.IngresarCondicionesConvocatoria(nuevoCondicionesConvocatoria);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.OcurrioError);
            }

        }
        #endregion

        #region Actualizar condicion convocatoria
        [HttpPut("actualiza_CondicionesConvocatoria")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCondicionesConvocatoria([FromBody] ModeloDatos.DTO.CondicionesConvocatoriaDTO CondicionesConvocatoriaDTO)
        {
            if (CondicionesConvocatoriaDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool CondicionesConvocatoriaExistente = await _ICondicionesConvocatoria.ActualizaCondicionesConvocatoria(CondicionesConvocatoriaDTO);
                    if (CondicionesConvocatoriaExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.OcurrioError);
                }
            }

        }
        #endregion


        #region Eliminar Condiciones Convocatoria
        [HttpPatch("Eliminar_CondicionesConvocatoria/{idCondicion},{IdConvocatoria}")]
        public async Task<IActionResult> EliminarCondicionesConvocatoria(int idCondicion, int IdConvocatoria)
        {
            try
            {
                bool eliminado = await _ICondicionesConvocatoria.EliminarCondicionesConvocatoria(idCondicion, IdConvocatoria);

                if (!eliminado)
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.OcurrioError);
                }

                return Ok(new
                {
                    exito = ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.ActualizacionExitosa,
                    datos = eliminado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModeloDatos.Utilidades.Mensaje.MensajeCondicionConvocatoria.OcurrioError);
            }
        }
        #endregion
    }
}
