using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Interfaces
{
    public interface ISoftDeleted
    {
        //[ScaffoldColumn(false)]
        //DateTime? EliminadoFecha { get; set; }
        DateTime? FechaEliminacion { get; set; }
        //[StringLength(maximumLength: 128)]
        //[ScaffoldColumn(false)]
        //string EliminadoPor { get; set; }
        //[ScaffoldColumn(false)]
        bool Eliminado { get; set; }
    }
}
