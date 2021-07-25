using System;
using System.Collections.Generic;

#nullable disable

namespace incidentes_api.Models
{
    public partial class Puesto
    {
        public int PuestoId { get; set; }
        public int? DepartamentoId { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public bool? Borrado { get; set; }
        public DateTimeOffset? FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }
    }
}
