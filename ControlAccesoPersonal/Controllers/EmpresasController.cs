using ControlAccesoPersonal.helper;
using ControlAccesoPersonal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Controllers
{
    /// <summary>
    /// controlador de las vistas de empresas
    /// </summary>
    public class EmpresasController : Controller
    {


        #region declaraciones iniciales
        /// <summary>
        /// a donde se aran los llamados del crud base del controlador
        /// </summary>
        private readonly string url = "https://localhost:44391/API/EmpresasAPI";


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

            List<Empresa> lista = await HttpSolicitudes.GetAll<Empresa>(url, TK);


            return View(lista);
        }

        /// <summary>
        /// aqui se crearan o editaran los feriados
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Insert(int id)
        {
            Empresa _f = null;
            if (id > 0)
                _f = await HttpSolicitudes.GetUniqueValue<Empresa>(url + "/" + id, TK);

            return View(_f);

        }


        #endregion


        #region funciones CRUD

        /// <summary>
        /// obtener un dato basado en si id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Empresa> GetId(int id)
        {

            return await HttpSolicitudes.GetUniqueValue<Empresa>(url + "/" + id, TK);
        }

        /// <summary>
        /// para generar un nuevo elemento
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>
        public async Task<IActionResult> Nuevo(Empresa insert)
        {
            if (await HttpSolicitudes.PostBool<Empresa>(insert, url, TK))
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
        public async Task<IActionResult> Editar(int id, Empresa edit)
        {
            if (await HttpSolicitudes.PutBool<Empresa>(id, edit, url, TK))
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
            if (await HttpSolicitudes.DeleteBool<Empresa>(id, url, TK))
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
