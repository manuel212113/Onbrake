using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OnBrake.Negocio;
using System.Speech.Synthesis;




namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para UserControlAdmiCliente.xaml
    /// </summary>
    public partial class UserControlAdmiCliente : UserControl
    {


        public UserControlAdmiCliente()
        {
            InitializeComponent();
            LimpiarControles();



        }

        private void CargarTipos()
        {
            CombtipoCli.ItemsSource = new TipoEmpresa().ReadAll();
            CombtipoCli.DisplayMemberPath = "Descripcion";
            CombtipoCli.SelectedValuePath = "IdTipoEmpresa";
            CombtipoCli.SelectedIndex = 0;
        }


        private void CargarActividadEmpresa()
        {
            CombActividadCli.ItemsSource = new ActividadEmpresa().ReadAll();
            CombActividadCli.DisplayMemberPath = "Descripcion";
            CombActividadCli.SelectedValuePath = "IdActividadEmpresa";
            CombActividadCli.SelectedIndex = 0;
        }


        private void LimpiarControles()
        {
            txtDireccionCli.Text = string.Empty;
            txtRutCli.Text = string.Empty;
            txtTelefonoCli.Text = string.Empty;
            txtemailCli.Text = string.Empty;
            txtRazonSocialCli.Text = string.Empty;
            txtNombreCli.Text = string.Empty;

            CargarTipos();

            CargarActividadEmpresa();
        }


        public string FormatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {

                    format = rut.Substring(i, 1) + format;

                    cont++;
                   
                }
                return format;
            }
        }










        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutCli.Text) || string.IsNullOrEmpty(txtNombreCli.Text) && string.IsNullOrEmpty(txtDireccionCli.Text) || string.IsNullOrEmpty(txtRazonSocialCli.Text) || string.IsNullOrEmpty(txtTelefonoCli.Text))
            {
                if (string.IsNullOrEmpty(txtRutCli.Text) && string.IsNullOrEmpty(txtNombreCli.Text) && string.IsNullOrEmpty(txtDireccionCli.Text) && string.IsNullOrEmpty(txtRazonSocialCli.Text) && string.IsNullOrEmpty(txtTelefonoCli.Text))
                {
                    MessageBox.Show("Debes Rellenar los Campos");
                }
                MessageBox.Show("Debes Rellenar Todos los Campos");
            }
            else
            {
                if (!txtRutCli.Equals("") && !txtNombreCli.Equals("") && !txtDireccionCli.Equals("") && !txtemailCli.Equals("") && !txtRazonSocialCli.Equals("") && !txtTelefonoCli.Equals(""))
                {

                    Cliente cli = new Cliente()
                    {
                        RutCliente = (txtRutCli.Text),
                        RazonSocial = (txtRazonSocialCli.Text),
                        NombreContacto = txtNombreCli.Text,
                        MailContacto = (txtemailCli.Text),
                        Direccion = txtDireccionCli.Text,
                        Telefono = (txtTelefonoCli.Text),
                        IdActividadEmpresa = (int)CombActividadCli.SelectedValue,
                        IdTipoEmpresa = (int)CombtipoCli.SelectedValue
                    };
                    /* Solicita la actualización del registro */
                    if (cli.Update())
                    {
                        var background = new BrushConverter();

                        flyoutAgregado.Background = (Brush)background.ConvertFrom("#FF02C122");
                        txtFlyout.FontSize = 28;
                        txtFlyout.Text = "El Cliente " + Environment.NewLine +
                            "fue Actualizado " + Environment.NewLine + "Correctamente";
                        flyoutAgregado.Header = "Cliente Actualizado Correctamente";
                        IconoFlyout.Kind = MaterialDesignThemes.Wpf.PackIconKind.Update;
                        flyoutAgregado.IsOpen = true;
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("El Cliente no pudo ser modificado", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {

                }
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRutCli.Text) || string.IsNullOrEmpty(txtNombreCli.Text) && string.IsNullOrEmpty(txtDireccionCli.Text) || string.IsNullOrEmpty(txtRazonSocialCli.Text) || string.IsNullOrEmpty(txtTelefonoCli.Text))
                {
                    if (string.IsNullOrEmpty(txtRutCli.Text) && string.IsNullOrEmpty(txtNombreCli.Text) && string.IsNullOrEmpty(txtDireccionCli.Text) && string.IsNullOrEmpty(txtRazonSocialCli.Text) && string.IsNullOrEmpty(txtTelefonoCli.Text))
                    {
                        MessageBox.Show("Debes Rellenar los Campos");
                    }
                    MessageBox.Show("Debes Rellenar Todos los Campos");
                } 
                else
                {



                    if (!txtRutCli.Equals("") && !txtNombreCli.Equals("") && !txtDireccionCli.Equals("") && !txtemailCli.Equals("") && !txtRazonSocialCli.Equals("") && !txtTelefonoCli.Equals(""))
                    {



                        Cliente cli = new Cliente()
                        {
                            RutCliente = (txtRutCli.Text),
                            RazonSocial = (txtRazonSocialCli.Text),
                            NombreContacto = txtNombreCli.Text,
                            MailContacto = (txtemailCli.Text),
                            Direccion = txtDireccionCli.Text,
                            Telefono = (txtTelefonoCli.Text),
                            IdActividadEmpresa = (int)CombActividadCli.SelectedValue,
                            IdTipoEmpresa = (int)CombtipoCli.SelectedValue
                        };


                        if (cli.Create())
                        {
                            var background = new BrushConverter();
                           
                            flyoutAgregado.Background = (Brush)background.ConvertFrom("#FF0078D7");
                            txtFlyout.FontSize = 28;
                            txtFlyout.Text = "El Cliente " + Environment.NewLine +
                                "fue Agregado " + Environment.NewLine + "Correctamente";
                            flyoutAgregado.Header = "Cliente Agregado Correctamente";
                            IconoFlyout.Kind = MaterialDesignThemes.Wpf.PackIconKind.UserAdd;
                            flyoutAgregado.IsOpen = true;
                            txtFlyout.Visibility = Visibility.Visible;
                            LimpiarControles();



                        }
                        else
                        {
                            MessageBox.Show("El Cliente no pudo ser creado", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debes rellenar todos los  Campos de Textos");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:" + ex);
            }



        }
      

        private void TxtRutCli_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            /*se formatea el rut al salir del campo txtrut*/
            txtRutCli.Text = FormatearRut(txtRutCli.Text);

        }

        private void TxtTelefonoCli_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            /* metodo para que el usuario solo le permita poner numeros en el campo txtTelefono*/
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back))
            {
                e.Handled = false;
            }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {

            Cliente cli = new Cliente()
            {
                RutCliente = (txtRutCli.Text)
            };
            /* solicita la informacion del cliente la cual sera desplegada en los campo de texto y combobox*/
            if (cli.Read())
            {
                txtRazonSocialCli.Text = cli.RazonSocial;
                txtNombreCli.Text = cli.NombreContacto;
                txtemailCli.Text= cli.MailContacto;
                txtDireccionCli.Text = cli.Direccion;
                txtTelefonoCli.Text = cli.Telefono;
                CombActividadCli.SelectedValue = cli.IdActividadEmpresa;
                CombtipoCli.SelectedValue = cli.IdTipoEmpresa;
                MessageBox.Show("Cliente Encontrado");
            }
            else
            {
                MessageBox.Show("El Cliente Buscado No Existe");
            }
        }

    

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente()
            {
                RutCliente = txtRutCli.Text,
            };

            if (!cli.Delete())
            {
                MessageBox.Show("El Cliente no Existe");
            }
            MessageBoxResult eliminar = MessageBox.Show("¿Eliminar este Cliente", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (eliminar == MessageBoxResult.Yes)
            {
                    
                
                    /* Solicita la eliminación  del registro */
                     if (cli.Delete())
                    {

                        var background = new BrushConverter();

                        flyoutAgregado.Background = (Brush)background.ConvertFrom("#FFD30707");
                        txtFlyout.FontSize = 28;
                        txtFlyout.Text = "El Cliente " + Environment.NewLine +
                            "fue Eliminado " + Environment.NewLine + "Correctamente";
                        flyoutAgregado.Header = "Cliente Eliminado Correctamente";
                        IconoFlyout.Kind = MaterialDesignThemes.Wpf.PackIconKind.Delete;
                        flyoutAgregado.IsOpen = true;
                        txtFlyout.Visibility = Visibility.Visible;
                        LimpiarControles();
                    }
                    else
                    {
                    MessageBox.Show("El cliente no pudo ser eliminado");       
                    }



            }
            else
            {
                MessageBox.Show("Operacion Cancelada");
            }
        }
    }
}
