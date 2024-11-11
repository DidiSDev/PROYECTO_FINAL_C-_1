using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_FINAL_PRIMER_TRIMESTRE
{
    class generarClaveUnica
    {
        public static string generarClave(int longitud = 24)
        {
            const string palabrasValidas= "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890";
            StringBuilder clave=new StringBuilder(); //STRINGBUILDER ME PERMITE ACUMULAR CARACTERES SIN TENER QUE SUMARLOS CON string algo=algo+ lonuevo
            using (var generadorAleatorio =new RNGCryptoServiceProvider()) //HE DESCUBIERTO
                                                                           //RNGCryptoServiceProvider ES UN GENERADOR DE CLAVES ALEATORIAS CRIPTOGRÁFICAMENTE SEGURO
            {
                byte[] datos = new byte[4];//ESTE ARRAY SERÁ USADO COMO BUFFER QUE ALMACENA 4 BYTES ¿POR QUÉ? PORQUE NECESITAMOS CONVERTITR ESOS BYTES A UN "uint"
                                                                //DE 32 BITS
                while (clave.Length<longitud)
                {
                    //AQUI LLENAMOS EL BUFFER CON VALORES ALEATORIOS GENERADOS DE LA FORMA MÁS SEGURA QUE EXISTE ACTUALMENTE EN C#, LA SEGURIDAD AQUÍ NO ESTÁ EN
                    //palabrasValidas, SINO EN EL ÍNDICE QUE RECOGE CADA CARÁCTER
                    generadorAleatorio.GetBytes(datos);
                    uint numero=BitConverter.ToUInt32(datos, 0); //CONVERTIMOS DE BYTES A BITS, ESTE "uint" SERÁ UTILIZADO COMO ÍNDICE PARA LA SELECCIÓN DE CARÁCTER
                    clave.Append(palabrasValidas[(int) (numero%(uint)palabrasValidas.Length)]); //CALCULAMOS INDICE, RECOGEMOS CARACTER Y LO AÑADIMOS A "clave"
                }
            }
            return clave.ToString();
            //HAY QUE INSTANCIAR CLASE EN REGISTRAR Y LLAMAR A ESTE MÉTODO PARA QUE NOS DEVUELVA LA CLAVE, EN UN STRING
        }
    
    }
}
