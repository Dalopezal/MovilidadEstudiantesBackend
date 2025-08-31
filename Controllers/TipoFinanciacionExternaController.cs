using Apis.Contrats;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFinanciacionExternaController : Controller
    {
        private readonly ModeloDatos.IModelos.IFinanciacionExterna _IFinanciacionExterna;

        public TipoFinanciacionExternaController(IFinanciacionExterna FinanciacionExterna)
        {
            _IFinanciacionExterna = FinanciacionExterna;
        }

        #region Consultar Tipo Financiacion Externa
        [HttpGet("Consultar_tipoFinanciacionExterna")]
        [ProducesResponseType<DataResponse<List<TipoFinanciacionExternaDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarFinanciacionExterna()
        {
            try
            {
                List<TipoFinanciacionExternaDTO> tipo = await _IFinanciacionExterna.ConsultarFinanciacionExterna();
                if (tipo != null)
                {
                    return Ok(new DataResponse<List<TipoFinanciacionExternaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacionExterna.BusquedaExitosa,
                        Datos = tipo
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



        #region Consultar tipo financiacion por Id 
        [HttpGet("Consultar_tipoFinanciacionExternaEspecifico")]
        [ProducesResponseType<DataResponse<TipoFinanciacionExternaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTipoFinanciacionExternaEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id del tipo de financiacion externa");
                }

                List<TipoFinanciacionExternaDTO> tipo = await _IFinanciacionExterna.ConsultarFinanciacionExternaEspecifico(id);
                if (tipo != null)
                {

                    return Ok(new DataResponse<List<TipoFinanciacionExternaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeTipoFinanciacionExterna.BusquedaExitosa,
                        Datos = tipo
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
    }
}
