using ControlAccesoPersonal.DataTransferObjects;
using ControlAccesoPersonal.helper;
using ControlAccesoPersonal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Controllers
{
    /// <summary>
    /// para tener la parte visula de mis reportes
    /// </summary>
    public class ReporteController : Controller
    {
        #region declaraciones

        
        /// <summary>
        /// el url del controlador API
        /// </summary>
        private readonly string url = "https://localhost:44391/API/ReporteAPI";
        private readonly string urlReg = "https://localhost:44391/API/RegistroDiarioAPI";
        /// <summary>
        /// el token de usuario,
        /// necesitare buscar una manera de tenerlo entre los scrips y cada ves que se llame a un elemento del controlador que lo lea del script
        /// </summary>
        private string TK = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imhpa2R1bEBzdXBlckFkbWluLmNvbSIsImNpZiI6ImYxNmFiYjA2NzM4MDRiMWQ4NWUyMDlkM2RkMDc0OGVlOWFkY2EwMjMxNzExNDZiOWFmYTUzOWMyMjRhMjdjNGQ6IiwianRpIjoiMGNiYTgzNGMtNTUwNi00ZDNjLTlmMDMtZjEyZWY4MTU5NDA1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU3VwZXJBZG1pbiIsImV4cCI6MTYwOTI0NjE5N30.q2y_EUWf_yyM7UAX_8BQDAOJsogfkK_Yd-dpCh0gK-I";

        #endregion



        #region vistas

        /// <summary>
        /// aqui retorna una lista con todos los feriados activos en la base de datos
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Index(Fechas fe)
        {
            validarFechas(ref fe);

            ViewBag.ff = fe.fechaFinal.ToString("yyyy-MM-dd");
            ViewBag.fi = fe.fechaInicio.ToString("yyyy-MM-dd");

            return View(await ObtenerEntreFechas(fe,url,TK));
        }
       
        /// <summary>
        /// para opbtener mi reporte en formato xml
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <returns></returns>
        public async Task<FileResult> Print(int id,Fechas fe)
        {

            return await PrintSelf(id, fe,url, urlReg, TK); 
                //ReporteIndividual(id, fe, url, TK);

        }
        
        /// <summary>
        /// aqui se crearan o editaran los feriados
        /// </summary>
        /// <returns></returns>
        //[HttpGet("{idEmpleado}")]
        //public async Task<IActionResult> Detalles(int idEmpleado, Fechas fe)
        //{
        //    try
        //    {
        //        validarFechas(ref fe);

        //        var lista = await ObtenerEntreFechas(fe, url, TK);
        //        List<SelectListItem> ListaGente = new();
        //        foreach (var inf in lista)
        //        {
        //            ListaGente.Add(new SelectListItem()
        //            {
        //                Text = inf.Persona.nombre + " " + inf.Persona.apellido + " | RUT: " + inf.Persona.rut,
        //                Value = inf.Empleado.id.ToString()
        //            });
        //        }

        //        List<RegistroDiario> ListaRegistros = await DameRegistros(idEmpleado, fe, url, TK);

        //        InformeEntreFechasDetallado informe = await EntreFechasConRegistros(idEmpleado, fe, url, TK);

        //            informe.ListaRegistros = ListaRegistros;

        //        ViewBag.listaGente = ListaGente;
        //        //fechaIdEmpleado/{idEmpleado}


        //    return View(informe);
        //    }
        //    catch
        //    {
        //        return View(NoContent());
        //    }




        //}


        #endregion

        #region funciones extras


        private void validarFechas(ref Fechas fe)
        {
            if (fe == null)
                fe = new Fechas()
                {
                    fechaFinal = DateTime.Now,
                    fechaInicio = DateTime.Now.AddDays(-7)
                };

            else if (fe.fechaInicio > fe.fechaFinal)
                fe = new Fechas()
                {
                    fechaFinal = DateTime.Now,
                    fechaInicio = DateTime.Now.AddDays(-7)
                };
            else if (fe.fechaInicio == new DateTime() || fe.fechaFinal == new DateTime())
                fe = new Fechas()
                {
                    fechaFinal = DateTime.Now,
                    fechaInicio = DateTime.Now.AddDays(-7)
                };

        }

        #endregion


        #region Peticiones del reporte


        private async Task<List<RegistroDiario>> DameRegistros(int idEmpleado, Fechas fe, string Url, string Token)
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                Url += "/" + idEmpleado;
                HttpResponseMessage response = await client.PostAsJsonAsync(Url, fe);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<List<RegistroDiario>>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }

        }

        /// <summary>
        /// peticion para mi pantalla inicial donde apenas se cargaran los elementos
        /// </summary>
        /// <param name="fe"></param>
        /// <param name="Url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        private async Task<List<InformeEntreFechas>> ObtenerEntreFechas(Fechas fe, string Url, string Token)
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);

                HttpResponseMessage response = await client.PostAsJsonAsync(Url, fe);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<List<InformeEntreFechas>>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }

        }


        /// <summary>
        /// para dar retornar un reporte dependiendo del id del empleado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <param name="Url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        private async Task<InformeEntreFechas> ObtenerEntreFechasIndividual(int id,Fechas fe, string Dir, string Token)
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                Dir = Dir + "/" + id;
                HttpResponseMessage response = await client.PostAsJsonAsync(Dir, fe);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<InformeEntreFechas>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }

        }

        /// <summary>
        /// para obtener los deatalles de un informe por empleado
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fe"></param>
        /// <param name="Url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>

        public async Task<InformeEntreFechasDetallado> EntreFechasConRegistros(int idEmpleado,Fechas fe, string Url, string Token)
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);

                HttpResponseMessage response = await client.PostAsJsonAsync(Url + "/" + idEmpleado, fe);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<InformeEntreFechasDetallado>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }
        }


        /// <summary>
        /// para obtener una lista especifica de registros segun su usuario y segun las fechas de estudio
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fe"></param>
        /// <param name="Url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<List<RegistroDiario>> ObtenerRegistros(int idEmpleado, Fechas fe, string Url, string Token)
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);

                HttpResponseMessage response = await client.PostAsJsonAsync(Url + "/" + idEmpleado, fe);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<List<RegistroDiario>>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }
        }
        #endregion


        #region peticiones XML
        /// <summary>
        /// para obtener los datos e imprimirlos en el xml
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <param name="Url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<FileResult> ReporteIndividual(int id, Fechas fe, string Url,string Token)
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                Url += "/xml/" + id;
                HttpResponseMessage response = await client.PostAsJsonAsync(Url, fe);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<FileResult>(responseBody);
               // return File(datos, "application/xml");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }


        }
        /// <summary>
        /// para imprimir desde aca!!!
        /// segun lei en la documentacion lo mejor es hacer las impreciones desde el front
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <param name="urlInforme"></param>
        /// <param name="urlRegistros"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<FileResult> PrintSelf(int id, Fechas fe,string urlInforme, string urlRegistros, string Token)
        {
            HttpClient client = new();

            try
            {
                InformeEntreFechas informe = await ObtenerEntreFechasIndividual(id, fe, urlInforme, TK);
                List<RegistroDiario> registros = await ObtenerRegistros(id, fe, urlRegistros, TK);

                if (informe == null || registros == null)
                    return null;

                //// ##########################     REPORTE

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
                        ew.Cells[2, 3].Value = String.IsNullOrEmpty(informe.Empleado.cargo) ? "N/A" : informe.Empleado.cargo;
                        ew.Cells[2, 4].Value = "articulo 22";
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
                        return File(ms.ToArray(), "application/xml");





                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            finally
            {
                client.Dispose();

            }


        }


        #endregion
    }



}
