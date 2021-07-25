using System;
using System.Collections.Generic;

#nullable disable

namespace incidentes_api.Models
{
    public partial class Incidente
    {
        public int IncidenteId { get; set; }
        public int? UsuarioReportaId { get; set; }
        public int? UsuarioAsignadoId { get; set; }
        public int? PrioridadId { get; set; }
        public int? DepartamentoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTimeOffset? FechaCierre { get; set; }
        public string ComentarioCierre { get; set; }
        public string Estatus { get; set; }
        public bool? Borrado { get; set; }
        public DateTimeOffset? FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }
    }
}
