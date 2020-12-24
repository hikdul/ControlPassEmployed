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

    // SuperAdmin, admin, auditor, user,register

    /// <summary>
    /// luego de generar un horarios desde aqui se puede administrar aque empleado se le asigna que horarrios y entre que fechas traba en estos horarios
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]
    public class AdministradoHorariosAPIController : ControllerBase
    {
        #region Declaraciones necesarias
        /// <summary>
        /// para la comunicacion con la base de datos
        /// </summary>
        private ContextAux BD;

        #endregion

        #region CRUD Basica


        /// <summary>
        /// obtiene una lista de los elements activos en base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AdmoHorarios>> Get()
        {
            try
            {
               
                BD = new();

                return await BD.GetAdmoHorarios();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// para obtener un elemento en base a su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AdmoHorarios> GetId(int id)
        {
            try
            {

                BD = new();

                return await BD.GetAdmoHorariosIOd(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// para insertar un elemento en base de datos
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<bool> Post(AdmoHorarios insert)
        {
            try {

                if (insert == null)
                    return false;

               //if( 7 != (insert.fechaInicio - insert.fechaAcaba).Days || insert.fechaInicio.DayOfWeek != DayOfWeek.Sunday || insert.fechaAcaba.DayOfWeek != DayOfWeek.Saturday)
               if( 7 > (insert.fechaAcaba - insert.fechaInicio).Days)// || insert.fechaInicio.DayOfWeek != DayOfWeek.Sunday || insert.fechaAcaba.DayOfWeek != DayOfWeek.Saturday)
                 return false;
                


                BD = new();

                return await BD.PostAH(insert);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// para actualizar un elemento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        [HttpPut("{id}")]

        public async Task<bool> Put(int id, AdmoHorarios insert)
        {
            try
            {

                if (insert == null)
                    return false;

                BD = new();

                return await BD.PuttAH(id,insert);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                return await BD.DeleteAdmoHorarios(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region usos Expeciales

        /// <summary>
        /// retorna el administrador de horarios mas el cumplimiento de los horarios detenidos en ese momento por el Empleado destino
        /// el id es del admo de horario, desde alli se toman todo sllos datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("all/{id}")]
        public async Task<AdmoDeHorariosDTO> GetDataAll(int id)
        {

            try
            {
                BD = new();

                return await BD.GetAdmoComplete(id);
            }
            catch(Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }

        }

        /// <summary>
        /// retorna los administradores existentes entre dos elementos
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>
        [HttpGet("entreFechas/")]
        public async Task<List<AdmoHorarios>> getListaEntreFechas(Fechas fe)
        {
            try
            {

                BD = new();

                return await BD.GetAdministradorEntreDosFechas(fe);


            }catch(Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }
        /// <summary>
        /// retorna al administrador de horarios mas actual entre dos fechas asignada y el id del empleado
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>

        [HttpGet("entrefechas/{idEmpleado}")]
        public async Task<AdmoHorarios> getByIdEmpleadoYFecha(int idEmpleado, DateTime fecha)
        {
            try
            {
                BD = new();
                return await BD.getAdmoHorarioIDEmpleadoYUnaFecha(idEmpleado, fecha);

            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        #endregion

      
    }
}
