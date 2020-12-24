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
    /// empleados
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor")]

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpleadoAPIController : ControllerBase
    {
        #region Declaraciones necesarias
        /// <summary>
        /// declaraciones de lo flujos de uso de nuestra base de datos
        /// </summary>
        private ContextAux BD;

        #endregion

        #region Crud Basica;

        /// <summary>
        /// para tomar una lista de los Empleados activos dentro de nuestra base de datos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<Empleados>> Get()
        {
            try
            {
                BD = new();
                return await BD.GetEmpleados(); 
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// retorna un elemento basado en su identificados de base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")]

        [HttpGet("{id}")]
        public async Task<Empleados> GetId(int id)
        {
            try
            {
                BD = new();
                return await BD.GetEmpleado(id);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// para insertar un empleado en la base de datos
        /// todos los valores tanto de empresa como de persona deben de existir
        /// no admite argumentos nulos
        /// true todo salio bien
        /// false todo salio mal
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]

        [HttpPost]
        public async Task<bool> Post(Empleados emp)
        {
            try
            {
                BD = new();
                return await BD.PostEmpleado(emp);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// actualiza un empleado en base de datos
        /// el id se para como parte de la url y tambien dentro del cuerpo
        /// falso es igual a nada sucedio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]
       
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, Empleados emp)
        {
            try
            {
                if (id < 1 || id != emp.id)
                    return false;

                BD = new();
                return await BD.PutEmpleado(id,emp);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// para desactivar el uso de un empleao de base de datos
        /// false, algo salio mal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]
        /// 
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                if (id < 1)
                    return false;

                BD = new();
                return await BD.DeleteEmpleado(id);
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}
