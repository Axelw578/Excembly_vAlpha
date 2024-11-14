using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class PoliticaService
    {
        private readonly ExcemblyDbContext _context;
        private readonly ILogger<PoliticaService> _logger;

        public PoliticaService(ExcemblyDbContext context, ILogger<PoliticaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtiene las políticas de cancelación y reembolso de forma asíncrona
        public async Task<List<PoliticaPrivacidad>> ObtenerPoliticasAsync()
        {
            try
            {
                return await _context.PoliticasPrivacidad
                    .Where(p => p.Titulo == "Política de Cancelación" || p.Titulo == "Política de Reembolso")
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener políticas de cancelación y reembolso");
                return new List<PoliticaPrivacidad>();
            }
        }
    }
}
