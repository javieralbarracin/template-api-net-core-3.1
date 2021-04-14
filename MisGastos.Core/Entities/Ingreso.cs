using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class Ingreso:BaseEntity
    {
        public string Descripcion { get; set; }
        public IngresoTipo IngresoTipo { get; set; }
    }
}
