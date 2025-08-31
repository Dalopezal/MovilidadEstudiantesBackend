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
    /// <summary>
    /// Daniel Alejandro Lopez Ciudad
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : Controller
    {
        private readonly ModeloDatos.IModelos.IPais _IPais;

        public PaisController(IPais Pais)
        {
            _IPais = Pais;
        }

        #region Consultar Pais
        [HttpGet("Consultar_Pais")]
        [ProducesResponseType<DataResponse<List<PaisDTO>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPaisConvenio()
        {
            try
            {
                List<PaisDTO> pais = await _IPais.ConsultarPaisConvenio();
                if (pais != null)
                {
                    return Ok(new DataResponse<List<PaisDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePais.BusquedaExitosa,
                        Datos = pais
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePais.BusquedaErrada);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModeloDatos.Utilidades.Mensaje.ErrorGeneral);
            }
        }
        #endregion



        #region Consultar pais por Id 
        [HttpGet("Consultar_PaisEspecifico")]
        [ProducesResponseType<DataResponse<PaisDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarPaisEspecifico(int? id = null)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) == null && id != null)
                {
                    return BadRequest("Debe especificar el id de la pais");
                }

                List<PaisDTO> pais = await _IPais.ConsultarPaisEspecifico(id);
                if (pais != null)
                {

                    return Ok(new DataResponse<List<PaisDTO>>
                    {
                        Exito = ModeloDatos.Utilidades.Mensaje.MensajePais.BusquedaExitosa,
                        Datos = pais
                    });
                }
                else
                {
                    return NotFound(ModeloDatos.Utilidades.Mensaje.MensajePais.BusquedaErrada);
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
