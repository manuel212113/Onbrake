using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnBrake.Negocio
{
   public class Contrato
    {
        /*Campo*/
     
        /*Propiedades de la entidad*/
        public string Numero { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime Termino { get; set; }
        public string RutCliente { get; set; }
        public string IdModalidad { get; set; }
        public int IdTipoEvento { get; set; }

        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraTermino { get; set; }
        public int Asistentes { get; set; }
        public int PersonalAdicional { get; set; }
        public bool Realizado { get; set; }
        public double ValorTotalContrato { get; set; }
        public string Observaciones { get; set; }





        /*propiedades Customizadas*/
     



        public Contrato()
        {
            this.Init();
        }

        private void Init()
        {
            Numero = string.Empty;
            Creacion = DateTime.MinValue;
            Termino = DateTime.MinValue;
            RutCliente = string.Empty;
            IdModalidad = string.Empty;
            IdTipoEvento = 0;
            FechaHoraInicio = DateTime.MinValue;
            FechaHoraTermino = DateTime.MinValue; 
            Asistentes = 0;
            PersonalAdicional = 0;
            Realizado = false;
            ValorTotalContrato = 0;
            Observaciones = string.Empty;

        }

   
  

        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Contrato Cont = new Datos.Contrato();
            try
            {
                CommonBC.Syncronize(this, Cont);
                bbdd.Contrato.Add(Cont);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                bbdd.Contrato.Remove(Cont);
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public bool Delete()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                /* Se obtiene el primer registro coincidente con el rut */
                Datos.Contrato Cont = bbdd.Contrato.First(e => e.Numero == Numero);
                CommonBC.Syncronize(this, Cont);


                /* Se elimina el registro del  cliente*/

                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    
        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                /* Se obtiene el primer registro coincidente con el numero contrato */
                Datos.Contrato Cont = bbdd.Contrato.First(e => e.Numero == Numero);

                /* Se copian las propiedades de datos al negocio */
                CommonBC.Syncronize(Cont, this);
              
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private List<Contrato> GenerarListado(List<Datos.Contrato> listadoDatos)
        {
            List<Contrato> listaNegocio = new List<Contrato>();

            foreach (Datos.Contrato dato in listadoDatos)
            {

                Contrato negocio = new Contrato();
                CommonBC.Syncronize(dato, negocio);
             

                listaNegocio.Add(negocio);
            }

            return listaNegocio;
        }
        public List<Contrato> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                /* Se obtiene todos los registro desde la tabla */
                List<Datos.Contrato> listadoDatos = bbdd.Contrato.ToList<Datos.Contrato>();

                /* Se convierte el listado de datos en un listado de negocio */
                List<Contrato> listadoNegocio = GenerarListado(listadoDatos);


                /* Se retorna la lista */
                return listadoNegocio;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadByNumero(string codigoTipo)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contrato> listaDatos =
                    bbdd.Contrato.Where(b => b.Numero == Numero).ToList<Datos.Contrato>();

                List<Contrato> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }
    }
}
