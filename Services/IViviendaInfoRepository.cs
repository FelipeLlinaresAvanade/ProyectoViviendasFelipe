using ProyectoFinal.Entities;

namespace ProyectoFinal.Services
{
    public interface IViviendaInfoRepository
    {
        Task<IEnumerable<Vivienda>> GetViviendasAsync();
        Task<(IEnumerable<Vivienda>, PaginationMetadata)> GetViviendasAsync(int pageSize, int pageNumber);
        Task<Vivienda?> GetViviendaAsync(int viviendaId, bool includeReservas);
        Task<(IEnumerable<Vivienda>, int)> GetViviendasAsync(string name);
        void AddVivienda(Vivienda vivienda);
        void DeleteVivienda(int viviendaId);
        void UpdateVivienda(Vivienda vivienda);
        Task<IEnumerable<Reserva>> GetReservasParaViviendaAsync(int viviendaId);
        Task<Reserva> GetReservaParaViviendaAsync(int viviendaId, int reservaId);
        Task<bool> ViviendaExistsAsync(int viviendaId);
        Task AddReservaParaViviendaAsync(int viviendaId, Reserva reserva);
        Task<bool> SaveChangesAsync();

        void DeleteReserva(Reserva reserva);

    }
}