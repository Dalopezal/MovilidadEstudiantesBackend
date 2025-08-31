using Apis.Contrats;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFinanciacionController : Controller
    {
        private readonly ModeloDatos.IModelos.ITipoFinanciacion _ITipoFinanciacion;

        public TipoFinanciacionController(ITipoFinanciacion TipoFinanciacion)
        {
            _ITipoFinanciacion = TipoFinanciacion;
        }

        #region Consultar Tipo Financiacion
        [HttpGet("Consultar_TipoFinanciacion")]
        [ProducesResponseType<DataResponse<List<TipoFinanciacionDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoFinanciacion()
        {
            try
            {
                List<TipoFinanciacionDTO> tipo = await _ITipoFinanciacion.ConsultarTipoFinanciacion();
                if (tipo != null)
                {
                    return Ok(new DataResponse<List<TipoFinanciacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.BusquedaExitosa,
                        Datos = tipo
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion



        #region Consultar tipo financiacion por Id 
        [HttpGet("Consultar_TipoFinanciacionEspecifico")]
        [ProducesResponseType<DataResponse<TipoFinanciacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoFinanciacionEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id del tipo de financiacion");
                }

                List<TipoFinanciacionDTO> tipo = await _ITipoFinanciacion.ConsultarTipoFinanciacionEspecifico(id);
                if (tipo != null)
                {

                    return Ok(new DataResponse<List<TipoFinanciacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.BusquedaExitosa,
                        Datos = tipo
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacion.BusquedaErrada);
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
