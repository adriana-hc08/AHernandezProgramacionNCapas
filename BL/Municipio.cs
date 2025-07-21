using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using DL_EF;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("MunicipioGetByIdEstado", contex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEstado", IdEstado);
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Municipio municipio= new ML.Municipio();
                            municipio.IdMunicipio = Convert.ToInt32(row[0].ToString());                            
                            municipio.Nombre = (row[1].ToString());
                            result.Objects.Add(municipio);
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
        public static ML.Result GetByIdEstadoEFSP(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var listaMunicipios = contex.MunicipioGetByIdEstado(IdEstado).ToList();
                    if (listaMunicipios.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipiosDB in listaMunicipios)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio= municipiosDB.IdMunicipio;
                            municipio.Nombre = municipiosDB.Nombre;
                            result.Objects.Add(municipio);
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
        public static ML.Result GetByIdEstadoLinQ(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var municipiobyId = (from municipioDB in contex.Municipios

                                         where municipioDB.IdEstado == IdEstado
                                         select new
                                         {
                                             IdMunicipio = municipioDB.IdMunicipio,
                                             Nombre = municipioDB.Nombre,

                                         });
                    result.Objects = new List<object>();
                    if (municipiobyId != null && municipiobyId.ToList().Count > 0)
                    {   
                        foreach(var obj in municipiobyId)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre;
                            result.Objects.Add(municipio);
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

