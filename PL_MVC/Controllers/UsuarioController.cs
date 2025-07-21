using BL;
using ML;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol=new ML.Rol();
            usuario.Rol.IdRol = 0;
           

            ML.Result resultRol=BL.Rol.RolGetAllEFSP();
            if (resultRol.Correct == true)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            else
            {
                usuario.Rol.Roles = new List<object>();
            }
            

            ML.Result result = BL.Usuario.GetAllEFSP(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
         
            return View(usuario);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;

            ML.Result resultRol = BL.Rol.RolGetAllEFSP();
            ML.Result resultUsuario=BL.Usuario.GetAllEFSP(usuario.Nombre,usuario.ApellidoPaterno,usuario.ApellidoMaterno,usuario.Rol.IdRol);

            if (resultRol.Correct == true)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            else
            {
                usuario.Rol.Roles = new List<object>();
            }
            usuario.Usuarios=resultUsuario.Objects;
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Formulario(int? IdUsuario)
        {
            ML.Usuario usuario=new ML.Usuario();
            usuario.Rol=new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            ML.Result resultEstados = BL.Estado.GetAllLINQ();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

            ML.Result resultRol = BL.Rol.GetAllRolLINQ();
            usuario.Rol.Roles = resultRol.Objects;
                    

            if (IdUsuario==0)
            {                             
            }
            else if (IdUsuario >0)
            {
                ML.Result result = BL.Usuario.GetByIdEFLinq(IdUsuario.Value);
                if(result.Correct==true)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;                    
                }
                
                if (resultEstados.Correct == true)
                {
                    
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                }
                ML.Result resultMunicipios = BL.Municipio.GetByIdEstadoLinQ(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                ML.Result resultColonias = BL.Colonia.GetByIdMunicipioLinQ(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias= resultColonias.Objects;
            }          
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario, HttpPostedFileBase imgUsuarioInput)
        {
            if (ModelState.IsValid)
            {
                ML.Result result = new ML.Result();
                ML.Direccion direccion = new ML.Direccion();
                ML.Result resultUsername = BL.Usuario.GetByIdUsernameEFLinq(usuario.Nombre);
                ML.Result resultEmail = BL.Usuario.GetByIdEmailEFLinq(usuario.Email);
                ML.Result resultCurp = BL.Usuario.GetByIdCURPEFLinq(usuario.CURP);

                if (usuario.IdUsuario == 0)
                {
                    if (resultUsername.Correct)
                    {
                        ViewBag.Error = "El Nombre de usuario ya se encunetra registrado";
                    }
                    if (resultCurp.Correct)
                    {
                        ViewBag.Error = "La CURP ya se encunetra registrada";
                    }
                    if (resultEmail.Correct)
                    {
                        ViewBag.Error = "El Email ya se encunetra registrado";
                    }
                }
                else
                {
                    usuario.Direccion.Colonia.Colonias = new List<Object>();
                    usuario.Direccion.Colonia.Municipio.Municipios = new List<Object>();
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = new List<Object>();
                    return View(usuario);
                }

               
                if (imgUsuarioInput != null)
                {
                    MemoryStream target = new MemoryStream();
                    imgUsuarioInput.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    usuario.Imagen = data;

                }
                if (usuario.Direccion.IdDireccion > 0)
                {
                    ML.Result resultDirecc = BL.Direccion.UpdateLINQ(usuario);
                    usuario.Direccion.IdDireccion = (int)resultDirecc.Object;
                }
                else
                {
                    ML.Result resultDirec = BL.Direccion.AddLINQ(usuario);
                    if (resultDirec.Correct)
                    {
                        usuario.Direccion.IdDireccion = Convert.ToInt32(resultDirec.Object);
                    }
                }

                if (usuario.IdUsuario == 0)
                {

                    result = BL.Usuario.AddLINQ(usuario);
                }
                else
                {
                    //DateTime
                    result = BL.Usuario.UpdateLINQ(usuario);
                }
                if (result.Correct)
                {
                    return RedirectToAction("GetAll");
                }

            }
            else
            {
                //llenar los ddl
                
                usuario.Direccion.Colonia.Colonias=new List<Object>();
                usuario.Direccion.Colonia.Municipio.Municipios = new List<Object>();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = new List<Object>();
                return View(usuario);
            }
            
            return View();
        }
        public ActionResult Delete(int IdDireccion)
        {
            ML.Result result = BL.Usuario.DeleteLINQ(IdDireccion);
            if (result.Correct)
            {
                
                if(IdDireccion > 0)
                {
                    ML.Result resultdirec=BL.Direccion.DeleteLINQ(IdDireccion);
                }
                //CScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Eata seguro de eliminar este usuario')", true);
                return RedirectToAction("GetAll");
            }
            else
            {
                ViewBag.ErrorMesagge = "No se pudo eliminar";
                return View("Error");
            }
                                
        }

        public ActionResult DropDownList()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultEstados = BL.Estado.GetAllLINQ();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            if (resultEstados.Correct==true)
            {             
                usuario.Direccion.Colonia.Municipio.Estado.Estados=resultEstados.Objects;
            }
            return View(usuario);
        }
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult UpdateEstatus(int IdUsuario,bool Estatus)
        {
            ML.Result result = BL.Usuario.UpdateEstatusEFSP(IdUsuario,Estatus);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}