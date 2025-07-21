using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using System.Runtime.Remoting.Contexts;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {

                    SqlCommand cmd = new SqlCommand("ColoniaGetByIdMunicipio", contex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMunicipio", IdMunicipio);
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Colonia colonia = new ML.Colonia();                            
                            colonia.IdColonia = Convert.ToInt32(row[0].ToString());                                                       
                            colonia.Nombre = (row[1].ToString());
                            result.Objects.Add(colonia);
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
        public static ML.Result GetByIdMunicipioEFSP(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var listaColonias = contex.ColoniaGetByIdMunicipio(IdMunicipio).ToList();
                    if (listaColonias.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var coloniaDB in listaColonias)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = coloniaDB.IdColonia;
                            colonia.Nombre = coloniaDB.Nombre;
                            result.Objects.Add(colonia);
                        }
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
        public static ML.Result GetByIdMunicipioLinQ(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var coloniabyId = (from coloniaDB in contex.Colonias

                                       where coloniaDB.IdMunicipio == IdMunicipio
                                       select new
                                       {
                                           IdColonia = coloniaDB.IdColonia,
                                           Nombre = coloniaDB.Nombre,

                                       });
                    result.Objects = new List<object>();
                    if (coloniabyId != null && coloniabyId.ToList().Count > 0)
                    {                        
                        foreach (var obj in coloniabyId)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;

                            result.Object = colonia;
                        }                       
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
    }
}
