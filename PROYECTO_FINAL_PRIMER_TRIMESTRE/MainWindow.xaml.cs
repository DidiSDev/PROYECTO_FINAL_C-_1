using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Threading;

namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    public partial class MainWindow : Window
    {
        private IFirebaseClient cliente;
        public MainWindow()
        {
            InitializeComponent();


            //VOY A ESTABLECER UN CONTADOR DE 10 SEGUNDOS HASTA QUE SE ABRA LA NUEVA VENTANA
            //ASI DOY TIEMPO A LA PRESENTACION A MOSTRARSE.
            DispatcherTimer temporizador = new DispatcherTimer();

            //LO CAMBIO A 1 PARA AGILIZAR VELOCIDAD, VOLVER A PONER EN 10 ANTES DE ENTREGAR
            temporizador.Interval = TimeSpan.FromSeconds(3); // CAMBIAR A 10 SEGUNDOS ANTES DE LA ENTREGA
            temporizador.Tick += async (sender, e) => await contadorTemporizador(sender, e); //USO EL TEMPORIZADOR CONECTADDO CON EL Task ASÍNCRONO DEL TIMER, DEBE ESPERAR
         
            temporizador.Start();
        }


        private async Task contadorTemporizador(object sender, EventArgs e)
        {
            DispatcherTimer temp = (DispatcherTimer)sender;
            temp.Stop();

            try
            {
                bool existeAdmin = await verificarExistenciaAdmin();

                if (existeAdmin)
                {
                    //SI EXISTE ADMIN, ABRIMOS LoginWindow
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                }
                else
                {
                    //ESTA VENTANA DE REGISTRO ÚNICA Y EXCLUSIVAMENTE SE VA A ABRIR SIEMPRE Y CUANDO NO EXISTA ADMINISTRADOR, EN CASO CONTRARIO NUNCA MÁS SE ABRIRÁ
                    RegistrarWindow registrarWindow = new RegistrarWindow();
                    registrarWindow.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error al buscar al admin en la BBDD");
                Application.Current.Shutdown(); //SI PASA ESTO APAGAMOS
            }
        }
        private async Task<bool> verificarExistenciaAdmin() //DEBE SER ASÍNCRONO, RECORDAR, IMPORTANTE
        {
            //RECOGEMOS LOS DATOS DE FB
            IFirebaseClient clienteBBDD = Firebase.GetInstance().GetClient();
            //TENGO UN ERROR AL CONECTAR EN LA BBDD, DEPURACIÓN:
            if (clienteBBDD == null)
            {
                MessageBox.Show("¡Error al conectar con la base de datos!"); //ESTE ERROR NO SALTA, ASÍ QUE LA CONEXIÓN ESTÁ BIEN
                Application.Current.Shutdown();
            }



            //SOLAMENTE HABRÁ ALGÚN ADMIN
            FirebaseResponse response = await clienteBBDD.GetAsync("Administrador");
           // MessageBox.Show("Respuesta de Firebase: " + response.Body); DEPPURACI`´ON

            if (response.Body == "null")
            {
                //ESTO ES Q NO HAY NIGNG ADMNIN
                return false;
            }

            //RECOGEMOS LA RESPUESTA EN UN DICCIONARIO, AUNQUE SOLAMENTE ES UNO DEJAMOS LA OPCIÓN DE QUE EN EL FUTURO SE QUIERAN AÑADIR MÁS, ASÍ ESTE CÓDIGO
            //NO TIENE QUE MODIFICARSEN EL FTURO, DEJAMOS ACTIVO EL DICTIONARY
            //Pues como da error desSerializamos directamente como objeto y no como diccionario
            Administrador admin = JsonConvert.DeserializeObject<Administrador>(response.Body);

            //HAY AL MENOS 1 ADMIN?? SI SÍ, LOGINWINDOW..
            return admin != null;
        }

    }
}
