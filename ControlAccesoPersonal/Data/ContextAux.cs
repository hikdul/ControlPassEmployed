using ControlAccesoPersonal.DataTransferObjects;
using ControlAccesoPersonal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Context
{
    /// <summary>
    /// donde respaldo toda mi comunicacion con base de datos
    /// </summary>
    public class ContextAux
    {

        #region declaracioens iniciales | conn

        private readonly string conn;
        /// <summary>
        /// iniciado mi contezto de datos
        /// </summary>
        public ContextAux()
        {


            conn = getConfiguration().GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// obtener la configuracion de mi contexto de datos
        /// </summary>
        /// <returns></returns>
        public IConfigurationRoot getConfiguration()
        {

            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return build.Build();
        }

        #endregion

        // ============================= ### =============================
        // aqui ira la Crud de mi tabla quecontiene los datos sobre la empresa
        // ============================= ### =============================

        #region Empresa 

        internal async Task<bool> postEmpresa(Empresa insert)
        {

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@nombre", insert.nombre);
            parametros[1] = new SqlParameter("@rut", insert.rut);
            parametros[2] = new SqlParameter("@description", insert.description);

            return await CustomProcedures.ProcedureBoolean<Empresa>("PostEmpresa", conn, parametros);

        }

        internal async Task<List<Empresa>> getEmpresas()
        {
            return await CustomProcedures.GetAll<Empresa>("GetEmpresas", conn);
        }

        internal async Task<Empresa> getEmpresa(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<Empresa>("GetEmpresa", conn, parametros);

        }

        internal async Task<bool> putEmpresa(int id, Empresa insert)
        {
            if (id < 1)
                return false;

            SqlParameter[] parametros = new SqlParameter[4];
            parametros[3] = new SqlParameter("@id", id);
            parametros[0] = new SqlParameter("@nombre", insert.nombre);
            parametros[1] = new SqlParameter("@rut", insert.rut);
            parametros[2] = new SqlParameter("@description", insert.description);

            return await CustomProcedures.ProcedureBoolean<Empresa>("PutEmpresa", conn, parametros);

        }


        internal async Task<bool> deleteEmpresa(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<Empresa>("DeleteEmpresa", conn, parametros);

        }

        #endregion

        // ============================= ### =============================
        // aqui iran los componenetes de Crud para PersonasHAsh
        // ============================= ### =============================

        #region Persona

        internal async Task<bool> PostPersona(PersonaHash _p)
        {
            if (_p == null)
                return false;

            SqlParameter[] parametros = new SqlParameter[8];
            parametros[0] = new SqlParameter("@rut", _p.rut);
            parametros[1] = new SqlParameter("@nombre", _p.nombre);
            parametros[2] = new SqlParameter("@nombre2", _p.nombre2);
            parametros[3] = new SqlParameter("@apellido", _p.apellido);
            parametros[4] = new SqlParameter("@apellido2", _p.apellido2);
            parametros[5] = new SqlParameter("@telefono", _p.telefono);
            parametros[6] = new SqlParameter("@correo", _p.correo);
            parametros[7] = new SqlParameter("@salt", _p.salt);


            return await CustomProcedures.ProcedureBoolean<PersonaHash>("PostPersona", conn, parametros);

        }

        internal async Task<bool> PutPersona(int id, PersonaHash _p)
        {
            if (_p == null)
                return false;
            if (id != _p.id)
                return false;

            SqlParameter[] parametros = new SqlParameter[9];
            parametros[8] = new SqlParameter("@id", id);
            parametros[0] = new SqlParameter("@rut", _p.rut);
            parametros[1] = new SqlParameter("@nombre", _p.nombre);
            parametros[2] = new SqlParameter("@nombre2", _p.nombre2);
            parametros[3] = new SqlParameter("@apellido", _p.apellido);
            parametros[4] = new SqlParameter("@apellido2", _p.apellido2);
            parametros[5] = new SqlParameter("@telefono", _p.telefono);
            parametros[6] = new SqlParameter("@correo", _p.correo);
            parametros[7] = new SqlParameter("@salt", _p.salt);


            return await CustomProcedures.ProcedureBoolean<PersonaHash>("PutPersona", conn, parametros);

        }


        internal async Task<List<PersonaHash>> GetPersonas()
        {
            return await CustomProcedures.GetAll<PersonaHash>("GetPersonas", conn);
        }

        internal async Task<PersonaHash> GetPersona(int id)
        {

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);
            return await CustomProcedures.GetByParameters<PersonaHash>("GetPersona", conn, parametros);
        }
        internal async Task<bool> DeletePersona(int id)
        {

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);
            return await CustomProcedures.ProcedureBoolean<PersonaHash>("DeletePersona", conn, parametros);
        }

        #endregion

        // ============================= ### =============================
        // Empleado
        // ============================= ### =============================

        #region Empleado

        internal  async Task<string> DevolverCorreo(int IdEmpleado)
        {
            try
            {
                Empleados _e = await GetEmpleado(IdEmpleado);
                PersonaHash _P = await GetPersona(_e.persona);
                Persona p = _P.InformacionParaVistas(_P);

                return p.correo;
            }
            catch
            {
                return "";
            }
        }

        public async Task<bool> PostEmpleado(Empleados emp)
        {

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@persona",emp.persona);
            parametros[1] = new SqlParameter("@empresa", emp.empresa);
            parametros[2] = new SqlParameter("@sueldo", emp.sueldo);
            parametros[3] = new SqlParameter("@articulo", emp.articulo);
            parametros[4] = new SqlParameter("@cargo", emp.cargo);

            return await CustomProcedures.ProcedureBoolean<Empleados>("PostEmpleado", conn, parametros);

        }

        public async Task<bool> PutEmpleado(int id, Empleados emp)
        {

            if (id != emp.id || id < 1)
                return false;

            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@persona", emp.persona);
            parametros[1] = new SqlParameter("@empresa", emp.empresa);
            parametros[2] = new SqlParameter("@sueldo", emp.sueldo);
            parametros[3] = new SqlParameter("@articulo", emp.articulo);
            parametros[4] = new SqlParameter("@cargo", emp.cargo);
            parametros[5] = new SqlParameter("@id", id);



            return await CustomProcedures.ProcedureBoolean<Empleados>("PutEmpleado", conn, parametros);
        }

        public async Task<List<Empleados>> GetEmpleados()
        {
            return await CustomProcedures.GetAll<Empleados>("getEmpleados", conn);
        }

        public async Task<Empleados> GetEmpleado(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<Empleados>("getEmpleado", conn, parametros);
        
        }

        public async Task<bool> DeleteEmpleado(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<Empleados>("DeleteEmpleado", conn, parametros);
        }

        #endregion

        // ============================= ### =============================
        //  feriados
        // ============================= ### =============================

        #region Feriados

        internal async Task<List<Feriados>> GetFeriadosEntreFechas(Fechas _f)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@fechaInicio", _f.fechaInicio);
            parametros[1] = new SqlParameter("@fechaFinal", _f.fechaFinal);

            return await CustomProcedures.GetAllByParameter<Feriados>("getFeriadosEntreFechas",conn, parametros);



        }

        public async Task<bool> PostFeriado(Feriados _f)
        {

            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@fecha", _f.fecha);
            parametros[1] = new SqlParameter("@nombre", _f.nombre);
            parametros[2] = new SqlParameter("@description", _f.description);
            parametros[3] = new SqlParameter("@anual", _f.anual);

            return await CustomProcedures.ProcedureBoolean<Feriados>("postFeriado", conn, parametros);

        }

        public async Task<bool> PutFeriado(int id, Feriados _f)
        {

            if (id != _f.id)
                return false;

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[4] = new SqlParameter("@id", id);
            parametros[0] = new SqlParameter("@fecha", _f.fecha);
            parametros[1] = new SqlParameter("@nombre", _f.nombre);
            parametros[2] = new SqlParameter("@description", _f.description);
            parametros[3] = new SqlParameter("@anual", _f.anual);

            return await CustomProcedures.ProcedureBoolean<Feriados>("putFeriado", conn, parametros);

        }

        public async Task<List<Feriados>> getFeriados()
        {
            return await CustomProcedures.GetAll<Feriados>("getFeriados", conn);
        }

        public async Task<Feriados> getFeriado(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<Feriados>("getFeriado", conn, parametros);
        }

        public async Task<bool> DeleteFeriados(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<Feriados>("deleteFeriado", conn, parametros);
        }

       

    #endregion

    // ============================= ### =============================
    // Horarios
    // ============================= ### =============================

    #region Horarios
    internal async Task<bool> PostHorarios(Horarios insert)
        {
            SqlParameter[] parametros = new SqlParameter[29];


            parametros[0] = new SqlParameter("@LHoraI", insert.Lunes.HoraI);
            parametros[1] = new SqlParameter("@LHoraS", insert.Lunes.HoraS);
            parametros[2] = new SqlParameter("@LHoraBS", insert.Lunes.HoraBS);
            parametros[3] = new SqlParameter("@LHoraBI", insert.Lunes.HoraBI);
            parametros[4] = new SqlParameter("@MHoraI", insert.Martes.HoraI);
            parametros[5] = new SqlParameter("@MHoraS", insert.Martes.HoraS);
            parametros[6] = new SqlParameter("@MHoraBS", insert.Martes.HoraBS);
            parametros[7] = new SqlParameter("@MHoraBI", insert.Martes.HoraBI);
            parametros[8] = new SqlParameter("@IHoraI", insert.Miercoles.HoraI);
            parametros[9] = new SqlParameter("@IHoraS", insert.Miercoles.HoraS);
            parametros[10] = new SqlParameter("@IHoraBS", insert.Miercoles.HoraBS);
            parametros[11] = new SqlParameter("@IHoraBI", insert.Miercoles.HoraBI);
            parametros[12] = new SqlParameter("@JHoraI", insert.Jueves.HoraI);
            parametros[13] = new SqlParameter("@JHoraS", insert.Jueves.HoraS);
            parametros[14] = new SqlParameter("@JHoraBS", insert.Jueves.HoraBS);
            parametros[15] = new SqlParameter("@JHoraBI", insert.Jueves.HoraBI);
            parametros[16] = new SqlParameter("@VHoraI", insert.Viernes.HoraI);
            parametros[17] = new SqlParameter("@VHoraS", insert.Viernes.HoraS);
            parametros[18] = new SqlParameter("@VHoraBI", insert.Viernes.HoraBI);
            parametros[19] = new SqlParameter("@VHoraBS", insert.Viernes.HoraBS);
            parametros[20] = new SqlParameter("@SHoraI", insert.Sabado.HoraI);
            parametros[21] = new SqlParameter("@SHoraS", insert.Sabado.HoraS);
            parametros[22] = new SqlParameter("@SHoraBS", insert.Sabado.HoraBS);
            parametros[23] = new SqlParameter("@SHoraBI", insert.Sabado.HoraBI);
            parametros[24] = new SqlParameter("@DHoraI", insert.Domingo.HoraI);
            parametros[25] = new SqlParameter("@DHoraS", insert.Domingo.HoraS);
            parametros[26] = new SqlParameter("@DHoraBS", insert.Domingo.HoraBS);
            parametros[27] = new SqlParameter("@DHoraBI", insert.Domingo.HoraBI);
            parametros[28] = new SqlParameter("@nombre", insert.nombre);

            return await CustomProcedures.ProcedureBoolean<Horarios>("postHorario", conn, parametros);
        }


        internal async Task<bool> PutHorario(int id ,Horarios insert)
        {

            if (id != insert.id)
                return false;

            SqlParameter[] parametros = new SqlParameter[30];


            parametros[0] = new SqlParameter("@LHoraI", insert.Lunes.HoraI);
            parametros[1] = new SqlParameter("@LHoraS", insert.Lunes.HoraS);
            parametros[2] = new SqlParameter("@LHoraBS", insert.Lunes.HoraBS);
            parametros[3] = new SqlParameter("@LHoraBI", insert.Lunes.HoraBI);
            parametros[4] = new SqlParameter("@MHoraI", insert.Martes.HoraI);
            parametros[5] = new SqlParameter("@MHoraS", insert.Martes.HoraS);
            parametros[6] = new SqlParameter("@MHoraBS", insert.Martes.HoraBS);
            parametros[7] = new SqlParameter("@MHoraBI", insert.Martes.HoraBI);
            parametros[8] = new SqlParameter("@IHoraI", insert.Miercoles.HoraI);
            parametros[9] = new SqlParameter("@IHoraS", insert.Miercoles.HoraS);
            parametros[10] = new SqlParameter("@IHoraBS", insert.Miercoles.HoraBS);
            parametros[11] = new SqlParameter("@IHoraBI", insert.Miercoles.HoraBI);
            parametros[12] = new SqlParameter("@JHoraI", insert.Jueves.HoraI);
            parametros[13] = new SqlParameter("@JHoraS", insert.Jueves.HoraS);
            parametros[14] = new SqlParameter("@JHoraBS", insert.Jueves.HoraBS);
            parametros[15] = new SqlParameter("@JHoraBI", insert.Jueves.HoraBI);
            parametros[16] = new SqlParameter("@VHoraI", insert.Viernes.HoraI);
            parametros[17] = new SqlParameter("@VHoraS", insert.Viernes.HoraS);
            parametros[18] = new SqlParameter("@VHoraBS", insert.Viernes.HoraBS);
            parametros[19] = new SqlParameter("@VHoraBI", insert.Viernes.HoraBI);
            parametros[20] = new SqlParameter("@SHoraI", insert.Sabado.HoraI);
            parametros[21] = new SqlParameter("@SHoraS", insert.Sabado.HoraS);
            parametros[22] = new SqlParameter("@SHoraBS", insert.Sabado.HoraBS);
            parametros[23] = new SqlParameter("@SHoraBI", insert.Sabado.HoraBI);
            parametros[24] = new SqlParameter("@DHoraI", insert.Domingo.HoraI);
            parametros[25] = new SqlParameter("@DHoraS", insert.Domingo.HoraS);
            parametros[26] = new SqlParameter("@DHoraBS", insert.Domingo.HoraBS);
            parametros[27] = new SqlParameter("@DHoraBI", insert.Domingo.HoraBI);
            parametros[28] = new SqlParameter("@nombre", insert.nombre);
            parametros[29] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<Horarios>("putHorario", conn, parametros);
        }

        internal async Task<List<Horarios>> GetHorarios()
        {

            List<Horarios> Lista = new List<Horarios>();
            SqlConnection Conex = new(conn);
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCmd = new SqlCommand("gethorarios",Conex);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                await Conex.OpenAsync();// Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                   

                    Horarios valor = new Horarios();
                    valor.id = (int)dr["id"];
                    valor.nombre = dr["nombre"].ToString();
                    valor.act = (bool)dr["act"];
                    valor.Lunes.HoraI = dr["LHoraI"].ToString();
                    valor.Lunes.HoraS = dr["LHoraS"].ToString();
                    valor.Lunes.HoraBS = dr["LHoraBS"].ToString();
                    valor.Lunes.HoraBI = dr["LHoraBI"].ToString();
                    valor.Martes.HoraI = dr["MHoraI"].ToString();
                    valor.Martes.HoraS = dr["MHoraS"].ToString();
                    valor.Martes.HoraBS = dr["MHoraBS"].ToString();
                    valor.Martes.HoraBI = dr["MHoraBI"].ToString();
                    valor.Miercoles.HoraI = dr["IHoraI"].ToString();
                    valor.Miercoles.HoraS = dr["IHoraS"].ToString();
                    valor.Miercoles.HoraBS = dr["IHoraBS"].ToString();
                    valor.Miercoles.HoraBI = dr["IHoraBI"].ToString();
                    valor.Jueves.HoraI = dr["JHoraI"].ToString();
                    valor.Jueves.HoraS = dr["JHoraS"].ToString();
                    valor.Jueves.HoraBS = dr["JHoraBS"].ToString();
                    valor.Jueves.HoraBI = dr["JHoraBI"].ToString();
                    valor.Viernes.HoraI = dr["VHoraI"].ToString();
                    valor.Viernes.HoraS = dr["VHoraS"].ToString();
                    valor.Viernes.HoraBS = dr["VHoraBS"].ToString();
                    valor.Viernes.HoraBI = dr["VHoraBI"].ToString();
                    valor.Sabado.HoraI = dr["SHoraI"].ToString();
                    valor.Sabado.HoraS = dr["SHoraS"].ToString();
                    valor.Sabado.HoraBS = dr["SHoraBS"].ToString();
                    valor.Sabado.HoraBI = dr["SHoraBI"].ToString();
                    valor.Domingo.HoraI = dr["DHoraI"].ToString();
                    valor.Domingo.HoraS = dr["DHoraS"].ToString();
                    valor.Domingo.HoraBS = dr["DHoraBS"].ToString();
                    valor.Domingo.HoraBI = dr["DHoraBI"].ToString();



                    Lista.Add(valor);
                }
                await Conex.CloseAsync(); // Close();
            }
            catch (SqlException ex)
            {
                await Conex.CloseAsync();
                Console.WriteLine( ex.Message);
            }

            return Lista;
        }


        internal async Task<Horarios> GetHorarioId(int id)
        {
            if (id < 1)
                return null;

            Horarios respose = new();
            try
            {
                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("getHorarioId", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                      
                            cmd.Parameters.Add( new SqlParameter("@id",id));
                      
                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {

                                //respose = MapToValue(lector);
                                respose.id = (int)lector["id"];
                                respose.nombre = lector["nombre"].ToString();
                                respose.act = (bool)lector["act"];
                                respose.Lunes.HoraI = lector["LHoraI"].ToString();
                                respose.Lunes.HoraS = lector["LHoraS"].ToString();
                                respose.Lunes.HoraBS = lector["LHoraBS"].ToString();
                                respose.Lunes.HoraBI = lector["LHoraBI"].ToString();
                                respose.Martes.HoraI = lector["MHoraI"].ToString();
                                respose.Martes.HoraS = lector["MHoraS"].ToString();
                                respose.Martes.HoraBS = lector["MHoraBS"].ToString();
                                respose.Martes.HoraBI = lector["MHoraBI"].ToString();
                                respose.Miercoles.HoraI = lector["IHoraI"].ToString();
                                respose.Miercoles.HoraS = lector["IHoraS"].ToString();
                                respose.Miercoles.HoraBS = lector["IHoraBS"].ToString();
                                respose.Miercoles.HoraBI = lector["IHoraBI"].ToString();
                                respose.Jueves.HoraI = lector["JHoraI"].ToString();
                                respose.Jueves.HoraS = lector["JHoraS"].ToString();
                                respose.Jueves.HoraBS = lector["JHoraBS"].ToString();
                                respose.Jueves.HoraBI = lector["JHoraBI"].ToString();
                                respose.Viernes.HoraI = lector["VHoraI"].ToString();
                                respose.Viernes.HoraS = lector["VHoraS"].ToString();
                                respose.Viernes.HoraBS = lector["VHoraBS"].ToString();
                                respose.Viernes.HoraBI = lector["VHoraBI"].ToString();
                                respose.Sabado.HoraI = lector["SHoraI"].ToString();
                                respose.Sabado.HoraS = lector["SHoraS"].ToString();
                                respose.Sabado.HoraBS = lector["SHoraBS"].ToString();
                                respose.Sabado.HoraBI = lector["SHoraBI"].ToString();
                                respose.Domingo.HoraI = lector["DHoraI"].ToString();
                                respose.Domingo.HoraS = lector["DHoraS"].ToString();
                                respose.Domingo.HoraBS = lector["DHoraBS"].ToString();
                                respose.Domingo.HoraBI = lector["DHoraBI"].ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return respose;

        }

  
        internal async Task<bool> DeleteHorario(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);
            return await CustomProcedures.ProcedureBoolean<Horarios>("deleteHorario", conn, parametros);
        }

        //private Horarios MapToValue(SqlDataReader dr)
        //{
        //    Horarios valor = new Horarios();
        //    valor.id = (int)dr["id"];
        //    valor.nombre = dr["nombre"].ToString();
        //    valor.act = (bool)dr["act"];
        //    valor.Lunes.HoraI = dr["LHoraI"].ToString();
        //    valor.Lunes.HoraS = dr["LHoraS"].ToString();
        //    valor.Lunes.HoraBS = dr["LHoraBS"].ToString();
        //    valor.Lunes.HoraBI = dr["LHoraBI"].ToString();
        //    valor.Martes.HoraI = dr["MHoraI"].ToString();
        //    valor.Martes.HoraS = dr["MHoraS"].ToString();
        //    valor.Martes.HoraBI = dr["MHoraBS"].ToString();
        //    valor.Martes.HoraBS = dr["MHoraBI"].ToString();
        //    valor.Miercoles.HoraI = dr["IHoraI"].ToString();
        //    valor.Miercoles.HoraS = dr["IHoraS"].ToString();
        //    valor.Miercoles.HoraBS = dr["IHoraBS"].ToString();
        //    valor.Miercoles.HoraBI = dr["IHoraBI"].ToString();
        //    valor.Jueves.HoraI = dr["JHoraI"].ToString();
        //    valor.Jueves.HoraS = dr["JHoraS"].ToString();
        //    valor.Jueves.HoraBS = dr["JHoraBS"].ToString();
        //    valor.Jueves.HoraBI = dr["JHoraBI"].ToString();
        //    valor.Viernes.HoraI = dr["VHoraI"].ToString();
        //    valor.Viernes.HoraS = dr["VHoraS"].ToString();
        //    valor.Viernes.HoraBS = dr["VHoraBS"].ToString();
        //    valor.Viernes.HoraBI = dr["VHoraBI"].ToString();
        //    valor.Sabado.HoraI = dr["SHoraI"].ToString();
        //    valor.Sabado.HoraS = dr["SHoraS"].ToString();
        //    valor.Sabado.HoraBS = dr["SHoraBS"].ToString();
        //    valor.Sabado.HoraBI = dr["SHoraBI"].ToString();
        //    valor.Domingo.HoraI = dr["DHoraI"].ToString();
        //    valor.Domingo.HoraS = dr["DHoraS"].ToString();
        //    valor.Domingo.HoraBS = dr["DHoraBS"].ToString();
        //    valor.Domingo.HoraBI = dr["DHoraBI"].ToString();

        //    return valor;

        //}


        #endregion

        // ============================= ### =============================
        //  Administrado de Horarios
        // ============================= ### =============================

        #region Aministrador de horarios

        internal async Task<List<AdmoHorarios>> GetAdministradorEntreDosFechas(Fechas _f)
        {



            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@fechaInicio", _f.fechaInicio);
            parametros[1] = new SqlParameter("@fechaFinal", _f.fechaFinal);

            return await CustomProcedures.GetAllByParameter<AdmoHorarios>("getAdmoHorariosEntreFechas", conn, parametros);

        }

        internal async Task<bool> PostAH(AdmoHorarios insert)
        {

            SqlParameter[] parametros= new SqlParameter[4];
            parametros[0] = new SqlParameter("@empleado", insert.Empleado);
            parametros[1] = new SqlParameter("@horario", insert.horario);
            parametros[2] = new SqlParameter("@fechaInicio", insert.fechaInicio);
            parametros[3] = new SqlParameter("@fechaAcaba", insert.fechaAcaba);

            return await CustomProcedures.ProcedureBoolean<AdmoHorarios>("postAdminH", conn, parametros);
        }

        internal async Task<bool> PuttAH(int id,AdmoHorarios insert)
        {
            if(id != insert.id || id <1)
                return false;

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@empleado", insert.Empleado);
            parametros[1] = new SqlParameter("@horario", insert.horario);
            parametros[2] = new SqlParameter("@fechaInicio", insert.fechaInicio);
            parametros[3] = new SqlParameter("@fechaAcaba", insert.fechaAcaba);
            parametros[4] = new SqlParameter("@id",id);

            return await CustomProcedures.ProcedureBoolean<AdmoHorarios>("PutAdmoH", conn, parametros);
        }

        internal async Task<List<AdmoHorarios>> GetAdmoHorarios()
        {
            return await CustomProcedures.GetAll<AdmoHorarios>("getAdmoH", conn);
        }

        internal async Task<AdmoHorarios> GetAdmoHorariosIOd(int id)
        {
            if (id < 1)
                return null;

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<AdmoHorarios>("getAdmoHID", conn, parametros);
        }

        internal async Task<bool> DeleteAdmoHorarios(int id)
        {
            if (id < 1)
                return false;

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<AdmoHorarios>("deleteAdmoH", conn, parametros);
        }

        //operacioens especiales


        
        internal async Task<AdmoDeHorariosDTO> GetAdmoComplete(int id)
        {
            try
            {
                List<RegistroDiario> Lista = new();
                SqlParameter[] paramID = new SqlParameter[1];
                SqlParameter[] idHorario = new SqlParameter[1];

                paramID[0] = new SqlParameter("@id", id);

                AdmoHorarios admo = await CustomProcedures.GetByParameters<AdmoHorarios>("getAdmoHID", conn, paramID);
                idHorario[0] = new SqlParameter("@id", admo.horario);

                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("registrosEntreFechasP", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", admo.fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFinal", admo.fechaAcaba));
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", admo.Empleado));
                        //cmd.Parameters.Add(new SqlParameter("@admoHorarios", admoR ));


                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {
                                RegistroDiario obj = new();

                                obj.id = (int)lector["id"];
                                obj.Empleado = (int)lector["Empleado"];
                                obj.fecha = (DateTime)lector["fecha"];
                                obj.HoraI = lector["HoraI"].ToString();
                                obj.HoraS = lector["HoraS"].ToString();
                                obj.HoraBI = lector["HoraBI"].ToString();
                                obj.HoraBS = lector["HoraBS"].ToString();


                                Lista.Add(obj);

                            }
                        }
                    }
                }



                return new AdmoDeHorariosDTO()
                {
                    id = admo.id,
                    horario = admo.horario,
                    Empleado = admo.Empleado,
                    fechaInicio = admo.fechaInicio,
                    fechaAcaba = admo.fechaAcaba,
                    RegistrosDiarios = Lista
                };

              



            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }


        internal async Task<AdmoHorarios> getAdmoHorarioIDEmpleadoYUnaFecha(int idEmpleado, DateTime fecha)
        {

          

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@idEmpleado", idEmpleado);
            parametros[1] = new SqlParameter("@fecha", fecha);


            return await CustomProcedures.GetByParameters<AdmoHorarios>("getAdmoHorarioIDEmpleadoYUnaFecha", conn, parametros);

        }

        #endregion

        // ============================= ### =============================
        //  RCH o  registro de cumplimiento de horarios
        // ============================= ### =============================

        #region RCH

        //internal  async Task<RegistroDiario> InsertarRegistro(insertRegistroDiario insert)
        //{
        //    try
        //    {
        //        RegistroDiario obj = new();

        //        using (SqlConnection sql = new SqlConnection(conn))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("InsertMarcasRegistroDiario", sql))
        //            {
        //                cmd.Parameters.Add(new SqlParameter("@Empleado", insert.Empleado));
        //                cmd.Parameters.Add(new SqlParameter("@fecha", insert.fecha));
        //                cmd.Parameters.Add(new SqlParameter("@hora", insert.hora));
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                await sql.OpenAsync();

        //                using (var lector = await cmd.ExecuteReaderAsync())
        //                {

        //                    while (await lector.ReadAsync())
        //                    {

        //                        obj.id = (int)lector["id"];
        //                        obj.Empleado = (int)lector["Empleado"];
        //                        obj.fecha = (DateTime)lector["fecha"];
        //                        obj.HoraI = lector["HoraI"].ToString();
        //                        obj.HoraS = lector["HoraS"].ToString();
        //                        obj.HoraBI = lector["HoraBI"].ToString();
        //                        obj.HoraBS = lector["HoraBS"].ToString();



        //                    }
        //                }
        //            }
        //        }
        //       return obj;
        //    }
        //    catch(Exception x)
        //    {
        //        Console.WriteLine(x.Message);
        //        return null;
        //    }

        //}

        //getNumeroDeReportesPorFecha
        /// <summary>
        /// para que me devuelva el numero de registros echos en una fecha X cualquiera
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>
        internal async Task<int> getNumeroDeReportesPorFecha(DateTime fe)
        {
            try
            {
                int retorno=0;

                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("getNumeroDeReportesPorFecha", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@fecha", fe));
                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {
                                //var newObjeto = new T();
                                retorno = (int)lector[0];  
                                // return respose;

                            }
                        }
                        await sql.CloseAsync();
                    }

                }
                return retorno;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        /// <summary>
        /// para insertar un nuevo registro
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>
        internal async Task<bool> PostRCH(RegistroDiario insert)
        {
            try
            {
                SqlParameter[] Parametros = new SqlParameter[3];

                Parametros[0] = new SqlParameter("@Empleado", insert.Empleado);
                Parametros[1] = new SqlParameter("@fecha", insert.fecha);
                Parametros[2] = new SqlParameter("@HoraI", insert.HoraI);

                return await CustomProcedures.ProcedureBoolean<RegistroDiario>("PostRegistro", conn, Parametros);


            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        internal async Task<bool> PutRCH(int id, RegistroDiario insert)
        {

            if (String.IsNullOrEmpty(insert.HoraBI) && String.IsNullOrEmpty(insert.HoraBS) && String.IsNullOrEmpty(insert.HoraS))
                return false;
            if (id != insert.id)
                return false;

            SqlParameter[] parametros = new SqlParameter[2];
            
            parametros[0] = new SqlParameter("@id", id );


            if (!String.IsNullOrEmpty(insert.HoraS))
            {
                parametros[1] = new SqlParameter("@HoraS" , insert.HoraS == "" ? null : insert.HoraS);
                return await CustomProcedures.ProcedureBoolean<RegistroDiario>("putRegistrosHS", conn, parametros);
            }else if (!String.IsNullOrEmpty(insert.HoraBI))
            {
                parametros[1] = new SqlParameter("@HoraBI", insert.HoraBI == "" ? null : insert.HoraBI);
                return await CustomProcedures.ProcedureBoolean<RegistroDiario>("putRegistrosBI", conn, parametros);

            }

            parametros[1] = new SqlParameter("@HoraBS", insert.HoraBS == "" ? null : insert.HoraBS);
            return await CustomProcedures.ProcedureBoolean<RegistroDiario>("putRegistrosBS", conn, parametros);

        }


       

        internal async Task<List<RegistroDiario>> GetRegistorsDiarios()
        {

            List<RegistroDiario> respose = new();

            //return await CustomProcedures.GetAll<RegistroDiario>("getRegistros", conn);

            try
            {
                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("getRegistros", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {
                                RegistroDiario obj = new();

                                obj.id = (int)lector["id"];
                                obj.Empleado = (int)lector["Empleado"];
                                obj.fecha = (DateTime)lector["fecha"];
                                obj.HoraI = lector["HoraI"].ToString();
                                obj.HoraS = lector["HoraS"].ToString();
                                obj.HoraBI = lector["HoraBI"].ToString();
                                obj.HoraBS = lector["HoraBS"].ToString();
                                obj.DiaDeSemana = (int)((DateTime)lector["fecha"]).DayOfWeek;

                                respose.Add(obj);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return respose;
        }

        internal async Task<RegistroDiario> GetRegistroID(int id)
        {
            RegistroDiario Response = new();

            //return await CustomProcedures.GetByParameters<RegistroDiario>("getRegistroID", conn, parametros);

            try
            {
                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("getRegistroID", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id",id));
                        
                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {

                                Response.id = (int)lector["id"];
                                Response.Empleado = (int)lector["Empleado"];
                                Response.fecha = (DateTime)lector["fecha"];
                                Response.HoraI = lector["HoraI"].ToString();
                                Response.HoraS = lector["HoraS"].ToString();
                                Response.HoraBI = lector["HoraBI"].ToString();
                                Response.HoraBS = lector["HoraBS"].ToString();
                                Response.DiaDeSemana = (int)((DateTime)lector["fecha"]).DayOfWeek;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
                return Response;
           
        }

        internal async Task<bool> DeleteRegistro(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<RegistroDiario>("DeleteRegistros", conn, parametros);
        }

        // ============================= ### =============================
        // Operaciones Extras
        // ============================= ### =============================

        // => ## para obtener valores entre dos fechas ## <=
        internal async Task<List<RegistroDiario>> GetEntreFechas(Fechas fe)
        {
            List<RegistroDiario> resp = new();

            try
            {
                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("registrosEntreFechas", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fe.fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFinal", fe.fechaFinal));
                        //cmd.Parameters.Add(new SqlParameter("@admoHorarios", admoR ));
                        

                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {
                                RegistroDiario obj = new();

                                obj.id = (int)lector["id"];
                                obj.Empleado = (int)lector["Empleado"];
                                obj.fecha = (DateTime)lector["fecha"];
                                obj.HoraI = lector["HoraI"].ToString();
                                obj.HoraS = lector["HoraS"].ToString();
                                obj.HoraBI = lector["HoraBI"].ToString();
                                obj.HoraBS = lector["HoraBS"].ToString();
                                obj.DiaDeSemana = (int)((DateTime)lector["fecha"]).DayOfWeek;


                                resp.Add(obj);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return resp;
        }

        internal async Task<RegistroDiario> getRegistrosPorFechaYIdEmpleado(int idEmpleado, DateTime fecha)
        {
           

            RegistroDiario Response = new();

            try
            {
                using (SqlConnection sql = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("getRegistrosPorFechaYIdEmpleado", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));

                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {

                                Response.id = (int)lector["id"];
                                Response.Empleado = (int)lector["Empleado"];
                                Response.fecha = (DateTime)lector["fecha"];
                                Response.HoraI = lector["HoraI"].ToString();
                                Response.HoraS = lector["HoraS"].ToString();
                                Response.HoraBI = lector["HoraBI"].ToString();
                                Response.HoraBS = lector["HoraBS"].ToString();
                                Response.DiaDeSemana = (int) ((DateTime)lector["fecha"]).DayOfWeek;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return Response;



        }

        internal async Task<List<RegistroDiario>> getRegistrosPorFecha(DateTime fecha)
        {
            

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@fecha", fecha);

            return await CustomProcedures.GetAllByParameter<RegistroDiario>("getRegistrosPorFecha", conn, parametros);

        }


        #endregion

        // ============================= ### =============================
        //
        // ============================= ### =============================
    }

}
       



