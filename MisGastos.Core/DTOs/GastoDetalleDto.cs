using MisGastos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.DTOs
{
    public class GastoDetalleDto
    {
        public decimal ImporteTotal { get; set; }
        public string Descripcion { get; set; }
        public string Frecuencia { get; set; }
        public string Mes { get; set; }
        public string GastoTipo { get; set; }
        public string Tipo { get; set; }
        public List<GastoCredito> Creditos { get; set; }
    }
}
