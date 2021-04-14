using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.Entities
{
    public class LogFrontend : BaseEntity
    {
        public string Message { get; set; }
        public string Level { get; set; }
        public string Timestamp { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }
    }
}
