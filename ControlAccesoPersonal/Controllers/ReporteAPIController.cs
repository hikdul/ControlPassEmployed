using ControlAccesoPersonal.Clases;
using ControlAccesoPersonal.Context;
using ControlAccesoPersonal.DataTransferObjects;
using ControlAccesoPersonal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Controllers
{
    /// <summary>
    /// para generar mis reportes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, admin, auditor,user")]

    public class ReporteAPIController : ControllerBase
    {

        #region Declaracioens

        /// <summary>
        /// para comunicarnos con nuestra base de datos
        /// </summary>
        private ContextAux BD;

        #endregion


        #region Reporte en Json
        /// <summary>
        /// dandole las fechas  un identificador de empleado retorna un reporte con los datos existentes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <returns></returns>

        [HttpPost("{id}")]
        public async Task<InformeEntreFechas> GetInformePorEmpleadi(int id, Fechas fe)
        {
            return await GererarReporte(id, fe);
        }
        /// <summary>
        /// me ingresan dos fechas y retorna un valor con los resultados del informa solicitado
        /// </summary>
        /// <param name="fe"> un formato de dos fechas entre las cyuales se realizaan los calculos</param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<InformeEntreFechas>> GetInformeEntreFechasGnral(Fechas fe)
        {
            try
            {
                BD = new();
                List<InformeEntreFechas> retorno = new();
                List<Empleados> ListaEMpleados = await BD.GetEmpleados();

                if (ListaEMpleados == null)
                    return null;

                foreach (Empleados emp in ListaEMpleados)
                    retorno.Add(await GererarReporte(emp.id, fe));

                return retorno;
            }catch(Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }
        /// <summary>
        /// para obtener un reporte acompañado de los registros estudiados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <returns></returns>
        [HttpPost("detallado/{id}")]
        public async Task<InformeEntreFechasDetallado> GetDetalladoID(int id, Fechas fe)
        {
            return await GenerarReporteDetallado(id, fe);
        }



        #endregion

        #region reporte en Xml
       /// <summary>
       /// me devuelve un string que contiene un array de bites con la lista de empleados por activos en base de datos
       /// </summary>
       /// <param name="fe"></param>
       /// <returns></returns>
        [HttpPost("xml")]
        public async Task<string> reporteExcelGnral(Fechas fe)
        {
            try
            {
                BD = new();
                List<Empleados> empleados = await BD.GetEmpleados();
                List<InformeEntreFechas> informes = new();

                foreach(var emp in empleados)
                {
                    InformeEntreFechas inf = await GererarReporte(emp.id, fe);
                    informes.Add(inf);

                }

                using (MemoryStream ms = new MemoryStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        ep.Workbook.Worksheets.Add("hoja");
                        ExcelWorksheet ew = ep.Workbook.Worksheets[0];

                        int fila = 1;
                        //int columna = 1;


                        foreach (var elemento in informes)
                        {
                            fila++;

                            ew.Cells[fila, 1].Value = "Reporte del: ";
                            ew.Cells[fila, 2].Value = elemento.fechaInicioCalculo;
                            ew.Cells[fila, 3].Value = " Hasta";
                            ew.Cells[fila, 4].Value = elemento.fechaFinalCalculo;
                            fila++;
                            ew.Cells[fila, 1].Value = "Datos Empleado";
                            ew.Cells[fila, 2].Value = "cargo: ";
                            ew.Cells[fila, 3].Value = elemento.Empleado.cargo;
                            ew.Cells[fila, 4].Value = "articulo 77";
                            ew.Cells[fila, 5].Value = elemento.Empleado.articulo ? "Si Aplica" : "no Aplica";
                            fila++;
                            ew.Cells[fila, 1].Value = "Nombre";
                            ew.Cells[fila, 2].Value = elemento.Persona.nombre;
                            ew.Cells[fila, 3].Value = "Apellido";
                            ew.Cells[fila, 4].Value = elemento.Persona.apellido;
                            ew.Cells[fila, 5].Value = "Rut";
                            ew.Cells[fila, 6].Value = elemento.Persona.rut;
                            fila++;
                            ew.Cells[fila, 1].Value = "Horas Cumplidas";
                            ew.Cells[fila, 2].Value = elemento.HorasCumplidas;
                            ew.Cells[fila, 3].Value = "Horas Que Deberia CUmplir";
                            ew.Cells[fila, 4].Value = elemento.HorasDeberiaCumplir;
                            fila++;
                            ew.Cells[fila, 1].Value = "Tiempo llegadas Tempranas";
                            ew.Cells[fila, 2].Value = elemento.minutosExtras < 60 ? elemento.minutosExtras.ToString() + " Minutos" : (elemento.minutosExtras / 60).ToString() + " Hr";
                            ew.Cells[fila, 3].Value = "Tiempo Retrazo";
                            ew.Cells[fila, 4].Value = elemento.minutosTardanza < 60 ? elemento.minutosTardanza.ToString() + " Minutos" : (elemento.minutosTardanza / 60).ToString() + " Hr";
                            fila++;
                            ew.Cells[fila, 1].Value = "Dias Con Marcas Faltantes";
                            ew.Cells[fila, 2].Value = elemento.MarcasFaltantes;
                            ew.Cells[fila, 3].Value = "Dias Faltados";
                            ew.Cells[fila, 4].Value = elemento.DiaFaltados;
                            ew.Cells[fila, 5].Value = "Feriados Trabajados";
                            ew.Cells[fila, 6].Value = elemento.FeriadoTrabajados;
                            fila = fila + 2;
                          

                            

                            
                        }

                        ep.SaveAs(ms);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }



            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// retorna un string base64 con un array de Bytes que contiene el formato para un FIle de tipo XML
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fe"></param>
        /// <returns></returns>
        [HttpPost("xml/{idEmpleado}")]
        public async Task<Byte[]> reporteExcel(int idEmpleado, Fechas fe)
        {

            try
            {
                BD = new();
                List<RegistroDiario> registros = new();
                InformeEntreFechas informe = await GererarReporte(idEmpleado, fe);

                for (var fecha = fe.fechaInicio; fecha < fe.fechaFinal; fecha = fecha.AddDays(1))
                {
                    RegistroDiario reg = await BD.getRegistrosPorFechaYIdEmpleado(idEmpleado, fecha);
                    registros.Add(reg);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        ep.Workbook.Worksheets.Add("hoja");
                        ExcelWorksheet ew = ep.Workbook.Worksheets[0];
                        //primero irian mis datos del informe

                        ew.Cells[1, 1].Value = "Reporte del: ";
                        ew.Cells[1, 2].Value = fe.fechaInicio;
                        ew.Cells[1, 3].Value = " Hasta";
                        ew.Cells[1, 4].Value = fe.fechaFinal;

                        ew.Cells[2, 1].Value = "Datos Empleado";
                        ew.Cells[2, 2].Value = "cargo: ";
                        ew.Cells[2, 3].Value = informe.Empleado.cargo;
                        ew.Cells[2, 4].Value = "articulo 77";
                        ew.Cells[2, 5].Value = informe.Empleado.articulo ? "Si Aplica" : "no Aplica";

                        ew.Cells[3, 1].Value = "Nombre";
                        ew.Cells[3, 2].Value = informe.Persona.nombre;
                        ew.Cells[3, 3].Value = "Apellido";
                        ew.Cells[3, 4].Value = informe.Persona.apellido;
                        ew.Cells[3, 5].Value = "Rut";
                        ew.Cells[3, 6].Value = informe.Persona.rut;

                        ew.Cells[5, 1].Value = "Horas Cumplidas";
                        ew.Cells[5, 2].Value = informe.HorasCumplidas;
                        ew.Cells[5, 3].Value = "Horas Que Deberia CUmplir";
                        ew.Cells[5, 4].Value = informe.HorasDeberiaCumplir;

                        ew.Cells[6, 1].Value = "Tiempo llegadas Tempranas";
                        ew.Cells[6, 2].Value = informe.minutosExtras < 60 ? informe.minutosExtras.ToString() + " Minutos" : (informe.minutosExtras / 60).ToString() + " Hr";
                        ew.Cells[6, 3].Value = "Tiempo Retrazo";
                        ew.Cells[6, 4].Value = informe.minutosTardanza < 60 ? informe.minutosTardanza.ToString() + " Minutos" : (informe.minutosTardanza / 60).ToString() + " Hr";

                        ew.Cells[7, 1].Value = "Dias Con Marcas Faltantes";
                        ew.Cells[7, 2].Value = informe.MarcasFaltantes;
                        ew.Cells[7, 3].Value = "Dias Faltados";
                        ew.Cells[7, 4].Value = informe.DiaFaltados;
                        ew.Cells[7, 5].Value = "Feriados Trabajados";
                        ew.Cells[7, 6].Value = informe.FeriadoTrabajados;
                        //y luego agrego todos los registros pertienentes

                        ew.Cells[9, 1].Value = "Registros obtenidos";

                        ew.Cells[10, 1].Value = "fecha";
                        ew.Cells[10, 2].Value = "Hora Ingreso";
                        ew.Cells[10, 3].Value = "Hora Salida";
                        ew.Cells[10, 4].Value = "Hora Break Sakida";
                        ew.Cells[10, 5].Value = "Hora Break Ingreso";

                        int fila = 11;


                        foreach (var item in registros)
                        {
                            ew.Cells[fila, 1].Value = item.fecha;
                            ew.Cells[fila, 2].Value = item.HoraI;
                            ew.Cells[fila, 3].Value = item.HoraS;
                            ew.Cells[fila, 4].Value = item.HoraBI;
                            ew.Cells[fila, 5].Value = item.HoraBS;

                            fila++;
                        }


                        ep.SaveAs(ms);
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        #endregion


        #region funcion Generar Reporte Pasandole fechas y un id
        /// <summary>
        /// esta es la funcion principal de este controlador ya que es quien calcula estos menesteres
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fe"></param>
        /// <returns></returns>
        private async Task<InformeEntreFechas> GererarReporte(int idEmpleado , Fechas fe)
        {
            try
            {
                BD = new();

                if ((fe.fechaFinal - fe.fechaInicio).Days < 0)
                    return null;

                if (idEmpleado < 1)
                    return null;

                List<InformeEntreFechas> retorno = new();
                //List<Empleados> ListaEMpleados = new();
                List<AdmoHorarios> ListAdmo = new();
                List<Feriados> feriados = await BD.GetFeriadosEntreFechas(fe);
                Empleados Empleado = await BD.GetEmpleado(idEmpleado);

                if (Empleado == null || Empleado.id == 0)
                    return null;



                List<string> mensajes = new();
                bool todoOk = true;
                int registrosSinMarca = 0;
                int diasFaltados = 0;
                int ContadorMintosExtra = 0;
                int contFeriadosTrabajados = 0;
                int ContadorMinRetrazo = 0;
                int ContadorHorasCumplidas = 0;
                int ContHorasDeberiaCumplir = 0;
                int ContadorMinutosTiempoCumplido = 0;
                int diasRetrazo = 0;
                int diasDescanzo = 0;

                //string cadenaBandera = "";
                // aqui si el articulo es true ignoramos por completo el horario
                if (Empleado.articulo)
                {
                    for (var recorrido = fe.fechaInicio;recorrido <= fe.fechaFinal; recorrido = recorrido.AddDays(1))
                    {
                        var reg = await BD.getRegistrosPorFechaYIdEmpleado(Empleado.id, recorrido);
                        ContadorHorasCumplidas = contHoras(reg);
                        ContadorMinutosTiempoCumplido += contMinutos(reg);
                    }
                }
                else
                {
                    for (var recorrido = fe.fechaInicio; recorrido <= fe.fechaFinal; recorrido = recorrido.AddDays(1))
                    {
                        RegistroDiario reg = await BD.getRegistrosPorFechaYIdEmpleado(idEmpleado, recorrido);
                        AdmoHorarios admo = await BD.getAdmoHorarioIDEmpleadoYUnaFecha(idEmpleado, recorrido);
                        Horarios horario = await BD.GetHorarioId(admo.horario);



                        if (reg == null || reg.id == 0)
                        {
                            if (admo == null || admo.id == 0)
                            {
                                mensajes.Add("el empleado descanso el dia " + recorrido.ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                if (EsLaboral(recorrido, horario))
                                {
                                    diasFaltados++;
                                    mensajes.Add("el empleado  falto el dia " + recorrido.ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    mensajes.Add("El empleado  descanso el dia " + recorrido.ToString("dd/MM/yyyy"));
                                }
                            }
                        }
                        else
                        {
                            if (admo == null)
                            {
                                if (RegOk(reg))
                                {
                                    TimeSpan RegHi, RegHs, RegHbi, RegHbs;
                                    string[] tiempo;
                                    tiempo = reg.HoraI.Split(":");
                                    RegHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                                    tiempo = reg.HoraS.Split(":");
                                    RegHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                                    tiempo = reg.HoraBI.Split(":");
                                    RegHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                                    tiempo = reg.HoraBS.Split(":");
                                    RegHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));

                                    if ((int)((RegHs - RegHi) - (RegHbs - RegHbi)).Minutes < 0)
                                    {
                                        mensajes.Add("El Dia " + recorrido.ToString("dd/MM/yyyy") + " El empleado asistio, pero no hay marcas completas");
                                        registrosSinMarca++;
                                    }
                                    else
                                    {
                                        mensajes.Add("El Dia " + recorrido.ToString("dd/MM/yyyy") + " El empleado asistio, y se le asignaron: " + (int)((RegHs - RegHi) - (RegHbs - RegHbi)).Hours + " Horas en tiempo extra"); ;
                                        ContadorMintosExtra += (int)((RegHs - RegHi) - (RegHbs - RegHbi)).Minutes;
                                    }
                                    /////// cuenta las horas asistifdas y las asigna como extras
                                    //mensajes.Add("No Hay Horario Asignado en la fecha " + recorrido.ToString("dd/MM/yyyy") + "Sin Embargo el Empleado  Asistio, se le agregaron" + ((RegHs - RegHi) - (RegHbs - RegHbi)).Hours + "Horas Extras");
                                    //ContadorMintosExtra += ((RegHs - RegHi) - (RegHbs - RegHbi)).Hours;
                                }
                                else
                                {
                                    mensajes.Add("El Dia " + recorrido.ToString("dd/MM/yyyy") + " El empleado asistio, pero no hay marcas completas");
                                    registrosSinMarca++;
                                }
                            }
                            else
                            {

                                if (!MismoDia(reg))
                                {
                                    var DiaIniciaJornada = (int)recorrido.DayOfWeek;
                                    todoOk = true;

                                    switch (DiaIniciaJornada)
                                    {
                                        case 0:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Domingo, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Domingo);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;


                                        case 1:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Lunes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Lunes);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;



                                        case 2:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Martes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Martes);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;

                                        case 3:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Miercoles, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Miercoles);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;


                                        case 4:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Jueves, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Jueves);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;

                                        case 5:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Viernes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Viernes);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;

                                        case 6:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Sabado, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Sabado);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;




                                    }

                                    ContadorHorasCumplidas += contHorasDD(reg);
                                    ContadorMinutosTiempoCumplido += contMinutosDD(reg);
                                    if (!todoOk)
                                    {
                                        mensajes.Add("el dia " + recorrido.ToString("dd/MM/yyyy") + " Faltan Marcas");
                                        todoOk = true;
                                    }
                                }
                                else
                                {

                                    var DIA = (int)recorrido.DayOfWeek;
                                    todoOk = true;

                                    switch (DIA)
                                    {
                                        case 0:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Domingo, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                //aqui si se cuentan las horas del horaio por dia
                                                ContHorasDeberiaCumplir += contHoras(horario.Domingo);

                                                // ContadorMinutosTiempoCumplido += contMinutos(horario.Domingo);



                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;

                                            }
                                            break;
                                        case 1:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Lunes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;

                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Lunes);
                                                // ContadorMinutosTiempoCumplido += contMinutos(horario.Lunes);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 2:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(
                                                        horario.Martes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Martes);
                                                ////ContadorMinutosTiempoCumplido += contMinutos(horario.Martes);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 3:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Miercoles, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Miercoles);
                                                //ContadorMinutosTiempoCumplido += contMinutos(horario.Miercoles);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 4:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Jueves, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Jueves);
                                                //ContadorMinutosTiempoCumplido += contMinutos(horario.Jueves);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 5:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Viernes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Viernes);
                                                //ContadorMinutosTiempoCumplido += contMinutos(horario.Viernes);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 6:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Sabado, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Sabado);


                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                    }


                                    //aqui se cuenta las horas cumplidas segun el registrop
                                    ContadorHorasCumplidas += contHoras(reg);
                                    ContadorMinutosTiempoCumplido += contMinutos(reg);
                                    if (!todoOk)
                                    {
                                        mensajes.Add("el dia " + recorrido.ToString("dd/MM/yyyy") + " Faltan Marcas");
                                        registrosSinMarca++;
                                        todoOk = true;
                                    }

                                }

                            }

                        }                        
                       



                    }// fin del cilo de recorrido
                }

                InformeEntreFechas informe = new();
                float flag;
                int bandera;

                if (ContadorMinutosTiempoCumplido >= 60)
                {
                    flag = ContadorMinutosTiempoCumplido / 60;
                    bandera = (int)flag;
                    flag = (flag - bandera) * 60;

                    informe.HorasCumplidas = ContadorHorasCumplidas + bandera;
                    informe.extraMinutosCumplidos = ContadorMinutosTiempoCumplido + (int)flag;
                }
                else
                {
                    informe.HorasCumplidas = ContadorHorasCumplidas;
                    informe.extraMinutosCumplidos = ContadorMinutosTiempoCumplido;
                }

                informe.DiaFaltados = diasFaltados;
                informe.MarcasFaltantes = registrosSinMarca;
                informe.minutosExtras = ContadorMintosExtra;
                informe.minutosTardanza = ContadorMinRetrazo;
                informe.FeriadoTrabajados = 0;
                informe.fechaInicioCalculo = fe.fechaInicio;
                informe.fechaFinalCalculo = fe.fechaFinal;
                informe.Mensajes = mensajes;
                informe.Empleado = Empleado;
                //informe.HorasCumplidas = ContadorHorasCumplidas + (ContadorMinutosTiempoCumplido /60)  ; // == (ContadorMinutosTiempoCumplido/60) ? conta;
                informe.extraMinutosCumplidos =  ContadorMinutosTiempoCumplido ;
                informe.HorasDeberiaCumplir = ContHorasDeberiaCumplir;
                informe.FeriadoTrabajados = contFeriadosTrabajados;
                informe.Persona = new PersonaHash().InformacionParaVistas(await BD.GetPersona(Empleado.persona));
                informe.DiasConRetrazoEntrada = diasRetrazo;
                informe.descanzos = diasDescanzo;

                return informe;


            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }


        private async Task<InformeEntreFechasDetallado> GenerarReporteDetallado(int idEmpleado, Fechas fe)
        {
            try
            {
                BD = new();

                if ((fe.fechaFinal - fe.fechaInicio).Days < 0)
                    return null;

                if (idEmpleado < 1)
                    return null;

                InformeEntreFechasDetallado retorno = new();
                retorno.ListaRegistros = new();
                //List<Empleados> ListaEMpleados = new();
                List<AdmoHorarios> ListAdmo = new();
                List<Feriados> feriados = await BD.GetFeriadosEntreFechas(fe);
                Empleados Empleado = await BD.GetEmpleado(idEmpleado);

                if (Empleado == null || Empleado.id == 0)
                    return null;



                List<string> mensajes = new();
                bool todoOk = true;
                int registrosSinMarca = 0;
                int diasFaltados = 0;
                int ContadorMintosExtra = 0;
                int contFeriadosTrabajados = 0;
                int ContadorMinRetrazo = 0;
                int ContadorHorasCumplidas = 0;
                int ContHorasDeberiaCumplir = 0;
                int ContadorMinutosTiempoCumplido = 0;
                int diasRetrazo = 0;
                int diasDescanzo = 0;

                //string cadenaBandera = "";
                // aqui si el articulo es true ignoramos por completo el horario
                if (Empleado.articulo)
                {
                    for (var recorrido = fe.fechaInicio; recorrido <= fe.fechaFinal; recorrido = recorrido.AddDays(1))
                    {
                        var reg = await BD.getRegistrosPorFechaYIdEmpleado(Empleado.id, recorrido);
                        ContadorHorasCumplidas = contHoras(reg);
                        ContadorMinutosTiempoCumplido += contMinutos(reg);
                        if(reg != null)
                            retorno.ListaRegistros.Add(reg);
                    }
                }
                else
                {
                    for (var recorrido = fe.fechaInicio; recorrido <= fe.fechaFinal; recorrido = recorrido.AddDays(1))
                    {
                        RegistroDiario reg = await BD.getRegistrosPorFechaYIdEmpleado(idEmpleado, recorrido);
                        AdmoHorarios admo = await BD.getAdmoHorarioIDEmpleadoYUnaFecha(idEmpleado, recorrido);
                        Horarios horario = await BD.GetHorarioId(admo.horario);
                        
                        


                        if (reg == null || reg.id == 0)
                        {
                            if (admo == null || admo.id == 0)
                            {
                                mensajes.Add("el empleado descanso el dia " + recorrido.ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                if (EsLaboral(recorrido, horario))
                                {
                                    diasFaltados++;
                                    mensajes.Add("el empleado  falto el dia " + recorrido.ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    mensajes.Add("El empleado  descanso el dia " + recorrido.ToString("dd/MM/yyyy"));
                                }
                            }
                        }
                        else
                        {
                            retorno.ListaRegistros.Add(reg);
                            if (admo == null)
                            {
                                if (RegOk(reg))
                                {
                                    TimeSpan RegHi, RegHs, RegHbi, RegHbs;
                                    string[] tiempo;
                                    tiempo = reg.HoraI.Split(":");
                                    RegHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                                    tiempo = reg.HoraS.Split(":");
                                    RegHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                                    tiempo = reg.HoraBI.Split(":");
                                    RegHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                                    tiempo = reg.HoraBS.Split(":");
                                    RegHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));

                                    if ((int)((RegHs - RegHi) - (RegHbs - RegHbi)).Minutes < 0)
                                    {
                                        mensajes.Add("El Dia " + recorrido.ToString("dd/MM/yyyy") + " El empleado asistio, pero no hay marcas completas");
                                        registrosSinMarca++;
                                    }
                                    else
                                    {
                                        mensajes.Add("El Dia " + recorrido.ToString("dd/MM/yyyy") + " El empleado asistio, y se le asignaron: " + (int)((RegHs - RegHi) - (RegHbs - RegHbi)).Hours + " Horas en tiempo extra"); ;
                                        ContadorMintosExtra += (int)((RegHs - RegHi) - (RegHbs - RegHbi)).Minutes;
                                    }
                                    /////// cuenta las horas asistifdas y las asigna como extras
                                    //mensajes.Add("No Hay Horario Asignado en la fecha " + recorrido.ToString("dd/MM/yyyy") + "Sin Embargo el Empleado  Asistio, se le agregaron" + ((RegHs - RegHi) - (RegHbs - RegHbi)).Hours + "Horas Extras");
                                    //ContadorMintosExtra += ((RegHs - RegHi) - (RegHbs - RegHbi)).Hours;
                                }
                                else
                                {
                                    mensajes.Add("El Dia " + recorrido.ToString("dd/MM/yyyy") + " El empleado asistio, pero no hay marcas completas");
                                    registrosSinMarca++;
                                }
                            }
                            else
                            {

                                if (!MismoDia(reg))
                                {
                                    var DiaIniciaJornada = (int)recorrido.DayOfWeek;
                                    todoOk = true;

                                    switch (DiaIniciaJornada)
                                    {
                                        case 0:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Domingo, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Domingo);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;


                                        case 1:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Lunes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Lunes);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;



                                        case 2:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Martes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Martes);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;

                                        case 3:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Miercoles, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Miercoles);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;


                                        case 4:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Jueves, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Jueves);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;

                                        case 5:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Viernes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Viernes);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;

                                        case 6:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Sabado, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;

                                                ContHorasDeberiaCumplir += contHorasDD(horario.Sabado);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                            }
                                            break;




                                    }

                                    ContadorHorasCumplidas += contHorasDD(reg);
                                    ContadorMinutosTiempoCumplido += contMinutosDD(reg);
                                    if (!todoOk)
                                    {
                                        mensajes.Add("el dia " + recorrido.ToString("dd/MM/yyyy") + " Faltan Marcas");
                                        todoOk = true;
                                    }
                                }
                                else
                                {

                                    var DIA = (int)recorrido.DayOfWeek;
                                    todoOk = true;

                                    switch (DIA)
                                    {
                                        case 0:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    //este cuenta solo las horas cumplidas segun el horario
                                                    verificarHoras(horario.Domingo, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                //aqui si se cuentan las horas del horaio por dia
                                                ContHorasDeberiaCumplir += contHoras(horario.Domingo);

                                                // ContadorMinutosTiempoCumplido += contMinutos(horario.Domingo);



                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;

                                            }
                                            break;
                                        case 1:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Lunes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;

                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Lunes);
                                                // ContadorMinutosTiempoCumplido += contMinutos(horario.Lunes);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 2:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(
                                                        horario.Martes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Martes);
                                                ////ContadorMinutosTiempoCumplido += contMinutos(horario.Martes);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 3:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Miercoles, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Miercoles);
                                                //ContadorMinutosTiempoCumplido += contMinutos(horario.Miercoles);
                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 4:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Jueves, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Jueves);
                                                //ContadorMinutosTiempoCumplido += contMinutos(horario.Jueves);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 5:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Viernes, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Viernes);
                                                //ContadorMinutosTiempoCumplido += contMinutos(horario.Viernes);

                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                        case 6:
                                            if (EsLaboral(recorrido, horario))
                                            {
                                                if (RegOk(reg))
                                                {
                                                    verificarHoras(horario.Sabado, reg, ref ContadorMinRetrazo, ref ContadorMintosExtra, ref todoOk, ref diasRetrazo);
                                                    if (feriados.Find(feriado => feriado.fecha == recorrido) != null)
                                                        contFeriadosTrabajados++;
                                                }
                                                else
                                                    registrosSinMarca++;
                                                ContHorasDeberiaCumplir += contHoras(horario.Sabado);


                                            }
                                            else
                                            {
                                                mensajes.Add("El Empleado, descanzo el dia: " + recorrido.ToString("dd/MM/yyyy"));
                                                diasDescanzo++;
                                            }
                                            break;

                                    }


                                    //aqui se cuenta las horas cumplidas segun el registrop
                                    ContadorHorasCumplidas += contHoras(reg);
                                    ContadorMinutosTiempoCumplido += contMinutos(reg);
                                    if (!todoOk)
                                    {
                                        mensajes.Add("el dia " + recorrido.ToString("dd/MM/yyyy") + " Faltan Marcas");
                                        registrosSinMarca++;
                                        todoOk = true;
                                    }

                                }

                            }

                        }




                    }// fin del cilo de recorrido
                }

               // InformeEntreFechas informe = new();


                retorno.DiaFaltados = diasFaltados;
                retorno.MarcasFaltantes = registrosSinMarca;
                retorno.minutosExtras = ContadorMintosExtra;
                retorno.minutosTardanza = ContadorMinRetrazo;
                retorno.FeriadoTrabajados = 0;
                retorno.fechaInicioCalculo = fe.fechaInicio;
                retorno.fechaFinalCalculo = fe.fechaFinal;
                retorno.Mensajes = mensajes;
                retorno.Empleado = Empleado;
                retorno.HorasCumplidas = ContadorHorasCumplidas; // == (ContadorMinutosTiempoCumplido/60) ? conta;
                retorno.extraMinutosCumplidos = ContadorMinutosTiempoCumplido;
                retorno.HorasDeberiaCumplir = ContHorasDeberiaCumplir;
                retorno.FeriadoTrabajados = contFeriadosTrabajados;
                retorno.Persona = new PersonaHash().InformacionParaVistas(await BD.GetPersona(Empleado.persona));
                retorno.DiasConRetrazoEntrada = diasRetrazo;
                retorno.descanzos = diasDescanzo;

                return retorno;


            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        #endregion

        #region Funciones auxiliares, o para eviatar repeticion de codigo
       
        /// <summary>
        /// retorna el tiempo en minutos
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>

        private static int contMinutos(Dia dia)
        {
            try
            {
                TimeSpan DiaHi, DiaHs, DiaHbi, DiaHbs;
                string[] tiempo;
                // int bandera;


                if (dia.HoraI.Contains(dia.HoraS))
                    return 0;


                else if (String.IsNullOrEmpty(dia.HoraI))
                    return 0;

                else if (String.IsNullOrEmpty(dia.HoraS))
                {
                    if (String.IsNullOrEmpty(dia.HoraBS))
                    {
                        return 0;
                    }
                    else
                    {
                        tiempo = dia.HoraI.Split(":");
                        DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                        tiempo = dia.HoraBS.Split(":");
                        DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));

                        //bandera = (int)(DiaHbs - DiaHi).Hours > 0 ? (int)(DiaHbs - DiaHi).Minutes : 0;
                        return (int)(DiaHbs - DiaHi).Minutes > 0 ? (int)(DiaHbs - DiaHi).Minutes : 0;


                    }


                }
                else
                {

                    tiempo = dia.HoraI.Split(":");
                    DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraS.Split(":");
                    DiaHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBI.Split(":");
                    DiaHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBS.Split(":");
                    DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));


                    if (DiaHi < DiaHs && DiaHi < DiaHbs && DiaHbs < DiaHs)
                    {
                        return (int)((DiaHs - DiaHi) - (DiaHbi - DiaHbs)).Minutes;
                    }
                    else if (DiaHi < DiaHbs)
                    {
                        return (int)(DiaHbs - DiaHi).Minutes;
                    }
                    else
                    {
                        return 0;
                    }

                }
                //return (double)(bandera / 60);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return 0;
            }

        }

        /// <summary>
        /// un contador para los minutops extras que no pueden faltar para cuando los minutos son diferentes
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>
        private static int contMinutosDD(Dia dia)
        {
            try
            {
                TimeSpan DiaHi, DiaHs, DiaHbi, DiaHbs;
                string[] tiempo;
                

                if (String.IsNullOrEmpty(dia.HoraI))
                    return 0;

                else if (String.IsNullOrEmpty(dia.HoraS))
                {
                    if (String.IsNullOrEmpty(dia.HoraBS))
                    {
                        return 0;
                    }
                    else
                    {
                        tiempo = dia.HoraI.Split(":");
                        DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                        tiempo = dia.HoraBS.Split(":");
                        DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));

                        //bandera = (int)(DiaHbs - DiaHi).Hours > 0 ? (int)(DiaHbs - DiaHi).Minutes : 0;
                        if(DiaHbs>DiaHi)
                            return (int)(DiaHbs - DiaHi).Minutes > 0 ? (int)(DiaHbs - DiaHi).Minutes : 0;
                        return (int)(DiaHi- DiaHbs).Minutes > 0 ? (int)(DiaHi- DiaHbs).Minutes : 0;


                    }


                }
                else
                {

                    tiempo = dia.HoraI.Split(":");
                    DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraS.Split(":");
                    DiaHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBI.Split(":");
                    DiaHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBS.Split(":");
                    DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));


                    TimeSpan aux = new(23, 59, 59);

                    var sum = (aux - DiaHi) + DiaHs;

                    TimeSpan dif;
                    if (DiaHbs < DiaHbi)
                        dif = DiaHbi - DiaHbs;
                    else
                        dif = (aux - DiaHbs) + DiaHbi;

                    return (sum - dif).Minutes;

                }
                
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return 0;
            }

        }
        /// <summary>
        /// cuentas las horas pasandoles un dia especifico
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>
        private static int contHoras(Dia dia)
        {
            try
            {
                TimeSpan DiaHi, DiaHs, DiaHbi, DiaHbs;
                string[] tiempo;
               // int bandera;


                if (dia.HoraI.Contains(dia.HoraS))
                    return 0;


                else if (String.IsNullOrEmpty(dia.HoraI))
                    return 0;

                else if (String.IsNullOrEmpty(dia.HoraS))
                {
                    if (String.IsNullOrEmpty(dia.HoraBS))
                    {
                        return 0;
                    }
                    else
                    {
                        tiempo = dia.HoraI.Split(":");
                        DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                        tiempo = dia.HoraBS.Split(":");
                        DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));

                        //bandera = (int)(DiaHbs - DiaHi).Hours > 0 ? (int)(DiaHbs - DiaHi).Minutes : 0;
                        return (int)(DiaHbs - DiaHi).Hours > 0 ? (int)(DiaHbs - DiaHi).Hours : 0;
                       

                    }


                }
                else
                {

                    tiempo = dia.HoraI.Split(":");
                    DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraS.Split(":");
                    DiaHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBI.Split(":");
                    DiaHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBS.Split(":");
                    DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));


                    if (DiaHi < DiaHs && DiaHi < DiaHbs && DiaHbs < DiaHs)
                    {
                       return (int)((DiaHs - DiaHi) - (DiaHbi - DiaHbs)).Hours;
                    }
                    else if (DiaHi < DiaHbs)
                    {
                        return (int)(DiaHbs - DiaHi).Hours;
                    }
                    else
                    {
                        return 0;
                    }

                }
             //return (double)(bandera / 60);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return 0;
            }
        }

        /// <summary>
        /// contador de horas de dias diferentes
        /// lo separo en otra funcionya que el comprobar si es el mismo dia o uno diferente tambien tiene su funcion aparte
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>
        private static int contHorasDD(Dia dia)
        {
            try
            {
                TimeSpan DiaHi, DiaHs, DiaHbi, DiaHbs;
                string[] tiempo;
                var aux = new TimeSpan(23, 59, 59);



                if (String.IsNullOrEmpty(dia.HoraI))
                    return 0;

                else if (String.IsNullOrEmpty(dia.HoraS))
                {
                    if (String.IsNullOrEmpty(dia.HoraBS))
                    {
                        return 0;
                    }
                    else
                    {
                        tiempo = dia.HoraI.Split(":");
                        DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                        tiempo = dia.HoraBS.Split(":");
                        DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));

                        //bandera = (int)(DiaHbs - DiaHi).Hours > 0 ? (int)(DiaHbs - DiaHi).Minutes : 0;

                        if(DiaHi< DiaHbs)
                        {
                            return (int)(DiaHbs - DiaHi).Hours > 0 ? (int)(DiaHbs - DiaHi).Hours : 0;
                         }
                         
                        
                        return ((aux - DiaHi) + DiaHbs).Hours;
                    }


                }
                else
                {

                    tiempo = dia.HoraI.Split(":");
                    DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraS.Split(":");
                    DiaHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBI.Split(":");
                    DiaHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
                    tiempo = dia.HoraBS.Split(":");
                    DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));



                    var sum = (aux - DiaHi) + DiaHs;

                    TimeSpan dif;
                    if (DiaHbs < DiaHbi)
                        dif = DiaHbi - DiaHbs;
                    else
                        dif = (aux - DiaHbs) + DiaHbi;

                    return (sum - dif).Hours;
                }
                //return (double)(bandera / 60);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return 0;
            }
        }

        /// <summary>
        /// aqui calcula el tiempo en retrazo o extrass en base a las anotaciones o registros diaris y las horas
        /// </summary>
        /// <param name="horario"> el horario que es este usado</param>
        /// <param name="reg"> el registro que se deseea comparar</param>
        /// <param name="MinRetrazo"> variable refencia donde ban los minutos retrazo</param>
        /// <param name="MinExtras">variable refencia donde ban los minutos extras</param>
        /// <param name="todoOk">variable refencia donde verifico que no hubieron dsetalles durante el calculo</param>
        private static void verificarHoras(Dia horario, RegistroDiario reg, ref int MinRetrazo, ref int MinExtras, ref bool todoOk, ref int DiasRetrazo)
        {
            // primero veo las horas cumplidas en base al registro
            TimeSpan RegHi, RegHs, RegHbi, RegHbs;
            TimeSpan DiaHi, DiaHs, DiaHbi, DiaHbs;
            string[] tiempo;

            //horasCumplidas = ((reg.HoraBS - reg.HoraI) + (reg.HoraS - reg.HoraBI) )





            // primero genero mis valores de modo que se me permita calcularlos

            // registro
            tiempo = reg.HoraI.Split(":");
            RegHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = reg.HoraS.Split(":");
            RegHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = reg.HoraBI.Split(":");
            RegHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = reg.HoraBS.Split(":");
            RegHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            // Horario
            tiempo = horario.HoraI.Split(":");
            DiaHi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = horario.HoraS.Split(":");
            DiaHs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = horario.HoraBI.Split(":");
            DiaHbi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = horario.HoraBS.Split(":");
            DiaHbs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));


            //primero calculo las horas cumplidas segun el registro
            //si me da un valor menor a 0 quiere decir que falta una marca
            if ((int)((RegHs - RegHi) - ( RegHbi - RegHbs)).Minutes < 0)
            {
                //horasCumplidas += 0;
                //mensaje = "El dia" + reg.fecha +" falto una marca";
                todoOk = false;
                return;
            }

            //horasCumplidas += (int)((RegHs - RegHi) - (RegHbs - RegHbi)).Hours;



            // verifico si las entradas y/o salidas del registros coinciden con el horario

            if (((RegHs - RegHi) - ( RegHbi - RegHbs)) == ((DiaHs - DiaHi) - (DiaHbi - DiaHbs)))
                return;

            //verificoi la hora de ingreso

            if ((RegHi - DiaHi).Minutes > 0)
            {
                MinRetrazo += (int)(RegHi - DiaHi).Minutes;
                DiasRetrazo++;
            }
            else
                MinExtras += (int)(-(RegHi - DiaHi).Minutes);
            //verifico la salida al break o colacion
            if ((RegHbs - DiaHbs).Minutes < 0)
                MinRetrazo += (int)(DiaHbs - RegHbs).Minutes;
            else
                MinExtras += (RegHbs - DiaHbs).Minutes;
            //verifico el ingreso despues del break
            if ((RegHbi - DiaHbi).Minutes > 0)
                MinRetrazo += (int)(RegHbi - DiaHbi).Minutes;
            else
                MinExtras += (DiaHbi - RegHbi).Minutes;
            //verifico la salida al final del turno
            if ((RegHs - DiaHs).Minutes < 0)
                MinRetrazo += (int)(DiaHs - RegHs).Minutes;
            else
                MinExtras += (RegHs - DiaHs).Minutes;


        }

        
        /// <summary>
        /// verifica que el registro tenga los datos necesarios
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>

        private static bool RegOk(RegistroDiario reg)
        {
            if (String.IsNullOrEmpty(reg.HoraI) || String.IsNullOrEmpty(reg.HoraS) || String.IsNullOrEmpty(reg.HoraBI) || String.IsNullOrEmpty(reg.HoraBS))
                return false;
            if (reg.HoraI.Contains(reg.HoraS))
                return false;
            return true;
        }


        /// <summary>
        /// verifica si un dia pasado es laboral segun el horario
        /// </summary>
        /// <param name="Dia"></param>
        /// <param name="horario"></param>
        /// <returns></returns>
        private static bool EsLaboral(DateTime Dia, Horarios horario)
        {
            int dia = (int)Dia.DayOfWeek;


            switch (dia)
            {
                case 0:
                    if (String.IsNullOrEmpty(horario.Domingo.HoraI) || String.IsNullOrEmpty(horario.Domingo.HoraS) || horario.Domingo.HoraS == horario.Domingo.HoraI)
                        return false;
                    break;
                case 1:

                    if (String.IsNullOrEmpty(horario.Lunes.HoraI) || String.IsNullOrEmpty(horario.Lunes.HoraS) || horario.Lunes.HoraS == horario.Lunes.HoraI)
                        return false;

                    break;
                case 2:
                    if (String.IsNullOrEmpty(horario.Martes.HoraI) || String.IsNullOrEmpty(horario.Martes.HoraS) || horario.Martes.HoraS == horario.Martes.HoraI)
                        return false;
                    break;
                case 3:
                    if (String.IsNullOrEmpty(horario.Miercoles.HoraI) || String.IsNullOrEmpty(horario.Miercoles.HoraS) || horario.Miercoles.HoraS == horario.Miercoles.HoraI)
                        return false;

                    break;
                case 4:

                    if (String.IsNullOrEmpty(horario.Jueves.HoraI) || String.IsNullOrEmpty(horario.Jueves.HoraS) || horario.Jueves.HoraS == horario.Jueves.HoraI)
                        return false;
                    break;
                case 5:

                    if (String.IsNullOrEmpty(horario.Viernes.HoraI) || String.IsNullOrEmpty(horario.Viernes.HoraS) || horario.Viernes.HoraS == horario.Viernes.HoraI)
                        return false;

                    break;
                case 6:
                    if (String.IsNullOrEmpty(horario.Sabado.HoraI) || String.IsNullOrEmpty(horario.Sabado.HoraS) || horario.Sabado.HoraS == horario.Sabado.HoraI)
                        return false;

                    break;

            }


            return true;
        }



        private static bool MismoDia(Dia dia)
        {

            TimeSpan Hi, Hs; 
            string[] tiempo;
            tiempo = dia.HoraI.Split(":");
            Hi = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
            tiempo = dia.HoraS.Split(":");
            Hs = new TimeSpan(Int32.Parse(tiempo[0]), Int32.Parse(tiempo[1]), Int32.Parse(tiempo[2]));
           

            if ((Hi - Hs).Minutes < 0)
                return true;

            return false;
        }
        #endregion
    }
}
