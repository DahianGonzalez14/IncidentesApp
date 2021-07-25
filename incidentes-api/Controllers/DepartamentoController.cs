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
    [Route("api/departamento")]
    public class DepartamentoController: ControllerBase
    {
        private readonly IncidentesDBContext context;

        public DepartamentoController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>> Get()
        {
            return await context.Departamentos.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("DepartamentosActivos")]
        public async Task<ActionResult<List<Departamento>>> GetDepartamentosActivos()
        {
            return await context.Departamentos.Where(x => x.Borrado == false && x.Estatus.Equals("0")).ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Departamento departamento)
        {
            var existsDepartamento = await context.Departamentos.AnyAsync(x => x.Nombre == departamento.Nombre && x.Borrado == false);

            if (existsDepartamento)
            {
                return BadRequest("El Departamento ya existe");
            }

            departamento.FechaRegistro = DateTime.Now;
            departamento.FechaModificacion = DateTime.Now;

            context.Departamentos.Add(departamento);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(Departamento departamento)
        {
            var existsDepartamento = await context.Departamentos.AnyAsync(x => (x.Nombre == departamento.Nombre && x.DepartamentoId != departamento.DepartamentoId && x.Borrado == false));

            if (existsDepartamento)
            {
                return BadRequest("El Departamento ya existe");
            }

            departamento.FechaModificacion = DateTime.Now;
            context.Departamentos.Update(departamento);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, int idUsuarioElimina)
        {
            var departamento = await context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                return BadRequest("El Departamento no existe");
            }

            departamento.FechaModificacion = DateTime.Now;
            departamento.Borrado = true;
            departamento.ModificadoPor = idUsuarioElimina;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
