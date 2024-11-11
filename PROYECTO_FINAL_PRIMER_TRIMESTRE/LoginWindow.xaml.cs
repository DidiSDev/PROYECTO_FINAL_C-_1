using System;
using System.Windows;
using FireSharp.Response;

namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    public partial class LoginWindow : Window
    {
        private Firebase conexion;

        public LoginWindow()
        {
            InitializeComponent();
            conexion = Firebase.GetInstance(); // CONECTO A FB
        }

        private async void botonConectar_Click(object sender, RoutedEventArgs e)
        {
            string usuarioEscrito = cajaUsuario.Text;
            string passEscrita = cajaPass.Password;

            try
            {
                //BUSCAMOS EN LA TABLA Administrador ALGUNA COINCIDENCIA
                FirebaseResponse response = await conexion.GetClient().GetAsync("Administrador");
                if (response.Body == "null")
                {
                    MessageBox.Show("¡Error! Los datos del administrador no son correctos.");
                    return;
                }
                var admin = response.ResultAs<Administrador>();

                //VEMOS SI COINCIDEN LOS DATOS DE LAS CAJAS CON LOS DE FIREBASE
                if (admin.nombreUsuario == usuarioEscrito && admin.pass == passEscrita)
                {
                    MessageBox.Show("¡Conectado!");
                    //SI ACIERTA ABRIMOS YA POR FIN LA VENTANA DE LA GESTIÓN
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con Firebase: {ex.Message}");
            }
        }

        private void botonCambiarContraseña_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            ventanaCambiarPass ventanaCambiarPass = new ventanaCambiarPass();
            ventanaCambiarPass.Closed += (s, args) => this.Show(); //ESTO ES COMO UNA SUSCRIPCION A CERRARSE LA NUEVA VENTANA, SE VUELVE A MOSTRAR LoginWindow
            ventanaCambiarPass.Show();
        }

        private void botonRecuperarPass_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            ventanaRecuperarPass ventanaRecuperarPass = new ventanaRecuperarPass();
            ventanaRecuperarPass.Closed += (s, args) => this.Show();
            ventanaRecuperarPass.Show();
        }
    }
}
