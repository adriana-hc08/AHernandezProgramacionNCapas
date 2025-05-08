using DL_EF;
using Microsoft.SqlServer.Server;
using ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Usuario
    {
        //Metodos con el query completo
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "INSERT INTO [dbo].[Usuario] ([Nombre],[ApellidoPaterno],[ApellidoMaterno],[IdRol]) " +
                    "VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno,@IdRol);";

                    SqlCommand command = new SqlCommand(query, contex);

                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    command.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    command.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    contex.Open();

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error...";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UPDATE [dbo].[Usuario] SET Nombre=@Nombre,ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno WHERE IdUsuario= @IdUsuario";

                    SqlCommand command = new SqlCommand(query, contex);
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    command.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    contex.Open();

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error...";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }


        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "DELETE FROM [dbo].[Usuario] WHERE IdUsuario=@IdUsuario";

                    SqlCommand command = new SqlCommand(query, contex);
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    contex.Open();

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error...";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }


        //Metodos con store Procedure

        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand command = new SqlCommand("UsuarioAdd", contex);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    command.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    command.Parameters.AddWithValue("@UserNAme", usuario.UserName);
                    command.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Password", usuario.Password);
                    command.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    command.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Celular", usuario.Celular);
                    command.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    command.Parameters.AddWithValue("@CURP", usuario.CURP);
                    command.Parameters.AddWithValue("@Imagen", usuario.Imagen);
                    command.Parameters.AddWithValue("@IdDireccion", usuario.Direccion.IdDireccion);

                    contex.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error...";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result UpdateSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand command = new SqlCommand("UsuarioUpdate", contex);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    command.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    command.Parameters.AddWithValue("@UserName", usuario.UserName);
                    command.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Password", usuario.Password);
                    command.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    command.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Celular", usuario.Celular);
                    command.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    command.Parameters.AddWithValue("@CURP", usuario.CURP);
                    command.Parameters.AddWithValue("@Imagen", usuario.Imagen);
                    command.Parameters.AddWithValue("@IdDireccion", usuario.Direccion.IdDireccion);

                    contex.Open();

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error...";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result DeleteSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand command = new SqlCommand("UsuarioDelete", contex);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    command.Parameters.Add("@IdDireccion", SqlDbType.Int).Direction=ParameterDirection.Output;
                    contex.Open();

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        int IdDireccion = Convert.ToInt32(command.Parameters["@IdDireccion"].Value);
                        result.Object = IdDireccion;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error...";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioGetAll", contex);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.Nombre = (row[1].ToString());
                            usuario.ApellidoPaterno = (row[2].ToString());
                            usuario.ApellidoMaterno = (row[3].ToString());
                            usuario.UserName = (row[4].ToString());
                            usuario.Rol.IdRol = Convert.ToByte(row[5].ToString());
                            usuario.Rol.Nombre = (row[6].ToString());
                            usuario.Email = (row[7].ToString());
                            usuario.Password = (row[8].ToString());
                            usuario.FechaNacimiento = (row[9].ToString());
                            usuario.Sexo = Convert.ToChar(row[10].ToString());
                            usuario.Telefono = (row[11].ToString());
                            usuario.Celular = (row[12].ToString());
                            usuario.Estatus = Convert.ToBoolean(row[13].ToString());
                            usuario.CURP = (row[14].ToString());
                            usuario.Imagen = row[15].ToString() != "" ? (byte[])row[15] : null;

                            usuario.Direccion = new ML.Direccion();
                            string valor = row[16].ToString();
                            if (row[16].ToString() != "")
                            {
                                //si trae un id

                                usuario.Direccion.IdDireccion = Convert.ToInt32(row[16].ToString()); //row[16] != UsuarioNull.Value && !string.IsNullOrWhiteSpace(row[16].ToString())
                            } else
                            {
                                usuario.Direccion.IdDireccion = 0;
                            }
                            //? Convert.ToInt32(row[16])
                            // : (int?)null;

                            usuario.Direccion.Calle = row[17].ToString();
                            usuario.Direccion.NumeroInterior = row[18].ToString();
                            usuario.Direccion.NumeroExterior = row[19].ToString();

                            usuario.Direccion.Colonia = new ML.Colonia();
                            string val = row[20].ToString();
                            if (row[20].ToString() != "")
                            {
                                usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[20].ToString());
                            }
                            else
                            {
                                usuario.Direccion.Colonia.IdColonia =0;
                            }
                             // != "" ? Convert.ToInt32(null);
                            usuario.Direccion.Colonia.Nombre = row[21].ToString();
                            usuario.Direccion.Colonia.CodigoPostal = row[22].ToString();

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            string mun = row[23].ToString();
                            if (row[23].ToString() != "")
                            {
                                usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(row[23].ToString());
                            }
                            else
                            {
                                usuario.Direccion.Colonia.Municipio.IdMunicipio = 0;
                            }                            
                            usuario.Direccion.Colonia.Municipio.Nombre = row[24].ToString();

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            string est = row[25].ToString();
                            if (row[25].ToString() != "")
                            {
                                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(row[25].ToString());
                            }
                            else
                            {
                                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = 0;
                            }                           
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = row[26].ToString();

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static ML.Result GetbyId(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioGetById", contex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        DataRow row = dataTable.Rows[0];

                        usuario.Nombre = (row[0].ToString());
                        usuario.ApellidoPaterno = (row[1].ToString());
                        usuario.ApellidoMaterno = (row[2].ToString());
                        usuario.UserName = (row[3].ToString());
                        usuario.Rol.IdRol = Convert.ToByte(row[4].ToString());
                        usuario.Email = (row[5].ToString());
                        usuario.Password = (row[6].ToString());
                        usuario.FechaNacimiento = (row[7].ToString());
                        usuario.Sexo = Convert.ToChar(row[8].ToString());
                        usuario.Telefono = (row[9].ToString());
                        usuario.Celular = (row[10].ToString());
                        usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                        usuario.CURP = (row[12].ToString());
                        usuario.Imagen = row[13].ToString() != "" ? (byte[])row[13] : null;
                        
                        if (row[14].ToString() != "")
                        {
                            usuario.Direccion.IdDireccion = Convert.ToInt32(row[14].ToString());
                        }
                        else
                        {
                            usuario.Direccion.IdDireccion = 0;
                        }                                                                   
                        usuario.Direccion.Calle = row[15].ToString();
                        usuario.Direccion.NumeroInterior = row[16].ToString();
                        usuario.Direccion.NumeroExterior = row[17].ToString();

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = row[18].ToString() != "" ? Convert.ToInt32(row[18].ToString()) : 0;
                        //usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[18].ToString());
                        usuario.Direccion.Colonia.CodigoPostal = row[19].ToString();

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();                       
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = row[20].ToString() != "" ? Convert.ToInt32(row[20].ToString()) : 0;


                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();                       
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = row[21].ToString() != "" ? Convert.ToInt32(row[21].ToString()) : 0;

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static ML.Result GetAllEFSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var listaUsuarios=contex.UsuarioGetAll().ToList();
                    if(listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var usuarioDB in listaUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                            usuario.IdUsuario = usuarioDB.IdUsuario;
                            usuario.Nombre= usuarioDB.Nombre;
                            usuario.ApellidoPaterno=usuarioDB.ApellidoPaterno;
                            usuario.ApellidoMaterno = usuarioDB.ApellidoMaterno;
                            usuario.UserName= usuarioDB.UserName;
                            usuario.Rol.IdRol = usuarioDB.IdRol.Value;
                            usuario.Rol.Nombre = usuarioDB.Rol;
                            usuario.Email= usuarioDB.Email;
                            usuario.Password= usuarioDB.Password;
                            usuario.FechaNacimiento= usuarioDB.FechaNacimiento.ToString();
                            usuario.Sexo= usuarioDB.Sexo[0];
                            usuario.Telefono= usuarioDB.Telefono;
                            usuario.Celular= usuarioDB.Celular;
                            usuario.Estatus= usuarioDB.Estatus;
                            usuario.CURP= usuarioDB.CURP;
                            usuario.Imagen= usuarioDB.Imagen;
                            usuario.Direccion.IdDireccion = usuarioDB.IdDireccion;
                            usuario.Direccion.Calle=usuarioDB.Calle;
                            usuario.Direccion.NumeroInterior = usuarioDB.NumeroInterior;
                            usuario.Direccion.NumeroExterior = usuarioDB.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia=usuarioDB.IdColonia;
                            usuario.Direccion.Colonia.Nombre = usuarioDB.Colonia;
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = usuarioDB.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = usuarioDB.Municipio;
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = usuarioDB.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre=usuarioDB.Estado;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }

            }catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetByIdEFSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var usuarioDB =contex.UsuarioGetById(IdUsuario).FirstOrDefault();
                    if (usuarioDB!=null)
                    {
                       
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
      
                            usuario.Nombre = usuarioDB.Nombre;
                            usuario.ApellidoPaterno = usuarioDB.ApellidoPaterno;
                            usuario.ApellidoMaterno = usuarioDB.ApellidoMaterno;
                            usuario.UserName = usuarioDB.UserName;
                            usuario.Rol.IdRol = usuarioDB.IdRol.Value;
                            
                            usuario.Email = usuarioDB.Email;
                            usuario.Password = usuarioDB.Password;
                            usuario.FechaNacimiento = usuarioDB.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.Sexo = usuarioDB.Sexo[0];
                            usuario.Telefono = usuarioDB.Telefono;
                            usuario.Celular = usuarioDB.Celular;
                            usuario.Estatus = usuarioDB.Estatus;
                            usuario.CURP = usuarioDB.CURP;
                            usuario.Imagen = usuarioDB.Imagen;
                            usuario.Direccion.IdDireccion = usuarioDB.IdDireccion.Value;
                            usuario.Direccion.Calle = usuarioDB.Calle;
                            usuario.Direccion.NumeroInterior = usuarioDB.NumeroInterior;
                            usuario.Direccion.NumeroExterior = usuarioDB.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia = usuarioDB.IdColonia.Value;
                            
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = usuarioDB.IdMunicipio.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = usuarioDB.IdEstado.Value;
                            

                        
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron el usuario";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result AddEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    ObjectParameter idUsuario= new ObjectParameter("IdUsuario",typeof(int));
                    var rowsAffected = contex.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno,usuario.ApellidoMaterno,
                        usuario.UserName,usuario.Rol.IdRol,usuario.Email, usuario.Password,usuario.FechaNacimiento,
                        usuario.Sexo.ToString(),usuario.Telefono,usuario.Celular,usuario.Estatus,usuario.CURP,usuario.Imagen,
                        usuario.Direccion.IdDireccion);
                    if (rowsAffected>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result UpdateEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var rowsAffected = contex.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                        usuario.UserName, usuario.Rol.IdRol, usuario.Email, usuario.Password, usuario.FechaNacimiento,
                        usuario.Sexo.ToString(), usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen,
                        usuario.Direccion.IdDireccion);

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el usuario";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result DeleteEFSP(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    
                    var rowsAffected = contex.UsuarioDelete(IdDireccion);
                    if (rowsAffected > 0)
                    {                       
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar al usuario";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            Result result = new ML.Result();
            try
            {
                using(AHernandezProgramacionNCapasEntities contex =new AHernandezProgramacionNCapasEntities())
                {
                    DL_EF.Usuario usuarioDL=new DL_EF.Usuario();
                    usuarioDL.Nombre = usuario.Nombre;
                    usuarioDL.ApellidoPaterno=usuario.ApellidoPaterno;
                    usuarioDL.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDL.UserName= usuario.UserName;
                    usuarioDL.IdRol = usuario.Rol.IdRol;
                    usuarioDL.Email= usuario.Email;
                    usuarioDL.Password= usuario.Password;
                    usuarioDL.FechaNacimiento= DateTime.ParseExact(usuario.FechaNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    usuarioDL.Sexo=usuario.Sexo.ToString();
                    usuarioDL.Telefono=usuario.Telefono;
                    usuarioDL.Celular=usuario.Celular;
                    usuarioDL.Estatus=usuario.Estatus;
                    usuarioDL.CURP=usuario.CURP;
                    usuarioDL.Imagen=usuario.Imagen;
                    usuarioDL.IdDireccion = usuario.Direccion.IdDireccion;

                    contex.Usuarios.Add(usuarioDL);
                    contex.SaveChanges();
                    result.Correct = true;
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
            }
            return result;
        }
        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var query =(from a in contex.Usuarios where a.IdUsuario==usuario.IdUsuario
                                select a).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.UserName = usuario.UserName;
                        query.IdRol = usuario.Rol.IdRol;
                        query.Email = usuario.Email;
                        query.Password = usuario.Password;
                        query.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        query.Sexo = usuario.Sexo.ToString();
                        query.Telefono = usuario.Telefono;
                        query.Celular = usuario.Celular;
                        query.Estatus = usuario.Estatus;
                        query.CURP = usuario.CURP;
                        query.Imagen = usuario.Imagen;
                        query.IdDireccion = usuario.Direccion.IdDireccion;
                        contex.SaveChanges();
                        result.Correct=true; 
                    }
                                     
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result DeleteLINQ(int IdDIreccion)
        {
            Result result = new ML.Result();
            try
            {
                using(AHernandezProgramacionNCapasEntities contex=new AHernandezProgramacionNCapasEntities())
                {
                    var query = (from a in contex.Usuarios
                                 where a.IdDireccion == IdDIreccion
                                 select a).First();
                    contex.Usuarios.Remove(query);
                    contex.SaveChanges();
                    result.Correct=true; 
                }
            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAllEFLinq()
        {
            Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var listUsuarios = (from usuarioDB in contex.Usuarios
                                        join rolDB in contex.Rols on usuarioDB.IdRol equals rolDB.IdRol
                                        join direccionDB in contex.Direccions on usuarioDB.IdDireccion equals direccionDB.IdDireccion
                                        join coloniaDB in contex.Colonias on direccionDB.IdColonia equals coloniaDB.IdColonia
                                        join municipioDB in contex.Municipios on coloniaDB.IdMunicipio equals municipioDB.IdMunicipio
                                        join estadoDB in contex.Estadoes on municipioDB.IdEstado equals estadoDB.IdEstado
                                        select new
                                        {
                                            IdUsuario= usuarioDB.IdUsuario,
                                            Nombre=usuarioDB.Nombre,
                                            ApellidoPaterno=usuarioDB.ApellidoPaterno,
                                            ApellidoMaterno=usuarioDB.ApellidoMaterno,
                                            UserName=usuarioDB.UserName,
                                            IdRol= rolDB.IdRol,
                                            Rol=rolDB.Nombre,
                                            Email=usuarioDB.Email,
                                            Password=usuarioDB.Password,
                                            FechaNacimiento=usuarioDB.FechaNacimiento,
                                            Sexo=usuarioDB.Sexo,
                                            Telefono=usuarioDB.Telefono,
                                            Celular=usuarioDB.Celular,
                                            Estatus=usuarioDB.Estatus,
                                            CURP=usuarioDB.CURP,
                                            Imagen=usuarioDB.Imagen,
                                            IdDirecccion=usuarioDB.Direccion.IdDireccion,
                                            Calle=usuarioDB.Direccion.Calle,
                                            NumeroInterior=usuarioDB.Direccion.NumeroInterior,
                                            NumeroExterior=usuarioDB.Direccion.NumeroInterior,
                                            IdColonia=usuarioDB.Direccion.Colonia.IdColonia,
                                            Colonia=usuarioDB.Direccion.Colonia.Nombre,
                                            IdMunicipio=usuarioDB.Direccion.Colonia.Municipio.IdMunicipio,
                                            Municipio = usuarioDB.Direccion.Colonia.Municipio.Nombre,
                                            IdEstado = usuarioDB.Direccion.Colonia.Municipio.Estado.IdEstado,
                                            Estado = usuarioDB.Direccion.Colonia.Municipio.Estado.Nombre,

                                        }).ToList();
                    if (listUsuarios !=null && listUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in listUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol=new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia= new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio=new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado=new ML.Estado();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre=obj.Nombre;
                            usuario.ApellidoPaterno= obj.ApellidoPaterno;
                            usuario.ApellidoMaterno= obj.ApellidoMaterno;
                            usuario.UserName= obj.UserName;                
                            usuario.Rol.IdRol = obj.IdRol;
                            usuario.Rol.Nombre = obj.Rol;
                            usuario.Email=obj.Email;
                            usuario.Password= obj.Password;
                            usuario.FechaNacimiento = (obj.FechaNacimiento).ToString("dd-MM-yyyy");
                            usuario.Sexo= obj.Sexo[0];
                            usuario.Telefono= obj.Telefono;
                            usuario.Celular= obj.Celular;
                            usuario.Estatus= obj.Estatus;
                            usuario.CURP= obj.CURP;
                            usuario.Imagen= obj.Imagen;
                            usuario.Direccion.IdDireccion = obj.IdDirecccion;
                            usuario.Direccion.Calle= obj.Calle;
                            usuario.Direccion.NumeroInterior= obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior= obj.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre=obj.Colonia;
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.Municipio;
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado= obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.Estado;


                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByIdEFLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var usuariobyid = (from usuarioDB in contex.Usuarios
                                         join rolDB in contex.Rols on usuarioDB.IdRol equals rolDB.IdRol 
                                        
                                        join direccionDB in contex.Direccions on usuarioDB.IdDireccion equals direccionDB.IdDireccion
                                        join coloniaDB in contex.Colonias on direccionDB.IdColonia equals coloniaDB.IdColonia
                                        join municipioDB in contex.Municipios on coloniaDB.IdMunicipio equals municipioDB.IdMunicipio
                                        join estadoDB in contex.Estadoes on municipioDB.IdEstado equals estadoDB.IdEstado
                                        where usuarioDB.IdUsuario== IdUsuario
                                        select new
                                        {
                                            IdUsuario = usuarioDB.IdUsuario,
                                            Nombre = usuarioDB.Nombre,
                                            ApellidoPaterno = usuarioDB.ApellidoPaterno,
                                            ApellidoMaterno = usuarioDB.ApellidoMaterno,
                                            UserName = usuarioDB.UserName,
                                            IdRol = rolDB.IdRol,
                                            Rol = rolDB.Nombre,
                                            Email = usuarioDB.Email,
                                            Password = usuarioDB.Password,
                                            FechaNacimiento = usuarioDB.FechaNacimiento,
                                            Sexo = usuarioDB.Sexo,
                                            Telefono = usuarioDB.Telefono,
                                            Celular = usuarioDB.Celular,
                                            Estatus = usuarioDB.Estatus,
                                            CURP = usuarioDB.CURP,
                                            Imagen = usuarioDB.Imagen,
                                            IdDirecccion = usuarioDB.Direccion.IdDireccion,
                                            Calle = usuarioDB.Direccion.Calle,
                                            NumeroInterior = usuarioDB.Direccion.NumeroInterior,
                                            NumeroExterior = usuarioDB.Direccion.NumeroInterior,
                                            IdColonia = usuarioDB.Direccion.Colonia.IdColonia,
                                            Colonia = usuarioDB.Direccion.Colonia.Nombre,
                                            IdMunicipio = usuarioDB.Direccion.Colonia.Municipio.IdMunicipio,
                                            Municipio = usuarioDB.Direccion.Colonia.Municipio.Nombre,
                                            IdEstado = usuarioDB.Direccion.Colonia.Municipio.Estado.IdEstado,
                                            Estado = usuarioDB.Direccion.Colonia.Municipio.Estado.Nombre,

                                        }).SingleOrDefault();
                    if (usuariobyid != null )
                    {
                        result.Objects = new List<object>();

                        
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol=new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia= new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio=new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado=new ML.Estado();
                            usuario.IdUsuario = usuariobyid.IdUsuario;
                            usuario.Nombre= usuariobyid.Nombre;
                            usuario.ApellidoPaterno= usuariobyid.ApellidoPaterno;
                            usuario.ApellidoMaterno= usuariobyid.ApellidoMaterno;
                            usuario.UserName= usuariobyid.UserName;                
                            usuario.Rol.IdRol = usuariobyid.IdRol;
                            usuario.Email=usuariobyid.Email;
                            usuario.Password= usuariobyid.Password;
                            usuario.FechaNacimiento = (usuariobyid.FechaNacimiento).ToString("dd-MM-yyyy");
                            usuario.Sexo= usuariobyid.Sexo[0];
                            usuario.Telefono= usuariobyid.Telefono;
                            usuario.Celular= usuariobyid.Celular;
                            usuario.Estatus= usuariobyid.Estatus;
                            usuario.CURP= usuariobyid.CURP;
                            usuario.Imagen= usuariobyid.Imagen;
                            usuario.Direccion.IdDireccion = usuariobyid.IdDirecccion;
                            usuario.Direccion.Calle= usuariobyid.Calle;
                            usuario.Direccion.NumeroInterior= usuariobyid.NumeroInterior;
                            usuario.Direccion.NumeroExterior= usuariobyid.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia = usuariobyid.IdColonia;
                            usuario.Direccion.Colonia.Nombre= usuariobyid.Colonia;
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = usuariobyid.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = usuariobyid.Municipio;
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado= usuariobyid.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = usuariobyid.Estado;
                            result.Object= usuario; 
                        
                        result.Correct = true;
                    }
                    else
                    {
                       result.Correct = false;
                       result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
          return result;
        }
        public static ML.Result GetByIdUsernameEFLinq(string UserName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var username = (from usuarioDB in contex.Usuarios
                                    where usuarioDB.UserName== UserName
                                    select usuarioDB).FirstOrDefault();
                    if (username != null)
                    {
                        result.Object = username;
                        result.Correct = true;
                        result.ErrorMessage = "El username ya existe";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByIdEmailEFLinq(string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var correo = (from usuarioDB in contex.Usuarios
                                 where usuarioDB.Email == Email
                                 select usuarioDB).FirstOrDefault();
                    if (correo != null)
                    {
                        result.Object = correo;
                        result.Correct = true;
                        result.ErrorMessage = "El Email ya existe";
                    }else
                    {
                        result.Correct = false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByIdCURPEFLinq(string CURP)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var curp= (from usuarioDB in contex.Usuarios
                               where usuarioDB.CURP== CURP
                               select usuarioDB).FirstOrDefault();
                    if (curp!= null)
                    {
                       result.Object= curp;
                       result.Correct = true;
                        result.ErrorMessage = "La CURP ya existe";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result UpdateEstatus(ML.Usuario usuario)
        {
            Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var query = (from a in contex.Usuarios
                                 where a.IdUsuario == usuario.IdUsuario
                                 select a).SingleOrDefault();
                    if (query != null)
                    {                       
                        query.Estatus = usuario.Estatus;                        
                        contex.SaveChanges();
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result UpdateEstatusEFSP(int IdUsuario,bool Estatus)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var rowsAffected = contex.ActualizarEstatus(IdUsuario,Estatus);

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el usuario";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}




