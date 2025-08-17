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
    public class ModalidadController : Controller
    {
        private readonly ModeloDatos.IModelos.IModalidad _IModalidad;

        public ModalidadController(IModalidad Modalidad)
        {
            _IModalidad = Modalidad;
        }

        #region Consultar Modalidad
        [HttpGet("Consultar_Modalidad")]
        [ProducesResponseType<DataResponse<List<ModalidadDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarModalidad()
        {
            try
            {
                List<ModalidadDTO> modalidad = await _IModalidad.ConsultarModalidad();
                if (modalidad != null)
                {
                    return Ok(new DataResponse<List<ModalidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeModalidad.BusquedaExitosa,
                        Datos = modalidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeModalidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar Modalidad por nombre
        [HttpGet("Consultar_ModalidadGeneral")]
        [ProducesResponseType<DataResponse<ModalidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarModalidadGeneral(string? nombreModalidad= null)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreModalidad))
                {
                    return BadRequest("Debe especificar el nombre de la modalidad.");
                }

                List<ModalidadDTO> modalidad = await _IModalidad.ConsultarModalidadGeneral(nombreModalidad);
                if (modalidad != null)
                {

                    return Ok(new DataResponse<List<ModalidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeModalidad.BusquedaExitosa,
                        Datos = modalidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeModalidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Modalidad por Id 
        [HttpGet("Consultar_ModalidadEspecifico")]
        [ProducesResponseType<DataResponse<ModalidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarModalidadEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la modalidad");
                }

                List<ModalidadDTO> modalidad = await _IModalidad.ConsultarModalidadEspecifico(id);
                if (modalidad != null)
                {

                    return Ok(new DataResponse<List<ModalidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeModalidad.BusquedaExitosa,
                        Datos = modalidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeModalidad.BusquedaErrada);
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
