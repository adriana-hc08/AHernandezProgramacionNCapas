using BL;
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

            ML.Result resultRol=BL.Rol.RolGetAll();

            ML.Result result = BL.Usuario.GetAll();

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

            ML.Result resultRol = BL.Rol.RolGetAll();
            usuario.Rol.Roles = resultRol.Objects;

            if (IdUsuario==0)
            {

            }else if (IdUsuario >0)
            {
                ML.Result result = BL.Usuario.GetbyId(IdUsuario.Value);
                if(result.Correct==true)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;
                }
            }          
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario, HttpPostedFileBase imgUsuarioInput)
        { 
            ML.Result result=new ML.Result();

            if(imgUsuarioInput!=null)
            {
                MemoryStream target = new MemoryStream();
                imgUsuarioInput.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                usuario.Imagen = data;

            }
           
            if (usuario.IdUsuario == 0)
            {
                
                result =BL.Usuario.AddSP(usuario);
            }
            else
            {
               result= BL.Usuario.UpdateSP(usuario);
            }
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            return View();
        }
        public ActionResult Delete(int IdUsuario)
        {
            BL.Usuario.DeleteSP(IdUsuario);
          
            return RedirectToAction("GetAll");
        }
    }
}