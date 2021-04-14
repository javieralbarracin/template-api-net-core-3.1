using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class Frecuencia : BaseEntity
    {
        public string Descripcion { get; set; }
        public int Valor { get; set; }
    }
}
