using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Mvc;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;

namespace Apis.Controllers
{
    /// <summary>
    /// Daniel Alejandro Lopez Categoria Movilidad
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaMovilidadController : Controller
    {
        private readonly ModeloDatos.IModelos.ICategoriaMovilidad _ICategoriaMovilidad;

        public CategoriaMovilidadController(ICategoriaMovilidad CategoriaMovilidad)
        {
            _ICategoriaMovilidad = CategoriaMovilidad;
        }

        #region Consultar Categoria Movilidad
        [HttpGet("Consultar_CategoriaMovilidad")]
        [ProducesResponseType<DataResponse<List<CategoriaMovilidadDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCategoriaMovilidad()
        {
            try
            {
                List<CategoriaMovilidadDTO> categoriaMovilidad = await _ICategoriaMovilidad.ConsultarCategoriaMovilidad();
                if (categoriaMovilidad != null)
                {
                    return Ok(new DataResponse<List<CategoriaMovilidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCategoriaMovilidad.BusquedaExitosa,
                        Datos = categoriaMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCategoriaMovilidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar Categoria Movilidad por nombre 
        [HttpGet("Consultar_CategoriaMovilidadGeneral")]
        [ProducesResponseType<DataResponse<CategoriaMovilidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCategoriaMovilidadGeneral(string? nombre= null)
        {
            try
            {

                if (string.IsNullOrEmpty(nombre) == null )
                {
                    return BadRequest("Debe especificar el nombre de la categoria movilidad.");
                }

                List<CategoriaMovilidadDTO> categoriaMovilidad = await _ICategoriaMovilidad.ConsultarCategoriaMovilidadGeneral(nombre);
                if (categoriaMovilidad != null)
                {

                    return Ok(new DataResponse<List<CategoriaMovilidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCategoriaMovilidad.BusquedaExitosa,
                        Datos = categoriaMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCategoriaMovilidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion

        #region Consultar Categoria Movilidad por Id 
        [HttpGet("Consultar_CategoriaMovilidadEspecifico")]
        [ProducesResponseType<DataResponse<CategoriaMovilidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarCategoriaMovilidadEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la categoria movilidad");
                }

                List<CategoriaMovilidadDTO> categoriaMovilidad = await _ICategoriaMovilidad.ConsultarCategoriaMovilidadEspecifico(id);
                if (categoriaMovilidad != null)
                {

                    return Ok(new DataResponse<List<CategoriaMovilidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeCategoriaMovilidad.BusquedaExitosa,
                        Datos = categoriaMovilidad
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeCategoriaMovilidad.BusquedaErrada);
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
