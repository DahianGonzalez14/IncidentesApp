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
    [Route("api/puesto")]
    public class PuestoController: ControllerBase
    {
        private readonly IncidentesDBContext context;

        public PuestoController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Puesto>>> Get()
        {
            return await context.Puestos.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("PuestosActivos")]
        public async Task<ActionResult<List<Puesto>>> GetPuestosActivos()
        {
            return await context.Puestos.Where(x => x.Borrado == false && x.Estatus.Equals("0")).ToListAsync();
        }


        [HttpPost("Create")]
        public async Task<ActionResult> Create(Puesto puesto)
        {
            var existsPuesto = await context.Puestos.AnyAsync(x => x.Nombre == puesto.Nombre && x.Borrado == false);

            if (existsPuesto)
            {
                return BadRequest("El Puesto ya existe");
            }

            puesto.FechaRegistro = DateTime.Now;
            puesto.FechaModificacion = DateTime.Now;

            context.Puestos.Add(puesto);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(Puesto puesto)
        {
            var existsPuesto = await context.Puestos.AnyAsync(x => (x.Nombre == puesto.Nombre && x.PuestoId != puesto.PuestoId && x.Borrado == false));

            if (existsPuesto)
            {
                return BadRequest("El Puesto ya existe");
            }

            puesto.FechaModificacion = DateTime.Now;
            context.Puestos.Update(puesto);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, int idUsuarioElimina)
        {
            var puesto = await context.Puestos.FindAsync(id);

            if (puesto == null)
            {
                return BadRequest("El Puesto no existe");
            }

            puesto.FechaModificacion = DateTime.Now;
            puesto.Borrado = true;
            puesto.ModificadoPor = idUsuarioElimina;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
