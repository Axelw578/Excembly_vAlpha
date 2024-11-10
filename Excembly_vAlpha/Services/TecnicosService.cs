using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Excembly_vAlpha.Services
{
    public class TecnicosService
    {
        private readonly ExcemblyDbContext _context;

        public TecnicosService(ExcemblyDbContext context)
        {
            _context = context;
        }

        public List<TecnicoViewModel> ObtenerTodosLosTecnicos()
        {
            return _context.Set<Tecnico>()
                           .Select(t => new TecnicoViewModel
                           {
                               TecnicoId = t.TecnicoId,
                               Nombre = t.Nombre,
                               Apellidos = t.Apellidos,
                               Edad = t.Edad,
                               Experiencia = t.Experiencia,
                               Foto = t.Foto,
                               Disponibilidad = t.Disponibilidad,
                               FechaRegistro = t.FechaRegistro.ToShortDateString()
                           })
                           .ToList();
        }

    }
}
