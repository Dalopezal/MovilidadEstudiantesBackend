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
    public class TipoConvenioController : Controller
    {
        private readonly ModeloDatos.IModelos.ITipoConvenio _ITipoConvenio;

        public TipoConvenioController(ITipoConvenio TipoConvenio)
        {
            _ITipoConvenio = TipoConvenio;
        }

        #region Consultar Tipo Convenio
        [HttpGet("Consultar_TipoConvenio")]
        [ProducesResponseType<DataResponse<List<TipoConvenioDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoConvenio()
        {
            try
            {
                List<TipoConvenioDTO> tipoconvenio = await _ITipoConvenio.ConsultarTipoConvenio();
                if (tipoconvenio != null)
                {
                    return Ok(new DataResponse<List<TipoConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoConvenio.BusquedaExitosa,
                        Datos = tipoconvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoConvenio.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar tipo convenio por nombre
        [HttpGet("Consultar_TipoMovilidaGeneral")]
        [ProducesResponseType<DataResponse<TipoConvenioDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoConvenioGeneral(string? nombre = null)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    return BadRequest("Debe especificar el nombre del tipo de convenio.");
                }

                List<TipoConvenioDTO> tipoconvenio = await _ITipoConvenio.ConsultarTipoConvenioGeneral(nombre);
                if (tipoconvenio != null)
                {
                    return Ok(new DataResponse<List<TipoConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoConvenio.BusquedaExitosa,
                        Datos = tipoconvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoConvenio.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar tipo convenio por Id 
        [HttpGet("Consultar_TipoConvenioEspecifico")]
        [ProducesResponseType<DataResponse<TipoConvenioDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoConvenioEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la modalidad");
                }

                List<TipoConvenioDTO> tipoconvenio = await _ITipoConvenio.ConsultarTipoConvenioEspecifico(id);
                if (tipoconvenio != null)
                {

                    return Ok(new DataResponse<List<TipoConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoConvenio.BusquedaExitosa,
                        Datos = tipoconvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoConvenio.BusquedaErrada);
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
