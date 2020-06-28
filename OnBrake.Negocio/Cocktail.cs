using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBrake.Negocio
{
   public class Cocktail
    {


        public string Numero { get; set; }
        public int IdTipoAmbientacion { get; set; }
        public bool Ambientacion { get; set; }
        public bool MusicaAmbiental { get; set; }
        public bool MusicaCliente { get; set; }





        public void Init()
        {
            Numero = string.Empty;
            IdTipoAmbientacion = 0;
            Ambientacion = false;
            MusicaAmbiental = false;
            MusicaCliente = false;



        }

        public Cocktail()
        {
            this.Init();
        }


        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Cocktail cocktail = new Datos.Cocktail();
            try
            {
                CommonBC.Syncronize(this, cocktail);
                bbdd.Cocktail.Add(cocktail);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                bbdd.Cocktail.Remove(cocktail);
                return false;
            }
        }

    }
}
