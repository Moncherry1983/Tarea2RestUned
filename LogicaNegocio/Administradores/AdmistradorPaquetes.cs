using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public static class AdmistradorPaquetes
    {
        //Este método convierte un objeto en una cadena de texto con un formato especial llamado JSON.
        //Este formato permite guardar y enviar información de manera fácil y segura.
        //El método usa una configuración especial para incluir el tipo de cada objeto en el JSON.
        //Si ocurre algún error durante la conversión, el método lo reporta y lo pasa al código que lo llamó.
        public static string SerializePackage(dynamic paquete)
        {
            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                var json = JsonConvert.SerializeObject(paquete, jsonSerializerSettings);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // este código es una función que toma un mensaje en formato JSON,
        // lo convierte en un objeto de datos dinámico y lo devuelve para que pueda ser utilizado
        // en el programa de manera flexible, sin necesidad de saber su estructura específica que viene.
        public static dynamic DeserializePackage(string mensaje)
        {
            try
            {
                var jsonCorregido = mensaje.TrimEnd('\u0013');

                var jsonDeserializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                var objData = JsonConvert.DeserializeObject<dynamic>(jsonCorregido, jsonDeserializerSettings);
                return objData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
