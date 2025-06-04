using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL
{
    public class Prueba
    {
        public static void LeerTxt()
        {
            String archivo = "C:\\Users\\ALIEN22\\Documents\\ADRIANA HERNANDEZ CELESTINO\\test.txt";
            string txtErrores = @"C:\Users\ALIEN22\Documents\ADRIANA HERNANDEZ CELESTINO\ErroresUsuario.txt";

            try
            {
                String NoInsertados = "";
                // Usar StreamWriter fuera del bucle para no sobrescribir en cada iteración
                using (StreamWriter escribir = new StreamWriter(txtErrores, false)) // false para sobrescribir el archivo al inicio
                {
                    // Crea un StreamReader para leer el archivo
                    using (StreamReader sr = new StreamReader(archivo))
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        // Lee la primera línea (encabezados) y descártala
                        sr.ReadLine();

                        // Lee el archivo línea por línea
                        string linea;
                        while ((linea = sr.ReadLine()) != null)
                        {
                            string[] partes = linea.Split('|');
                            usuario.Nombre = partes[0];
                            usuario.ApellidoPaterno = partes[1];
                            usuario.ApellidoMaterno = partes[2];
                            usuario.UserName = partes[3];
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = Convert.ToByte(partes[4]);
                            usuario.Email = partes[5];

                            foreach (string parte in partes)
                            {
                                Console.WriteLine(parte);
                            }
                            Console.WriteLine();

                            string Errores = ValidarDatos(usuario);

                            // Si hay errores, los escribimos en el archivo
                            if (!string.IsNullOrEmpty(Errores))
                            {
                                escribir.WriteLine($"Error en línea: {linea}");
                                escribir.WriteLine($"Detalles: {Errores}");
                                escribir.WriteLine(); // Separador entre errores
                            }
                            else
                            {
                                ML.Result result = BL.Usuario.AddSP(usuario);
                                if (!result.Correct)
                                {
                                    NoInsertados += $"No se pudo insertar al Usuario: {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno} ({usuario.UserName})\n";
                                }
                            }
                        }
                    }

                    // Escribir todos los no insertados al final
                    if (!string.IsNullOrEmpty(NoInsertados))
                    {
                        escribir.WriteLine("\nUSUARIOS NO INSERTADOS:");
                        escribir.WriteLine(NoInsertados);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("El archivo no se encontró.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error: " + e.Message);
            }

            Console.ReadKey();
        }
        public static string ValidarDatos(ML.Usuario usuario)
        {
            List<string> errores = new List<string>();

            if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                errores.Add("El nombre solo debe contener letras y espacios");
            }

            if (!Regex.IsMatch(usuario.ApellidoPaterno, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                errores.Add("El Apellido Paterno solo debe contener letras y espacios");
            }

            if (!Regex.IsMatch(usuario.ApellidoMaterno, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                errores.Add("El Apellido Materno solo debe contener letras y espacios");
            }

            if (!Regex.IsMatch(usuario.UserName, @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8}$"))
            {
                errores.Add("El username debe tener 8 caracteres, primera letra mayúscula, al menos un número y un caracter especial");
            }

            if (!(usuario.Rol.IdRol > 0 && usuario.Rol.IdRol < 2))
            {
                errores.Add("El rango de rol es de 1 a 1"); // Corregí el mensaje porque tu condición es >0 y <2
            }

            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                errores.Add("El email debe tener un formato válido (ejemplo: usuario@dominio.com)");
            }

            return string.Join("\n", errores);
        }
    }
}
