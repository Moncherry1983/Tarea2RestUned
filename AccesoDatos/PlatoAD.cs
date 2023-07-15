using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class PlatoAD
    {
        private static Plato[] platos = new Plato[20];

        public static void AgregarPlato(Plato ingresarPlatos)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < platos.Count(); i++)
            {

                if (platos[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                platos[contador] = ingresarPlatos;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static Entidades.Plato[] ListarPlatos()
        {
            try
            {
                return platos;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //public static Entidades.Plato[] ListarPlatos()
        //{
        //    try
        //    {
        //        Plato[] ingresarPlatos = new Plato[10];
        //        ingresarPlatos[0] = new Plato(1, "hamburguesa", 5000, 01);
        //        ingresarPlatos[1] = new Plato(2, "perros", 8000, 02);
        //        ingresarPlatos[2] = new Plato(3, "arroz con carne", 11000, 03);
        //        ingresarPlatos[3] = new Plato(4, "Camarones", 60000, 04);
        //        ingresarPlatos[4] = new Plato(5, "sopas", 2000, 05);
        //        //ingresarPlatos[5] = new Plato(6, "Sushi", 19000, 06);
        //        //ingresarPlatos[6] = new Plato(7, "helados", 700, 07);
        //        //ingresarPlatos[7] = new Plato(8, "Albondigas", 60000, 12);
        //        //ingresarPlatos[8] = new Plato(9, "sopas De Moluscos", 2000, 10);
        //        //ingresarPlatos[9] = new Plato(10, "carme en salsa", 19000, 15);

        //        return ingresarPlatos;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}





        public static Entidades.Plato ObtenerPlato(int idPlato)
        {

            return platos.Where(x => x.IdPlato == idPlato).FirstOrDefault();

        }

    }
}
