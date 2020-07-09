using OnBrake.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para UserControlListarContrato.xaml
    /// </summary>
    public partial class UserControlListarContrato : UserControl

    {


        Contrato cont = new Contrato();

        public UserControlListarContrato()
        {
            InitializeComponent();
            CargarDatagridContrato();
        }


        public void CargarDatagridContrato()
        {
            

            DataGridClientes.ItemsSource = cont.ReadAllDescripcion();

        }

        private void TxtConsulta_KeyDown(object sender, KeyEventArgs e)
        {

            if (COMBOTIPO.Text.Equals("RUT") && txtConsulta.Text.Length >= 2)
            {

                DataGridClientes.ItemsSource = cont.FiltroRut(txtConsulta.Text.ToString());

            }
            else if (COMBOTIPO.Text.Equals("Modalidad") && txtConsulta.Text.Length >= 2)
            {
                DataGridClientes.ItemsSource = cont.ReadByModalidad(txtConsulta.Text.ToString());
            }

            else if (COMBOTIPO.Text.Equals("Tipo Evento") && txtConsulta.Text.Length >= 2)
            {
                DataGridClientes.ItemsSource = cont.FiltroTipoEvento(txtConsulta.Text.ToString());
            }

            else if (COMBOTIPO.Text.Equals("Numero Contrato") && txtConsulta.Text.Length >= 2)
            {
                DataGridClientes.ItemsSource = cont.FiltroNumeroContrato(txtConsulta.Text.ToString());
            }
            else if (txtConsulta.Text.Length == 0)
            {
                DataGridClientes.ItemsSource = cont.ReadAllDescripcion();

            }

        }
    }
}
