using AutoMapper;

namespace ProyectoFinal.Profiles
{
    public class ViviendaProfile : Profile
    {
        public ViviendaProfile()
        {
            CreateMap<Entities.Vivienda, Models.ViviendaSinReservasDto>();
            CreateMap<Entities.Vivienda, Models.ViviendaDto>();
            CreateMap<Models.ViviendaSinReservasDto,Entities.Vivienda>();
        }
    }
}
