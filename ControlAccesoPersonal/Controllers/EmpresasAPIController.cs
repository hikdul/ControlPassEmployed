using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlAccesoPersonal.Models;
using ControlAccesoPersonal.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ControlAccesoPersonal.Controllers
{
    /// <summary>
    /// trabaja con las empresas 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]

    public class EmpresasAPIController : ControllerBase
    {

        #region deeclaracioens base

        /// <summary>
        /// nuestro medio de comunicacion con la base de datos
        /// </summary>
        private ContextAux BD;

        #endregion

        #region CRUD Basica

        /// <summary>
        /// para inserta una nueva empres en base de datos
        /// retorna true si se obtiene respuesta de base de datos o false si no la hubo
        /// si la empresa definina por RUT ya se encuentra registrada ignora el caso y no genera un nuevo ergistro
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Post(Empresa empresa)
        {
            BD = new();

            return await BD.postEmpresa(empresa);
        }


        /// <summary>
        /// para obtener una lista de todas las empress activas en nuestra base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Empresa>> GetAll()
        {
            BD = new();

            return await BD.getEmpresas();
        }

        /// <summary>
        /// para obtener una empresa por medio de su id 
        /// del modo api/Empresa/3
        /// </summary>
        /// <param name="id">identificador unico de la ubicacion dentro de la tabla</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor")]

        [HttpGet("{id}")]
        public async Task<Empresa> GetById(int id)
        {
            BD = new();

            return await BD.getEmpresa(id);
        }

        /// <summary>
        /// actualiza los datos de una empresa en base a su id
        /// si los datos son erroneos este elemento no procede
        /// retorna true si se hace llamado a la base de datos
        /// si el RUt no coincide con el registro, los datos no se actualizan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empresa"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<bool> Put(int id, Empresa empresa)
        {

            if (id < 1)
                return false;

            BD = new();

            return await BD.putEmpresa(id, empresa);
        }


        /// <summary>
        /// se desactiva el uso de esta empresa en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                return false;

            BD = new();

            return await BD.deleteEmpresa(id);
        }

        #endregion
    }
}
