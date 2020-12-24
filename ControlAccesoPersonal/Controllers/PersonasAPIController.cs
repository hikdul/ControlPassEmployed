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
    /// para trabajar con cada individuo o persona 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]

    public class PersonasAPIController : ControllerBase
    {

        #region Declaracioens Inmiciales
        /// <summary>
        /// para poder tener comunicacion con mi base de datos
        /// </summary>

        private ContextAux BD;

        #endregion


        #region Crud Basico

        /// <summary>
        /// para obtener una lista de los elementos
        /// en caso de error retorna null
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor")]
        [HttpGet]
        public async Task<List<Persona>> Get()
        {

            try
            {
                BD = new();
                List<PersonaHash> lista = await BD.GetPersonas();

                List<Persona> retorno = new();

                foreach (var item in lista)
                {
                    retorno.Add(item.InformacionParaVistas(item));
                }

                return retorno;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// para obtener un elemento en base a su id
        /// si el elemento no existe retorna null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")]

        [HttpGet("{id}")]
        public async Task<Persona> GetById(int id)
        {
            try
            {
                if (id < 1)
                    return null;

                BD = new();

                PersonaHash p = await BD.GetPersona(id);

                if (p == null)
                    return null;

                Persona ret = p.InformacionParaVistas(p);

                return ret;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// para insertar un elemento en la base de datos
        /// </summary>
        /// <param name="_P"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Post(Persona _P)
        {
            try
            {
                if (_P == null)
                    return false;

                BD = new();

                PersonaHash insert = new PersonaHash();
                insert = insert.ParaBD(_P);

                return await BD.PostPersona(insert);
            }
            catch
            {
                return false;
            }


        }
        /// <summary>
        /// para actualizar todos los elementos de un objetivo
        /// el id debe venir n los dos datos para ser valido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_P"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<bool> Put(int id, Persona _P)
        {
            try
            {
                if (id != _P.id)
                    return false;

                PersonaHash hash = new();

                hash = hash.ParaBD(_P);

                BD = new();
                return await BD.PutPersona(id, hash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// para eliminar un elemento de la lista de activos en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {

            try {
                if (id < 1)
                    return false;

                BD = new();

                return await BD.DeletePersona(id);

            }
            catch
            {
                return false;
            }
            
            
        }



        #endregion

    }
}
