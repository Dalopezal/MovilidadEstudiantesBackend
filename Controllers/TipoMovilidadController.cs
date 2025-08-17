using AccesoDatos.Movilidad;
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
    public class TipoMovilidadController : Controller
    {
        private readonly ModeloDatos.IModelos.ITipoMovilidad _ITipoMovilidad;

        public TipoMovilidadController(ITipoMovilidad TipoMovilidad)
        {
            _ITipoMovilidad = TipoMovilidad;
        }

        #region Consultar Tipo Movilida
        [HttpGet("Consultar_TipoMovilida")]
        [ProducesResponseType<DataResponse<List<TipoMovilidadDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoMovilida()
        {
            try
            {
                List<TipoMovilidadDTO> tipomovilidad = await _ITipoMovilidad.ConsultarTipoMovilida();
                if (tipomovilidad != null)
                {
                    return Ok(new DataResponse<List<TipoMovilidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoMovilidad.BusquedaExitosa,
                        Datos = tipomovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoMovilidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar tipo movilidad por nombre
        [HttpGet("Consultar_TipoMovilidaGeneral")]
        [ProducesResponseType<DataResponse<TipoMovilidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoMovilidaGeneral(string? nombreTipoModalidad = null)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreTipoModalidad))
                {
                    return BadRequest("Debe especificar el nombre del tipo de movilidad.");
                }

                List<TipoMovilidadDTO> tipomovilidad = await _ITipoMovilidad.ConsultarTipoMovilidaGeneral(nombreTipoModalidad);
                if (tipomovilidad != null)
                {
                    return Ok(new DataResponse<List<TipoMovilidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoMovilidad.BusquedaExitosa,
                        Datos = tipomovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoMovilidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar tipo movilidad por Id 
        [HttpGet("Consultar_ConsultarTipoMovilidaEspecificoTipoMovilidaEspecifico")]
        [ProducesResponseType<DataResponse<TipoMovilidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoMovilidaEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la modalidad");
                }

                List<TipoMovilidadDTO> tipomovilidad = await _ITipoMovilidad.ConsultarTipoMovilidaEspecifico(id);
                if (tipomovilidad != null)
                {

                    return Ok(new DataResponse<List<TipoMovilidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoMovilidad.BusquedaExitosa,
                        Datos = tipomovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoMovilidad.BusquedaErrada);
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
