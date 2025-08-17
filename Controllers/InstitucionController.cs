using AccesoDatos.Movilidad;
using Apis.Contrats;
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
    public class InstitucionController : Controller
    {
        private readonly ModeloDatos.IModelos.IInstitucion _IInstitucion;

        public InstitucionController(IInstitucion Institucion)
        {
            _IInstitucion = Institucion;
        }

        #region Consultar Institucion
        [HttpGet("Consultar_Institucion")]
        [ProducesResponseType<DataResponse<List<InstitucionDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarInstitucion()
        {
            try
            {
                List<InstitucionDTO> institucion = await _IInstitucion.ConsultarInstitucion();
                if (institucion != null)
                {
                    return Ok(new DataResponse<List<InstitucionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.BusquedaExitosa,
                        Datos = institucion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Institucion por nombre 
        [HttpGet("Consultar_InstitucionGeneral")]
        [ProducesResponseType<DataResponse<InstitucionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarInstitucionGeneral(string? nombreInstitucion = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreInstitucion) == null )
                {
                    return BadRequest("Debe especificar el nombre de la institucion.");
                }

                List<InstitucionDTO> institucion = await _IInstitucion.ConsultarInstitucionGeneral(nombreInstitucion);
                if (institucion != null)
                {

                    return Ok(new DataResponse<List<InstitucionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.BusquedaExitosa,
                        Datos = institucion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Institucion por id
        [HttpGet("Consultar_InstitucionEspecifico")]
        [ProducesResponseType<DataResponse<InstitucionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarInstitucionEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el beneficio");
                }

                List<InstitucionDTO> institucion = await _IInstitucion.ConsultarInstitucionEspecifico(id);
                if (institucion != null)
                {

                    return Ok(new DataResponse<List<InstitucionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.BusquedaExitosa,
                        Datos = institucion
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Institucion
        [HttpPost("crear_Institucion")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarInstitucionConvenio([FromBody] InstitucionDTO nuevoInstitucion)
        {
            try
            {
                bool result = await _IInstitucion.IngresarInstitucionConvenio(nuevoInstitucion);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Institucion
        [HttpPut("Actualiza_Institucion")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaInstitucionConvenio([FromBody] ModeloDatos.DTO.InstitucionDTO InstitucionDTO)
        {
            if (InstitucionDTO == null)
            {
                return BadRequest("Los datos de la Institucion no pueden ser nulos.");
            }
            if (InstitucionDTO.Id == null)
            {

                return BadRequest("el id de la Institucion no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool institucionExistente = await _IInstitucion.ActualizaInstitucionConvenio(InstitucionDTO);
                    if (institucionExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeIntitucion.OcurrioError);
                }
            }

        }
        #endregion
    }
}
