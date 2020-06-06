using MySql.Data.MySqlClient;
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
using OnBrake.Negocio;

namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para UserControlAdmContratos.xaml
    /// </summary>
    public partial class UserControlAdmContratos : UserControl
    {
        UserControlAdmiCliente cliente = new UserControlAdmiCliente();
        double valorBaseModalidad = 0;
        public UserControlAdmContratos()
        {
            InitializeComponent();
            CargarTipoEvento();
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


        public void CalcularValorAdicionalContrato(int cantAsis, int cantPersA)
        {
            UF_VALOR valoruf = new UF_VALOR();
            double ufvalordia = 0;
            valoruf.ObtenerResultadoUf(ref ufvalordia);


            double valor_Asistente = 0;
            double valor_personal = 0;

            int CantAsistentes = cantAsis;
            int cantPersonal = cantPersA;

            if (CantAsistentes <= 20)
            {
                valor_Asistente = 3 * ufvalordia;
            }
            else if (CantAsistentes > 20 && CantAsistentes < 51)
            {
                valor_Asistente = 5 * ufvalordia;

            }
            else if (CantAsistentes > 50)
            {
                CantAsistentes = CantAsistentes - 50;
                double porcentajeAdicional_asis = CantAsistentes * 0.1;
                porcentajeAdicional_asis = porcentajeAdicional_asis + 5;
                valor_Asistente = porcentajeAdicional_asis * ufvalordia;


            }

            if (cantPersonal == 2)
            {
                valor_personal = 2 * ufvalordia;

            }
            else if (cantPersonal == 3)
            {
                valor_personal = 3 * ufvalordia;
            }

            else if (cantPersonal == 4)
            {
                valor_personal = 3.5 * ufvalordia;
            }
            else if (cantPersonal > 4)
            {
                cantPersonal = cantPersonal - 4;
                double porcentajeAdicional = cantPersonal * 0.5;
                porcentajeAdicional = porcentajeAdicional + 3.5;
                valor_personal = porcentajeAdicional * ufvalordia;

            }

            double valor_final = (valor_Asistente) + (valor_personal);
            MessageBox.Show("valor calculado es: " + valor_final.ToString("#,##0.00"));
        }








        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CalcularTarifa_Click(object sender, RoutedEventArgs e)
        {
            UF_VALOR valoruf = new UF_VALOR();
            double ufvalordia = 0;
            valoruf.ObtenerResultadoUf(ref ufvalordia);

            if (!txtAsistentes.Equals("") || !txtPersonalAdicional.Equals(""))
            {
                double valor_Asistente = 0;
                double valor_personal = 0;

                int CantAsistentes = int.Parse(txtAsistentes.Text);
                int cantPersonal = int.Parse(txtPersonalAdicional.Text);

                if (CantAsistentes <= 20)
                {
                    valor_Asistente = 3 * ufvalordia;
                }
                else if (CantAsistentes > 20 && CantAsistentes < 51)
                {
                    valor_Asistente = 5 * ufvalordia;

                }
                else if (CantAsistentes > 50)
                {
                    CantAsistentes = CantAsistentes - 50;
                    double porcentajeAdicional_asis = 2;
                    porcentajeAdicional_asis = porcentajeAdicional_asis + 5;
                    valor_Asistente = porcentajeAdicional_asis * ufvalordia;


                }

                if (cantPersonal == 2)
                {
                    valor_personal = 2 * ufvalordia;

                }
                else if (cantPersonal == 3)
                {
                    valor_personal = 3 * ufvalordia;
                }

                else if (cantPersonal == 4)
                {
                    valor_personal = 3.5 * ufvalordia;
                }
                else if (cantPersonal > 4)
                {
                    cantPersonal = cantPersonal - 4;
                    double porcentajeAdicional = cantPersonal * 0;
                    porcentajeAdicional = porcentajeAdicional + 3.5;
                    valor_personal = porcentajeAdicional * ufvalordia;

                }

                double valor_final = (valor_Asistente) + (valor_personal);
                MessageBox.Show("valor calculado es: " + valor_final.ToString("#,##0.00"));
            }
            else if (txtAsistentes.Equals("0") || txtPersonalAdicional.Equals("0"))
            {
                MessageBox.Show("Debe Ingresar un valor mayor a cero");

            }
            else if (txtAsistentes.Equals("0") && txtPersonalAdicional.Equals("0"))
            {
                MessageBox.Show("Debe Ingresar un valor mayor a cero");
            }
            else if (txtAsistentes.Equals("") || txtPersonalAdicional.Equals(""))
            {
                MessageBox.Show("Los Campos asistentes y Personal Adicional no pueden estar Vacios");
            }



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
            flyoutConsulta.IsOpen = true;
            TxtConsulta.Width = 250;
            TxtConsulta.Background = Brushes.White;
            boton_Consultar.Content = "Buscar";
            txtFlyout.Text = "";
            boton_Consultar.Visibility = Visibility.Visible;
            Canvas.SetTop(boton_Consultar, 407);
            Canvas.SetLeft(boton_Consultar, 80);

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
            string SelectedText = txtRutCliente.Text;

            Cliente cli = new Cliente()
            {
                RutCliente = SelectedText
            };

            if (cli.Read())
            {
                txtFlyout.Visibility = Visibility.Visible;
                flyoutConsulta.Header = "Informacion Cliente";
                IconoFlyout.Kind = MaterialDesignThemes.Wpf.PackIconKind.InformationCircle;
                flyoutConsulta.IsOpen = true;
                txtFlyout.Text = "Nombre Cliente:";
                boton_Consultar.Visibility = Visibility.Hidden;
                TxtConsulta.Text = cli.NombreContacto;
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
                int tipoEvento = (int)ComboTipoEvento.SelectedValue;


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
                    MessageBox.Show("ValorBaseModalidad" + valorBaseModalidad);

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

                if (cantAsistentes <=0 || CantPersonal <2)
                {
                    MessageBox.Show("Valores Fuera del Rango");
                }
                else
                {


                    CalculosContrato calculosContrato = new CalculosContrato();

                    double valorfinal = 0;
                    calculosContrato.CalcularValorAdicionalContrato(cantAsistentes, CantPersonal, valorBaseModalidad, ref valorfinal);
                    txtValorTotal.Text = valorfinal.ToString("$#,##0.00");
                }

                
            }
        }
    }
}
