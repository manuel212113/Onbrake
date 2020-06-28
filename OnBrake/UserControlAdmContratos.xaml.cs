using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using OnBrake.Negocio;

namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para UserControlAdmContratos.xaml
    /// </summary>
    public partial class UserControlAdmContratos : UserControl
    {
        UserControlAdmiCliente cliente = new UserControlAdmiCliente();
        CalculosContrato calculosContrato = new CalculosContrato();
        double valorBaseModalidad = 0;
        int tipoEvento = 0;
        int tipoAmbientacion = 0;
        int uf_ambientacion = 0;
        double valorfinal = 0;
        DispatcherTimer DdispatcherTimer = new DispatcherTimer();
          


        public UserControlAdmContratos()
        {
            InitializeComponent();
            CargarTipoEvento();
            CargarTipoAmbientacion();
            DdispatcherTimer.Interval = new TimeSpan(0, 0, 10);
           

        }





        private void BtnAgregarContrato_Click(object sender, RoutedEventArgs e)
        {


        }

        private void CargarTipoEvento()
        {
            ComboTipoEvento.ItemsSource = new TipoEvento().ReadAll();

            ComboTipoEvento.DisplayMemberPath = "Descripcion";
            ComboTipoEvento.SelectedValuePath = "IdTipoEvento";

            ComboTipoEvento.SelectedIndex = 0;
        }

        private void CargarTipoAmbientacion()
        {
            comboAmbientacion.ItemsSource = new TipoAmbientacion().ReadAll();

            comboAmbientacion.DisplayMemberPath = "Descripcion";
            comboAmbientacion.SelectedValuePath = "IdTipoAmbientacion";

            comboAmbientacion.SelectedIndex = 0;
        }

        public void CargarModalidad(int IdTipoEvento)
        {

            ModalidadServicio mod = new ModalidadServicio()

            {
                IdTipoEvento = IdTipoEvento
            };

            if (mod.Read())
            {
                comboModalidad.ItemsSource = mod.Nombre;
            }
            else
            {
                MessageBox.Show("El Cliente Buscado No Existe");
            }

        }




        public void TipoCalculoContrato(int tipoevento)
        {
            int CantPersonal = int.Parse(txtPersonalAdicional.Text);
            int cantAsistentes = int.Parse(txtAsistentes.Text);
            bool musicaAmbiental = false;

            if (chkboxMusicaAmbiental.IsChecked == true)
            {
                musicaAmbiental = true;
            }



            switch (tipoevento)
            {
                case (10):
                    calculosContrato.CalculoContratoCoffeBreak(cantAsistentes, CantPersonal, valorBaseModalidad, ref valorfinal);
                    break;
                case (20):
                    calculosContrato.CalculoContratoCocktail(cantAsistentes, CantPersonal, valorBaseModalidad, ref valorfinal, musicaAmbiental, uf_ambientacion);
                    break;
                case (30):
                    calculosContrato.CalculoContratoCena(cantAsistentes, CantPersonal, valorBaseModalidad, ref valorfinal, musicaAmbiental);
                    break;


            }
        }

        public void DatosOpcionalesEventos(int tipoevento)
        {
            switch (tipoevento)
            {
                case (10):
                    CkBoxVegetariano.Visibility = Visibility.Visible;
                    chkboxMusicaAmbiental.Visibility = Visibility.Hidden;

                    break;
                case (20):
                    CkBoxVegetariano.Visibility = Visibility.Hidden;
                    CkBoxVegetariano.IsChecked = false;

                    chkboxMusicaAmbiental.Visibility = Visibility.Visible;
                    break;
                case (30):
                    CkBoxVegetariano.Visibility = Visibility.Hidden;
                    CkBoxVegetariano.IsChecked = false;
                    chkboxMusicaAmbiental.IsChecked = false;
                    chkboxMusicaAmbiental.Visibility = Visibility.Visible;
                    break;


            }


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private void TxtAsistentes_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back))
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void BtnConsultarContrato_Click(object sender, RoutedEventArgs e)
        {
            TxtConsulta.Text = "";
            TxtConsulta.Foreground = Brushes.Black;
            flyoutConsulta.IsOpen = true;
            TxtConsulta.Width = 250;
            TxtConsulta.Background = Brushes.White;
            boton_ConsultarContrato.Content = "Buscar";
            txtFlyout.Text = "";
            boton_ConsultarContrato.Visibility = Visibility.Visible;
            Canvas.SetTop(boton_ConsultarContrato, 407);
            Canvas.SetLeft(boton_ConsultarContrato, 80);
            
            IconoFlyout.Kind = MaterialDesignThemes.Wpf.PackIconKind.Contract;
            Canvas.SetLeft(TxtConsulta, 42);
            Canvas.SetTop(TxtConsulta, 300);



        }

        private void boton_Consultar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("HOLA");
        }

        private void BtnConsultarPorRut_Click(object sender, RoutedEventArgs e)
        {


            Cliente cli = new Cliente()
            {
                RutCliente = (txtRutCliente.Text)
            };

            if (cli.Read())
            {
                txtFlyout.Visibility = Visibility.Visible;
                flyoutConsulta.Header = "Informacion Cliente";
                IconoFlyout.Kind = MaterialDesignThemes.Wpf.PackIconKind.InformationCircle;
                flyoutConsulta.IsOpen = true;
                txtFlyout.Text = "Nombre Cliente:";
                BtnConsultarContrato.Visibility = Visibility.Hidden;
                TxtConsulta.Text = cli.NombreContacto;
                TxtConsulta.IsEnabled = false;
                TxtConsulta.Foreground = Brushes.Black;
                Canvas.SetLeft(TxtConsulta, 42);
                Canvas.SetTop(TxtConsulta, 300);


            }
            else
            {
                MessageBox.Show("Cliente no Encontrado");


            }
        }

        private void ComboTipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboTipoEvento.SelectedValue != null)
            {
                tipoEvento = (int)ComboTipoEvento.SelectedValue;

                DatosOpcionalesEventos(tipoEvento);

                comboModalidad.ItemsSource = new ModalidadServicio().ReadByTipo(tipoEvento);
                comboModalidad.SelectedValuePath = "IdModalidad";
                comboModalidad.DisplayMemberPath = "Nombre";
                comboModalidad.SelectedIndex = 0;





            }
        }

        private void TxtRutCliente_MouseLeave(object sender, MouseEventArgs e)
        {

            txtRutCliente.Text = cliente.FormatearRut(txtRutCliente.Text);
        }

        private void ComboModalidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboModalidad.SelectedValue != null)
            {
                string CodModalidad = comboModalidad.SelectedValue.ToString();
                ModalidadServicio modalidad = new ModalidadServicio()
                {
                    IdModalidad = CodModalidad.ToString()
                };
                if (modalidad.Read())
                {
                    txtValorTotal.IsEnabled = false;
                    txtValorTotal.Text = "$0";
                    var valorBaseS = (float)modalidad.ValorBase;
                    valorBaseModalidad = valorBaseS;

                }

            }
        }

        private void BtnConsultarValorTotal_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(txtAsistentes.Text) || string.IsNullOrEmpty(txtPersonalAdicional.Text))
            {

            }

            else if (string.IsNullOrEmpty(txtAsistentes.Text) && string.IsNullOrEmpty(txtPersonalAdicional.Text))
            {
                MessageBox.Show("Debes Rellenar Todos los Campos Asistentes y Personal Adicional");
            }
            else if (txtAsistentes.Text.Equals("0") || txtPersonalAdicional.Text.Equals("0"))
            {
                MessageBox.Show("Debe Ingresar Valores superior a 0");
            }
            else if (txtPersonalAdicional.Text.Equals("0") && txtAsistentes.Text.Equals("0"))
            {
                MessageBox.Show("Debe Ingresar Valores superior a 0");
            }


            else
            {

                int CantPersonal = int.Parse(txtPersonalAdicional.Text);
                int cantAsistentes = int.Parse(txtAsistentes.Text);

                if (cantAsistentes <= 1 || CantPersonal < 2)
                {
                    MessageBox.Show("Valores Fuera del Rango");
                }
                else if (cantAsistentes <= 1 && CantPersonal > 2)
                {
                    MessageBox.Show("Valores Fuera del Rango");
                }
                else
                {

                    TipoCalculoContrato(tipoEvento);

                    txtValorTotal.Text = valorfinal.ToString("$#,##0.00");
                }


            }
        }

        public void OcultarDatosPrincipales(bool visibilidad)
        {
            if (visibilidad == true)
            {
                /* CHECKBOX OPCIONALES*/
                CkBoxVegetariano.Visibility = Visibility.Hidden;
                chkboxMusicaAmbiental.Visibility = Visibility.Hidden;

                /* Botones*/
                BtnConsultarPorRut.Visibility = Visibility.Hidden;
                BtnAgregarDatosExtra.Visibility = Visibility.Visible;
                BtnEliminar.Visibility = Visibility.Hidden;
                BtnConsultarContrato.Visibility = Visibility.Hidden;
                BtnAgregar.Visibility = Visibility.Hidden;
                BtnActualizar.Visibility = Visibility.Hidden;
                /* Combobox*/
                comboModalidad.Visibility = Visibility.Hidden;
                ComboTipoEvento.Visibility = Visibility.Hidden;
                /* CAMPOS DE TEXTO */
                txtRutCliente.Visibility = Visibility.Hidden;
                txtValorTotal.Visibility = Visibility.Hidden;
                txtObservaciones.Visibility = Visibility.Hidden;
                txtPersonalAdicional.Visibility = Visibility.Hidden;
                txtAsistentes.Visibility = Visibility.Hidden;
                comboAmbientacion.Visibility = Visibility.Visible;
                TxtCabeceraContrato.Text = "Datos Adicionales";
                txtsuperiorRut.Content = "Tipo Ambientacion";
                /* ICONOS que cambian */
                iconoTipoEvento.Kind = MaterialDesignThemes.Wpf.PackIconKind.EventNote;
                iconoRut.Kind = MaterialDesignThemes.Wpf.PackIconKind.LocalRestaurant;
                /* ICONOS OCULTOS */
                iconoAsistentes.Visibility = Visibility.Hidden;
                iconoModalidad.Visibility = Visibility.Hidden;
                iconoValortotal.Visibility = Visibility.Hidden;
                iconoPersonalAdi.Visibility = Visibility.Hidden;
                iconoObservacion.Visibility = Visibility.Hidden;
                /* texto superior */
                txtsuperiorAsistentes.Visibility = Visibility.Hidden;
                txtsuperiorObservaciones.Visibility = Visibility.Hidden;
                txtsuperiorValor.Visibility = Visibility.Hidden;
                txtsuperiorModalidad.Visibility = Visibility.Hidden;
                txtsuperiorPersonalAD.Visibility = Visibility.Hidden;
                BtnAtrasDatosAdicionales.Visibility = Visibility.Visible;
                txtsuperiorTipoEvento.Content = "Lugar Evento";
                comboAmbientacion.IsEnabled = true;
                comboAmbientacion.SelectedIndex = 0;




            }
            else if (visibilidad == false)
            {
                bool checkeadoMusicaAmbiental = (bool)chkboxMusicaAmbiental.IsChecked;

                DatosOpcionalesEventos(tipoEvento);


                comboAmbientacion.IsEnabled = true;
                comboAmbientacion.SelectedIndex = 0;
                txtsuperiorTipoEvento.Content = "Tipo Evento";
                chkboxLocal.Visibility = Visibility.Hidden;

                txtAsistentes.Visibility = Visibility.Visible;
                TxtConsulta.Visibility = Visibility.Visible;
                BtnConsultarPorRut.Visibility = Visibility.Visible;
                BtnAgregarDatosExtra.Visibility = Visibility.Hidden;
                BtnActualizar.Visibility = Visibility.Visible;
                TxtCabeceraContrato.Text = "Administracion Contrato";
                ComboTipoEvento.Visibility = Visibility.Visible;
                txtObservaciones.Visibility = Visibility.Visible;

                txtsuperiorAsistentes.Visibility = Visibility.Visible;
                txtsuperiorObservaciones.Visibility = Visibility.Visible;
                txtsuperiorValor.Visibility = Visibility.Visible;
                txtsuperiorModalidad.Visibility = Visibility.Visible;
                txtsuperiorPersonalAD.Visibility = Visibility.Visible;


                comboAmbientacion.Visibility = Visibility.Hidden;
                BtnEliminar.Visibility = Visibility.Visible;
                BtnConsultarContrato.Visibility = Visibility.Visible;
                BtnAgregar.Visibility = Visibility.Visible;
                txtRutCliente.Visibility = Visibility.Visible;

                txtValorTotal.Visibility = Visibility.Visible;
                comboModalidad.Visibility = Visibility.Visible;
                /* iconos */
                iconoTipoEvento.Kind = MaterialDesignThemes.Wpf.PackIconKind.EventMultiple;
                iconoRut.Kind = MaterialDesignThemes.Wpf.PackIconKind.IdCard;
                iconoAsistentes.Visibility = Visibility.Visible;
                iconoModalidad.Visibility = Visibility.Visible;
                iconoValortotal.Visibility = Visibility.Visible;
                iconoPersonalAdi.Visibility = Visibility.Visible;
                iconoObservacion.Visibility = Visibility.Visible;
                iconoAsistentes.Visibility = Visibility.Visible;
                iconoModalidad.Visibility = Visibility.Visible;
                iconoValortotal.Visibility = Visibility.Visible;
                iconoPersonalAdi.Visibility = Visibility.Visible;
                iconoObservacion.Visibility = Visibility.Visible;
                txtPersonalAdicional.Visibility = Visibility.Visible;
                BtnAtrasDatosAdicionales.Visibility = Visibility.Hidden;


                txtsuperiorRut.Content = "RUT";


            }

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRutCliente.Text.Length > 0 || txtObservaciones.Text.Length > 0 || txtAsistentes.Text.Length > 0 || txtPersonalAdicional.Text.Length > 0)
            {
                Cliente cli = new Cliente()
                {
                    RutCliente = (txtRutCliente.Text)
                };
                /* solicita la informacion del cliente la cual sera desplegada en los campo de texto y combobox*/
                if (cli.Read())
                {




                    if (tipoEvento == 10)
                    {
                        TipoCalculoContrato(tipoEvento);
                        string numeroContrato = DateTime.Now.ToString("yyyyMMddHHmm");
                        string fecha = System.DateTime.Now.ToString("dd-MM-yyyy");
                        string hora = DateTime.Now.ToString("hh:mm:ss");

                        Contrato Cont = new Contrato()
                        {
                            Numero = numeroContrato,
                            Creacion = Convert.ToDateTime(fecha),
                            Termino = Convert.ToDateTime(fecha),
                            RutCliente = txtRutCliente.Text,
                            IdModalidad = (string)comboModalidad.SelectedValue,
                            IdTipoEvento = (int)ComboTipoEvento.SelectedValue,
                            FechaHoraInicio = Convert.ToDateTime(hora),
                            FechaHoraTermino = Convert.ToDateTime(hora),
                            Asistentes = int.Parse(txtAsistentes.Text),
                            PersonalAdicional = int.Parse(txtPersonalAdicional.Text),
                            Realizado = false,
                            ValorTotalContrato = valorfinal,
                            Observaciones = txtObservaciones.Text
                        };
                        if (Cont.Create())
                        {
                            CoffeBreak coffe = new CoffeBreak()
                            {
                                Numero = numeroContrato,
                                Vegetariana = (bool)CkBoxVegetariano.IsChecked
                            };
                            if (coffe.Create())
                            {
                                MessageBox.Show("Contrato Creado Correctamente");
                            }
                        }



                    }
                    else if (tipoEvento == 20)
                    {

                        txtsuperiorTipoEvento.Visibility = Visibility.Visible;
                        chkboxLocal.Visibility = Visibility.Visible;
                        
                        OcultarDatosPrincipales(true);
                        txtsuperiorTipoEvento.Visibility = Visibility.Hidden;
                        chkboxLocal.Visibility = Visibility.Hidden;

                    }
                    else if (tipoEvento == 30)
                    {
                        
                        OcultarDatosPrincipales(true);
                        comboAmbientacion.SelectedIndex = 0;
                        txtsuperiorTipoEvento.Visibility = Visibility.Visible;
                        chkboxLocal.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("El Cliente que Intenta Agregar a el Contrato no Existe");
                }
            }







        }

        private void BtnAgregarDatosExtra_Click(object sender, RoutedEventArgs e)
        {
            TipoCalculoContrato(tipoEvento);
            MessageBox.Show("el valor del Contrato Que se agregara sera de: "+valorfinal.ToString("$#,##0.00"));
            OcultarDatosPrincipales(false);
            Thread.Sleep(5000);
            BtnAgregar.IsEnabled = false;
          
            {
                string numeroContrato = DateTime.Now.ToString("yyyyMMddHHmm");
                string fecha = System.DateTime.Now.ToString("dd-MM-yyyy");
                string hora = DateTime.Now.ToString("hh:mm:ss");

                Contrato Cont = new Contrato()
                {
                    Numero = numeroContrato,
                    Creacion = Convert.ToDateTime(fecha),
                    Termino = Convert.ToDateTime(fecha),
                    RutCliente = txtRutCliente.Text,
                    IdModalidad = (string)comboModalidad.SelectedValue,
                    IdTipoEvento = (int)ComboTipoEvento.SelectedValue,
                    FechaHoraInicio = Convert.ToDateTime(hora),
                    FechaHoraTermino = Convert.ToDateTime(hora),
                    Asistentes = int.Parse(txtAsistentes.Text),
                    PersonalAdicional = int.Parse(txtPersonalAdicional.Text),
                    Realizado = false,
                    ValorTotalContrato = valorfinal,
                    Observaciones = txtObservaciones.Text
                };
                if (Cont.Create())
                {
                    Cocktail cokta = new Cocktail()
                    {
                        Numero = numeroContrato,
                        IdTipoAmbientacion = (int)comboAmbientacion.SelectedValue,
                        Ambientacion = true,
                        MusicaAmbiental = (bool)chkboxMusicaAmbiental.IsChecked,
                        MusicaCliente = false
                    };
                    if (cokta.Create())
                    {
                        MessageBox.Show("Contrato Creado Correctamente");
                    }
                }
                BtnAgregar.IsEnabled = true;
            };

        }

        private void BtnAtrasDatosAdicionales_Click(object sender, RoutedEventArgs e)
        {
            OcultarDatosPrincipales(false);
        }

        private void ChkboxAmbientacionEvento_Click(object sender, RoutedEventArgs e)
        {


        }

        private void ComboAmbientacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboAmbientacion.SelectedValue != null)
            {
                tipoAmbientacion = (int)ComboTipoEvento.SelectedValue;
                if (tipoAmbientacion == 10)/* ambientacion basica*/
                {
                     uf_ambientacion = 2;
                }else if (tipoAmbientacion == 20)/* ambientacion personalizada*/
                {
                     uf_ambientacion = 2;
                }
            }
        }

        private void Boton_ConsultarContrato_Click(object sender, RoutedEventArgs e)
        {
            if (TxtConsulta.Text.Length > 0)
            {
                Contrato cont = new Contrato()
                {
                    Numero = TxtConsulta.Text,
                };
                if (cont.Read())
                {
                    txtRutCliente.Text = cont.RutCliente.ToString();
                    txtAsistentes.Text = cont.Asistentes.ToString();
                    txtPersonalAdicional.Text = cont.PersonalAdicional.ToString();
                    ComboTipoEvento.SelectedValue = cont.IdTipoEvento;
                    comboModalidad.SelectedValue = cont.IdModalidad;
                    txtValorTotal.Text = cont.ValorTotalContrato.ToString();
                    txtObservaciones.Text = cont.Observaciones.ToString();
                }
                else
                {
                    MessageBox.Show(" Contrato No Encontrado");
                }
            }
        }
    } 
}
