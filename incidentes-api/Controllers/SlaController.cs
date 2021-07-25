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
    [Route("api/sla")]
    public class SlaController: ControllerBase
    {
        private readonly IncidentesDBContext context;


        public SlaController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sla>>> Get()
        {
            return await context.Slas.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("SlaActivos")]
        public async Task<ActionResult<List<Sla>>> GetSlaActivos()
        {
            return await context.Slas.Where(x => x.Borrado == false && x.Estatus.Equals("0")).ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Sla sla)
        {
            var existsSla = await context.Slas.AnyAsync(x => x.Descripcion == sla.Descripcion && x.Borrado == false);

            if (existsSla)
            {
                return BadRequest("El Sla ya existe");
            }

            sla.FechaRegistro = DateTime.Now;
            sla.FechaModificacion = DateTime.Now;

            context.Slas.Add(sla);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(Sla sla)
        {
            var existsSla = await context.Slas.AnyAsync(x => (x.Descripcion == sla.Descripcion && x.SlaId != sla.SlaId && x.Borrado == false));

            if (existsSla)
            {
                return BadRequest("El Sla ya existe");
            }

            sla.FechaModificacion = DateTime.Now;
            context.Slas.Update(sla);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, int idUsuarioElimina)
        {
            var sla = await context.Slas.FindAsync(id);

            if (sla == null)
            {
                return BadRequest("El Sla no existe");
            }

            sla.FechaModificacion = DateTime.Now;
            sla.Borrado = true;
            sla.ModificadoPor = idUsuarioElimina;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
