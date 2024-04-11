using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;

class Correo
{
    static string GenerarContrase�a()
    {
        Random random = new Random();

        // Primer caracter: letra may�scula
        char primerCaracter = (char)random.Next('A', 'Z' + 1);

        // Caracteres 2 a 5: letras aleatorias
        char[] letrasAleatorias = new char[4];
        for (int i = 0; i < letrasAleatorias.Length; i++)
        {
            letrasAleatorias[i] = (char)random.Next('a', 'z' + 1);
        }

        // �ltimo caracter: n�mero
        int ultimoCaracter = random.Next(0, 10);

        // Combinar todos los caracteres
        string contrase�a = string.Format("{0}{1}{2}{3}{4}{5}",
                                         primerCaracter,
                                         letrasAleatorias[0],
                                         letrasAleatorias[1],
                                         letrasAleatorias[2],
                                         letrasAleatorias[3],
                                         ultimoCaracter);
            
        return contrase�a;
    }

    public static String sendEmail(String correo, String motivo, String usuarioBD)
    {
        using (MailMessage message = new MailMessage())
        {
            String retorno;
            
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.From = new MailAddress(GetUserName(), "BOT FEMA");
            if(motivo == "USUARIO")
            {
                message.Subject = "Recuperaci�n de usuario";
                message.Body = "Has solicitado la recuperaci�n de tu usuario para el acceso a la aplicaci�n FEMA<br>" +
                    "<b>Tu usuario es: " + usuarioBD;
                retorno = "";
            }
            else
            {
                //Invocacion del metodo generador de clave
                String nuevaClave = GenerarContrase�a();
                message.Subject = "Recuperaci�n de contrase�a";
                message.Body = "Has solicitado la recuperaci�n de tu contrase�a para el acceso a la aplicaci�n FEMA<br>" +
                    "<b>Tu nueva contrase�a es: " + nuevaClave;
                retorno = nuevaClave;
            }
            message.IsBodyHtml = true;
            message.To.Add(correo);


            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,

                Credentials = new NetworkCredential(GetUserName(), GetPassword())
            };

            try
            {
                email.Send(message);
                return retorno;
            }
            catch (SmtpException e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }
    }

    private static String GetUserName()
    {
        return "luisperezfema@gmail.com";
    }

    private static String GetPassword()
    {
        return "aueq wifu wshp sgqv";
    }

}