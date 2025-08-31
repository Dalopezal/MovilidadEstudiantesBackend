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
    public class BeneficioPostulacionController : Controller
    {
        private readonly ModeloDatos.IModelos.IBeneficiosPostulacion _IBeneficiosPostulacion;

        public BeneficioPostulacionController(IBeneficiosPostulacion IBeneficiosPostulacion)
        {
            _IBeneficiosPostulacion = IBeneficiosPostulacion;
        }

        #region Consultar Beneficios de la postulacion
        [HttpGet("Consultar_PostulacionBeneficios")]
        [ProducesResponseType<DataResponse<List<BeneficiosPostulacionDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostulacionBeneficios()
        {
            try
            {
                List<BeneficiosPostulacionDTO> beneficios = await _IBeneficiosPostulacion.ConsultarPostulacionBeneficios();
                if (beneficios != null)
                {
                    return Ok(new DataResponse<List<BeneficiosPostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Beneficios por nombre o nombre de la convocatoria
        [HttpGet("Consultar_BeneficiosPostulacionGeneral")]
        [ProducesResponseType<DataResponse<BeneficiosPostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosPostulacionGeneral(string? nombreBeneficio = null, string? nombreConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombreBeneficio) == null && string.IsNullOrEmpty(nombreConvocatoria))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros: nombre de beneficio o nombre convocatoria.");
                }

                List<BeneficiosPostulacionDTO> beneficios = await _IBeneficiosPostulacion.ConsultarBeneficiosPostulacionGeneral(nombreBeneficio, nombreConvocatoria);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<BeneficiosPostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Beneficios por nombre o nombre de la convocatoria
        [HttpGet("Consultar_BeneficiosPostulacionEspecifico")]
        [ProducesResponseType<DataResponse<BeneficiosPostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarBeneficiosPostulacionEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el beneficio");
                }

                List<BeneficiosPostulacionDTO> beneficios = await _IBeneficiosPostulacion.ConsultarBeneficiosPostulacionEspecifico(id);
                if (beneficios != null)
                {

                    return Ok(new DataResponse<List<BeneficiosPostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.BusquedaExitosa,
                        Datos = beneficios
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Beneficio
        [HttpPost("crear_BeneficioPostulacion")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<BeneficiosPostulacionDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarBeneficios([FromBody] BeneficiosPostulacionDTO nuevoBeneficio)
        {
            try
            {
                bool result = await _IBeneficiosPostulacion.IngresarBeneficios(nuevoBeneficio);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Beneficio
        [HttpPut("actualiza_BeneficioPostulacion")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaBeneficios([FromBody] ModeloDatos.DTO.BeneficiosPostulacionDTO BeneficiosPostulacionDTO)
        {
            if (BeneficiosPostulacionDTO == null)
            {
                return BadRequest("Los datos de la solicitud no pueden ser nulos.");
            }
            if (BeneficiosPostulacionDTO.Id == null)
            {

                return BadRequest("el id de la solicitud no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool BeneficioExistente = await _IBeneficiosPostulacion.ActualizaBeneficios(BeneficiosPostulacionDTO);
                    if (BeneficioExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficiosPoastulacion.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeBeneficios.OcurrioError);
                }
            }

        }
        #endregion
    }
}
