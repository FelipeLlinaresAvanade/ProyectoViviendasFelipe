using Microsoft.EntityFrameworkCore;
using ProyectoViviendasFelipe.DbContexts;
using ProyectoViviendasFelipe.Entities;

namespace ProyectoViviendasFelipe.Services
{
    
        public class ViviendaInfoRepository : IViviendaInfoRepository
        {
            private readonly ViviendaInfoContext _context;

            public ViviendaInfoRepository(ViviendaInfoContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<(IEnumerable<Vivienda>, PaginationMetadata)> GetViviendasAsync(int pageNumber, int pageSize)
            {
                var collection = _context.Viviendas as IQueryable<Vivienda>;

                var totalItemCount = await collection.CountAsync();
                var paginationMetadata = new PaginationMetadata(
                   totalItemCount, pageSize, pageNumber);

                var collectionToReturn = await collection.OrderBy(c => c.Name)
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync();

                return (collectionToReturn, paginationMetadata);
            }

            public async Task<IEnumerable<Vivienda>> GetViviendasAsync()
            {
                return await _context.Viviendas.OrderBy(c => c.Name).ToListAsync();
            }

            public async Task<Vivienda?> GetViviendaAsync(int viviendaId, bool includeReservas)
            {
                if (includeReservas)
                {
                    return await _context.Viviendas.Include(c => c.Reservas)
                        .Where(c => c.Id == viviendaId).FirstOrDefaultAsync();
                }

                return await _context.Viviendas
                      .Where(c => c.Id == viviendaId).FirstOrDefaultAsync();
            }

            public async Task<(IEnumerable<Vivienda>, int)> GetViviendasAsync(string name)
            {
                var collection = _context.Viviendas as IQueryable<Vivienda>;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    name = name.Trim();
                    collection = collection.Where(c => c.Name == name);
                }
                var numViviendas = await collection.CountAsync();

                return (await collection.OrderBy(c => c.Name).ToListAsync(), numViviendas);
            }

            public async Task<IEnumerable<Reserva>> GetReservasParaViviendaAsync(int viviendaId)
            {
                return await _context.Reservas
                               .Where(r => r.ViviendaId == viviendaId).ToListAsync();
            }

            public async Task<Reserva> GetReservaParaViviendaAsync(int viviendaId, int reservaId)
            {
                return await _context.Reservas
                              .Where(r => r.ViviendaId == viviendaId && r.Id == reservaId)
                              .FirstOrDefaultAsync();
            }
            public async Task<bool> ViviendaExistsAsync(int viviendaId)
            {
                return await _context.Viviendas.AnyAsync(v => v.Id == viviendaId);
            }

            public async Task AddReservaParaViviendaAsync(int viviendaId,
                Reserva reserva)
            {
                var vivienda = await GetViviendaAsync(viviendaId, false);
                if (vivienda != null)
                {
                    vivienda.Reservas.Add(reserva);
                }
            }
            public async Task<bool> SaveChangesAsync()
            {
                return (await _context.SaveChangesAsync() >= 0);
            }

            public async void DeleteReserva(Reserva reserva)
            {
                _context.Reservas.Remove(reserva);
            }

            public async void AddVivienda(Vivienda vivienda)
            {
                _context.Viviendas.Add(vivienda);
            }

        public async void DeleteVivienda(int viviendaId)
        {
            var vivienda = await GetViviendaAsync(viviendaId, false);
            if (vivienda != null)
            {
                _context.Viviendas.Remove(vivienda);
            }
        }

        public void UpdateVivienda(Vivienda vivienda)
        {
             _context.Viviendas.Update(vivienda);
        }
    }
}
