using System;
using System.Collections.Generic;

#nullable disable

namespace incidentes_api.Models
{
    public partial class Usuario
    {
        public int UsuarioId { get; set; }
        public int? PuestoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Estatus { get; set; }
        public bool? Borrado { get; set; }
        public DateTimeOffset? FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }
    }
}
