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
    public class CiudadController : Controller
    {
        private readonly ModeloDatos.IModelos.ICiudad _ICiudad;

        public CiudadController(ICiudad Ciudad)
        {
            _ICiudad = Ciudad;
        }

        #region Consultar Ciudad 
        [HttpGet("Consultar_Ciudad")]
        [ProducesResponseType<DataResponse<List<CiudadDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCiudad()
        {
            try
            {
                List<CiudadDTO> ciudadMovilidad = await _ICiudad.ConsultarCiudad();
                if (ciudadMovilidad != null)
                {
                    return Ok(new DataResponse<List<CiudadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaExitosa,
                        Datos = ciudadMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar ciudad por nombre ciudad o nombre pais
        [HttpGet("Consultar_CiudadGeneral")]
        [ProducesResponseType<DataResponse<CiudadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCiudadGeneral(string? nombreCiudad = null, string? nombrePais = null)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreCiudad) == null || string.IsNullOrEmpty(nombrePais) == null)
                {
                    return BadRequest("Debe especificar el nombre del pais o el nombre de la ciudad.");
                }

                List<CiudadDTO> ciudadMovilidad = await _ICiudad.ConsultarCiudadGeneral(nombreCiudad, nombrePais);
                if (ciudadMovilidad != null)
                {

                    return Ok(new DataResponse<List<CiudadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaExitosa,
                        Datos = ciudadMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar ciudad por Id 
        [HttpGet("Consultar_CiudadEspecifico")]
        [ProducesResponseType<DataResponse<CiudadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCiudadEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id del pais");
                }

                List<CiudadDTO> ciudadMovilidad = await _ICiudad.ConsultarCiudadEspecifico(id);
                if (ciudadMovilidad != null)
                {

                    return Ok(new DataResponse<List<CiudadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaExitosa,
                        Datos = ciudadMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar ciudad por Id Pais
        [HttpGet("Consultar_CiudadEspecificoPais")]
        [ProducesResponseType<DataResponse<CiudadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCiudadPais(int? idPais = null)
        {
            try
            {

                if (idPais == null)
                {
                    return BadRequest("Debe especificar el id del pais");
                }

                List<CiudadDTO> ciudadMovilidad = await _ICiudad.ConsultarCiudadPais(idPais);
                if (ciudadMovilidad != null)
                {

                    return Ok(new DataResponse<List<CiudadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaExitosa,
                        Datos = ciudadMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCiudad.BusquedaErrada);
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
