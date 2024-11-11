using System;
using System.Text.RegularExpressions;
using System.Windows;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    public partial class ventanaRecuperarPass : Window
    {
        private IFirebaseClient clienteBBDD;

        public ventanaRecuperarPass()
        {
            InitializeComponent();
            //FB
            clienteBBDD = Firebase.GetInstance().GetClient();

            if (radioClaveUnica != null)
            {
                radioClaveUnica.Checked += RadioButton_Checked;
            }

            if (radioCorreoElectronico != null)
            {
                radioCorreoElectronico.Checked += RadioButton_Checked;
            }
        }

        //VALIDACIONES
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (seccionClaveUnica == null || seccionCorreoElectronico == null)
            {
                return;
            }

            //CAMBIO DE CAJA EN FUNCIÓN DEL RADIOBUTTON
            if (radioClaveUnica.IsChecked == true)
            {
                seccionClaveUnica.Visibility = Visibility.Visible;
                seccionCorreoElectronico.Visibility = Visibility.Collapsed;
            }
            else if (radioCorreoElectronico.IsChecked == true)
            {
                seccionClaveUnica.Visibility = Visibility.Collapsed;
                seccionCorreoElectronico.Visibility = Visibility.Visible;
            }
        }

        private async void botonRecuperarClaveUnica_Click(object sender, RoutedEventArgs e)
        {
            string claveUnicaIngresada = cajaClaveUnica.Text.Trim();

            if (string.IsNullOrWhiteSpace(claveUnicaIngresada))
            {
                MessageBox.Show("¡Debes introducir la clave secreta otorgada la primera vez que te registraste!");
                return;
            }

            try
            {
                FirebaseResponse response = await clienteBBDD.GetAsync("Administrador");
                if (response.Body == "null")
                {
                    MessageBox.Show("No hay ningún administrador");
                    return;
                }

                var admin = JsonConvert.DeserializeObject<Administrador>(response.Body);

                if (admin.claveUnica == claveUnicaIngresada)
                {
                    MessageBox.Show($"Su contraseña es: {admin.pass}", "Contraseña Recuperada", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                {
                    MessageBox.Show("La clave secreta no es correcta :(");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con Firebase: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //LIMPIAMOS SIEMPRE CAJA TRAS PULSAR EL BOTON
            cajaClaveUnica.Text = "";
        }

        private async void botonEnviarCorreo_Click(object sender, RoutedEventArgs e)
        {
            //NO FUNCIONA, POR NINGUNA RAZÓN, DECIDE QUE NO FUNCIONA ASÍ QUE NO VA A FUNCIONAR HASTA QUE QUIERA FUNCIONAR, QUIZÁ MAÑANA SE LEVANTE CON MEJOR PIE
            //Y FUNCIONE, HOY NO FUNCIONA, MAÑANA QUIZÁ
            string correoIngresado = cajaCorreoElectronico.Text.Trim();

            if (string.IsNullOrWhiteSpace(correoIngresado))
            {
                MessageBox.Show("Por favor, introduzca su correo electrónico.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!EsCorreoValido(correoIngresado))
            {
                MessageBox.Show("Por favor, introduzca un correo electrónico válido.", "Correo Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //METEMOS EN TRY-CATCH PORQUE SINO CRASHEA
            try
            {
                FirebaseResponse response = await clienteBBDD.GetAsync("Administrador");
                if (response.Body == "null")
                {
                    MessageBox.Show("No se encontró ningún administrador registrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var admin = JsonConvert.DeserializeObject<Administrador>(response.Body);

                if (admin.correoElectronico.Equals(correoIngresado, StringComparison.OrdinalIgnoreCase))
                {
                    string token = GenerateToken();
                    string enlaceRestablecimiento = $"AQUI VA EL ENLACE PERO NO FUNCIONA NI EL DE HOTMAIL NI EL DE GMAIL";

                    string asuntoCorreo = "Recuperación de Contraseña Ayudaris";
                    string cuerpoCorreo = $"Hola {admin.nombreUsuario},\n\n" +
                                          "Has solicitado restablecer tu contraseña. Por favor, haz clic en el siguiente enlace para restablecerla:\n" +
                                          $"{enlaceRestablecimiento}\n\n" +
                                          "Si no has solicitado este cambio, por favor ignora este correo.\n\n" +
                                          "Saludos,\n" +
                                          "Tu soporte favorito: Diego Díaz Senovilla";

                    await EnviarCorreoRecuperacionAsync(admin.correoElectronico, admin.pass, asuntoCorreo, cuerpoCorreo);
                }
                else
                {
                    MessageBox.Show("El correo electrónico ingresado no coincide con ningún administrador registrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con Firebase: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> EnviarCorreoRecuperacionAsync(string correoRemitente, string contraseñaRemitente, string asunto, string cuerpo)
        {
            //ESTRUCTURA
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Ayudaris", correoRemitente));
            mensaje.To.Add(new MailboxAddress("Administrador", correoRemitente));
            mensaje.Subject = asunto;
            mensaje.Body = new TextPart("plain")
            {
                Text = cuerpo
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(correoRemitente, contraseñaRemitente);
                    client.Send(mensaje);
                    client.Disconnect(true);

                    MessageBox.Show("Correo de recuperación enviado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        private void botonVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            try
            {
                string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(correo, patron, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }

}
