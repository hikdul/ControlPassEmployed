using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Clases
{
    /// <summary>
    /// record simple para enviar los mail a la hora de generar una marca
    /// </summary>
    public record Mail
    {
        /// <summary>
        ///declaro mis variables del email
        /// 
        /// </summary>
        public string  mail { get;} = "medios@topaca.cl";
        /// <summary>
        /// contraseña
        /// </summary>
        public string psw { get; } = "dhr#}H#N2MEs";

        //bytvawighquxnsfl

        //genero mi funcion de enviar el correo con un correo entrante
        /// <summary>
        /// para enviar un correo
        /// </summary>
        /// <param name="Contenido"></param>
        /// <param name="Asunto"></param>
        /// <param name="Destino"></param>
        /// <returns></returns>

        public bool enviarCorreo(string Contenido, string Asunto, string Destino)
        {
            try {

                if (String.IsNullOrEmpty(Destino) || String.IsNullOrEmpty(Contenido) || String.IsNullOrEmpty(Asunto))
                    return false;

                string regMail = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
                if (Regex.IsMatch(Destino, regMail))
                {
                    MailMessage send = new(mail, Destino, Asunto, Contenido);

                    send.IsBodyHtml = true;

                    SmtpClient smtp = new("topaca.cl");
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = "162.241.61.67";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential(mail, psw);
                    //await smtp.SendAsync(send);
                    //await smtp.SendCompleted(send);
                    smtp.Send(send);
                    smtp.Dispose();
                    return true;
                }
                return false;


            }catch(Exception x)
            {
                Console.WriteLine(x.Message);
                return false;
            }

            }


    }
}
