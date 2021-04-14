using MisGastos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class Gasto:BaseEntity,IAuditoria
    {
        public int FrecuenciaId { get; set; }
        public Frecuencia Frecuencia { get; set; }
        public int GastoTipoId { get; set; }
        public GastoTipo GastoTipo { get; set; }
        public int MesId { get; set; }
        public Mes Mes { get; set; }
        //public DateTime FechaVencimiento { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ImporteTotal { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public List<GastoCredito> GastosCreditos { get; set; }

        #region campos de auditoria
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoFecha { get; set; }
        public string EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }
        public bool Eliminado { get; set; }
        #endregion
    }
}
