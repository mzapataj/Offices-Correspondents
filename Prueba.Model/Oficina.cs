using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public partial class Oficina
    {
        [Display(Name = "Id")]
        public long OfiId { get; set; }
        [Display(Name = "Id Corresponsal")]
        public long OfiCorresponsalId { get; set; }
        
        [Display(Name = "Nombre Oficina")]
        [Required(ErrorMessage = "El campo 'Nombre' es obligario")]
        public string OfiNombre { get; set; } = null!;

        [Display(Name = "Id Corresponsal")]
        public virtual Corresponsal? OfiCorresponsal { get; set; } = null!;
    }
}
