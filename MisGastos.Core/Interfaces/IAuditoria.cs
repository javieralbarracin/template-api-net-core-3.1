using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Interfaces
{
    public interface IAuditoria
    {
        string CreadoPor { get; set; }
        DateTime CreadoFecha { get; set; }
        string ModificadoPor { get; set; }
        DateTime? ModificadoFecha { get; set; }
        string EliminadoPor { get; set; }
        DateTime? EliminadoFecha { get; set; }
        bool Eliminado { get; set; }
    }
}
