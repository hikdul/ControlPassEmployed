using ControlAccesoPersonal.Context;
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
    /// trabaja con los Horarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]

    public class HorariosAPIController : ControllerBase
    {
        #region Declaracioens necesarias

        /// <summary>
        /// poara nuestra comunicacion con base de datos
        /// </summary>
        private ContextAux BD;

        #endregion

        #region Crud Basico

        /// <summary>
        /// para obtener una lista de los horarios activos en base de datos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<Horarios>> Get()
        {
            try
            {
                

                BD = new();
                return await BD.GetHorarios(); 
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// para obtener un horaios por medio de su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")]
        [HttpGet("{id}")]
        public async Task<Horarios> GetById(int id)
        {
            try
            {
                BD = new();
                return await BD.GetHorarioId(id);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// para agregar unnnuevo horario en BD
        /// </summary>
        /// <param name="Insert"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Post(Horarios Insert)
        {
            try
            {
                BD = new();
                return await BD.PostHorarios(Insert);
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// para actualizar un elemeto existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_h"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, Horarios _h)
        {
            try
            {
                if(id != _h.id)
                    return false;

                BD = new();
                return await BD.PutHorario(id, _h);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// para eliminar un elemento
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
                return await BD.DeleteHorario(id);
            }
            catch
            {
                return false;
            }
        }



        #endregion
    }
}
