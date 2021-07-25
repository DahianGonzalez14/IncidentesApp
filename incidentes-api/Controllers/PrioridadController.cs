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
    [Route("api/prioridad")]
    public class PrioridadController: ControllerBase
    {
        private readonly IncidentesDBContext context;

        public PrioridadController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Prioridad>>> Get()
        {
            return await context.Prioridads.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("PrioridadesActivas")]
        public async Task<ActionResult<List<Prioridad>>> GetPrioridadesActivas()
        {
            return await context.Prioridads.Where(x => x.Borrado == false && x.Estatus.Equals("0")).ToListAsync();
        }


        [HttpPost("Create")]
        public async Task<ActionResult> Create(Prioridad prioridad)
        {
            var existsPrioridad = await context.Prioridads.AnyAsync(x => x.Nombre == prioridad.Nombre && x.Borrado == false);

            if (existsPrioridad)
            {
                return BadRequest("La Prioridad ya existe");
            }

            prioridad.FechaRegistro = DateTime.Now;
            prioridad.FechaModificacion = DateTime.Now;

            context.Prioridads.Add(prioridad);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(Prioridad prioridad)
        {
            var existsPrioridad = await context.Prioridads.AnyAsync(x => (x.Nombre == prioridad.Nombre && x.PrioridadId != prioridad.PrioridadId && x.Borrado == false));

            if (existsPrioridad)
            {
                return BadRequest("La Prioridad ya existe");
            }

            prioridad.FechaModificacion = DateTime.Now;
            context.Prioridads.Update(prioridad);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, int idUsuarioElimina)
        {
            var prioridad = await context.Prioridads.FindAsync(id);

            if (prioridad == null)
            {
                return BadRequest("La Prioridad no existe");
            }

            prioridad.FechaModificacion = DateTime.Now;
            prioridad.Borrado = true;
            prioridad.ModificadoPor = idUsuarioElimina;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
