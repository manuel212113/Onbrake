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
        /*Conexion Base de datos (Patron Singleton)*/
        Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();


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





        /*LISTA CONTRATO CON CAMPOS DE OTRAS TABLAS PARA MOSTRAR EN LA TABLA LISTAR CONTRATOS*/

        public class ListaContrato
        {
            public string Numero { get; set; }
            public DateTime Creacion { get; set; }
            public DateTime Termino { get; set; }
            public string RutCliente { get; set; }
            public string Modalidad { get; set; }
            public String TipoEvento { get; set; }
            public DateTime FechaHoraInicio { get; set; }
            public DateTime FechaHoraTermino { get; set; }
            public int Asistentes { get; set; }
            public int PersonalAdicional { get; set; }
            public bool Realizado { get; set; }
            public double ValorTotalContrato { get; set; }
            public string Observaciones { get; set; }

            public ListaContrato()
            {

            }
        }


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

            try
            {
                /* Se obtiene el primer registro coincidente con el numero */


                var contrato = bbdd.Contrato.Where(s => s.Numero == Numero).First();

                contrato.Realizado = true;

                CommonBC.Syncronize(this, contrato);

                bbdd.SaveChanges();

                return true;


            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Update()
        {


            try
            {
                Datos.Contrato cont = bbdd.Contrato.First(e => e.Numero == Numero);
                CommonBC.Syncronize(this, cont);


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




        public List<ListaContrato> FiltroRut(string rut)
        {
            var co = from con in bbdd.Contrato
                     join temp in bbdd.Cliente on con.RutCliente equals temp.RutCliente
                     join mod in bbdd.ModalidadServicio on con.IdModalidad equals mod.IdModalidad
                     join tip in bbdd.TipoEvento on con.IdTipoEvento equals tip.IdTipoEvento
                     where con.RutCliente.StartsWith(rut)

                     select new ListaContrato()
                     {
                         Numero = con.Numero,
                         Creacion = con.Creacion,
                         Termino = con.Termino,
                         RutCliente = con.RutCliente,
                         Modalidad = mod.Nombre,
                         TipoEvento = tip.Descripcion,
                         FechaHoraInicio = con.FechaHoraInicio,
                         FechaHoraTermino = con.FechaHoraTermino,
                         Asistentes = con.Asistentes,
                         PersonalAdicional = con.PersonalAdicional,
                         Realizado = con.Realizado,
                         ValorTotalContrato = con.ValorTotalContrato,
                         Observaciones = con.Observaciones

                     };

            return co.ToList();
        }


        public List<ListaContrato> FiltroTipoEvento(string tipo)
        {
            var co = from con in bbdd.Contrato
                     join modal in bbdd.ModalidadServicio on con.IdModalidad equals modal.IdModalidad
                     join tip in bbdd.TipoEvento on con.IdTipoEvento equals tip.IdTipoEvento
                     where tip.Descripcion.StartsWith(tipo)

                     select new ListaContrato()
                     {
                         Numero = con.Numero,
                         Creacion = con.Creacion,
                         Termino = con.Termino,
                         RutCliente = con.RutCliente,
                         Modalidad = modal.Nombre,
                         TipoEvento = tip.Descripcion,
                         FechaHoraInicio = con.FechaHoraInicio,
                         FechaHoraTermino = con.FechaHoraTermino,
                         Asistentes = con.Asistentes,
                         PersonalAdicional = con.PersonalAdicional,
                         Realizado = con.Realizado,
                         ValorTotalContrato = con.ValorTotalContrato,
                         Observaciones = con.Observaciones

                     };

            return co.ToList();
        }

        public List<ListaContrato> ReadByModalidad(string mod)
        {
            var cont = from contra in bbdd.Contrato
                       join modal in bbdd.ModalidadServicio on contra.IdModalidad equals modal.IdModalidad
                       join tip in bbdd.TipoEvento on contra.IdTipoEvento equals tip.IdTipoEvento
                       where modal.Nombre.StartsWith(mod)

                       select new ListaContrato()
                       {
                           Numero = contra.Numero,
                           Creacion = contra.Creacion,
                           Termino = contra.Termino,
                           RutCliente = contra.RutCliente,
                           Modalidad = modal.Nombre,
                           TipoEvento = tip.Descripcion,
                           FechaHoraInicio = contra.FechaHoraInicio,
                           FechaHoraTermino = contra.FechaHoraTermino,
                           Asistentes = contra.Asistentes,
                           PersonalAdicional = contra.PersonalAdicional,
                           Realizado = contra.Realizado,
                           ValorTotalContrato = contra.ValorTotalContrato,
                           Observaciones = contra.Observaciones

                       };

            return cont.ToList();
        }


        public List<ListaContrato> FiltroNumeroContrato(string num)
        {
            var co = from con in bbdd.Contrato
                     join modal in bbdd.ModalidadServicio on con.IdModalidad equals modal.IdModalidad
                     join tip in bbdd.TipoEvento on con.IdTipoEvento equals tip.IdTipoEvento
                     where con.Numero.StartsWith(num)

                     select new ListaContrato()
                     {
                         Numero = con.Numero,
                         Creacion = con.Creacion,
                         Termino = con.Termino,
                         RutCliente = con.RutCliente,
                         Modalidad = modal.Nombre,
                         TipoEvento = tip.Descripcion,
                         FechaHoraInicio = con.FechaHoraInicio,
                         FechaHoraTermino = con.FechaHoraTermino,
                         Asistentes = con.Asistentes,
                         PersonalAdicional = con.PersonalAdicional,
                         Realizado = con.Realizado,
                         ValorTotalContrato = con.ValorTotalContrato,
                         Observaciones = con.Observaciones

                     };

            return co.ToList();
        }


        public List<ListaContrato> ReadAllDescripcion()  //lista con la descripcion De Modalidad
        {
            try
            {
                var c = from con in bbdd.Contrato
                        join modal in bbdd.ModalidadServicio on con.IdModalidad equals modal.IdModalidad
                        join tip in bbdd.TipoEvento on con.IdTipoEvento equals tip.IdTipoEvento
                        join cli in bbdd.Cliente on con.RutCliente equals cli.RutCliente

                        select new ListaContrato()
                        {

                            Numero = con.Numero,
                            Creacion = con.Creacion,
                            Termino = con.Termino,
                            RutCliente = con.RutCliente,
                            Modalidad = modal.Nombre,
                            TipoEvento = tip.Descripcion,
                            FechaHoraInicio = con.FechaHoraInicio,
                            FechaHoraTermino = con.FechaHoraTermino,
                            Asistentes = con.Asistentes,
                            PersonalAdicional = con.PersonalAdicional,
                            Realizado = con.Realizado,
                            ValorTotalContrato = con.ValorTotalContrato,
                            Observaciones = con.Observaciones

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
    }
}
