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
    [Route("api/incidente")]
    public class IncidenteController: ControllerBase
    {
        private readonly IncidentesDBContext context;

        public IncidenteController(IncidentesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Incidente>>> Get()
        {
            return await context.Incidentes.Where(x => x.Borrado == false).ToListAsync();
        }

        [HttpGet("GetIncidentesActivos")]
        public async Task<ActionResult<List<Incidente>>> GetIncidentesActivos()
        {
            return await context.Incidentes.Where(x => x.Borrado == false && x.Estatus == "0").ToListAsync();
        }

        [HttpGet("GetIncidentesSinAsignar")]
        public async Task<ActionResult<List<Incidente>>> GetIncidentesSinAsignar()
        {
            return await context.Incidentes.Where(x => x.Borrado == false && x.Estatus == "0" && x.UsuarioAsignadoId == null).ToListAsync();
        }

        [HttpGet("GetIncidentesAsignados")]
        public async Task<ActionResult<List<Incidente>>> GetIncidentesAsignados()
        {
            return await context.Incidentes.Where(x => x.Borrado == false && x.Estatus == "0" && x.UsuarioAsignadoId != null).ToListAsync();
        }
        [AllowAnonymous]
        [HttpGet("GetIncidentesAsignadoByUser")]
        public async Task<ActionResult<List<Incidente>>> GetIncidentesAsignadoByUser(int usuarioId)
        {
            return await context.Incidentes.Where(x => x.Borrado == false && x.UsuarioAsignadoId == usuarioId && x.Estatus == "0").ToListAsync();
        }

        [HttpGet("GetHistorialSolicitudesByUser")]
        public async Task<ActionResult<List<Incidente>>> GetHistorialSolicitudesByUser(int usuarioId)
        {
            return await context.Incidentes.Where(x => x.Borrado == false && x.UsuarioReportaId == usuarioId).ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Incidente incidente)
        {
            var existsIncidente = await context.Incidentes.AnyAsync(x => (x.Titulo == incidente.Titulo && x.DepartamentoId == incidente.DepartamentoId && x.Borrado == false));

            if (existsIncidente)
            {
                return BadRequest("El Incidente ya existe");
            }

            incidente.FechaRegistro = DateTime.Now;
            incidente.FechaModificacion = DateTime.Now;

            context.Incidentes.Add(incidente);
            await context.SaveChangesAsync();
            CrearHistorico(incidente, "Incidente Creado");
            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(Incidente incidente)
        {
            var existsIncidente = await context.Incidentes.AnyAsync(x => (x.Titulo == incidente.Titulo && x.IncidenteId != incidente.IncidenteId && x.DepartamentoId == incidente.DepartamentoId && x.Borrado == false));

            if (existsIncidente)
            {
                return BadRequest("El Incidente ya existe");
            }

            incidente.FechaModificacion = DateTime.Now;
            context.Incidentes.Update(incidente);
            await context.SaveChangesAsync();
            CrearHistorico(incidente, "Incidente Modificado");
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, int idUsuarioElimina)
        {
            var incidente = await context.Incidentes.FindAsync(id);

            if (incidente == null)
            {
                return BadRequest("El Incidente no existe");
            }

            incidente.FechaModificacion = DateTime.Now;
            incidente.Borrado = true;
            incidente.ModificadoPor = idUsuarioElimina;
            context.Incidentes.Update(incidente);
            await context.SaveChangesAsync();
            CrearHistorico(incidente, "Incidente Eliminado");
            return Ok();
        }

        [HttpPut("AsignarIncidente")]
        public async Task<ActionResult> AsignarIncidente(Incidente incidente)
        {
            var incidenteActualizar = await context.Incidentes.FindAsync(incidente.IncidenteId);

            if (incidente == null)
            {
                return BadRequest("El Incidente no existe");
            }

            incidenteActualizar.FechaModificacion = DateTime.Now;
            incidenteActualizar.ModificadoPor = incidente.ModificadoPor;
            incidenteActualizar.UsuarioAsignadoId = incidente.UsuarioAsignadoId;
            context.Incidentes.Update(incidenteActualizar);
            await context.SaveChangesAsync();
            CrearHistorico(incidenteActualizar, "Incidente Asignado");
            return Ok();
        }

        [HttpPut("CerrarIncidente")]
        public async Task<ActionResult> CerrarIncidente(Incidente incidente)
        {
            var incidenteActualizar = await context.Incidentes.FindAsync(incidente.IncidenteId);

            if (incidenteActualizar == null)
            {
                return BadRequest("El Incidente no existe");
            }

            incidenteActualizar.FechaModificacion = DateTime.Now;
            incidenteActualizar.ModificadoPor = incidente.ModificadoPor;
            incidenteActualizar.FechaCierre = DateTime.Now;
            incidenteActualizar.ComentarioCierre = incidente.ComentarioCierre;
            incidenteActualizar.Estatus = "1";
            context.Incidentes.Update(incidenteActualizar);
            await context.SaveChangesAsync();
            CrearHistorico(incidenteActualizar, "Incidente Cerrado");
            return Ok();
        }

        private  void CrearHistorico(Incidente incidente, string comentario)
        {
            HistorialIncidente historicoIncidente = new HistorialIncidente();
            historicoIncidente.IncidenteId = incidente.IncidenteId;
            historicoIncidente.Comentario = comentario;
            historicoIncidente.Estatus = "0";
            historicoIncidente.Borrado = false;
            historicoIncidente.FechaRegistro = DateTime.Now;
            historicoIncidente.FechaModificacion = DateTime.Now;
            historicoIncidente.CreadoPor = incidente.CreadoPor;
            historicoIncidente.ModificadoPor = incidente.ModificadoPor;

            context.HistorialIncidentes.Add(historicoIncidente);
            context.SaveChanges();
        }

        


    }
}
