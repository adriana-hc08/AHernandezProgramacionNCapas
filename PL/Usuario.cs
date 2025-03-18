using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   
    public class Usuario
    {
       
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingresa tu nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingresa tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("ingresa tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el id del rol que tiene");
            usuario.Rol=new ML.Rol();
            usuario.Rol.IdRol=Convert.ToByte(Console.ReadLine());   

            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Usuario registrado");
            }
            else
            {
                Console.WriteLine("Ocurrio un error: " + result.ErrorMessage);
            }
            Console.ReadKey();
        }
        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("ingresa el id de la comlumna a actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingresa el nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("ingresa tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("ingresa tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();


            ML.Result result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {

                Console.WriteLine("Usuario actualizado");
            }
            else
            {
                Console.WriteLine("Error al actualizar");
            }
            Console.ReadKey();
        }

        public static void Delete()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("ingresa el id del usuario a eliminar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {

                Console.WriteLine("Usuario eliminado");
            }
            else
            {
                Console.WriteLine("Error al eliminar");
            }
            Console.ReadKey();
        }

        //Metodos con stored procedures
        public static void AddSP()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingresa tu nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingresa tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("ingresa tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el id del rol que tiene");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToByte(Console.ReadLine());

            ML.Result result = BL.Usuario.AddSP(usuario);          

            if (result.Correct)
            {
                Console.WriteLine("Usuario registrado");
            }
            else
            {
                Console.WriteLine("Ocurrio un error: " + result.ErrorMessage);
            }
            Console.ReadKey();
        }
        public static void UpdateSP()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("ingresa el id de la comlumna a actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingresa el nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("ingresa tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("ingresa tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el id del rol que tiene");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToByte(Console.ReadLine());

            ML.Result result = BL.Usuario.UpdateSP(usuario);

            if(result.Correct){

                Console.WriteLine("Usuario actualizado");
            }
            else
            {
                Console.WriteLine("Error al actualizar");
            }
            Console.ReadKey();
        }

        public static void DeleteSP()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("ingresa el id del usuario a eliminar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.DeleteSP(usuario.IdUsuario);

            if (result.Correct)
            {

                Console.WriteLine("Usuario eliminado");
            }
            else
            {
                Console.WriteLine("Error al eliminar");
            }
            Console.ReadKey();
        }
        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("IdUsuario:" + usuario.IdUsuario);
                    Console.WriteLine("Nombre:" + usuario.Nombre);
                    Console.WriteLine("ApellidoPaterno:" + usuario.ApellidoPaterno);
                    Console.WriteLine("ApellidoMaterno:" + usuario.ApellidoMaterno);
                    
                    Console.WriteLine("Rol:" + usuario.Rol.IdRol);
                    

                }
                result.Correct = true;
            }
            else
            {
                Console.WriteLine("ocurrio un error" + result.ErrorMessage);
            }
            Console.ReadKey();
        }
        public static void GetbyId()
        {
            

            Console.WriteLine("ingresa el id del usuario a visualizar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

           ML.Result result = BL.Usuario.GetbyId(IdUsuario);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                Console.WriteLine("Nombre:" + usuario.Nombre);
                    Console.WriteLine("ApellidoPaterno:" + usuario.ApellidoPaterno);
                    Console.WriteLine("ApellidoMaterno:" + usuario.ApellidoMaterno);
                    Console.WriteLine("Rol:" + usuario.Rol.IdRol);
                
                result.Correct = true;

            }
            else
            {
                Console.WriteLine("ocurrio un error" + result.ErrorMessage);
            }
            Console.ReadKey();
        }
    }
}
