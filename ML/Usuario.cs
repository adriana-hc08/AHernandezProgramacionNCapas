using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚñÑüÜ ]+$", ErrorMessage = "Solo se aceptan letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido Paterno es obligatorio")]
        //[RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚñÑüÜ ]+$", ErrorMessage = "Solo se aceptan letras")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        //[RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚñÑüÜ ]+$", ErrorMessage = "Solo se aceptan letras")]
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El Nombre de usuario es obligatorio")]
        //[RegularExpression(@"^[A-Z](?=.*\d)[a-zA-Z0-9]{7}$")]
        [Display(Name ="Nombre de usuario")]
        public string UserName { get; set; }

        public ML.Rol Rol { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Ingrese un Email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        //[RegularExpression(@"^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$", ErrorMessage = "Contraseña incorrecta")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Eliga una opcion")]
        public char Sexo { get; set; }

        [Required(ErrorMessage = "El numero telefono es requerido")]
        //[RegularExpression(@"^[0-9]{2}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Solo se aceptan Números")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El numero celular es requerido")]
        //[RegularExpression(@"^[0-9]{2}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Solo se aceptan Números")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public Boolean Estatus { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        //[RegularExpression("^([A-Z][AEIOUX][A-Z]{2}\\d{2}(?:0\\d|1[0-2])(?:[0-2]\\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\\d])(\\d)$")]
        public string CURP { get; set; }
        
        public Byte[] Imagen { get; set; }
        public ML.Direccion Direccion { get; set; } 
        public List<object> Usuarios { get; set; }

    }
}
