using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
 

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection("Data Source=.;Initial Catalog=AHernandezProgramacionNCapas;User ID=sa;Password=pass@word1"))
                {
                    string query = "INSERT INTO [dbo].[Usuario] ([Nombre],[ApellidoPaterno],[ApellidoMaterno]) " +
                        "VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno);";

                    SqlCommand command = new SqlCommand(query,contex);
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
            catch(Exception ex) 
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
                using (SqlConnection contex = new SqlConnection("Data Source=.;Initial Catalog=AHernandezProgramacionNCapas;User ID=sa;Password=pass@word1"))
                {
                    string query = "UPDATE [dbo].[Usuario] SET Nombre=@Nombre,ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno WHERE IdUsuario= @IdUsuario";

                    SqlCommand command = new SqlCommand(query, contex);
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    command.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    contex.Open();

                    int rowAffected=command.ExecuteNonQuery();

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
                using (SqlConnection contex = new SqlConnection("Data Source=.;Initial Catalog=AHernandezProgramacionNCapas;User ID=sa;Password=pass@word1"))
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
    }
}
