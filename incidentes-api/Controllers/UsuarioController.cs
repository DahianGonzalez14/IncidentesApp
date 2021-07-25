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
    [Authorize]
    [Route("api/usuario")]
    public class UsuarioController: ControllerBase
    {
        private readonly IncidentesDBContext context;

        public UsuarioController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("GetUsuarioByDepartamentoId")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarioByDepartamentoId(int departamentoId)
        {
            var puestos = await context.Puestos.Where(x => x.DepartamentoId == departamentoId).Select(x => x.PuestoId).ToListAsync();
            var usuarios = await context.Usuarios.Where(x => x.Borrado == false).ToListAsync();
            var listadoRetornar = new List<Usuario>();
            foreach (var puesto in puestos)
            {
                listadoRetornar.AddRange(usuarios.Where(x => x.PuestoId == puesto));
            }
            return listadoRetornar;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            var existsUser = await context.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario && x.Borrado == false);

            if (existsUser)
            {
                return BadRequest("El Nombre de Usuario ya existe");
            }

            usuario.FechaRegistro = DateTime.Now;
            usuario.FechaModificacion = DateTime.Now;

            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(Usuario usuario)
        {
            var existsUser = await context.Usuarios.AnyAsync(x => (x.NombreUsuario == usuario.NombreUsuario && x.UsuarioId != usuario.UsuarioId && x.Borrado == false));

            if (existsUser)
            {
                return BadRequest("El Nombre de Usuario ya existe");
            }

            usuario.FechaModificacion = DateTime.Now;
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, int idUsuarioElimina)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return BadRequest("El Usuario no existe");
            }

            usuario.FechaModificacion = DateTime.Now;
            usuario.Borrado = true;
            usuario.ModificadoPor = idUsuarioElimina;
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
