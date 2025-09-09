using AccesoDatos.Movilidad;
using Apis.Contrats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [DisableCors]
    public class ConsulltaPostuladosTipoController : Controller
    {
        private readonly ModeloDatos.IModelos.IConsultaCategoriaMovilidad _IConsultaCategoriaMovilidad;

        public ConsulltaPostuladosTipoController(IConsultaCategoriaMovilidad ConsultaCategoriaMovilidad)
        {
            _IConsultaCategoriaMovilidad = ConsultaCategoriaMovilidad;
        }

        #region Consultar Postulados de una convocatoria por tipo de movilidad Entrante
        [HttpGet("Consultar_PostuladosTipoEntrante")]
        [ProducesResponseType<DataResponse<ConsultarConvocatoriaModalidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostuladosModalidadEntrante(int? idEstado = null,
            int? IdTipo = null, string? DocumentoPostulado = null, DateOnly? FechaInicioConvocatoria = null, DateOnly? FechaFinConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idEstado.ToString()) == null && string.IsNullOrEmpty(IdTipo.ToString()) && string.IsNullOrEmpty(DocumentoPostulado) && string.IsNullOrEmpty(FechaInicioConvocatoria.ToString())
                    && string.IsNullOrEmpty(FechaFinConvocatoria.ToString()))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros de busqueda.");
                }

                List<ConsultarConvocatoriaModalidadDTO> consulta = await _IConsultaCategoriaMovilidad.ConsultarPostuladosModalidadEntrante(idEstado,
                    IdTipo, DocumentoPostulado, FechaInicioConvocatoria, FechaFinConvocatoria );
                if (consulta != null)
                {

                    return Ok(new DataResponse<List<ConsultarConvocatoriaModalidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostuladosMovilidad.BusquedaExitosa,
                        Datos = consulta
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostuladosMovilidad.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion


        #region Consultar Postulados de una convocatoria por tipo de movilidad Entrante
        [HttpGet("Consultar_PostuladosTipoSaliente")]
        [ProducesResponseType<DataResponse<ConsultarConvocatoriaModalidadDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPostuladosModalidadSaliente(int? idEstado = null,
            int? IdTipo = null, string? DocumentoPostulado = null, DateOnly? FechaInicioConvocatoria = null, DateOnly? FechaFinConvocatoria = null)
        {
            try
            {

                if (string.IsNullOrEmpty(idEstado.ToString()) == null && string.IsNullOrEmpty(IdTipo.ToString()) && string.IsNullOrEmpty(DocumentoPostulado) && string.IsNullOrEmpty(FechaInicioConvocatoria.ToString())
                    && string.IsNullOrEmpty(FechaFinConvocatoria.ToString()))
                {
                    return BadRequest("Debe especificar al menos uno de los parámetros de busqueda.");
                }

                List<ConsultarConvocatoriaModalidadDTO> consulta = await _IConsultaCategoriaMovilidad.ConsultarPostuladosModalidadSaliente(idEstado,
                    IdTipo, DocumentoPostulado, FechaInicioConvocatoria, FechaFinConvocatoria);
                if (consulta != null)
                {

                    return Ok(new DataResponse<List<ConsultarConvocatoriaModalidadDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePostuladosMovilidad.BusquedaExitosa,
                        Datos = consulta
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePostuladosMovilidad.BusquedaErrada);
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
