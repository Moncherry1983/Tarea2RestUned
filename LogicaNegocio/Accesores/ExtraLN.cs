using Entidades;
using System;

namespace LogicaNegocio.Accesores
{
    public class ExtraLN
    {
        public void AgregarExtra(Extra ingresarExtra)
        {
            try
            {
                //    var ExtraActuales = ExtraAD.ListarExtra();
                //    if (ExtraActuales.Where(ext => ext != null && ext.IdExtra == ingresarExtra.IdExtra).Count() == 0)
                //        ExtraAD.AgregarExtra(ingresarExtra);
                //    else
                //        throw new Exception("Ya existe el id extra que quieres agregar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Extra[] ListarExtra()
        {
            try
            {
                //return ExtraAD.ListarExtra();
                return new Extra[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}