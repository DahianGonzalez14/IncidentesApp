using incidentes_api.Dtos;
using incidentes_api.Interfaces;
using incidentes_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incidentes_api.Controllers
{
    [ApiController]
    [Route("api/cuenta")]
    public class CuentaController: ControllerBase
    {
        private readonly IncidentesDBContext context;
        private readonly ITokenService tokenService;

        public CuentaController(IncidentesDBContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<CuentaUsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await context.Usuarios.SingleOrDefaultAsync(x => x.NombreUsuario == loginDto.NombreUsuario && x.Borrado == false && x.Estatus.Equals("0"));

            if (usuario == null) return Unauthorized("Usuario Incorrecto");

            if (usuario.Contrasena != loginDto.Contrasena) return Unauthorized("Contraseña Incorrecta");

            return new CuentaUsuarioDto
            {
                UsuarioId = usuario.UsuarioId,
                NombreUsuario = usuario.NombreUsuario,
                Token = tokenService.CreateToken(usuario)
            };
    }
    }
}


