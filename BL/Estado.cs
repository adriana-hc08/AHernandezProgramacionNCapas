using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using DL_EF;
using System.Runtime.Remoting.Contexts;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("EstadoGetAll", contex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    contex.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = Convert.ToInt32(row[0].ToString());
                            estado.Nombre = row[1].ToString();
                                              
                            result.Objects.Add(estado);
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
        public static ML.Result GetAllEFSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AHernandezProgramacionNCapasEntities contex = new DL_EF.AHernandezProgramacionNCapasEntities())
                {
                    var listaEstados = contex.EstadoGetAll().ToList();
                    if (listaEstados.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var estadoDB in listaEstados)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = estadoDB.IdEstado;
                            estado.Nombre = estadoDB.Nombre;
                            result.Objects.Add(estado);
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
            }
            return result;
        }
        public static ML.Result GetAllLINQ()
        {
            Result result = new ML.Result();
            try
            {
                using (AHernandezProgramacionNCapasEntities contex = new AHernandezProgramacionNCapasEntities())
                {
                    var listEstados = (from estadoDB in contex.Estadoes
                                      
                                        select new
                                        {
                                            IdEstado = estadoDB.IdEstado,
                                            Nombre = estadoDB.Nombre
                                            
                                        });
                    result.Objects = new List<object>();
                    if (listEstados != null && listEstados.ToList().Count > 0)
                    {
                        foreach (var obj in listEstados)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre= obj.Nombre;

                            result.Objects.Add(estado);
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
    }
}

