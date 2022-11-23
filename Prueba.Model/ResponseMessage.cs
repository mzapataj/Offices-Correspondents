using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Model
{
    public class ResponseMessage<T>
    {
        public T Info { get; set; }

        public bool Success { get; set; }
    }
}
