using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        DispatcherTimer DdispatcherTimer = new DispatcherTimer();
        double valorBaseModalidad = 0;
        int tipoEvento = 0;
        int tipoAmbientacion = 0;
        int uf_ambientacion = 0;
        double valorfinal = 0;
        bool Contratorealizado = false;
        /*variables para guardar Contrato Consultado*/
        string Numero_C = string.Empty;
        DateTime  Creacion_C = DateTime.MinValue;
        DateTime  Termino_C = DateTime.MinValue;
        string  RutCliente_C = string.Empty;
        string   IdModalidad_C = string.Empty;
        int  IdTipoEvento_C = 0;
        DateTime FechaHoraInicio_C = DateTime.MinValue;
        DateTime FechaHoraTermino_C = DateTime.MinValue; 
        int  Asistentes_C = 0;
        int   PersonalAdicional_C = 0;
        bool  Realizado_C = true;
        double   ValorTotalContrato_C = 0;
        string   Observaciones_C = string.Empty;


        public UserControlAdmContratos()
        {
            InitializeComponent();
            CargarTipoEvento();
            CargarTipoAmbientacion();
            CrearCarpetaRestauracion();

            DdispatcherTimer.Interval = new TimeSpan(0, 0, 300);
            DdispatcherTimer.Tick += (a, b) =>
            {
                SaveTotxtContrato();
                MessageBox.Show("Se Ha Guardado un Respaldo del Contrato en la ruta C:/OnBrakeRecovery");
            };

        }




        public void CrearCarpetaRestauracion()
        {
            string ruta = @"C:\OnBrakeRecovery\";

            if (!Directory.Exists(ruta))
            {
               MessageBox.Show("Se Creo una Carpeta que guardara los Contratos cada 5 Minutos si se Produce un Fallo en el Sistema");
                DirectoryInfo di = Directory.CreateDirectory(ruta);
            }
            else
            {
                MessageBox.Show("El Directorio de Restauracion esta Creado y Puede contener Respaldos de Contratos");
            }

        }


        public void SaveTotxtContrato()
        {

            string fecha = System.DateTime.Now.ToString("dd-MM-yyyy-");
            string hora = System.DateTime.Now.ToString("hh-mm-ss");
            string rutanombre = fecha + hora + "-CONTRATO.txt";
            var dir = @"C:\OnBrakeRecovery\";
            string ruta = System.IO.Path.Combine(dir, rutanombre);

            using (TextWriter tw = new StreamWriter(ruta))
            {
                
                
                   tw.WriteLine(string.Format("{0} {1} {02} {03} {04} {05}", txtRutCliente, ComboTipoEvento.SelectedValue.ToString(),comboModalidad.SelectedValue.ToString(),txtAsistentes,txtPersonalAdicional,txtObservaciones));
                
            }

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
                boton_ConsultarContrato.Visibility = Visibility.Hidden ;
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
                    MessageBox.Show("Contrato Encontrado, se deplegara la informacion");
                    flyoutConsulta.IsOpen = false;
                    Numero_C =cont.Numero;
                    Creacion_C = cont.Creacion;
                    Termino_C = cont.Termino;
                    RutCliente_C = cont.RutCliente;
                    IdModalidad_C = cont.IdModalidad;
                    IdTipoEvento_C = cont.IdTipoEvento;
                    FechaHoraInicio_C = cont.FechaHoraInicio;
                    FechaHoraTermino_C = cont.FechaHoraTermino;
                    Asistentes_C = cont.Asistentes;
                    PersonalAdicional_C = cont.PersonalAdicional;
                    Realizado_C = cont.Realizado;
                    ValorTotalContrato_C = cont.ValorTotalContrato;
                    Observaciones_C = cont.Observaciones;


                    /*valores a mostrar*/
                    txtRutCliente.Text = cont.RutCliente.ToString();
                    txtAsistentes.Text = cont.Asistentes.ToString();
                    txtPersonalAdicional.Text = cont.PersonalAdicional.ToString();
                    ComboTipoEvento.SelectedValue = cont.IdTipoEvento;
                    comboModalidad.SelectedValue = cont.IdModalidad;
                    txtValorTotal.Text = cont.ValorTotalContrato.ToString();
                    txtObservaciones.Text = cont.Observaciones.ToString();
                    Contratorealizado = cont.Realizado;

                }
                else
                {
                    MessageBox.Show(" Contrato No Encontrado");
                }
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (!Realizado_C == true)
            {
                if (!Numero_C.Equals("") )
                {
                    if (PersonalAdicional_C == int.Parse(txtPersonalAdicional.Text) || Asistentes_C == int.Parse(txtAsistentes.Text))
                    {


                        Contrato cont = new Contrato()
                        {
                            Numero = Numero_C,
                            RutCliente = txtRutCliente.Text,
                            Asistentes = int.Parse(txtAsistentes.Text),
                            PersonalAdicional = int.Parse(txtPersonalAdicional.Text),
                            IdTipoEvento = (int)ComboTipoEvento.SelectedValue,
                            IdModalidad = (string)comboModalidad.SelectedValue,
                            ValorTotalContrato = double.Parse(txtValorTotal.Text),
                            Observaciones = txtObservaciones.Text,
                            Realizado = Contratorealizado
                        };
                        if (cont.Update())
                        {
                            MessageBox.Show("Contrato Actualizado");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("el Contrato esta Realizado o no se ha consultado con el numero del Contrato ");
                }
            }
            else
            {
                MessageBox.Show("El Contrato se Encuentra Finalizado");
            }
    }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (Numero_C.Length > 11)
            {
                try
                {


                    Contrato cont = new Contrato
                    {
                        Numero = Numero_C,
                        FechaHoraInicio = FechaHoraInicio_C,
                        Creacion = Creacion_C,
                        FechaHoraTermino = FechaHoraTermino_C,
                        Termino=Termino_C,
                        RutCliente = RutCliente_C,
                        Asistentes = Asistentes_C,
                        PersonalAdicional = PersonalAdicional_C,
                        IdTipoEvento = IdTipoEvento_C,
                        IdModalidad = IdModalidad_C,
                        ValorTotalContrato = ValorTotalContrato_C,
                        Observaciones = Observaciones_C,
                        Realizado = true,

                    };
                    if (cont.Update())
                    {
                        MessageBox.Show("Contrato: " + Numero_C + " fue terminado correctamente ");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("error" + ex);
                }
              }
            else
            {
                MessageBox.Show("Primero debe Consultar un Contrato para su posterior Termino");
            }
        }
    } 
}
