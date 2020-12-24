using ControlAccesoPersonal.DataTransferObjects;
using ControlAccesoPersonal.helper;
using ControlAccesoPersonal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Controllers
{
    /// <summary>
    /// donde se contendran las vistas para el administrador de horarios
    /// </summary>
    public class AdministradorDeHorariosController : Controller
    {

        #region declaraciones

        /// <summary>
        ///  dodne ira toda la informacion de mis elementos
        /// </summary>
        //private List<AdmoHorarios> lista;
        /// <summary>
        /// el url del controlador API
        /// </summary>
        private readonly string url = "https://localhost:44391/API/AdministradoHorariosAPI";
        private readonly string urlPersona = "https://localhost:44391/API/PersonasAPI";
        private readonly string urlHorarios = "https://localhost:44391/API/HorariosAPI";
        private readonly string urlEmpleado = "https://localhost:44391/API/EmpleadoAPI";
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
        public async Task<IActionResult> Index()
        {
            ViewBag.listaHorarios = await HttpSolicitudes.GetAll<Horarios>(urlHorarios, TK);
            ViewBag.listapersonas = await HttpSolicitudes.GetAll<Persona>(urlPersona, TK);
            ViewBag.listaEmpleado = await HttpSolicitudes.GetAll<Empleados>(urlEmpleado, TK);


            return View(await HttpSolicitudes.GetAll<AdmoHorarios>(url, TK));
        }
        /// <summary>
        /// aqui se crearan o editaran los feriados
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Insert(int id)
        {

            var listaHorarios = await HttpSolicitudes.GetAll<Horarios>(urlHorarios, TK);
            var listapersonas = await HttpSolicitudes.GetAll<Persona>(urlPersona, TK);
            var listaEmpleado = await HttpSolicitudes.GetAll<Empleados>(urlEmpleado, TK);



            ViewBag.listaH = listaHorarios.Select(p => new SelectListItem()
            {
                Text = p.nombre,
                Value = p.id.ToString()
            });

            ViewBag.listaPE = listaEmpleado.Select(Emp => new SelectListItem()
            {
                Value = Emp.id.ToString(),
                Text = listapersonas.Find(x => x.id == Emp.persona).rut
            });


            AdmoHorarios _f = null;
            if (id > 0)
                _f = await HttpSolicitudes.GetUniqueValue<AdmoHorarios>(url + "/" + id, TK);

            return View(_f);

        }


        #endregion


        #region funciones CRUD

        /// <summary>
        /// obtener un dato basado en si id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdmoHorarios> GetId(int id)
        {

            return await HttpSolicitudes.GetUniqueValue<AdmoHorarios>(url + "/" + id, TK);
        }

        /// <summary>
        /// para generar un nuevo elemento
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>
        public async Task<IActionResult> Nuevo(AdmoHorarios insert)
        {
            if (await HttpSolicitudes.PostBool<AdmoHorarios>(insert, url, TK))
                return RedirectToAction("Index");
            //return View("Index");
            else
                return View("Insert", insert);
        }

        /// <summary>
        /// para editar un elemento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edit"></param>
        /// <returns></returns>
        public async Task<IActionResult> Editar(int id, AdmoHorarios edit)
        {
            if (await HttpSolicitudes.PutBool<AdmoHorarios>(id, edit, url, TK))
                return RedirectToAction("Index");
            else
                return View("Insert", edit);
        }
        /// <summary>
        /// para eliminar un elemento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Eliminar(int id)
        {
            if (await HttpSolicitudes.DeleteBool<AdmoHorarios>(id, url, TK))
                return RedirectToAction("Index");
            else
            {
                ViewBag.mensaje = "Algo Salio Mal";
                return RedirectToAction("Index");
            }
        }



        #endregion

    }
}
