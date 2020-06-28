using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnBrake.Negocio
{
    public class CoffeBreak
    {

        public string Numero { get; set; }
        public bool Vegetariana { get; set; }




        public CoffeBreak()
        {
            this.Init();
        }

        public void Init()
        {
            Numero = string.Empty;
            Vegetariana = false;
        }







        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.CoffeeBreak  coffee= new Datos.CoffeeBreak();
            try
            {
                CommonBC.Syncronize(this, coffee);
                bbdd.CoffeeBreak.Add(coffee);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                bbdd.CoffeeBreak.Remove(coffee);
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
