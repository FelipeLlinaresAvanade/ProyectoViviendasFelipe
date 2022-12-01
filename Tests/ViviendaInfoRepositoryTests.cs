using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProyectoViviendasFelipe.DbContexts;
using ProyectoViviendasFelipe.Entities;
using ProyectoViviendasFelipe.Services;

namespace ProyectoViviendasFelipe.Tests.RepositoryTests
{
    public class ViviendaInfoRepositoryTests : IDisposable
    {
        private ViviendaInfoRepository _viviendaInfoRepositoy;
        public void Dispose() { }

        public ViviendaInfoRepositoryTests()
        {
            var connection = new SqliteConnection("Data source=:memory:");
            connection.Open();
            var optionsBuilder = new DbContextOptionsBuilder<ViviendaInfoContext>().UseSqlite(connection);
            var dbContext = new ViviendaInfoContext(optionsBuilder.Options);
            dbContext.Database.Migrate();

            _viviendaInfoRepositoy = new ViviendaInfoRepository(dbContext);
        }

        [Fact]
        public async Task GetViviendas()
        {
            var viviendas = await _viviendaInfoRepositoy.GetViviendasAsync();

            Assert.NotNull(viviendas);
            Assert.Equal(2, viviendas.Count());
        }

        [Fact]
        public async Task GetViviendas_Paginadas()
        {
            int pageNumber = 1;
            int pageSize = 1;
            var (viviendas, metadata) = await _viviendaInfoRepositoy.GetViviendasAsync(pageNumber, pageSize);

            Assert.NotNull(viviendas);
            Assert.Equal(2, metadata.TotalPageCount);
            Assert.Equal(2, metadata.TotalItemCount);
            Assert.Equal(pageNumber, metadata.CurrentPage);
            Assert.Equal(pageSize, viviendas.Count());
        }

        [Fact]
        public async Task GetViviendas_PorID_SinReservas()
        {
            var id = 1;
            var vivienda = await _viviendaInfoRepositoy.GetViviendaAsync(id, false);

            Assert.NotNull(vivienda);
            Assert.Equal(id, vivienda.Id);
            Assert.Equal(0,vivienda.Reservas.Count);
        }

        [Fact]
        public async Task GetViviendas_PorID_ConReservas()
        {
            var id = 1;
            var vivienda = await _viviendaInfoRepositoy.GetViviendaAsync(id, true);

            Assert.NotNull(vivienda);
            Assert.Equal(id, vivienda.Id);
            Assert.Equal(2, vivienda.Reservas.Count);
        }

        [Fact]
        public async Task GetReservas_ViviendaNoExiste()
        {
            int id = 99;

            var reservas = await _viviendaInfoRepositoy.GetReservasParaViviendaAsync(id);

            Assert.Empty(reservas);
        }

        [Fact]
        public async Task GetReservas_ViviendaId()
        {
            int id = 2;

            var reservas = await _viviendaInfoRepositoy.GetReservasParaViviendaAsync(id);

            Assert.Equal(2,reservas.Count());
            Assert.Equal(2, reservas.First().ViviendaId);
            Assert.Equal(2, reservas.Last().ViviendaId);
        }

        [Fact]
        public async Task GetReserva_Vivienda()
        {
            int id = 1;
            int idReserva = 1;
            var reserva = await _viviendaInfoRepositoy.GetReservaParaViviendaAsync(id,idReserva);

            Assert.NotNull(reserva);
            Assert.Equal("Juan Gonzalez", reserva.NameUsuario);
            Assert.Equal(idReserva, reserva.Id);
            Assert.Equal(id, reserva.ViviendaId);

        }

        [Fact]
        public async Task Vivienda_Exists()
        {
            int id = 1;
            var exists = await _viviendaInfoRepositoy.ViviendaExistsAsync(id);

            Assert.True(exists);

        }

        [Fact]
        public async Task Vivienda_NoExists()
        {
            int id = 99;
            var exists = await _viviendaInfoRepositoy.ViviendaExistsAsync(id);

            Assert.False(exists);

        }

       

    }
}
