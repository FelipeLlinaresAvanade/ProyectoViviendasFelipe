using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Entities;

namespace ProyectoFinal.DbContexts
{
    
       public class ViviendaInfoContext : DbContext
        {
            public DbSet<Vivienda> Viviendas { get; set; } = null!;
            public DbSet<Reserva> Reservas { get; set; } = null!;

            public ViviendaInfoContext(DbContextOptions<ViviendaInfoContext> options)
                : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Vivienda>()
                    .HasData(
                    new Vivienda("Casa de Campo")
                    {
                        Id = 1,
                        Description = "Casa en las afueras muy grande",
                        Direccion = "Málaga, Marbella, Calle Lopez, n20",
                        Propietario = "Luis Gómez"
                    },
                    new Vivienda("Piso en Ciudad")
                    {
                        Id = 2,
                        Description = "Piso en el centro de malaga para turistas",
                        Direccion = "Málaga, Málaga, Calle Granada, n30",
                        Propietario = "José Pérez"
                    }
                    );

                modelBuilder.Entity<Reserva>()
                    .HasData(
                    new Reserva("Juan Gonzalez")
                    {
                        Id = 1,
                        FechaInicio = "10-10-2022",
                        FechaFin = "15-10-2022",
                        ViviendaId = 1
                    },
                    new Reserva("Javi Ruiz")
                    {
                        Id = 2,
                        FechaInicio = "10-10-2022",
                        FechaFin = "15-10-2022",
                        ViviendaId = 2
                    },
                    new Reserva("Juan Gonzalez")
                    {
                        Id = 3,
                        FechaInicio = "20-11-2022",
                        FechaFin = "27-11-2022",
                        ViviendaId = 2
                    },
                    new Reserva("Alberto Muñoz")
                    {
                        Id = 4,
                        FechaInicio = "11-11-2022",
                        FechaFin = "14-11-2022",
                        ViviendaId = 1
                    }
                    );

                base.OnModelCreating(modelBuilder);
            }
        }
}
