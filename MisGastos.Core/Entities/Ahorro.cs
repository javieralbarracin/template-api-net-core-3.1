using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class Ahorro:BaseEntity
    {
        public string Descripcion { get; set; }
        public AhorroTipo AhorroTipo { get; set; }
    }
}
