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
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol=new ML.Rol();
            //ML.Result resultDirec=BL.Direccion.Di

            ML.Result resultRol=BL.Rol.RolGetAllEFSP();

            ML.Result result = BL.Usuario.GetAllEFSP();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
         
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
            ML.Result resultEstados = BL.Estado.GetAllEFSP();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

            ML.Result resultRol = BL.Rol.RolGetAllEFSP();
            usuario.Rol.Roles = resultRol.Objects;
                    

            if (IdUsuario==0)
            {                             
            }
            else if (IdUsuario >0)
            {
                ML.Result result = BL.Usuario.GetByIdEFSP(IdUsuario.Value);
                if(result.Correct==true)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;                    
                }
                
                if (resultEstados.Correct == true)
                {
                    
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                }
                ML.Result resultMunicipios = BL.Municipio.GetByIdEstadoEFSP(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                ML.Result resultColonias = BL.Colonia.GetByIdMunicipioEFSP(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias= resultColonias.Objects;
            }          
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario, HttpPostedFileBase imgUsuarioInput)
        { 
            ML.Result result=new ML.Result();
            ML.Direccion direccion= new ML.Direccion();

            if(imgUsuarioInput!=null)
            {
                MemoryStream target = new MemoryStream();
                imgUsuarioInput.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                usuario.Imagen = data;

            }
            if (usuario.Direccion.IdDireccion > 0)
            {
                ML.Result resultDirecc = BL.Direccion.DireccionUpdateEFSP(usuario);
                usuario.Direccion.IdDireccion = (int)resultDirecc.Object;
            }
            else
            {
                ML.Result resultDirec= BL.Direccion.DireccionAddEFSP(usuario);
                if (resultDirec.Correct)
                {
                    usuario.Direccion.IdDireccion=Convert.ToInt32(resultDirec.Object);
                }
            }
         
            if (usuario.IdUsuario == 0)
            {
                
                result = BL.Usuario.AddEFSP(usuario);
            }
            else
            {
                //DateTime
                result = BL.Usuario.UpdateEFSP(usuario);
            }
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            return View();
        }
        public ActionResult Delete(int IdDireccion)
        {
            ML.Result result = BL.Usuario.DeleteEFSP(IdDireccion);
            if (result.Correct)
            {
                
                if(IdDireccion > 0)
                {
                    ML.Result resultdirec=BL.Direccion.DireccionDeleteEFSP(IdDireccion);
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
            ML.Result resultEstados = BL.Estado.GetAll();

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
    }
}