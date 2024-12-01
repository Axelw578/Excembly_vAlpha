using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Services
{
    public interface IComentarioAdminService
    {
        Task<List<ComentarioAdminViewModel>> ObtenerTodosComentariosAsync();
        Task<List<ComentarioAdminViewModel>> ObtenerComentariosFiltradosAsync(string ordenPor = "FechaComentario", bool ascendente = true);
        Task<ComentarioAdminViewModel> ObtenerComentarioPorIdAsync(int comentarioId);
        Task<ComentarioDetalleAdminViewModel> ObtenerDetalleComentarioAsync(int comentarioId);
    }
}
