using incidentes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incidentes_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
