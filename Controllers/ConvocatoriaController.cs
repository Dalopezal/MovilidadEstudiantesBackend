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
    public class ConvocatoriaController : Controller
    {
        private readonly ModeloDatos.IModelos.IConvocatoria _IConvocatoria;

        public ConvocatoriaController(IConvocatoria Convocatoria)
        {
            _IConvocatoria = Convocatoria;
        }

        #region Consultar convocatoria
        [HttpGet("Consultar_Convocatoria")]
        [ProducesResponseType<DataResponse<List<ConvocatoriaDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarConvocatoria()
        {
            try
            {
                List<ConvocatoriaDTO> convocatoria = await _IConvocatoria.ConsultarConvocatoria();
                if (convocatoria != null)
                {
                    return Ok(new DataResponse<List<ConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.BusquedaExitosa,
                        Datos = convocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion




        #region Consultar convocatoria por id
        [HttpGet("Consultar_ConvocatoriaEspecifico")]
        [ProducesResponseType<DataResponse<ConvocatoriaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarConvocatoriaEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id convocatoria");
                }

                List<ConvocatoriaDTO> convocatoria = await _IConvocatoria.ConsultarConvocatoriaEspecifico(id);
                if (convocatoria != null)
                {

                    return Ok(new DataResponse<List<ConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.BusquedaExitosa,
                        Datos = convocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar convocatoria por filtros
        [HttpGet("Consultar_ConvocatoriaGeneral")]
        [ProducesResponseType<DataResponse<ConvocatoriaDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarConvocatoriaGeneral(string? NombreConvocatoria = null,
            DateOnly? FechaInicio = null, DateOnly? FechaFinal = null, int? IdModalidad = null)
        {
            try
            {

                if ( (string.IsNullOrEmpty(NombreConvocatoria) == null) ||
                    (string.IsNullOrEmpty(IdModalidad.ToString()) == null && IdModalidad == null) ||
                    (string.IsNullOrEmpty(FechaInicio.ToString()) == null && FechaInicio != null) ||
                    (string.IsNullOrEmpty(FechaFinal.ToString()) == null && FechaFinal != null))

                {
                    return BadRequest("Debe especificar al menos un parametro de busqueda");
                }

                List<ConvocatoriaDTO> convocatoria = await _IConvocatoria.ConsultarConvocatoriaGeneral(NombreConvocatoria,
                    FechaInicio, FechaFinal, IdModalidad);
                if (convocatoria != null)
                {

                    return Ok(new DataResponse<List<ConvocatoriaDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.BusquedaExitosa,
                        Datos = convocatoria
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Crear Convocatoria
        [HttpPost("crear_Convocatoria")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarConvocatoria([FromBody] ConvocatoriaDTO nuevoConvocatoria)
        {
            try
            {
                bool result = await _IConvocatoria.IngresarConvocatoria(nuevoConvocatoria);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Convocatoria
        [HttpPut("actualiza_Convocatoria")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaConvocatoria([FromBody] ModeloDatos.DTO.ConvocatoriaDTO ConvocatoriaDTO)
        {
            if (ConvocatoriaDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            if (ConvocatoriaDTO.Id == null)
            {

                return BadRequest("el id de la solicitud no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool ConvocatoriaExistente = await _IConvocatoria.ActualizaConvocatoria(ConvocatoriaDTO);
                    if (ConvocatoriaExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeConvocatoria.OcurrioError);
                }
            }

        }
        #endregion
    }
}
