using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBrake.Negocio
{
   public class TipoEmpresa
    {
        public int IdTipoEmpresa { get; set; }
        public string Descripcion { get; set; }

        public TipoEmpresa()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoEmpresa = 0;
            Descripcion = string.Empty;
        }



        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {

                /* Se obtiene el primer registro coincidente con el id */
                Datos.TipoEmpresa emp = bbdd.TipoEmpresa.First(e => e.IdTipoEmpresa == IdTipoEmpresa);

                /* Se copian las propiedades de datos al negocio */
                CommonBC.Syncronize(emp, this);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TipoEmpresa> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.TipoEmpresa> listaDatos = bbdd.TipoEmpresa.ToList<Datos.TipoEmpresa>();

                List<TipoEmpresa> listaNegocio = GenerarListado(listaDatos);

                return listaNegocio;

            }
            catch (Exception ex)
            {
                return new List<TipoEmpresa>();
            }
        }
        private List<TipoEmpresa> GenerarListado(List<Datos.TipoEmpresa> listaDatos)
        {
            List<TipoEmpresa> listaNegocio = new List<TipoEmpresa>();

            foreach (Datos.TipoEmpresa dato in listaDatos)
            {
                TipoEmpresa negocio = new TipoEmpresa();
                CommonBC.Syncronize(dato, negocio);

                listaNegocio.Add(negocio);
            }


            return listaNegocio;
        }
    }
}
