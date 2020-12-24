using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.helper
{
    /// <summary>
    /// para tener un genericos de mis peticioens http
    /// httpclient
    /// </summary>
    public static class HttpSolicitudes
    {

        #region GET

        /// <summary>
        /// para obtener un elemento en lista de un tipo  de objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async static Task<List<T>> GetAll<T>(string url, string Token) where T : class, new()
        {
            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                //string output = JsonConvert.SerializeObject(product);
                //string Sjson = JsonConvert.SerializeObject(fechas);
                //StringContent json = new StringContent(Sjson);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<List<T>>(responseBody);

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
        /// para obtener un elemeto mediante un id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>

        public async static Task<T> GetUniqueValue<T>(string url, string Token) where T : class, new()
        {
            {
                HttpClient client = new();
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", Token);
                    //string output = JsonConvert.SerializeObject(product);
                    //string Sjson = JsonConvert.SerializeObject(fechas);
                    //StringContent json = new StringContent(Sjson);
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);
                    Console.WriteLine(responseBody);
                    return JsonConvert.DeserializeObject<T>(responseBody);

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
        }
        #endregion

        #region POST
        /// <summary>
        /// para insertar un valor
        /// returna true si todo salio bien
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async static Task<bool> PostBool<T>(T element, string url, string Token) where T : class, new()
        {
           
                HttpClient client = new();
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", Token);
                    
                    HttpResponseMessage response = await client.PostAsJsonAsync(url,element);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    Console.WriteLine(responseBody);
                    return JsonConvert.DeserializeObject<bool>(responseBody);

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return false;
            }
            finally
            {
                client.Dispose();

            }


        }
        /// <summary>
        /// inserta un valor y retorna en valor en si ya registrado en data base
        /// null es que algo salio mal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async static Task<T> Post<T>(T element, string url, string Token) where T : class, new()
        {

            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);

                HttpResponseMessage response = await client.PostAsJsonAsync(url, element);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<T>(responseBody);

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

        #region PUT
        /// <summary>
        /// para actualizar los datos de un elemento 
        /// retorna tru si se actualizo y false en caso contrario
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<bool> PutBool<T>(int id, T element, string url, string Token) where T : class, new()
        {

            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                url = url + "/" + id;
                HttpResponseMessage response = await client.PutAsJsonAsync(url,element);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<bool>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }
            finally
            {
                client.Dispose();

            }


        }
        /// <summary>
        /// actualiza un valor y retorna el mismo valor actualizado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<T> Put<T>(int id, T element, string url, string Token) where T : class, new()
        {

            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                url = url + "/" + id;
                HttpResponseMessage response = await client.PutAsJsonAsync(url, element);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<T>(responseBody);

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

        #region DELETE
        /// <summary>
        /// para eliminar un elemento que retorna el mismo pero tya vacio o con el estado cambiado
        /// o null si algo salio mal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async static Task<T> Delete<T>(int id, string url, string Token) where T : class, new()
        {

            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                url = url + id;
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<T>(responseBody);

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
        /// elimina un elemento basado en su id
        /// retorna el elemento vacio o con un estado cambiado si todo salio bien
        /// o retorne false si todo salio mal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async static Task<bool> DeleteBool<T>(int id, string url, string Token) where T : class, new()
        {

            HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Token);
                url = url +"/"+ id;
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return JsonConvert.DeserializeObject<bool>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }
            finally
            {
                client.Dispose();

            }

        }
        #endregion

    }
}
