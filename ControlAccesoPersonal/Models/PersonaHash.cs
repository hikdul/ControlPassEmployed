using ControlAccesoPersonal.DataTransferObjects;
using ControlAccesoPersonal.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Models
{
    public class PersonaHash : MD5
    {
        [Key]
        public int id { get; set; }     
        public string rut { get; set; }       
        public string nombre { get; set; }      
        public string nombre2 { get; set; }      
        public string apellido { get; set; }     
        public string apellido2 { get; set; }    
        public string telefono { get; set; }
        public string correo { get; set; }
        public bool act { get; set; }
        public string salt { get; set; }

        #region funciones ParaOrganizar informacion
        /// <summary>
        /// aqui organizo mi informacion del cliente y genero de modo de poderla introducir a base de datos
        /// en caso de algun error retorna null
        /// </summary>
        /// <param name="_p"></param>
        /// <returns></returns>

        public PersonaHash ParaBD(Persona _p)
        {
            try
            {
                string sal = GenerarKey();

                PersonaHash p = new PersonaHash();


                p.salt = sal;
                p.nombre = Encriptar(_p.nombre, sal);
                p.nombre2 = Encriptar(_p.nombre2, sal);
                p.apellido = Encriptar(_p.apellido, sal);
                p.apellido2 = Encriptar(_p.apellido2, sal);
                p.telefono = Encriptar(_p.telefono, sal);
                p.correo = Encriptar(_p.correo, sal);
                p.rut = Encriptar(_p.rut, sal);
                p.act = true;
                p.id = _p.id > 0 ? _p.id : 0;
              

                return p;

            }
            catch
            {
                return null;
            }
            
        }

        /// <summary>
        /// con la informacion de base de datos generar informacion viable para el cliente final
        /// en caso de error retorna null
        /// </summary>
        /// <param name="_p"></param>
        /// <returns></returns>

        public Persona InformacionParaVistas(PersonaHash _p)
        {
            try
            {
                if (String.IsNullOrEmpty(_p.salt))
                    return null;


                return new Persona()
                {
                    nombre = Desencriptar(_p.nombre, _p.salt),
                    nombre2 = Desencriptar(_p.nombre2, _p.salt),
                    apellido = Desencriptar(_p.apellido, _p.salt),
                    apellido2 = Desencriptar(_p.apellido2, _p.salt),
                    telefono = Desencriptar(_p.telefono, _p.salt),
                    correo = Desencriptar(_p.correo, _p.salt),
                    rut = Desencriptar(_p.rut, _p.salt),
                    id = _p.id > 0 ? _p.id : 0
                };

            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
