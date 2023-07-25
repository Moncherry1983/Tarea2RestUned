
//La capa de acceso a datos es una parte importante de cualquier aplicación que necesita trabajar con una base de datos o un servicio externo.
//Su objetivo es simplificar la conexión, consulta y modificación de los datos, para que el resto de la aplicación los pueda usar de forma fácil y eficaz. 
//La capa de acceso a datos también se ocupa de las transacciones, los errores y la seguridad de los datos.
using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace AccesoDatos
{
    //Este método sirve para añadir un restaurante al arreglo de restaurantes.Primero, declara una variable contador y una variable revisión.
    //Luego, recorre el arreglo con un ciclo for y busca un espacio vacío.Si lo encuentra, guarda el índice en el contador y cambia la revisión a falso.
    //Después, sale del ciclo y verifica si la revisión es falsa. Si es así, asigna el restaurante al espacio vacío usando el contador.Si no, significa que el arreglo
    //está lleno y lanza una excepción.
    public static class RestauranteAD
    {
        private static Restaurante[] restaurantes = new Restaurante[20];
        public static void AgregarRestaurante(Restaurante restaurante)
        {
            int contador = 0;
            bool revision = true;
            for (int i = 0; i < restaurantes.Count(); i++)
            {

                if (restaurantes[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                restaurantes[contador] = restaurante;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static Restaurante[] ListarRestaurante()
        {
            try
            {
                return restaurantes;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        //Este método busca un restaurante en una lista de restaurantes según su identificador.Si lo encuentra, lo devuelve.Si no lo encuentra, devuelve null.
        public static Restaurante ObtenerRestaurante(int idRestaurante)
        {

            return restaurantes.Where(x => x.IdRestaurante == idRestaurante).FirstOrDefault();

        }

    }
}