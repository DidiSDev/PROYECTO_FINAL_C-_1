using System;
using System.Windows;
using System.Threading.Tasks;
using FireSharp.Response;
using Newtonsoft.Json;
using FireSharp.Interfaces;
using System.Text.RegularExpressions;

namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    public partial class ventanaCambiarPass : Window
    {
        private IFirebaseClient clienteBBDD;

        public ventanaCambiarPass()
        {
            InitializeComponent();
        }

        private async void botonCambiarPass_Click(object sender, RoutedEventArgs e)
        {
            // Obtener las contraseñas ingresadas por el usuario
            string passwordActual = cajaPassActual.Password;
            string passwordNueva = cajaPassNueva.Password;
            string passwordRepetir = cajaRepetirPassNueva.Password;

            // Validar que todas las cajas no estén vacías
            if (string.IsNullOrWhiteSpace(passwordActual) ||
                string.IsNullOrWhiteSpace(passwordNueva) ||
                string.IsNullOrWhiteSpace(passwordRepetir))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que las nuevas contraseñas coincidan
            if (passwordNueva != passwordRepetir)
            {
                MessageBox.Show("Las nuevas contraseñas no coinciden", "Contraseñas No Coinciden", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que la nueva contraseña no sea igual a la actual
            if (passwordNueva == passwordActual)
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la actual.", "Contraseña Inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que la nueva contraseña cumpla con los criterios
            if (!EsPasswordValida(passwordNueva, out string mensajeError))
            {
                MessageBox.Show(mensajeError, "Contraseña Inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Obtener la instancia de Firebase
                clienteBBDD = Firebase.GetInstance().GetClient();

                // Obtener los datos actuales del usuario administrador desde Firebase
                FirebaseResponse response = await clienteBBDD.GetAsync("Administrador");
                if (response.Body == "null")
                {
                    MessageBox.Show("No se encontraron datos del administrador", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var datosAdmin = JsonConvert.DeserializeObject<Administrador>(response.Body);

                // Verificar que la contraseña actual ingresada coincida con la almacenada en Firebase
                // Si estás usando hashing, usa BCrypt para verificar
                // bool isPasswordCorrect = BCrypt.Verify(passwordActual, datosAdmin.pass);
                // if (!isPasswordCorrect)
                if (datosAdmin.pass != passwordActual) // Cambia esto si usas hashing
                {
                    MessageBox.Show("La contraseña actual es incorrecta", "Contraseña Incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Actualizar la contraseña en Firebase
                // Si usas hashing, hashea la nueva contraseña
                // datosAdmin.pass = BCrypt.HashPassword(passwordNueva);
                datosAdmin.pass = passwordNueva; // Cambia esto si usas hashing

                SetResponse setResponse = await clienteBBDD.SetAsync("Administrador", datosAdmin);

                // Confirmar éxito al usuario
                MessageBox.Show("Contraseña cambiada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Opcional: Limpiar los campos de contraseña
                cajaPassActual.Password = string.Empty;
                cajaPassNueva.Password = string.Empty;
                cajaRepetirPassNueva.Password = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar la contraseña: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void botonVolver_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana actual o navegar a la ventana anterior
            this.Close();
        }

        /// <summary>
        /// Valida que la contraseña cumpla con los criterios establecidos:
        /// - Mínimo 8 caracteres
        /// - Al menos 1 letra mayúscula
        /// - Al menos 1 letra minúscula
        /// - Al menos 1 número
        /// - Al menos 1 símbolo especial
        /// </summary>
        /// <param name="password">La contraseña a validar</param>
        /// <param name="mensajeError">Mensaje de error si la validación falla</param>
        /// <returns>True si la contraseña es válida, de lo contrario, False</returns>
        private bool EsPasswordValida(string password, out string mensajeError)
        {
            // Definir la expresión regular para validar la contraseña
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$";

            if (!Regex.IsMatch(password, patron))
            {
                mensajeError = "La nueva contraseña debe tener:\n" +
                               "- Mínimo 8 caracteres\n" +
                               "- Al menos 1 letra mayúscula\n" +
                               "- Al menos 1 letra minúscula\n" +
                               "- Al menos 1 número\n" +
                               "- Al menos 1 símbolo especial";
                return false;
            }

            mensajeError = string.Empty;
            return true;
        }
    }

}
