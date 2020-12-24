using ControlAccesoPersonal.Context;
using ControlAccesoPersonal.DataTransferObjects;
using ControlAccesoPersonal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Controllers
{
    /// <summary>
    /// para trabajar con los dias feriados
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]

    public class FeriadosAPIController : ControllerBase
    {
        #region Declaraciones necesarias

        /// <summary>
        /// para la comunicacion con database
        /// </summary>
        private ContextAux BD;

        #endregion

        #region CRUD Basico
        /// <summary>
        /// para ingresar un feriado a la lista de elementos
        /// </summary>
        /// <param name="_f"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Post(Feriados _f)
        {
            try
            {
                BD = new();
                return await BD.PostFeriado(_f);

            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// para obtener un elemento por su id en base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Feriados> GetById(int id)
        {
            try
            {
                BD = new();

                return await BD.getFeriado(id);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// obtiene una lista de los elementos activos en base de datos
        /// </summary>
        /// <returns></returns>
        /// 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")]
        [HttpGet]
        public async Task<List<Feriados>> Get()
        {
            try
            {
                BD = new();

                return await BD.getFeriados();
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// para actualizar un Dia Feriado Existente en base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_f"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, Feriados _f)
        {
            try
            {
                if (id != _f.id)
                    return false;

                BD = new();

                return await BD.PutFeriado(id,_f);
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// para desactivar un elemeto en base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                if (id < 1)
                    return false;

                BD = new();

                return await BD.DeleteFeriados(id);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Extras o utiles
        /// <summary>
        /// retorna una lista de dias feriados que se encuentre entre dos fechas
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>
        /// 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor")]
        [HttpGet("entreFechas/")]
        public async Task<List<Feriados>> GetFeriadosEntreFechas(Fechas fe)
        {
            try
            {
                BD = new();
                return await BD.GetFeriadosEntreFechas(fe); 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        #endregion


    }
}
