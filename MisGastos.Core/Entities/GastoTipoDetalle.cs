using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class GastoTipoDetalle: BaseEntity
    {
        public int GastoTipoId { get; set; }
        public GastoTipo GastoTipo { get; set; }
        public string Descripcion { get; set; }
    }
}
