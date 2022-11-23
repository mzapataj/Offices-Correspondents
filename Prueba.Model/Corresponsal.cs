using Prueba.Model.JsonConverter;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    [JsonConverter(typeof(CorresponsalConverter))]
    public partial class Corresponsal
    {
        public Corresponsal()
        {
            Oficinas = new HashSet<Oficina>();
        }
        [Display(Name = "Id")]
        public long CorCorresponsalId { get; set; }

        [Display(Name = "Nombre Corresponsal")]
        [Required(ErrorMessage = "El campo 'Nombre' es obligario")]
        public string CorNombre { get; set; } = null!;   
        public virtual ICollection<Oficina> Oficinas { get; set; }
    }
}
