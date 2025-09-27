using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class FinanciacionExternaController : Controller
    {
        private readonly ModeloDatos.IModelos.IFinanciacionExterna _IFinanciacionExterna;

        public FinanciacionExternaController(IFinanciacionExterna FinanciacionExterna)
        {
            _IFinanciacionExterna = FinanciacionExterna;
        }

        #region Consultar FinanciacionExterna
        [HttpGet("Consultar_FinanciacionExterna")]
        [ProducesResponseType<DataResponse<List<TipoFinanciacionExternaDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciacionExterna()
        {
            try
            {
                List<TipoFinanciacionExternaDTO> entregable = await _IFinanciacionExterna.ConsultarFinanciacionExterna();
                if (entregable != null)
                {
                    return Ok(new DataResponse<List<TipoFinanciacionExternaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacionExterna.BusquedaExitosa,
                        Datos = entregable
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacionExterna.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Consultar FinanciacionExterna Especifico
        [HttpGet("Consultar_FinanciacionExternaEspecifico")]
        [ProducesResponseType<DataResponse<TipoFinanciacionExternaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciacionExternaEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null)
                {
                    return BadRequest("Debe especificar el id de la financiacion.");
                }

                List<TipoFinanciacionExternaDTO> entregable = await _IFinanciacionExterna.ConsultarFinanciacionExternaEspecifico(id);
                if (entregable != null)
                {

                    return Ok(new DataResponse<List<TipoFinanciacionExternaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacionExterna.BusquedaExitosa,
                        Datos = entregable
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacionExterna.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Crear Financiacion Externa
        [HttpPost("crear_FinanciacionExterna")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<TipoFinanciacionExternaDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarFinanciacionExterna([FromBody] TipoFinanciacionExternaDTO nuevo)
        {
            try
            {
                bool result = await _IFinanciacionExterna.IngresarFinanciacionExterna(nuevo);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.OcurrioError);
            }

        }
        #endregion

        #region Actualizar FinanciacionExterna
        [HttpPut("Actualiza_FinanciacionExterna")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaFinanciacionExterna([FromBody] ModeloDatos.DTO.TipoFinanciacionExternaDTO TipoFinanciacionExternaDTO)
        {
            if (TipoFinanciacionExternaDTO == null)
            {
                return BadRequest("Los datos de la financiacion externa no pueden ser nulos.");
            }
            if (TipoFinanciacionExternaDTO.Id == null)
            {

                return BadRequest("el id de la financiacion no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool financiacionExistente = await _IFinanciacionExterna.ActualizaFinanciacionExterna(TipoFinanciacionExternaDTO);
                    if (financiacionExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.OcurrioError);
                }
            }

        }
        #endregion


        #region Eliminar FinanciacionExterna
        [HttpPatch("Eliminar_FinanciacionExterna/{id}")]
        public async Task<IActionResult> EliminarFinanciacionExterna(int id)
        {
            try
            {
                bool eliminado = await _IFinanciacionExterna.EliminarFinanciacionExterna(id);

                if (!eliminado)
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.OcurrioError);
                }

                return Ok(new
                {
                    exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.ActualizacionExitosa,
                    datos = eliminado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.OcurrioError);
            }
        }
        #endregion

    }
}
