using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.DTOs
{
    public class GastoCreacionDto
    {
        public decimal Importe { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Descripcion { get; set; }
        public int FrecuenciaId { get; set; }
        public int GastoTipoId { get; set; }
        public int MesId { get; set; }
    }
}
