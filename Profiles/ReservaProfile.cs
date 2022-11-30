using AutoMapper;

namespace ProyectoFinal.Profiles
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<Entities.Reserva, Models.ReservaDto>();
            CreateMap<Models.ReservaDto, Entities.Reserva>();

            CreateMap<Entities.Reserva, Models.ReservaCreateUpdateDto>();
            CreateMap<Models.ReservaCreateUpdateDto, Entities.Reserva>();


        }
    }
}
