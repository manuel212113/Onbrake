using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using MahApps.Metro.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : MetroWindow
    {
        public Dashboard()
        {
            InitializeComponent();
            
        }


        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            Console.WriteLine(index);
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlInicio()); 
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlAdmiCliente());
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlAdmContratos());
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlListarClientes());
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlListarContrato());
                    break;
                default:
                    break;
            }



        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (150 + (80 * index)), 0, 0);
        }

        internal static Task ShowMessageAsync(UserControlAdmiCliente userControlAdmiCliente, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNotificacion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ColorButton.IsChecked == true)
            {
                var bc = new BrushConverter();
                GridVentana.Background = Brushes.Black;
                iconoAyuda.Foreground = Brushes.White;
                GridAbajoBarraInformacion.Background = (Brush)bc.ConvertFrom("#212941");
                BarraInformacion.Background = (Brush)bc.ConvertFrom("#212941");
                GridCentral.Background = (Brush)bc.ConvertFrom("#212941");
                GridColorPrincipal.Background = (Brush)bc.ConvertFrom("#292728");
                string color = "Cyan";
                string temaOscuro = "BaseDark";
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(color), ThemeManager.GetAppTheme(temaOscuro));
            }
            else if (ColorButton.IsChecked == false)
            {
                var bc = new BrushConverter();
                GridVentana.Background=(Brush)bc.ConvertFrom("#FFEEEEEE");
                GridAbajoBarraInformacion.Background =Brushes.White;
                BarraInformacion.Background = Brushes.White;
                GridCentral.Background = Brushes.White;
                iconoAyuda.Foreground =Brushes.Black;
                GridColorPrincipal.Background = (Brush)bc.ConvertFrom("#FF1290CB");
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Blue"), ThemeManager.GetAppTheme("BaseLight"));


            }
        }

        private void ColorButton_Checked(object sender, RoutedEventArgs e)
        {
           
        }

      
    }
}

