using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocios
{
    public static class ValidacionArrays
    {
        // Método que permite buscar un restaurante por su id en el arreglo
        public static Restaurante BuscarRestaurante(Restaurante[] restaurantes, int id)
        {
            // Variable para almacenar el resultado de la búsqueda
            Restaurante encontrado = null;

            // Recorrer el arreglo hasta encontrar el restaurante con el id buscado o hasta llegar al final del arreglo
            for (int i = 0; i < 20 && encontrado == null; i++)
            {
                // Verificar si el elemento actual del arreglo no es nulo y tiene el id buscado
                if (restaurantes[i] != null && restaurantes[i].IdRestaurante == id)
                {
                    // Asignar el elemento actual del arreglo a la variable resultado
                    encontrado = restaurantes[i];
                }
            }

            // Retornar la variable resultado
            return encontrado;
        }

        // Método que permite buscar Categoria Plato por su id en el arreglo
        public static CategoriaPlato BuscarCategoriaPlato(CategoriaPlato[] categorias, int id)
        {
            // Variable para almacenar el resultado de la búsqueda
            CategoriaPlato encontrado = null;

            // Recorrer el arreglo hasta encontrar el restaurante con el id buscado o hasta llegar al final del arreglo
            for (int i = 0; i < 20 && encontrado == null; i++)
            {
                // Verificar si el elemento actual del arreglo no es nulo y tiene el id buscado
                if (categorias[i] != null && categorias[i].IdCategoria == id)
                {
                    // Asignar el elemento actual del arreglo a la variable resultado
                    encontrado = categorias[i];
                }
            }

            // Retornar la variable resultado
            return encontrado;
        }

        // Método que permite buscar un Plato por su id en el arreglo
        public static Plato BuscarPlato(Plato[] ingresarPlato, int id)
        {
            // Variable para almacenar el resultado de la búsqueda
            Plato encontrado = null;

            // Recorrer el arreglo hasta encontrar el restaurante con el id buscado o hasta llegar al final del arreglo
            for (int i = 0; i < 20 && encontrado == null; i++)
            {
                // Verificar si el elemento actual del arreglo no es nulo y tiene el id buscado
                if (ingresarPlato[i] != null && ingresarPlato[i].IdPlato == id)
                {
                    // Asignar el elemento actual del arreglo a la variable resultado
                    encontrado = ingresarPlato[i];
                }
            }

            // Retornar la variable resultado
            return encontrado;
        }


        // Método que permite buscar un Plato por su id en el arreglo
        public static Cliente BuscarCliente(Cliente[] ingresarCliente, string idcedula)
        {
            // Variable para almacenar el resultado de la búsqueda
            Cliente encontrado = null;

            // Recorrer el arreglo hasta encontrar el restaurante con el id buscado o hasta llegar al final del arreglo
            for (int i = 0; i < 20 && encontrado == null; i++)
            {
                // Verificar si el elemento actual del arreglo no es nulo y tiene el id buscado
                if (ingresarCliente[i] != null && ingresarCliente[i].IdPersona == idcedula)
                {
                    // Asignar el elemento actual del arreglo a la variable resultado
                    encontrado = ingresarCliente[i];
                }
            }

            // Retornar la variable resultado
            return encontrado;
        }

    }
}
