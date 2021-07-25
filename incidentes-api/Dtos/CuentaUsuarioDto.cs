using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incidentes_api.Dtos
{
    public class CuentaUsuarioDto
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Token { get; set; }
    }
}
