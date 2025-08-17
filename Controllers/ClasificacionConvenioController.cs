using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    /// <summary>
    /// Daniel Alejandro Lopez Clasificacion Convenio
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionConvenioController : Controller
    {
        private readonly ModeloDatos.IModelos.IClasificacionConvenio _IClasificacionConvenio;

        public ClasificacionConvenioController(IClasificacionConvenio ClasificacionConvenio)
        {
            _IClasificacionConvenio = ClasificacionConvenio;
        }

        #region Consultar Clasificacion Convenio
        [HttpGet("Consultar_ClasificacionConvenio")]
        [ProducesResponseType<DataResponse<List<ClasificacionConvenioDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarClasificacionConvenio()
        {
            try
            {
                List<ClasificacionConvenioDTO> clasificacionConvenio = await _IClasificacionConvenio.ConsultarClasificacionConvenio();
                if (clasificacionConvenio != null)
                {
                    return Ok(new DataResponse<List<ClasificacionConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeClasificacionConvenio.BusquedaExitosa,
                        Datos = clasificacionConvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeClasificacionConvenio.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar Clasificacion Convenio por Id 
        [HttpGet("Consultar_ClasificacionConvenioEspecifico")]
        [ProducesResponseType<DataResponse<ClasificacionConvenioDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarClasificacionConvenioEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la clasificacion Convenio");
                }

                List<ClasificacionConvenioDTO> clasificacionConvenio = await _IClasificacionConvenio.ConsultarClasificacionConvenioEspecifico(id);
                if (clasificacionConvenio != null)
                {

                    return Ok(new DataResponse<List<ClasificacionConvenioDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeClasificacionConvenio.BusquedaExitosa,
                        Datos = clasificacionConvenio
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeClasificacionConvenio.BusquedaErrada);
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
