using Microsoft.SqlServer.Server;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                            usuario.Sexo = (row[10].ToString());
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
                        usuario.Sexo = (row[8].ToString());
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

    }
}




