using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnBrake.Negocio
{
    public class Cliente
    {
        /*Campo*/
        string _descripcionEmpresa;
        string _descripcionTipo;
        /*Propiedades de la entidad*/
        public string RutCliente { get; set; }
        public string NombreContacto { get; set; }
        public string MailContacto { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }

        public int IdTipoEmpresa { get; set; }
        public int IdActividadEmpresa { get; set; }
        /*propiedades Customizadas*/
        public string DescripcionTipo { get { return _descripcionTipo; } }
        public string DescripcionEmpresa { get { return _descripcionEmpresa; } }



        public Cliente()
        {
            this.Init();
        }

        private void Init()
        {
            RutCliente = string.Empty;
            RazonSocial = string.Empty;
            NombreContacto = string.Empty;
            Direccion = string.Empty;
            IdActividadEmpresa = 0;
            MailContacto = string.Empty;
            IdTipoEmpresa = 0;
            _descripcionTipo = string.Empty;
            _descripcionEmpresa = string.Empty;
        }

        public void LeerDescripcionTipo()
        {
            TipoEmpresa tipo = new TipoEmpresa() { IdTipoEmpresa = IdTipoEmpresa };

            if (tipo.Read())
            {
                _descripcionTipo = tipo.Descripcion;
            }
            else
            {
                _descripcionTipo = string.Empty;
            }
        }
        public void LeerDescripcionEmpresa()
        {
            ActividadEmpresa empresa = new ActividadEmpresa() { IdActividadEmpresa = IdActividadEmpresa };

            if (empresa.Read())
            {
                _descripcionEmpresa = empresa.Descripcion;
            }
            else
            {
                _descripcionEmpresa = string.Empty;
            }
        }

        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Cliente cli = new Datos.Cliente();
            try
            {
                CommonBC.Syncronize(this, cli);
                bbdd.Cliente.Add(cli);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                bbdd.Cliente.Remove(cli);
                return false;
            }
        }
        public bool Delete()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                /* Se obtiene el primer registro coincidente con el CODIGO MODELO */
                Datos.Cliente cli = bbdd.Cliente.First(e => e.RutCliente == RutCliente);

                /* Se elimina el registro del EDM */
                bbdd.Cliente.Remove(cli);

                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);

                return false;
            }
        }
        public bool Update()
        {
            
                Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

                try
                {
                    Datos.Cliente cli = bbdd.Cliente.First(e => e.RutCliente == RutCliente);
                    CommonBC.Syncronize(this, cli);

                    
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
                /* Se obtiene el primer registro coincidente con el Rut */
                Datos.Cliente emp = bbdd.Cliente.First(e => e.RutCliente == RutCliente);

                /* Se copian las propiedades de datos al negocio */
                CommonBC.Syncronize(emp, this);
                LeerDescripcionTipo();
                LeerDescripcionEmpresa();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private List<Cliente> GenerarListado(List<Datos.Cliente> listadoDatos)
        {
            List<Cliente> listaNegocio = new List<Cliente>();

            foreach (Datos.Cliente dato in listadoDatos)
            {

                Cliente negocio = new Cliente();
                CommonBC.Syncronize(dato, negocio);
                negocio.LeerDescripcionTipo();
                negocio.LeerDescripcionEmpresa();

                listaNegocio.Add(negocio);
            }

            return listaNegocio;
        }
        public List<Cliente> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                /* Se obtiene todos los registro desde la tabla */
                List<Datos.Cliente> listadoDatos = bbdd.Cliente.ToList<Datos.Cliente>();

                /* Se convierte el listado de datos en un listado de negocio */
                List<Cliente> listadoNegocio = GenerarListado(listadoDatos);
                LeerDescripcionTipo();


                /* Se retorna la lista */
                return listadoNegocio;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);
                return new List<Cliente>();
            }
        }
       
        public List<Cliente> ReadByRut(string codigoTipo)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Cliente> listaDatos =
                    bbdd.Cliente.Where(b => b.RutCliente == RutCliente).ToList<Datos.Cliente>();

                List<Cliente> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);
                return new List<Cliente>();
            }
        }

    }
}
