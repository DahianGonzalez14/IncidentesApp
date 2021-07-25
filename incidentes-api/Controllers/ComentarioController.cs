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
    [Route("api/comentario")]
    public class ComentarioController: ControllerBase
    {
        private readonly IncidentesDBContext context;

        public ComentarioController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comentario>>> Get()
        {
            return await context.Comentarios.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("GetComentarioByIncidenteId")]
        public async Task<ActionResult<List<Comentario>>> GetComentarioByIncidenteId(int incidenteId)
        {
            return await context.Comentarios.Where(x => x.Borrado == false && x.IncidenteId == incidenteId).ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Comentario comentario)
        {
            var existsComentario = await context.Comentarios.AnyAsync(x => x.IncidenteId == comentario.IncidenteId && x.UsuarioComentaId == comentario.UsuarioComentaId &&
                                                                        x.Descripcion == comentario.Descripcion && x.Borrado == false);

            if (existsComentario)
            {
                return BadRequest("El Comentario ya existe");
            }

            comentario.FechaRegistro = DateTime.Now;
            comentario.FechaModificacion = DateTime.Now;

            context.Comentarios.Add(comentario);
            await context.SaveChangesAsync();

            return Ok();
        }


    }
}
