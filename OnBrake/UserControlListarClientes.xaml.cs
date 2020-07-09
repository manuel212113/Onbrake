using OnBrake.Negocio;
using System.Windows;
using System.Windows.Controls;

namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para UserControlListarClientes.xaml
    /// </summary>

    public partial class UserControlListarClientes : UserControl
    {
        Cliente cliente = new Cliente();
        public UserControlListarClientes()
        {
            InitializeComponent();
            CargarDatagridCliente();
            

        }

        public void CargarDatagridCliente()
        {
             DataGridClientes.ItemsSource=cliente.ReadAll();

        }

        private void TxtConsulta_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

           if (COMBOTIPO.Text.Equals("RUT") && txtConsulta.Text.Length>=2)
            { 
              
              DataGridClientes.ItemsSource = cliente.ReadByRut(txtConsulta.Text.ToString());
                 
            }else if(COMBOTIPO.Text.Equals("Nombre") && txtConsulta.Text.Length >= 2)
            {
                DataGridClientes.ItemsSource = cliente.ReadByNombre(txtConsulta.Text.ToString());
            }
           else if (txtConsulta.Text.Length == 0)
            {
              DataGridClientes.ItemsSource = cliente.ReadAll();

            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            TextBlock celdaSeleccionada = DataGridClientes.Columns[0].GetCellContent(DataGridClientes.Items[1]) as TextBlock;
            if (celdaSeleccionada != null)
                MessageBox.Show("Estas"+celdaSeleccionada.Text);
        }
    }
}
