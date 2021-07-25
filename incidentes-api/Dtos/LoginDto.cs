using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incidentes_api.Dtos
{
    public class LoginDto
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
    }
}
