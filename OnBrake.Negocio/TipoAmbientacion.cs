using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBrake.Negocio
{
    public class TipoAmbientacion
    {
        public int IdTipoAmbientacion { get; set; }
        public string Descripcion { get; set; }

        public TipoAmbientacion()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoAmbientacion = 0;
            Descripcion = string.Empty;
        }



        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {

                /* Se obtiene el primer registro coincidente con el id */
                Datos.TipoAmbientacion tpa = bbdd.TipoAmbientacion.First(e => e.IdTipoAmbientacion == IdTipoAmbientacion);

                /* Se copian las propiedades de datos al negocio */
                CommonBC.Syncronize(tpa, this);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TipoAmbientacion> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.TipoAmbientacion> listaDatos = bbdd.TipoAmbientacion.ToList<Datos.TipoAmbientacion>();

                List<TipoAmbientacion> listaNegocio = GenerarListado(listaDatos);

                return listaNegocio;

            }
            catch (Exception ex)
            {
                return new List<TipoAmbientacion>();
            }
        }
        private List<TipoAmbientacion> GenerarListado(List<Datos.TipoAmbientacion> listaDatos)
        {
            List<TipoAmbientacion> listaNegocio = new List<TipoAmbientacion>();

            foreach (Datos.TipoAmbientacion dato in listaDatos)
            {
                TipoAmbientacion negocio = new TipoAmbientacion();
                CommonBC.Syncronize(dato, negocio);

                listaNegocio.Add(negocio);
            }


            return listaNegocio;
        }
    }

}
