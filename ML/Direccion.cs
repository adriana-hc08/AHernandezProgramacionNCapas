using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Numero Interior ")]
        public string NumeroInterior { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Numero Exterior ")]
        public string NumeroExterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public List<object> Direcciones { get; set; }
    }
}
