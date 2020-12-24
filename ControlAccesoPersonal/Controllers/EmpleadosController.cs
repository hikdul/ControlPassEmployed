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
    /// donde se contendran las vistas para la creacion de nuesvos empleados
    /// </summary>

    public class EmpleadosController : Controller
    {

        #region declaraciones iniciales
        /// <summary>
        /// a donde se aran los llamados del crud base del controlador
        /// </summary>
        private readonly string url = "https://localhost:44391/API/EmpleadoAPI";
        private readonly string urlEmpresa = "https://localhost:44391/API/EmpresasAPI";
        private readonly string urlPersona = "https://localhost:44391/API/PersonasAPI";


        /// <summary>
        /// el token de usuario,
        /// necesitare buscar una manera de tenerlo entre los scrips y cada ves que se llame a un elemento del controlador que lo lea del script
        /// </summary>
        private string TK = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imhpa2R1bEBzdXBlckFkbWluLmNvbSIsImNpZiI6ImYxNmFiYjA2NzM4MDRiMWQ4NWUyMDlkM2RkMDc0OGVlOWFkY2EwMjMxNzExNDZiOWFmYTUzOWMyMjRhMjdjNGQ6IiwianRpIjoiMGNiYTgzNGMtNTUwNi00ZDNjLTlmMDMtZjEyZWY4MTU5NDA1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU3VwZXJBZG1pbiIsImV4cCI6MTYwOTI0NjE5N30.q2y_EUWf_yyM7UAX_8BQDAOJsogfkK_Yd-dpCh0gK-I";

        #endregion


        #region vistas


        /// <summary>
        /// vista principal
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {

            List<Empleados> lista = await HttpSolicitudes.GetAll<Empleados>(url, TK);
            List<Empresa> listaEmpresa = await HttpSolicitudes.GetAll<Empresa>(urlEmpresa, TK);
            List<Persona> listaPersonas = await HttpSolicitudes.GetAll<Persona>(urlPersona, TK);
            ViewBag.listaEmpresa = listaEmpresa;
            ViewBag.listaPersonas = listaPersonas;

            //ViewBag.ListaProveedores = ListaProveedores.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Descripcion }).ToList<SelectListItem>();

            return View(lista);
        }

        /// <summary>
        /// aqui se crearan o editaran los feriados
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Insert(int id)
        {
            Empleados _f = null;
            if (id > 0)
                _f = await HttpSolicitudes.GetUniqueValue<Empleados>(url + "/" + id, TK);

            List<Empresa> listaEmpresa = await HttpSolicitudes.GetAll<Empresa>(urlEmpresa, TK);
            List<Persona> listaPersonas = await HttpSolicitudes.GetAll<Persona>(urlPersona, TK);
            

            ViewBag.listaEmpresa = listaEmpresa.Select(p => new SelectListItem()
            {
                Text = "Rut: " + p.rut + " | Nombre:" + p.nombre,
                Value = p.id.ToString()
            });
            ViewBag.listaPersonas = listaPersonas.Select(p => new SelectListItem()
            {
                Text = "Rut: " + p.rut + " | Nombre:" + p.nombre + " | Apellido:" + p.apellido,
                Value = p.id.ToString()
            });


            return View(_f);

        }


        #endregion


        #region funciones CRUD

        /// <summary>
        /// obtener un dato basado en si id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Empleados> GetId(int id)
        {

            return await HttpSolicitudes.GetUniqueValue<Empleados>(url + "/" + id, TK);
        }

        /// <summary>
        /// para generar un nuevo elemento
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>
        public async Task<IActionResult> Nuevo(Empleados insert)
        {
            if (await HttpSolicitudes.PostBool<Empleados>(insert, url, TK))
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
        public async Task<IActionResult> Editar(int id, Empleados edit)
        {
            if (await HttpSolicitudes.PutBool<Empleados>(id, edit, url, TK))
                return RedirectToAction("Index");
            
            return View("Insert", edit);
        }
        /// <summary>
        /// para eliminar un elemento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Eliminar(int id)
        {
            if (await HttpSolicitudes.DeleteBool<Empleados>(id, url, TK))
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
