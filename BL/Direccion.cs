using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using System.Data.Entity.Core.Objects;
using System.Xml;

namespace BL
{
    public class Direccion
    {
        public static ML.Result DireccionAdd(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionAdd", contex);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    
                    cmd.Parameters.AddWithValue("@Calle", usuario.Direccion.Calle);
                    cmd.Parameters.AddWithValue("@NumeroInterior", usuario.Direccion.NumeroInterior);
                    cmd.Parameters.AddWithValue("@NumeroExterior", usuario.Direccion.NumeroExterior);
                    cmd.Parameters.AddWithValue("@IdColonia", usuario.Direccion.Colonia.IdColonia);

                    SqlParameter outputIdDireccion = new SqlParameter("@IdDireccion", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    cmd.Parameters.Add(outputIdDireccion);
                    contex.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();                    

                    if (rowsAffected > 0)
                    {
                        result.Object = outputIdDireccion.Value;
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
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static ML.Result DireccionUpdate(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionUpdate", contex);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdDireccion", usuario.Direccion.IdDireccion);
                    cmd.Parameters.AddWithValue("@Calle", usuario.Direccion.Calle);
                    cmd.Parameters.AddWithValue("@NumeroInterior", usuario.Direccion.NumeroInterior);
                    cmd.Parameters.AddWithValue("@NumeroExterior", usuario.Direccion.NumeroExterior);
                    cmd.Parameters.AddWithValue("@IdColonia", usuario.Direccion.Colonia.IdColonia);
                    contex.Open();

                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                        int IdDireccion = Convert.ToInt32(cmd.Parameters["@IdDireccion"].Value);
                        result.Object = IdDireccion;
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
        public static ML.Result DireccionDelete(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionDelete", contex);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdDireccion", IdDireccion);
                    contex.Open();
                    
                    int rowsAffected = cmd.ExecuteNonQuery();

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
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static ML.Result DireccionAddEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    ObjectParameter idDireccion = new ObjectParameter("IdDireccion", typeof(int));
                    var rowsAffected = contex.DireccionAdd(usuario.Direccion.Calle,usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior,usuario.Direccion.Colonia.IdColonia,idDireccion);
                    if (rowsAffected > 0)
                    {
                        usuario.Direccion.IdDireccion = (int)idDireccion.Value;
                        result.Object = usuario.Direccion.IdDireccion;
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
        public static ML.Result DireccionUpdateEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    
                    var rowsAffected = contex.DireccionUpdate(usuario.Direccion.IdDireccion,usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    if (rowsAffected > 0)
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
        public static ML.Result DireccionDeleteEFSP(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    
                    var rowsAffected = contex.DireccionDelete(IdDireccion);
                    if (rowsAffected > 0)
                    {                      
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar";
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
