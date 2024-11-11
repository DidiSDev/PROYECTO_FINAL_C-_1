using System;
using System.Text.RegularExpressions;
using System.Windows;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    public partial class RegistrarWindow : Window
    {
        private IFirebaseClient clienteBBDD;

        public RegistrarWindow()
        {
            InitializeComponent();

            //ESTABLEZCO CONEXION COMO CLIENT
            clienteBBDD = Firebase.GetInstance().GetClient();
        }

        private async void BotonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = CajaUsuario.Text.Trim();
            string contraseña = CajaContraseña.Password;
            string repetirContraseña = CajaRepetirContraseña.Password;
            string correoElectronico = CajaCorreoElectronico.Text.Trim();
            string repetirCorreoElectronico = CajaRepetirCorreoElectronico.Text.Trim();

            //VALIDACIONES
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña) ||
                string.IsNullOrEmpty(correoElectronico) || string.IsNullOrEmpty(repetirCorreoElectronico))
            {
                MessageBox.Show("¡Error! Por favor, rellena todos los campos antes de pulsar el botón");
                return;
            }

            // LA PASS TENDRÁ VERIFICACIONES ADICIONALES (MÍNIMO 8 CARÁCTERES, 1 MAYUS, 1 MINUS, 1 NUM Y 1 ESPECIAL
            if (!ValidarContraseña(contraseña))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 carácteres, incluyendo una mayúscula, una minúscula, un número y un carácter especial.",
                                "¡¡Error!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //LAS 2 CAJAS DEBVEN TENER LA MISMA APSS
            if (contraseña != repetirContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden");
                return;
            }

            //EL MAIL DEBE TENER OBLIGATORIAMENTE ESTE FORMATO: ____@___.___
            if (!ValidarCorreoElectronico(correoElectronico))
            {
                MessageBox.Show("El correo electrónico no tiene un formato válido.", "¡¡Error!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // TAMBIEN DEBEN COINCIDIR
            if (correoElectronico != repetirCorreoElectronico)
            {
                MessageBox.Show("Los correos electrónicos no coinciden.", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // AQUÍ VERIFICAMOS SI HAY ADMIN LLAMANDO AL MÉTODO
            //ESTA VENTANA NO DEBE ABNRRIRSE NNUNCA SI HAY ADMINISTRADOR, PERO NO ESTÁ DE MÁS VALIDARLO POR SI ALGUIEN SE QUIERE COLAR
            if (await existeAdmin())
            {
                MessageBox.Show("Ya existe un administrador registrado.", "¡Error! ¡NO DEBES ESTAR AQUÍ! :(", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //GUARDAMOS EN UN STRING LA CLAVE SEGURA DE 24 DÍGITOS GENERADA ALEATORIAMENTE ENCRIPTADA
            string claveUnica = generarClaveUnica.generarClave();

            // AQUÍ ES DONDE SE LA MUESTRO AL USUARIO PARA QUE LA GUARDE, GUARDO ENN RESPUESTA SI PULSA OK, SINO NO REGISTRO
            MessageBoxResult RESPUESTA = MessageBox.Show($"Esta es tu clave única: {claveUnica}\n\n¡Por favor, guárdala de forma segura y no la pierdas!. Una vez pulses 'Aceptar', esta clave no volverá a mostrarse nunca y no tendrás forma de recuperarla.",
                                                      "Clave única generada", MessageBoxButton.OK, MessageBoxImage.Information);

            // SI EL USUARIO PULSA ACEPTAR DEL MENSAJE ANTERIOR
            if (RESPUESTA == MessageBoxResult.OK)
            {
                Administrador nuevoAdmin = new Administrador
                {
                    nombreUsuario = nombreUsuario,
                    correoElectronico= correoElectronico,
                    pass= contraseña, //HE VISTO EN INTERNET QUE LAS CONTRASEÑAS MÁS SEGURAS LLEVAN UN HASH, IMPORTANTE INVESTIGAR
                                             ////E IMPLEMENTAR SI HAY TIEMPO
                    claveUnica= claveUnica
                };

                //ALMACENO EL ADMIN EN FIREBASE POR PRIMERA VEZ
                SetResponse response = await clienteBBDD.SetAsync("Administrador", nuevoAdmin);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("¡Enhorabuena, administrador registrado!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    //OBVIAMENTE SI HEMOS REGISTRADO, NOS VAMOS AL LOGIN, PARA QUE INTRODUZCA SUS DATOS POR PRIMERA VEZ Y DECIDA SI GUARADRLOS O NO
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error desconocido al registrar el administrador.", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task<bool> existeAdmin()
        {
            FirebaseResponse response = await clienteBBDD.GetAsync("administrador");
            return response.Body != "null"; 
            //DEVUELVE TRUE SI YA HAY ADMIN
        }

        private bool ValidarContraseña(string contraseña)
        {
            //1 MAYUS, 1 MINUS, 1 NUM, 1 CARACTER ESPECIAL
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            Regex regex = new Regex(patron);
            return regex.IsMatch(contraseña);
        }

        private bool ValidarCorreoElectronico(string correo)
        {
            //___@___.___
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(patron);
            return regex.IsMatch(correo);
        }
    }
}
