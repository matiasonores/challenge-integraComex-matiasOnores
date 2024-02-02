using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace challenge_integraComex_matiasOnores.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El CUIT es requerido.")]
        
        public long CUIT { get; set; }

        [Required(ErrorMessage = "La razón social es requerida.")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono debe ser numérico.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es requerida.")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo Activo es requerido.")]
        public bool Activo { get; set; }
    }
}