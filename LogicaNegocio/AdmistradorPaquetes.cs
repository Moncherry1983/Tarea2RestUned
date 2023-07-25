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
