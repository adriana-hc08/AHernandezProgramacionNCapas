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
        public static void update()
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

            if(result.Correct){

                Console.WriteLine("Usuario actualizado");
            }
            else
            {
                Console.WriteLine("Error al actualizar");
            }
            Console.ReadKey();
        }

        public static void delete()
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
    }
}
