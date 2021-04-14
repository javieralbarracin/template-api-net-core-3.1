using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class GastoCredito : BaseEntity
    {
        public int GastoId { get; set; }
        public virtual Gasto Gasto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? CantidadTotalCuotas { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Importe { get; set; }
    }
}
