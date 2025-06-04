using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL
{
    public class CargaMasiva
    {
        public static void LeerTxt()
        {
            String archivo = "C:\\Users\\ALIEN22\\Documents\\ADRIANA HERNANDEZ CELESTINO\\test.txt";
            string txtErrores = @"C:\Users\ALIEN22\Documents\ADRIANA HERNANDEZ CELESTINO\ErroresUsuario.txt";
            try
            {
                string NoInsertados = "";
                // Crea un StreamReader para leer el archivo
                using (StreamReader sr = new StreamReader(archivo))
                {
                    ML.Usuario usuario = new ML.Usuario();

                    // Lee el archivo línea por línea
                    string linea=sr.ReadLine() ;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] partes = linea.Split('|'); // Reemplaza '|' con el delimitador que quieras usar
                        usuario.Nombre = partes[0];
                        usuario.ApellidoPaterno = partes[1];
                        usuario.ApellidoMaterno = partes[2];
                        usuario.UserName = partes[3];
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Convert.ToByte(partes[4]);
                        usuario.Email = partes[5];
                        usuario.Password = partes[6];
                        usuario.FechaNacimiento = partes[7];
                        usuario.Sexo = Convert.ToChar(partes[8]);
                        usuario.Telefono = partes[9];
                        usuario.Celular = partes[10];
                        usuario.Estatus=Convert.ToBoolean(partes[11]);
                        usuario.CURP= partes[12];

                        foreach (string parte in partes)
                        {
                            Console.WriteLine(parte); // Elimina espacios en blanco
                        }
                        Console.WriteLine();

                        string Errores = ValidarDatos(usuario);

                        if (Errores=="")
                        {
                            ML.Result result=BL.Usuario.AddEFSP(usuario);
                            if (!result.Correct)
                            {
                                NoInsertados += $"No se pudo insertar al Usuario: {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}\n";
                                                          
                            }
                        }else
                        {
                            NoInsertados += $"** Error en registro: {usuario.Nombre} {usuario.ApellidoPaterno} **\n";
                            NoInsertados = NoInsertados + Errores;
                            NoInsertados += "\n";
                            Console.WriteLine("=============================================================");
                        }
                        
                    }
                    
                    using (StreamWriter escribir = new StreamWriter(txtErrores))
                    {                       
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
            string Errores = "";
            if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                Errores +=("El nombre solo debe contener letras \n");
            }

            if (!Regex.IsMatch(usuario.ApellidoPaterno, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                Errores += ("El Apellido Paterno solo debe contener letras \n");
            }

            if (!Regex.IsMatch(usuario.ApellidoMaterno, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                Errores += ("El Apellido Materno solo debe contener letras \n");
            }

            if (!Regex.IsMatch(usuario.UserName, @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8}$"))
            {
                Errores += ("El username debe tener 8 caracteres, primera letra mayúscula, al menos un número y un caracter especial \n");
            }

            if (!(usuario.Rol.IdRol > 0 && usuario.Rol.IdRol <=2))
            {
                Errores += ("El rango de rol es de 1 a 2 \n"); 
            }
            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Errores += ("El email debe tener un formato válido (ejemplo: usuario@dominio.com)\n");
            }
            if(!Regex.IsMatch(usuario.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!$@%*?&])[A-Za-z\d!$@%*?&]{8,}$"))
            {
                Errores += ("La contraseña tiene que tener 8 caracteres, La primera letra mayúscula y debe contener al menos un número\n");
            }        
            if (usuario.Sexo!= 'M'&& usuario.Sexo!= 'F')
            {
                Errores += ("El campo Sexo debe ser 'M' (Masculino) o 'F' (Femenino))\n");
            }
            if (!Regex.IsMatch(usuario.Telefono, @"^[0-9]+$"))
            {
                Errores += ("El telefono solo debe contener numeros\n");
            }
            if (!Regex.IsMatch(usuario.Celular, @"^[0-9]+$"))
            {
                Errores += ("El Celular solo debe contener numeros\n");
            }
            /*if (usuario.Estatus !=1 && usuario.Estatus != 0)
            {
                Errores += "El estatus debe ser '1' (Activo) o '0' (Inactivo)\n";
            }*/
            if (!Regex.IsMatch(usuario.CURP, @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$"))
            {
                Errores += ("Ingrese una CURP valida\n");
            }
            return Errores;
        }
    }
}
