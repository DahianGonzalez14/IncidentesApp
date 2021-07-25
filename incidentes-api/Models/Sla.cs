using System;
using System.Collections.Generic;

#nullable disable

namespace incidentes_api.Models
{
    public partial class Sla
    {
        public int SlaId { get; set; }
        public string Descripcion { get; set; }
        public int? CantidadHoras { get; set; }
        public string Estatus { get; set; }
        public bool? Borrado { get; set; }
        public DateTimeOffset? FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }
    }
}
