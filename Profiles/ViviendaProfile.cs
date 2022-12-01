using AutoMapper;

namespace ProyectoViviendasFelipe.Profiles
{
    public class ViviendaProfile : Profile
    {
        public ViviendaProfile()
        {
            CreateMap<Entities.Vivienda, Models.ViviendaSinReservasDto>();
            CreateMap<Entities.Vivienda, Models.ViviendaDto>();
            CreateMap<Models.ViviendaSinReservasDto,Entities.Vivienda>();
            CreateMap<Models.ViviendaDto, Entities.Vivienda>();
        }
    }
}
