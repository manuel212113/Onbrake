using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using OnBrake.Datos;


namespace OnBrake
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            

        }


      

        



        private async void LoginBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordButton.IsChecked == true)
            {
                sender.ToString();
                MessageBox.Show("Nose se puede ingresar al sistema con la Clave descubierta");
                e.ToString();
                ShowPasswordButton_Click(sender,e);

            }
            else
            {



                if (textUser.Text.Equals(string.Empty) && textPassword.Password.ToString().Equals(string.Empty))
                {
                    await this.ShowMessageAsync("Error:",
                      string.Format("Debes Llenar todos los campos "));
                }
                else if (textUser.Text.Equals(string.Empty) || textPassword.Password.ToString().Equals(string.Empty))
                {
                    await this.ShowMessageAsync("Error:",
                      string.Format("Debes Llenar todos los campos ", MessageBoxImage.Error));
                }
                else
                {
                    try
                    {


                        var db = new OnBreakEntities();

                        var usuario = db.USUARIO.FirstOrDefault(u => u.nombre_usuario == textUser.Text && u.contraseña == textPassword.Password.ToString());

                        if (usuario != null)
                        {
                            await this.ShowMessageAsync("Informacion:",
                         string.Format("Logeado Correctamente ", MessageBoxImage.Information));
                            Dashboard subWindow = new Dashboard();
                            subWindow.Show();


                            this.Close();
                            await subWindow.ShowMessageAsync("Informacion:",
                            string.Format("Bienvenido:" + textUser.Text));

                        }
                        else
                        {
                            await this.ShowMessageAsync("Informacion:",
                         string.Format("Constraseña o Usuario Incorrectos ", MessageBoxImage.Error));
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR:" + ex);
                    }
                }
            } 
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordButton.IsChecked == true)
            {
                textPass.Text = textPassword.Password;
                textPass.Visibility =Visibility.Visible;
                textPassword.Visibility =Visibility.Hidden;
            }
            else
            {
                textPassword.Password =textPass.Text;
                textPass.Visibility = Visibility.Hidden;
                textPassword.Visibility = Visibility.Visible;

            }
        }
    }
}
