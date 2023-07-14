
//La capa lógica del negocio es el corazón de tu proyecto de programación! Aquí es donde das vida a tu sistema, aplicando las reglas,
//los procesos y las operaciones que lo hacen funcionar de forma eficiente y efectiva.
using System;
using Entidades;
using AccesoDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using static LogicaNegocio.PlatoRestauranteLN;

namespace LogicaNegocio
{   
    public class RestauranteLN
    {

        //Esta función permite agregar un nuevo restaurante a la array de objetos, siempre y cuando no exista otro con el mismo identificador.Para ello, consulta la lista
        //de restaurantes actuales y verifica que no haya ninguno con el mismo IdRestaurante que el que se quiere agregar.Si no hay ningún conflicto, llama al método 
        //AgregarRestaurante de la clase RestauranteAD, que se encarga de insertar el restaurante en la base de datos.Si hay algún error o excepción, la función lo propaga
        //hacia arriba para que sea manejado por el código que la invoca.
        public void AgregarRestaurante(Restaurante restaurante)
        {
            try
            {
                var restaurantesActuales= RestauranteAD.ListarRestaurante();                
                if (restaurantesActuales.Where(rest => rest != null && rest.IdRestaurante == restaurante.IdRestaurante).Count() == 0)
                    RestauranteAD.AgregarRestaurante(restaurante);
                else
                    throw new Exception("Ya existe un restaurate con ese Id");

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //Esta función te permite acceder a todos los restaurantes que tenemos en nuestra array.Solo tienes que usar el método ListarRestaurante de la clase RestauranteAD, 
        //que se ocupa de hacer la consulta SQL y devolverte un array de objetos Restaurante.Si algo sale mal, no te preocupes, la función lo detecta y lo lanza como una excepción           
        //para que puedas manejarlo como quieras.Así te aseguras de que tus datos están protegidos y actualizados.
        public Restaurante[] ListarRestaurantes()
        {

            try
            {
                return RestauranteAD.ListarRestaurante();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //Este método es para encontrar los restaurantes que están en el array y  Usa la clase RestauranteAD para obtener una colección de objetos Restaurante y luego
        //aplica un filtro para quedarte solo con los que tienen el Estado en true. Así, solo verás los restaurantes que funcionan y que puedes visitar.
        //Al final, convierte la colección en un arreglo de Restaurante, que es lo que devuelve el método.Si algo sale mal, te avisa con una excepción y un mensaje explicativo.
        public Restaurante[] ListarRestaurantesActivos()
        {

            try
            {
                return RestauranteAD.ListarRestaurante().Where(cat => cat != null && cat.Estado == true).ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Restaurante ObtenerRestaurantePorId(int id)
        {

            try
            {
                return RestauranteAD.ListarRestaurante().Where(rest => rest != null && (rest.Estado == true && rest.IdRestaurante == id)).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
