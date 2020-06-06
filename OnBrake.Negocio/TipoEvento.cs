using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBrake.Negocio
{
    public class TipoEvento
    {

        public int IdTipoEvento { get; set; }
        public string Descripcion { get; set; }

        public TipoEvento()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoEvento = 0;
            Descripcion = string.Empty;
        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {

                /* Se obtiene el primer registro coincidente con el id */
                Datos.TipoEvento TipoE = bbdd.TipoEvento.First(e => e.IdTipoEvento == IdTipoEvento);

                /* Se copian las propiedades de datos al negocio */
                CommonBC.Syncronize(TipoE, this);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<TipoEvento> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.TipoEvento> listaDatos = bbdd.TipoEvento.ToList<Datos.TipoEvento>();

                List<TipoEvento> listaNegocio = GenerarListado(listaDatos);

                return listaNegocio;

            }
            catch (Exception ex)
            {
                return new List<TipoEvento>();
            }
        }
        private List<TipoEvento> GenerarListado(List<Datos.TipoEvento> listaDatos)
        {
            List<TipoEvento> listaNegocio = new List<TipoEvento>();

            foreach (Datos.TipoEvento dato in listaDatos)
            {
                TipoEvento negocio = new TipoEvento();
                CommonBC.Syncronize(dato, negocio);

                listaNegocio.Add(negocio);
            }


            return listaNegocio;
        }
    }
}
