using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Model.Dao
{
    public class OficinaCreate
    {
        public long OfiId { get; set; }
        public long OfiCorresponsalId { get; set; }
        public string OfiNombre { get; set; } = null!;
    }
}
