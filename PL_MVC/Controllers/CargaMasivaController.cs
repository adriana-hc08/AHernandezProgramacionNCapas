using Antlr.Runtime;
using BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PL_MVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult CargaMasiva()            
        {
            Session["RutaError"] = null;
            ML.Usuario usuario = new ML.Usuario();
            usuario.Errores = new List<object>();
            usuario.Correctos = new List<object>();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult CargaMasiva(HttpPostedFileBase archivo,string Action)
        {            
            ML.Usuario usuarioView = new ML.Usuario();
            usuarioView.Errores = new List<object>();
            usuarioView.Correctos = new List<object>();

            if (Action == "Validar")
            {
                string rutaErrorTxt = System.Configuration.ConfigurationManager.AppSettings["CargaMasivaTxtErrores"];
                string rutaOkTxt= System.Configuration.ConfigurationManager.AppSettings["CargaMasivaTxtOk"];
                string ruta = Server.MapPath(@rutaErrorTxt);
                string rutaDestino = Server.MapPath(@rutaOkTxt);
                string extension = archivo.FileName.Split('.')[1];
                if (extension == "txt" )
                {
                    try
                    {
                        string NoInsertados = "";
                        string validos = "";
                        int contador = 1;
                        // Crea un StreamReader para leer el archivo
                        using (StreamReader sr = new StreamReader(archivo.InputStream))
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            // Lee el archivo línea por línea

                            string linea = sr.ReadLine();
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
                                usuario.Estatus = Convert.ToBoolean(partes[11]);
                                usuario.CURP = partes[12];

                                string Errores = ValidarDatos(usuario);
                                //string correcto="";

                                if (Errores == "")
                                {
                                    string lista;
                                    validos = validos + usuario.Nombre + "|" + usuario.ApellidoPaterno + "|" + usuario.ApellidoMaterno + "|" + usuario.UserName + "|" +
                                        usuario.Rol.IdRol + "|" + usuario.Email + "|" + usuario.Password + "|" + usuario.FechaNacimiento + "|" + usuario.Sexo +
                                        "|" + usuario.Telefono + "|" + usuario.Celular + "|" + usuario.Estatus + "|" + usuario.CURP + "\n";
                                    lista = usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno + "\n";

                                    usuarioView.Correctos.Add(lista);
                                }
                                else
                                {
                                    //NoInsertados = "";
                                    //NoInsertados += $"** Error el usuario {contador}: no pudo ser registrado: {usuario.Nombre} {usuario.ApellidoPaterno} no pudo ser registrado debido a: **\n";
                                    //NoInsertados = NoInsertados + Errores;
                                    //NoInsertados += "\n";
                                    string incorrectos ="";
                                    incorrectos += $"** Error el usuario {contador} {usuario.Nombre} {usuario.ApellidoPaterno} no pudo ser registrado debido a:\n";       
                                    incorrectos =incorrectos + Errores + "\n";
                                    NoInsertados += incorrectos + "\n";
                                    usuarioView.Errores.Add(incorrectos);
                                }
                                contador++;
                            }
                        }
                        if (NoInsertados == "")
                        {
                            Session["rutaDestino"] = rutaDestino;
                            using (StreamWriter esc = new StreamWriter(rutaDestino))
                            {
                                esc.WriteLine(validos);
                            }
                            //archivo.SaveAs(rutaDestino);
                        }
                        else
                        {
                            Session["RutaError"] = ruta;
                            using (StreamWriter escribir = new StreamWriter(ruta))
                            {
                                escribir.WriteLine(NoInsertados);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.Error = "Ocurrió un error: " + e.Message;
                    }
                }
                else
                {
                    ///Carga masiva excel 
                    string rutafileExcel = System.Configuration.ConfigurationManager.AppSettings["CargaMasivafilePath"];
                    string filePath = Server.MapPath(@rutafileExcel);
                    var fecha = DateTime.Now.ToString("ddMMyyyy hh:mm:ss");
                    string nombreArchivo = Path.GetFileNameWithoutExtension(archivo.FileName);
                    string rutaCompleta = Path.Combine(filePath, nombreArchivo)+".xlsx";
                    archivo.SaveAs(rutaCompleta);

                    string rutaErrorExcel = System.Configuration.ConfigurationManager.AppSettings["CargaMasivaExcelErrores"];
                    string rutaOkExcel = System.Configuration.ConfigurationManager.AppSettings["CargaMasivaExcelOk"];
                    string rutaExcel = Server.MapPath(@rutaErrorExcel);
                    string rutaDestinoExcel = Server.MapPath(@rutaOkExcel);
                    string validos = "";
                    string NoInsertados = "";
                    string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] +
                    rutaCompleta;
                    ML.Result result=BL.Usuario.leerExcel(connectionString);
                    if (result.Correct)
                    {                          
                        usuarioView.Usuarios = result.Objects;
                        int contador = 1;
                        
                        foreach(ML.Usuario usuario in result.Objects)
                        {
                            string Errores = ValidarDatos(usuario);
                            if (Errores == "")
                            {
                                string lista;
                                validos = validos + usuario.Nombre + "|" + usuario.ApellidoPaterno + "|" + usuario.ApellidoMaterno + "|" + usuario.UserName + "|" +
                                    usuario.Rol.IdRol + "|" + usuario.Email + "|" + usuario.Password + "|" + usuario.FechaNacimiento + "|" + usuario.Sexo +
                                    "|" + usuario.Telefono + "|" + usuario.Celular + "|" + usuario.Estatus + "|" + usuario.CURP + "\n";
                                lista = usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno + "\n";

                                usuarioView.Correctos.Add(lista);
                            }
                            else
                            {
                                string incorrectos = "";
                                incorrectos += $"** Error el usuario {contador} {usuario.Nombre} {usuario.ApellidoPaterno} no pudo ser registrado debido a:\n";
                                incorrectos = incorrectos + "\n" + Errores + "\n";
                                NoInsertados += incorrectos + "\n";
                                usuarioView.Errores.Add(incorrectos);
                            }
                            contador++;

                        }

                    }
                    else
                    {
                        ViewBag.Error = "Ocurrió un error, el usuario ya esta registrado ";
                    }
                    if (NoInsertados == "")
                    {
                        Session["rutaDestino"] = rutaDestinoExcel;
                        using (StreamWriter esc = new StreamWriter(rutaDestinoExcel))
                        {
                            esc.WriteLine(validos);
                        }
                        //archivo.SaveAs(rutaDestino);
                    }
                    else
                    {
                        Session["RutaError"] = rutaExcel;
                        using (StreamWriter escribir = new StreamWriter(rutaExcel))
                        {
                            escribir.WriteLine(NoInsertados);
                        }
                    }


                    return View(usuarioView);
                }

            }
            if (Action == "Cargar" && Session["rutaDestino"] != null)
            {
                string validos = Session["rutaDestino"].ToString();

                try
                {
                    //string NoInsertados = "";
                    //string validos = "";
                    // Crea un StreamReader para leer el archivo
                    using (StreamReader sr = new StreamReader(validos))
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        // Lee el archivo línea por línea
                        string linea;
                        int cont = 1;
                        while ((linea = sr.ReadLine()) != null)
                        {
                            string[] partes = linea.Split('|'); // Reemplaza '|' con el delimitador que quieras usar
                            if (partes.Length == 13)
                            {
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
                                usuario.Estatus = Convert.ToBoolean(partes[11]);
                                usuario.CURP = partes[12];

                                ML.Result result = BL.Usuario.AddEFSP(usuario);
                                if (!result.Correct)
                                {
                                    usuarioView.Errores.Add($"No se pudo insertar al Usuario: {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}\n");
                                }
                                else
                                {
                                    usuarioView.Correctos.Add($"Usuario insertado: {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}\n");
                                }

                                cont++;
                            }
                        }

                    }
                    Session["rutaDestino"] = null;
                }
                catch (Exception e)
                {
                    ViewBag.Error = "Ocurrió un error: " + e.Message;
                }

            }           

            return View(usuarioView);
        }
        [HttpGet]
        public ActionResult Download()
        {
            //Session["RutaError"] = null;

            if (Session["RutaError"] != null)
            {
                string error = Session["RutaError"].ToString();

                byte[] fileBytes = System.IO.File.ReadAllBytes(error);
                string fileName = "Errores.txt";

                Session["RutaError"] = null;

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                //return Json("");
            } else
            {
                return RedirectToAction("CargaMasiva");
            }
           
        }
        [NonAction]
        public static string ValidarDatos(ML.Usuario usuario)
        {
            string Errores = "";
            if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s-]+$"))
            {
                Errores += ("El nombre solo debe contener letras \n");
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

            if (!(usuario.Rol.IdRol > 0 && usuario.Rol.IdRol <= 2))
            {
                Errores += ("El rango de rol es de 1 a 2 \n");
            }
            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Errores += ("El email debe tener un formato válido (ejemplo: usuario@dominio.com)\n");
            }
            if (!Regex.IsMatch(usuario.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!$@%*?&])[A-Za-z\d!$@%*?&]{8,}$"))
            {
                Errores += ("La contraseña tiene que tener 8 caracteres, La primera letra mayúscula y debe contener al menos un número\n");
            }
            if (usuario.Sexo != 'M' && usuario.Sexo != 'F')
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