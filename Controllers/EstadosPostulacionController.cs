using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeloDatos.DTO;
using ModeloDatos.IModelos;
using ModeloDatos.Modelos;
using ModeloDatos.Utilidades;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosPostulacionController : Controller
    {
        private readonly ModeloDatos.IModelos.IEstadosPostulacion _IEstadosPostulacion;

        public EstadosPostulacionController(IEstadosPostulacion EstadosPostulacion)
        {
            _IEstadosPostulacion = EstadosPostulacion;
        }

        #region Consultar Estados
        [HttpGet("Consultar_Estado")]
        [ProducesResponseType<DataResponse<List<EstadoPostulacionDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarEstado()
        {
            try
            {
                List<EstadoPostulacionDTO> estado = await _IEstadosPostulacion.ConsultarEstado();
                if (estado != null)
                {
                    return Ok(new DataResponse<List<EstadoPostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEstado.BusquedaExitosa,
                        Datos = estado
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEstado.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion



        #region Consultar estado por Id 
        [HttpGet("Consulta_EstadoEspecifico")]
        [ProducesResponseType<DataResponse<EstadoPostulacionDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultaEstadoEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la estado");
                }

                List<EstadoPostulacionDTO> estado = await _IEstadosPostulacion.ConsultaEstadoEspecifico(id);
                if (estado != null)
                {

                    return Ok(new DataResponse<List<EstadoPostulacionDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajeEstado.BusquedaExitosa,
                        Datos = estado
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajeEstado.BusquedaErrada);
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
