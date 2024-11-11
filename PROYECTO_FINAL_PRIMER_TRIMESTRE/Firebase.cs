using Firebase.Database;
using FirebaseAdmin;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows;


namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    internal class Firebase
    {
        private static Firebase conexion;
        private IFirebaseClient bbdd;
        private readonly IFirebaseConfig config;
        private Firebase()
        {
            config = new FirebaseConfig
            {
                AuthSecret = "t2Io029yVgzKGIZ2w3N6RYWISdmRAcxJXsK7vgBt",
                BasePath = "https://ayudaris-b7ec9-default-rtdb.europe-west1.firebasedatabase.app/"
            };
            bbdd = new FireSharp.FirebaseClient(config);
            if (bbdd == null)
            {
                MessageBox.Show("Error al establecer la conexión con la base de datos");
            }

        }
        public static Firebase GetInstance()
        {
            if (conexion == null)
            {
                conexion = new Firebase();
            }
            return conexion;
        }

        public IFirebaseClient GetClient()
        {
            return bbdd;
        }
    }
}