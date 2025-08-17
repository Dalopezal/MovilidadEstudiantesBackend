using Apis.Contrats;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    /// <summary>
    /// Daniel Alejandro Lopez Ciudad
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class TipoActividadController : Controller
    {
        private readonly ModeloDatos.IModelos.ITipoActividad _ITipoActividad;

        public TipoActividadController(ITipoActividad TipoActividad)
        {
            _ITipoActividad = TipoActividad;
        }

        #region Consultar Tipo Actividad
        [HttpGet("Consultar_TipoActividad")]
        [ProducesResponseType<DataResponse<List<TipoActividadDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoActividad()
        {
            try
            {
                List<TipoActividadDTO> tipoActividad = await _ITipoActividad.ConsultarTipoActividad();
                if (tipoActividad != null)
                {
                    return Ok(new DataResponse<List<TipoActividadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoActividad.BusquedaExitosa,
                        Datos = tipoActividad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoActividad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar Tipo Actividad  por nombre
        [HttpGet("Consultar TipoActividad General")]
        [ProducesResponseType<DataResponse<TipoActividadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoActividadGeneral(string? nombre= null)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    return BadRequest("Debe especificar el nombre del tipo de actividad.");
                }

                List<TipoActividadDTO> tipoActividad = await _ITipoActividad.ConsultarTipoActividadGeneral(nombre);
                if (tipoActividad != null)
                {

                    return Ok(new DataResponse<List<TipoActividadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoActividad.BusquedaExitosa,
                        Datos = tipoActividad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoActividad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Tipo Actividad  por Id 
        [HttpGet("Consultar_TipoActividadEspecifico")]
        [ProducesResponseType<DataResponse<TipoActividadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoActividadEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id del tipo de actividad");
                }

                List<TipoActividadDTO> tipoActividad = await _ITipoActividad.ConsultarTipoActividadEspecifico(id);
                if (tipoActividad != null)
                {

                    return Ok(new DataResponse<List<TipoActividadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoActividad.BusquedaExitosa,
                        Datos = tipoActividad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoActividad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion
    }
}
