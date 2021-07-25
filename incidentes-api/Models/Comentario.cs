using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incidentes_api.Models
{
    public partial class Comentario
    {
        public int ComentarioId { get; set; }
        public int IncidenteId { get; set; }
        public int UsuarioComentaId { get; set; }
        public string Descripcion { get; set; }
        public string Estatus { get; set; }
        public bool? Borrado { get; set; }
        public DateTimeOffset? FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }
    }
}
