using ControlAccesoPersonal.Clases;
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
    /// para trabajar con los registros diarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin")]

    public class RegistroDiarioAPIController : ControllerBase
    {
        #region Declaracioens necesarias
        /// <summary>
        /// para nuestra comunicacion con database
        /// </summary>
        private ContextAux BD;

        #endregion

        #region CRUD basico
        /// <summary>
        /// se ingresa el elemento deseado y se insertan los datos, si el registro no existe de crea uno nuevo
        /// si existe simplemente se agrega la proxima hora neecsaria
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>

        //[HttpPost]
        //public async Task<RegistroDiario> Post(insertRegistroDiario insert)
        //{
        //    try
        //    {
        //        BD = new();
        //        return await BD.InsertarRegistro(insert);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "register")]
        [HttpPost]
        public async Task<bool> AddNew(RegistroDiario insert)
        {
            try
            {
                if (insert == null)
                    return false;

                BD = new();
                Mail m = new();

                if( await BD.PostRCH(insert))
                {
                 string contenido = "<h1>Inicio De Jornada Laboral </h1> <p> ingresaste a trabajar el dia:"+insert.fecha+" a las : "+insert.HoraI+" .  </p>";
                    var destino = await BD.DevolverCorreo(insert.Empleado);
                   
                   return  m.enviarCorreo(contenido, "Aviso de Inicio De Joranada Laboral",destino);
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        /// <summary>
        /// para realizar los demas marcajes de entradas y o salidas
        /// este registro no se permite editar ya que es unico
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "register")]
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, RegistroDiario insert)
        {
            try
            {
                if (insert == null)
                    return false;

                if (insert.HoraS == null && insert.HoraBI == null && insert.HoraBS == null)
                    return false;

                BD = new();

                if( await BD.PutRCH(id, insert))
                {
                    Mail m = new();
                    string contenido = "<h1>Marcas Laborales </h1> <p> Marcaste el dia:" + insert.fecha + " a las : " + insert.HoraI + " .  </p>";
                    var destino = await BD.DevolverCorreo(insert.Empleado);

                    return m.enviarCorreo(contenido, "Aviso De Nueva Marca", destino);
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// para obtener una lista de los registros activos en este momento
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor")]
        [HttpGet]
        public async Task<List<RegistroDiario>> Get()
        {
            try
            {
                BD = new();

                return await BD.GetRegistorsDiarios();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// para obtener un registro diario basado en su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")]
        [HttpGet("{id}")]

        public async Task<RegistroDiario> GetId(int id)
        {
            try
            {
                BD = new();

                return await BD.GetRegistroID(id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// para desactivar un registro, eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                BD = new();

                return await BD.DeleteRegistro(id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        #endregion

        #region retornos Especiales
        /// <summary>
        /// retonr una lista con el numero de registro iniciado por cada dia
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>
        [HttpPost("RegistroFechas/Numero")]
        public async Task<List<numeroRegistroFecha>> NumeroRegistroFechas(Fechas fe)
        {
            try
            {
                List<numeroRegistroFecha> lista = new();
                int flag = 0;
                BD = new();

                if ((fe.fechaInicio - fe.fechaFinal).Milliseconds > 0)
                    return null;

                for (DateTime recorrido = fe.fechaInicio; recorrido <= fe.fechaFinal; recorrido = recorrido.AddDays(1))
                {
                    flag = await BD.getNumeroDeReportesPorFecha(recorrido);
                    lista.Add(new numeroRegistroFecha(flag, recorrido));
                }

                return lista;
            }
            catch(Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }

        }

        /// <summary>
        /// para retornar un valor entre dos fechas asignadas
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>
        [HttpPost("entreFechas/")]
        public async Task<List<RegistroDiario>> EntreFechas(Fechas fe)
        {
            try
            {

                if (fe.fechaInicio > fe.fechaFinal)
                    return null;

                BD = new();

                return await BD.GetEntreFechas(fe);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// retorna todos los datos entre dos fechas asignadas
        /// pero retorna realmente es TODO
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,auditor,user")]
        [HttpGet("entreFechasTodo/")]
        public async Task<List<RegistroTotal>> EnreFechasTODO(Fechas fe)
        {
            List<RegistroTotal> ListaRetrono = new();
            int cont = 0;

            try
            {
                BD = new();

                AdmoHorarios admo = new();
                Horarios horario = new();
                Empleados empleado = new();
                Empresa empresa = new();
                PersonaHash flag = new();
                Persona persona = new();

                List<RegistroDiario> ListaRegistros = await BD.GetEntreFechas(fe);

                if (ListaRegistros == null)
                    return null;

                foreach (RegistroDiario reg in ListaRegistros)
                {



                    admo = await BD.GetAdmoHorariosIOd(reg.Empleado);
                    horario = await BD.GetHorarioId(admo.horario);
                    empleado = await BD.GetEmpleado(admo.Empleado);
                    empresa = await BD.getEmpresa(empleado.empresa);

                    flag = await BD.GetPersona(empleado.persona);
                    persona = flag.InformacionParaVistas(flag);


                    ListaRetrono.Add(new RegistroTotal()
                    {
                        num = cont,
                        AdmoHorario = admo,
                        persona = persona,
                        empresa = empresa,
                        Empleado = empleado,
                        horario = horario,
                        registro = reg
                    });

                    cont++;

                }




            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return ListaRetrono;
        }
        /// <summary>
        /// retorna el registro especifico de un dia en base al ID del empleado
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")] 
        [HttpGet("fechaIdEmpleado/{idEmpleado}")]
        //

        public async Task<RegistroDiario> GetFechaAndIdEmpleado(int idEmpleado, DateTime fecha)
        {
            BD = new();

            return await BD.getRegistrosPorFechaYIdEmpleado(idEmpleado, fecha);


        }





        #endregion

        #region Envio Mail || a cada contacto cuando sea tarde por 30 minutos
        
        /// <summary>
        /// para enviar un correo solo ingresdando el tipo
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
      
       

        #endregion

    }
}
