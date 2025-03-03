using Microsoft.SqlServer.Server;
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
            catch
            {

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
            catch
            {

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
                    command.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
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
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
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
            catch
            {

            }
            return result;
        }
        public static ML.Result DeleteSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand command = new SqlCommand("UsuarioDelete", contex);
                    command.CommandType = CommandType.StoredProcedure;

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
            catch
            {

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
                    SqlCommand cmd = new SqlCommand("UsuarioGetAll",contex);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.Nombre = (row[1].ToString());
                            usuario.ApellidoPaterno= (row[2].ToString());
                            usuario.ApellidoMaterno= (row[3].ToString());
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = Convert.ToByte(row[4].ToString());  
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
       
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage=ex.Message;
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
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {                           
                            ML.Usuario usuario=new ML.Usuario();
                            
                            usuario.Nombre = (row[0].ToString());
                            usuario.ApellidoPaterno = (row[1].ToString());
                            usuario.ApellidoMaterno = (row[2].ToString());
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = Convert.ToByte(row[3].ToString());
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
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}




