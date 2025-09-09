using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class EntregablePostulacionController : Controller
    {
        private readonly ModeloDatos.IModelos.IEntregablePostulacion _IEntregablePostulacion;

        public EntregablePostulacionController(IEntregablePostulacion EntregablePostulacion)
        {
            _IEntregablePostulacion = EntregablePostulacion;
        }


        #region Consultar Entregables Postulacion por Id Postulacion
        [HttpGet("Consultar_EntregablePostulacion")]
        [ProducesResponseType<DataResponse<EntregablePostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarEntregablePostulacion(int? idPostulacion = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idPostulacion.ToString()) == null && idPostulacion == null)
                {
                    return BadRequest("Debe especificar la postulacion");
                }

                List<EntregablePostulacionDTO> entregables = await _IEntregablePostulacion.ConsultarEntregablePostulacion(idPostulacion);
                if (entregables != null)
                {

                    return Ok(new DataResponse<List<EntregablePostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.BusquedaExitosa,
                        Datos = entregables
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Crear Beneficio
        [HttpPost("Ingresar_EntregablePostulacion")]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<DataResponse<EntregablePostulacionDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> IngresarEntregablePostulacion([FromBody] EntregablePostulacionDTO nuevo)
        {
            try
            {
                bool result = await _IEntregablePostulacion.IngresarEntregablePostulacion(nuevo);

                if (result == false)
                {

                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.CreacionErrada);
                }

                return Ok(new DataResponse<bool>
                {
                    Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.CreacionExitosa,
                    Datos = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.OcurrioError);
            }

        }
        #endregion

        #region Actualizar Beneficio
        [HttpPut("Actualiza_EntregablePostulacion")]
        [ProducesResponseType<DataResponse<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaEntregablePostulacion([FromBody] ModeloDatos.DTO.EntregablePostulacionDTO EntregablePostulacionDTO)
        {
            if (EntregablePostulacionDTO == null)
            {
                return BadRequest("Los datos del entregable no pueden ser nulos.");
            }
            if (EntregablePostulacionDTO.Id == null)
            {

                return BadRequest("el id del entregable no pueden ser nulos.");
            }
            else
            {
                try
                {

                    bool BeneficioExistente = await _IEntregablePostulacion.ActualizaEntregablePostulacion(EntregablePostulacionDTO);
                    if (BeneficioExistente)
                    {
                        return Ok(new DataResponse<bool>
                        {
                            Exito = ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.ActualizacionExitosa,
                            Datos = true
                        });
                    }
                    else
                    {
                        return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.ActualizacionErrada);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ModeloDatos.Utilidades.Mensaje.MensajeEntregablesPostulacion.OcurrioError);
                }
            }

        }
        #endregion
    }
}
